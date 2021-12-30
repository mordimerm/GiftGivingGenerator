using MoreLinq;

namespace GiftGivingGenerator.API.Entities;

public class Event : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; private set; }
	public DateTime CreatingDate { get; set; } = DateTime.Now;
	public DateTime EndDate { get; set; }
	public bool? IsActive { get; set; } = true;


	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public List<Person> Persons { get; set; } = new List<Person>();
	public List<DrawingResult> DrawingResults { get; set; } = new List<DrawingResult>();

	public static Event Create(Guid organizerId, string name, DateTime date)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Name can't be null.");
		}

		if (date < DateTime.Now)
		{
			throw new ArgumentException("Date must be later then now.");
		}

		var @event = new Event()
		{
			OrganizerId = organizerId,
			Name = name,
			EndDate = date.Date,
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
	public void DrawResults()
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
		do
		{
			permutationB = MoreEnumerable.Shuffle(personsIds).ToList();
			for (i = 0; i < permutationA.Count; i++)
			{
				if (permutationA[i] == permutationB[i])
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
	}
}