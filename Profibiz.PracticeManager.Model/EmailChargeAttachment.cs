using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
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
	public class EmailChargeAttachment
	{
		public EmailChargeAttachment() { }

		public Guid RowId { get; set; }
		public Guid EmailChargeRowId { get; set; }
		public String FileName { get; set; }
		public Byte[] FileBytes { get; set; }

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }
	}
}
