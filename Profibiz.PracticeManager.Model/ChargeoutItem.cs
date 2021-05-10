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
	public class ChargeoutItem
	{
		public ChargeoutItem()
		{
		}

		public Guid RowId { get; set; }
		public Guid ChargeoutRowId { get; set; }
		public Guid InvoiceItemRowId { get; set; }
		public decimal? Units { get; set; }
		public decimal? Price { get; set; }
		public decimal? Tax { get; set; }
		public string Description { get; set; }
		public DateTime? ItemDate { get; set; }
		public DateTime? Created { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? Updated { get; set; }
		public string UpdatedBy { get; set; }

		public InvoiceItem InvoiceItem { get; set; }
		//public virtual Appointment Appointment { get; set; }
		//public virtual Chargeout Chargeout { get; set; }
		//public virtual MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }


		public Decimal? LineTotal => Units * (Price + Tax);


		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			this.OnAddRowFromPopup?.Invoke(column);
		});
		public Action<String> OnAddRowFromPopup;


		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;




		public ChargeoutItem Clone() => (ChargeoutItem)this.MemberwiseClone();


		public static ChargeoutItem CreateFromAppointment(Appointment appointment, InvoiceItem invoiceItem)
		{
			var medicalService = LookupDataProvider.FindMedicalService(appointment.MedicalServicesOrSupplyRowId);
			if (medicalService == null) throw new LogicalException("medicalService is null");
			var serviceProvider = LookupDataProvider.FindServiceProvider(appointment.ServiceProviderRowId);
			if (serviceProvider == null) throw new LogicalException("serviceProvider is null");

			var description =
				"Appointment: " + appointment.StartEndTimeString + "\n" +
				"Service: " + appointment.MedicalServiceName + "\n" +
				"Provider: " + appointment.ServiceProviderName;

			var chargeoutItem = new ChargeoutItem
			{
				RowId = Guid.NewGuid(),
				InvoiceItemRowId = invoiceItem.RowId,
				//AppointmentRowId = appointment.RowId,
				IsNew = true,
				//ServcieOrSupplyRowId = appointment.MedicalServicesOrSupplyRowId,
				Description = description,
				ItemDate = appointment.Start.Date,
			};
			chargeoutItem.Units = Math.Round(((Decimal)(appointment.Finish - appointment.Start).TotalMinutes / 60), 2);

			var serviceProviderService = serviceProvider.ServiceProviderServices.SingleOrDefault(q => q.MedicalServiceOrSupplyRowId == medicalService.RowId);

			decimal rate, taxRate;
			if (serviceProviderService != null)
			{
				if (serviceProviderService.ChargeModel == TypeHelper.ChargeModel.PerHour)
				{
					rate = serviceProviderService.ChargeoutHourlyRate;
					taxRate = serviceProviderService.ChargeoutHourlyRateTaxRate;
				}
				else if (serviceProviderService.ChargeModel == TypeHelper.ChargeModel.PerVisit)
				{
					chargeoutItem.Units = 1;
					rate = serviceProviderService.ChargeoutBasePrice;
					taxRate = serviceProviderService.ChargeoutBasePriceTaxRate;
				}
				else throw new LogicalException();
			}
			else if (serviceProvider.Rate != null)
			{
				rate = serviceProvider.Rate ?? 0;
				taxRate = serviceProvider.TaxRate ?? 0;
			}
			else
			{
				rate = medicalService.UnitPrice ?? 0;
				taxRate = medicalService.TaxRate ?? 0;
			}

			chargeoutItem.Price = rate;
			chargeoutItem.Tax = rate * taxRate / 100;

			//chargeoutItem.Price = Math.Round((decimal)chargeoutItem.Price, 2);

			return chargeoutItem;
		}

	}
}
