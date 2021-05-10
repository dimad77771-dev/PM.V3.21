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
		public IEnumerable<DTO.SchedulerRecord> GetSchedulerRecords(Guid serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var wh = ExpressionFunc.True<EF.SchedulerRecord>();
			wh = PredicateBuilder.And(wh, q => q.ServiceProviderRowId == serviceProviderRowId);
	

			var qry = 
				db.SchedulerRecords
				.Include(q => q.SchedulerRecordItems)
				.Where(wh.Expand());
			
			var list = qry.Where(wh.Expand()).ToArray();


			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.SchedulerRecord>((q) => q.SchedulerRecordItems);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.SchedulerRecord), typeof(EF.SchedulerRecordItem));
			var rows = mapper.Map<List<DTO.SchedulerRecord>>(list);

			return rows;
		}

		public void PutSchedulerRecords(List<DTO.SchedulerRecord> rows)
		{
			var db = EF.PracticeManagerEntities.Connection;
			using (var scope = new TransactionScope())
			{
				var serviceProviderRowId = rows[0].ServiceProviderRowId;

				//delete
				db.SchedulerRecords.Where(q => q.ServiceProviderRowId == serviceProviderRowId).SelectMany(q => q.SchedulerRecordItems).Delete();
				db.SchedulerRecords.Where(q => q.ServiceProviderRowId == serviceProviderRowId).Delete();

				//insert
				var options = AutoMapperHelper.CreateOptions();
				options.AddIncludeProp<DTO.SchedulerRecord>(q => q.SchedulerRecordItems);
				var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.SchedulerRecord), typeof(EF.SchedulerRecordItem));
				var entities = mapper.Map<List<EF.SchedulerRecord>>(rows);
				entities.ForEach(q =>
				{
					db.SchedulerRecords.Add(q);
					q.SchedulerRecordItems.ForEach(z =>
					{
						db.SchedulerRecordItems.Add(z);
					});
				});
				


				db.SaveChangesEx();
				scope.Complete();
			}
		}


		public IEnumerable<CalculateAppointmentStartFinishResult> CalculateAppointmentStartFinish(Guid serviceProviderRowId, DateTime[] dates)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var resultList = new List<CalculateAppointmentStartFinishResult>();
			var serviceProvider = db.ServiceProviders.Single(q => q.RowId == serviceProviderRowId);
			var appointmentBook = db.AppointmentBooks.Single(q => q.RowId == serviceProvider.AppointmentBookRowId);
			var interval = appointmentBook.Interval;
			var maximumDayAppointments = serviceProvider.MaximumDayAppointments;
			var schedulerRecords = db.SchedulerRecords.Include(q => q.SchedulerRecordItems).Where(q => q.ServiceProviderRowId == serviceProviderRowId).ToList();

			foreach (var date in dates)
			{
				var date2 = date.AddDays(1);
				var appointments = db.AppointmentsT
					.Where(q => q.ServiceProviderRowId == serviceProviderRowId && (q.Start >= date && q.Start < date2))
					.OrderBy(q => q.Start)
					.ToArray();
				var schedulerRecordItem = SchedulerRecordFunc.FindItemForDate(schedulerRecords, date);

				CalculateAppointmentStartFinishResult result = null;
				if (schedulerRecordItem == null)
				{
					result = CalculateAppointmentStartFinishResult.BuildError("The specialist is off duty on this day");
				}
				else
				{
					var startTime = schedulerRecordItem.StartTime;
					var finishTime = schedulerRecordItem.FinishTime;

					for (int i = -1; i < appointments.Length; i++)
					{
						var appoinmentStart = (i == -1 ? startTime : appointments[i].Finish);   //вариант начала
						var appoinmentFinish = appoinmentStart.AddMinutes(interval);			//вариант конца 
						var existsIntersection = appointments.Any(q => !(q.Start >= appoinmentFinish || q.Finish <= appoinmentStart));	//есть ли пересекающиеся с предлагаемым
						if (!existsIntersection)
						{
							result = new CalculateAppointmentStartFinishResult
							{
								Start = appoinmentStart,
								Finish = appoinmentFinish,
							};
							break;
						}
					}

					if (result == null) throw new ArgumentException();

					if (result.Finish > finishTime)
					{
						result = CalculateAppointmentStartFinishResult.BuildError("There are no available timeslot to place the appointment");
					}
					else if (appointments.Length >= maximumDayAppointments)
					{
						result = CalculateAppointmentStartFinishResult.BuildError("Maximum number of appointments is reached for this specialist");
					}
				}

				result.Date = date;
				resultList.Add(result);
			}


			return resultList;
		}

	}
}

