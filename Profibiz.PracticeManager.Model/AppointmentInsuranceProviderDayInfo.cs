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
	public partial class AppointmentInsuranceProviderDayInfo
	{
		public AppointmentInsuranceProviderDayInfo()
		{
		}

		public Guid InsuranceProviderRowId { get; set; }
		public String InsuranceProviderCode { get; set; }
		public Int32 Count { get; set; }

		public bool IsChanged { get; set; }

		public string Rowtype9 => "-";
	}
}
