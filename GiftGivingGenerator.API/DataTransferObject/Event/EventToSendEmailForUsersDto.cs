using GiftGivingGenerator.API.DataTransferObject.Person;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventToSendEmailForUsersDto
{
	public string Name { get; set; }
	public Entities.Person Organizer { get; set; }
	
	public Guid OrganizerId { get; set; }
	public virtual IEnumerable<CreatePersonDto> Persons { get; set; }
	
	public List<Entities.Exclusion> Exclusions { get; set; }
}