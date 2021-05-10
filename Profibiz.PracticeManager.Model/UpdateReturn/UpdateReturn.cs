using Profibiz.PracticeManager.SharedCode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	public class UpdateReturn
	{
		public UpdateReturn() { }

		public Boolean IsSuccess { get; set; }
		public String ResponseJson { get; set; }

		public Boolean IsUserError { get; set; }
		public String UserErrorText { get; set; }
		public UserErrorCodes UserErrorCode { get; set; }
		public String UserErrorInfoObjectJson { get; set; }

		public String Message { get; set; }
		public String ExceptionMessage { get; set; }
		public String ExceptionType { get; set; }
		public String StackTrace { get; set; }
		

		public String AllErrorText { get; set; }
	
	}



}
