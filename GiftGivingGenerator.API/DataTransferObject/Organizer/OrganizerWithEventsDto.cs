using GiftGivingGenerator.API.DataTransferObject.Event;
using GiftGivingGenerator.API.DataTransferObject.Person;

namespace GiftGivingGenerator.API.DataTransferObject.Organizer;

public class OrganizerWithEventsDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public List<EventToListDto> Events { get; set; }
}