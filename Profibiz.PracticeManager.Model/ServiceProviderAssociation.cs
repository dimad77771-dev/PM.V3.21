using DevExpress.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class ServiceProviderAssociation //: INotifyPropertyChanged
	{
		//public event PropertyChangedEventHandler PropertyChanged;

		public Guid RowId { get; set; }
        public Guid AssociationRowId { get; set; }
        public Guid ServiceProviderRowId { get; set; }
        public string RegistrationNumber { get; set; }
        public DateTime? RegistrationDate { get; set; }
        public DateTime? RegistrationExpiryDate { get; set; }
        public bool IsPrimary { get; set; }
        public ProfessionalAssociation ProfessionalAssociation { get; set; }

		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			this.OnAddRowFromPopup?.Invoke(column);
		});
		public Action<String> OnAddRowFromPopup;

		//public void OnIsPrimaryChanged()
		//{

		//}

		//public void OnPropertyChanged(string propertyName, object before, object after)
		//{
		//	//Perform property validation
		//	var propertyChanged = PropertyChanged;
		//	if (propertyChanged != null)
		//	{
		//		propertyChanged(this, new PropertyChangedEventArgs(propertyName));
		//	}
		//}

		public bool IsChanged { get; set; }
	}
}
