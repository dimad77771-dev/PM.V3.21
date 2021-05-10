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
	public class EmailSendRecipient
	{
		public EmailSendRecipient() { }

		public Guid RowId { get; set; }
		public Guid EmailSendRowId { get; set; }
		public String Name { get; set; }
		public String Email { get; set; }
		public Guid? PatientRowId { get; set; }
		public Guid? ServiceProviderRowId { get; set; }

		public enum RecipientTypes { Patient, Specialist, Other };
		public RecipientTypes RecipientType =>
			PatientRowId != null ? RecipientTypes.Patient :
			ServiceProviderRowId != null ? RecipientTypes.Specialist :
			RecipientTypes.Other;
		public String RecipientTypeText => Enum.GetName(typeof(RecipientTypes), RecipientType);


		public bool IsChecked { get; set; } = true;
		public bool IsVisibleIsChecked => (RecipientType != RecipientTypes.Other);

		public bool IsNew { get; set; }
		public bool IsChanged { get; set; }
	}
}
