namespace GiftGivingGenerator.API.DataTransferObject.Events;

public class PrintListOfEventsDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
}