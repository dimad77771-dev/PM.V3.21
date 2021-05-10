using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class AppointmentClinicalNote
	{
		public AppointmentClinicalNote()
		{
		}

		public Guid RowId { get; set; }
		public Guid AppointmentRowId { get; set; }
		public byte[] ClinicalNoteDocx { get; set; }
	}
}
