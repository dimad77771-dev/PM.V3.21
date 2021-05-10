using DevExpress.Mvvm;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class MessageBoxServiceExtExtensions
	{
		public static MessageResult ShowError(this IMessageBoxService service, string messageBoxText)
		{
			return service.ShowMessage(messageBoxText, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
		}

		public static MessageResult ShowWarning(this IMessageBoxService service, string messageBoxText)
		{
			return service.ShowMessage(messageBoxText, CommonResources.Warning_Caption, MessageButton.OK, MessageIcon.Warning);
		}


		public static MessageResult Confirmation(this IMessageBoxService service, string text)
		{
			var ret = service.ShowMessage(text, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			return ret;
		}


		
	}
}
