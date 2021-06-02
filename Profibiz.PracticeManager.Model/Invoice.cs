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
	public class Invoice
	{
		public Invoice() { }

		public System.Guid RowId { get; set; }
		public System.Guid PatientRowId { get; set; }
		public string InvoiceType { get; set; }
		public string InvoiceNumber { get; set; }
		public DateTime? InvoiceDate { get; set; }
		public string BillTo { get; set; }
		public string BillToAddress1 { get; set; }
		public string BillToAddress2 { get; set; }
		public string BillToCity { get; set; }
		public string BillToProvince { get; set; }
		public string BillToPostCode { get; set; }
		public string PrintTemplate { get; set; }
		public Guid? ThirdPartyServiceProviderRowId { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }

		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
		public decimal? ApproveAmont { get; set; }
		public decimal? DueByPatient { get; set; }
		public bool HasCoordinationProblem { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdateBy { get; set; }

		public Nullable<decimal> Amount { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public Nullable<decimal> PaymentAmount { get; set; }
		public Nullable<decimal> PaymentTax { get; set; }
		public decimal RefundAmount { get; set; }
		public Nullable<decimal> Total { get; set; }
		public Nullable<decimal> PaymentTotal { get; set; }
		public Nullable<decimal> PaymentRequest { get; set; }
		public string PatientFullName { get; set; }
		public Guid? PatientReferrerRowId { get; set; }
		public string ServiceProvidersList { get; set; }
		public string MedicalServicesList { get; set; }
		public string CategoriesList { get; set; }
		public DateTime? MaxInvoiceItemDate { get; set; }
		public DateTime? MinInvoiceItemDate { get; set; }

		public virtual List<InvoiceItem> InvoiceItems { get; set; } = new List<InvoiceItem>();
		public virtual List<InvoicePayment> InvoicePayments { get; set; } = new List<InvoicePayment>();
		public virtual List<InvoiceRefund> InvoiceRefunds { get; set; } = new List<InvoiceRefund>();
		public virtual List<InvoiceClaim> InvoiceClaims { get; set; } = new List<InvoiceClaim>();
		public virtual InvoiceAdvinfo InvoiceAdvinfo { get; set; }
		public virtual Patient Patient { get; set; }


		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }


		public bool IsSelectedRow { get; set; }

		public Nullable<decimal> Balance => DueByPatient - PaymentTotal + RefundAmount;
		public string AccountAging => Invoice.GetAccountAging(Balance, InvoiceDate);
		public string AccountAgingToolTip => Invoice.GetAccountAgingToolTip(Balance, InvoiceDate);

		public bool HasApprovedAmount => ApproveAmont > 0;


		public static decimal GetInvoiceItemsTotal(IEnumerable<InvoiceItem> invoiceItems)
		{
			var amount = invoiceItems.Sum(q => q.Units * q.Price);
			var tax = invoiceItems.Sum(q => q.Units * q.Tax);
			return (amount + tax) ?? 0;
		}

		public static void CalcTotalFields(Invoice row, IEnumerable<InvoiceItem> invoiceItems, IEnumerable<InvoiceClaim> invoiceClaims)
		{
			row.Amount = invoiceItems.Sum(q => q.Units * q.Price);
			row.Tax = invoiceItems.Sum(q => q.Units * q.Tax);
			row.Total = row.Amount + row.Tax;

			row.ApproveAmont = invoiceClaims.Sum(q => q.ApproveAmont);

			invoiceClaims.ForEach(q => q.DueByPatient = q.ApproveAmont * (row.Rate / 100M));
			row.DueByPatient = invoiceClaims.Sum(q => q.DueByPatient);
		}


		public static string InvoiceType2DefaultPrintTemplate(string invoiceType)
		{
			return InvoiceTemplateInfo.GetDefaultForInvoiceType(invoiceType);
		}


		public static string GetAccountAging(Decimal? balance, DateTime? invoiceDate)
		{
			if (balance < 0) return AccountAgingInfo.AMinus;
			if (balance == 0) return AccountAgingInfo.A0;

			var dayDelta = (DateTime.Today - (invoiceDate ?? DateTime.Today)).Days;
			if (dayDelta < 30)
			{
				return AccountAgingInfo.A30;
			}
			else if (dayDelta < 60)
			{
				return AccountAgingInfo.A60;
			}
			else if (dayDelta < 90)
			{
				return AccountAgingInfo.A90;
			}
			else return AccountAgingInfo.A90plus;
		}

		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;

		public static string GetAccountAgingToolTip(Decimal? balance, DateTime? invoiceDate)
		{
			if (balance <= 0) return "";

			var dayDelta = (DateTime.Today - (invoiceDate ?? DateTime.Today)).Days;
			return "Account Aging: " + dayDelta + " day";
		}

		public enum PaidProblemEnum { None, SentButNotApproved, ApprovedButNotPaidToUs }
		public PaidProblemEnum PaidProblem { get; set; }
		public void UpdatePaidStatus()
		{
			if (InvoiceClaims.Where(q => q.SentAmont > 0 && q.ApproveAmont == null).Any())  //условие "ApproveAmont == null" стало 
			{
				PaidProblem = PaidProblemEnum.SentButNotApproved;
			}
			else if (Balance > 0)
			{
				PaidProblem = PaidProblemEnum.ApprovedButNotPaidToUs;
			}
			else
			{
				PaidProblem = PaidProblemEnum.None;
			}
		}

		public bool IsPaid => ((Total ?? 0) > 0 && (PaymentRequest ?? 0) <= 0);
	}
}
