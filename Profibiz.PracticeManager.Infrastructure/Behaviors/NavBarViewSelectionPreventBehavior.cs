using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Navigation;
using DevExpress.Xpf.WindowsUI.Base;
using DevExpress.Xpf.NavBar;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class NavBarViewSelectionPreventBehavior : Behavior<NavBarViewBase>
	{
		public BehaviorSelectionPreventResolver Resolver
		{
			get { return (BehaviorSelectionPreventResolver)GetValue(ResolverProperty); }
			set { SetValue(ResolverProperty, value); }
		}
		public static readonly DependencyProperty ResolverProperty =
            DependencyProperty.Register("Resolver",  typeof(BehaviorSelectionPreventResolver), typeof(NavBarViewSelectionPreventBehavior));

		protected override void OnAttached()
		{
            base.OnAttached();
			AssociatedObject.ActiveGroupChanging += AssociatedObject_ActiveGroupChanging;
        }

		protected override void OnDetaching()
		{
            base.OnDetaching();
			AssociatedObject.ActiveGroupChanging -= AssociatedObject_ActiveGroupChanging;
		}

		private void AssociatedObject_ActiveGroupChanging(object sender, NavBarActiveGroupChangingEventArgs e)
		{
			var newModel = (e.NewGroup as FrameworkContentElement)?.DataContext;
			var oldModel = (e.PrevGroup as FrameworkContentElement)?.DataContext;
			var eventArg = new BehaviorSelectionPreventResolveEventArgs
			{
				NewModel = newModel,
				OldModel = oldModel,
			};

			Resolver.Run(AssociatedObject, eventArg);
			if (eventArg.Cancel)
			{
				e.Cancel = true;
			}
		}
    }
}
