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
		public IEnumerable<DTO.EmailCharge> GetEmailChargeList(DateTime? sendDateFrom, DateTime? sendDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.EmailChargeV>();
			if (sendDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SendDate >= sendDateFrom);
			}
			if (sendDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SendDate <= sendDateTo);
			}

			var qry = db.EmailChargesV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.EmailChargeV));
			var rows = mapper.Map<List<DTO.EmailCharge>>(list);
			return rows;
		}

		public IEnumerable<DTO.EmailChargeAttachment> GetEmailChargeAttachmentList(Guid emailChargeRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.EmailChargeAttachment>();
			wh = PredicateBuilder.And(wh, q => q.EmailChargeRowId == emailChargeRowId);

			var qry = db.EmailChargeAttachments.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();
			//list.ForEach(q => q.F)

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.EmailChargeAttachment));
			var rows = mapper.Map<List<DTO.EmailChargeAttachment>>(list);
			return rows;
		}

		public void SendChargeEmail(DTO.EmailCharge row)
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

				foreach (var recipient in row.EmailChargeRecipients)
				{
					var toName = recipient.Name ?? "";
					var toAddress = recipient.Email ?? "";
					message.To.Add(new MailboxAddress(toName, toAddress));
				}


				var builder = new BodyBuilder();
				builder.TextBody = row.Body ?? "";
				foreach (var attachment in row.EmailChargeAttachments)
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
			catch (Exception ex)
			{
				errorMessage = ex.Message;
				isSuccess = false;
			}

			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(
				typeof(EF.EmailChargeT),
				typeof(EF.EmailChargeRecipient),
				typeof(EF.EmailChargeAttachment));

				var emailCharge = mapper.Map<EF.EmailChargeT>(row);
				var emailChargeRecipients = mapper.Map<EF.EmailChargeRecipient[]>(row.EmailChargeRecipients);
				var emailChargeAttachments = mapper.Map<EF.EmailChargeAttachment[]>(row.EmailChargeAttachments);
				emailChargeRecipients.ForEach(q =>
				{
					q.EmailChargeRowId = emailCharge.RowId;
					q.RowId = Guid.NewGuid();
				});
				emailChargeAttachments.ForEach(q =>
				{
					q.EmailChargeRowId = emailCharge.RowId;
					q.RowId = Guid.NewGuid();
				});
				emailCharge.IsSuccess = isSuccess;
				emailCharge.ErrorMessage = errorMessage;
				emailCharge.SendDate = DateTimeHelper.Now;

				db.EmailChargesT.Add(emailCharge);
				db.EmailChargeRecipients.AddRange(emailChargeRecipients);
				db.EmailChargeAttachments.AddRange(emailChargeAttachments);
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

