using System.ComponentModel.DataAnnotations;
using GiftGivingGenerator.API.Validations;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class EditEventDto
{
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	public string Name { get; set; }

	[DateValidator]
	public DateTime Date { get; set; }
	
	public string? Message { get; set; }
	public int? Budget { get; set; }
}