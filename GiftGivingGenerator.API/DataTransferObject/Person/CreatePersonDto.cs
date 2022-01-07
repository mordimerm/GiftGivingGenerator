using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class CreatePersonDto
{
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	public string Name { get; set; }
}