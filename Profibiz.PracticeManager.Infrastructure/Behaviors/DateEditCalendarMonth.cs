using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.NavBar;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Editors.Popups.Calendar;
using System.Globalization;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class DateEditSettingsMonth : DateEditSettings
	{

	}


	public class DateEditCalendarMonth : DateEditCalendar
	{
		public DateEditCalendarMonth()
		{
			Loaded += DateEditCalendarMonth_Loaded;
		}

		void DateEditCalendarMonth_Loaded(object sender, RoutedEventArgs e)
		{
			Loaded -= DateEditCalendarMonth_Loaded;
			SetNewContent(DateTime, DateEditCalendarState.Year, YearInfoTemplate, DateEditCalendarTransferType.None);
		}

		protected override void OnDateTimeChanged()
		{
			UpdateDateTimeText();
			if (OwnerDateEdit == null)
				SetNewContent(DateTime, DateEditCalendarState.Year, YearInfoTemplate, DateEditCalendarTransferType.None);
		}

		protected override void OnTodayButtonClick(object sender, RoutedEventArgs e)
		{
			DateEditCalendarTransferType animationType;
			if (ActiveContent.State == DateEditCalendarState.Month)
				animationType = DateEditCalendarTransferType.ZoomOut;
			if (ActiveContent.State == DateEditCalendarState.Year)
				animationType = DateTime.Year == DateTimeHelper.Now.Year ? DateEditCalendarTransferType.None : DateEditCalendarTransferType.ShiftLeft;
			else
				animationType = DateEditCalendarTransferType.ZoomIn;

			DateTime dt = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Hour, DateTime.Minute, DateTime.Second, DateTime.Millisecond);
			SetNewContent(dt, DateEditCalendarState.Year, YearInfoTemplate, animationType);
			DateTime = ActiveContent.DateTime;
		}

		protected override void OnMonthCellButtonClick(Button button)
		{
			if (OwnerDateEdit == null)
			{
				if (button != null)
					DateTime = ((DateTime)DateEditCalendar.GetDateTime(button));
				return;
			}
			if (!OwnerDateEdit.IsReadOnly)
				OwnerDateEdit.DateTime = (DateTime)DateEditCalendar.GetDateTime(button);
			OwnerDateEdit.IsPopupOpen = false;
		}

		protected override string GetTodayText()
		{
			return DateTime.Today.ToString(CultureInfo.CurrentCulture.DateTimeFormat.YearMonthPattern, CultureInfo.CurrentCulture);
		}
	}
}
