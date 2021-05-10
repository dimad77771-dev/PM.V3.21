using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class CalendarEvent
	{
		public CalendarEvent()
		{
		}

		public Guid RowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public string Notes { get; set; }
		public Guid? PatientRowId { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public bool AllDay { get; set; }
		public int RemainderInMinutes { get; set; }
		public bool IsDisabled { get; set; }
		public DateTime? SnoozedTo { get; set; }
		public string RefNumber { get; set; }
		public string RefStatus { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public bool Completed { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }
		public bool IsVacation { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
		public string PatientFullName { get; set; }
		public string ServiceProviderFullName { get; set; }


		public virtual Patient Patient { get; set; }

		public bool UpdateFlagRemainderFieldsOnly { get; set; }

		public String BeforeStartString => DateTimeHelper.FormatTimeIntervalUserFriendly(DateTime.Now, Start);
		public String CommonFullName => (PatientRowId != null ? PatientFullName : "(*)" + ServiceProviderFullName);

		public DateTime? StartDate { get; set; }
		public DateTime? FinishDate { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? FinishTime { get; set; }


		public void DateTimePropsBuild()
		{
			StartDate = (Start == default(DateTime) ? (DateTime?)null : Start.Date);
			FinishDate = (Start == default(DateTime) ? (DateTime?)null : Finish.Date.AddDays(-1));
			StartTime = (Start == default(DateTime) ? (DateTime?)null : Start);
			FinishTime = (Finish == default(DateTime) ? (DateTime?)null : Finish);
		}

		public List<string> DateTimePropsUpdate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(StartDate, "Start Date", errors);
			if (IsVacation)
			{
				ValidateHelper.Empty(FinishTime, "Finish Date", errors);
			}
			else
			{
				ValidateHelper.Empty(StartTime, "Start Time", errors);
				ValidateHelper.Empty(FinishTime, "Finish Time", errors);
			}
            if (errors.Count > 0)
            {
                return errors;
            }

			if (IsVacation)
			{
				Start = ((DateTime)StartDate);
				Finish = ((DateTime)FinishDate).AddDays(1);
				if (StartDate > FinishDate)
				{
					errors.Add("\"Start Date\" must be less or equal then \"Finish Date\"");
				}
			}
			else if (AllDay)
			{
				Start = ((DateTime)StartDate);
				Finish = Start.AddDays(1);
			}
			else
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

				Start = ((DateTime)StartDate) + startTimeOfDay;
				Finish = ((DateTime)StartDate) + finishTimeOfDay;
			}

            return errors;
		}

        public String StartEndDateString
        {
            get
            {
                return AllDay ? "All Day Event" : Start.FormatShortDate() + " " + Start.FormatHHMM() + " - " + Finish.FormatHHMM(); 
            }
        }

		public String StartEndTimeString
		{
			get
			{
				return AllDay ? "All Day Event" : Start.FormatHHMM() + " - " + Finish.FormatHHMM();
			}
		}

		public String StartEndDateVacationString
		{
			get
			{
				return Start.FormatShortDate() + " - " + Finish.FormatShortDate();
			}
		}


		public String DateString
		{
			get
			{
				return Start.FormatShortDate();
			}
		}

		public Double DurationInMinutes => (Finish - Start).TotalMinutes;

		public virtual bool IsShowIsVacation => IsVacation;

		public bool IsChanged { get; set; }
        public bool IsNew { get; set; }


		public DelegateCommand OpenDetailCommand => new DelegateCommand(() =>
		{
			OnOpenDetail?.Invoke();
		});
		public Action OnOpenDetail;




		public const int REMAINDER_NONE = -1;
	}
}
