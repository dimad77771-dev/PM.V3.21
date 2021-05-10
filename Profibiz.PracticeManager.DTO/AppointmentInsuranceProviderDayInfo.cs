using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class AppointmentInsuranceProviderDayInfo
	{
		public Guid InsuranceProviderRowId { get; set; }
		public String InsuranceProviderCode { get; set; }
		public Int32 Count { get; set; }
	}
}
