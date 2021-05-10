using DevExpress.Mvvm;
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
	public partial class OrderItem
	{
		public OrderItem()
		{
		}

		public Guid RowId { get; set; }
		public Guid OrderRowId { get; set; }
		public Guid MedicalServiceOrSupplyRowId { get; set; }
		public Decimal Qty { get; set; }
		public Decimal Price { get; set; }
		public Decimal Tax { get; set; }
		public String Description { get; set; }
		public DateTime? Created { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public String UpdatedBy { get; set; }

		public List<OrderItem> OrderItems { get; set; }


		public Decimal LineTotal => Qty * (Price + Tax);
		public String MedicalServiceOrSupplyName => LookupDataProvider.MedicalService2Name(MedicalServiceOrSupplyRowId);


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }


		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			this.OnAddRowFromPopup?.Invoke(column);
		});
		public Action<String> OnAddRowFromPopup;
	}
}
