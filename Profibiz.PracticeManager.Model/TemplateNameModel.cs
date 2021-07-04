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
	public partial class TemplateNameModel
	{
		public TemplateNameModel()
		{
		}

		public Guid TemplateRowId { get; set; }
		public string TemplateName { get; set; }
		//public string TemplatePath { get; set; }

		public string FullName => TemplateName;
		public string Rowtype9 => "-";

		public bool IsChanged { get; set; }
	}
}
