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
		public IEnumerable<DTO.SupplierPayment> GetSupplierPaymentList(Guid? rowId, Guid? supplierRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.SupplierPaymentV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (supplierRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierRowId == supplierRowId);
			}
			if (hasNoDistributedAmount == 1)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierPaymentBalance > 0);
			}
			if (paymentDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierPaymentDate >= paymentDateFrom);
			}
			if (paymentDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierPaymentDate <= paymentDateTo);
			}


			var list = db.SupplierPaymentsV.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.SupplierPaymentV));
			return mapper.Map<List<DTO.SupplierPayment>>(list);
		}

		public DTO.SupplierPayment GetSupplierPayment(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var row = db.SupplierPaymentsV
				.Include(q => q.Supplier)
				.Include(q => q.OrderPayments)
				.Include(q => q.OrderPayments.Select(z => z.Order))
				.Include(q => q.SupplierPaymentRefunds)
				.Include(q => q.SupplierPaymentRefunds.Select(z => z.SupplierRefund))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.SupplierPayment>((q) => q.Supplier)
					.AddIncludeProp<DTO.SupplierPayment>((q) => q.OrderPayments)
					.AddIncludeProp<DTO.OrderPayment>((q) => q.Order)
					.AddIncludeProp<DTO.SupplierPayment>((q) => q.SupplierPaymentRefunds)
					.AddIncludeProp<DTO.SupplierPaymentRefund>((q) => q.SupplierRefund);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.SupplierPaymentV), typeof(EF.OrderPayment), typeof(EF.SupplierPaymentRefundV), typeof(EF.OrderV), typeof(EF.SupplierRefundV), typeof(EF.Supplier));
			var ret = mapper.Map<DTO.SupplierPayment>(row);
			return ret;
		}


		public void UpdateSupplierPaymentCore(DTO.SupplierPayment entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var SupplierPaymentRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.SupplierPaymentT),
					typeof(EF.OrderPayment));

				if (isDelete)
				{
					db.OrderPayments.Where(q => q.SupplierPaymentRowId == SupplierPaymentRowId).Delete();
					db.SupplierPaymentsT.Where(q => q.RowId == SupplierPaymentRowId).Delete();
				}
				else
				{
					var SupplierPayment = db.SupplierPaymentsT.FirstOrDefault(q => q.RowId == SupplierPaymentRowId);
					var OrderPayments = db.OrderPayments.Where(q => q.SupplierPaymentRowId == SupplierPaymentRowId).ToArray();

					var nSupplierPayment = mapper.Map<EF.SupplierPaymentT>(entity);
					var nOrderPayments = mapper.Map<EF.OrderPayment[]>(entity.OrderPayments);
					nOrderPayments.ForEach(q => q.SupplierPaymentRowId = SupplierPaymentRowId);

					if (SupplierPayment == null)
					{
						SupplierPayment = nSupplierPayment;
						db.SupplierPaymentsT.Add(SupplierPayment);
					}
					else
					{
						mapper.Map(nSupplierPayment, SupplierPayment);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(OrderPayments, nOrderPayments, q => q.RowId, db, this);
				}

				scope.Complete();
			}
		}
	}
}

