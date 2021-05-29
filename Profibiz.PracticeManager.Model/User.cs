﻿using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
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


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";


		public string GetAddress()
		{
			var adr = "";
			adr = (adr ?? "").Trim();
			return adr;
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
			};
		}

	}
}
