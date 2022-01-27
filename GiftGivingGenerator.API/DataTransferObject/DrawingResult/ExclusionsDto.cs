namespace GiftGivingGenerator.API.DataTransferObject.DrawingResult;

public class ExclusionsDto
{
	public Guid ExcluderingId { get; set; }
	public List<Guid?> ExcludedId { get; set; }
}