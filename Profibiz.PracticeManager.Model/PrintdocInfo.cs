using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class PrintdocInfo
	{
		public PrintdocInfo()
		{
		}

		public Invoice Invoice { get; set; }
		public Patient Patient { get; set; }
		public List<MedicalHistoryRecord> MedicalHistoryRecords { get; set; }
		public List<Appointment> Appointments { get; set; }
		public DateTime? FirstAppointmentStart { get; set; }
	}
}
