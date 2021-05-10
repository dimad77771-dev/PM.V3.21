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
	public class PayrollPaymentAllocation
	{
		public Guid RowId { get; set; }
		public Guid PayrollPaymentRowId { get; set; }
		public DateTime PeriodStart { get; set; }
		public DateTime PeriodFinish { get; set; }
		public decimal Amount { get; set; }

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }

		public DateTime? EditPeriodStart { get; set; }
		public void InitEditPeriodStart()
		{
			EditPeriodStart = (PeriodStart == default(DateTime) ? (DateTime?)null : PeriodStart);
			(this as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(EditPeriodStart))
				{
					if (EditPeriodStart != null)
					{
						PeriodStart = EditPeriodStart.Value.FirstDayMonth();
						PeriodFinish = PeriodStart.LastDayMonth();
					}
				}
			};
		}


		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;
	}
}
