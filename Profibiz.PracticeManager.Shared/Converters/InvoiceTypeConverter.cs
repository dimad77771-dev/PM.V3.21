using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class InvoiceTypeConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var invoiceType = (value as string ?? "");
			if (parameter.Equals("ShortText"))
			{
				return (invoiceType.Length >= 1 ? invoiceType.Substring(0, 1) : "");
			}
			else throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
