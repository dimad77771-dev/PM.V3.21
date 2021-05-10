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
	public class InvoiceAdvinfo
	{
		public InvoiceAdvinfo() { }

		public Guid InvoiceRowId { get; set; }
		public string InsuranceProvidersInClaimsList { get; set; }
		public string PolicyOwnersInClaimsList { get; set; }

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }
	}
}
