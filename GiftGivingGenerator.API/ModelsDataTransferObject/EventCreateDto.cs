using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.ModelsDataTransferObject;

public class CreateEventDto
{
	[Required]
	[MaxLength (50)]
	public string Name { get; set; }
	[Required]
	public DateTime EndDate { get; set; }
}