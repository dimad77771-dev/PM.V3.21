using DevExpress.Mvvm;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class Order
	{
		public Order()
		{
		}

		public Guid RowId { get; set; }
		public DateTime OrderDate { get; set; }
		public String OrderNumber { get; set; }
		public Guid? SupplierRowId { get; set; }
		public String Description { get; set; }
		public DateTime? Created { get; set; }
		public String CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public String UpdatedBy { get; set; }

		public Decimal? Amount { get; set; }
		public Decimal? Tax { get; set; }
		public Decimal? SupplierPaymentAmount { get; set; }
		public Decimal? SupplierPaymentTax { get; set; }
		public Decimal Total { get; set; }
		public Decimal PaymentTotal { get; set; }
		public Decimal PaymentRequest { get; set; }
		public String SupplierFullName { get; set; }
		public String OrderItemInfo { get; set; }


		public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

		public Decimal Balance => Total - PaymentTotal;


		public bool IsChanged { get; set; }


		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			this.OnAddRowFromPopup?.Invoke(column);
		});
		public Action<String> OnAddRowFromPopup;



		public String OrderItemInfoText
		{
			get
			{
				var xdoc = XDocument.Parse("<root>" + OrderItemInfo + "</root>");

				var items = xdoc.Elements().Single().Elements().Select(q =>
				new
				{
					MedicalServiceName = (string)q.Element("MedicalServiceName"),
					Qty = (decimal?)q.Element("Qty"),
					Price = (decimal?)q.Element("Price"),
					Tax = (decimal?)q.Element("Tax"),
					Description = (string)q.Element("Description"),
				}).OrderBy(q => q.MedicalServiceName).ToArray();

				var ret = string.Join("\n", items.Select(q => q.MedicalServiceName + "*" + q.Qty.Format("#,##0.####") + ", " + ((q.Price ?? 0) + (q.Tax ?? 0)).FormatMoney()).ToArray());
				return ret;
			}
		}
	}
}
