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
	public class SupplierPaymentRefund
	{
		public SupplierPaymentRefund() { }

		public Guid RowId { get; set; }
		public Guid SupplierPaymentRowId { get; set; }
		public Guid SupplierRefundRowId { get; set; }
		public Decimal Amount { get; set; }
		public Decimal Tax { get; set; }
		public DateTime AllocationDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual SupplierRefund SupplierRefund { get; set; }
		public virtual SupplierPayment SupplierPayment { get; set; }

		public Decimal Amount0 { get; set; }
		public Decimal? NewBalanceDue => SupplierPayment?.SupplierPaymentBalance - (Amount - Amount0);
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
