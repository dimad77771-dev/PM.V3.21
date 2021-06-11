using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class UserSetting
	{
		public Guid RowId { get; set; }
		public string UserCode { get; set; }
		public string Json { get; set; }
		public bool ShowChargeout { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";

		public bool IsRequestError { get; set; }
	}
}
