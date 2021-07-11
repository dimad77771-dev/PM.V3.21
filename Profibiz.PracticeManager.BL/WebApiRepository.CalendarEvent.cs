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
using Profibiz.PracticeManager.SharedCode;
using System.Threading;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.CalendarEvent> GetCalendarEventList(Guid? patientRowId, Guid? serviceProviderRowId, Guid? rowId, DateTime? startFrom, DateTime? startTo, Boolean? completed, string rowIds, bool? forRemainder, bool? isVacation)
		{
			//Thread.Sleep(5000);

			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.CalendarEventV>();
			if (patientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
			}
			if (serviceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);
			}
			if (rowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			}
			if (!string.IsNullOrEmpty(rowIds))
			{
				var ids = WebQueryHelper.Guids(rowIds);
				wh = PredicateBuilder.And(wh, q => ids.Contains(q.RowId));
			}
			if (startFrom != null)
			{
				wh = PredicateBuilder.And(wh, q => q.Start >= startFrom);
			}
			if (startTo != null)
			{
                startTo = startTo.Value.AddDays(1);
                wh = PredicateBuilder.And(wh, q => q.Start < startTo);
			}
			if (completed != null)
			{
				wh = PredicateBuilder.And(wh, q => q.Completed == completed.Value);
			}
			if (forRemainder == true)
			{
				wh = PredicateBuilder.And(wh, q => db.CalendarEventsForRemainders.Any(z => z.RowId == q.RowId));
			}
			if (isVacation != null)
			{
				wh = PredicateBuilder.And(wh, q => q.IsVacation == isVacation.Value || q.IsBusyEvent == isVacation.Value);
			}
			var list = db.CalendarEventsV.Include(q => q.Patient).Where(wh.Expand()).ToArray();

			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.CalendarEvent>((q) => q.Patient);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.CalendarEventV), typeof(EF.PatientV));
			var ret = mapper.Map<List<DTO.CalendarEvent>>(list);
			return ret;
		}


		public void UpdateCalendarEventCore(List<DTO.CalendarEvent> entities, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				//update
				var updateRows = new List<EF.CalendarEventT>();
				foreach (var entity in entities)
				{
					var isDelete = (state == EntityState.Deleted);
					var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.CalendarEventT));

					if (!isDelete)
					{
						if (entity.UpdateFlagRemainderFieldsOnly)
						{
							var row = db.CalendarEventsT.FirstOrDefault(q => q.RowId == entity.RowId);
							if (row != null)
							{
								row.RemainderInMinutes = entity.RemainderInMinutes;
								row.IsDisabled = entity.IsDisabled;
								row.SnoozedTo = entity.SnoozedTo;
								db.SaveChangesEx();
								updateRows.Add(row);
							}
						}
						else
						{
							var row = mapper.Map<EF.CalendarEventT>(entity);
							var entry = db.Entry(row);
							entry.State = state;
							db.SaveChangesEx();
							updateRows.Add(row);
						}
					}


					if (isDelete)
					{
						try
						{
							db.CalendarEventsT.Where(q => q.RowId == entity.RowId).Delete();
						}
						catch (Exception ex)
						{
							if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
							{
								ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Calendar Event \"" + "" + "\" is used in database and cannot be deleted");
							}
							else throw ex;
						}
					}
				}

				

				scope.Complete();
			}
		}
	}
}

