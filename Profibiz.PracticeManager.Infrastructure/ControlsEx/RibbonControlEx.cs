using DevExpress.Xpf.Ribbon;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class RibbonControlEx : RibbonControl
	{
		public static readonly DependencyProperty HideHeaderAndTabsGridProperty = 
			DependencyProperty.Register(nameof(HideHeaderAndTabsGrid), typeof(bool), typeof(RibbonControlEx), new PropertyMetadata(OnHideHeaderAndTabsGridChange));
		public bool HideHeaderAndTabsGrid
		{
			get { return (bool)GetValue(HideHeaderAndTabsGridProperty); }
			set { SetValue(HideHeaderAndTabsGridProperty, value); }
		}

		public static void OnHideHeaderAndTabsGridChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((RibbonControlEx)d).UpdateHideHeaderAndTabsGrid();
		}
		

		public RibbonControlEx() : base()
		{
		}

		void UpdateHideHeaderAndTabsGrid()
		{
			if (HeaderAndTabsGrid != null)
			{
				HeaderAndTabsGrid.Visibility = (HideHeaderAndTabsGrid ? Visibility.Collapsed : Visibility.Visible);
			}
		}

		public override void OnApplyTemplate()
		{
			base.OnApplyTemplate();
			UpdateHideHeaderAndTabsGrid();
		}
	}
}

