using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Servicess;

public class AuthorizationService
{
	private readonly IOrganizerRepository _repository;
	public AuthorizationService(IOrganizerRepository repository)
	{
		_repository = repository;
	}

	public Guid Id { get; set; }
	public DateTime AuthorizationDate { get; set; } = DateTime.UtcNow;
	public string Status { get; set; } = "Ok";

	public Guid AuthorizateAndGetId(string email, string password)
	{
		var organizer = _repository.GetByEmail(email);

		if (organizer==null)
		{
			Status = "Wrong email";
			throw new Exception("Email address is incorrect.");
		}
		
		if (organizer.Password != password)
		{
			Status = "Wrong password.";
			throw new Exception("The password is incorrect.");
		}

		return organizer.Id;
	}
}