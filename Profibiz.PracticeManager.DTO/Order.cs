using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class Order
	{
        public Guid RowId { get; set; }
		public DateTime OrderDate { get; set; }
		public String OrderNumber { get; set; }
		public Guid? SupplierRowId { get; set; }
		public String Description { get; set; }
		public DateTime? Created { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public String UpdatedBy { get; set; }

		public Decimal? Amount { get; set; }
		public Decimal? Tax { get; set; }
		public Decimal? SupplierPaymentAmount { get; set; }
		public Decimal? SupplierPaymentTax { get; set; }
		public Decimal Total { get; set; }
		public Decimal PaymentTotal { get; set; }
		public Decimal PaymentRequest { get; set; }
		public String SupplierFullName { get; set; }
		public String OrderItemInfo { get; set; }

		public List<OrderItem> OrderItems { get; set; }
	}
}
