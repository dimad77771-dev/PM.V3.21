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

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.Payment> GetPaymentList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.PaymentV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
            if (patientRowId != null)
            {
                wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
            }
			if (hasNoDistributedAmount == 1)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentBalance > 0);
			}
			if (paymentDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate >= paymentDateFrom);
			}
			if (paymentDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate <= paymentDateTo);
			}


			var list = db.PaymentsV.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PaymentV));
			return mapper.Map<List<DTO.Payment>>(list);
		}

		public DTO.Payment GetPayment(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var row = db.PaymentsV
				.Include(q => q.Patient)
				.Include(q => q.InvoicePayments)
				.Include(q => q.InvoicePayments.Select(z => z.Invoice))
				.Include(q => q.PaymentRefunds)
				.Include(q => q.PaymentRefunds.Select(z => z.Refund))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Payment>((q) => q.Patient)
					.AddIncludeProp<DTO.Payment>((q) => q.InvoicePayments)
					.AddIncludeProp<DTO.InvoicePayment>((q) => q.Invoice)
					.AddIncludeProp<DTO.Payment>((q) => q.PaymentRefunds)
					.AddIncludeProp<DTO.PaymentRefund>((q) => q.Refund);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.PaymentV), typeof(EF.InvoicePayment), typeof(EF.PaymentRefundV), typeof(EF.InvoiceV), typeof(EF.RefundV), typeof(EF.PatientV));
			var ret =  mapper.Map<DTO.Payment>(row);
			return ret;
		}


		public void UpdatePaymentCore(DTO.Payment entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var PaymentRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.PaymentT),
					typeof(EF.InvoicePayment));

				if (isDelete)
				{
					db.InvoicePayments.Where(q => q.PaymentRowId == PaymentRowId).Delete();
					db.PaymentsT.Where(q => q.RowId == PaymentRowId).Delete();
				}
				else
				{
					var Payment = db.PaymentsT.FirstOrDefault(q => q.RowId == PaymentRowId);
					var InvoicePayments = db.InvoicePayments.Where(q => q.PaymentRowId == PaymentRowId).ToArray();

					var nPayment = mapper.Map<EF.PaymentT>(entity);
					var nInvoicePayments = mapper.Map<EF.InvoicePayment[]>(entity.InvoicePayments);
					nInvoicePayments.ForEach(q => q.PaymentRowId = PaymentRowId);

					if (Payment == null)
					{
						Payment = nPayment;
						db.PaymentsT.Add(Payment);
					}
					else
					{
						mapper.Map(nPayment, Payment);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(InvoicePayments, nInvoicePayments, q => q.RowId, db, this);
				}

				scope.Complete();
			}
		}
	}
}

