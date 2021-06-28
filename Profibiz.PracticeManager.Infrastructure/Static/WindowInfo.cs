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
    public static class WindowInfo
    {
        public static bool ShowInTaskbar { get; set; } = false;

        public static double Height80 => GetHeight(80);
		public static double Height85 => GetHeight(85);
		public static double Height90 => GetHeight(90);
		public static double Height95 => GetHeight(95);
		public static double Width80 => GetWidth(80);
		public static double Width85 => GetWidth(85);
		public static double Width90 => GetWidth(90);
		public static double Width95 => GetWidth(95);
		public static double Width99 => GetWidth(99);

		public static double HeightMax => GetHeight(95);
		public static double WidthMax => GetWidth(99);
		//public static double HeightMax => GetHeight(66);
		//public static double WidthMax => GetWidth(66);


		static double GetHeight(double percent)
        {
            return ScreenHeight * percent / 100.0;
        }

        static double GetWidth(double percent)
        {
            return ScreenWidth * percent / 100.0;
        }


        static double ScreenHeight => SystemParameters.WorkArea.Height;
        static double ScreenWidth => SystemParameters.WorkArea.Width;

		public static void CenterWindowVertical(Window window)
		{
			window.Top = (ScreenHeight - window.Height) / 2;
		}
    }
}
