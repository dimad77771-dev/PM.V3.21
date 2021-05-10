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
	public class ChargeoutListBackgroundColorModel
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

	public static class ChargeoutListBackgroundColorInfo
	{
		public static string AMinus => "<0";
		public static string A0 => "0";
		public static string A30 => "30";
		public static string A60 => "60";
		public static string A90 => "90";
		public static string A90plus => "90+";

		public static ChargeoutListBackgroundColorModel[] All => new[]
		{
			new ChargeoutListBackgroundColorModel { Code = AMinus,  Name = "Sent But Not Approved",     BackgroundColor = "#FF0000", ForegroundColor = "Black" },
			new ChargeoutListBackgroundColorModel { Code = A0,      Name = "Approved But Not Paid",     BackgroundColor = "#FF6A00", ForegroundColor = "Black" },
		};

		public static ObservableCollection<ChargeoutListBackgroundColorModel> GetAll()
		{
			return All.ToObservableCollection();
		}

		public static ObservableCollection<ChargeoutListBackgroundColorModel> RibbonChargeoutListBackgroundColorListItems => GetAll();
		public static Int32 RibbonSpChargeoutListBackgroundColorListColumnCount => 2;
	}

	public class ChargeoutListBackgroundColorToColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = ChargeoutListBackgroundColorInfo.All.FirstOrDefault(q => q.Code == code)?.BackgroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class ChargeoutListBackgroundColorToForegrondColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = ChargeoutListBackgroundColorInfo.All.FirstOrDefault(q => q.Code == code)?.ForegroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

}
