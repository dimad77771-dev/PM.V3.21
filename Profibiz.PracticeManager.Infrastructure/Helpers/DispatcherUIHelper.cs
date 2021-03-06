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
	public static class DispatcherUIHelper
	{
		public async static void Run(Action action)
		{
			await Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(action));
		}

		public static Task<int> RunTask(Task action)
		{
			var task = new TaskCompletionSource<int>();

			var invoke = Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(async () =>
			{
				try
				{
					await action;
					task.SetResult(0);
				}
				catch(Exception ex)
				{
					task.SetException(ex);
				}
			}));
			invoke.Wait();

			return task.Task;
		}

		public async static void Run2(Task runtask)
		{
			await Dispatcher.CurrentDispatcher.BeginInvoke(DispatcherPriority.ApplicationIdle, new Action(async () =>
			{
				try
				{
					await runtask;
				}
				catch(Exception ex)
				{
					throw new AggregateException(ex);
				}
			}));
		}

	}
}
