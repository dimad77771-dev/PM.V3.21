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
	public class InsuranceProvidersViewGroup
	{
		public Guid RowId { get; set; }
		public string Name { get; set; }

		public virtual ObservableCollection<InsuranceProvidersViewGroupMapping> InsuranceProvidersViewGroupMappings { get; set; } = new ObservableCollection<InsuranceProvidersViewGroupMapping>();

		public string ListInsuranceProvidersCompanyName => 
			string.Join("\n", InsuranceProvidersViewGroupMappings.Select(q => q?.InsuranceProvider?.CompanyName).ToArray());

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";
		public string DragDropRowText => Name;
	}
}
