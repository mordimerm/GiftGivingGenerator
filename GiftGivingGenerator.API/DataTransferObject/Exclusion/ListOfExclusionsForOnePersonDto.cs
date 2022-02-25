namespace GiftGivingGenerator.API.DataTransferObject.Exclusion;

public class ListOfExclusionsForOnePersonDto
{
	public Guid PersonId { get; set; }
	public IEnumerable<Guid>? ExcludedId { get; set; }
}