using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BankproBPDomain.Managers
{
	public class EmailManager
	{
		private readonly IConfiguration _configuration;

		public EmailManager(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		public async Task SendEmail(List<string> emails, string subject, string body)
		{
			using (var client = new SmtpClient())
			{
				var credential = new NetworkCredential
				{
					UserName = _configuration["Email:userName"],
					Password = _configuration["Email:password"]
				};
				client.Credentials = credential;
				client.Host = _configuration["Email:host"];
				client.Port = int.Parse(_configuration["Email:port"]);
				client.EnableSsl = (_configuration["Email:useSSL"] == "Y") ? true : false;
				client.DeliveryMethod = SmtpDeliveryMethod.Network;
				if (_configuration["Email:deliveryMethod"] == "SpecifiedPickupDirectory")
				{
					client.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
					client.PickupDirectoryLocation = _configuration["Email:pickupDirectoryLocation"];
					client.EnableSsl = false;
				}
				using (var mailMessage = new MailMessage())
				{
					mailMessage.From = new MailAddress(_configuration["Email:from"]);
					emails.ForEach(e => mailMessage.To.Add(e));
					mailMessage.Subject = subject;
					mailMessage.Body = body;
					mailMessage.IsBodyHtml = true;
					client.Send(mailMessage);
				}
			}

			await Task.CompletedTask;
		}
	}
}
