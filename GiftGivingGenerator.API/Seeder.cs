﻿using GiftGivingGenerator.API.Entities;
using GiftGivingGenerator.API.HashingPassword;
using GiftGivingGenerator.API.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace GiftGivingGenerator.API;

public class Seeder
{
	private readonly AppContext _dbContext;
	private readonly HashingOptions _options;
	private readonly IOrganizerRepository _organizerRepository;
	public Seeder(AppContext dbContext, HashingOptions options, IOrganizerRepository organizerRepository)
	{
		_dbContext = dbContext;
		_options = options;
		_organizerRepository = organizerRepository;
	}

	public void Seed()
	{
		if (!_dbContext.Organizer.Any())
		{
			_dbContext.Organizer.AddRange(GetOrganizers());
			_dbContext.SaveChanges();
		}

		AddAttendeesToEvents();
		_dbContext.SaveChanges();
	}

	private List<Organizer> GetOrganizers()
	{
		var organizers = new List<Organizer>()
		{
			new Organizer
			{
				Name = "Adrian",
				Email = "aczekaj.mat@gmail.com",
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
						EndDate = new DateTime(2022, 02, 14, 00, 00, 00),
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

			new Organizer
			{
				Name = "Maciek",
				Email = "maciek@gmail.com",
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

			new Organizer
			{
				Name = "Monika",
				Email = "monika@gmail.com",
				Password = new PasswordHasher(_options).Hash("monika"),
			},

			new Organizer
			{
				Name = "Gargamel",
				Email = "gargamel@gmail.com",
				Password = new PasswordHasher(_options).Hash("gargamel"),
			}
		};

		return organizers;
	}

	public void AddAttendeesToEvents()
	{
		var organizer = new Organizer();
		var @event = new Event();
		
		organizer = _dbContext.Organizer
			.Include(x => x.Events)
			.Include(x => x.Persons)
			.Single(x => x.Email == "maciek@gmail.com");
		@event = organizer.Events.Single(x => x.Name == "Boże Narodzenie");

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

		
		@event.AssignAttendees(persons);
		
		organizer = _dbContext.Organizer
			.Include(x => x.Events)
			.Include(x => x.Persons)
			.Single(x => x.Email == "aczekaj.mat@gmail.com");
		@event = organizer.Events.Single(x => x.Name == "Walentynki");

		persons = new List<Person>()
		{
			organizer.Persons.Single(x => x.Name == "S_Klaudia"),
			organizer.Persons.Single(x => x.Name == "S_Zofia"),
			organizer.Persons.Single(x => x.Name == "S_Ksawery"),
			organizer.Persons.Single(x => x.Name == "S_Łukasz"),
		};
		
		@event.AssignAttendees(persons);
	}
}