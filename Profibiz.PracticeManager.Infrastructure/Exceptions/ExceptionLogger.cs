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
	public static class ExceptionLogger
	{
		public static void ProcessException(Object e)
		{
			var err = e.ToString() ?? "";
			//EventLog.WriteEntry(".NET Runtime", err.Substring(0, Math.Min(err.Length, 32000)), EventLogEntryType.Error);

			var path = Path.Combine(AssemblyHelper.GetMainPath(), "Exceptions");
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
			var file = Path.Combine(path, DateTime.Now.ToString("yyyyMMddhhmmss") + ".txt");
			File.WriteAllText(file, err);

			NLog.Flush();
			NLog.Error(err);

			UnhandledExceptionProccesing.SendErrorToServer(err);
			//DispatcherUIHelper.Run(async () =>
			//{
			//	await UnhandledExceptionProccesing.SendErrorToServer(err);
			//});
		}
	}
}

