using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class ChargeoutPaycharge
	{
		public System.Guid RowId { get; set; }
		public System.Guid ChargeoutRowId { get; set; }
		public System.Guid PaychargeRowId { get; set; }
		public Nullable<decimal> Amount { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual Chargeout Chargeout { get; set; }
		public virtual Paycharge Paycharge { get; set; }
	}
}
