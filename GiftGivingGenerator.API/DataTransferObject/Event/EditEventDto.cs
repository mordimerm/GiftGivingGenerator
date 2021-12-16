namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EditEventDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
}