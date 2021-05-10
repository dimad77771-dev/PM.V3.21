using DevExpress.Xpf.Grid;
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
using System.Windows.Controls;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class WidthInfo
	{
		static double _dateColumn = -1;
		public static GridColumnWidth DateColumn
		{
			get
			{
				if (_dateColumn > 0)
				{
					return new GridColumnWidth(_dateColumn);
				}

				var textBlock = new TextBlock { Text = new DateTime(2018,12,22).ToShortDateString(), TextWrapping = TextWrapping.NoWrap };
				textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
				textBlock.Arrange(new Rect(textBlock.DesiredSize));
				_dateColumn = textBlock.ActualWidth;
				_dateColumn = Math.Round(_dateColumn) + 12;
				return new GridColumnWidth(_dateColumn);
			}
		}

		public static GridColumnWidth DateColumnInGrid => new GridColumnWidth(DateColumn.Value + 10);

		public static GridColumnWidth DateYYColumn => new GridColumnWidth(65);
		public static GridColumnWidth DateYYColumnInGrid => new GridColumnWidth(65);


		static double _moneyColumn = -1;
        public static GridColumnWidth MoneyColumn
        {
            get
            {
                if (_moneyColumn > 0)
                {
                    return new GridColumnWidth(_moneyColumn);
                }

                var val = 9999999.99M;
                var textBlock = new TextBlock { Text = val.ToString("c"), TextWrapping = TextWrapping.NoWrap };
                textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                textBlock.Arrange(new Rect(textBlock.DesiredSize));
                _moneyColumn = textBlock.ActualWidth;
                _moneyColumn = Math.Round(_moneyColumn) + 12;
                return new GridColumnWidth(_moneyColumn);
            }
        }


        static double _string10Column = -1;
        public static GridColumnWidth String10Column
        {
            get
            {
                if (_string10Column > 0)
                {
                    return new GridColumnWidth(_string10Column);
                }

                var textBlock = new TextBlock { Text = "WWWWWWWWWW", TextWrapping = TextWrapping.NoWrap };
                textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
                textBlock.Arrange(new Rect(textBlock.DesiredSize));
                _string10Column = textBlock.ActualWidth;
                _string10Column = Math.Round(_string10Column) + 12;
                return new GridColumnWidth(_string10Column);
            }
        }


		static double _insuranceCodeInAppointment = -1;
		public static Double InsuranceCodeInAppointment
		{
			get
			{
				if (_insuranceCodeInAppointment > 0)
				{
					return _insuranceCodeInAppointment;
				}

				var textBlock = new TextBlock { Text = "WWW", TextWrapping = TextWrapping.NoWrap };
				textBlock.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
				textBlock.Arrange(new Rect(textBlock.DesiredSize));
				_insuranceCodeInAppointment = textBlock.ActualWidth;
				_insuranceCodeInAppointment = Math.Round(_insuranceCodeInAppointment);
				return _insuranceCodeInAppointment;
			}
		}


		public static GridColumnWidth DateMonthYearColumn => 100;
	}

}
