namespace GiftGivingGenerator.API.DataTransferObject.Events;

public class ListOfEventsDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
}