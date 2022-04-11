using System.Net.Mail;
using GiftGivingGenerator.API.Configurations;
using GiftGivingGenerator.API.DataTransferObject.Email;
using Microsoft.Extensions.Options;
using Serilog;

namespace GiftGivingGenerator.API.Servicess;

public class MailService : IMailService
{
	private readonly MailConfiguration _options;
	public MailService(IOptionsMonitor<MailConfiguration> options)
	{
		_options = options.CurrentValue;
	}

	public void Send(Mail mail)
	{
		var mails = new List<Mail>();
		mails.Add(mail);
		Send(mails);
	}

	public void Send(List<Mail> mails)
	{
		var basicCredential1 = new System.Net.NetworkCredential(_options.userName, _options.password);
		var client = new SmtpClient("smtp.gmail.com", 587)
		{
			EnableSsl = true,
			UseDefaultCredentials = false,
			Credentials = basicCredential1,
		};
		
		foreach (var mail in mails)
		{
			var message = new MailMessage(_options.userName, mail.Recipient, mail.Subject, mail.Body)
			{
				IsBodyHtml = true,
			};

			try
			{
				client.Send(message);
			}

			catch (Exception ex)
			{
				Log.Warning(ex.Message);
				throw;
			}
		}
	}
}