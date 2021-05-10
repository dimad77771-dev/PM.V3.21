using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using Profibiz.PracticeManager.Model;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ChargeoutPaidProblemImageConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			try
			{
				if (parameter == null) return null;
				var chargeoutType = (Chargeout.PaidProblemEnum)(value);

				string icon, tooltip;
				bool iconVisible;
				if (chargeoutType == Chargeout.PaidProblemEnum.NotPaid)
				{
					icon = "pack://application:,,,/Profibiz.PracticeManager.Infrastructure;component/Resources/icon-PaidProblem-ApprovedButNotPaidToUs-16.png";
					tooltip = "Not Paid To Us";
					iconVisible = true;
				}
				else if (chargeoutType == Chargeout.PaidProblemEnum.None)
				{
					icon = "";
					tooltip = "";
					iconVisible = false;
				}
				else throw new ArgumentException();


				if (parameter.Equals("Icon"))
				{
					return icon != "" ? new BitmapImage(new Uri(icon)) : null;
				}
				if (parameter.Equals("IconVisibility"))
				{
					return iconVisible ? Visibility.Visible : Visibility.Collapsed;
				}
				if (parameter.Equals("ToolTip"))
				{
					return tooltip;
				}
				else throw new ArgumentException();
			}
			catch (Exception ex)
			{
				throw new AggregateException(ex);
			}
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}
}
