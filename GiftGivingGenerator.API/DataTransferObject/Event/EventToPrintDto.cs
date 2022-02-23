using GiftGivingGenerator.API.DataTransferObject.Person;
namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventToPrintDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime CreatingDate { get; set; }
	public DateTime EndDate { get; set; }
	public int? Budget { get; set; }
	public string? Message { get; set; }
	public virtual IEnumerable<PersonToPrintingEventDto> Persons { get; set; }
}