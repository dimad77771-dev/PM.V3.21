using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class Appointment
	{
		public Appointment()
		{
		}

		public Guid RowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public string Notes { get; set; }
		public Guid PatientRowId { get; set; }
		public string Description { get; set; }
		public Guid? AppointmentClinicalNoteRowId { get; set; }
		public Guid? AppointmentTreatmentNoteRowId { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }
		public string Type { get; set; }
		public string RefNumber { get; set; }
		public string RefStatus { get; set; }
		public Guid AppointmentBookRowId { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
		public bool Completed { get; set; }
		public Guid? MedicalServicesOrSupplyRowId { get; set; }
		public Guid? InsuranceCoverageRowId { get; set; }
		public string PatientFullName { get; set; }
		public decimal PatientRate { get; set; }
		public bool HasNoCoverage { get; set; }
		public Guid? InvoiceRowId { get; set; }
		public string InvoiceNumber { get; set; }
		public bool IsRemainderEmail { get; set; }
		public bool IsRemainderSms { get; set; }
		public bool IsEmailWhenRegistered { get; set; }
		public bool IsSmsWhenRegistered { get; set; }


		public Guid? InsuranceProviderRowId { get; set; }
		public string PolicyNumber { get; set; }
		public Guid? PolicyOwnerRowId { get; set; }
		public string PolicyOwnerFullName { get; set; }
		public DateTime PolicyCoverageStartDate { get; set; }
		public DateTime PolicyCoverageValidUntil { get; set; }
		public String PolicyFullName => InsuranceCoverage.GetPolicyFullName(InsuranceProviderRowId, PolicyOwnerFullName, PolicyNumber, PolicyCoverageStartDate, PolicyCoverageValidUntil);


		public virtual Patient Patient { get; set; }


		public DateTime? StartDate { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? FinishTime { get; set; }

		public Boolean IsExistInsuranceCoverageInfo { get; set; }
		public Decimal InsuranceCoverageInfoTotalAmount { get; set; }
		public Decimal InsuranceCoverageInfoApproveAmount { get; set; }
		public Decimal InsuranceCoverageInfoRemaindAmount => InsuranceCoverageInfoTotalAmount - InsuranceCoverageInfoApproveAmount;
		public String InsuranceCoverageInfoRemaindAmountForegroundColor => InsuranceCoverageInfoRemaindAmount >= 0 ? "Green" : "Red";


		public ObservableCollection<MultiDateInfo> MultiDates { get; set; } = new ObservableCollection<MultiDateInfo>();
		public String MultiDatesText { get; set; }
		public void UpdateMultiDatesText() => MultiDatesText = string.Join("; ", MultiDates.OrderBy(q => q.Date).Select(q => q.Date.ToString("d")).ToArray());
		public bool HasMultiDates => MultiDates.Any();
		public bool IsAutoAllocate { get; set; }
		public bool IsEnabledStartFinishTime => !IsAutoAllocate;

		public InvoiceItem InvoiceItem { get; set; }
		public AppointmentRemainder[] AppointmentRemainders { get; set; } = new AppointmentRemainder[0];
		public ObservableCollection<FormDocument> FormDocuments { get; set; } = new FormDocument[0].ToObservableCollection();

		public class MultiDateInfo
		{
			public DateTime Date { get; set; }
			public DateTime Start { get; set; }
			public DateTime Finish { get; set; }
			public String ErrorInfo { get; set; }
		}

		public void UpdateMultiDates()
		{
			if (!IsAutoAllocate)
			{
				MultiDates.ForEach(q =>
				{
					q.Start = q.Date + StartTime.Value.TimeOfDay;
					q.Finish = q.Date + FinishTime.Value.TimeOfDay;
				});
			}
		}

		public void DateTimePropsBuild()
		{
			StartDate = (Start == default(DateTime) ? (DateTime?)null : Start.Date);
			StartTime = (Start == default(DateTime) ? (DateTime?)null : Start);
			FinishTime = (Finish == default(DateTime) ? (DateTime?)null : Finish);
		}

		public List<string> DateTimePropsUpdate()
		{
			List<string> errors = new List<string>();

			if (!HasMultiDates)
			{
				ValidateHelper.Empty(StartDate, "Start Date", errors);
			}
			if (IsEnabledStartFinishTime)
			{
				ValidateHelper.Empty(StartTime, "Start Time", errors);
				ValidateHelper.Empty(FinishTime, "Finish Time", errors);
			}

            if (errors.Count > 0)
            {
                return errors;
            }

			if (IsEnabledStartFinishTime)
			{
				var startTimeOfDay = ((DateTime)StartTime).TimeOfDay;
				var finishTimeOfDay = ((DateTime)FinishTime).TimeOfDay;
				if (startTimeOfDay >= finishTimeOfDay)
				{
					errors.Add("\"Start Time\" must be less then \"Finish Time\"");
				}

				if (errors.Count > 0)
				{
					return errors;
				}

				if (!HasMultiDates)
				{
					Start = ((DateTime)StartDate) + startTimeOfDay;
					Finish = ((DateTime)StartDate) + finishTimeOfDay;
				}
			}


			return errors;
		}

        public String StartEndDateString
        {
            get
            {
                return Start.FormatShortDate() + " " + Start.FormatHHMM() + " - " + Finish.FormatHHMM(); 
            }
        }

		public String StartEndTimeString
		{
			get
			{
				return Start.FormatHHMM() + " - " + Finish.FormatHHMM();
			}
		}

		public Double DurationInMinutes => (Finish - Start).TotalMinutes;

		

		public String AppointmentBookName => LookupDataProvider.AppointmentBook2Name(AppointmentBookRowId);
        public String ServiceProviderName => LookupDataProvider.ServiceProvider2Name(ServiceProviderRowId);
        public String MedicalServiceName => LookupDataProvider.MedicalService2Name(MedicalServicesOrSupplyRowId);
		public String MedicalServiceNameWithPrintLabel => LookupDataProvider.MedicalService2FullNameWithPrintLabel(MedicalServicesOrSupplyRowId);
		public Boolean AppointmentTreatmentNoteExists => AppointmentTreatmentNoteRowId != null;


		public bool InInvoice => (InvoiceRowId != null);

        public bool IsChanged { get; set; }
        public bool IsNew { get; set; }
        public bool IsSelected { get; set; }
        public bool? IsGroupSelected { get; set; }
        public bool IsVisibleSelected => !InInvoice;
		public bool IsVisibleGroupSelected { get; set; } = true;

		public ChargeoutItem BuildingChargeoutItem { get; set; }

		public DelegateCommand OpenInvoiceDetailCommand => new DelegateCommand(() =>
		{
			this.OnOpenInvoiceDetail?.Invoke();
		});
		public Action OnOpenInvoiceDetail;

		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;

		public class ButtonAppointmentForm
		{
			public FormDocument FormDocument { get; set; }
			public String ButtonText { get; set; }
			public Color ButtonColor { get; set; }
			public String FontStyle { get; set; }
			public String FontWeight { get; set; }
			public DelegateCommand OnClickCommand { get; set; }
		}

		public Action<Appointment, FormDocument> OnClickButtonAppointmentForm;
		public ButtonAppointmentForm[] ButtonsAppointmentForm
		{
			get
			{
				if (this.RowId == default(Guid))
				{
					var b = 100;
				}

				var buttons = new List<ButtonAppointmentForm>();

				foreach(var formDocument in FormDocuments)
				{
					var filename = formDocument.TemplateName;
					buttons.Add(new ButtonAppointmentForm
					{
						ButtonText = filename,
						FontStyle = "Regular",
						FontWeight = "Normal",
						OnClickCommand = new DelegateCommand(() => OnClickButtonAppointmentForm(this, formDocument)),
					});
				}

				buttons.Add(new ButtonAppointmentForm
				{
					ButtonText = " Add new form... ",
					FontStyle = "Italic",
					FontWeight = "Bold",
					OnClickCommand = new DelegateCommand(() => OnClickButtonAppointmentForm(this, null)),
				});

				//AppointmentFormDocumentMappings
				return buttons.ToArray();
			}
		}
	}


	public static class AppointmentExtensions
	{
		public static void SetOnClickButtonAppointmentForm(this IEnumerable<Appointment> appointments, Action<Appointment, FormDocument> onClickButtonAppointmentForm)
		{
			appointments.ForEach(q => q.OnClickButtonAppointmentForm = onClickButtonAppointmentForm);
		}
		public static void SetOnClickButtonAppointmentForm(this Appointment appointments, Action<Appointment, FormDocument> onClickButtonAppointmentForm)
		{
			appointments.OnClickButtonAppointmentForm = onClickButtonAppointmentForm;
		}
	}

}
