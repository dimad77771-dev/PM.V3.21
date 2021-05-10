using DevExpress.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class PropertyHelper
	{
		public static string GetPropertyName<T>(Expression<Func<T>> expression)
		{
			return BindableBase.GetPropertyName(expression);
		}
	}
}
