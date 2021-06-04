using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class User
	{
		public User()
		{
			//AppointmentsScheduler_HideStatuses2_Items = new List<object>() { "11", "22", "33", "44" }
			//	.ToObservableCollection();
			//AppointmentsScheduler_HideStatuses2_SelectedItems = new[] { AppointmentsScheduler_HideStatuses2_Items[0], AppointmentsScheduler_HideStatuses2_Items[3] }
			//	.ToList();
		}

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

		public ObservableCollection<object> AppointmentsScheduler_HideStatuses2_Items { get; set; } 
				= LookupDataProvider.Instance?.AppointmentStatuses?.Cast<object>()?.ToObservableCollection();
		public List<object> AppointmentsScheduler_HideStatuses2_SelectedItems { get; set; } = new AppointmentStatus[0].Cast<object>().ToList();

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";

		public bool HasAppointmentsScheduler_HideStatuses2()
		{
			return !string.IsNullOrEmpty(AppointmentsScheduler_HideStatuses2);
		}


		public string GetAddress()
		{
			var adr = "";
			adr = (adr ?? "").Trim();
			return adr;
		}

		public void Update_AppointmentsScheduler_HideStatuses2()
		{
			if (AppointmentsScheduler_HideStatuses2_SelectedItems != null)
			{
				AppointmentsScheduler_HideStatuses2 = string.Join(";", AppointmentsScheduler_HideStatuses2_SelectedItems.Cast<AppointmentStatus>().Select(q => q.RowId.ToString()));
			}
			else
			{
				AppointmentsScheduler_HideStatuses2 = null;
			}
		}

		public void Load_AppointmentsScheduler_HideStatuses2()
		{
			if (!string.IsNullOrEmpty(AppointmentsScheduler_HideStatuses2))
			{
				var rowIds = AppointmentsScheduler_HideStatuses2.Split(';').Select(q => new Guid(q)).ToArray();
				AppointmentsScheduler_HideStatuses2_SelectedItems = AppointmentsScheduler_HideStatuses2_Items.Where(q => rowIds.Contains(((AppointmentStatus)q).RowId)).ToList();
			}
		}


		public static User GetFullRole()
		{
			return new User
			{
				IsAdmin = true,
				Main_Patients = true,
				Main_Specialists = true,
				Main_AppointmentsScheduler = true,
				Main_CalendarEventsScheduler = true,
				Main_Finances = true,
				Main_Inventory = true,
				Main_Chargeouts = true,
				Main_Lookups = true,
				Main_WorkInout = true,
				Patient_Patient = true,
				Patient_MedicalHistory = true,
				Patient_InsuranceCoverage = true,
				Patient_Invoices = true,
				Patient_CalendarEvents = true,
				Patient_Notes = true,
				Patient_AppontmentNotes = true,
				Patient_PatientNotes = true,
				Patient_Documents = true,
				Patient_TreatmentNotes = true,
				Patient_TreatmentPlan = true,

				//AppointmentsScheduler_IsReadOnly = true,
				AppointmentsScheduler_IsReadOnly = false,
				AppointmentsScheduler_HideStatuses2 = "",//"9b3ef35d-b5a1-4f68-818b-eca0698724f4;e023d5fe-9c07-4156-a1a2-1f7c20c4b1d1;df1c22f2-ebd6-4174-969f-abeb2de19dc8",

				AppointmentsScheduler_RestrictBookAccess = false,
				Patient_DataReadOnly = false,
				Patient_MedicalHistoryReadOnly = false,
				Patient_RestrictPatientList = false,

				//AppointmentsScheduler_RestrictBookAccess = true,
				//Patient_DataReadOnly = true,
				//Patient_MedicalHistoryReadOnly = true,
				//Patient_RestrictPatientList = true,
			};
		}

	}
}
