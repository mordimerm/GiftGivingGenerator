using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class CreateEventDto
{
	[Required]
	[MaxLength (50)]
	public string Name { get; set; }
	[Required]
	public DateTime EndDate { get; set; }
}