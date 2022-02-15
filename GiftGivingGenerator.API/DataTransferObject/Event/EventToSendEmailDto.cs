namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventToSendEmailDto
{
	public string Name { get; set; }
	public Guid OrganizerId { get; set; }
}