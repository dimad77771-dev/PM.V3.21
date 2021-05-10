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
	public class InsuranceCoverageItemCategory
	{
		public InsuranceCoverageItemCategory()
		{
		}

		public Guid RowId { get; set; }
		public Guid InsuranceCoverageItemRowId { get; set; }
		public Guid CategoryRowId { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
	}
}
