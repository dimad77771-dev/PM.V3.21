using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class Template
	{
		public Guid RowId { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public string InvoiceType { get; set; }
		public bool IsDefault { get; set; }
		public bool IsEnabled { get; set; }
		public bool IsHidden { get; set; }
		public string TemplateType { get; set; }
		public string FormType { get; set; }
		public Guid? CategoryRowId { get; set; }
		public string Comments { get; set; }
		public bool HasDocumentBytes { get; set; }
	}
}
