namespace GiftGivingGenerator.API.DataTransferObject.DrawingResult;

public class ListOfExclusionsForOnePersonDto
{
	public Guid PersonId { get; set; }
	public List<Guid?> ExcludedId { get; set; }
}