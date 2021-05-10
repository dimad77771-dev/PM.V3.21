using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ColorToStringConverter : IValueConverter
	{
		ColorConverter colorConverter;
		

		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (colorConverter == null) colorConverter = new ColorConverter();
			if (value == null) return null;
			var ret = colorConverter.ConvertFromInvariantString(value.ToString());
			return ret;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (colorConverter == null) colorConverter = new ColorConverter();
			var ret = colorConverter.ConvertToInvariantString(value);
			return value;
		}
	}
}
