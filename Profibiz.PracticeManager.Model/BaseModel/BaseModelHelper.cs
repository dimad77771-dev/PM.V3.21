using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	public static class BaseModelHelper
	{
		public static void RaisePropertyChanged(object model, string col)
		{
			var tp = model.GetType();
			var prop = tp.GetMethod("OnPropertyChanged");
			prop.Invoke(model, new object[] { col });
		}
		

	}
}
