using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class EmailSendRecipient
	{
		public Guid RowId { get; set; }
		public Guid EmailSendRowId { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public Guid? PatientRowId { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
	}
}