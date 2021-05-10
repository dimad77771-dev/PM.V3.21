// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewSelectionControlWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System.Collections;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListViewSelectionControlWrapper : SelectionControlWrapper
  {
    protected System.Action<IList, IList> Action { get; set; }

    protected TreeListView View { get; set; }

    public TreeListViewSelectionControlWrapper(TreeListView view)
    {
      this.View = view;
    }

    public static void Register()
    {
      SelectionControlWrapper.Wrappers.Add(typeof (TreeListView), typeof (TreeListViewSelectionControlWrapper));
    }

    public override void SubscribeSelectionChanged(System.Action<IList, IList> action)
    {
      this.Action = action;
      if (this.View.DataControl is GridControl)
        ((GridControl) this.View.DataControl).SelectionChanged += new GridSelectionChangedEventHandler(this.OnSelectionChanged);
      if (!(this.View.DataControl is TreeListControl))
        return;
      ((TreeListControl) this.View.DataControl).SelectionChanged += new TreeListSelectionChangedEventHandler(this.OnSelectionChanged);
    }

    public override void UnsubscribeSelectionChanged()
    {
      if (this.View.DataControl is GridControl)
        ((GridControl) this.View.DataControl).SelectionChanged -= new GridSelectionChangedEventHandler(this.OnSelectionChanged);
      if (!(this.View.DataControl is TreeListControl))
        return;
      ((TreeListControl) this.View.DataControl).SelectionChanged -= new TreeListSelectionChangedEventHandler(this.OnSelectionChanged);
    }

    private void OnSelectionChanged(object sender, GridSelectionChangedEventArgs e)
    {
      IList list1 = (IList) null;
      IList list2 = (IList) null;
      if (e.Action == CollectionChangeAction.Add)
        list1 = (IList) new object[1]
        {
          this.View.DataProviderBase.GetRowValue(e.ControllerRow)
        };
      if (e.Action == CollectionChangeAction.Remove)
        list2 = (IList) new object[1]
        {
          this.View.DataProviderBase.GetRowValue(e.ControllerRow)
        };
      this.Action(list1, list2);
    }

    public override IList GetSelectedItems()
    {
      if (this.View.DataControl != null)
        return this.View.DataControl.SelectedItems;
      return (IList) null;
    }

    public override void ClearSelection()
    {
      if (this.View.DataControl == null)
        return;
      this.View.DataControl.UnselectAll();
    }

    public override void SelectItem(object item)
    {
      TreeListNode nodeByContent = this.View.GetNodeByContent(item);
      if (nodeByContent == null)
        return;
      this.View.DataControl.SelectItem(nodeByContent.RowHandle);
    }

    public override void UnselectItem(object item)
    {
      TreeListNode nodeByContent = this.View.GetNodeByContent(item);
      if (nodeByContent == null)
        return;
      this.View.DataControl.UnselectItem(nodeByContent.RowHandle);
    }
  }
}
