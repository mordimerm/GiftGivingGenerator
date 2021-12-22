namespace GiftGivingGenerator.API.Entities;

public class Person : PersonBase
{
	public Guid OrganizerId { get; set; }
	public Organizer Organizer { get; set; }
	public bool? IsActive { get; set; } = true;
}