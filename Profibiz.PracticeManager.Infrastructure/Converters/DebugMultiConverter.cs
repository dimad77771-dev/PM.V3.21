using System;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
	class DebugMultiConverter : IMultiValueConverter
	{
		public object Convert(object[] values, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			return null;
		}

		public object[] ConvertBack(object value, System.Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
		{
			throw new System.NotImplementedException();
		}
	}

	public class DebugMultiConverterExtension : MarkupExtension
	{
		public override object ProvideValue(IServiceProvider serviceProvider)
		{
			return new DebugMultiConverter();
		}
	}
}
