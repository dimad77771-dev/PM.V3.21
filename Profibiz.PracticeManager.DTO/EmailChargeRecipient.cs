using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class EmailChargeRecipient
	{
		public Guid RowId { get; set; }
		public Guid EmailChargeRowId { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public Guid? ChargeoutRecipientRowId { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
	}
}