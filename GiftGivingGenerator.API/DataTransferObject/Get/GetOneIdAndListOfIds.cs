namespace GiftGivingGenerator.API.DataTransferObject.Get;

public class GetOneIdAndListOfIds
{
	public Guid Id { get; set; }
	public List<Guid> Ids { get; set; }
}