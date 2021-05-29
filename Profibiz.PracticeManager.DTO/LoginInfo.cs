using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class LoginInfo
	{
		public Boolean IsSuccess { get; set; }
		public String ResponseJson { get; set; }
		public Guid UserRowId { get; set; }
		public string Error { get; set; }
		public Guid CodRowId { get; set; }
		public User Role { get; set; }
	}
}
