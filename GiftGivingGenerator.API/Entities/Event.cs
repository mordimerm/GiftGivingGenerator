namespace GiftGivingGenerator.API.Entities;

public class Event
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; } = DateTime.Now;
	public DateTime EndDate { get; set; }
	public bool? IsActive { get; set; }


	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public List<Person> Persons { get; set; } = new List<Person>();
	public List<DrawingResult> DrawingResults { get; set; }	
}