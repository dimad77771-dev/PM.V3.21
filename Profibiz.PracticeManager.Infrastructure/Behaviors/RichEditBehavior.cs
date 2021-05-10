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
using System.Threading.Tasks;
using DevExpress.Xpf.RichEdit;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.IO;

namespace Profibiz.PracticeManager.Infrastructure
{
    public class RichEditControlBehavior : Behavior<RichEditControl>
	{
        public RichEditControl Source
		{
            get { return (RichEditControl)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(RichEditControl), typeof(RichEditControlBehavior));

		public RichEditControlBehaviorManager Manager
		{
			get { return (RichEditControlBehaviorManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public static readonly DependencyProperty ManagerProperty =
			DependencyProperty.Register("Manager", typeof(RichEditControlBehaviorManager), typeof(RichEditControlBehavior), new PropertyMetadata(OnManagerChange));


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
			if (e.NewValue != null)
			{
				((RichEditControlBehaviorManager)(e.NewValue)).Behavior = (RichEditControlBehavior)d;
			}
		}
	}

	public class RichEditControlBehaviorManager
	{
		public RichEditControlBehavior Behavior { get; set; }
		public RichEditControl Control => (RichEditControl)Behavior.AssociatedObject;



		public void LoadDocument(string fileName, DocumentFormat documentFormat)
		{
			Control.LoadDocument(fileName, documentFormat);
		}

		public void LoadDocument(Stream stream, DocumentFormat documentFormat)
		{
			Control.LoadDocument(stream, documentFormat);
		}

		public void BeginUpdate()
		{
			Control.BeginUpdate();
		}

		public void EndUpdate()
		{
			Control.EndUpdate();
		}

		public void CancelUpdate()
		{
			Control.CancelUpdate();
		}

		public Document Document => Control.Document;

		

		public bool Modified
		{
			get
			{
				return Control.Modified;
			}
			set
			{
				Control.Modified = value;
			}
		}

		public bool ReadOnly
		{
			get
			{
				return Control.ReadOnly;
			}
			set
			{
				Control.ReadOnly = value;
			}
		}


		public void Focus()
		{
			Control.Focus();
		}






 
	}
}
