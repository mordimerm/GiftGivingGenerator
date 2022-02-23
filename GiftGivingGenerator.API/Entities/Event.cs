using GiftGivingGenerator.API.DataTransferObject.DrawingResult;
using MoreLinq;

namespace GiftGivingGenerator.API.Entities;

public class Event : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; } = DateTime.Now;
	public DateTime EndDate { get; set; }
	public bool? IsActive { get; set; } = true;
	public int? Budget { get; set; }
	public string? Message { get; set; }
	
	public Guid OrganizerId  { get; set; }
	public Person Organizer { get; set; }

	public List<Person> Persons { get; set; } = new List<Person>();
	public List<DrawingResult> DrawingResults { get; set; } = new List<DrawingResult>();
	public List<GiftWish> GiftWishes { get; set; }
	public List<Exclusion> Exclusions { get; set; } = new List<Exclusion>();

	//Maciek: Wheather the method below shouldn't be in the DrawingResultRepository?
	public static Event Create(Person organizer, string name, DateTime date, int? budget, string? message)
	{
		if (date < DateTime.Now)
		{
			throw new ArgumentException("Date must be later than now.");
		}

		var @event = new Event()
		{
			Organizer = organizer,
			Name = name,
			EndDate = date.Date,
			Budget = budget,
			Message = message,
		};

		return @event;
	}

	public void ChangeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Name can't be null.");
		}

		Name = name;
	}
	
	public void ChangeMessage(string? message)
	{
		Message = message;
	}
	
	public void ChangeBudget(int? budget)
	{
		Budget = budget;
	}
	
	public void ChangeEndDate(DateTime date)
	{
		if (date < DateTime.Now)
		{
			throw new ArgumentException("Date must be later then now.");
		}

		EndDate = date;
	}
	public void AssignAttendees(List<Person> persons)
	{
		if (DrawingResults.Count > 0)
		{
			throw new Exception("The drawing results was generated, can't change attendees.");
		}

		var exceptedPersons = Persons.Except(persons);
		var exclusions = Exclusions
			.Where(x => exceptedPersons.Contains(x.Person) || exceptedPersons.Contains(x.Excluded))
			.ToList();
		Exclusions.RemoveAll(x => exclusions.Contains(x));
		
		Persons.Clear();
		if (persons.Count != 0)
		{
			Persons.AddRange(persons);
		}
	}
	public void Deactivate()
	{
		IsActive = false;
	}
	public int DrawResultsAndNumberTries()
	{
		if (DrawingResults.Count != 0)
		{
			throw new Exception("The draw was genereted. Can't do it second time.");
		}

		var personsIds = Persons
			.Select(x => x.Id)
			.ToList();

		if (personsIds.Count <2)
		{
			throw new Exception("There must be minimum 2 persons to generate drawing results.");
		}

		var permutationA = MoreEnumerable.Shuffle(personsIds).ToList();
		var permutationB = new List<Guid>();

		var i = 0;
		var numberOfTries = 0;
		do
		{
			numberOfTries += 1;
			permutationB = MoreEnumerable.Shuffle(personsIds).ToList();
			for (i = 0; i < permutationA.Count; i++)
			{
				var exclusionsAi = Exclusions
					.Where(x => x.PersonId == permutationA[i])
					.Select(x=>x.ExcludedId)
					.ToList();
				if (permutationA[i] == permutationB[i] || exclusionsAi.Contains(permutationB[i]))
				{
					break;
				}
			}
		}
		while (personsIds.Count != i);

		for (int j = 0; j < personsIds.Count; j++)
		{
			var drawingResult = new DrawingResult
			{
				GiverPersonId = permutationA[j],
				RecipientPersonId = permutationB[j],
			};

			DrawingResults.Add(drawingResult);
		}

		return numberOfTries;
	}
	public void InsertExclusions(List<ListOfExclusionsForOnePersonDto> dto)
	{
		foreach (var personDto in dto)
		{
			foreach (Guid excludedId in personDto.ExcludedId)
			{
				var exclusion = new Exclusion()
				{
					PersonId = personDto.PersonId,
					ExcludedId = excludedId,
				};
				
				Exclusions.Add(exclusion);
			}
		}
	}
}