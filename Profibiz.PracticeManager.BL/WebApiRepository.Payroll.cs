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
		public IEnumerable<DTO.PayrollInfoResult> GetPayrollInfo(DateTime periodStart, DateTime periodFinish, Guid? serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var list = db.sp_PayrollInfo(periodStart, periodFinish, serviceProviderRowId).ToList();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PayrollInfoResult));
			var rows = mapper.Map<List<DTO.PayrollInfoResult>>(list);
			return rows;
		}

		public IEnumerable<DTO.PayrollPaymentByDoctorAndPeriodView> GetPayrollPaymentByDoctorAndPeriod(Guid? serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.PayrollPaymentByDoctorAndPeriodView>();
			if (serviceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);
			}

			var list = db.PayrollPaymentByDoctorAndPeriodViews.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PayrollPaymentByDoctorAndPeriodView));
			var rows = mapper.Map<List<DTO.PayrollPaymentByDoctorAndPeriodView>>(list);
			return rows;
		}

		public IEnumerable<DTO.InvoicePaymentByDoctorAndPeriodView> GetInvoicePaymentByDoctorAndPeriod(Guid? serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.InvoicePaymentByDoctorAndPeriodView>();
			if (serviceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);
			}

			var list = db.InvoicePaymentByDoctorAndPeriodViews.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.InvoicePaymentByDoctorAndPeriodView));
			var rows = mapper.Map<List<DTO.InvoicePaymentByDoctorAndPeriodView>>(list);
			return rows;
		}


		public IEnumerable<DTO.InvoicePaymentByDoctors> GetPayrollDetail(DateTime periodStart, DateTime periodFinish, Guid serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.InvoicePaymentByDoctorsV>();
			wh = PredicateBuilder.And(wh, q => q.PaymentDate >= periodStart);
			wh = PredicateBuilder.And(wh, q => q.PaymentDate <= periodFinish);
			wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);

			var list = 
				db.InvoicePaymentByDoctorsV
				.Include(q => q.Invoice)
				.Include(q => q.Invoice.InvoiceAdvinfo)
				.Include(q => q.Payment)
				.Where(wh.Expand())
				.ToArray();

			var options = new AutoMapperHelper.Options();
			options.AddIncludeProp<DTO.InvoicePaymentByDoctors>(q => q.Invoice);
			options.AddIncludeProp<DTO.InvoicePaymentByDoctors>(q => q.Payment);
			options.AddIncludeProp<DTO.Invoice>(q => q.InvoiceAdvinfo);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, 
				typeof(EF.InvoicePaymentByDoctorsV), 
				typeof(EF.InvoiceV), typeof(EF.InvoiceAdvinfoV), typeof(EF.PaymentV));
			var rows = mapper.Map<List<DTO.InvoicePaymentByDoctors>>(list);
			return rows;
		}

		public IEnumerable<DTO.PayrollPayment> GetPayrollPaymentList(Guid? serviceProviderRowId, DateTime? paymentDateFrom, DateTime? paymentDateTo)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.PayrollPaymentV>();
			if (serviceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);
			}
			if (paymentDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate >= paymentDateFrom);
			}
			if (paymentDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentDate <= paymentDateTo);
			}


			var list = db.PayrollPaymentsV.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PayrollPaymentV));
			return mapper.Map<List<DTO.PayrollPayment>>(list);
		}

		public DTO.PayrollPayment GetPayrollPayment(Guid id)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var row = db.PayrollPaymentsV
				.Include(q => q.PayrollPaymentAllocations)
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.PayrollPayment>((q) => q.PayrollPaymentAllocations);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.PayrollPaymentV), typeof(EF.PayrollPaymentAllocationV));
			var ret = mapper.Map<DTO.PayrollPayment>(row);
			return ret;
		}

		public void UpdatePayrollPaymentCore(DTO.PayrollPayment entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var PayrollPaymentRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.PayrollPaymentT),
					typeof(EF.PayrollPaymentAllocationT));

				if (isDelete)
				{
					db.PayrollPaymentAllocationsT.Where(q => q.PayrollPaymentRowId == PayrollPaymentRowId).Delete();
					db.PayrollPaymentsT.Where(q => q.RowId == PayrollPaymentRowId).Delete();
				}
				else
				{
					var PayrollPayment = db.PayrollPaymentsT.FirstOrDefault(q => q.RowId == PayrollPaymentRowId);
					var InvoicePayrollPayments = db.PayrollPaymentAllocationsT.Where(q => q.PayrollPaymentRowId == PayrollPaymentRowId).ToArray();

					var nPayrollPayment = mapper.Map<EF.PayrollPaymentT>(entity);
					var nInvoicePayrollPayments = mapper.Map<EF.PayrollPaymentAllocationT[]>(entity.PayrollPaymentAllocations);
					nInvoicePayrollPayments.ForEach(q => q.PayrollPaymentRowId = PayrollPaymentRowId);

					if (PayrollPayment == null)
					{
						PayrollPayment = nPayrollPayment;
						db.PayrollPaymentsT.Add(PayrollPayment);
					}
					else
					{
						mapper.Map(nPayrollPayment, PayrollPayment);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(InvoicePayrollPayments, nInvoicePayrollPayments, q => q.RowId, db);
				}

				scope.Complete();
			}
		}
	}
}

