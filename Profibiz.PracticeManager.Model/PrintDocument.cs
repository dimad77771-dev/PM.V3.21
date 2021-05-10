using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class PrintDocument
	{
		public PrintDocument()
		{
		}

		public Guid RowId { get; set; }
		public int OrderNum { get; set; }
		public string Name { get; set; }
		public string TemplateFile { get; set; }

		public bool IsChecked { get; set; }
	}
}
