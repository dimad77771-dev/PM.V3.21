using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class InsuranceProvidersViewGroup
	{
		public InsuranceProvidersViewGroup()
		{
			this.InsuranceProvidersViewGroupMappings = new HashSet<InsuranceProvidersViewGroupMapping>();
		}

		public Guid RowId { get; set; }
		public string Name { get; set; }

		public virtual ICollection<InsuranceProvidersViewGroupMapping> InsuranceProvidersViewGroupMappings { get; set; }
	}
}
