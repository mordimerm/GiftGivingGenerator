using System.Net.Mail;
using GiftGivingGenerator.API.Configurations;
using Microsoft.Extensions.Options;

namespace GiftGivingGenerator.API.Servicess;

public class MailService : IMailService
{
	private readonly MailConfiguration _options;
	public MailService(IOptionsMonitor<MailConfiguration> options)
	{
		_options = options.CurrentValue;
	}
	public void Send(string to, string subject, string body)
	{
		var message = new MailMessage(_options.userName, to, subject, body)
		{
			IsBodyHtml = true,
		};

		var basicCredential1 = new System.Net.NetworkCredential(_options.userName, _options.password);
		var client = new SmtpClient("smtp.gmail.com", 587)
		{
			EnableSsl = true,
			UseDefaultCredentials = false,
			Credentials = basicCredential1,
		};
		
		try
		{
			client.Send(message);
		}

		catch (Exception ex)
		{
			throw ex;
		}
	}
}