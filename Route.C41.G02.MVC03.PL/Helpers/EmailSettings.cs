using Route.C41.G02.DAL.Models;
using System.Net;
using System.Net.Mail;

namespace Route.C41.G02.MVC03.PL.Helpers
{
	public class EmailSettings
	{
		public static void SendEmail(Email email)
		{
			// Mail Server --> Gmail 
			var client = new SmtpClient("smtp.gmail.com", 587);
			
			client.EnableSsl = true;
			client.Credentials = new NetworkCredential("mohammedelgammal003@gmail.com", "yaxy apem gnkr vknl");

			client.Send("mohammedelgammal003@gmail.com", email.Recipients, email.Subject, email.Body);
		}

	}
}
