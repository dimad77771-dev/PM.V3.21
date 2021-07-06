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
using Profibiz.PracticeManager.Model;
using System.Web.Http;
using System.Net.Http;
using System.Net;
using LinqKit;
using System.Linq.Expressions;
using Profibiz.PracticeManager.SharedCode;
using System.Web.Http.Controllers;
using Newtonsoft.Json;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository : IWebApiRepository
    {
		public WebApiRepository()
		{
			var g = 10;
		}

		public Guid? CurrentUserRowId { get; set; }
		public void SetCurrentUserRowId(HttpControllerContext controllerContext)
		{
			var headers = controllerContext?.Request?.Headers;
			if (headers != null)
			{
				var pair = headers.FirstOrDefault(q => q.Key == "CurrentUserRowId");
				if (pair.Key != null)
				{
					var currentUserRowId = pair.Value.FirstOrDefault();
					if (!String.IsNullOrEmpty(currentUserRowId))
					{
						this.CurrentUserRowId = new Guid(currentUserRowId);
					}
				}
			}
		}

		public DTO.Patient GetPatient(Guid id, bool isShortForm, bool isAddressOnly)
		{
			if (isAddressOnly)
			{
				isShortForm = true;
			}

			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var patient = db.Patients.FirstOrDefault(p => p.RowId == id);

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Patient), typeof(DTO.InsuranceCoverage), typeof(DTO.PatientNote), 
															typeof(DTO.PatientDocument), typeof(EF.AppointmentV), typeof(EF.FormDocument));
			var ret = mapper.Map<DTO.Patient>(patient);

			if (!isShortForm)
			{
				ret.FamilyHead = mapper.Map<DTO.Patient>(patient.FamilyHead);
				ret.FamilyMembers = mapper.Map<List<DTO.Patient>>(patient.FamilyHead.FamilyMembers.Where(q => q.RowId != ret.RowId));

				//для него
				var insuranceCoveragesRowIds1 =
					db.InsuranceCoverageHolders
					.Where(q => q.PolicyHolderRowId == id)
					.Select(q => q.InsuranceCoverageRowId)
					.Distinct()
					.ToArray();
				//для всех членов семьи
				var insuranceCoveragesRowIds2 = 
					db.InsuranceCoverageHolders
					.Where(q => db.PatientAllFamilyMemberViews.Where(z => z.PatientRowId == id).Select(z => z.FamilyMemberPatientRowId).Contains(q.PolicyHolderRowId))
					.Select(q => q.InsuranceCoverageRowId)
					.Distinct()
					.ToArray();
				var insuranceCoveragesRowIds = insuranceCoveragesRowIds1.Union(insuranceCoveragesRowIds2);


				var insuranceCoveragesQry = db.InsuranceCoverages.Where(q => insuranceCoveragesRowIds.Contains(q.RowId));

				var insuranceCoverages = mapper.Map<List<DTO.InsuranceCoverage>>(insuranceCoveragesQry);
				foreach (var insuranceCoverage in insuranceCoveragesQry)
				{
					var policyOwner = insuranceCoverage.GetPolicyOwner();
					if (policyOwner != null)
					{
						insuranceCoverages.Single(q => q.RowId == insuranceCoverage.RowId).PolicyOwner = mapper.Map<DTO.Patient>(policyOwner);
					}
				}
				insuranceCoverages.ForEach(q => q.IsOnlyForOtherFamilyMember = !insuranceCoveragesRowIds1.Contains(q.RowId));
				ret.InsuranceCoverages = insuranceCoverages;


				var patientNotesQry = db.PatientNotes.Where(q => q.PatientRowId == id);
				ret.PatientNotes = mapper.Map<List<DTO.PatientNote>>(patientNotesQry);

				var patientDocuments = db.PatientDocuments.Where(q => q.PatientRowId == id).ToArray();
				patientDocuments.ForEach(q => q.BinaryDocument = null);
				ret.PatientDocuments = mapper.Map<List<DTO.PatientDocument>>(patientDocuments);


				var appointmentWithClinicalNotes = 
					db.AppointmentsV
					.Where(q => q.PatientRowId == id)	// && q.AppointmentClinicalNoteRowId != null)
					.OrderByDescending(q => q.Start);
				ret.AppointmentWithClinicalNotes = mapper.Map<List<DTO.Appointment>>(appointmentWithClinicalNotes);
				var appointmentRowIds = appointmentWithClinicalNotes.Select(z => (Guid?)z.RowId).ToArray();

				var formDocumentsAll = db.FormDocuments
					.Where(q => appointmentRowIds.Contains(q.AppointmentRowId))
					.ToArray()
					.Select(q => mapper.Map<DTO.FormDocument>(q))
					.ToArray();
				foreach(var appointmentWithClinicalNote in ret.AppointmentWithClinicalNotes)
				{
					appointmentWithClinicalNote.FormDocuments = formDocumentsAll.Where(q => q.AppointmentRowId == appointmentWithClinicalNote.RowId).ToArray();
				}

				var patientFormDocuments = db.FormDocuments
					.Where(q => q.PatientRowId == id)
					.ToArray()
					.Select(q => mapper.Map<DTO.FormDocument>(q))
					.ToArray();
				ret.PatientFormDocuments = new DTO.Appointment { FormDocuments = patientFormDocuments };

				var appointmentWithTreatmentNotes =
					db.AppointmentsV
					//.Where(q => q.PatientRowId == id && q.AppointmentTreatmentNoteRowId != null)
					.Where(q => q.PatientRowId == id)
					.OrderByDescending(q => q.Start);
				ret.AppointmentWithTreatmentNotes = mapper.Map<List<DTO.Appointment>>(appointmentWithTreatmentNotes);
			}

			return ret;
		}


		public byte[] GetPatientPhoto(Guid id)
		{
			//throw new Exception("11");
			var _db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			//var _db = db;
			//_db.Database.Connection.
			var photo = _db.PatientPhotos.FirstOrDefault(q => q.PatientRowId == id)?.Photo;
			return photo;
		}
		


		public IEnumerable<DTO.PatientsListView> GetPatientsList(Guid? insuranceProviderRowId, Guid? insuranceProvidersViewGroupRowId, bool hasNoInsuranceProvider, bool includeAllFamilyMember, string restrictPatientList)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var wh = ExpressionFunc.True<EF.PatientsListView>();
			if (insuranceProviderRowId != null)
			{
				wh = PredicateBuilder.And(wh, q => db.PatientInsuranceCoverageViews.Any(z => z.PatientRowId == q.RowId && z.InsuranceProviderRowId == insuranceProviderRowId));
			}
			if (insuranceProvidersViewGroupRowId != null)
			{
				var qryPatient0 = db.GetPatientRowIdByInsuranceProvidersViewGroupRowId(insuranceProvidersViewGroupRowId);
				wh = PredicateBuilder.And(wh, q => qryPatient0.Contains(q.RowId));
			}
			if (hasNoInsuranceProvider)
			{
				wh = PredicateBuilder.And(wh, q => !db.PatientInsuranceCoverageViews.Any(z => z.PatientRowId == q.RowId));
			}

			Expression<Func<EF.PatientsListView, bool>> wh2;
			if (!includeAllFamilyMember)
			{
				wh2 = wh;
			}
			else
			{
				var patientsQry = db.PatientsListView.Where(wh.Expand()).Select(q => q.RowId);
				wh2 = (q) => db.PatientFamilyMemberViews.Any(z => z.FamilyMemberPatientRowId == q.RowId && patientsQry.Contains(z.PatientRowId));
			}

			//wh2 = PredicateBuilder.And(wh2, q => !q.IsNotRegistered);

			if (!string.IsNullOrEmpty(restrictPatientList))
			{
				var serviceProviderRowId = new Guid(restrictPatientList);
				wh2 = PredicateBuilder.And(wh2, q => db.AppointmentsT.Any(z => z.PatientRowId == q.RowId && z.ServiceProviderRowId == serviceProviderRowId));
			}

			var list = db.PatientsListView.Where(wh2.Expand()).ToArray();
			var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.PatientsListView));
			var ret = mapper.Map<List<DTO.PatientsListView>>(list);

			return ret;
        }

		public Guid[] PatientRowId2InsuranceProviders(Guid patientRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var insuranceProviderRowIds = 
				db.InsuranceCoverageHolders
				.Where(q => q.PolicyHolderRowId == patientRowId)
				.Select(q => q.InsuranceCoverage)
				.Select(q => q.InsuranceProviderRowId)
				.Distinct()
				.ToArray();

			return insuranceProviderRowIds;
		}

		private Guid[] PatientRowId2InsuranceCoverageRowIds(Guid patientRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var insuranceCoverageRowIds =
				db.InsuranceCoverageHolders
				.Where(q => q.PolicyHolderRowId == patientRowId)
				.Select(q => q.InsuranceCoverage.RowId)
				.Distinct()
				.ToArray();

			return insuranceCoverageRowIds;
		}

		public List<DTO.InsuranceCoverage> PatientRowId2InsuranceCoverages(Guid patientRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);

			var list =
				db.InsuranceCoverageHolders
				.Where(q => q.PolicyHolderRowId == patientRowId)
				.Select(q => q.InsuranceCoverageV)
				.Distinct()
				.ToArray();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.InsuranceCoverageV));
			var rez = mapper.Map<List<DTO.InsuranceCoverage>>(list);
			return rez;
		}



		public void UpdatePatientCore(DTO.Patient entity, EntityState state)
		{
			if (entity.ChangeFamilyMember != null)
			{
				UpdateFamilyMember(entity);
			}
			else
			{
				UpdatePatientAll(entity, state);
			}
		}


		private void UpdatePatientAll(DTO.Patient entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);

				if (!isDelete)
				{
					var mapper = AutoMapperHelper.GetPocoMapper(typeof(DTO.Patient));
					var epatient = mapper.Map<EF.Patient>(entity);

					if (!entity.IgnoreDuplicateLastFirstNameFlag)
					{
						if (db.Patients.Any(q => q.LastName == entity.LastName && q.FirstName == entity.FirstName && q.RowId != epatient.RowId))
						{
							ExceptionHelper.UserUpdateError(UserErrorCodes.PatientNameDuplicate);
						}
					}

					var en = db.Entry(epatient);
					en.State = state;
					db.SaveChangesEx();

					if (entity.ChangeFamilyMembersAddress)
					{
						foreach (var familyMember in entity.FamilyMembers)
						{
							var efamilyMember = mapper.Map<EF.Patient>(familyMember);
							var en2 = db.Entry(efamilyMember);
							en2.State = EntityState.Modified;
							db.SaveChangesEx();
						}
					}
				}
				else
				{
					var row = db.Patients.Single(q => q.RowId == entity.RowId);
					db.Patients.Remove(row);
					try
					{
						db.SaveChangesEx();
					}
					catch (Exception ex)
					{
						if (ExceptionHelper.IsDeleteReferenceConstraintException(ex))
						{
							ExceptionHelper.UserUpdateError(UserErrorCodes.DeleteForeignKey, "Patient \"" + row.LastName + ", " + row.FirstName + "\" is used in database and cannot be deleted");
						}
						else throw ex;
					}
				}

				if (!String.IsNullOrEmpty(entity.City1) && !String.IsNullOrEmpty(entity.Province1))
				{
					if (!db.Cities.Any(p => p.CityName == entity.City1 && p.Province == entity.Province1))
					{
						db.Cities.Add(new EF.City { RowId = Guid.NewGuid(), CityName = entity.City1, Province = entity.Province1 });
						db.SaveChangesEx();
					}
				}

                scope.Complete();
			}
		}

		
		private void UpdateFamilyMember(DTO.Patient entity)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
                if (!db.Cities.Any(p => p.CityName == entity.City1 && p.Province == entity.Province1))
                {
                    db.Cities.Add(new EF.City { RowId = Guid.NewGuid(), CityName = entity.City1, Province = entity.Province1 });
                    db.SaveChangesEx();
                }

                if (entity.ChangeFamilyMember.Action == DTO.Patient.ChangeFamilyMemberInfo.ActionEnum.RemoveFromFamily)
				{
					var row = db.Patients.Single(q => q.RowId == entity.RowId);
					row.FamilyHeadRowId = entity.RowId;
					row.FamilyMemberType = TypeHelper.FamilyMemberType.Head;
					db.SaveChangesEx();
				}
				else if (entity.ChangeFamilyMember.Action == DTO.Patient.ChangeFamilyMemberInfo.ActionEnum.MoveMemberToHeader)
				{
					var row = db.Patients.Single(q => q.RowId == entity.RowId);
					var oldFamilyHeadRowId = row.FamilyHeadRowId;
					var newFamilyHeadRowId = entity.RowId;
					row.FamilyHeadRowId = newFamilyHeadRowId;
					row.FamilyMemberType = TypeHelper.FamilyMemberType.Head;
					db.SaveChangesEx();

					foreach (var otherMember in db.Patients.Where(q => q.FamilyHeadRowId == oldFamilyHeadRowId && q.RowId != entity.RowId))
					{
						otherMember.FamilyMemberType = TypeHelper.FamilyMemberType.Member;
						otherMember.FamilyHeadRowId = newFamilyHeadRowId;
					}
					db.SaveChangesEx();
				}
				else if (entity.ChangeFamilyMember.Action == DTO.Patient.ChangeFamilyMemberInfo.ActionEnum.MoveToAnotherFamily)
				{
					var row = db.Patients.Single(q => q.RowId == entity.RowId);
					row.FamilyHeadRowId = entity.ChangeFamilyMember.NewFamilyHeadRowId;
					row.FamilyMemberType = TypeHelper.FamilyMemberType.Member;
					db.SaveChangesEx();
				}

                else throw new NotSupportedException();

                scope.Complete();
			}
		}







		public void PatientBuildFamily(List<DTO.Patient> nrows)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var rowIds = nrows.Select(z => z.RowId).ToList();
				var rows = db.Patients.Where(q => rowIds.Contains(q.RowId)).ToList();
				if (rows.Count != nrows.Count) ExceptionHelper.InternalError();

				//validate
				foreach (var row in rows)
				{
					if (row.RowId != row.FamilyHeadRowId)
					{
						ExceptionHelper.InternalError("PatientBuildFamily. Error 1");
					}
					else if (row.FamilyMemberType != TypeHelper.FamilyMemberType.Head)
					{
						ExceptionHelper.InternalError("PatientBuildFamily. Error 2");
					}
					else if (db.Patients.Any(q => q.RowId != row.RowId && q.FamilyHeadRowId == row.RowId))
					{
						ExceptionHelper.InternalError("PatientBuildFamily. Error 3");
					}
				}

				//rowHead + rowsOther
				var familyHeadRowId = nrows.Single(q => q.IsSelectHeadFamily).RowId;
				var rowHead = rows.Single(q => q.RowId == familyHeadRowId);
				var rowsOther = rows.Where(q => q.RowId != familyHeadRowId).ToList();

				//update
				rowHead.FamilyHeadRowId = rowHead.RowId;
				db.SaveChangesEx();

				foreach (var rowOther in rowsOther)
				{
					rowOther.FamilyHeadRowId = rowHead.RowId;
					rowOther.FamilyMemberType = TypeHelper.FamilyMemberType.Member;

					if (nrows.Single(q => q.RowId == rowOther.RowId).IsSelectUseHeadAddress)
					{
						rowOther.UseHeadAddress = true;
						rowOther.Province1 = rowHead.Province1;
						rowOther.Address1 = rowHead.Address1;
						rowOther.City1 = rowHead.City1;
						rowOther.Postcode1 = rowHead.Postcode1;
						rowOther.Province2 = rowHead.Province2;
						rowOther.Address2 = rowHead.Address2;
						rowOther.City2 = rowHead.City2;
						rowOther.Postcode2 = rowHead.Postcode2;
					}

					db.SaveChangesEx();
				}

				scope.Complete();
			}
		}



		public void PostPatientsFromBodyrevivalsalonspa(string json)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var customers = JsonConvert.DeserializeObject<DTO.spaCustomer[]>(json);
				foreach(var customer in customers)
				{
					var birthDate = default(DateTime?);
					if (customer.BirthDate_Year != null && customer.BirthDate_Month != null && customer.BirthDate_Day != null)
					{
						birthDate = new DateTime(customer.BirthDate_Year.Value, customer.BirthDate_Month.Value, customer.BirthDate_Day.Value);
						if (birthDate <= new DateTime(1901,1,1))
						{
							birthDate = null;
						}
					}

					var patientRowId = Guid.NewGuid();
					var patient = new EF.Patient
					{
						RowId = patientRowId,
						FamilyHeadRowId = patientRowId,
						FirstName = customer.FirstName,
						LastName = customer.LastName,
						MiddleName = customer.MiddleName,
						BirthDate = birthDate,
						Province1 = customer.State?.Trim(),
						City1 = customer.City,
						Postcode1 = customer.Zip,
						Address1 = customer.Address,
						HomePhoneNumber = customer.Phone1,
						MobileNumber = customer.Phone2,
						WorkPhone = customer.Phone3,
						EmailAddress = customer.EMail,
						spaCustomerNumber = customer.CustomerNumber,
						FamilyMemberType = "Head",
						Rate = 0,
					};

					if (!string.IsNullOrEmpty(patient.Province1) && !new[] { "AB", "BC", "MB", "NB", "NL", "NT", "NS", "NU", "ON", "PE", "QC", "SK", "YT" }.Contains(patient.Province1))
					{
						patient.Province1 = null;
					}

					db.Patients.Add(patient);
					db.SaveChangesEx();
				}
				scope.Complete();
			}
		}


	}
}
