using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using MimeKit;
using MailKit.Net.Smtp;

namespace Profibiz.PracticeManager.Service
{
	public static class EmailFunc
	{
		public static string SendEmail(string email, string subject, string html)
		{
			try
			{
				var sendRealEmail = ConfigurationManager.AppSettings["SendRealEmail"];
				if (sendRealEmail != "1")
				{
					return "";
				}

				var message = new MimeMessage();
				var fromName = ConfigurationManager.AppSettings["smtp.from.name"];
				var fromAddress = ConfigurationManager.AppSettings["smtp.from.address"];
				message.From.Add(new MailboxAddress(fromName, fromAddress));
				message.Subject = subject ?? "";
				message.To.Add(new MailboxAddress(email));

				var builder = new BodyBuilder();
				builder.HtmlBody = html ?? "";
				message.Body = builder.ToMessageBody();



				var url = ConfigurationManager.AppSettings["smtp.url"];
				var port = Int32.Parse(ConfigurationManager.AppSettings["smtp.port"]);
				var username = ConfigurationManager.AppSettings["smtp.username"];
				var password = ConfigurationManager.AppSettings["smtp.password"];

				var client = new SmtpClient();
				client.Connect(url, port);
				//client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate(username, password);

				client.Send(message);

				return "";
			}
			catch (Exception ex)
			{
				var errorMessage = ex.ToString();
				//return "";
				return errorMessage;
			}
		}
	}
}
