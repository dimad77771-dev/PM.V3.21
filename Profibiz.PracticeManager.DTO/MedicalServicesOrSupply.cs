using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
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
	}
}
