using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public class InvoiceClaim
	{
        public Guid RowId { get; set; }
		public Guid InvoiceRowId { get; set; }
		public Guid? InsuranceCoverageRowId { get; set; }
		public DateTime SentDate { get; set; }
		public decimal SentAmont { get; set; }
		public DateTime? ApproveDate { get; set; }
		public decimal? ApproveAmont { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }
		public string Description { get; set; }
		public decimal? DueByPatient { get; set; }
		public string Forms { get; set; }
		public bool HasNoCoverage { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public InsuranceCoverage InsuranceCoverage { get; set; }
		public List<InvoiceClaimDetail> InvoiceClaimDetails { get; set; } = new List<InvoiceClaimDetail>();
	}
}
