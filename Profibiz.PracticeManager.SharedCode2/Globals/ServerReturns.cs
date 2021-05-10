using System;
using System.Collections.Generic;
using System.Text;

namespace Profibiz.PracticeManager.SharedCode
{
	public class ServerReturnUpdateInvoice
	{
		public string InvoiceNumber { get; set; }
	}

	public class ServerReturnCloneInsuranceCoverage
	{
		public Guid CloneRowId { get; set; }
	}

	public class ServerReturnUpdateChargeout
	{
		public string ChargeoutNumber { get; set; }
	}

}
