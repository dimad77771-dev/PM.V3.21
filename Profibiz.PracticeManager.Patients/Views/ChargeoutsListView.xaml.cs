﻿using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.Views
{
	public partial class ChargeoutsListView : UserControl
	{
		public ChargeoutsListView()
		{
			InitializeComponent();

			tableViewGridControl.SelectionChanged += TableViewGridControl_SelectionChanged;
			tableViewGridControl.LayoutUpdated += TableViewGridControl_LayoutUpdated;
			//tableViewGridControl.fo

			//DevExpress.Xpf.Grid.TableView
			//DevExpress.Xpf

			//(DataContext as ViewModels.ChargeoutsListViewModel).aa11 = aaa11;
		}

		private void TableViewGridControl_LayoutUpdated(object sender, System.EventArgs e)
		{
			//tableViewGridControl.View.FocusedRowHandle = 1;
		}

		private void TableViewGridControl_SelectionChanged(object sender, DevExpress.Xpf.Grid.GridSelectionChangedEventArgs e)
		{
			//e.Action
		}
	}
}
