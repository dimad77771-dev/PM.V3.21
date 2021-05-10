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
    public class TreeViewSelectItemBehavior : Behavior<TreeView>
	{
        public object SelectedItem
		{
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }
        public static readonly DependencyProperty SelectedItemProperty =
            DependencyProperty.Register(
				"SelectedItem", 
				typeof(object), 
				typeof(TreeViewSelectItemBehavior), 
				new PropertyMetadata
				(
					null, 
					(d, e) => ((TreeViewSelectItemBehavior)d).OnSelectedItemChanged()//,
					//(d, e) => ((TreeViewSelectItemBehavior)d).CoerceValueCallback(e)
					)
				);

		object CoerceValueCallback(object e)
		{
			return null;
		}


		protected override void OnAttached()
		{
            base.OnAttached();
            AssociatedObject.SelectedItemChanged += OnTreeViewSelectedItemChanged;
            Dispatcher.BeginInvoke(new Action(OnSelectedItemChanged), DispatcherPriority.ApplicationIdle);
        }

        protected override void OnDetaching()
		{
            base.OnDetaching();
            AssociatedObject.SelectedItemChanged -= OnTreeViewSelectedItemChanged;
        }

		void OnTreeViewSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
		{
			if (e.NewValue is FrameworkElement)
			{
				SelectedItem = ((FrameworkElement)(e.NewValue)).DataContext;
			}
			else
			{
				SelectedItem = e.NewValue;
			}
		}

        void OnSelectedItemChanged()
		{
			var selectedItem = GetAllItems().FirstOrDefault(x => x.DataContext == SelectedItem);
			if (selectedItem != null)
			{
				selectedItem.IsSelected = true;
			}
        }

		IEnumerable<TreeViewItem> GetAllItems()
		{
			if (AssociatedObject == null)
			{
				return Enumerable.Empty<TreeViewItem>();
			}
			var ret = new List<TreeViewItem>();
			var lev0 = AssociatedObject.Items.Cast<TreeViewItem>();
			ret.AddRange(lev0);
			var lev1 = AssociatedObject.Items.Cast<TreeViewItem>().SelectMany(x => x.Items.Cast<object>().Select((y, i) => (TreeViewItem)x.ItemContainerGenerator.ContainerFromIndex(i)).Where(y => y != null));
			ret.AddRange(lev1);
			return ret;
		}

		//IEnumerable<TreeViewItem> GetAllItems()
		//{
		//	var ret = getTreeViewItems(AssociatedObject);
		//	return ret;
		//}

		private static TreeViewItem[] getTreeViewItems(TreeView treeView)
		{
			List<TreeViewItem> returnItems = new List<TreeViewItem>();
			for (int x = 0; x < treeView.Items.Count; x++)
			{
				returnItems.AddRange(getTreeViewItems((TreeViewItem)treeView.Items[x]));
			}
			return returnItems.ToArray();
		}

		private static TreeViewItem[] getTreeViewItems(TreeViewItem currentTreeViewItem)
		{
			List<TreeViewItem> returnItems = new List<TreeViewItem>();
			returnItems.Add(currentTreeViewItem);
			for (int x = 0; x < currentTreeViewItem.Items.Count; x++)
			{
				var ii = currentTreeViewItem.Items[x];
				var childItem2 = (TreeViewItem)currentTreeViewItem.ItemContainerGenerator.ContainerFromItem(ii);
				var childItem = (TreeViewItem)currentTreeViewItem.ItemContainerGenerator.ContainerFromIndex(x);
				if (childItem == null)
				{
					var a = 10;
				}
				returnItems.AddRange(getTreeViewItems(childItem));
			}
			return returnItems.ToArray();
		}
	}
}
