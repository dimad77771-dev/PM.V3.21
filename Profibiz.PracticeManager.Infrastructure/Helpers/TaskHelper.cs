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
	public static class TaskHelper
	{
		public static Task<T> IfTrue<T>(Func<Boolean> wh, Task<T> task)
		{
			if (wh())
			{
				return task;
			}
			else
			{
				//return Task.FromResult(default(T));
				return null;
			}
		}

		public static Task WhenAll(params Task[] tasks)
		{
			var tasks2 = tasks.Where(q => q != null).ToArray();
			return Task.WhenAll(tasks2);
		}
	}
}

