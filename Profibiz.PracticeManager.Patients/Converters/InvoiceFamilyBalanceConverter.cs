using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.DevAV;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Profibiz.PracticeManager.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using Profibiz.PracticeManager.Model;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.Converters
{
    public class InvoiceFamilyBalanceConverter : IValueConverter
	{
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var val = (decimal?)value;
			var backgroundColor = "";
			var tooltip = "";
			if (val > 0)
			{
				backgroundColor = "Red";
				tooltip = "Balance: " + val.FormatMoney();
			}
			else if (val == 0)
			{
				backgroundColor = "Green";
				tooltip = "Balance: " + val.FormatMoney();
			}
			else
			{
				backgroundColor = "White";
				tooltip = "No Invoices";
			}

			if (parameter.Equals("ToolTip"))
			{
				return tooltip;
			}
			else if (parameter.Equals("BackgroundColor"))
			{
				return backgroundColor;
			}
			else throw new ArgumentException();
		}

		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
    }
}
