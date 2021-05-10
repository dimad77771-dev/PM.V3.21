using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using System.Collections.ObjectModel;
using System.Windows.Data;
using System.Windows.Markup;
using System.Globalization;
using PropertyChanged;


namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class InvoiceListBackgroundColorModel
	{
		public string Name { get; set; }
		public string Code { get; set; }
		public String BackgroundColor { get; set; }
		public String ForegroundColor { get; set; }
		public Boolean IsSelected { get; set; }

		public DelegateCommand SelectUnselectCommand => new DelegateCommand(() =>
		{
			IsSelected = !IsSelected;
		});
	}

	public static class InvoiceListBackgroundColorInfo
	{
		public static string AMinus => "<0";
		public static string A0 => "0";
		public static string A30 => "30";
		public static string A60 => "60";
		public static string A90 => "90";
		public static string A90plus => "90+";

		public static InvoiceListBackgroundColorModel[] All => new[]
		{
			new InvoiceListBackgroundColorModel { Code = AMinus,	Name = "Sent But Not Approved",		BackgroundColor = "#FF0000", ForegroundColor = "Black" },
			new InvoiceListBackgroundColorModel { Code = A0,		Name = "Approved But Not Paid",		BackgroundColor = "#FF6A00", ForegroundColor = "Black" },
		};

		public static ObservableCollection<InvoiceListBackgroundColorModel> GetAll()
		{
			return All.ToObservableCollection();
		}

		public static ObservableCollection<InvoiceListBackgroundColorModel> RibbonInvoiceListBackgroundColorListItems => GetAll();
		public static Int32 RibbonSpInvoiceListBackgroundColorListColumnCount => 2;
	}

	public class InvoiceListBackgroundColorToColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = InvoiceListBackgroundColorInfo.All.FirstOrDefault(q => q.Code == code)?.BackgroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class InvoiceListBackgroundColorToForegrondColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = InvoiceListBackgroundColorInfo.All.FirstOrDefault(q => q.Code == code)?.ForegroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

}
