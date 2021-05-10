using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public partial class InsuranceCoverageItemCategory
	{
		public Guid RowId { get; set; }
		public Guid InsuranceCoverageItemRowId { get; set; }
		public Guid CategoryRowId { get; set; }
	}
}
