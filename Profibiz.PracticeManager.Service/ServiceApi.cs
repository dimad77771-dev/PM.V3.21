using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.IO;
using MimeKit;
using System.Configuration;
using MailKit.Net.Smtp;
using Plivo.API;
using System.Net;

namespace Profibiz.PracticeManager.Service
{
    public class ServiceApi
    {
		public void OneCycle()
		{
			var db = EF.PracticeManagerEntities.GetConnection(null, useLogFile: false);

			var qry =
				from a in db.AppointmentRemaindersForProcesseds
				join b in db.AppointmentsV on a.AppointmentRowId equals b.RowId
				join p in db.Patients on b.PatientRowId equals p.RowId
				join s in db.ServiceProviders on b.ServiceProviderRowId equals s.RowId
				join m in db.MedicalServicesOrSupplies on b.MedicalServicesOrSupplyRowId equals m.RowId
				select new { Remainder = a, Appointment = b, Patient = p, ServiceProvider = s, MedicalService = m }
				;
			var rows = qry.ToArray();
			foreach(var row in rows)
			{
				var template_files = new[] { "sms.txt", "email-subject.txt", "email.htm" };
				var rez_text = new List<string>();
				for (int k = 0; k < template_files.Length; k++)
				{
					var text = GetTemplate(template_files[k]);

					text = text
						.Replace("{{Patient.FullName}}", row.Patient.FullName)
						.Replace("{{Patient.Title}}", row.Patient.Title)
						.Replace("{{Patient.FirstName}}", row.Patient.FirstName)
						.Replace("{{Patient.MiddleName}}", row.Patient.MiddleName)
						.Replace("{{Patient.LastName}}", row.Patient.LastName)
						.Replace("{{Patient.Sex}}", row.Patient.Sex)
						.Replace("{{Specialist.FullName}}", row.ServiceProvider.FullName)
						.Replace("{{Specialist.Title}}", row.ServiceProvider.Title)
						.Replace("{{Specialist.FirstName}}", row.ServiceProvider.FullName)
						.Replace("{{Specialist.LastName}}", row.ServiceProvider.LastName)
						.Replace("{{Service.Name}}", row.MedicalService.Name)
						.Replace("{{Appointment.StartTime}}", row.Appointment.Start.ToString("HH:mm"))
						.Replace("{{Appointment.FinishTime}}", row.Appointment.Finish.ToString("HH:mm"))
						.Replace("{{Appointment.AppointmentDate}}", row.Appointment.Start.ToString(@"MM\/dd\/yyyy"))
						.Replace("{{Appointment.DurationInMinutes}}", "" + (row.Appointment.Finish - row.Appointment.Start).TotalMinutes)
						.Replace("{{Appointment.Notes}}", row.Appointment.Notes)
						.Replace("{{Appointment.Description}}", row.Appointment.Description);
					rez_text.Add(text);
				}

				var sms_text = rez_text[0];
				var email_subject = rez_text[1];
				var email_text = rez_text[2];

				var logheader0 = "Appointment: " + row.Appointment.Start + " - " + row.Appointment.Finish
					+ "; Specialist: " + row.ServiceProvider.FullName
					+ "; Patient: " + row.Patient.FullName;
				//+ " (" + (type == ReminderType.Email ? "email" : "sms") + ")";

				var emailAddress = row.Patient.EmailAddress;
				if (row.Appointment.IsRemainderEmail && !string.IsNullOrEmpty(emailAddress))
				{
					var logheader = logheader0 + "(email)" + ": ";
					NLog.Debug(logheader + "START");
					var err = SendEmail(emailAddress, email_subject, email_text);
					if (string.IsNullOrEmpty(err))
					{
						NLog.Debug(logheader + "START");
						SetRemainderIsProcessedEmail(db, row.Remainder.RowId);
					}
					else
					{
						NLog.Debug(logheader + "ERROR=" + err);
					}
				}


				var mobileNumber = row.Patient.MobileNumber;
				if (row.Appointment.IsRemainderSms && !string.IsNullOrEmpty(mobileNumber))
				{
					var logheader = logheader0 + "(sms)" + ": ";
					NLog.Debug(logheader + "START");
					var err = SendSms(mobileNumber, sms_text);
					if (string.IsNullOrEmpty(err))
					{
						NLog.Debug(logheader + "START");
						SetRemainderIsProcessedSms(db, row.Remainder.RowId);
					}
					else
					{
						NLog.Debug(logheader + "ERROR=" + err);
					}
				}


			}
		}

		void SetRemainderIsProcessedEmail(EF.PracticeManagerEntities db, Guid appointmentRemainderRowId)
		{
			var appointmentRemainder = db.AppointmentRemainders.Single(q => q.RowId == appointmentRemainderRowId);
			appointmentRemainder.IsProcessedEmail = true;
			appointmentRemainder.ProcessedEmailTime = DateTime.Now;
			db.SaveChanges();
		}

		void SetRemainderIsProcessedSms(EF.PracticeManagerEntities db, Guid appointmentRemainderRowId)
		{
			var appointmentRemainder = db.AppointmentRemainders.Single(q => q.RowId == appointmentRemainderRowId);
			appointmentRemainder.IsProcessedSms = true;
			appointmentRemainder.ProcessedSmsTime = DateTime.Now;
			db.SaveChanges();
		}


		string SendEmail(string email, string subject, string html)
		{
			try
			{
				var message = new MimeMessage();
				var fromName = ConfigurationManager.AppSettings["smtp.from.name"];
				var fromAddress = ConfigurationManager.AppSettings["smtp.from.address"];
				message.From.Add(new MailboxAddress(fromName, fromAddress));
				message.Subject = subject ?? "";

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


		static string SendSms(string phone, string text)
		{
			try
			{
				NLog.Debug("Email2Sms. SendSms. Phone=" + phone);
				NLog.Debug("Email2Sms. SendSms. Text=" + text);
				//return false;

				var auth_id = ConfigurationManager.AppSettings["plivo.auth_id"];
				var auth_token = ConfigurationManager.AppSettings["plivo.auth_token"];
				var plivo = new RestAPI(auth_id, auth_token);

				var phoneSrc = ConfigurationManager.AppSettings["phone.src"];
				var resp = plivo.send_message(new Dictionary<string, string>()
				{
					{ "src", phoneSrc },	// Sender's phone number with country code
					{ "dst", phone },		// Receiver's phone number wiht country code
					{ "text", text }		// Your SMS text message
				});

				NLog.Debug("Email2Sms. SendSms. Response=" + resp.Content);

				if (resp.Data.message_uuid != null)
				{
					var count = resp.Data.message_uuid.Count;
					for (var i = 0; i < count; i++)
					{
						NLog.Debug("Email2Sms. SendSms. Message UUID:" + resp.Data.message_uuid[i]);
					}
				}

				NLog.Debug("Email2Sms. SendSms. StatusCode=" + resp.StatusCode);
				if (resp.StatusCode == HttpStatusCode.Accepted)
				{
					NLog.Debug("Email2Sms. SendSms. Success");
					return "";
				}
				else
				{
					NLog.Debug("Email2Sms. SendSms. Error");
					return "error StatusCode=" + resp.StatusCode;
				}
			}
			catch(Exception ex)
			{
				var errorMessage = ex.ToString();
				//return "";
				return errorMessage;
			}
		}
	




	enum ReminderType { Email, Sms }

		string GetTemplate(string template)
		{
			var location = GetMainPath();
			var file = Path.Combine(location, "Templates", template);
			var text = File.ReadAllText(file);
			return text;
		}

		public static string GetMainPath()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			var path = Path.GetDirectoryName(location);
			return path;
		}
	}
}

