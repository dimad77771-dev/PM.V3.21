using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class Chargeout
	{
		public Guid RowId { get; set; }
		public Guid ChargeoutRecipientRowId { get; set; }
		public string ChargeoutType { get; set; }
		public string ChargeoutNumber { get; set; }
		public Nullable<System.DateTime> ChargeoutDate { get; set; }
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
		public Nullable<decimal> PaychargeAmount { get; set; }
		public Nullable<decimal> PaychargeTax { get; set; }
		public decimal RefchargeAmount { get; set; }
		public Nullable<decimal> Total { get; set; }
		public Nullable<decimal> PaychargeTotal { get; set; }
		public Nullable<decimal> PaychargeRequest { get; set; }
		public decimal Rate { get; set; }
		public bool HasNoCoverage { get; set; }
		public Guid? ServiceProviderRowId { get; set; }


		public decimal? Amount { get; set; }
		public string ServiceProvidersList { get; set; }
		public string MedicalServicesList { get; set; }
		public string CategoriesList { get; set; }
		public string ChargeoutRecipientName { get; set; }
		public DateTime? MaxChargeoutItemDate { get; set; }
		public DateTime? MinChargeoutItemDate { get; set; }

		public List<ChargeoutItem> ChargeoutItems { get; set; }
		public List<ChargeoutPaycharge> ChargeoutPaycharges { get; set; }
		public List<ChargeoutRefcharge> ChargeoutRefcharges { get; set; }
		public ChargeoutRecipient ChargeoutRecipient { get; set; }
	}
}