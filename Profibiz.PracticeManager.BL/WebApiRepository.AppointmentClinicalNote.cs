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
		public IEnumerable<DTO.AppointmentClinicalNote> GetAppointmentClinicalNoteList(Guid? rowId, Guid? appointmentRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.AppointmentClinicalNote>();
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (appointmentRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.AppointmentRowId == appointmentRowId);
			}

			var qry = db.AppointmentClinicalNotes.Where(wh.Expand());
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.AppointmentClinicalNote));
			var rows = mapper.Map<List<DTO.AppointmentClinicalNote>>(list);
			return rows;
		}


		public void UpdateAppointmentClinicalNoteCore(DTO.AppointmentClinicalNote entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var rowId = entity.RowId;
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.AppointmentClinicalNote));

				if (isDelete)
				{
					db.AppointmentClinicalNotes.Where(q => q.RowId == rowId).Delete();
				}
				else
				{
					var row = db.AppointmentClinicalNotes.FirstOrDefault(q => q.RowId == rowId);

					var nRow = mapper.Map<EF.AppointmentClinicalNote>(entity);

					if (row == null)
					{
						row = nRow;
						db.AppointmentClinicalNotes.Add(row);
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

