using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class IsLessThenZeroConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return false;
			}
			else if (value is decimal)
			{
				return (decimal)value < 0;
			}
			else if (value is double)
			{
				return (double)value < 0;
			}
			else if (value is int)
			{
				return (int)value < 0;
			}
			throw new NotSupportedException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
