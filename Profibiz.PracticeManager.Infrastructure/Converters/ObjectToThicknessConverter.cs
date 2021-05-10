using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ObjectToThicknessConverter : IValueConverter
	{
		ThicknessConverter colorConverter;
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (colorConverter == null) colorConverter = new ThicknessConverter();
			if (value == null) return null;
			var ret = colorConverter.ConvertFromInvariantString(value.ToString());
			return ret;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
			//if (colorConverter == null) colorConverter = new ThicknessConverter();
			//var ret = colorConverter.ConvertToInvariantString(value);
			//return value;
		}
	}
}
