using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Organizer;

public class CreateOrganizerDto
{
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	public string Name { get; set; }
	[EmailAddress]
	public string Email { get; set; }
}