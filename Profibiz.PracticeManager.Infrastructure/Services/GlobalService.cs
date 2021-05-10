using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public interface IGlobalService
	{
		void Start();
	}


	public static class GlobalServiceHelper
	{
		public static void Start(GlobalServiceCodes code)
		{
			var service = ServiceLocator.Current.GetInstance<object>(code.ToString());
			(service as IGlobalService).Start();
		}
	}


	public enum GlobalServiceCodes
	{
		CalendarEventsRemindersService,
	}
}

