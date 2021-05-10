﻿using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ProvinceToCityConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var province = (string)value;
			var val = LookupDataProvider.Province2Cities(province);
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
