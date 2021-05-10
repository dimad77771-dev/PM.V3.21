using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class NLogItem
	{
		public NLogItem()
		{
		}

		
		public DateTime date { get; set; }
		public string level { get; set; }
		public string logger { get; set; }
		public string message { get; set; }
		public Guid activityid { get; set; }
		public string machinename { get; set; }
		//public string callsite_linenumber { get; set; }
		//public string callsite { get; set; }
	}
}
