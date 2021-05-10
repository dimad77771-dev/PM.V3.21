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
	public class EventCalendarRemainderConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (int?)value;
			var ret = EventCalendarRemainderInfo.All.FirstOrDefault(q => q.Value == val)?.Name;
			return ret;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (string)value;
			var ret = EventCalendarRemainderInfo.All.FirstOrDefault(q => q.Name == val)?.Value;
			return ret;
		}
	}
}
