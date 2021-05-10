using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class DataContextReferenceBehavior : Behavior<FrameworkElement>
	{
        public object Source
		{
            get { return GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(object), typeof(DataContextReferenceBehavior));


		protected override void OnAttached()
		{
            base.OnAttached();
			//Source = AssociatedObject;

			AssociatedObject.DataContextChanged += AssociatedObject_DataContextChanged;
			Source = AssociatedObject.DataContext;

		}

		private void AssociatedObject_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Source = e.NewValue;
		}

		protected override void OnDetaching()
		{
            base.OnDetaching();
        }
    }
}
