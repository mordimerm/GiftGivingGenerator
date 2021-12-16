namespace GiftGivingGenerator.API.ModelsDataTransferObject;

public class EditEventDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public DateTime EndDate { get; set; }
}