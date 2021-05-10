using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class ChargeoutItem
	{
		public ChargeoutItem()
		{
		}

		public Guid RowId { get; set; }
		public Guid ChargeoutRowId { get; set; }
		public Guid InvoiceItemRowId { get; set; }
		public decimal? Units { get; set; }
		public decimal? Price { get; set; }
		public decimal? Tax { get; set; }
		public string Description { get; set; }
		public DateTime? ItemDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public Chargeout Chargeout { get; set; }
		public InvoiceItem InvoiceItem { get; set; }
	}
}
