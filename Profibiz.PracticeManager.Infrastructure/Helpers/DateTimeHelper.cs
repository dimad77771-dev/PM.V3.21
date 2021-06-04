using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class DateTimeHelper
	{
		public static DateTime Now => DateTime.Now;

		public static bool IntervalsIntersection(DateTime start1, DateTime finish1, DateTime start2, DateTime finish2)
		{
			if (finish1 <= start2 || finish2 <= start1 || start1 >= finish2 || start2 >= finish1)
			{
				return false;
			}
			else
			{
				return true;
			}
		}

		public static string FormatHHMM(this DateTime arg)
		{
			var ret = arg.ToString("t");
			//var ret = arg.ToString("t");
			return ret;
		}
		public static string FormatHHMM(this DateTime? arg)
		{
			return arg == null ? "" : FormatHHMM((DateTime)arg);
		}


        public static string FormatShortDate(this DateTime arg)
        {
            return arg.ToString("d");
            //return arg.ToString("dd-MM-yyyy");
        }
        public static string FormatShortDate(this DateTime? arg)
        {
            return arg == null ? "" : FormatShortDate((DateTime)arg);
        }

		public static string FormatShortYYDate(this DateTime arg)
		{
			return arg.ToString(@"MM\/dd\/yy");
		}
		public static string FormatShortYYDate(this DateTime? arg)
		{
			return arg == null ? "" : FormatShortYYDate((DateTime)arg);
		}

		public static string FormatDDMMYY(this DateTime? arg)
		{
			return arg == null ? "" : arg.Value.ToString("dd-MM-yy");
		}
		public static string FormatDDMMYY2(this DateTime? arg)
		{
			return arg == null ? "" : arg.Value.ToString("dd/MM/yy");
		}


		public static string FormatLongDate(this DateTime arg)
		{
			return arg.ToString("D");
		}
		public static string FormatLongDate(this DateTime? arg)
		{
			return arg == null ? "" : FormatLongDate((DateTime)arg);
		}

		public static string FormatMonthYear(this DateTime arg)
		{
			return arg.ToString(@"MM\/yyyy");
		}
		public static string FormatMonthYear(this DateTime? arg)
		{
			return arg == null ? "" :arg.Value.ToString(@"MM\/yyyy");
		}



		public static DateTime FirstDayCurrentMonth() => FirstDayMonth(DateTime.Today);
		public static DateTime FirstDayMonth(this DateTime cur)
		{
			return new DateTime(cur.Year, cur.Month, 1);
		}


		public static DateTime FirstDayCurrentQuarter() => FirstDayQuarter(DateTime.Today);
		public static DateTime FirstDayQuarter(DateTime cur)
		{
			cur = new DateTime(cur.Year, cur.Month, 1);
			while (cur.Month % 3 != 1)
			{
				cur = cur.AddMonths(-1);
			}
			return cur;
		}


		public const DayOfWeek START_OF_WEEK = DayOfWeek.Sunday;
		public static DateTime FirstDayCurrentWeek() => FirstDayWeek(DateTime.Today);
		public static DateTime FirstDayWeek(DateTime cur)
		{
			while (cur.DayOfWeek != START_OF_WEEK)
			{
				cur = cur.AddDays(-1);
			}
			return cur;
		}
		

		public static DateTime LastDayCurrentMonth() => LastDayMonth(DateTime.Today);
		public static DateTime LastDayMonth(this DateTime cur)
		{
			cur = cur.AddMonths(1);
			return new DateTime(cur.Year, cur.Month, 1).AddDays(-1);
		}



		public static DateTime FirstDayCurrentYear() => FirstDayYear(DateTime.Today);
		public static DateTime FirstDayYear(DateTime cur)
		{
			return new DateTime(cur.Year, 1, 1);
		}

		public static DateTime LastDayCurrentYear() => LastDayYear(DateTime.Today);
		public static DateTime LastDayYear(DateTime cur)
		{
			return new DateTime(cur.Year, 12, 31);
		}

		public static string ToWebQuery(this DateTime arg)
		{
			return arg.ToString("yyyy-MM-dd");
		}

        public static string ToWebQuery(this DateTime? arg)
        {
            return arg.Value.ToString("yyyy-MM-dd");
        }

        public static string ToWebQuery(this Boolean arg)
        {
            return arg ? "true" : "false";
        }

		public static string ToWebQuery(this Guid[] arg)
		{
			return string.Join(";", arg.Select(q => q.ToString()).ToArray());
		}

		public static string ToWebQuery(this Guid arg)
		{
			return arg.ToString();
		}

		public static string ToWebQuery(this Guid? arg)
		{
			return (arg == null ? "" : arg.Value.ToString());
		}

		public static string ToWebQuery(this Boolean? arg)
        {
            return ToWebQuery(arg.Value);
        }

		public static DateTime MIN_DATE = new DateTime(1900, 01, 01);
		public static DateTime MAX_DATE = new DateTime(3000, 01, 01);


		public static string FormatTimeIntervalUserFriendly(DateTime start, DateTime finish)
		{
			if (start > finish)
			{
				return FormatTimeIntervalUserFriendly(finish, start) + " overdue";
			}

			var timespan = finish - start;

			var totalDays = (int)timespan.TotalDays;
			if (totalDays > 0)
			{
				if (totalDays == 1)
				{
					return "1 day";
				}
				else
				{
					return totalDays + " days";
				}
			}

			var totalHours = (int)timespan.TotalHours;
			if (totalHours > 0)
			{
				if (totalHours == 1)
				{
					return "1 hour";
				}
				else
				{
					return totalHours + " hours";
				}
			}

			var totalMinutes = (int)timespan.TotalMinutes;
			if (totalMinutes == 0)
			{
				return "0 minute";
			}
			else if (totalMinutes == 1)
			{
				return "1 minute";
			}
			else
			{
				return totalMinutes + " minutes";
			}

			
		}


		public static ChangeMonthFromToRetrun ChangeMonthFromTo(int arg, DateTime from, DateTime to)
		{
			if (arg == 1)
			{
				from = (new DateTime(to.Year, to.Month, 1)).AddMonths(1);
				to = from.AddMonths(1).AddDays(-1);
			}
			else if (arg == -1)
			{
				from = (new DateTime(from.Year, from.Month, 1)).AddMonths(-1);
				to = from.AddMonths(1).AddDays(-1);
			}
			else throw new ArgumentException();

			//if (from != DateTimeHelper.MAX_DATE)
			//{
			//	from = from.AddMonths(arg);
			//}
			//if (to != DateTimeHelper.MAX_DATE)
			//{
			//	to = to.AddMonths(arg);
			//}

			return new ChangeMonthFromToRetrun
			{
				From = from,
				To = to,
			};
		}
		public class ChangeMonthFromToRetrun
		{
			public DateTime From { get; set; }
			public DateTime To { get; set; }
		}



		

	}
}
