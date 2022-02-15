namespace GiftGivingGenerator.API.Entities;

public class Person : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; protected internal set; }

	public string? Email { get; set; }
	
	public List<Event> CreatedEvents { get; set; } = new List<Event>();

	public List<Event> Events { get; set; } = new List<Event>();

	public void ChangeName(string name)
	{
		if (string.IsNullOrWhiteSpace(name))
		{
			throw new ArgumentException("Name can't be null.");
		}
			
		Name = name;
	}
}