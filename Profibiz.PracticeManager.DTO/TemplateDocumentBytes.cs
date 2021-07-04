using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class TemplateDocumentBytes
	{
		public Guid RowId { get; set; }
		public byte[] DocumentBytes { get; set; }
	}
}
