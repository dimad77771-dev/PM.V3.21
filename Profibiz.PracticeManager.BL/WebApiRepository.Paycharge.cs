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
		public IEnumerable<DTO.Paycharge> GetPaychargeList(Guid? rowId, Guid? chargeoutRecipientRowId, int? hasNoDistributedAmount, DateTime? paychargeDateFrom, DateTime? paychargeDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.PaychargeV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (chargeoutRecipientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutRecipientRowId == chargeoutRecipientRowId);
			}
			if (hasNoDistributedAmount == 1)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeBalance > 0);
			}
			if (paychargeDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeDate >= paychargeDateFrom);
			}
			if (paychargeDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeDate <= paychargeDateTo);
			}


			var list = db.PaychargesV.Where(wh.Expand());

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PaychargeV));
			return mapper.Map<List<DTO.Paycharge>>(list);
		}

		public DTO.Paycharge GetPaycharge(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var row = db.PaychargesV
				.Include(q => q.ChargeoutRecipient)
				.Include(q => q.ChargeoutPaycharges)
				.Include(q => q.ChargeoutPaycharges.Select(z => z.Chargeout))
				.Include(q => q.PaychargeRefcharges)
				.Include(q => q.PaychargeRefcharges.Select(z => z.Refcharge))
				.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Paycharge>((q) => q.ChargeoutRecipient)
					.AddIncludeProp<DTO.Paycharge>((q) => q.ChargeoutPaycharges)
					.AddIncludeProp<DTO.ChargeoutPaycharge>((q) => q.Chargeout)
					.AddIncludeProp<DTO.Paycharge>((q) => q.PaychargeRefcharges)
					.AddIncludeProp<DTO.PaychargeRefcharge>((q) => q.Refcharge);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.PaychargeV), typeof(EF.ChargeoutPaycharge), typeof(EF.ChargeoutV), typeof(EF.RefchargeV), typeof(EF.ChargeoutRecipient), typeof(EF.PaychargeRefchargeV));
			var ret = mapper.Map<DTO.Paycharge>(row);
			return ret;
		}

		public void UpdatePaychargeCore(DTO.Paycharge entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var PaychargeRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(EF.PaychargeT),
					typeof(EF.ChargeoutPaycharge));

				if (isDelete)
				{
					db.ChargeoutPaycharges.Where(q => q.PaychargeRowId == PaychargeRowId).Delete();
					db.PaychargesT.Where(q => q.RowId == PaychargeRowId).Delete();
				}
				else
				{
					var Paycharge = db.PaychargesT.FirstOrDefault(q => q.RowId == PaychargeRowId);
					var ChargeoutPaycharges = db.ChargeoutPaycharges.Where(q => q.PaychargeRowId == PaychargeRowId).ToArray();

					var nPaycharge = mapper.Map<EF.PaychargeT>(entity);
					var nChargeoutPaycharges = mapper.Map<EF.ChargeoutPaycharge[]>(entity.ChargeoutPaycharges);
					nChargeoutPaycharges.ForEach(q => q.PaychargeRowId = PaychargeRowId);

					if (Paycharge == null)
					{
						Paycharge = nPaycharge;
						db.PaychargesT.Add(Paycharge);
					}
					else
					{
						mapper.Map(nPaycharge, Paycharge);
					}
					db.SaveChangesEx();

					DbUpdateRowsHelper.UpdateList(ChargeoutPaycharges, nChargeoutPaycharges, q => q.RowId, db, this);
				}

				scope.Complete();
			}
		}
	}
}

