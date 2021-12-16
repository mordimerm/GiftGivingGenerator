using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Get;

public class GetName
{
	[Required]
	[MaxLength(25)]
	public string Name { get; set; }
}