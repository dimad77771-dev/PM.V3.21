using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class PrintDocument
	{
		public PrintDocument()
		{
		}

		public Guid RowId { get; set; }
		public int OrderNum { get; set; }
		public string Name { get; set; }
		public string TemplateFile { get; set; }
	}
}
