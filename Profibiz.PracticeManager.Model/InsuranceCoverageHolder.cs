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
	public class InsuranceCoverageHolder
	{
		public InsuranceCoverageHolder()
		{
			this.InsuranceCoverageHolderServices = new ObservableCollection<InsuranceCoverageHolderService>();
		}

		public Guid RowId { get; set; }
		public Guid InsuranceCoverageRowId { get; set; }
		public Guid PolicyHolderRowId { get; set; }
		public string PolicyHolderType { get; set; }

		public virtual InsuranceCoverage InsuranceCoverages { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual ObservableCollection<InsuranceCoverageHolderService> InsuranceCoverageHolderServices { get; set; }
	}
}
