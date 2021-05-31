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
		public IEnumerable<DTO.FormDocmodel> GetFormDocmodelList(int? formDictionary, Guid? patientRowId, Guid? formRowId)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			var model = new DTO.FormDocmodel();

			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options
				, typeof(EF.Form), typeof(EF.FormItem)
				, typeof(EF.AppointmentForm), typeof(EF.AppointmentFormItem)
				, typeof(EF.PatientForm), typeof(EF.PatientFormItem));

			if (formDictionary == 1)
			{
				var result = db.Forms
					.Include(q => q.FormItems)
					.ToArray()
					.Select(q =>
					{
						var form = mapper.Map<DTO.Form>(q);
						return new DTO.FormDocmodel
						{
							Form = form,
							FormItems = q.FormItems.Select(z => mapper.Map<DTO.FormItem>(z)).ToArray(),
						};
					})
					.ToArray();
				return result;
			}
			else if (patientRowId != null)
			{
				var result = db.PatientForms
					.Include(q => q.PatientFormItems)
					.Where(q => q.PatientRowId == patientRowId && (q.FormRowId == formRowId || formRowId == null))
					.ToArray()
					.Select(q =>
					{
						var patientForm = mapper.Map<DTO.PatientForm>(q);
						return new DTO.FormDocmodel
						{
							PatientForm = patientForm,
							PatientFormItems = q.PatientFormItems.Select(z => mapper.Map<DTO.PatientFormItem>(z)).ToArray(),
						};
					})
					.ToArray();
				return result;
			}
			else throw new ArgumentException();
		}

		public void UpdateFormDocmodelCore(DTO.FormDocmodel entity, EntityState state)
		{
			var db = EF.PracticeManagerEntities.GetConnection(CurrentUserRowId);
			using (var scope = new TransactionScope())
			{
				var isDelete = (state == EntityState.Deleted);
				var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.AppointmentForm), typeof(EF.AppointmentFormItem), typeof(EF.PatientForm), typeof(EF.PatientFormItem));

				if (isDelete)
				{
					var rowId = entity.RowId;
					db.AppointmentFormItems.Where(q => q.AppointmentFormRowId == rowId).Delete();
					db.AppointmentForms.Where(q => q.RowId == rowId).Delete();
					db.PatientFormItems.Where(q => q.PatientFormRowId == rowId).Delete();
					db.PatientForms.Where(q => q.RowId == rowId).Delete();
				}
				else
				{
					if (entity.AppointmentForm != null)
					{
						if (state == EntityState.Added)
						{
							var appointmentForm = mapper.Map<EF.AppointmentForm>(entity.AppointmentForm);
							db.AppointmentForms.Add(appointmentForm);
							db.SaveChangesEx();
						}

						var exItems = db.AppointmentFormItems.Where(q => q.AppointmentFormRowId == entity.AppointmentForm.RowId).ToArray();
						exItems.ForEach(q => db.AppointmentFormItems.Remove(q));
						db.SaveChangesEx();

						var appointmentFormItems = mapper.Map<EF.AppointmentFormItem[]>(entity.AppointmentFormItems);
						db.AppointmentFormItems.AddRange(appointmentFormItems);
						db.SaveChangesEx();
					}
					else if (entity.PatientForm != null)
					{
						if (state == EntityState.Added)
						{
							var patientForm = mapper.Map<EF.PatientForm>(entity.PatientForm);
							db.PatientForms.Add(patientForm);
							db.SaveChangesEx();
						}
						else if (state == EntityState.Modified)
						{
							var patientForm = db.PatientForms.Single(q => q.RowId == entity.PatientForm.RowId);
							mapper.Map(entity.PatientForm, patientForm);
						}

						var exItems = db.PatientFormItems.Where(q => q.PatientFormRowId == entity.PatientForm.RowId).ToArray();
						exItems.ForEach(q => db.PatientFormItems.Remove(q));
						db.SaveChangesEx();

						var patientFormItems = mapper.Map<EF.PatientFormItem[]>(entity.PatientFormItems);
						db.PatientFormItems.AddRange(patientFormItems);
						db.SaveChangesEx();
					}
					else throw new Exception();
				}


				scope.Complete();
			}
		}
	}
}

