using System;
using System.Collections.Generic;
using System.Text;

namespace Profibiz.PracticeManager.SharedCode
{
	public class UserErrorInformation
	{
		public UserErrorCodes Code { get; set; }
		public string Message { get; set; }
		public string InfoObjectJson { get; set; }
	}

	public enum UserErrorCodes
	{
		NoError,
		InternalError,
		DeleteForeignKey,
		PatientNameDuplicate,
		AppointmentIntersection,
		AppointmentPatientVacation,
		AppointmentServiceProviderVacation,
		InvoiceNumberDuplicate,
		ChargeoutNumberDuplicate,
		CloneInsuranceCoverageDuplicateStartDate,
		EmailError,
	}
}
