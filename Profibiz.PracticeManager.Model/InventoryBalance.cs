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
	public partial class InventoryBalance
	{
		public InventoryBalance()
		{
		}

		public Guid RowId { get; set; }
		public String Code { get; set; }
		public String Name { get; set; }
		public String ItemType { get; set; }
		public String Notes { get; set; }
		public Decimal? UnitPrice { get; set; }
		public Decimal? TaxRate { get; set; }
		public String Model { get; set; }
		public String Supplier { get; set; }
		public String Size { get; set; }
		public Guid? CategoryRowId { get; set; }
		public String Factory { get; set; }
		public String Article { get; set; }
		public Decimal? Balance { get; set; }

		public Decimal? NewBalance { get; set; }
		public String TransactionDescription { get; set; }
		public DateTime? TransactionDate { get; set; }

		public Boolean IsBalanceModify => (NewBalance ?? 0) != (Balance ?? 0);
		public String CategoryName => LookupDataProvider.Category2Name(CategoryRowId);






		//public string FullName => Name;
		//public string Rowtype9 => "Medical" + CategoryType;

		public bool IsChanged { get; set; }
	}
}
