using GiftGivingGenerator.API.DataTransferObject.Email;

namespace GiftGivingGenerator.API.Servicess;

public interface IEmailService
{
	void Send(List<Email> emails);
	void Send(Email email);
}