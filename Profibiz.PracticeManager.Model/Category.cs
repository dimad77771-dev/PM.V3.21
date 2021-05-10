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
	public partial class Category
	{
		public Category()
		{
		}

		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string CategoryType { get; set; }

		public string FullName => Name;
		public bool IsServiceOrSuppy => true;
		public string Rowtype9 => "Medical" + CategoryType;

		public bool IsChanged { get; set; }
	}
}
