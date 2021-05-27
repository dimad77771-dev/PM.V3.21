using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class Appointment
	{
		public Appointment()
		{
		}

		public Guid RowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public string Notes { get; set; }
		public Guid PatientRowId { get; set; }
		public string Description { get; set; }
		public Guid? AppointmentClinicalNoteRowId { get; set; }
		public Guid? AppointmentTreatmentNoteRowId { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }
		public string Type { get; set; }
		public string RefNumber { get; set; }
		public string RefStatus { get; set; }
		public Guid AppointmentBookRowId { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<DateTime> CreateDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public Nullable<DateTime> UpdatedDateTime { get; set; }
		public Nullable<Guid> ServiceProviderRowId { get; set; }
		public bool Completed { get; set; }
		public Guid? MedicalServicesOrSupplyRowId { get; set; }
		public Guid? InsuranceCoverageRowId { get; set; }
		public string PatientFullName { get; set; }
		public decimal PatientRate { get; set; }
		public bool HasNoCoverage { get; set; }
		public Guid? InvoiceRowId { get; set; }
		public string InvoiceNumber { get; set; }
		public bool IsRemainderEmail { get; set; }
		public bool IsRemainderSms { get; set; }

		public Guid? InsuranceProviderRowId { get; set; }
		public string PolicyNumber { get; set; }
		public Guid? PolicyOwnerRowId { get; set; }
		public string PolicyOwnerFullName { get; set; }
		public DateTime PolicyCoverageStartDate { get; set; }
		public DateTime PolicyCoverageValidUntil { get; set; }



		public Patient Patient { get; set; }
		public InvoiceItem InvoiceItem { get; set; }
		public AppointmentRemainder[] AppointmentRemainders { get; set; }

		public FormDocument[] FormDocuments { get; set; }
	}
}
