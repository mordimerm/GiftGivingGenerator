using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.Models;

public class GetName
{
	[Required]
	[MaxLength(25)]
	public string Name { get; set; }
}