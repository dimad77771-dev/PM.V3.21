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

namespace Profibiz.PracticeManager.Infrastructure
{
	public class OfficeNavigationBarSelectionPreventBehavior : Behavior<OfficeNavigationBar>
	{
		public BehaviorSelectionPreventResolver Resolver
		{
			get { return (BehaviorSelectionPreventResolver)GetValue(ResolverProperty); }
			set { SetValue(ResolverProperty, value); }
		}
		public static readonly DependencyProperty ResolverProperty =
            DependencyProperty.Register("Resolver",  typeof(BehaviorSelectionPreventResolver), typeof(OfficeNavigationBarSelectionPreventBehavior));

		protected override void OnAttached()
		{
            base.OnAttached();
			AssociatedObject.SelectionChanging += AssociatedObject_SelectionChanging;
        }
		protected override void OnDetaching()
		{
            base.OnDetaching();
			AssociatedObject.SelectionChanging += AssociatedObject_SelectionChanging;
		}

		private void AssociatedObject_SelectionChanging(object sender, SelectionChangingEventArgs e)
		{
			var newModel = (e.NewValue as FrameworkContentElement)?.DataContext;
			var oldModel = (e.OldValue as FrameworkContentElement)?.DataContext;
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




	public class BehaviorSelectionPreventResolveEventArgs
	{
		public Object NewModel { get; set; }
		public Object OldModel { get; set; }
		public Boolean Cancel { get; set; }
	}
	public delegate void BehaviorSelectionPreventResolveEventHandler(object sender, BehaviorSelectionPreventResolveEventArgs e);
	public class BehaviorSelectionPreventResolver
	{
		public event BehaviorSelectionPreventResolveEventHandler OnResolve;
		public void Run(object sender, BehaviorSelectionPreventResolveEventArgs eventArg)
		{
			OnResolve.Invoke(sender, eventArg);
		}
	}
}
