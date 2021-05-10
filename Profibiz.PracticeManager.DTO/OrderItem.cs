using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class OrderItem
	{
		public Guid RowId { get; set; }
		public Guid OrderRowId { get; set; }
		public Guid MedicalServiceOrSupplyRowId { get; set; }
		public Decimal Qty { get; set; }
		public Decimal Price { get; set; }
		public Decimal Tax { get; set; }
		public String Description { get; set; }
		public DateTime? Created { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public String UpdatedBy { get; set; }
	}
}
