using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Category
	{
		public Category()
		{
		}

		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string CategoryType { get; set; }
	}
}
