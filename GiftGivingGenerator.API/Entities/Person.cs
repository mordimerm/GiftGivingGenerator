using GiftGivingGenerator.API.DataTransferObject.Person;

namespace GiftGivingGenerator.API.Entities;

public class Person : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; protected internal set; }

	public string? Email { get; set; }

	public List<Event> CreatedEvents { get; set; } = new List<Event>();

	public List<Event> Events { get; set; } = new List<Event>();

	public static List<Person> CreateMany(List<CreatePersonDto> personsDtos)
	{
		var persons = new List<Person>();
		foreach (var personDto in personsDtos)
		{
			var person = new Person()
			{
				Name = personDto.Name,
				Email = personDto.Email
			};
			persons.Add(person);
		}

		return persons;
	}

	public void ChangeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Name can't be null.");
		}

		Name = name;
	}
	public static Person Create(string name, string? email)
	{
		var person = new Person()
		{
			Name = name,
			Email = email,
		};

		return person;
	}
}