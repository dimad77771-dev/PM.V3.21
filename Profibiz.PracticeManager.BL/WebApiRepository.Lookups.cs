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
using System.Data.SqlClient;
using Profibiz.PracticeManager.SharedCode;
using MimeKit;
using System.Configuration;
using MailKit.Net.Smtp;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public DTO.UserSetting GetUserSettings(string userCode)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var row = db.UserSettings.SingleOrDefault(q => q.UserCode == userCode);
			if (row == null)
			{
				row = new EF.UserSetting();
			}
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.UserSetting));
			var userSetting = mapper.Map<DTO.UserSetting>(row);

			var settings = db.Settings.Where(q => q.Name == "BusinessName").FirstOrDefault();
			if (settings != null)
			{
				userSetting.BusinessName = settings.Value;
			}

			return userSetting;
		}

        public IEnumerable<DTO.Category> GetCategories()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Categories;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Category));
			return mapper.Map<List<DTO.Category>>(list);
		}

		public IEnumerable<string> GetOntarioCities()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Cities.Where(p => p.Province == "ON").Select(q => q.CityName).ToList();
			return list;
		}

		public IEnumerable<DTO.AppointmentStatus> GetAppointmentStatuses()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.AppointmentStatuses;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.AppointmentStatus));
			return mapper.Map<List<DTO.AppointmentStatus>>(list);
		}

		public IEnumerable<DTO.PatientNoteStatus> GetPatientNoteStatuses()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.PatientNoteStatuses;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.PatientNoteStatus));
			return mapper.Map<List<DTO.PatientNoteStatus>>(list);
		}

		public IEnumerable<DTO.CalendarEventStatus> GetCalendarEventStatuses()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.CalendarEventStatuses;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.CalendarEventStatus));
			return mapper.Map<List<DTO.CalendarEventStatus>>(list);
		}

		public IEnumerable<DTO.PublicHoliday> GetPublicHolidays()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.PublicHolidays;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.PublicHoliday));
			return mapper.Map<List<DTO.PublicHoliday>>(list);
		}

		public IEnumerable<DTO.InvoiceStatus> GetInvoiceStatuses()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.InvoiceStatuses;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InvoiceStatus));
			return mapper.Map<List<DTO.InvoiceStatus>>(list);
		}

		public IEnumerable<DTO.ChargeoutStatus> GetChargeoutStatuses()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.ChargeoutStatuses;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ChargeoutStatus));
			return mapper.Map<List<DTO.ChargeoutStatus>>(list);
		}

		public IEnumerable<DTO.ChargeoutRecipient> GetChargeoutRecipientes()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.ChargeoutRecipients;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ChargeoutRecipient));
			return mapper.Map<List<DTO.ChargeoutRecipient>>(list);
		}

		public IEnumerable<DTO.Template> GetTemplates()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Templates.Select(q => new DTO.Template
			{
				RowId = q.RowId,
				Code = q.Code,
				Name = q.Name,
				InvoiceType = q.InvoiceType,
				IsDefault = q.IsDefault,
				IsEnabled = q.IsEnabled,
				IsHidden = q.IsHidden,
				TemplateType = q.TemplateType,
				FormType = q.FormType,
				CategoryRowId = q.CategoryRowId,
				Comments = q.Comments,
				HasDocumentBytes = (q.DocumentBytes != null),
			}).ToArray();
			return list;
		}


		


		public IEnumerable<DTO.InsuranceProvider> GetInsuranceProviders()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.InsuranceProviders;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InsuranceProvider));
			return mapper.Map<List<DTO.InsuranceProvider>>(list);
		}

		public IEnumerable<DTO.MedicalServicesOrSupply> GetMedicalServicesOrSupplies()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.MedicalServicesOrSupplies;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.MedicalServicesOrSupply));
			return mapper.Map<List<DTO.MedicalServicesOrSupply>>(list);
		}

		public IEnumerable<DTO.ProfessionalAssociation> GetProfessionalAssociations()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.ProfessionalAssociations;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ProfessionalAssociation));
			return mapper.Map<List<DTO.ProfessionalAssociation>>(list);
		}

		public IEnumerable<DTO.Setting> GetSettings()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Settings;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Setting));
			return mapper.Map<List<DTO.Setting>>(list);
		}

		public IEnumerable<DTO.ThirdPartyServiceProvider> GetThirdPartyServiceProviders()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.ThirdPartyServiceProviders;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ThirdPartyServiceProvider));
			return mapper.Map<List<DTO.ThirdPartyServiceProvider>>(list);
		}

		public IEnumerable<DTO.Referrer> GetReferrers()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Referrers;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Referrer));
			return mapper.Map<List<DTO.Referrer>>(list);
		}

		public IEnumerable<DTO.User> GetUsers()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Users;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.User));
			return mapper.Map<List<DTO.User>>(list);
		}

		public IEnumerable<DTO.Supplier> GetSuppliers()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var list = db.Suppliers;
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Supplier));
			return mapper.Map<List<DTO.Supplier>>(list);
		}

		public IEnumerable<DTO.AppointmentBook> GetAppointmentBooks()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var list = db.AppointmentBooks.ToArray();

			var options = AutoMapperHelper.CreateOptions()
				.AddIncludeProp<DTO.AppointmentBook>((q) => q.ServiceProviders);
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, typeof(DTO.AppointmentBook), typeof(DTO.ServiceProvider));
			return mapper.Map<List<DTO.AppointmentBook>>(list.OrderBy(q => q.Name));
		}

		public void PutMedicalServicesOrSupplies(IEnumerable<DTO.MedicalServicesOrSupply> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.MedicalServicesOrSupply));

				var oldRows = db.MedicalServicesOrSupplies.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.MedicalServicesOrSupply>(entity);
						db.MedicalServicesOrSupplies.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteMedicalServicesOrSupply(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.MedicalServicesOrSupplies.Single(q => q.RowId == id);
				db.MedicalServicesOrSupplies.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutCategories(IEnumerable<DTO.Category> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Category));

				var oldRows = db.Categories.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.Category>(entity);
						db.Categories.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteCategory(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Categories.Single(q => q.RowId == id);
				db.Categories.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}



		public void PutInsuranceProviders(IEnumerable<DTO.InsuranceProvider> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InsuranceProvider));

				var oldRows = db.InsuranceProviders.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.InsuranceProvider>(entity);
						db.InsuranceProviders.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteInsuranceProvider(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.InsuranceProviders.Single(q => q.RowId == id);
				db.InsuranceProviders.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.CompanyName + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}


		public void PutProfessionalAssociations(IEnumerable<DTO.ProfessionalAssociation> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ProfessionalAssociation));

				var oldRows = db.ProfessionalAssociations.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.ProfessionalAssociation>(entity);
						db.ProfessionalAssociations.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteProfessionalAssociation(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.ProfessionalAssociations.Single(q => q.RowId == id);
				db.ProfessionalAssociations.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}


		public void PutSettings(IEnumerable<DTO.Setting> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Setting));

				var oldRows = db.Settings.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.Setting>(entity);
						db.Settings.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteSetting(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Settings.Single(q => q.RowId == id);
				db.Settings.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}


		public void PutThirdPartyServiceProviders(IEnumerable<DTO.ThirdPartyServiceProvider> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ThirdPartyServiceProvider));

				var oldRows = db.ThirdPartyServiceProviders.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.ThirdPartyServiceProvider>(entity);
						db.ThirdPartyServiceProviders.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteThirdPartyServiceProvider(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.ThirdPartyServiceProviders.Single(q => q.RowId == id);
				db.ThirdPartyServiceProviders.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutReferrers(IEnumerable<DTO.Referrer> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Referrer));

				var oldRows = db.Referrers.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.Referrer>(entity);
						db.Referrers.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteReferrer(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Referrers.Single(q => q.RowId == id);
				db.Referrers.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutUsers(IEnumerable<DTO.User> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.User));

				var oldRows = db.Users.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.User>(entity);
						db.Users.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteUser(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Users.Single(q => q.RowId == id);
				db.Users.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutSuppliers(IEnumerable<DTO.Supplier> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Supplier));

				var oldRows = db.Suppliers.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.Supplier>(entity);
						db.Suppliers.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteSupplier(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Suppliers.Single(q => q.RowId == id);
				db.Suppliers.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutAppointmentBooks(IEnumerable<DTO.AppointmentBook> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.AppointmentBook));

				var oldRows = db.AppointmentBooks.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.AppointmentBook>(entity);
						db.AppointmentBooks.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteAppointmentBook(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.AppointmentBooks.Single(q => q.RowId == id);
				db.AppointmentBooks.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutAppointmentStatuses(IEnumerable<DTO.AppointmentStatus> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.AppointmentStatus));

				var oldRows = db.AppointmentStatuses.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.AppointmentStatus>(entity);
						db.AppointmentStatuses.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteAppointmentStatus(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.AppointmentStatuses.Single(q => q.RowId == id);
				db.AppointmentStatuses.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}


		public void PutPatientNoteStatuses(IEnumerable<DTO.PatientNoteStatus> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.PatientNoteStatus));

				var oldRows = db.PatientNoteStatuses.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.PatientNoteStatus>(entity);
						db.PatientNoteStatuses.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeletePatientNoteStatus(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.PatientNoteStatuses.Single(q => q.RowId == id);
				db.PatientNoteStatuses.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutCalendarEventStatuses(IEnumerable<DTO.CalendarEventStatus> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.CalendarEventStatus));

				var oldRows = db.CalendarEventStatuses.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.CalendarEventStatus>(entity);
						db.CalendarEventStatuses.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteCalendarEventStatus(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.CalendarEventStatuses.Single(q => q.RowId == id);
				db.CalendarEventStatuses.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}


		public void PutPublicHolidays(IEnumerable<DTO.PublicHoliday> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.PublicHoliday));

				var oldRows = db.PublicHolidays.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.PublicHoliday>(entity);
						db.PublicHolidays.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeletePublicHoliday(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.PublicHolidays.Single(q => q.RowId == id);
				db.PublicHolidays.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutTemplates(IEnumerable<DTO.Template> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Template));

				var oldRows = db.Templates.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.Template>(entity);
						db.Templates.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteTemplate(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Templates.Single(q => q.RowId == id);
				db.Templates.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutTemplateDocumentBytes(DTO.TemplateDocumentBytes erow)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.Templates.SingleOrDefault(q => q.RowId == erow.RowId);
				if (row != null)
				{
					row.DocumentBytes = erow.DocumentBytes;
				}

				db.SaveChangesEx();
				scope.Complete();
			}
		}

		public void PutInvoiceStatuses(IEnumerable<DTO.InvoiceStatus> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InvoiceStatus));

				var oldRows = db.InvoiceStatuses.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.InvoiceStatus>(entity);
						db.InvoiceStatuses.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteInvoiceStatus(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.InvoiceStatuses.Single(q => q.RowId == id);
				db.InvoiceStatuses.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutChargeoutStatuses(IEnumerable<DTO.ChargeoutStatus> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ChargeoutStatus));

				var oldRows = db.ChargeoutStatuses.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.ChargeoutStatus>(entity);
						db.ChargeoutStatuses.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteChargeoutStatus(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.ChargeoutStatuses.Single(q => q.RowId == id);
				db.ChargeoutStatuses.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public void PutChargeoutRecipientes(IEnumerable<DTO.ChargeoutRecipient> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ChargeoutRecipient));

				var oldRows = db.ChargeoutRecipients.ToArray();

				foreach (var entity in entities)
				{
					var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
					if (oldRow == null)
					{
						var newRow = mapper.Map<EF.ChargeoutRecipient>(entity);
						db.ChargeoutRecipients.Add(newRow);
						db.SaveChangesEx();
					}
					else
					{
						mapper.Map(entity, oldRow);
						db.SaveChangesEx();
					}
				}

				scope.Complete();
			}
		}
		public void DeleteChargeoutRecipient(Guid id)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.ChargeoutRecipients.Single(q => q.RowId == id);
				db.ChargeoutRecipients.Remove(row);
				try
				{
					db.SaveChangesEx();
				}
				catch (Exception ex)
				{
					if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
					{
						ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Row \"" + row.Name + "\" is used in database and cannot be deleted");
					}
					else throw ex;
				}

				scope.Complete();
			}
		}

		public IEnumerable<DTO.InsuranceProvidersViewGroup> GetInsuranceProvidersViewGroups()
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InsuranceProvidersViewGroup), typeof(DTO.InsuranceProvidersViewGroupMapping));

			foreach (var row in db.InsuranceProvidersViewGroups)
			{
				var ret = mapper.Map<DTO.InsuranceProvidersViewGroup>(row);
				ret.InsuranceProvidersViewGroupMappings = mapper.Map<List<DTO.InsuranceProvidersViewGroupMapping>>(row.InsuranceProvidersViewGroupMappings);
				yield return ret;
			}
		}


		public void PutInsuranceProvidersViewGroups(IEnumerable<DTO.InsuranceProvidersViewGroup> entities)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.InsuranceProvidersViewGroup), typeof(DTO.InsuranceProvidersViewGroupMapping));

				db.InsuranceProvidersViewGroupMappings.Delete();
				db.InsuranceProvidersViewGroups.Delete();

				var newInsuranceProvidersViewGroups = mapper.Map<List<EF.InsuranceProvidersViewGroup>>(entities);
				var newInsuranceProvidersViewGroupMappings = mapper.Map<List<EF.InsuranceProvidersViewGroupMapping>>(entities.SelectMany(q => q.InsuranceProvidersViewGroupMappings));
				db.InsuranceProvidersViewGroups.AddRange(newInsuranceProvidersViewGroups);
				db.InsuranceProvidersViewGroupMappings.AddRange(newInsuranceProvidersViewGroupMappings);
				db.SaveChangesEx();


				//var oldRows = db.InsuranceProviders.ToArray();

				//foreach (var entity in entities)
				//{
				//	var oldRow = oldRows.SingleOrDefault(q => q.RowId == entity.RowId);
				//	if (oldRow == null)
				//	{
				//		var newRow = mapper.Map<EF.InsuranceProvider>(entity);
				//		db.InsuranceProviders.Add(newRow);
				//		db.SaveChanges();
				//	}
				//	else
				//	{
				//		mapper.Map(entity, oldRow);
				//		db.SaveChanges();
				//	}
				//}

				scope.Complete();
			}
		}

		public void PostErrorToServer(DTO.ClientError errorInfo)
		{
			//db
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.ClientError));
				var row = mapper.Map<EF.ClientError>(errorInfo);
				row.ServerDateTime = DateTimeHelper.Now;

				db.ClientErrors.Add(row);
				db.SaveChangesEx();

				scope.Complete();
			}

			//e-mail
			SendErrorToEmail(errorInfo);
		}

		public void SendErrorToEmail(DTO.ClientError errorInfo)
		{
			if (ConfigurationManager.AppSettings["error.email.send"] != "true")
			{
				return;
			}

			try
			{
				var message = new MimeMessage();
				var fromName = ConfigurationManager.AppSettings["smtp.from.name"];
				var fromAddress = ConfigurationManager.AppSettings["smtp.from.address"];
				message.From.Add(new MailboxAddress(fromName, fromAddress));
				message.Subject = ConfigurationManager.AppSettings["error.email.subject"] ?? "";
				var toName = ConfigurationManager.AppSettings["error.email.to.name"] ?? "";
				var toAddress = ConfigurationManager.AppSettings["error.email.to.address"] ?? "";
				message.To.Add(new MailboxAddress(toName, toAddress));

				var text =
					"DateTime: " + errorInfo.ErrorDateTime + "\n" +
					"Error: " + errorInfo.ErrorText + "\n" +
					"MachineName: " + errorInfo.MachineName + "\n" +
					"OSVersion: " + errorInfo.OSVersion + "\n" +
					"UserName: " + errorInfo.UserName + "\n" +
					"UserDomainName: " + errorInfo.UserDomainName + "\n" +
					"";

				var builder = new BodyBuilder();
				builder.TextBody = text;
				message.Body = builder.ToMessageBody();


				var url = ConfigurationManager.AppSettings["smtp.url"];
				var port = Int32.Parse(ConfigurationManager.AppSettings["smtp.port"]);
				var username = ConfigurationManager.AppSettings["smtp.username"];
				var password = ConfigurationManager.AppSettings["smtp.password"];

				var client = new SmtpClient();
				client.Connect(url, port);
				//client.AuthenticationMechanisms.Remove("XOAUTH2");
				client.Authenticate(username, password);

				client.Send(message);
			}
			catch (Exception ex)
			{
				throw new AggregateException(ex);
			}
		}

		public void PostNLogItem(DTO.NLogItem nlogItem)
		{
			//System.Threading.Thread.Sleep(10000);

			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var row = new EF.NLogItem
			{
				RowId = Guid.NewGuid(),
				ActivityId = nlogItem.activityid,
				Date = nlogItem.date,
				Level = nlogItem.level,
				Logger = nlogItem.logger,
				MachineName = nlogItem.machinename,
				Message = nlogItem.message,
			};
			db.NLogItems.Add(row);
			db.SaveChangesEx();

			//var cmd = new SqlCommand();
			//cmd.Connection = db.Database;

			//var dbc = db.Database;
			//dbc.ExecuteSqlCommand(
			//	"insert into NLogItems(RowId, ActivityId, MachineName, Date, Level, Logger, Message) values(@RowId, @ActivityId, @MachineName, @Date, @Level, @Logger, @Message)",
			//	Guid.NewGuid(), nlogItem.activityid, nlogItem.machinename, nlogItem.date, nlogItem.level, nlogItem.logger, nlogItem.message
			//	);
		}

		public void PostUserSettings(DTO.UserSetting userSetting)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var row = db.UserSettings.SingleOrDefault(q => q.UserCode == userSetting.UserCode);
				if (row != null)
				{
					row.Json = userSetting.Json;
				}
				else
				{
					row = new EF.UserSetting
					{
						RowId = Guid.NewGuid(),
						UserCode = userSetting.UserCode,
						Json = userSetting.Json,
					};
					db.UserSettings.Add(row);
				}
				db.SaveChangesEx();
				scope.Complete();
			}
		}

		public DTO.LoginInfo GetLoginInfo(string name, string password)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			name = (name ?? "").ToLower().Trim();

			var user = db.ServiceProviders.FirstOrDefault(q => q.Username.ToLower().Trim() == name && q.Password == password);
			var userRowId = user?.RowId;

			if (userRowId != null)
			{
				var role = db.Users.SingleOrDefault(q => q.RowId == user.RoleRowId);
				if (role == null)
				{
					role = new EF.User();
				}
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.User));
				var drole = mapper.Map<DTO.User>(role);

				db.LoginInouts.Add(new EF.LoginInout
				{
					RowId = Guid.NewGuid(),
					Start = DateTime.Now,
					ServiceProviderRowId = userRowId.Value,
				});
				db.SaveChanges();

				return new DTO.LoginInfo
				{
					IsSuccess = true,
					UserRowId = userRowId.Value,
					Role = drole,
				};
			}
			else
			{
				return new DTO.LoginInfo
				{
					IsSuccess = false,
					Error = "Invalid username or password",
				};
			}
		}

		public DTO.TemplateDocumentBytes GetTemplateDocumentBytes(Guid rowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var row = db.Templates.SingleOrDefault(q => q.RowId == rowId);
			var result = new DTO.TemplateDocumentBytes
			{
				RowId = row.RowId,
				DocumentBytes = row.DocumentBytes,
			};
			return result;
		}

	}
}
