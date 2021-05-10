using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class InsuranceProvidersViewGroupMapping
	{
		public Guid RowId { get; set; }
		public Guid InsuranceProvidersViewGroupRowId { get; set; }
		public Guid InsuranceProviderRowId { get; set; }

		public virtual InsuranceProvider InsuranceProvider { get; set; }
		public virtual InsuranceProvidersViewGroup InsuranceProvidersViewGroup { get; set; }
	}
}
