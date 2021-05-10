using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class UserSetting
	{
		public Guid RowId { get; set; }
		public string UserCode { get; set; }
		public string Json { get; set; }
		public bool ShowChargeout { get; set; }
	}
}
