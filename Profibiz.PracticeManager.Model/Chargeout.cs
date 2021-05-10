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
	public class Chargeout
	{
		public Chargeout() { }

		public Guid RowId { get; set; }
		public Guid ChargeoutRecipientRowId { get; set; }
		public string ChargeoutType { get; set; }
		public string ChargeoutNumber { get; set; }
		public DateTime? ChargeoutDate { get; set; }
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
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdateBy { get; set; }

		public Nullable<decimal> Amount { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public Nullable<decimal> PaychargeAmount { get; set; }
		public Nullable<decimal> PaychargeTax { get; set; }
		public decimal RefchargeAmount { get; set; }
		public Nullable<decimal> Total { get; set; }
		public Nullable<decimal> PaychargeTotal { get; set; }
		public Nullable<decimal> PaychargeRequest { get; set; }
		public string PatientFullName { get; set; }
		public Guid? PatientReferrerRowId { get; set; }
		public string ServiceProvidersList { get; set; }
		public string MedicalServicesList { get; set; }
		public string CategoriesList { get; set; }
		public string ChargeoutRecipientName { get; set; }
		public DateTime? MaxChargeoutItemDate { get; set; }
		public DateTime? MinChargeoutItemDate { get; set; }

		public virtual List<ChargeoutItem> ChargeoutItems { get; set; } = new List<ChargeoutItem>();
		public virtual List<ChargeoutPaycharge> ChargeoutPaycharges { get; set; } = new List<ChargeoutPaycharge>();
		public virtual List<ChargeoutRefcharge> ChargeoutRefcharges { get; set; } = new List<ChargeoutRefcharge>();
		public virtual ChargeoutAdvinfo ChargeoutAdvinfo { get; set; }
		public virtual ChargeoutRecipient ChargeoutRecipient { get; set; }


		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }


		public bool IsSelectedRow { get; set; }

		public Nullable<decimal> Balance => Total - PaychargeTotal + RefchargeAmount;
		public string AccountAging => Chargeout.GetAccountAging(Balance, ChargeoutDate);
		public string AccountAgingToolTip => Chargeout.GetAccountAgingToolTip(Balance, ChargeoutDate);


		public static decimal GetChargeoutItemsTotal(IEnumerable<ChargeoutItem> chargeoutItems)
		{
			var amount = chargeoutItems.Sum(q => q.Units * q.Price);
			var tax = chargeoutItems.Sum(q => q.Units * q.Tax);
			return (amount + tax) ?? 0;
		}

		public static void CalcTotalFields(Chargeout row, IEnumerable<ChargeoutItem> chargeoutItems)
		{
			row.Amount = chargeoutItems.Sum(q => q.Units * q.Price);
			row.Tax = chargeoutItems.Sum(q => q.Units * q.Tax);
			row.Total = row.Amount + row.Tax;
		}


		public static string ChargeoutType2DefaultPrintTemplate(string chargeoutType)
		{
			return ChargeoutTemplateInfo.GetDefaultForChargeoutType(chargeoutType);
		}


		public static string GetAccountAging(Decimal? balance, DateTime? chargeoutDate)
		{
			if (balance < 0) return AccountAgingInfo.AMinus;
			if (balance == 0) return AccountAgingInfo.A0;

			var dayDelta = (DateTime.Today - (chargeoutDate ?? DateTime.Today)).Days;
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

		public static string GetAccountAgingToolTip(Decimal? balance, DateTime? chargeoutDate)
		{
			if (balance <= 0) return "";

			var dayDelta = (DateTime.Today - (chargeoutDate ?? DateTime.Today)).Days;
			return "Account Aging: " + dayDelta + " day";
		}

		public enum PaidProblemEnum { None, NotPaid }
		public PaidProblemEnum PaidProblem { get; set; }
		public void UpdatePaidStatus()
		{
			if (Balance > 0)
			{
				PaidProblem = PaidProblemEnum.NotPaid;
			}
			else
			{
				PaidProblem = PaidProblemEnum.None;
			}
		}

		public static readonly string CHARGEOUT_TYPE = "Chargeout";
	}
}
