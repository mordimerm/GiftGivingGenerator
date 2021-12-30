using GiftGivingGenerator.API.Repositories.Abstractions;

namespace GiftGivingGenerator.API.Servicess;

public class AuthorizationServicee
{
	private readonly IOrganizerRepository _repository;
	public AuthorizationServicee(IOrganizerRepository repository)
	{
		_repository = repository;
	}

	public Guid AuthorizateAndGetId(string email, string password)
	{
		var organizer = _repository.GetByEmail(email);

		if (organizer.Password != password)
		{
			throw new Exception("The password is incorrect.");
		}

		return organizer.Id;
	}
}