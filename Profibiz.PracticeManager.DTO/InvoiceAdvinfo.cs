using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class InvoiceAdvinfo
	{
		public Guid InvoiceRowId { get; set; }
		public string InsuranceProvidersInClaimsList { get; set; }
		public string PolicyOwnersInClaimsList { get; set; }
	}
}