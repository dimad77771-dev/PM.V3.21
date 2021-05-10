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
	public class InvoiceItem
	{
		public InvoiceItem()
		{
		}

		public System.Guid RowId { get; set; }
		public System.Guid InvoiceRowId { get; set; }
		public Nullable<System.Guid> AppointmentRowId { get; set; }
		public Nullable<System.Guid> ServcieOrSupplyRowId { get; set; }
		public Nullable<decimal> Units { get; set; }
		public Nullable<decimal> Price { get; set; }
		public Nullable<decimal> Tax { get; set; }
		public string Description { get; set; }
		public DateTime? ItemDate { get; set; }
		public Nullable<System.DateTime> Created { get; set; }
		public string CreatedBy { get; set; }
		public Nullable<System.DateTime> Updated { get; set; }
		public string UpdatedBy { get; set; }

		public virtual Appointment Appointment { get; set; }
		//public virtual Invoice Invoice { get; set; }
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




		public InvoiceItem Clone() => (InvoiceItem)this.MemberwiseClone();


		public static InvoiceItem CreateFromAppointment(Appointment appointment)
		{
			var medicalService = LookupDataProvider.FindMedicalService(appointment.MedicalServicesOrSupplyRowId);
			if (medicalService == null) throw new LogicalException("medicalService is null");
			var serviceProvider = LookupDataProvider.FindServiceProvider(appointment.ServiceProviderRowId);
			if (serviceProvider == null) throw new LogicalException("serviceProvider is null");

			var description =
				"Appointment: " + appointment.StartEndTimeString + "\n" +
				"Service: " + appointment.MedicalServiceName + "\n" +
				"Provider: " + appointment.ServiceProviderName;

			var invoiceItem = new InvoiceItem
			{
				RowId = Guid.NewGuid(),
				AppointmentRowId = appointment.RowId,
				IsNew = true,
				ServcieOrSupplyRowId = appointment.MedicalServicesOrSupplyRowId,
				Description = description,
				ItemDate = appointment.Start.Date,
			};
			invoiceItem.Units = Math.Round(((Decimal)(appointment.Finish - appointment.Start).TotalMinutes / 60), 2);

			var serviceProviderService = serviceProvider.ServiceProviderServices.SingleOrDefault(q => q.MedicalServiceOrSupplyRowId == medicalService.RowId);

			decimal rate, taxRate;
			if (serviceProviderService != null)
			{
				if (serviceProviderService.ChargeModel == TypeHelper.ChargeModel.PerHour)
				{
					rate = serviceProviderService.HourlyRate;
					taxRate = serviceProviderService.HourlyRateTaxRate;
				}
				else if (serviceProviderService.ChargeModel == TypeHelper.ChargeModel.PerVisit)
				{
					invoiceItem.Units = 1;
					rate = serviceProviderService.BasePrice;
					taxRate = serviceProviderService.BasePriceTaxRate;
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

			invoiceItem.Price = rate;
			invoiceItem.Tax = rate * taxRate / 100;

			//invoiceItem.Price = Math.Round((decimal)invoiceItem.Price, 2);

			return invoiceItem;
		}

	}
}
