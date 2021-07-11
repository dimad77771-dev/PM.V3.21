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
	public partial class FormDocument : IEntityCreatedUpdated
	{
		public FormDocument()
		{
		}

		public Guid RowId { get; set; }
		public byte[] DocxBytes { get; set; }
		public string TemplateName { get; set; }
		public string TemplatePath { get; set; }
		public Guid? AppointmentRowId { get; set; }
		public Guid? PatientRowId { get; set; }
		public Guid? CategoryRowId { get; set; }
		public Guid? ServiceProviderRowId { get; set; }

		public bool IsChanged { get; set; }

		public Guid? CreatedByUserRowId { get; set; }
		public Guid? UpdatedByUserRowId { get; set; }
		public DateTime? CreatedByDateTime { get; set; }
		public DateTime? UpdatedByDateTime { get; set; }

		public String CreatedUpdatedString => this.GetCreatedUpdatedString();
	}
}
