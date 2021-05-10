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

namespace Profibiz.PracticeManager.Infrastructure
{
	public class TreeListViewShowingEditorResponseEventArgs
	{
		public String FieldName { get; set; }
		public Object RowModel { get; set; }
		public Object Value { get; set; }
		public Boolean Cancel { get; set; }
	} 
	public delegate void TreeListViewShowingEditorResponseEventHandler(object sender, TreeListViewShowingEditorResponseEventArgs e);
	public class TreeListViewShowingEditorResponse
	{
		public event TreeListViewShowingEditorResponseEventHandler OnShowingEditor;

		public bool Run(TreeListViewShowingEditorBehavior sender, TreeListShowingEditorEventArgs e)
		{
			var arg = new TreeListViewShowingEditorResponseEventArgs
			{
				FieldName = e.Column.FieldName,
				RowModel = e.Node.Content,
				Value = e.Value,
			};
			OnShowingEditor.Invoke(sender, arg);
			return arg.Cancel;
		}
	}


	public class TreeListViewShowingEditorBehavior : Behavior<TreeListView>
	{
        public TreeListViewShowingEditorResponse Response
		{
            get { return (TreeListViewShowingEditorResponse)GetValue(ResponseProperty); }
            set { SetValue(ResponseProperty, value); }
        }
        public static readonly DependencyProperty ResponseProperty =
            DependencyProperty.Register("Response",  typeof(TreeListViewShowingEditorResponse), typeof(TreeListViewShowingEditorBehavior));

		protected override void OnAttached()
		{
            base.OnAttached();
			AssociatedObject.ShowingEditor += AssociatedObject_ShowingEditor;
        }
		protected override void OnDetaching()
		{
            base.OnDetaching();
			AssociatedObject.ShowingEditor += AssociatedObject_ShowingEditor;
		}

		private void AssociatedObject_ShowingEditor(object sender, TreeListShowingEditorEventArgs e)
		{
			if (Response.Run(this, e))
			{
				e.Cancel = true;
			}
		}




    }
}
