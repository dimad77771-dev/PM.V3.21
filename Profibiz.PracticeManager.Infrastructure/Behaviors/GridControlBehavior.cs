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

namespace Profibiz.PracticeManager.Infrastructure
{
    public class GridControlBehavior : Behavior<GridControl>
	{
        public GridControl Source
		{
            get { return (GridControl)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }
		public static readonly DependencyProperty SourceProperty =
			DependencyProperty.Register("Source", typeof(GridControl), typeof(GridControlBehavior));

		public GridControlBehaviorManager Manager
		{
			get { return (GridControlBehaviorManager)GetValue(ManagerProperty); }
			set { SetValue(ManagerProperty, value); }
		}
		public static readonly DependencyProperty ManagerProperty =
			DependencyProperty.Register("Manager", typeof(GridControlBehaviorManager), typeof(GridControlBehavior), new PropertyMetadata(OnManagerChange));


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
				((GridControlBehaviorManager)(e.NewValue)).Behavior = (GridControlBehavior)d;
			}
		}
	}

	public class GridControlBehaviorManager
	{
		public GridControlBehavior Behavior { get; set; }
		public GridControl Control => (GridControl)Behavior.AssociatedObject;

		public void SetCurrentColumn(string fieldName)
		{
			var column = Control.Columns.Single(q => q.FieldName == fieldName);
			Control.CurrentColumn = column;
		}

		public void FocuseRow(int rowNumber)
		{
			var rowhandle = Control.GetRowHandleByListIndex(rowNumber);
			Control.View.FocusedRowHandle = rowhandle;
		}

		public void ShowEditor(bool selectAll = false)
		{
			Control.View.ShowEditor(selectAll);
			Control.View.ShowEditor(selectAll);
			Control.View.ShowEditor(selectAll);

			//Control.View.ActiveEditor.SelectAll();

			//Control.View.Focus();
			//Debug.WriteLine("ZZZZ11=" + Control.View.IsFocused);
			//Control.selectro
			//var rowhandle = Control.GetRowHandleByListIndex(0);
			//Control.View.FocusedRowHandle = rowhandle;
			//var column = Control.Columns.Single(q => q.FieldName == "Name");
			//Control.CurrentColumn = column;
			//Thread.Sleep(1000);
			//var bb = Control.View.GetIsCellFocused(rowhandle, column);
			//Control.View.ShowEditor(selectAll);
			//var a = Control.View.ActiveEditor;
			//Debug.WriteLine("ZZZZ=" + a?.GetType()?.FullName);
			//Debug.WriteLine("ZZZZ=" + bb);
		}

		public void FocuseSearchControl()
		{
			Control.View.SearchControl.Focus();
		}

		public void Focus()
		{
			Control.Focus();
		}

		public void BeginDataUpdate()
		{
			Control.BeginDataUpdate();
		}

		public void EndDataUpdate()
		{
			Control.EndDataUpdate();
		}


		public void UpdateGroupSummary()
		{
			Control.UpdateGroupSummary();
		}

		public void UpdateTotalSummary()
		{
			Control.UpdateTotalSummary();
		}

		public void ExpandAllGroups()
		{
			Control.ExpandAllGroups();
		}

		public void CollapseAllGroups()
		{
			Control.CollapseAllGroups();
		}

		public T GetRow<T>(int rowHandle) where T : class
		{
			return Control.GetRow(rowHandle) as T;
		}

		public TableViewHitInfo GetCalcHitInfo(System.Windows.Input.MouseButtonEventArgs e)
		{
			var tview = (TableView)Control.View;
			var hitInfo = tview.CalcHitInfo(e.GetPosition(tview));
			return hitInfo;
		}

		public void Test22()
		{
			//GridColumn qqq;
			//qqq.CellStyle

			//LightweightCellEditor a;
			//a.Background
			//a.Foreground
			//a.Background
			var rowNumber = 0;
			var rowhandle = Control.GetRowHandleByListIndex(rowNumber);
			var row = Control.GetRow(rowhandle);
		}



		public void Test11()
		{
			DispatcherUIHelper.Run(async () =>
			{
				Control.View.ShowEditor(true);
				await Task.Delay(500);
				Control.View.ShowEditor(true);
				await Task.Delay(500);
				Control.View.ShowEditor(true);
				await Task.Delay(500);
				var bb = Control.View.ActiveEditor;
				Debug.WriteLine("bb=" + (bb == null));
			});
		}



		public IEnumerable<int> GetAllVisibleRowHandles()
		{
			for (int i = 0; i < Control.VisibleRowCount; i++)
			{
				int rowHandle = Control.GetRowHandleByVisibleIndex(i);
				yield return rowHandle;
			}
		}

		public void OnFilterSortGroupChange(Action action)
		{
			Control.EndSorting += (s, e) => action();
			Control.FilterChanged += (s, e) => action();
			Control.EndGrouping += (s, e) => action();
		}

		public void OnBeforeLayoutRefresh(Action action)
		{
			Control.View.BeforeLayoutRefresh += View_BeforeLayoutRefresh;
		}

		private void View_BeforeLayoutRefresh(object sender, DevExpress.Xpf.Core.CancelRoutedEventArgs e)
		{
			//e.Cancel = false;
			e.Cancel = true;
		}

		//public void OnCustomRowFilter(Action<RowFilterEventArgs> action)
		//{
		//	Control.CustomRowFilter += (s, e) => action(e);
		//}

		public void zzzzzzzzzz()
		{
			var a1 = Control.FilterCriteria;
			var a2 = Control.FilteredComponent;
			var a3 = Control.FilterString;

			//Debug.WriteLine("===========================================================");
			var tv = ((DevExpress.Xpf.Grid.TreeListView)Control.View);
			TreeListNodeIterator nodeIterator = new TreeListNodeIterator(tv.Nodes, false);
			var cnt = 0;
			foreach (var node in nodeIterator)
			{
				var a = node.Content.ToString();
				//Debug.WriteLine("Z=" + a + "=" + node.IsFiltered);
				var b = node.IsFiltered;
				if (node.IsFiltered) cnt++;
			}
			Debug.WriteLine("EE=" + cnt);
			//Debug.WriteLine("===========================================================");
		}
	}
}
