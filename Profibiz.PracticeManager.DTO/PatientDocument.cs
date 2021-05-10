using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class PatientDocument
	{
		public PatientDocument()
		{
		}

		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public string DocumentType { get; set; }
		public string DocumentName { get; set; }
		public string FileName { get; set; }
		public byte[] BinaryDocument { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
	}
}
