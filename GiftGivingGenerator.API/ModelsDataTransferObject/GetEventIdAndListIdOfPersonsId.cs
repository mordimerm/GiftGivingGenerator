namespace GiftGivingGenerator.API.ModelsDataTransferObject;

public class GetEventIdAndListIdOfPersons
{
	public Guid EventId { get; set; }
	public List<Guid> PersonsId { get; set; }
}