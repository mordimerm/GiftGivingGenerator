using GiftGivingGenerator.API.DataTransferObject.Email;

namespace GiftGivingGenerator.API.Servicess;

public interface IMailService
{
	void Send(List<Mail> mails);
	void Send(Mail mail);
}