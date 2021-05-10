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
	public partial class PatientNoteStatus
	{
		public PatientNoteStatus()
		{
		}

		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string ShortName { get; set; }
		public string BackgroundColor { get; set; }
		public string ForegroundColor { get; set; }
		public int? DisplayOrder { get; set; }

		public bool IsChanged { get; set; }

		public string Rowtype9 => "-";
	}
}
