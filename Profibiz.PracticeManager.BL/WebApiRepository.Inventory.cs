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
		public IEnumerable<DTO.Inventory> GetInventoryList(Guid? rowId, Guid? orderRowId, Guid? invoiceRowId, DateTime? transactionDateFrom, DateTime? transactionDateTo)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.InventoryV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (orderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.OrderRowId == orderRowId);
			}
			if (invoiceRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceRowId == invoiceRowId);
			}
			if (transactionDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.TransactionDate >= transactionDateFrom);
			}
			if (transactionDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.TransactionDate <= transactionDateTo);
			}

			var qry = db.InventoriesV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.InventoryV));
			var rows = mapper.Map<List<DTO.Inventory>>(list);
			return rows;
		}


		public IEnumerable<DTO.InventoryBalance> GetInventoryBalanceList(Guid? rowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.InventoryBalance>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}

			var qry = db.InventoryBalances.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.InventoryBalance));
			var rows = mapper.Map<List<DTO.InventoryBalance>>(list);
			return rows;
		}

		public DTO.Order GetOrder(Guid id)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var sqlquery = db.OrdersV
				.Include(q => q.OrderItems);
			var row = sqlquery.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.Order>((q) => q.OrderItems);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.OrderV), typeof(EF.OrderItemV));
			var ret = mapper.Map<DTO.Order>(row);

			return ret;
		}

		public IEnumerable<DTO.Order> GetOrderList(Guid? rowId, Guid? supplierRowId, DateTime? orderDateFrom, DateTime? orderDateTo, Boolean? noPaidOnly)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.OrderV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (supplierRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.SupplierRowId == supplierRowId);
			}
			if (orderDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.OrderDate >= orderDateFrom);
			}
			if (orderDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.OrderDate <= orderDateTo);
			}
			if (noPaidOnly == true)
			{
				wh = PredicateBuilder.And(wh, q => q.PaymentRequest > 0);
			}

			var qry = db.OrdersV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.OrderV));
			var rows = mapper.Map<List<DTO.Order>>(list);
			return rows;
		}

		public void UpdateOrderCore(DTO.Order entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				InventoryFunc.BeforeUpdateOrder(db, entity.RowId);

				var isDelete = (state == EntityState.Deleted);
				var orderRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.OrderT), typeof(EF.OrderItemT));

				if (isDelete)
				{
					db.OrderItemsT.Where(q => q.OrderRowId == orderRowId).Delete();
					db.OrdersT.Where(q => q.RowId == orderRowId).Delete();
				}
				else
				{
					var order = db.OrdersT.FirstOrDefault(q => q.RowId == orderRowId);
					var orderItems = db.OrderItemsT.Where(q => q.OrderRowId == orderRowId).ToArray();

					var nOrder = mapper.Map<EF.OrderT>(entity);
					var nOrderItems = mapper.Map<EF.OrderItemT[]>(entity.OrderItems);
					nOrderItems.ForEach(q => q.OrderRowId = orderRowId);

					if (order == null)
					{
						order = nOrder;
						db.OrdersT.Add(order);
					}
					else
					{
						mapper.Map(nOrder, order);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(orderItems, nOrderItems, q => q.RowId, db);
				}

				InventoryFunc.AfterUpdateOrder(db, entity.RowId);

				scope.Complete();
			}
		}

		public void PostInventoryBalances(List<DTO.InventoryBalance> rows)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				foreach(var row in rows)
				{
					var qty = (row.NewBalance ?? 0) - (row.Balance ?? 0);
					var inventory = new EF.InventoryT
					{
						RowId = Guid.NewGuid(),
						TransactionDate = (DateTime)row.TransactionDate,
						MedicalServiceOrSupplyRowId = row.RowId,
						Qty = qty,
						Price = 0,
						Tax = 0,
						Credit = 0,
						Debit =  0,
						Description = row.TransactionDescription,
					};
					db.InventoriesT.Add(inventory);
				}

				db.SaveChangesEx();
				scope.Complete();
			}
		}


		public void UpdateInventoryCore(DTO.Inventory entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var inventoryRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.InventoryT));

				if (isDelete)
				{
					db.InventoriesT.Where(q => q.RowId == inventoryRowId).Delete();
				}
				else
				{
					throw new NotSupportedException();
					//var order = db.OrdersT.FirstOrDefault(q => q.RowId == orderRowId);
					//var orderItems = db.OrderItemsT.Where(q => q.OrderRowId == orderRowId).ToArray();

					//var nOrder = mapper.Map<EF.OrderT>(entity);
					//var nOrderItems = mapper.Map<EF.OrderItemT[]>(entity.OrderItems);
					//nOrderItems.ForEach(q => q.OrderRowId = orderRowId);

					//if (order == null)
					//{
					//	order = nOrder;
					//	db.OrdersT.Add(order);
					//}
					//else
					//{
					//	mapper.Map(nOrder, order);
					//}
					//db.SaveChangesEx();

					//DbUpdateRowsHelper.UpdateList(orderItems, nOrderItems, q => q.RowId, db);
				}

				scope.Complete();
			}
		}
	}


	public static class InventoryFunc
	{
		public static void BeforeUpdateOrder(EF.PracticeManagerEntities db, Guid? orderRowId)
		{
			var orderItems = db.OrderItemsT.Where(q => q.OrderRowId == orderRowId).Select(q => q.RowId);
			db.InventoriesT.Where(q => orderItems.Contains((Guid)q.OrderItemRowId)).Delete();
		}

		public static void BeforeUpdateInvoice(EF.PracticeManagerEntities db, Guid? invoiceRowId)
		{
			var invoiceItems = db.InvoiceItems.Where(q => q.InvoiceRowId == invoiceRowId).Select(q => q.RowId);
			db.InventoriesT.Where(q => invoiceItems.Contains((Guid)q.InvoiceItemRowId)).Delete();
		}

		public static void AfterUpdateOrder(EF.PracticeManagerEntities db, Guid? orderRowId)
		{
			var order = db.OrdersV.Include(q => q.OrderItems).Include(q => q.Supplier).FirstOrDefault(q => q.RowId == orderRowId);
			if (order != null)
			{
				foreach (var orderItem in order.OrderItems)
				{
					//var description = "Order #: " + order.OrderNumber;
					var description = "#" + order.OrderNumber;
					//if (order.Supplier != null)
					//{
					//	description += "; " + "Supplier: " + order.Supplier.Name;
					//}
					var qty = orderItem.Qty;
					var price = orderItem.Price;
					var tax = orderItem.Tax;
					var inventory = new EF.InventoryT
					{
						RowId = Guid.NewGuid(),
						TransactionDate = order.OrderDate,
						MedicalServiceOrSupplyRowId = orderItem.MedicalServiceOrSupplyRowId,
						OrderItemRowId = orderItem.RowId,
						Qty = qty,
						Price = price,
						Tax = tax,
						Credit = qty * (price + tax),
						Debit = 0,
						Description = description,
					};
					db.InventoriesT.Add(inventory);
				}
				db.SaveChanges();
			}
		}

		public static void AfterUpdateInvoice(EF.PracticeManagerEntities db, Guid? invoiceRowId)
		{
			//var invoice = db.InvoicesT.Include(q => q.InvoiceItems).FirstOrDefault(q => q.RowId == invoiceRowId);
			var invoice = db.InvoicesT.Include(q => q.InvoiceItems).Where(q => q.InvoiceType == "Supply").FirstOrDefault(q => q.RowId == invoiceRowId);
			if (invoice != null)
			{
				foreach (var invoiceItem in invoice.InvoiceItems)
				{
					//var description = "Invoice #: " + invoice.InvoiceNumber;
					var description = "#" + invoice.InvoiceNumber;
					var qty = invoiceItem.Units ?? 0;
					var price = invoiceItem.Price ?? 0;
					var tax = invoiceItem.Tax ?? 0;
					var inventory = new EF.InventoryT
					{
						RowId = Guid.NewGuid(),
						TransactionDate = (DateTime)invoice.InvoiceDate,
						MedicalServiceOrSupplyRowId = (Guid)invoiceItem.ServcieOrSupplyRowId,
						InvoiceItemRowId = invoiceItem.RowId,
						Qty = -qty,
						Price = price,
						Tax = tax,
						Debit = qty * (price + tax),
						Credit = 0,
						Description = description,
					};
					db.InventoriesT.Add(inventory);
				}
				db.SaveChanges();
			}
		}

		public static void BeforeUpdateChargeout(EF.PracticeManagerEntities db, Guid? chargeoutRowId)
		{
			//nothing
		}
		public static void AfterUpdateChargeout(EF.PracticeManagerEntities db, Guid? chargeoutRowId)
		{
			//nothing
		}

	}
}

