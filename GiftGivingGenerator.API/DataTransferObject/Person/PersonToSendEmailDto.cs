using GiftGivingGenerator.API.DataTransferObject.Event;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class PersonToSendEmailDto
{
	public string Name { get; set; }
	public string Email { get; set; }
	public List<EventWithDrawingResultDto> Events { get; set; }
}