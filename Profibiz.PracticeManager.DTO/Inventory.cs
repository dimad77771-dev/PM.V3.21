using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Inventory
	{
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
	}
}
