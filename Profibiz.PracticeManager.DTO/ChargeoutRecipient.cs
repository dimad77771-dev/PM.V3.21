using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class ChargeoutRecipient
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
		public string ShortName { get; set; }
		public string BackgroundColor { get; set; }
		public string ForegroundColor { get; set; }
		public int DisplayOrder { get; set; }
	}
}
