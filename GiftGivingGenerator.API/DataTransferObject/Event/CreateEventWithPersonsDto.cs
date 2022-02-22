using System.ComponentModel.DataAnnotations;
using GiftGivingGenerator.API.DataTransferObject.Person;
using GiftGivingGenerator.API.Validations;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class CreateEventWithPersonsDto
{
	[Required]
	[MinLength(2)]
	[MaxLength(30)]
	public string OrganizerName { get; set; }
	[Required]
	public string OrganizerEmail { get; set; }
	
	[Required]
	[MinLength(3)]
	[MaxLength(30)]
	public string EventName { get; set; }
	
	[DateValidator]
	public DateTime EndDate { get; set; }
	public int? Budget { get; set; }
	public string? Message { get; set; }
	public List<CreatePersonDto> Persons { get; set; }
}