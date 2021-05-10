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
using System.Windows.Data;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class EnumFunc
	{
		public static List<T> GetValues<T>()
		{
			return Enum.GetValues(typeof(T)).Cast<T>().ToList();
		}
	}


	public class EnumToNameConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null) return "";

			var name = Enum.GetName(value.GetType(), value);
			return name.Replace("_", " ");
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
