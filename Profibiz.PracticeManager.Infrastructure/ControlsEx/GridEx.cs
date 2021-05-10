using DevExpress.Xpf.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class GridEx : Grid
	{
		public static readonly DependencyProperty HeightIsZeroProperty =
			DependencyProperty.Register(nameof(HeightIsZero), typeof(bool), typeof(GridEx));
		public bool HeightIsZero
		{
			get { return (bool)GetValue(HeightIsZeroProperty); }
			set { SetValue(HeightIsZeroProperty, value); }
		}

		public static readonly DependencyProperty MeasureHeightMaximumProperty =
			DependencyProperty.Register(nameof(MeasureHeightMaximum), typeof(double), typeof(GridEx));
		public double MeasureHeightMaximum
		{
			get { return (double)GetValue(MeasureHeightMaximumProperty); }
			set { SetValue(MeasureHeightMaximumProperty, value); }
		}


		public GridEx() : base()
		{
		}

		protected override Size MeasureOverride(Size availableSize)
		{
			var desiredSize = base.MeasureOverride(availableSize);
			System.Diagnostics.Debug.WriteLine("Grid2.MeasureOverride=" + desiredSize + ";" + availableSize);
			if (HeightIsZero)
			{
				desiredSize = new Size(desiredSize.Width, 0);
			}
			else if (MeasureHeightMaximum > 0)
			{
				desiredSize = new Size(desiredSize.Width, Math.Min(desiredSize.Height, MeasureHeightMaximum));
			}
			return desiredSize;
		}

		protected override Size ArrangeOverride(Size finalSize)
		{
			var res = base.ArrangeOverride(finalSize);
			System.Diagnostics.Debug.WriteLine("Grid2.ArrangeOverride=" + finalSize + ";" + res);
			return res;
		}
	}
}

