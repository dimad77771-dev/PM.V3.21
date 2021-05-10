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
	public partial class Inventory
	{
		public Inventory()
		{
		}

		public Guid RowId { get; set; }
		public DateTime TransactionDate { get; set; }
		public Guid MedicalServiceOrSupplyRowId { get; set; }
		public Guid? OrderItemRowId { get; set; }
		public Guid? InvoiceItemRowId { get; set; }
		public Guid? OrderRowId { get; set; }
		public Guid? InvoiceRowId { get; set; }
		public Decimal Qty { get; set; }
		public Decimal Price { get; set; }
		public Decimal Tax { get; set; }
		public Decimal Credit { get; set; }
		public Decimal Debit { get; set; }
		public String Description { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public String UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public String ProductCode { get; set; }
		public String ProductName { get; set; }
		public String ProductItemType { get; set; }
		public String ProductNotes { get; set; }
		public Decimal? ProductUnitPrice { get; set; }
		public Decimal? ProductTaxRate { get; set; }
		public String ProductModel { get; set; }
		public String ProductSupplier { get; set; }
		public String ProductSize { get; set; }
		public Guid? ProductCategoryRowId { get; set; }
		public String ProductFactory { get; set; }
		public String ProductArticle { get; set; }
		public Guid? InvoicePatientRowId { get; set; }
		public String InvoicePatientName { get; set; }
		public Guid? OrderSupplierRowId { get; set; }
		public String OrderSupplierName { get; set; }


		public Decimal? CreditDisplay => (Credit == 0 ? (decimal?)null : Credit);
		public Decimal? DebitDisplay => (Debit == 0 ? (decimal?)null : Debit);
		public Decimal QtyDisplay => (Rowtype == TYPE_INVOICE ? -Qty : Qty);
		public String DisplayPatientOrSupplier =>
			Rowtype == TYPE_INVOICE ? InvoicePatientName :
			Rowtype == TYPE_ORDER ? OrderSupplierName :
			"";



		public string Rowtype =>
			OrderItemRowId == null && InvoiceItemRowId != null ? TYPE_INVOICE :
			OrderItemRowId != null && InvoiceItemRowId == null ? TYPE_ORDER :
			OrderItemRowId == null && InvoiceItemRowId == null ? TYPE_CORRECTION :
			LogicalException.Throw<string>();
		public string RowtypeToolTip =>
			Rowtype == TYPE_INVOICE ? "Invoice" :
			Rowtype == TYPE_ORDER ? "Order" :
			Rowtype == TYPE_CORRECTION ? "Adjustment" :
			"";
		public string RowtypeColor =>
			Rowtype == TYPE_INVOICE ? "Blue" :
			Rowtype == TYPE_ORDER ? "Green" :
			Rowtype == TYPE_CORRECTION ? "Red" :
			"";

		public const string TYPE_INVOICE = "I";
		public const string TYPE_ORDER = "O";
		public const string TYPE_CORRECTION = "A";


		//public string FullName => Name;
		//public string Rowtype9 => "Medical" + CategoryType;

		public bool IsChanged { get; set; }
	}
}
