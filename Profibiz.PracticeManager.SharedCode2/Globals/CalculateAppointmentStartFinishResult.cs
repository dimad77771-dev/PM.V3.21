using System;
using System.Collections.Generic;
using System.Text;

namespace Profibiz.PracticeManager.SharedCode
{
	public class CalculateAppointmentStartFinishResult
	{
		public DateTime Date { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public String ErrorInfo { get; set; }

		public Boolean HasError() => (!string.IsNullOrEmpty(ErrorInfo));

		public static CalculateAppointmentStartFinishResult BuildError(string error)
		{
			return new CalculateAppointmentStartFinishResult { ErrorInfo = error };
		}
	}
}
