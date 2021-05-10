﻿using DevExpress.Mvvm;
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
	public class PayrollPayment
	{
		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public DateTime? PaymentDate { get; set; }
		public string Notes { get; set; }
		public string PaymentType { get; set; }
		public string BankName { get; set; }
		public string ChequeNumber { get; set; }
		public string BrunchNumber { get; set; }
		public string TransitNumber { get; set; }
		public string AccountNumber { get; set; }
		public string TransactionId { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }
		public decimal? Amount { get; set; }
		public string ServiceProviderFullName { get; set; }
		public string FullDescription { get; set; }
		public string AllocationInfo { get; set; }

		public virtual List<PayrollPaymentAllocation> PayrollPaymentAllocations { get; set; }

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }

		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;
	}
}
