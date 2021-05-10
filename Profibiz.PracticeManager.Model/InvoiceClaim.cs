using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
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
	public class InvoiceClaim
	{
		public InvoiceClaim() {}

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

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public DateTime? SentDateInView => (SentDate == default(DateTime) ? (DateTime?)null : SentDate);

		public bool HasDetailsAmountError => false;// ( (ApproveAmont ?? 0) != InvoiceClaimDetails.Sum(q => q.Amount) );



		//public StatusEnum Status { get; set; }
		//public enum StatusEnum { Sent, Rejected, Partially, Approved }

		public String StatusInfo
		{
			get
			{
				if (ApproveAmont == null) return "Sent";
				else if (ApproveAmont <= 0) return "Rejected";
				else if (ApproveAmont < SentAmont) return "Partially";
				else return "Approved";
			}
		}

		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;
	}
}
