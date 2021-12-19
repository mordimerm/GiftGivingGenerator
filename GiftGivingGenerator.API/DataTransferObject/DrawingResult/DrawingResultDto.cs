namespace GiftGivingGenerator.API.DataTransferObject.DrawingResult;

public class DrawingResultDto
{
	public Guid GiverPersonId { get; set; }
	public Guid RecipientPersonId { get; set; }
	public string GiverPersonName { get; set; }
	public string RecipientPersonName { get; set; }
}