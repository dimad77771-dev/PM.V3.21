using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
	public static class ExceptionHelper
	{
		public static Boolean Validate(this UpdateReturn arg, IMessageBoxService messageBoxService)
		{
			if (arg.IsSuccess)
			{
				return true;
			}
			else
			{
				ExceptionLogger.ProcessException(arg.AllErrorText);
				var errorText = (arg.IsUserError ? arg.UserErrorText : "Server Error");
				messageBoxService.ShowError(errorText);
				if (RuntimeHelper.Debuger && !arg.IsUserError)
				{
					DispatcherUIHelper.Run(() =>
					{
						throw new AggregateException(new BusinessServiceException(arg));
					});
				}
				return false;
			}
		}

		public static Boolean IsUserErrorWithText(this UpdateReturn arg, string err)
		{
			return (!arg.IsSuccess && arg.IsUserError && arg.UserErrorText == err);
		}
	}



	public class BusinessServiceException : Exception
	{
		public UpdateReturn ExceptionInfo { get; set; }

		public BusinessServiceException(UpdateReturn exceptionInfo) : base(UpdateReturnToString(exceptionInfo))
		{
			ExceptionInfo = exceptionInfo;
		}


		public static string UpdateReturnToString(UpdateReturn exceptionInfo)
		{
			var err =
				"AllErrorText=\n" + exceptionInfo.AllErrorText + "\n\n\n\n" +
				"Message=\n" + exceptionInfo.Message + "\n\n\n\n" +
				"ExceptionType=\n" + exceptionInfo.ExceptionType + "\n\n\n\n" +
				"ExceptionMessage=\n" + exceptionInfo.ExceptionMessage + "\n\n\n\n" +
				"StackTrace=\n" + exceptionInfo.StackTrace + "\n\n\n\n";
			return err;
		}
	}
}
