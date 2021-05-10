using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class ClientError
	{
		public ClientError()
		{
		}

		public Guid RowId { get; set; }
		public DateTime ErrorDateTime { get; set; }
		public string ErrorText { get; set; }
		public string MachineName { get; set; }
		public string OSVersion { get; set; }
		public string UserName { get; set; }
		public string UserDomainName { get; set; }
	}
}
