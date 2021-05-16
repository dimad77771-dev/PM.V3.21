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
		public IEnumerable<DTO.SupplierRefund> GetSupplierRefundList(Guid? rowId, Guid? supplierRowId, int? hasNoDistributedAmount, DateTime? paymentDateFrom, DateTime? paymentDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.SupplierRefundV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (supplierRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierRowId == supplierRowId);
			}
			//if (hasNoDistributedAmount == 1)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.Amount > q.AmountInOrders);
			//}
			if (paymentDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierPaymentDate >= paymentDateFrom);
			}
			if (paymentDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierPaymentDate <= paymentDateTo);
			}


			var list = db.SupplierRefundsV.Where(wh.Expand()).ToArray();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.SupplierRefundV));
			return mapper.Map<List<DTO.SupplierRefund>>(list);
		}

		public DTO.SupplierRefund GetSupplierRefund(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var row = db.SupplierRefundsV
				.Include(q => q.Supplier)
				.Include(q => q.SupplierPaymentRefunds)
				.Include(q => q.SupplierPaymentRefunds.Select(z => z.SupplierPayment))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.SupplierRefund>((q) => q.Supplier)
					.AddIncludeProp<DTO.SupplierRefund>((q) => q.SupplierPaymentRefunds)
					.AddIncludeProp<DTO.SupplierPaymentRefund>((q) => q.SupplierPayment);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.SupplierRefundV), typeof(EF.OrderV), typeof(EF.Supplier), typeof(EF.SupplierPaymentRefundV), typeof(EF.SupplierPaymentV));
			var ret = mapper.Map<DTO.SupplierRefund>(row);
			return ret;
		}


		public void UpdateSupplierRefundCore(DTO.SupplierRefund entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var SupplierRefundRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.SupplierRefundT),
					typeof(EF.SupplierPaymentRefundT));

				if (isDelete)
				{
					db.SupplierPaymentRefundsT.Where(q => q.SupplierRefundRowId == SupplierRefundRowId).Delete();
					db.SupplierRefundsT.Where(q => q.RowId == SupplierRefundRowId).Delete();
				}
				else
				{
					var SupplierRefund = db.SupplierRefundsT.FirstOrDefault(q => q.RowId == SupplierRefundRowId);
					var SupplierPaymentRefunds = db.SupplierPaymentRefundsT.Where(q => q.SupplierRefundRowId == SupplierRefundRowId).ToArray();

					var nSupplierRefund = mapper.Map<EF.SupplierRefundT>(entity);
					var nSupplierPaymentRefunds = mapper.Map<EF.SupplierPaymentRefundT[]>(entity.SupplierPaymentRefunds);
					nSupplierPaymentRefunds.ForEach(q => q.SupplierRefundRowId = SupplierRefundRowId);

					if (SupplierRefund == null)
					{
						SupplierRefund = nSupplierRefund;
						db.SupplierRefundsT.Add(SupplierRefund);
					}
					else
					{
						mapper.Map(nSupplierRefund, SupplierRefund);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(SupplierPaymentRefunds, nSupplierPaymentRefunds, q => q.RowId, db, this);
				}

				scope.Complete();
			}
		}
	}
}

