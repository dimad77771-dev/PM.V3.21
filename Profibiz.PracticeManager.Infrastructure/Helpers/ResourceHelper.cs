using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ResourceHelper
	{
		public static T FindResource<T>(string arg)
		{
			return (T)(Application.Current.FindResource(arg));
		}

		public static T ConvertByResource<T>(object value, string converterName)
		{
			var converter = (IValueConverter)(Application.Current.FindResource(converterName));
			var ret = (T)(converter.Convert(value, typeof(T), null, CultureInfo.CurrentCulture));
			return ret;
		}

	}
}
