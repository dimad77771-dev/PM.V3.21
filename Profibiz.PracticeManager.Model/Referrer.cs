using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class Referrer
	{
		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string AdrdessLine1 { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Postcode { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";


		public string GetAddress()
		{
			var adr = AdrdessLine1 + " " + City + " " + Province + " " + Postcode;
			adr = (adr ?? "").Trim();
			return adr;
		}
	}
}
