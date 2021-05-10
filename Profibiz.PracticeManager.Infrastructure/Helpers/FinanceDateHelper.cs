using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class FinanceDateHelper
	{
		public static List<FinanceDateClass> AllFinanceDateItems { get; set; } = BuildAllFinanceDateItems();

		public static List<FinanceDateClass> BuildAllFinanceDateItems()
		{
			return EnumFunc.GetValues<FinanceDateEnum>().Select(q => new FinanceDateClass(q)).ToList();
		}


	}

	public class FinanceDateClass
	{
		public FinanceDateEnum Type { get; set; }
		public String Name { get; set; }

		public FinanceDateClass(FinanceDateEnum type)
		{
			Type = type;
			Name = Enum.GetName(typeof(FinanceDateEnum), type).Replace("_", " ").PadRight(50 - Enum.GetName(typeof(FinanceDateEnum), type).Length, ' ');
		}

		public ReturnGet Get()
		{
			DateTime from, to;

			var today = DateTime.Today;
			if (Type == FinanceDateEnum.Today)
			{
				from = today;
				to = today;
			}
			else if (Type == FinanceDateEnum.This_Week)
			{
				from = DateTimeHelper.FirstDayCurrentWeek();
				to = from.AddDays(6);
			}
			else if (Type == FinanceDateEnum.This_Week_To_Date)
			{
				from = DateTimeHelper.FirstDayCurrentWeek();
				to = today;
			}
			else if (Type == FinanceDateEnum.This_Month)
			{
				from = DateTimeHelper.FirstDayCurrentMonth();
				to = from.AddMonths(1).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.This_Month_To_Date)
			{
				from = DateTimeHelper.FirstDayCurrentMonth();
				to = today;
			}
			else if (Type == FinanceDateEnum.This_Quarter)
			{
				from = DateTimeHelper.FirstDayCurrentQuarter();
				to = from.AddMonths(3).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.This_Quarter_To_Date)
			{
				from = DateTimeHelper.FirstDayCurrentQuarter();
				to = today;
			}
			else if (Type == FinanceDateEnum.This_Year)
			{
				from = DateTimeHelper.FirstDayCurrentYear();
				to = from.AddYears(1).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.This_Year_To_Date)
			{
				from = DateTimeHelper.FirstDayCurrentYear();
				to = today;
			}
			else if (Type == FinanceDateEnum.Yesterday)
			{
				from = today.AddDays(-1);
				to = today.AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Last_Week)
			{
				from = DateTimeHelper.FirstDayWeek(today.AddDays(-7));
				to = from.AddDays(6);
			}
			else if (Type == FinanceDateEnum.Last_Week_To_Date)
			{
				from = DateTimeHelper.FirstDayWeek(today.AddDays(-7));
				to = today.AddDays(-7);
			}
			else if (Type == FinanceDateEnum.Last_Month)
			{
				from = DateTimeHelper.FirstDayMonth(today.AddMonths(-1));
				to = from.AddMonths(1).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Last_Month_To_Date)
			{
				from = DateTimeHelper.FirstDayMonth(today.AddMonths(-1));
				to = from.AddDays(today.Day - 1);
				var lastDayOfMonth = from.AddMonths(1).AddDays(-1);
				if (to > lastDayOfMonth) to = lastDayOfMonth;
			}
			else if (Type == FinanceDateEnum.Last_Quarter)
			{
				from = DateTimeHelper.FirstDayQuarter(today.AddMonths(-3));
				to = from.AddMonths(3).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Last_Quarter_To_Date)
			{
				from = DateTimeHelper.FirstDayQuarter(today.AddMonths(-3));
				var days = (today - DateTimeHelper.FirstDayCurrentQuarter()).TotalDays;
				to = from.AddDays(days);
				var lastDayOfQuarter = from.AddMonths(3).AddDays(-1);
				if (to > lastDayOfQuarter) to = lastDayOfQuarter;
			}
			else if (Type == FinanceDateEnum.Last_Year)
			{
				from = DateTimeHelper.FirstDayYear(today.AddYears(-1));
				to = from.AddYears(1).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Last_Year_To_Date)
			{
				from = DateTimeHelper.FirstDayYear(today.AddYears(-1));
				to = today.AddYears(-1);
			}
			else if (Type == FinanceDateEnum.Next_Week)
			{
				from = DateTimeHelper.FirstDayWeek(today.AddDays(7));
				to = from.AddDays(6);
			}
			else if (Type == FinanceDateEnum.Next_Month)
			{
				from = DateTimeHelper.FirstDayMonth(today.AddMonths(1));
				to = from.AddMonths(1).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Next_Quarter)
			{
				from = DateTimeHelper.FirstDayQuarter(today.AddMonths(3));
				to = from.AddMonths(3).AddDays(-1);
			}
			else if (Type == FinanceDateEnum.Next_Year)
			{
				from = DateTimeHelper.FirstDayYear(today.AddYears(1));
				to = from.AddYears(1).AddDays(-1);
			}
			else throw new ArgumentException();

			return new ReturnGet
			{
				From = from,
				To = to,
			};
		}
		public class ReturnGet
		{
			public DateTime From { get; set; }
			public DateTime To { get; set; }
		}

	}


	public enum FinanceDateEnum
	{
		Today,
		This_Week,
		This_Week_To_Date,
		This_Month,
		This_Month_To_Date,
		This_Quarter,
		This_Quarter_To_Date,
		This_Year,
		This_Year_To_Date,
		Yesterday,
		Last_Week,
		Last_Week_To_Date,
		Last_Month,
		Last_Month_To_Date,
		Last_Quarter,
		Last_Quarter_To_Date,
		Last_Year,
		Last_Year_To_Date,
		Next_Week,
		Next_Month,
		Next_Quarter,
		Next_Year,
	}

	public class FinanceDateFieldsBarSubItem : BarSubItem
	{
		public FinanceDateFieldsBarSubItem()
		{
			this.Content = "Set Ranges";
			var a32 = (DXImageInfo)new DXImageConverter().ConvertFromString("PreviewChart_32x32.png");
			var a16 = (DXImageInfo)new DXImageConverter().ConvertFromString("PreviewChart_16x16.png");
			this.LargeGlyph = new BitmapImage(a32.MakeUri());
			this.Glyph = new BitmapImage(a16.MakeUri());
			this.GetItemData += OnGetItemData;
		}

		void OnGetItemData(object sender, EventArgs e)
		{
			UpdateItems();
		}

		void UpdateItems()
		{
			foreach (var item in FinanceDateHelper.AllFinanceDateItems)
			{
				AppendItem(item);
			}
		}

		void AppendItem(FinanceDateClass arg)
		{
			BarButtonItem item = new BarButtonItem();
			item.Content = arg.Name;
			item.CommandParameter = arg;
			var binding = new Binding();
			binding.Path = new PropertyPath("FinanceDateApplyCommand");
			BindingOperations.SetBinding(item, BarButtonItem.CommandProperty, binding);
			ItemLinks.Add(item);
		}

	}
}
