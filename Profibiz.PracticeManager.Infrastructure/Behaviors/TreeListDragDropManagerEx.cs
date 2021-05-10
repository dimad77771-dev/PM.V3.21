using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class TreeListDragDropManagerEx : TreeListDragDropManager
	{
        public object Reference
		{
            get { return GetValue(ReferenceProperty); }
            set { SetValue(ReferenceProperty, value); }
        }
		public static readonly DependencyProperty ReferenceProperty =
			DependencyProperty.Register("Reference", typeof(object), typeof(TreeListDragDropManagerEx));

		FrameworkElement AssociatedObject2 => AssociatedObject as FrameworkElement;

		protected override void OnAttached()
		{
            base.OnAttached();
			if (AssociatedObject2 != null)
			{
				AssociatedObject2.DataContextChanged += AssociatedObject2_DataContextChanged;
			}
			Reference = this;
		}

		private void AssociatedObject2_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
		{
			Reference = this;
		}

		protected override void OnDetaching()
		{
            base.OnDetaching();
			if (AssociatedObject2 != null)
			{
				AssociatedObject2.DataContextChanged -= AssociatedObject2_DataContextChanged;
			}

		}
	}
}
