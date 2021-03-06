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
using MimeKit;
using System.Configuration;
using System.Net.Mail;
using Profibiz.PracticeManager.Service;
using System.IO;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public IEnumerable<DTO.Appointment> GetAppointmentList(Guid? appointmentBookRowId, Guid? patientRowId, Guid? insuranceProvidersViewGroupRowId, Guid? rowId, DateTime? startFrom, DateTime? startTo, Boolean? completed, Boolean? inInvoice, Boolean? forChargeout, Boolean? calcAppointmentPaid, string rowIds, string hideStatuses2)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var includeAppointmentRemainders = (rowId != null);

			var wh = ExpressionFunc.True<EF.AppointmentV>();
			if (appointmentBookRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.AppointmentBookRowId == appointmentBookRowId);
			}
			if (patientRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
			}
			if (insuranceProvidersViewGroupRowId != null)
			{
				var qryPatient = db.PatientInsuranceProvidersViewGroupViews
					.Where(q => q.InsuranceProvidersViewGroupRowId == insuranceProvidersViewGroupRowId)
					.Select(q => q.PatientRowId);
				wh = PredicateBuilder.And(wh, q => qryPatient.Contains(q.PatientRowId));
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
			if (inInvoice != null)
			{
				if (inInvoice.Value)
				{
					wh = PredicateBuilder.And(wh, q => q.InvoiceRowId != null);
				}
				else
				{
					wh = PredicateBuilder.And(wh, q => q.InvoiceRowId == null);
				}
			}
			if (forChargeout == true)
			{
				wh = PredicateBuilder.And(wh, q => q.InvoiceItem != null && !q.InvoiceItem.ChargeoutItems.Any() && !q.IsIgnoreForChargeout);
			}
			if (!string.IsNullOrEmpty(hideStatuses2))
			{
				var statusRowIds = hideStatuses2.Split(';').Select(q => new Guid(q)).ToArray();
				wh = PredicateBuilder.And(wh, q => !statusRowIds.Any(z => z == q.Status2RowId));
			}

			var qry = db.AppointmentsV
						.Include(q => q.Patient)
						.Where(wh.Expand());
			if (forChargeout == true)
			{
				qry = qry.Include(q => q.InvoiceItem);
			}
			if (includeAppointmentRemainders)
			{
				qry = qry.Include(q => q.AppointmentRemainders);
			}

			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.Appointment>((q) => q.Patient);
			if (forChargeout == true)
			{
				options = options.AddIncludeProp<DTO.Appointment>((q) => q.InvoiceItem);
			}
			if (includeAppointmentRemainders)
			{
				options = options.AddIncludeProp<DTO.Appointment>((q) => q.AppointmentRemainders);
			}
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.AppointmentV), typeof(EF.Patient), typeof(EF.InvoiceItem), typeof(EF.AppointmentRemainder));


			DTO.Appointment[] ret;
			if (calcAppointmentPaid == true)
			{
				var qry2 = qry.Select(q => new
				{
					Item = q,
					IsAppointmentPaid = db.InvoicesV.Any(z => z.RowId == q.InvoiceRowId && z.PaymentRequest <= 0)
				});
				var list = qry2.ToArray();
				ret = list.Select(q =>
				{
					var item = mapper.Map<DTO.Appointment>(q.Item);
					item.IsAppointmentPaid = q.IsAppointmentPaid;
					return item;
				}).ToArray();
			}
			else
			{
				var list = qry.ToArray();
				ret = mapper.Map<DTO.Appointment[]>(list);
			}
			
			return ret;
		}

		public void UpdateAppointmentCore(List<DTO.Appointment> entities, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				//update
				var updateRows = new List<EF.AppointmentT>();
				foreach (var entity in entities)
				{
					var isDelete = (state == EntityState.Deleted);
					var options = AutoMapperHelper.CreateOptions();
					options.ExcludeCreatedUpdatedColumns = true;
					var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.AppointmentT), typeof(EF.AppointmentRemainder));

					if (!isDelete)
					{
						//var row = mapper.Map<EF.AppointmentT>(entity);

						var row = db.AppointmentsT.SingleOrDefault(q => q.RowId == entity.RowId);
						if (row == null)
						{
							row = mapper.Map<EF.AppointmentT>(entity);
							db.AppointmentsT.Add(row);
						}
						else
						{
							mapper.Map(entity, row);
						}

						if (entity.Patient?.IsNotRegistered == true)
						{
							EF.Patient findPatient = null;
							if (findPatient == null)
							{
								var mobileNumber = entity.Patient.MobileNumber?.Trim();
								if (!string.IsNullOrEmpty(mobileNumber))
								{
									findPatient = db.Patients.FirstOrDefault(q => q.IsNotRegistered && q.MobileNumber.Trim() == mobileNumber);
								}
							}
							if (findPatient == null)
							{
								var emailAddress = entity.Patient.EmailAddress?.Trim();
								if (!string.IsNullOrEmpty(emailAddress))
								{
									findPatient = db.Patients.FirstOrDefault(q => q.IsNotRegistered && q.EmailAddress.Trim() == emailAddress);
								}
							}

							if (findPatient == null)
							{
								var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.Patient));
								var patient = mapper2.Map<EF.Patient>(entity.Patient);
								patient.FamilyHeadRowId = patient.RowId;
								if (patient.FirstName == null) patient.FirstName = "";
								if (patient.LastName == null) patient.LastName = "";
								db.Patients.Add(patient);
								db.SaveChanges();
							}
							else
							{
								row.PatientRowId = findPatient.RowId;
							}

							AppointmentRegisteredNotify(entity);
						}
						

						//var entry = db.Entry(row);
						//entry.State = state;
						db.SaveChangesEx();
						updateRows.Add(row);

						if (entity.AppointmentRemainders != null)
						{
							var exRemainders = db.AppointmentRemainders.Where(q => q.AppointmentRowId == entity.RowId).ToArray();
							var rowRemainders = mapper.Map<EF.AppointmentRemainder[]>(entity.AppointmentRemainders);
							var newRemainders = rowRemainders.Select(q =>
							{
								var findRemainder = exRemainders.FirstOrDefault(z => z.RemainderInMinutes == q.RemainderInMinutes);
								if (findRemainder != null)
								{
									return findRemainder;
								}
								else
								{
									q.RowId = Guid.NewGuid();
									q.AppointmentRowId = row.RowId;
									return q;
								}
							}).ToArray();
							DbUpdateRowsHelper.UpdateList(exRemainders, newRemainders, q => q.RowId, db, this);
						}
					}


					if (isDelete)
					{
						try
						{
							db.AppointmentRemainders.Where(q => q.AppointmentRowId == entity.RowId).Delete();
							db.AppointmentsT.Where(q => q.RowId == entity.RowId).Delete();
						}
						catch (Exception ex)
						{
							if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
							{
								ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Appointment \"" + "" + "\" is used in database and cannot be deleted");
							}
							else throw ex;
						}
					}
				}

				//тест, что нету пересечения с другими Appointment
				{
					var listErrors = new List<EF.AppointmentV>();
					foreach (var row in updateRows)
					{
						if (row.ServiceProviderRowId != null)
						{
							var intersectionAppointments = db.AppointmentsV.Where(q =>
								q.ServiceProviderRowId == row.ServiceProviderRowId &&
								q.RowId != row.RowId &&
								(!(q.Start >= row.Finish || q.Finish <= row.Start)))
								.ToArray();
							listErrors.AddRange(intersectionAppointments);
						}
					}
					if (listErrors.Any())
					{
						var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.AppointmentV));
						var errObject = mapper2.Map<DTO.Appointment[]>(listErrors);
						ExceptionHelper.UserUpdateError(UserErrorCodes.AppointmentIntersection, "", errObject);
					}
				}


				//тест, что нету Vacation(Patient)
				{
					var listErrors = new List<EF.CalendarEventV>();
					foreach (var row in updateRows)
					{
						var errRows = 
							db.CalendarEventsV
							.Where(q => 
									q.PatientRowId == row.PatientRowId && 
									q.IsVacation && 
									((row.Start >= q.Start && row.Start < q.Finish) || (row.Finish >= q.Start && row.Finish < q.Finish)))
							.ToArray();
						listErrors.AddRange(errRows);
					}
					if (listErrors.Any())
					{
						var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.CalendarEventV));
						var errObject = mapper2.Map<DTO.CalendarEvent[]>(listErrors);
						ExceptionHelper.UserUpdateError(UserErrorCodes.AppointmentPatientVacation, "", errObject);
					}
				}

				//тест, что нету Vacation(ServiceProvider)
				{
					var listErrors = new List<EF.CalendarEventV>();
					foreach (var row in updateRows)
					{
						var errRows =
							db.CalendarEventsV
							.Where(q =>
									q.ServiceProviderRowId == row.ServiceProviderRowId &&
									q.IsVacation &&
									((row.Start >= q.Start && row.Start < q.Finish) || (row.Finish >= q.Start && row.Finish < q.Finish)))
							.ToArray();
						listErrors.AddRange(errRows);
					}
					if (listErrors.Any())
					{
						var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.CalendarEventV));
						var errObject = mapper2.Map<DTO.CalendarEvent[]>(listErrors);
						ExceptionHelper.UserUpdateError(UserErrorCodes.AppointmentServiceProviderVacation, "", errObject);
					}
				}

				//тест, что нету BusyEvent(Patient)
				{
					var listErrors = new List<EF.CalendarEventV>();
					foreach (var row in updateRows)
					{
						var errRows =
							db.CalendarEventsV
							.Where(q =>
									q.PatientRowId == row.PatientRowId &&
									q.IsBusyEvent &&
									((row.Start >= q.Start && row.Start < q.Finish) || (row.Finish >= q.Start && row.Finish < q.Finish)))
							.ToArray();
						listErrors.AddRange(errRows);
					}
					if (listErrors.Any())
					{
						var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.CalendarEventV));
						var errObject = mapper2.Map<DTO.CalendarEvent[]>(listErrors);
						ExceptionHelper.UserUpdateError(UserErrorCodes.AppointmentPatientBusyEvent, "", errObject);
					}
				}

				//тест, что нету BusyEvent(ServiceProvider)
				{
					var listErrors = new List<EF.CalendarEventV>();
					foreach (var row in updateRows)
					{
						var errRows =
							db.CalendarEventsV
							.Where(q =>
									q.ServiceProviderRowId == row.ServiceProviderRowId &&
									q.IsBusyEvent &&
									((row.Start >= q.Start && row.Start < q.Finish) || (row.Finish >= q.Start && row.Finish < q.Finish)))
							.ToArray();
						listErrors.AddRange(errRows);
					}
					if (listErrors.Any())
					{
						var mapper2 = AutoMapperHelper.GetPocoMapper(typeof(EF.CalendarEventV));
						var errObject = mapper2.Map<DTO.CalendarEvent[]>(listErrors);
						ExceptionHelper.UserUpdateError(UserErrorCodes.AppointmentServiceProviderBusyEvent, "", errObject);
					}
				}



				scope.Complete();
			}
		}

		void AppointmentRegisteredNotify(DTO.Appointment appointment)
		{
			var url = ConfigurationManager.AppSettings["base.url"];
			var business = ConfigurationManager.AppSettings["base.name"];
			var patient = appointment.Patient;

			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var service = db.MedicalServicesOrSupplies.First(p => p.RowId == appointment.MedicalServicesOrSupplyRowId);

			url = url + @"/#/client/appointment" + @"/" + appointment.RowId;

			var email = patient.EmailAddress;
			if (appointment.IsEmailWhenRegistered && !string.IsNullOrEmpty(email))
			{
				var subject = "Registration with " + business;
				var message = GetTemplateHtml("appointment");

				message = message.Replace("{{name}}", patient.FirstName);
				message = message.Replace("{{appointment}}", appointment.Start.ToString());
				message = message.Replace("{{service}}", service.Name);
				message = message.Replace("{{url}}", url);

				var err = EmailFunc.SendEmail(email, subject, message);
				if (!string.IsNullOrEmpty(err))
				{
					//Service.NLog.Error("send email=" + email + "\n" + err);
					throw new Exception("error when sending email");
				}
				else
				{
					//Service.NLog.Info("send email=" + email);
				}
			}

			var phone = patient.MobileNumber;
			if (appointment.IsSmsWhenRegistered && !string.IsNullOrEmpty(phone))
			{
				var html = "url=" + url;
				var err = SmsFunc.SendSms(phone, html);
				if (!string.IsNullOrEmpty(err))
				{
					//Service.NLog.Error("send email=" + email + "\n" + err);
					throw new Exception("error when sending email");
				}
				else
				{
					//Service.NLog.Info("send email=" + email);
				}
			}
		}
		public IEnumerable<DTO.AppointmentInsuranceProviderDayInfo> GetAppointmentInsuranceProviderDayInfo(DateTime dat, Guid serviceProviderRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var edat = dat.AddDays(1);

			var qry =
				from a in db.AppointmentsV
				join b in db.InsuranceCoverages on a.InsuranceCoverageRowId equals b.RowId
				where (a.Start >= dat && a.Start < edat) && (a.ServiceProviderRowId == serviceProviderRowId)
				group new {a,b} by new {b.InsuranceProviderRowId} into grp
				select new DTO.AppointmentInsuranceProviderDayInfo
				{
					Count					= grp.Count(),
					InsuranceProviderRowId	= grp.Key.InsuranceProviderRowId,
					InsuranceProviderCode	= db.InsuranceProviders.Where(q => q.RowId == grp.Key.InsuranceProviderRowId).Select(q => q.Code).FirstOrDefault(),
				};
			var rows = qry.ToArray();

			//var wh = ExpressionFunc.True<EF.AppointmentV>();
			//var list = db.AppointmentsV.Include(q => q.Patient).Where(wh.Expand()).ToArray();

			//var options = AutoMapperHelper.CreateOptions();
			//	.AddIncludeProp<DTO.Appointment>((q) => q.Patient);
			//var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(EF.AppointmentV), typeof(EF.Patient));
			//return mapper.Map<List<DTO.Appointment>>(list);
			return rows;
		}

		private string GetTemplateHtml(string templateId)
		{
			var path = ConfigurationManager.AppSettings["email.template.dir"];
			string file = Path.Combine(path, templateId + ".html");
			return File.ReadAllText(file);
		}
	}
}

