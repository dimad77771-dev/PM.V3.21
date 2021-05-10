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
using System.Data.Entity;
using EntityFramework.Extensions;
using Profibiz.PracticeManager.Model;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Xml.Linq;
using System.Linq.Expressions;
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public DTO.InsuranceCoverage GetInsuranceCoverage(Guid id)
		{
			var db = EF.PracticeManagerEntities.Connection;
			var insuranceCoverage = 
				db
				.InsuranceCoverages
				.Include(q => q.InsuranceCoverageItems)
				.Include(q => q.InsuranceCoverageHolders)
				.Include(q => q.InsuranceCoverageHolders.Select(z => z.Patient))
				.Include(q => q.InsuranceCoverageServices)
				.Include(q => q.InsuranceCoverageServices.Select(z => z.InsuranceCoverageHolderServices))
				.Include(q => q.InsuranceCoverageItems.Select(z => z.InsuranceCoverageItemHolders))
				.Include(q => q.InsuranceCoverageItems.Select(z => z.InsuranceCoverageItemCategories))
				.FirstOrDefault(q => q.RowId == id);
			var insuranceCoverageHolderServices = insuranceCoverage.InsuranceCoverageServices.SelectMany(q => q.InsuranceCoverageHolderServices);
			var insuranceCoverageItemCategories = insuranceCoverage.InsuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemCategories).ToArray();
			var insuranceCoverageItemHolders = insuranceCoverage.InsuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemHolders).ToArray();


			var options = new AutoMapperHelper.Options();
			options.AddIncludeProp<DTO.InsuranceCoverageHolder>(q => q.Patient);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(
				options,
				typeof(EF.Patient),
				typeof(EF.InsuranceCoverage), 
				typeof(EF.InsuranceCoverageService),
				typeof(EF.InsuranceCoverageHolder),
				typeof(EF.InsuranceCoverageHolderService),
				typeof(EF.InsuranceCoverageItem),
				typeof(EF.InsuranceCoverageItemCategory),
				typeof(EF.InsuranceCoverageItemHolder));

			var ret = mapper.Map<DTO.InsuranceCoverage>(insuranceCoverage);
			ret.InsuranceCoverageHolders = mapper.Map<List<DTO.InsuranceCoverageHolder>>(insuranceCoverage.InsuranceCoverageHolders);
			ret.InsuranceCoverageServices = mapper.Map<List<DTO.InsuranceCoverageService>>(insuranceCoverage.InsuranceCoverageServices);
			ret.InsuranceCoverageItems = mapper.Map<List<DTO.InsuranceCoverageItem>>(insuranceCoverage.InsuranceCoverageItems);
			
			ret.InsuranceCoverageHolderServices = mapper.Map<List<DTO.InsuranceCoverageHolderService>>(insuranceCoverageHolderServices);

			ret.InsuranceCoverageItemCategories = mapper.Map<List<DTO.InsuranceCoverageItemCategory>>(insuranceCoverageItemCategories);
			ret.InsuranceCoverageItemHolders = mapper.Map<List<DTO.InsuranceCoverageItemHolder>>(insuranceCoverageItemHolders);

			ret.PolicyOwner = mapper.Map<DTO.Patient>(insuranceCoverage.GetPolicyOwner());
			ret.PolicyOwnerRowId = ret.PolicyOwner?.RowId;

			return ret;
		}

		public void UpdateInsuranceCoverageCore(DTO.InsuranceCoverage entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var mapper = AutoMapperHelper.GetPocoMapper(
					typeof(DTO.InsuranceCoverage),
					typeof(DTO.InsuranceCoverageService),
					typeof(DTO.InsuranceCoverageHolder),
					typeof(DTO.InsuranceCoverageHolderService),
					typeof(DTO.InsuranceCoverageItem),
					typeof(DTO.InsuranceCoverageItemCategory),
					typeof(DTO.InsuranceCoverageItemHolder));

				if (!isDelete)
				{
					var row = mapper.Map<EF.InsuranceCoverage>(entity);
					var entry = db.Entry(row);
					entry.State = state;
					db.SaveChangesEx();
				}

				var insuranceCoverageServices = db.InsuranceCoverageServices.Where(q => q.InsuranceCoverageRowId == entity.RowId);
				var insuranceCoverageHolders = db.InsuranceCoverageHolders.Where(q => q.InsuranceCoverageRowId == entity.RowId);
				var insuranceCoverageHolderServices = insuranceCoverageServices.SelectMany(q => q.InsuranceCoverageHolderServices);

				var insuranceCoverageItems = db.InsuranceCoverageItems.Where(q => q.InsuranceCoverageRowId == entity.RowId);
				var insuranceCoverageItemCategories = insuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemCategories);
				var insuranceCoverageItemHolders = insuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemHolders);

				insuranceCoverageHolderServices.Delete();

				insuranceCoverageItemCategories.Delete();
				insuranceCoverageItemHolders.Delete();
				insuranceCoverageItems.Delete();

				insuranceCoverageHolders.Delete();
				insuranceCoverageServices.Delete();

				if (isDelete)
				{
					db.InsuranceCoverages.Where(q => q.RowId == entity.RowId).Delete();
				}

				if (!isDelete)
				{
					var eInsuranceCoverageServices = mapper.Map<List<EF.InsuranceCoverageService>>(entity.InsuranceCoverageServices);
					db.InsuranceCoverageServices.AddRange(eInsuranceCoverageServices);
					db.SaveChangesEx();

					var eInsuranceCoverageHolders = mapper.Map<List<EF.InsuranceCoverageHolder>>(entity.InsuranceCoverageHolders);
					db.InsuranceCoverageHolders.AddRange(eInsuranceCoverageHolders);
					db.SaveChangesEx();

					var eInsuranceCoverageHolderServices = mapper.Map<List<EF.InsuranceCoverageHolderService>>(entity.InsuranceCoverageHolderServices);
					db.InsuranceCoverageHolderServices.AddRange(eInsuranceCoverageHolderServices);
					db.SaveChangesEx();

					var eInsuranceCoverageItems = mapper.Map<List<EF.InsuranceCoverageItem>>(entity.InsuranceCoverageItems);
					db.InsuranceCoverageItems.AddRange(eInsuranceCoverageItems);
					db.SaveChangesEx();

					var eInsuranceCoverageItemCategories = mapper.Map<List<EF.InsuranceCoverageItemCategory>>(entity.InsuranceCoverageItemCategories);
					db.InsuranceCoverageItemCategories.AddRange(eInsuranceCoverageItemCategories);
					db.SaveChangesEx();

					var eInsuranceCoverageItemHolders = mapper.Map<List<EF.InsuranceCoverageItemHolder>>(entity.InsuranceCoverageItemHolders);
					db.InsuranceCoverageItemHolders.AddRange(eInsuranceCoverageItemHolders);
					db.SaveChangesEx();
				}


				scope.Complete();
			}

		}

		public DTO.InsuranceCoverageInvoiceClaimsInfo[] GetInsuranceCoverageInvoiceClaimsInfo(String insuranceCoverageRowIdsStr)
		{
			return null;
			//var db = EF.PracticeManagerEntities.Connection;

			//var insuranceCoverageRowIds = WebQueryHelper.Guids(insuranceCoverageRowIdsStr);
			//var qry =
			//	from a in db.InsuranceCoverageJoinInvoiceClaimV
			//	join b in db.InsuranceCoveragePatientCategory2ItemV
			//		on new { a.InsuranceCoverageRowId, a.PatientRowId, a.CategoryRowId } equals new { b.InsuranceCoverageRowId, b.PatientRowId, b.CategoryRowId }
			//	join c in db.InvoiceClaimsV
			//		on a.InvoiceClaimRowId equals c.RowId
			//	where insuranceCoverageRowIds.Contains(a.InsuranceCoverageRowId)
			//	select new DTO.InsuranceCoverageInvoiceClaimsInfo
			//	{
			//		InsuranceCoverageRowId = a.InsuranceCoverageRowId,
			//		CoversAllHolders = b.CoversAllHolders,
			//		InsuranceCoverageItemRowId = b.InsuranceCoverageItemRowId,
			//		InsuranceCoverageItemHolderRowId = b.InsuranceCoverageItemHolderRowId,
			//		PatientRowId = a.PatientRowId,
			//		CategoryRowId = a.CategoryRowId,
			//		TotalAmont = b.InsuranceCoverageItemHolderRowId != null ?
			//			db.InsuranceCoverageItemHolders.Where(q => q.RowId == b.InsuranceCoverageItemHolderRowId).Sum(q => q.AnnualAmountCovered) ?? 0 :
			//			db.InsuranceCoverageItems.Where(q => q.RowId == b.InsuranceCoverageItemRowId).Sum(q => q.AnnualAmountCovered) ?? 0,
			//		SentAmont = c.SentAmont,
			//		ApproveAmont = c.ApproveAmont ?? 0,
			//	};
			//var rows = qry.ToArray();

			//return rows;
		}

		public DTO.InsurancePatientCategoryInfo GetInsurancePatientCategoryInfo(String insuranceCoverageRowIdsStr)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var insuranceCoverageRowIds = WebQueryHelper.GuidsNull(insuranceCoverageRowIdsStr);

			var ret = new DTO.InsurancePatientCategoryInfo();

			var rows = db.InsuranceArticleV.Where(q => insuranceCoverageRowIds.Contains(q.InsuranceCoverageRowId)).ToList();

			ret.Articles = 
				rows
				.Select(a => new DTO.InsurancePatientCategoryInfo.Article
				{
					CoversAllHolders = a.CoversAllHolders,
					InsuranceCoverageRowId = a.InsuranceCoverageRowId,
					InsuranceCoverageItemHolderRowId = a.InsuranceCoverageItemHolderRowId,
					InsuranceCoverageItemRowId = a.InsuranceCoverageItemRowId,

					TotalAmont = a.AnnualAmountCovered ?? 0,
					TotalUnits = a.MaximumQuantity ?? 0,
					CategoryRowIds = XDocumentFunc.ParseArray(a.CategoryInfo).Select(q => (Guid)q.Element("CategoryRowId")).ToList(),
					PatientRowIds = XDocumentFunc.ParseArray(a.PatientInfo).Select(q => (Guid)q.Element("PatientRowId")).ToList(),

					ApproveAmount = XDocumentFunc.ParseArray(a.ClaimInfo).Sum(q => (Decimal?)q.Element("Amount")) ?? 0,
					ApproveUnits = XDocumentFunc.ParseArray(a.ClaimInfo).Sum(q => (Decimal?)q.Element("Units")) ?? 0,

				})
				.ToList();

			return ret;
		}

		public DTO.InsuranceArticleInfo GetInsuranceArticleInfo(Guid insuranceCoverageRowId, Guid patientRowId, Guid categoryRowId, Boolean isShowAllYears, Boolean showProblemOnly)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var ret = new DTO.InsuranceArticleInfo();

			var insuranceCoverageRowIds = new List<Guid>{ insuranceCoverageRowId };
			if (isShowAllYears)
			{
				var irow = db.CHECKVIEW_InsuranceCoverages.Single(q => q.InsuranceCoverageRowId == insuranceCoverageRowId);
				insuranceCoverageRowIds = 
					db.CHECKVIEW_InsuranceCoverages
					.Where(q => q.PolicyNumber == irow.PolicyNumber && q.PolicyHolderRowId == irow.PolicyHolderRowId)
					.Select(q => q.InsuranceCoverageRowId)
					.ToList();
			}

			var InvoiceClaimDetailRowIds =
				showProblemOnly ?

				db.InvoiceClaimDetail2InsurancePatientCategoryV
				.Where(q => !db.InsurancePatientCategory2InsuranceArticleV.Any(z => z.InsuranceCoverageRowId == q.InsuranceCoverageRowId && z.PatientRowId == q.PatientRowId && z.CategoryRowId == q.CategoryRowId))
				.Select(q => q.InvoiceClaimDetailRowId)
				.ToArray() :

				db.InsurancePatientCategory2InvoiceClaimDetailV
				.Where(q => insuranceCoverageRowIds.Contains(q.InsuranceCoverageRowId) && q.PatientRowId == patientRowId && q.CategoryRowId == categoryRowId)
				.Select(q => q.InvoiceClaimDetailRowId)
				.ToArray();
				

			var gqry = 
				from a in db.InvoiceItems
				where a.ServcieOrSupplyRowId != null
				group a by new { a.InvoiceRowId, a.ServcieOrSupplyRowId } into g
				select new
				{
					InvoiceRowId = g.Key.InvoiceRowId,
					ServcieOrSupplyRowId = (Guid)g.Key.ServcieOrSupplyRowId,
					SumUnits = g.Sum(q => q.Units),
					SumAmount = g.Sum(q => q.Amount),
				}
				;

			var qry =
				from b in db.InvoiceClaimDetails 
				join c in db.InvoiceClaimsV on b.InvoiceClaimRowId equals c.RowId
				join d in db.InvoicesV on c.InvoiceRowId equals d.RowId
				join e in db.InsuranceCoveragesV on c.InsuranceCoverageRowId equals e.RowId
				join f in gqry on new { c.InvoiceRowId, b.ServcieOrSupplyRowId } equals new { f.InvoiceRowId, f.ServcieOrSupplyRowId }
				where InvoiceClaimDetailRowIds.Contains(b.RowId)
				select new
				{
					InvoiceClaimDetail = b,
					InvoiceClaim = c,
					Invoice = d,
					InsuranceCoverage = e,
					InvoiceItems = f,
				}
			;
			var rows = qry.ToArray();


			var options = new AutoMapperHelper.Options();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(
				options,
				typeof(EF.InvoiceClaimDetail),
				typeof(EF.InvoiceClaimV),
				typeof(EF.InvoiceV),
				typeof(EF.InsuranceCoverageV));

			var erows = rows.Select(q => new DTO.InsuranceArticleInfo.Row
			{
				InvoiceClaimDetail = mapper.Map<DTO.InvoiceClaimDetail>(q.InvoiceClaimDetail),
				InvoiceClaim = mapper.Map<DTO.InvoiceClaim>(q.InvoiceClaim),
				Invoice = mapper.Map<DTO.Invoice>(q.Invoice),
				InsuranceCoverage = mapper.Map<DTO.InsuranceCoverage>(q.InsuranceCoverage),
				InvoiceItems = new DTO.InsuranceArticleInfo.InvoiceItemsClass { SumAmount = q.InvoiceItems.SumAmount, SumUnits = q.InvoiceItems.SumUnits },
			}).ToArray();

			ret.Rows = erows;

			return ret;
		}

		public DTO.InsuranceArticleSummary GetInsuranceArticleSummary(string categoriesRowIds)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var categoriesRowIdSet = WebQueryHelper.Guids(categoriesRowIds);

			var ret = new DTO.InsuranceArticleSummary();

			var rows = db.InsuranceArticleV
				.Select(q => new
				{
					InsuranceArticle = q,
					InsuranceCoverage = db.InsuranceCoveragesV.FirstOrDefault(z => z.RowId == q.InsuranceCoverageRowId),
				}).ToArray();

			var options = new AutoMapperHelper.Options();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(
				options,
				typeof(EF.InsuranceArticleV),
				typeof(EF.InsuranceCoverageV));

			ret.Rows = rows.Select(q =>
			{
				var obj = mapper.Map<DTO.InsuranceArticleV>(q.InsuranceArticle);
				obj.InsuranceCoverage = mapper.Map<DTO.InsuranceCoverage>(q.InsuranceCoverage);
				return obj;
			}).ToArray();

			

			return ret;
		}


		public ServerReturnCloneInsuranceCoverage CloneInsuranceCoverage(DTO.InsuranceCoverage row)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var rowId = row.RowId;

			var insuranceCoverages = db.InsuranceCoverages.AsNoTracking().Where(q => q.RowId == rowId).ToArray();
			if (insuranceCoverages.Length != 1) throw new Exception("InsuranceCoverages not found");

			var insuranceCoverageServices = db.InsuranceCoverageServices.AsNoTracking()
				.Include(q => q.InsuranceCoverageHolderServices)
				.Where(q => q.InsuranceCoverageRowId == rowId).ToArray();
			var insuranceCoverageHolders = db.InsuranceCoverageHolders.AsNoTracking().Where(q => q.InsuranceCoverageRowId == rowId).ToArray();
			var insuranceCoverageHolderServices = insuranceCoverageServices.SelectMany(q => q.InsuranceCoverageHolderServices).ToArray();

			var insuranceCoverageItems = db.InsuranceCoverageItems.AsNoTracking()
				.Include(q => q.InsuranceCoverageItemCategories)
				.Include(q => q.InsuranceCoverageItemHolders)
				.Where(q => q.InsuranceCoverageRowId == rowId).ToArray();
			var insuranceCoverageItemCategories = insuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemCategories).ToArray();
			var insuranceCoverageItemHolders = insuranceCoverageItems.SelectMany(q => q.InsuranceCoverageItemHolders).ToArray();

			var allRowIdArrays = new[]
			{
				insuranceCoverages.Select(q => q.RowId),
				insuranceCoverageServices.Select(q => q.RowId),
				insuranceCoverageHolders.Select(q => q.RowId),
				insuranceCoverageHolderServices.Select(q => q.RowId),
				insuranceCoverageItems.Select(q => q.RowId),
				insuranceCoverageItemCategories.Select(q => q.RowId),
				insuranceCoverageItemHolders.Select(q => q.RowId),
			};
			var allRowIds = allRowIdArrays.SelectMany(q => q.Select(z => z));
			var replaceRowIds = new Dictionary<Guid, Guid>();
			allRowIds.ForEach(q => replaceRowIds.Add(q, Guid.NewGuid()));


			ReplaceGuids(insuranceCoverages, replaceRowIds													);
			ReplaceGuids(insuranceCoverageServices, replaceRowIds,				q => q.InsuranceCoverageRowId);
			ReplaceGuids(insuranceCoverageHolders, replaceRowIds,				q => q.InsuranceCoverageRowId);
			ReplaceGuids(insuranceCoverageHolderServices, replaceRowIds,		q => q.InsuranceCoverageServiceRowId, q => q.InsuranceCoverageHolderRowId);
			ReplaceGuids(insuranceCoverageItems, replaceRowIds,					q => q.InsuranceCoverageRowId);
			ReplaceGuids(insuranceCoverageItemCategories, replaceRowIds,		q => q.InsuranceCoverageItemRowId);
			ReplaceGuids(insuranceCoverageItemHolders, replaceRowIds,			q => q.InsuranceCoverageItemRowId, q => q.InsuranceCoverageHolderRowId);


			var insuranceCoverage = insuranceCoverages[0];
			var stratDate = insuranceCoverage.CoverageStartDate;
			var finishDate = insuranceCoverage.CoverageValidUntil.AddDays(1);
			var monthLenght = finishDate.Year * 12 + finishDate.Month - stratDate.Year * 12 - stratDate.Month;
			var newStart = finishDate;
			var newFinish = newStart.AddMonths(monthLenght);
			insuranceCoverage.CoverageStartDate = newStart;
			insuranceCoverage.CoverageValidUntil = newFinish.AddDays(-1);

			
			using (var scope = new TransactionScope())
			{
				db.InsuranceCoverages.AddRange(insuranceCoverages);
				db.InsuranceCoverageServices.AddRange(insuranceCoverageServices);
				db.InsuranceCoverageHolders.AddRange(insuranceCoverageHolders);
				db.InsuranceCoverageHolderServices.AddRange(insuranceCoverageHolderServices);
				db.InsuranceCoverageItems.AddRange(insuranceCoverageItems);
				db.InsuranceCoverageItemCategories.AddRange(insuranceCoverageItemCategories);
				db.InsuranceCoverageItemHolders.AddRange(insuranceCoverageItemHolders);

				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsCHECKVIEW_InsuranceCoveragesDuplicateConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.CloneInsuranceCoverageDuplicateStartDate, "Start Date \"" + insuranceCoverage.CoverageStartDate.ToShortDateString() + "\" already exists");
					}
					else throw new AggregateException(ex);
				}
				
				scope.Complete();
			}


			return new ServerReturnCloneInsuranceCoverage { CloneRowId = insuranceCoverages[0].RowId };
		}

		void ReplaceGuids<T>(IEnumerable<T> rows, Dictionary<Guid, Guid> replaceGuids, params Expression<Func<T, object>>[] propertyExpressions)
		{
			var props = propertyExpressions.Select(q => MapperReflectionHelper.FindProperty(q)).ToList();
			props.Add(typeof(T).GetProperty("RowId"));

			foreach (var row in rows)
			{
				foreach (var prop in props)
				{
					var oldId = (Guid?)prop.GetValue(row);
					if (oldId != null)
					{
						var newId = replaceGuids[(Guid)oldId];
						prop.SetValue(row, newId);
					}
				}
			}
		}





		public void BatchCloneInsuranceCoverage()
		{
			var db = EF.PracticeManagerEntities.Connection;
			var yy = 2019;
			var yy2 = yy + 1;

			var rows =
				db.InsuranceCoverages
				.Where(q =>
						q.CoverageStartDate == new DateTime(yy, 1, 1) &&
						q.CoverageValidUntil == new DateTime(yy, 12, 31) &&
						!db.InsuranceCoverages.Any(z => z.InsuranceProviderRowId == q.InsuranceProviderRowId && z.PolicyNumber == q.PolicyNumber && z.CoverageStartDate >= new DateTime(yy2, 1, 1)))
				.ToArray();
			//rows = rows;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.InsuranceCoverage));
			var drows = mapper.Map<DTO.InsuranceCoverage[]>(rows);

			using (var scope = new TransactionScope())
			{
				foreach (var drow in drows)
				{
					CloneInsuranceCoverage(drow);
				}

				scope.Complete();
			}
		}
	}
}
