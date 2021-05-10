// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.TableViewSelectionStrategyNone
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using System;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.Native
{
  internal class TableViewSelectionStrategyNone : SelectionStrategyNone
  {
    private int oldFocusedRowHandle = int.MinValue;

    protected SelectionActionBase SelectionAction
    {
      get
      {
        return this.RootView.GlobalSelectionAction;
      }
      set
      {
        this.RootView.SetSelectionAction(value);
      }
    }

    protected GridViewBase GridView
    {
      get
      {
        return (GridViewBase) this.view;
      }
    }

    protected GridViewBase RootView
    {
      get
      {
        return (GridViewBase) this.view.RootView;
      }
    }

    private GridControl Grid
    {
      get
      {
        return (GridControl) this.view.DataControl;
      }
    }

    public TableViewSelectionStrategyNone(GridViewBase view)
      : base((DataViewBase) view)
    {
    }

    public override void OnAssignedToGrid()
    {
      base.OnAssignedToGrid();
      this.GridView.SetSelectionAnchor();
    }

    protected virtual bool CanExecuteSelectionActionOnFocusedRowHandleChanged()
    {
      if (this.SelectionAction != null)
        return this.SelectionAction.CanFocusChangeDeleteAction;
      return false;
    }

    public override void OnFocusedRowHandleChanged(int oldRowHandle)
    {
      if (this.CanExecuteSelectionActionOnFocusedRowHandleChanged())
        this.GridView.ExecuteSelectionAction();
      else
        this.GridView.SetSelectionAnchor();
    }

    public override void CopyMasterDetailToClipboard()
    {
      this.Grid.CopyAllSelectedItemsToClipboard();
    }

    public override void OnBeforeProcessKeyDown(KeyEventArgs e)
    {
      this.oldFocusedRowHandle = this.view.FocusedRowHandle;
      SelectionStrategyRow.GetKeyboardSelectionAction(this.GridView, e).Do<SelectionActionBase>((Action<SelectionActionBase>) (action => this.SelectionAction = action));
    }

    public override void OnAfterProcessKeyDown(KeyEventArgs e)
    {
      if (e.Key == Key.Next || e.Key == Key.Prior)
        return;
      this.OnNavigationComplete(e.Key == Key.Tab);
    }

    public override void OnNavigationComplete(bool IsTabPressed)
    {
      if (this.oldFocusedRowHandle != this.view.FocusedRowHandle)
        return;
      this.SelectionAction = (SelectionActionBase) null;
    }

    public override void OnNavigationCanceled()
    {
      this.SelectionAction = (SelectionActionBase) null;
    }
  }
}
