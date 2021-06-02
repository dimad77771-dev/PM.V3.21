using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class Setting
	{
		public Guid RowId { get; set; }
		public Guid? UserRowId { get; set; }
		public string Name { get; set; }
		public string Type { get; set; }
		public string Value { get; set; }
	}
}
