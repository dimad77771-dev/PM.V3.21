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
	public class PatientNoteStatusConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var statusRowId = (Guid?)value;
			var returnType = (string)parameter;

			var row = LookupDataProvider.FindPatientNoteStatus(statusRowId);
			if (row == null)
			{
				if (returnType == "Visibility")
				{
					return "Collapsed";
				}
				else if (returnType == "Background")
				{
					return "#FFFFFF";
				}
				else if (returnType == "Foreground")
				{
					return "#FFFFFF";
				}
				else if (returnType == "Text")
				{
					return "";
				}
			}
			else
			{
				if (returnType == "Visibility")
				{
					return "Visible";
				}
				else if (returnType == "Background")
				{
					return row.BackgroundColor;
				}
				else if (returnType == "Foreground")
				{
					return row.ForegroundColor;
				}
				else if (returnType == "Text")
				{
					return row.Name;
				}
			}
			throw new ArgumentException();
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
