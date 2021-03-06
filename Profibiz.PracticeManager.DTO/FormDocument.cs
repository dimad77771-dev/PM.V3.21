using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class FormDocument
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

		public Guid? CreatedByUserRowId { get; set; }
		public Guid? UpdatedByUserRowId { get; set; }
		public DateTime? CreatedByDateTime { get; set; }
		public DateTime? UpdatedByDateTime { get; set; }
	}
}
