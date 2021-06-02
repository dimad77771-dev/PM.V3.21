using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class Setting
	{
		public Guid RowId { get; set; }
		public Guid? UserRowId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";
	}
}
