using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm.UI.Interactivity;
using DevExpress.Xpf.Grid;
using System.Diagnostics;
using System.Threading;
using DevExpress.Xpf.Editors;
using System.Windows.Input;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class UIElementBehavior : Behavior<DependencyObject>
	{
        public DependencyObject Source
		{
            get { return (DependencyObject)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(DependencyObject), typeof(UIElementBehavior));

		public UIElementManager Manager
		{
			get { return (UIElementManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public static readonly DependencyProperty ManagerProperty =
			DependencyProperty.Register("Manager", typeof(UIElementManager), typeof(UIElementBehavior), new PropertyMetadata(OnManagerChange));


		protected override void OnAttached()
		{
            base.OnAttached();
			Source = AssociatedObject;
		}

        protected override void OnDetaching()
		{
            base.OnDetaching();
        }

		public static void OnManagerChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
		{
			((UIElementManager)(e.NewValue)).Behavior = (UIElementBehavior)d;
		}
	}

	public class UIElementManager
	{
		public UIElementBehavior Behavior { get; set; }
		DependencyObject Control => (DependencyObject)Behavior.AssociatedObject;
		TextEdit TextEditControl => (TextEdit)Behavior.AssociatedObject;

		public bool DoValidate()
		{
			return ((BaseEdit)Control).DoValidate();
		}

		public void SelectNone()
		{
			var control = (TextEdit)Control;
			control.SelectionLength = 0;
		}

		public int SelectionStart
		{
			get
			{
				return TextEditControl.SelectionStart;
			}
			set
			{
				TextEditControl.SelectionStart = value;
			}
		}

		public int SelectionLength
		{
			get
			{
				return TextEditControl.SelectionLength;
			}
			set
			{
				TextEditControl.SelectionLength = value;
			}
		}

		public void Focus()
		{
			((UIElement)Control).Focus();
		}

		public void ClearFocus()
		{
			Keyboard.ClearFocus();
		}

	}
}
