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
	public static class DecimalHelper
	{
		public static string FormatMoney(this Decimal arg)
		{
			return arg.ToString("c");
		}
		public static string FormatMoney(this Decimal? arg)
		{
			return arg == null ? "" : arg.Value.FormatMoney();
		}



		public static string FormatUnit(this Decimal arg)
		{
			return arg.ToString();
		}
		public static string FormatUnit(this Decimal? arg)
		{
			return arg == null ? "" : arg.Value.FormatUnit();
		}

		public static string Format(this Decimal arg, String format)
		{
			return arg.ToString(format);
		}
		public static string Format(this Decimal? arg, String format)
		{
			return arg == null ? "" : arg.Value.Format(format);
		}

	}
}
