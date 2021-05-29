using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class LoginInfo : UpdateReturn
	{
		public LoginInfo()
		{
		}

		public Guid UserRowId { get; set; }
		public string Error { get; set; }
		public Guid CodRowId { get; set; }
		public User Role { get; set; }
	}
}
