using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.HashingPassword;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API;

public class Seeder : ISeeder
{
	private readonly AppContext _dbContext;
	private readonly HashingOptions _options;
	public Seeder(AppContext dbContext, HashingOptions options)
	{
		_dbContext = dbContext;
		_options = options;
	}

	public void RemoveAllDataInDb()
	{
		var events = _dbContext.Events.ToList();
		foreach (var @event in events)
		{
			var persons = _dbContext.Events
				.Include(x => x.Persons)
				.Single(x => x.Id == @event.Id)
				.Persons
				.ToList();
			foreach (var person in persons)
			{
				@event.Persons.Remove(person);
			}

			@event.Persons.RemoveAll(x => persons.Contains(x));
		}

		_dbContext.Persons.RemoveRange(_dbContext.Persons);
		_dbContext.DrawingResults.RemoveRange(_dbContext.DrawingResults);
		_dbContext.Events.RemoveRange(_dbContext.Events);
		_dbContext.Organizer.RemoveRange(_dbContext.Organizer);
		_dbContext.SaveChanges();
	}

	public void Seed()
	{
		if (!_dbContext.Organizer.Any())
		{
			_dbContext.Organizer.AddRange(GetOrganizers());
			_dbContext.SaveChanges();
		}

		//Asociate attendees to event: Maciek_ - Boże Narodzenie
		var organizer = _dbContext.Organizer
			.Include(x => x.Events)
			.ThenInclude(x => x.Persons)
			.Include(x => x.Persons)
			.Single(x => x.Email == "aczekaj.mat+test2@gmail.com");
		var @event = organizer.Events.Single(x => x.Name == "Boże Narodzenie");
		if (!@event.Persons.Any())
		{
			@event.AssignAttendees(GetPersonToMaciek(organizer));
			_dbContext.Update(@event);
			_dbContext.SaveChanges();
		}
		
		//Add exclusions
		organizer = _dbContext.Organizer
			.Include(x => x.Events)
			.ThenInclude(x=>x.Exclusions)
			.Include(x => x.Persons)
			.Single(x => x.Email == "aczekaj.mat+test2@gmail.com");
		@event = organizer.Events.Single(x => x.Name == "Boże Narodzenie");
		@event.Exclusions.AddRange(GetExclusionToMaciek(organizer));
		_dbContext.SaveChanges();
		
		//Asociate attendees to event: Adrian - Walentynki
		organizer = _dbContext.Organizer
			.Include(x => x.Events)
			.ThenInclude(x=>x.Persons)
			.Include(x => x.Persons)
			.Single(x => x.Email == "aczekaj.mat+test1@gmail.com");
		@event = organizer.Events.Single(x => x.Name == "Walentynki");
		if (!@event.Persons.Any())
		{
			@event.AssignAttendees(GetPersonToAdrian(organizer));
			_dbContext.Update(@event);
			_dbContext.SaveChanges();
		}
	}

	private List<Organizer> GetOrganizers()
	{
		var organizers = new List<Organizer>()
		{
			//Create organizer Adrian with 3 events and 6 persons
			new Organizer
			{
				Name = "Adrian",
				Email = "aczekaj.mat+test1@gmail.com",
				Password = new PasswordHasher(_options).Hash("adrian"),
				Events = new List<Event>()
				{
					new Event
					{
						Name = "Boże Narodzenie",
						EndDate = new(2022, 12, 24, 00, 00, 00),
					},
					new Event
					{
						Name = "Walentynki",
						EndDate = new DateTime(2021, 02, 14, 00, 00, 00),
					},
					new Event
					{
						Name = "Wielkanoc",
						EndDate = new DateTime(2022, 04, 20, 00, 00, 00),
					},
				},
				Persons = new List<Person>()
				{
					new Person() {Name = "S_Ewa"},
					new Person() {Name = "S_Klaudia"},
					new Person() {Name = "S_Zofia"},
					new Person() {Name = "S_Ksawery"},
					new Person() {Name = "S_Łukasz"},
					new Person() {Name = "S_Mateusz"},
				}
			},

			//Create organizer Maciek_ with 2 events and 12 persons
			new Organizer
			{
				Name = "Maciek",
				Email = "aczekaj.mat+test2@gmail.com",
				Password = new PasswordHasher(_options).Hash("maciek"),
				Events = new List<Event>()
				{
					new Event
					{
						Name = "Boże Narodzenie",
						EndDate = new DateTime(2022, 12, 24, 00, 00, 00),
					},
					new Event
					{
						Name = "Wielkanoc",
						EndDate = new DateTime(2022, 04, 18, 00, 00, 00),
					},
				},
				Persons = new List<Person>()
				{
					new Person() {Name = "Władysław"},
					new Person() {Name = "Krystyna"},
					new Person() {Name = "Krzysztof"},
					new Person() {Name = "Adrian"},
					new Person() {Name = "Justyn"},
					new Person() {Name = "Daniel"},
					new Person() {Name = "Maciek"},
					new Person() {Name = "Kamil"},
					new Person() {Name = "Dagmara"},
					new Person() {Name = "Monika"},
					new Person() {Name = "Justyna"},
					new Person() {Name = "Daria"},
				}
			},

			//Create organizer Monika with 0 events and 0 persons
			new Organizer
			{
				Name = "Monika",
				Email = "aczekaj.mat+test3@gmail.com",
				Password = new PasswordHasher(_options).Hash("monika"),
			},

			//Create organizer Gargamel with 0 events and 0 persons
			new Organizer
			{
				Name = "Gargamel",
				Email = "aczekaj.mat+test4@gmail.com",
				Password = new PasswordHasher(_options).Hash("gargamel"),
			}
		};

		return organizers;
	}

	public List<Person> GetPersonToMaciek(Organizer organizer)
	{
		var persons = new List<Person>()
		{
			organizer.Persons.Single(x => x.Name == "Władysław"),
			organizer.Persons.Single(x => x.Name == "Krystyna"),
			organizer.Persons.Single(x => x.Name == "Krzysztof"),
			organizer.Persons.Single(x => x.Name == "Adrian"),
			organizer.Persons.Single(x => x.Name == "Justyn"),
			organizer.Persons.Single(x => x.Name == "Daniel"),
			organizer.Persons.Single(x => x.Name == "Maciek"),
			organizer.Persons.Single(x => x.Name == "Kamil"),
			organizer.Persons.Single(x => x.Name == "Dagmara"),
			organizer.Persons.Single(x => x.Name == "Monika"),
			organizer.Persons.Single(x => x.Name == "Justyna"),
			organizer.Persons.Single(x => x.Name == "Daria"),
		};
		return persons;
	}

	public List<Person> GetPersonToAdrian(Organizer organizer)
	{
		var persons = new List<Person>()
		{
			organizer.Persons.Single(x => x.Name == "S_Klaudia"),
			organizer.Persons.Single(x => x.Name == "S_Zofia"),
			organizer.Persons.Single(x => x.Name == "S_Ksawery"),
			organizer.Persons.Single(x => x.Name == "S_Łukasz"),
		};

		return persons;
	}

	public List<Exclusion> GetExclusionToMaciek(Organizer organizer)
	{
		var exclusionsss = new List<Exclusion>()
		{
			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Władysław"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krystyna"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Władysław"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krzysztof"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Władysław"),
				Excluded = organizer.Persons.Single(x => x.Name == "Justyn"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Krzysztof"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krystyna"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Krystyna"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krzysztof"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Justyn"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krystyna"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Justyn"),
				Excluded = organizer.Persons.Single(x => x.Name == "Krzysztof"),
			},

			new Exclusion
			{
				Person = organizer.Persons.Single(x => x.Name == "Justyn"),
				Excluded = organizer.Persons.Single(x => x.Name == "Władysław"),
			},
		};

		return exclusionsss;
	}


}