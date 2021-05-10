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
	public partial class PatientNote
	{
		public PatientNote()
		{
		}

		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public DateTime? NoteDate { get; set; }
		public Guid? NoteStatusRowId { get; set; }
		public string NoteText { get; set; }
		public byte[] NoteDocx { get; set; }

		public bool IsChanged { get; set; }
	}
}
