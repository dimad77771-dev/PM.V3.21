using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class InvoiceClaimStatus
	{
		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }
		public string BackgroundColor { get; set; }
		public string ForegroundColor { get; set; }
		public int? DisplayOrder { get; set; }
	}
}
