namespace GiftGivingGenerator.API.Entities;

public class Person : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; protected internal set; }
	public string? Email { get; set; }
	public virtual List<Event> CreatedEvents { get; set; } = new List<Event>();
	public virtual List<Event> Events { get; set; } = new List<Event>();
	public virtual List<Exclusion> Exclusions { get; set; }

	public static Person Create(string name, string email)
	{
		var person = new Person()
		{
			Name = name,
			Email = email
		};

		return person;
	}
	
	public void ChangeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Name can't be null.");
		}

		Name = name;
	}
}