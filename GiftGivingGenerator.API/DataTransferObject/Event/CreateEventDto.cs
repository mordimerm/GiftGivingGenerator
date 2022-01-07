using System.ComponentModel.DataAnnotations;
using GiftGivingGenerator.API.Validations;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class CreateEventDto
{
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	public string Name { get; set; }
	[DateValidator]
	public DateTime EndDate { get; set; }
}