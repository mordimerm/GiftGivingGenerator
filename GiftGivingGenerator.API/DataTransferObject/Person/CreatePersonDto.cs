using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class CreatePersonDto
{
	[Required]
	[MinLength(3)]
	[MaxLength(30)]
	public string Name { get; set; }
	[EmailAddress]
	public string? Email { get; set; }
}