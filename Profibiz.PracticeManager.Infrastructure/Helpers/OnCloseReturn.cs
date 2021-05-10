using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public enum OnCloseReturn { Yes, No, Cancel }

	public static class OnCloseReturntExtensions
	{
		public static bool IsCancel(this OnCloseReturn arg)
		{
			return arg == OnCloseReturn.Cancel;
		}
	}
}

