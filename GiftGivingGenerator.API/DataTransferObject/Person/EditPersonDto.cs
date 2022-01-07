using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Person;

public class EditPersonDto
{
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	public string Name { get; set; }
}