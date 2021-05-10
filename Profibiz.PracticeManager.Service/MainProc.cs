using Nito.AsyncEx;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Service
{
	public class MainProc
	{
		static void Main(string[] args)
		{
			//sc create PracticeManagerService binpath= "E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Service\bin\Debug\Profibiz.PracticeManager.Service.exe --service"
			//sc delete PracticeManagerService

			NLog.Init();
			if (args != null && args.Length == 1 && args[0] == "--service")
			{
				NLog.Debug("Start WindowsServiceFunc.Run");
				WindowsServiceFunc.Run();
			}
			else
			{
				//консольный вариант
				var mainCycle = new MainCycle();
				AsyncContext.Run(mainCycle.Run);
			}
		}
	}
}
