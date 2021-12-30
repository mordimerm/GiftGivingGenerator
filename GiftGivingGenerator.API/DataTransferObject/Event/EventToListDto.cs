namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EventToListDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
	public bool IsActive { get; set; }
}