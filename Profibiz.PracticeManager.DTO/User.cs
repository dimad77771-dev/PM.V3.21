using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class User
	{
		public Guid RowId { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsGroup { get; set; }
		public Guid? GroupRowId { get; set; }
		public bool IsRole { get; set; }
		public bool IsLevel { get; set; }
		public bool IsUser { get; set; }
		public bool IsTeam { get; set; }
		public string AspNetUserId { get; set; }
		public int? OrderNum { get; set; }
		public string Email { get; set; }
		public bool IsLockedOut { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Title { get; set; }
		public string WinAccountName { get; set; }
		public string Password { get; set; }

		public bool Main_Patients { get; set; }
		public bool Main_Specialists { get; set; }
		public bool Main_AppointmentsScheduler { get; set; }
		public bool Main_CalendarEventsScheduler { get; set; }
		public bool Main_Finances { get; set; }
		public bool Main_Inventory { get; set; }
		public bool Main_Chargeouts { get; set; }
		public bool Main_Lookups { get; set; }
		public bool Main_WorkInout { get; set; }
		public bool Patient_Patient { get; set; }
		public bool Patient_MedicalHistory { get; set; }
		public bool Patient_InsuranceCoverage { get; set; }
		public bool Patient_Invoices { get; set; }
		public bool Patient_CalendarEvents { get; set; }
		public bool Patient_Notes { get; set; }
		public bool Patient_AppontmentNotes { get; set; }
		public bool Patient_PatientNotes { get; set; }
		public bool Patient_Documents { get; set; }
		public bool Patient_TreatmentNotes { get; set; }
		public bool Patient_TreatmentPlan { get; set; }
		public bool AppointmentsScheduler_IsReadOnly { get; set; }
		public string AppointmentsScheduler_HideStatuses2 { get; set; }
		public bool Patient_DataReadOnly { get; set; }
		public bool Patient_MedicalHistoryReadOnly { get; set; }
		public bool Patient_RestrictPatientList { get; set; }
		public bool AppointmentsScheduler_RestrictBookAccess { get; set; }
		public bool Patient_MedicalFormReadOnly { get; set; }
		public bool Patient_MedicalForm { get; set; }
	}
}
