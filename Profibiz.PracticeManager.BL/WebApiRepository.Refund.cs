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
		public IEnumerable<DTO.Refund> GetRefundList(Guid? rowId, Guid? patientRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.RefundV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
            if (patientRowId != null)
            {
                wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
            }
			//if (hasNoDistributedAmount == 1)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.Amount > q.AmountInInvoices);
			//}
			if (paymentDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate >= paymentDateFrom);
			}
			if (paymentDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate <= paymentDateTo);
			}


			var list = db.RefundsV.Where(wh.Expand()).ToArray();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.RefundV));
			return mapper.Map<List<DTO.Refund>>(list);
		}

		public DTO.Refund GetRefund(Guid id)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var row = db.RefundsV
				.Include(q => q.Patient)
				.Include(q => q.InvoiceRefunds)
				.Include(q => q.InvoiceRefunds.Select(z => z.Invoice))
				.Include(q => q.PaymentRefunds)
				.Include(q => q.PaymentRefunds.Select(z => z.Payment))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Refund>((q) => q.Patient)
					.AddIncludeProp<DTO.Refund>((q) => q.InvoiceRefunds)
					.AddIncludeProp<DTO.InvoiceRefund>((q) => q.Invoice)
					.AddIncludeProp<DTO.Refund>((q) => q.PaymentRefunds)
					.AddIncludeProp<DTO.PaymentRefund>((q) => q.Payment);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.RefundV), typeof(EF.InvoiceRefundV), typeof(EF.InvoiceV), typeof(EF.PatientV), typeof(EF.PaymentRefundV), typeof(EF.PaymentV));
			var ret =  mapper.Map<DTO.Refund>(row);
			return ret;
		}

		public void UpdateRefundCore(DTO.Refund entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var RefundRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.RefundT),
					typeof(EF.InvoiceRefundT),
					typeof(EF.PaymentRefundT));

				if (isDelete)
				{
					db.PaymentRefundsT.Where(q => q.RefundRowId == RefundRowId).Delete();
					db.InvoiceRefundsT.Where(q => q.RefundRowId == RefundRowId).Delete();
					db.RefundsT.Where(q => q.RowId == RefundRowId).Delete();
				}
				else
				{
					var Refund = db.RefundsT.FirstOrDefault(q => q.RowId == RefundRowId);
					var InvoiceRefunds = db.InvoiceRefundsT.Where(q => q.RefundRowId == RefundRowId).ToArray();
					var PaymentRefunds = db.PaymentRefundsT.Where(q => q.RefundRowId == RefundRowId).ToArray();

					var nRefund = mapper.Map<EF.RefundT>(entity);
					var nInvoiceRefunds = mapper.Map<EF.InvoiceRefundT[]>(entity.InvoiceRefunds);
					var nPaymentRefunds = mapper.Map<EF.PaymentRefundT[]>(entity.PaymentRefunds);
					nInvoiceRefunds.ForEach(q => q.RefundRowId = RefundRowId);
					nPaymentRefunds.ForEach(q => q.RefundRowId = RefundRowId);

					if (Refund == null)
					{
						Refund = nRefund;
						db.RefundsT.Add(Refund);
					}
					else
					{
						mapper.Map(nRefund, Refund);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(InvoiceRefunds, nInvoiceRefunds, q => q.RowId, db);
					DbUpdateRowsHelper.UpdateList(PaymentRefunds, nPaymentRefunds, q => q.RowId, db);
				}

				scope.Complete();
			}
		}
	}
}

