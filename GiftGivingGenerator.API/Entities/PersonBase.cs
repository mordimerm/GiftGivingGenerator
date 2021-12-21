namespace GiftGivingGenerator.API.Entities;

public class PersonBase
{
	public Guid Id { get; set; }
	public string Name { get; set; }



	public List<Event> Events { get; set; } = new List<Event>();
}