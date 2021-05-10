using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Service
{
	public static class WindowsServiceFunc
	{
		public static void Run()
		{
			ServiceBase[] ServicesToRun;
			ServicesToRun = new ServiceBase[]
			{
				new WindowsService()
			};
			ServiceBase.Run(ServicesToRun);
		}
	}
}
