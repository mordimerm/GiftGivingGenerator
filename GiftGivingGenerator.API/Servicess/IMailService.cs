namespace GiftGivingGenerator.API.Servicess;

public interface IMailService
{
	void Send(string to, string subject, string body);
}