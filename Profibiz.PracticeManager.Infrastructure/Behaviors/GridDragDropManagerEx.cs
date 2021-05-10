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
    public class GridDragDropManagerEx : GridDragDropManager
	{
        public object Reference
		{
            get { return GetValue(ReferenceProperty); }
            set { SetValue(ReferenceProperty, value); }
        }
		public static readonly DependencyProperty ReferenceProperty =
			DependencyProperty.Register("Reference", typeof(object), typeof(GridDragDropManagerEx));


		protected override void OnAttached()
		{
            base.OnAttached();
			Reference = this;
		}

        protected override void OnDetaching()
		{
            base.OnDetaching();
        }
    }
}
