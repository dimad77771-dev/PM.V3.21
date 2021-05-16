using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class User
	{
		public Guid RowId { get; set; }
		public string UserName { get; set; }
		public string Name { get; set; }
		public bool IsAdmin { get; set; }
		public bool IsGroup { get; set; }
		public Guid? GroupRowId { get; set; }
		public bool IsRole { get; set; }
		public bool IsLevel { get; set; }
		public bool IsUser { get; set; }
		public bool IsTeam { get; set; }
		public string AspNetUserId { get; set; }
		public int? OrderNum { get; set; }
		public string Email { get; set; }
		public bool IsLockedOut { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string MiddleName { get; set; }
		public string Title { get; set; }
		public string WinAccountName { get; set; }
		public string Password { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";


		public string GetAddress()
		{
			var adr = "";
			adr = (adr ?? "").Trim();
			return adr;
		}
	}
}
