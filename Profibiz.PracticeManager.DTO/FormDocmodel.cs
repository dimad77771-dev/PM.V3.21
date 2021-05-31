using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class FormDocmodel
	{
		public FormDocmodel()
		{
		}

		public Guid RowId { get; set; }

		public Form Form { get; set; }
		public FormItem[] FormItems { get; set; }
		public AppointmentForm AppointmentForm { get; set; }
		public AppointmentFormItem[] AppointmentFormItems { get; set; }
		public PatientForm PatientForm { get; set; }
		public PatientFormItem[] PatientFormItems { get; set; }
	}
}
