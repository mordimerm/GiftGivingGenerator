using System.Collections;

namespace GiftGivingGenerator.API.DataTransferObject.DrawingResult;

public class ExclusionsDto
{
	public Guid PersonId { get; set; }
	public List<Guid?> ExcludedId { get; set; }
}