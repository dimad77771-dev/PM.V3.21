using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Service
{
	public class MainCycle
	{
		public bool InWindowsService { get; set; }

		public async Task Run()
		{
			try
			{
				NLog.Debug("MainCycle--1");
				var seconds = Int32.Parse(ConfigurationManager.AppSettings["Service.CycleSeconds"]);
				NLog.Debug("MainCycle--2");
				var cyclenum = 0;
				NLog.Debug("MainCycle--3");

				while (true)
				{
					cyclenum++;
					var str = "Cycle start. Count=" + cyclenum;
					Console.WriteLine(str);
					NLog.Debug(str);

					try
					{
						await OneCycle();
					}
					catch (Exception ex)
					{
						NLog.Error(ex.ToString());
						Console.WriteLine(ex.ToString());
					}

					Thread.Sleep(seconds * 1000);
				}
			}
			catch (Exception ex)
			{
				NLog.Error(ex.ToString());
				throw new AggregateException(ex);
			}
		}

		public async Task OneCycle()
		{
			new ServiceApi().OneCycle();
		}
	}
}
