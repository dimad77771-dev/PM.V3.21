using Profibiz.PracticeManager.Infrastructure;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class LoginInout
	{
		public Guid RowId { get; set; }
		public Guid ServiceProviderRowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime? Finish { get; set; }
		public string Description { get; set; }

		public String ServiceProviderName => LookupDataProvider.ServiceProvider2Name(ServiceProviderRowId);
		public DateTime Date => Start.Date;

		public DateTime? StartDate { get; set; }
		public DateTime? StartTime { get; set; }
		public DateTime? FinishTime { get; set; }

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";

		public void DateTimePropsBuild()
		{
			StartDate = (Start == default(DateTime) ? (DateTime?)null : Start.Date);
			StartTime = (Start == default(DateTime) ? (DateTime?)null : Start);
			FinishTime = (Finish == default(DateTime) ? (DateTime?)null : Finish);
		}

		public List<string> DateTimePropsUpdate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(StartDate, "Date", errors);
			ValidateHelper.Empty(StartTime, "Start Time", errors);
			if (errors.Count > 0)
			{
				return errors;
			}

			var startTimeOfDay = ((DateTime)StartTime).TimeOfDay;
			Start = ((DateTime)StartDate) + startTimeOfDay;

			if (FinishTime != null)
			{ 
				var finishTimeOfDay = ((DateTime)FinishTime).TimeOfDay;
				if (startTimeOfDay >= finishTimeOfDay)
				{
					errors.Add("\"Start Time\" must be less then \"Finish Time\"");
				}
				if (errors.Count > 0)
				{
					return errors;
				}
				Finish = ((DateTime)StartDate) + finishTimeOfDay;
			}
			else
			{
				Finish = null;
			}

			return errors;
		}
	}
}
