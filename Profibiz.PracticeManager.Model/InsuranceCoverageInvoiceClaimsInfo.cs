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
	public class InsuranceCoverageInvoiceClaimsInfo
	{
		public Guid InsuranceCoverageRowId { get; set; }
		public bool CoversAllHolders { get; set; }
		public Guid? InsuranceCoverageItemRowId { get; set; }
		public Guid? InsuranceCoverageItemHolderRowId { get; set; }
		public Guid PatientRowId { get; set; }
		public Guid CategoryRowId { get; set; }
		public Decimal TotalAmont { get; set; }
		public Decimal SentAmont { get; set; }
		public Decimal ApproveAmont { get; set; }
	}
}
