using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
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
