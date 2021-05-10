using System;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.NavBar;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class NavBarControlExt
	{
		public static readonly DependencyProperty CompactProperty = DependencyProperty.RegisterAttached(
				"Compact", typeof(bool), typeof(NavBarControlExt), new PropertyMetadata(new PropertyChangedCallback(CompactPropertyChanged)));
		public static void CompactPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			var navBarControl = (NavBarControl)d;
			var value = (bool)e.NewValue;
			navBarControl.Compact = value;
		}
		public static void SetCompact(DependencyObject element, bool value)
		{
			element.SetValue(CompactProperty, value);
		}
		public static bool GetCompact(DependencyObject element)
		{
			return (bool)element.GetValue(CompactProperty);
		}
	}
}
