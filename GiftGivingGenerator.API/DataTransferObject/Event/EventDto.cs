using GiftGivingGenerator.API.DataTransferObject.Person;
namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; }
	public DateTime EndDate { get; set; }
	public int? Budget { get; set; }
	public string? Message { get; set; }
	public string OrganizerName { get; set; }
	public virtual IEnumerable<PersonDto> Persons { get; set; }
}