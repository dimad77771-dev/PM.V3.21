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

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.Invoice> GetInvoiceList(Guid? rowId, Guid? patientRowId, bool? useFamilyHead, int? noPaidOnly, bool flagNoPaidOrNoApprovedAmount, bool negativeBalanceOnly, DateTime? invoiceDateFrom, DateTime? invoiceDateTo, bool includeInvoiceClaims, bool isShowSentOnly, bool isShowPaidOnly, Guid? referrerRowId, Guid? insuranceProviderRowId, DateTime? createdDateFrom, DateTime? createdDateTo, bool isCoordinationProblemOnly, int hideEstimation)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			//var qparm = JsonHelper.DeserializeObject<QueryParamGetInvoicesList>(query);

			var wh = ExpressionFunc.True<EF.InvoiceV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (patientRowId != null)
			{
				if (useFamilyHead == true)
				{
					wh = PredicateBuilder.And(wh, 
						q =>
							(q.PatientRowId == patientRowId) 
							||
							(db.PatientFamilyMemberViews.Where(z => z.PatientRowId == patientRowId).Select(z => z.FamilyMemberPatientRowId).Contains(q.PatientRowId)));
				}
				else
				{
					wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
				}
			}
			if (noPaidOnly == 1)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentRequest > 0);
			}
			if (hideEstimation == 1)
			{
				wh = PredicateBuilder.And(wh, q => !q.IsEstimation);
			}
			if (flagNoPaidOrNoApprovedAmount)
			{
				var wh1 = ExpressionFunc.False<EF.InvoiceV>();
				wh1 = PredicateBuilder.Or(wh1, q => (q.ApproveAmont ?? 0) == 0);
				wh1 = PredicateBuilder.Or(wh1, q => q.PaymentRequest > 0);

				wh = PredicateBuilder.And(wh, wh1);
			}
			if (negativeBalanceOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentRequest < 0);
			}
			if (invoiceDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceDate >= invoiceDateFrom);
			}
			if (invoiceDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceDate <= invoiceDateTo);
			}
			if (createdDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.Created >= createdDateFrom);
			}
			if (createdDateTo != null)
			{
				createdDateTo = createdDateTo.Value.AddDays(1);
				wh = PredicateBuilder.And(wh, q => q.Created < createdDateTo);
			}
			if (isShowSentOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceClaims.Any(z => z.SentAmont > 0 && z.ApproveDate == null));
			}
			if (isShowPaidOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoicePayments.Any());
			}
			if (referrerRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.Patient.ReferrerRowId == referrerRowId);
			}
			if (insuranceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceClaims.Any(z => z.InsuranceCoverage.InsuranceProviderRowId == insuranceProviderRowId));
			}
			if (isCoordinationProblemOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.HasCoordinationProblem == true);
			}

			var qry = db.InvoicesV.Where(wh.Expand());
			if (includeInvoiceClaims)
			{
				qry = qry.Include(q => q.InvoiceClaims);
				qry = qry.Include(q => q.InvoiceClaims.Select(x => x.InsuranceCoverage));
			}
			var list = qry.Where(wh.Expand()).ToArray();

			//var list = db.InvoicesV.Where(wh.Expand()).Select(q => new { BillTo = q.BillTo, BillToCity = q.BillToCity }).ToArray();
			//var list = db.InvoicesV.Select(q => new { q.Amount, q.BillTo, q.BillToCity }).Where(q => q.Amount > 100).ToArray();
			//var list = db.Invoices.Select(q => new { q.BillTo, q.BillToCity }).Where(q => q.BillTo == "").ToArray();

			//var iii = db.InvoicesV.Where(wh.Expand());
			//AutoMapper.QueryableExtensions.Extensions.ProjectTo<DTO.InvoiceV>(iii, null);


			var options = AutoMapperHelper.CreateOptions();
			if (includeInvoiceClaims)
			{
				options.AddIncludeProp<DTO.Invoice>(q => q.InvoiceClaims);
				options.AddIncludeProp<DTO.InvoiceClaim>(q => q.InsuranceCoverage);
			}
			//options.CreateMissingTypeMaps = true;
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.InvoiceV), typeof(EF.InvoiceClaimV), typeof(EF.InsuranceCoverageV));
			var rows = mapper.Map<List<DTO.Invoice>>(list);
			return rows;
		}

		public DTO.Invoice GetInvoice(Guid id, bool includeAppointment)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var sqlquery = db.InvoicesV
				.Include(q => q.Patient)
				.Include(q => q.InvoiceItems)
				.Include(q => q.InvoiceClaims)
				.Include(q => q.InvoiceClaims.Select(x => x.InsuranceCoverage))
				.Include(q => q.InvoicePayments)
				.Include(q => q.InvoicePayments.Select(x => x.Payment))
				.Include(q => q.InvoiceRefunds)
				.Include(q => q.InvoiceRefunds.Select(x => x.Refund));
			if (includeAppointment)
			{
				sqlquery.Include(q => q.InvoiceItems.Select(x => x.Appointment));
			}
			var row = sqlquery.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Invoice>((q) => q.Patient)
					.AddIncludeProp<DTO.Invoice>((q) => q.InvoiceItems)
					.AddIncludeProp<DTO.Invoice>((q) => q.InvoiceClaims)
					.AddIncludeProp<DTO.InvoiceClaim>((q) => q.InsuranceCoverage)
					.AddIncludeProp<DTO.InvoiceClaim>((q) => q.InvoiceClaimDetails)
					.AddIncludeProp<DTO.Invoice>((q) => q.InvoicePayments)
					.AddIncludeProp<DTO.InvoicePayment>((q) => q.Payment)
					.AddIncludeProp<DTO.Invoice>((q) => q.InvoiceRefunds)
					.AddIncludeProp<DTO.InvoiceRefund>((q) => q.Refund);
			if (includeAppointment)
			{
				options = options.AddIncludeProp<DTO.InvoiceItem>((q) => q.Appointment);
			}
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.InvoiceV), typeof(EF.InvoiceItem), 
				typeof(EF.InvoiceClaimV), typeof(EF.InvoiceClaimDetail), typeof(EF.InsuranceCoverageV),
				typeof(EF.InvoicePayment), typeof(EF.PaymentV),
				typeof(EF.InvoiceRefundV), typeof(EF.RefundV),
				typeof(EF.PatientV), typeof(EF.AppointmentV));
			var ret = mapper.Map<DTO.Invoice>(row);

			return ret;
		}

		public ServerReturnUpdateInvoice UpdateInvoiceCore(DTO.Invoice entity)
		{
			lock (GlobalLocker.InvoiceUpdate)
			{
				var result = new ServerReturnUpdateInvoice();
				var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
				using (var scope = new TransactionScope())
				{
					var invoiceRowId = entity.RowId;
					var mapper = AutoMapperHelper.GetPocoMapper(
						typeof(EF.InvoiceT),
						typeof(EF.InvoiceItem),
						typeof(EF.InvoiceClaimT),
						typeof(EF.InvoiceClaimDetail),
						typeof(EF.InvoicePayment));

					InventoryFunc.BeforeUpdateInvoice(db, invoiceRowId);

					var invoice = db.InvoicesT.FirstOrDefault(q => q.RowId == invoiceRowId);
					var invoiceItems = db.InvoiceItems.Where(q => q.InvoiceRowId == invoiceRowId).ToArray();
					var invoiceClaims = db.InvoiceClaimsT.Where(q => q.InvoiceRowId == invoiceRowId).ToArray();
					var invoiceClaimDetails = db.InvoiceClaimsT.Where(q => q.InvoiceRowId == invoiceRowId).SelectMany(q => q.InvoiceClaimDetails).ToArray();
					var invoicePayments = db.InvoicePayments.Where(q => q.InvoiceRowId == invoiceRowId).ToArray();

					var nInvoice = mapper.Map<EF.InvoiceT>(entity);
					var nInvoiceItems = mapper.Map<EF.InvoiceItem[]>(entity.InvoiceItems);
					nInvoiceItems.ForEach(q => q.InvoiceRowId = invoiceRowId);

					var nInvoiceClaims = mapper.Map<EF.InvoiceClaimT[]>(entity.InvoiceClaims);
					nInvoiceClaims.ForEach(q => q.InvoiceRowId = invoiceRowId);

					var nInvoiceClaimDetails = mapper.Map<EF.InvoiceClaimDetail[]>(entity.InvoiceClaims.SelectMany(q => q.InvoiceClaimDetails));

					var nInvoicePayments = mapper.Map<EF.InvoicePayment[]>(entity.InvoicePayments);
					nInvoicePayments.ForEach(q => q.InvoiceRowId = invoiceRowId);

					if (invoice == null)
					{
						invoice = nInvoice;
						var invoiceNumber = GenerateInvoiceNumber(db);
						invoice.InvoiceNumber = invoiceNumber;
						result.InvoiceNumber = invoiceNumber;
						db.InvoicesT.Add(invoice);
					}
					else
					{
						mapper.Map(nInvoice, invoice);
					}

					try
					{
						db.SaveChangesEx();
					}
					catch (Exception ex)
					{
						if (ExceptionHelper.IsInvoiceNumberDuplicateConstraintException(ex))
						{
							ExceptionHelper.UserUpdateError(UserErrorCodes.InvoiceNumberDuplicate, "Invoice Number \"" + invoice.InvoiceNumber + "\" is duplicate");
						}
						else throw new AggregateException(ex);
					}

					DbUpdateRowsHelper.UpdateList(invoiceItems, nInvoiceItems, q => q.RowId, db, this);
					DbUpdateRowsHelper.UpdateList(invoiceClaims, nInvoiceClaims, q => q.RowId, db, this);
					DbUpdateRowsHelper.UpdateList(invoiceClaimDetails, nInvoiceClaimDetails, q => q.RowId, db, this);
					DbUpdateRowsHelper.UpdateList(invoicePayments, nInvoicePayments, q => q.RowId, db, this);

					InventoryFunc.AfterUpdateInvoice(db, invoiceRowId);

					scope.Complete();
				}
				return result;
			}
		}

		public ServerReturnUpdateInvoice DeleteInvoiceCore(List<Guid> invoiceRowIds)
		{
			lock (GlobalLocker.InvoiceUpdate)
			{
				var result = new ServerReturnUpdateInvoice();
				var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
				using (var scope = new TransactionScope())
				{
					foreach(var invoiceRowId in invoiceRowIds)
					{
						InventoryFunc.BeforeUpdateInvoice(db, invoiceRowId);
						db.InvoiceItems.Where(q => q.InvoiceRowId == invoiceRowId).Delete();
						db.InvoicePayments.Where(q => q.InvoiceRowId == invoiceRowId).Delete();
						db.InvoiceClaimsT.Where(q => q.InvoiceRowId == invoiceRowId).SelectMany(q => q.InvoiceClaimDetails).Delete();
						db.InvoiceClaimsT.Where(q => q.InvoiceRowId == invoiceRowId).Delete();
						db.EmailSendsT.Where(q => q.InvoiceRowId == invoiceRowId).SelectMany(q => q.EmailSendAttachments).Delete();
						db.EmailSendsT.Where(q => q.InvoiceRowId == invoiceRowId).SelectMany(q => q.EmailSendRecipients).Delete();
						db.EmailSendsT.Where(q => q.InvoiceRowId == invoiceRowId).Delete();
						db.InvoicesT.Where(q => q.RowId == invoiceRowId).Delete();
					}

					scope.Complete();
				}
				return result;
			}
		}

		String GenerateInvoiceNumber(EF.PracticeManagerEntities db)
		{
			//System.Threading.Thread.Sleep(20000);

			var prefix = DateTime.Today.ToString("yyyyMMdd") + "-";

			var existsNumbers = db.InvoicesV.Where(q => q.InvoiceNumber.StartsWith(prefix)).Select(q => q.InvoiceNumber).ToList();
			var random = new Random();

			for (int len = 2; len <= 5; len++)
			{
				var maxnum = (int)Math.Pow(10, len) - 1;
				var format = new string('0', len);
				var allVariants = Enumerable.Range(1, maxnum).Select(q => prefix + q.ToString(format)).Where(q => !existsNumbers.Contains(q)).ToArray();
				if (allVariants.Length > 0)
				{
					var npp = random.Next(0, allVariants.Length - 1);
					var invoiceNumber = allVariants[npp];
					return invoiceNumber;
				}
			}
			throw new NotSupportedException();
		}
	}
}

