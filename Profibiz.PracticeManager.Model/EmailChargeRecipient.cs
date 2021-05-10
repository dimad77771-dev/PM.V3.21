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
	public class EmailChargeRecipient
	{
		public EmailChargeRecipient() { }

		public Guid RowId { get; set; }
		public Guid EmailChargeRowId { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public Guid? ChargeoutRecipientRowId { get; set; }
		public Guid? ServiceProviderRowId { get; set; }

		public enum RecipientTypes { ChargeoutRecipient, Specialist, Other };
		public RecipientTypes RecipientType =>
			ChargeoutRecipientRowId != null ? RecipientTypes.ChargeoutRecipient :
			ServiceProviderRowId != null ? RecipientTypes.Specialist :
			RecipientTypes.Other;
		public String RecipientTypeText => Enum.GetName(typeof(RecipientTypes), RecipientType);


		public bool IsChecked { get; set; } = true;
		public bool IsVisibleIsChecked => (RecipientType != RecipientTypes.Other);

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }
	}
}
