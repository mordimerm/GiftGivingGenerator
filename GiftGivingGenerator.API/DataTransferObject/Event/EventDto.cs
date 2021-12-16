using GiftGivingGenerator.API.DataTransferObject.Person;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
	public IEnumerable<PersonDto> Persons { get; set; }
}