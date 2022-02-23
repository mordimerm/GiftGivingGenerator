using GiftGivingGenerator.API.DataTransferObject.DrawingResult;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class PersonToPrintingEventDto
{
	public Guid Id { get; set; }
	public string Name { get; set; }
	public string? Email { get; set; }
	public List<ExclusionToPrintingEventDto> Exclusions { get; set; }
}