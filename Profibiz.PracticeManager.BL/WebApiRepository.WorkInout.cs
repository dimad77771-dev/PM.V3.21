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
		public DTO.WorkInout GetWorkInout(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var sqlquery = db.WorkInouts;
			var row = sqlquery.FirstOrDefault(q => q.RowId == id);

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.WorkInout));
			var ret = mapper.Map<DTO.WorkInout>(row);

			return ret;
		}

		public IEnumerable<DTO.WorkInout> GetWorkInoutList(Guid? rowId, DateTime? workInoutDateFrom, DateTime? workInoutDateTo)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.WorkInout>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (workInoutDateFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.Start >= workInoutDateFrom);
			}
			if (workInoutDateTo != null)
			{
				var workInoutDateTo2 = workInoutDateTo.Value.AddDays(1);
				wh = PredicateBuilder.And(wh, q => q.Start < workInoutDateTo2);
			}

			var qry = db.WorkInouts.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.WorkInout));
			var rows = mapper.Map<List<DTO.WorkInout>>(list);
			return rows;
		}

		public void UpdateWorkInoutCore(DTO.WorkInout entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var workInoutRowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.WorkInout));

				if (isDelete)
				{
					db.WorkInouts.Where(q => q.RowId == workInoutRowId).Delete();
				}
				else
				{
					var workInout = db.WorkInouts.SingleOrDefault(q => q.RowId == workInoutRowId);
					var nWorkInout = mapper.Map<EF.WorkInout>(entity);

					if (workInout == null)
					{
						workInout = nWorkInout;
						db.WorkInouts.Add(workInout);
					}
					else
					{
						mapper.Map(nWorkInout, workInout);
					}
					db.SaveChangesEx();
				}

				scope.Complete();
			}
		}
	}
}

