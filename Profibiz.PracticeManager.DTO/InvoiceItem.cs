using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class InvoiceItem
	{
		public InvoiceItem()
		{
		}

		public System.Guid RowId { get; set; }
		public System.Guid InvoiceRowId { get; set; }
		public Nullable<System.Guid> AppointmentRowId { get; set; }
		public Nullable<System.Guid> ServcieOrSupplyRowId { get; set; }
		public Nullable<decimal> Units { get; set; }
		public Nullable<decimal> Price { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public string Description { get; set; }
		public DateTime? ItemDate { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual Appointment Appointment { get; set; }
		public virtual Invoice Invoice { get; set; }
		public virtual MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
	}
}
