using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class Template
	{
		public Guid RowId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string InvoiceType { get; set; }
		public bool IsDefault { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string FullName => Name;
		public string Rowtype9 => "-";
	}
}
