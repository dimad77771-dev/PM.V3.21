using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class MedicalServicesOrSupply
    {
        public Guid RowId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string ItemType { get; set; }
		public string Notes { get; set; }
		public decimal? UnitPrice { get; set; }
		public decimal? TaxRate { get; set; }
		public string Model { get; set; }
		public string Supplier { get; set; }
		public string Size { get; set; }
		public Guid CategoryRowId { get; set; }
		public string Factory { get; set; }
		public string Article { get; set; }
		public string MeasurementUnit { get; set; }
		public string PrintLabel { get; set; }
		public bool UsePrintLabel { get; set; }
		public string TemplateFolder { get; set; }
		public int? Qty { get; set; }

		public string FullName => Name;
		public string FullNameWithPrintLabel => (UsePrintLabel && !string.IsNullOrEmpty(PrintLabel) ? PrintLabel : Name);

		public string Rowtype9 => "Medical" + ItemType;
		public string CategoryName => LookupDataProvider.Category2Name(CategoryRowId);

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public bool IsServiceOrSuppy => (ItemType == TypeHelper.MedicalItemType.Service || ItemType == TypeHelper.MedicalItemType.Supply);
		public bool IsReadOnlyPrice => (ItemType != TypeHelper.MedicalItemType.Supply);
		public bool IsReadOnlyModelSupplierSize => (ItemType != TypeHelper.MedicalItemType.Supply);


		
		public string FullNameWithPriceInfo => FullName + PriceInfoServiceProvider;
		public string PriceInfoServiceProvider { get; set; }
		public void SetPriceInfoFromServiceProvider(ServiceProviderService serviceProviderService)
		{
			if (serviceProviderService == null)
			{
				PriceInfoServiceProvider = "";
				return;
			}

			PriceInfoServiceProvider = "";
			if (serviceProviderService.BasePrice > 0)
			{
				PriceInfoServiceProvider += " - Base: " + serviceProviderService.BasePrice.FormatMoney();
			}
			if (serviceProviderService.HourlyRate > 0)
			{
				PriceInfoServiceProvider += " - Hourly: " + serviceProviderService.HourlyRate.FormatMoney();
			}
		}
	}
}
