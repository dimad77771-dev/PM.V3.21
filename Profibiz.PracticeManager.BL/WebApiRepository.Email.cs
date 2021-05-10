using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EF = Profibiz.PracticeManager.EF;
using DTO = Profibiz.PracticeManager.DTO;
using System.Transactions;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using LinqKit;
using System.Data.Entity;
using System.Diagnostics;
using Newtonsoft.Json;
using Profibiz.PracticeManager.SharedCode;
using System.Configuration;
using MailKit.Net.Smtp;
using MimeKit;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.EmailSend> GetEmailSendList(DateTime? sendDateFrom, DateTime? sendDateTo)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.EmailSendV>();
			if (sendDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SendDate >= sendDateFrom);
			}
			if (sendDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SendDate <= sendDateTo);
			}

			var qry = db.EmailSendsV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.EmailSendV));
			var rows = mapper.Map<List<DTO.EmailSend>>(list);
			return rows;
		}

		public IEnumerable<DTO.EmailSendAttachment> GetEmailSendAttachmentList(Guid emailSendRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.EmailSendAttachment>();
			wh = PredicateBuilder.And(wh, q => q.EmailSendRowId == emailSendRowId);

			var qry = db.EmailSendAttachments.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();
			//list.ForEach(q => q.F)

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.EmailSendAttachment));
			var rows = mapper.Map<List<DTO.EmailSendAttachment>>(list);
			return rows;
		}

		public void SendEmail(DTO.EmailSend row)
		{
			var errorMessage = "";
			var isSuccess = true;

			try
			{
				var message = new MimeMessage();
				var fromName = ConfigurationManager.AppSettings["smtp.from.name"];
				var fromAddress = ConfigurationManager.AppSettings["smtp.from.address"];
				message.From.Add(new MailboxAddress(fromName, fromAddress));
				message.Subject = row.Subject ?? "";

				foreach (var recipient in row.EmailSendRecipients)
				{
					var toName = recipient.Name ?? "";
					var toAddress = recipient.Email ?? "";
					message.To.Add(new MailboxAddress(toName, toAddress));
				}
			

				var builder = new BodyBuilder();
				builder.TextBody = row.Body ?? "";
				foreach (var attachment in row.EmailSendAttachments)
				{
					builder.Attachments.Add(attachment.FileName, attachment.FileBytes);
				}
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
			}
			catch(Exception ex)
			{
				errorMessage = ex.Message;
				isSuccess = false;
			}

			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(
				typeof(EF.EmailSendT),
				typeof(EF.EmailSendRecipient),
				typeof(EF.EmailSendAttachment));

				var emailSend = mapper.Map<EF.EmailSendT>(row);
				var emailSendRecipients = mapper.Map<EF.EmailSendRecipient[]>(row.EmailSendRecipients);
				var emailSendAttachments = mapper.Map<EF.EmailSendAttachment[]>(row.EmailSendAttachments);
				emailSendRecipients.ForEach(q =>
				{
					q.EmailSendRowId = emailSend.RowId;
					q.RowId = Guid.NewGuid();
				});
				emailSendAttachments.ForEach(q =>
				{
					q.EmailSendRowId = emailSend.RowId;
					q.RowId = Guid.NewGuid();
				});
				emailSend.IsSuccess = isSuccess;
				emailSend.ErrorMessage = errorMessage;
				emailSend.SendDate = DateTimeHelper.Now;

				db.EmailSendsT.Add(emailSend);
				db.EmailSendRecipients.AddRange(emailSendRecipients);
				db.EmailSendAttachments.AddRange(emailSendAttachments);
				db.SaveChangesEx();
				scope.Complete();
			}

			if (!isSuccess)
			{
				ExceptionHelper.UserUpdateError(UserErrorCodes.EmailError, "Error: " + errorMessage);
			}
		}
	}
}

