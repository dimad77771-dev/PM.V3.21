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
		public IEnumerable<DTO.Chargeout> GetChargeoutList(Guid? rowId, Guid? chargeoutRecipientRowId, int? noPaidOnly, bool flagNoPaidOrNoApprovedAmount, bool negativeBalanceOnly, DateTime? chargeoutDateFrom, DateTime? chargeoutDateTo, bool isShowSentOnly, bool isShowPaidOnly, DateTime? createdDateFrom, DateTime? createdDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);


			var wh = ExpressionFunc.True<EF.ChargeoutV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (chargeoutRecipientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutRecipientRowId == chargeoutRecipientRowId);
			}
			if (noPaidOnly == 1)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeRequest > 0);
			}
			if (flagNoPaidOrNoApprovedAmount)
			{
				var wh1 = ExpressionFunc.False<EF.ChargeoutV>();
				wh1 = PredicateBuilder.Or(wh1, q => q.PaychargeRequest > 0);

				wh = PredicateBuilder.And(wh, wh1);
			}
			if (negativeBalanceOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.PaychargeRequest < 0);
			}
			if (chargeoutDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutDate >= chargeoutDateFrom);
			}
			if (chargeoutDateTo != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutDate <= chargeoutDateTo);
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
			if (isShowPaidOnly)
			{
				wh = PredicateBuilder.And(wh, q => q.ChargeoutPaycharges.Any());
			}

			var qry = db.ChargeoutsV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.ChargeoutV));
			var rows = mapper.Map<List<DTO.Chargeout>>(list);
			return rows;
		}


		public DTO.Chargeout GetChargeout(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var sqlquery = db.ChargeoutsV
				.Include(q => q.ChargeoutRecipient)
				.Include(q => q.ChargeoutItems)
				.Include(q => q.ChargeoutPaycharges)
				.Include(q => q.ChargeoutPaycharges.Select(x => x.Paycharge))
				.Include(q => q.ChargeoutRefcharges)
				.Include(q => q.ChargeoutRefcharges.Select(x => x.Refcharge))
				.Include(q => q.ChargeoutItems.Select(x => x.InvoiceItem))
				.Include(q => q.ChargeoutItems.Select(x => x.InvoiceItem.Appointment));
			var row = sqlquery.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions()
					.AddIncludeProp<DTO.Chargeout>((q) => q.ChargeoutRecipient)
					.AddIncludeProp<DTO.Chargeout>((q) => q.ChargeoutItems)
					.AddIncludeProp<DTO.Chargeout>((q) => q.ChargeoutPaycharges)
					.AddIncludeProp<DTO.ChargeoutPaycharge>((q) => q.Paycharge)
					.AddIncludeProp<DTO.Chargeout>((q) => q.ChargeoutRefcharges)
					.AddIncludeProp<DTO.ChargeoutRefcharge>((q) => q.Refcharge)
					.AddIncludeProp<DTO.ChargeoutItem>((q) => q.InvoiceItem)
					.AddIncludeProp<DTO.InvoiceItem>((q) => q.Appointment);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options,
				typeof(EF.ChargeoutV), typeof(EF.ChargeoutItem), typeof(EF.InvoiceItem),
				typeof(EF.ChargeoutPaycharge), typeof(EF.PaychargeV), typeof(EF.PaychargeT),
				typeof(EF.ChargeoutRefchargeV), typeof(EF.RefchargeV),
				typeof(EF.ChargeoutRecipient), typeof(EF.AppointmentV));
			var ret = mapper.Map<DTO.Chargeout>(row);

			return ret;
		}


		public ServerReturnUpdateChargeout UpdateChargeoutCore(DTO.Chargeout entity)
		{
			lock (GlobalLocker.ChargeoutUpdate)
			{
				var result = new ServerReturnUpdateChargeout();
				var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
				using (var scope = new TransactionScope())
				{
					var chargeoutRowId = entity.RowId;
					var mapper = AutoMapperHelper.GetPocoMapper(
						typeof(EF.ChargeoutT),
						typeof(EF.ChargeoutItem),
						typeof(EF.ChargeoutPaycharge));

					InventoryFunc.BeforeUpdateChargeout(db, chargeoutRowId);

					var chargeout = db.ChargeoutsT.FirstOrDefault(q => q.RowId == chargeoutRowId);
					var chargeoutItems = db.ChargeoutItems.Where(q => q.ChargeoutRowId == chargeoutRowId).ToArray();
					var chargeoutPaycharges = db.ChargeoutPaycharges.Where(q => q.ChargeoutRowId == chargeoutRowId).ToArray();

					var nChargeout = mapper.Map<EF.ChargeoutT>(entity);
					var nChargeoutItems = mapper.Map<EF.ChargeoutItem[]>(entity.ChargeoutItems);
					nChargeoutItems.ForEach(q => q.ChargeoutRowId = chargeoutRowId);

					var nChargeoutPaycharges = mapper.Map<EF.ChargeoutPaycharge[]>(entity.ChargeoutPaycharges);
					nChargeoutPaycharges.ForEach(q => q.ChargeoutRowId = chargeoutRowId);

					if (chargeout == null)
					{
						chargeout = nChargeout;
						var chargeoutNumber = GenerateChargeoutNumber(db);
						chargeout.ChargeoutNumber = chargeoutNumber;
						result.ChargeoutNumber = chargeoutNumber;
						db.ChargeoutsT.Add(chargeout);
					}
					else
					{
						mapper.Map(nChargeout, chargeout);
					}

					try
					{
						db.SaveChangesEx();
					}
					catch (Exception ex)
					{
						if (ExceptionHelper.IsChargeoutNumberDuplicateConstraintException(ex))
						{
							ExceptionHelper.UserUpdateError(UserErrorCodes.ChargeoutNumberDuplicate, "Outgoing Invoice Number \"" + chargeout.ChargeoutNumber + "\" is duplicate");
						}
						else throw new AggregateException(ex);
					}

					DbUpdateRowsHelper.UpdateList(chargeoutItems, nChargeoutItems, q => q.RowId, db, this);
					DbUpdateRowsHelper.UpdateList(chargeoutPaycharges, nChargeoutPaycharges, q => q.RowId, db, this);

					InventoryFunc.AfterUpdateChargeout(db, chargeoutRowId);

					scope.Complete();
				}
				return result;
			}
		}
		

		public ServerReturnUpdateChargeout DeleteChargeoutCore(List<Guid> chargeoutRowIds)
		{
			lock (GlobalLocker.ChargeoutUpdate)
			{
				var result = new ServerReturnUpdateChargeout();
				var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
				using (var scope = new TransactionScope())
				{
					foreach (var chargeoutRowId in chargeoutRowIds)
					{
						InventoryFunc.BeforeUpdateChargeout(db, chargeoutRowId);
						db.ChargeoutItems.Where(q => q.ChargeoutRowId == chargeoutRowId).Delete();
						db.ChargeoutPaycharges.Where(q => q.ChargeoutRowId == chargeoutRowId).Delete();
						db.EmailChargesT.Where(q => q.ChargeoutRowId == chargeoutRowId).SelectMany(q => q.EmailChargeAttachments).Delete();
						db.EmailChargesT.Where(q => q.ChargeoutRowId == chargeoutRowId).SelectMany(q => q.EmailChargeRecipients).Delete();
						db.EmailChargesT.Where(q => q.ChargeoutRowId == chargeoutRowId).Delete();
						db.ChargeoutsT.Where(q => q.RowId == chargeoutRowId).Delete();
					}

					scope.Complete();
				}
				return result;
			}
		}


		String GenerateChargeoutNumber(EF.PracticeManagerEntities db)
		{
			//System.Threading.Thread.Sleep(20000);

			var prefix = "O-" + DateTime.Today.ToString("yyyyMMdd") + "-";

			var existsNumbers = db.ChargeoutsV.Where(q => q.ChargeoutNumber.StartsWith(prefix)).Select(q => q.ChargeoutNumber).ToList();
			var random = new Random();

			for (int len = 2; len <= 5; len++)
			{
				var maxnum = (int)Math.Pow(10, len) - 1;
				var format = new string('0', len);
				var allVariants = Enumerable.Range(1, maxnum).Select(q => prefix + q.ToString(format)).Where(q => !existsNumbers.Contains(q)).ToArray();
				if (allVariants.Length > 0)
				{
					var npp = random.Next(0, allVariants.Length - 1);
					var chargeoutNumber = allVariants[npp];
					return chargeoutNumber;
				}
			}
			throw new NotSupportedException();
		}
	}
}

