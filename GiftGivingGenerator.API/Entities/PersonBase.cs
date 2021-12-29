namespace GiftGivingGenerator.API.Entities;

public class PersonBase : IEntity
{
	public Guid Id { get; set; }
	public string Name { get; protected internal set; }
	
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