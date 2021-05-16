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
		public IEnumerable<DTO.TreatmentPlanRecord> GetTreatmentPlanRecordList(Guid? rowId, Guid? patientRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.TreatmentPlanRecordV>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (patientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
			}

			var qry = db.TreatmentPlanRecordsV.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.TreatmentPlanRecordV));
			var rows = mapper.Map<List<DTO.TreatmentPlanRecord>>(list);
			return rows;
		}


		public void UpdateTreatmentPlanRecordCore(DTO.TreatmentPlanRecord entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var rowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.TreatmentPlanRecordT));

				if (isDelete)
				{
					db.TreatmentPlanRecordsT.Where(q => q.RowId == rowId).Delete();
				}
				else
				{
					var row = db.TreatmentPlanRecordsT.FirstOrDefault(q => q.RowId == rowId);

					var nRow = mapper.Map<EF.TreatmentPlanRecordT>(entity);

					if (row == null)
					{
						row = nRow;
						db.TreatmentPlanRecordsT.Add(row);
					}
					else
					{
						mapper.Map(nRow, row);
					}
					db.SaveChangesEx();
				}

				scope.Complete();
			}
		}
	}
}

