using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Get;

public class CreatePersonDto
{
	[Required(ErrorMessage = "Name is required"),
	 MinLength(4, ErrorMessage = "Min length is 4 chars."),
	 MaxLength(30, ErrorMessage = "Max length is 30 chars.")]
	public string Name { get; set; }
}