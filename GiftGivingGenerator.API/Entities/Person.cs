namespace GiftGivingGenerator.API.Entities;

public class Person
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public List<Event> Events { get; set; }
}