using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;

namespace GiftGivingGenerator.API.DataTransferObject.Event;

public class CreateEventDto
{
	[Required(ErrorMessage = "Name is required"),
	 MinLength(4, ErrorMessage = "Min length is 4 chars."),
	 MaxLength(30, ErrorMessage = "Max length is 30 chars.")]
	public string Name { get; set; }
	
	public DateTime EndDate { get; set; }
}