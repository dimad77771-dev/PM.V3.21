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
	public class TreeListViewBehavior : Behavior<TreeListView>
	{
		public TreeListViewActions Actions
		{
			get { return (TreeListViewActions)GetValue(ActionsProperty); }
			set { SetValue(ActionsProperty, value); }
		}
		public static readonly DependencyProperty ActionsProperty =
			DependencyProperty.Register(
				"Actions",
				typeof(TreeListViewActions),
				typeof(TreeListViewBehavior),
				new PropertyMetadata(null, (d, e) => ((TreeListViewBehavior)d).OnSelectedItemChanged((TreeListViewActions)e.NewValue))
				);

		void OnSelectedItemChanged(TreeListViewActions action)
		{
			if (action != null)
			{
				action.AssociatedObject = AssociatedObject;
			}
		}


		protected override void OnAttached()
		{
			base.OnAttached();
		}

		protected override void OnDetaching()
		{
			base.OnDetaching();
		}
	}


	public class TreeListViewActions
	{
		public TreeListView AssociatedObject { get; set; }

		public void ExpandAllNodes()
		{
			AssociatedObject.ExpandAllNodes();
		}

		public void CollapseAllNodes()
		{
			AssociatedObject.CollapseAllNodes();
		}

	}
}
