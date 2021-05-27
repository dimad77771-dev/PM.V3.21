using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class WorkInout
	{
		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime? Finish { get; set; }
		public string Description { get; set; }
	}
}
