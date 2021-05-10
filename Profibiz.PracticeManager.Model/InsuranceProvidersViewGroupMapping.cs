using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class InsuranceProvidersViewGroupMapping
	{
		public Guid RowId { get; set; }
		public Guid InsuranceProvidersViewGroupRowId { get; set; }
		public Guid InsuranceProviderRowId { get; set; }

		public virtual InsuranceProvider InsuranceProvider { get; set; }
		public virtual InsuranceProvidersViewGroup InsuranceProvidersViewGroup { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";
	}
}
