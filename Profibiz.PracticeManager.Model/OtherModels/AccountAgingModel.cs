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
	public class AccountAgingModel
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

	public static class AccountAgingInfo
	{
		public static string AMinus => "<0";
		public static string A0 => "0";
		public static string A30 => "30";
		public static string A60 => "60";
		public static string A90 => "90";
		public static string A90plus => "90+";

		public static AccountAgingModel[] All => new[]
		{
			new AccountAgingModel { Code = AMinus,  Name = "         Refund       ", BackgroundColor = "Green", ForegroundColor = "White" },
			new AccountAgingModel { Code = A0,		Name = "         Paid         ", BackgroundColor = "White", ForegroundColor = "Black" },
			new AccountAgingModel { Code = A30,		Name = "         0-30         ", BackgroundColor = "#FCD5B4", ForegroundColor = "Black" },
		    new AccountAgingModel { Code = A60,		Name = "         30-60        ", BackgroundColor = "#CCC0DA", ForegroundColor = "Black" },
		    new AccountAgingModel { Code = A90,		Name = "         60-90        ", BackgroundColor = "#DA9694", ForegroundColor = "Black" },
			new AccountAgingModel { Code = A90plus, Name = "         90+          ", BackgroundColor = "#FF7F7F", ForegroundColor = "Black" },
		};

		public static ObservableCollection<AccountAgingModel> GetAll()
		{
			return All.ToObservableCollection();
		}

		public static ObservableCollection<AccountAgingModel> RibbonSpAccountAgingListItems => GetAll();
		public static Int32 RibbonSpAccountAgingListColumnCount => 2;
	}

	public class AccountAgingToColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = AccountAgingInfo.All.FirstOrDefault(q => q.Code == code)?.BackgroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

	public class AccountAgingToForegrondColorConverterConverter : IValueConverter
	{
		object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var code = (string)value ?? "";
			var val = AccountAgingInfo.All.FirstOrDefault(q => q.Code == code)?.ForegroundColor;
			return val;
		}
		object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotSupportedException();
		}
	}

}
