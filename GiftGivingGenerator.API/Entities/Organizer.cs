namespace GiftGivingGenerator.API.Entities;

public class Organizer
{
	public Guid Id { get; set; }
	
	public string Email { get; set; }

	public string Name { get; set; }

	public List<Event> Events { get; set; } = new List<Event>();
	
}