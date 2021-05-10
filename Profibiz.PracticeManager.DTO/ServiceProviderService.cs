using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
    public class ServiceProviderService
	{
		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public Guid MedicalServiceOrSupplyRowId { get; set; }
		public Decimal BasePrice { get; set; }
		public Decimal BasePriceTaxRate { get; set; }
		public Decimal HourlyRate { get; set; }
		public Decimal HourlyRateTaxRate { get; set; }
		public string ChargeModel { get; set; }
		public string FoоterTeхt { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }
		public decimal ChargeoutBasePrice { get; set; }
		public decimal ChargeoutBasePriceTaxRate { get; set; }
		public decimal ChargeoutHourlyRate { get; set; }
		public decimal ChargeoutHourlyRateTaxRate { get; set; }
	}
}
