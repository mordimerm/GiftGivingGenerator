using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace GiftGivingGenerator.API.DataTransferObject.Organizer;

public class OrganizerAuthorizationDto
{
	[EmailAddress]
	public string Email { get; set; }
	[Required]
	[MinLength(4)]
	[MaxLength(30)]
	[PasswordPropertyText(true)]
	public string Password { get; set; }
}