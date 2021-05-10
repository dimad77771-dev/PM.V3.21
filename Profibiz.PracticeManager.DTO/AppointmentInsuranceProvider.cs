using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class AppointmentInsuranceProvider
	{
		public System.Guid RowId { get; set; }
		public System.Guid AppointmentRowId { get; set; }
		public System.Guid InsuranceProviderRowId { get; set; }
		public decimal Percentage { get; set; }

		public virtual Appointment Appointment { get; set; }
		public virtual InsuranceProvider InsuranceProvider { get; set; }
	}
}
