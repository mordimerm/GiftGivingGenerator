using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class EditPersonDto
{
	[Required(ErrorMessage = "Name is required"),
	 MinLength(4, ErrorMessage = "Min length is 4 chars."),
	 MaxLength(30, ErrorMessage = "Max length is 30 chars.")]
	public string Name { get; set; }
}