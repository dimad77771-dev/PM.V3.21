using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class ChargeoutRefcharge
	{
		public Guid RowId { get; set; }
		public Guid ChargeoutRowId { get; set; }
		public Guid RefchargeRowId { get; set; }
		public Decimal Amount { get; set; }
		public Decimal Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public Chargeout Chargeout { get; set; }
		public Refcharge Refcharge { get; set; }
	}
}
