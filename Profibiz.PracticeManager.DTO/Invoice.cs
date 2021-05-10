using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Invoice
	{
		public Guid RowId { get; set; }
		public Guid PatientRowId { get; set; }
		public string InvoiceType { get; set; }
		public string InvoiceNumber { get; set; }
		public Nullable<System.DateTime> InvoiceDate { get; set; }
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
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdateBy { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public Nullable<decimal> PaymentAmount { get; set; }
		public Nullable<decimal> PaymentTax { get; set; }
		public decimal RefundAmount { get; set; }
		public Nullable<decimal> Total { get; set; }
		public Nullable<decimal> PaymentTotal { get; set; }
		public Nullable<decimal> PaymentRequest { get; set; }
		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
		public decimal? ApproveAmont { get; set; }
		public decimal? DueByPatient { get; set; }
		public bool HasCoordinationProblem { get; set; }


		public decimal? Amount { get; set; }
		public string PatientFullName { get; set; }
		public Guid? PatientReferrerRowId { get; set; }
		public string ServiceProvidersList { get; set; }
		public string MedicalServicesList { get; set; }
		public string CategoriesList { get; set; }
		public DateTime? MaxInvoiceItemDate { get; set; }
		public DateTime? MinInvoiceItemDate { get; set; }

		public List<InvoiceItem> InvoiceItems { get; set; }
		public List<InvoicePayment> InvoicePayments { get; set; }
		public List<InvoiceRefund> InvoiceRefunds { get; set; }
		public List<InvoiceClaim> InvoiceClaims { get; set; }
		public InvoiceAdvinfo InvoiceAdvinfo { get; set; }
		public Patient Patient { get; set; }
	}
}