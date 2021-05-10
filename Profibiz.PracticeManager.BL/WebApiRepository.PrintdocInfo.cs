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
		public DTO.PrintdocInfo GetPrintdocInfo(Guid invoiceRowId)
		{
			var db = EF.PracticeManagerEntities.Connection;

			var invoice = db.InvoicesV.Single(q => q.RowId == invoiceRowId);
			var patientRowId = invoice.PatientRowId;
			var patient = db.PatientsV.Single(q => q.RowId == patientRowId);
			var medicalHistoryRecords = db.MedicalHistoryRecordsV.Where(q => q.PatientRowId == patientRowId).ToArray();
			var appointments = db.AppointmentsV.Where(q => q.InvoiceRowId == invoiceRowId).ToArray();
			var firstAppointmentStart = appointments.OrderBy(q => q.Start).FirstOrDefault()?.Start;



			var options = AutoMapperHelper.CreateOptions();
			var mapper = AutoMapperHelper.GetPocoMapperWithOptions(options, 
				typeof(EF.PatientV), typeof(EF.InvoiceV), typeof(EF.MedicalHistoryRecordV), typeof(EF.AppointmentV));

			var ret = new DTO.PrintdocInfo
			{
				Invoice = mapper.Map<DTO.Invoice>(invoice),
				Patient = mapper.Map<DTO.Patient>(patient),
				MedicalHistoryRecords = mapper.Map<List<DTO.MedicalHistoryRecord>>(medicalHistoryRecords),
				Appointments = mapper.Map<List<DTO.Appointment>>(appointments),
				FirstAppointmentStart = firstAppointmentStart,
			};


			return ret;
		}


		public DTO.PrintDocument[] GetPrintDocuments()
		{
			var db = EF.PracticeManagerEntities.Connection;

			var list = db.PrintDocuments.ToArray();

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PrintDocument));
			var ret = mapper.Map<DTO.PrintDocument[]>(list);
			return ret;
		}
	}
}

