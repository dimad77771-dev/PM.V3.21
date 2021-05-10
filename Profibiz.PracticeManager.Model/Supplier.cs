using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class Supplier
	{
		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string ContactName { get; set; }
		public string ContactPhoneNumber { get; set; }
		public string ContactEmailAddress { get; set; }
		public string AddressLine { get; set; }
		public string Province { get; set; }
		public string City { get; set; }
		public string Postcode { get; set; }
		public string PhoneNumber { get; set; }
		public string MobilePhoneNumber { get; set; }
		public string EmailAddress { get; set; }
		public string HSTRegNo { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";
		public string FullName => Name;
	}
}
