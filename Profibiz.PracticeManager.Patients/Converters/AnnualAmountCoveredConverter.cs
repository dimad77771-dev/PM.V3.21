using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.DevAV;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.Patients.Converters
{
    public class AnnualAmountCoveredConverter : IValueConverter
	{
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value == null)
			{
				return "";
			}
			else if (value.Equals(InsuranceCoverageService.NO_LIMIT))
			{
				return "No Limit";
			}
			else
			{
				return ((decimal)value).ToString("c");
			}
		}

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
    }
}
