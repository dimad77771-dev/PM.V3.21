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
		public IEnumerable<DTO.Refcharge> GetRefchargeList(Guid? rowId, Guid? chargeoutRecipientRowId, int? hasNoDistributedAmount, DateTime? paychargeDateFrom, DateTime? paychargeDateTo)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.RefchargeV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (chargeoutRecipientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutRecipientRowId == chargeoutRecipientRowId);
			}
			if (paychargeDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeDate >= paychargeDateFrom);
			}
			if (paychargeDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeDate <= paychargeDateTo);
			}

			var list = db.RefchargesV.Where(wh.Expand()).ToArray();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.RefchargeV));
			return mapper.Map<List<DTO.Refcharge>>(list);
		}

		public DTO.Refcharge GetRefcharge(Guid id)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var row = db.RefchargesV
				.Include(q => q.ChargeoutRecipient)
				.Include(q => q.ChargeoutRefcharges)
				.Include(q => q.ChargeoutRefcharges.Select(z => z.Chargeout))
				.Include(q => q.PaychargeRefcharges)
				.Include(q => q.PaychargeRefcharges.Select(z => z.Paycharge))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Refcharge>((q) => q.ChargeoutRecipient)
					.AddIncludeProp<DTO.Refcharge>((q) => q.ChargeoutRefcharges)
					.AddIncludeProp<DTO.ChargeoutRefcharge>((q) => q.Chargeout)
					.AddIncludeProp<DTO.Refcharge>((q) => q.PaychargeRefcharges)
					.AddIncludeProp<DTO.PaychargeRefcharge>((q) => q.Paycharge);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.RefchargeV), typeof(EF.ChargeoutRefchargeV), typeof(EF.ChargeoutV), typeof(EF.ChargeoutRecipient), typeof(EF.PaychargeRefchargeV), typeof(EF.PaychargeV));
			var ret = mapper.Map<DTO.Refcharge>(row);
			return ret;
		}

		public void UpdateRefchargeCore(DTO.Refcharge entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var RefchargeRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.RefchargeT),
					typeof(EF.ChargeoutRefchargeT),
					typeof(EF.PaychargeRefchargeT));

				if (isDelete)
				{
					db.PaychargeRefchargesT.Where(q => q.RefchargeRowId == RefchargeRowId).Delete();
					db.ChargeoutRefchargesT.Where(q => q.RefchargeRowId == RefchargeRowId).Delete();
					db.RefchargesT.Where(q => q.RowId == RefchargeRowId).Delete();
				}
				else
				{
					var Refcharge = db.RefchargesT.FirstOrDefault(q => q.RowId == RefchargeRowId);
					var ChargeoutRefcharges = db.ChargeoutRefchargesT.Where(q => q.RefchargeRowId == RefchargeRowId).ToArray();
					var PaychargeRefcharges = db.PaychargeRefchargesT.Where(q => q.RefchargeRowId == RefchargeRowId).ToArray();

					var nRefcharge = mapper.Map<EF.RefchargeT>(entity);
					var nChargeoutRefcharges = mapper.Map<EF.ChargeoutRefchargeT[]>(entity.ChargeoutRefcharges);
					var nPaychargeRefcharges = mapper.Map<EF.PaychargeRefchargeT[]>(entity.PaychargeRefcharges);
					nChargeoutRefcharges.ForEach(q => q.RefchargeRowId = RefchargeRowId);
					nPaychargeRefcharges.ForEach(q => q.RefchargeRowId = RefchargeRowId);

					if (Refcharge == null)
					{
						Refcharge = nRefcharge;
						db.RefchargesT.Add(Refcharge);
					}
					else
					{
						mapper.Map(nRefcharge, Refcharge);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(ChargeoutRefcharges, nChargeoutRefcharges, q => q.RowId, db);
					DbUpdateRowsHelper.UpdateList(PaychargeRefcharges, nPaychargeRefcharges, q => q.RowId, db);
				}

				scope.Complete();
			}
		}
	}
}

