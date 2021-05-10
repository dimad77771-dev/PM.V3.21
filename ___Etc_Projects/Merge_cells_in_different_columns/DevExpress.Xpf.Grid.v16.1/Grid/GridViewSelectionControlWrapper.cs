// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridViewSelectionControlWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using System;
using System.Collections;

namespace DevExpress.Xpf.Grid
{
  public class GridViewSelectionControlWrapper : SelectionControlWrapper
  {
    private GridViewBase View { get; set; }

    public GridViewSelectionControlWrapper(GridViewBase view)
    {
      this.View = view;
    }

    public static void Register()
    {
      SelectionControlWrapper.Wrappers.Add(typeof (GridViewBase), typeof (GridViewSelectionControlWrapper));
    }

    public override void SubscribeSelectionChanged(Action<IList, IList> a)
    {
      this.View.DataControl.SelectedItems = this.View.GetValue(SelectionAttachedBehavior.SelectedItemsSourceProperty) as IList;
    }

    public override void UnsubscribeSelectionChanged()
    {
      this.View.DataControl.SelectedItems = (IList) null;
    }

    public override IList GetSelectedItems()
    {
      return (IList) null;
    }

    public override void ClearSelection()
    {
    }

    public override void SelectItem(object item)
    {
    }

    public override void UnselectItem(object item)
    {
    }
  }
}
