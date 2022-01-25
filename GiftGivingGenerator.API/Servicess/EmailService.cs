using System.Net.Mail;
using System.Text;

namespace GiftGivingGenerator.API.Servicess;

public class EmailService
{
	public void Send(string userName, string password, string to, string subject, string body)
	{
		MailMessage message = new MailMessage(userName, to, subject, body);

		System.Net.NetworkCredential basicCredential1 = new
			System.Net.NetworkCredential(userName, password);
		SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
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