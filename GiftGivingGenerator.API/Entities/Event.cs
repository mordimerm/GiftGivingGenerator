namespace GiftGivingGenerator.API.Entities;

public class Event
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; }
	public DateTime EndDate { get; set; }
	//TODO: EndData and after that the event will be moved to archive

	
	
	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public List<Person> Persons { get; set; } = new List<Person>();
	public List<DrawingResult> DrawingResults { get; set; }	
}