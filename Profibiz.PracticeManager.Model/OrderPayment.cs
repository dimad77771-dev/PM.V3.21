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
	public class OrderPayment
	{
		public OrderPayment() { }

		public Guid RowId { get; set; }
		public Guid OrderRowId { get; set; }
		public Guid SupplierPaymentRowId { get; set; }
		public Decimal? Amount { get; set; }
		public Decimal? Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual Order Order { get; set; }
		public virtual SupplierPayment SupplierPayment { get; set; }

		public Decimal? Amount0 { get; set; }
		public Decimal? NewBalanceDue => Order?.PaymentRequest - (Amount - (Amount0 ?? 0));
		//public Decimal? NewBalanceDue => (Amount - (Amount0 ?? 0));
		public void OnAfterLoad()
		{
			Amount0 = Amount;
		}


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }
		public BackgroundAnimationElement CellBackgroundAmount { get; set; } = new BackgroundAnimationElement();


		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;
	}
}
