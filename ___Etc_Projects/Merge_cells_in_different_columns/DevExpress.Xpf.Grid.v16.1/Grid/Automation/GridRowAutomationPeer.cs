// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridRowAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GridRowAutomationPeer : GridControlVirtualElementAutomationPeerBase, IInvokeProvider, IScrollItemProvider, ISelectionItemProvider
  {
    private Dictionary<int, GridCellAutomationPeer> cellPeers;

    protected int RowHandle { get; set; }

    bool ISelectionItemProvider.IsSelected
    {
      get
      {
        return this.DataControl.DataView.IsRowSelected(this.RowHandle);
      }
    }

    IRawElementProviderSimple ISelectionItemProvider.SelectionContainer
    {
      get
      {
        return this.ProviderFromPeer((AutomationPeer) this.DataControl.AutomationPeer);
      }
    }

    public GridRowAutomationPeer(int rowHandle, DataControlBase dataControl)
      : base(dataControl)
    {
      this.RowHandle = rowHandle;
      this.cellPeers = new Dictionary<int, GridCellAutomationPeer>();
    }

    protected override FrameworkElement GetFrameworkElement()
    {
      return this.DataControl.viewCore.GetRowElementByRowHandle(this.RowHandle);
    }

    protected override bool HasKeyboardFocusCore()
    {
      if (this.DataControl.DataView.NavigationStyle == GridViewNavigationStyle.Row)
        return this.DataControl.DataView.FocusedRowHandle == this.RowHandle;
      return false;
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> automationPeerList = new List<AutomationPeer>();
      for (int columnIndex = 0; columnIndex < this.DataControl.viewCore.VisibleColumnsCore.Count; ++columnIndex)
        automationPeerList.Add((AutomationPeer) this.GetCellPeer(columnIndex, false));
      return automationPeerList;
    }

    protected internal virtual GridCellAutomationPeer GetCellPeer(int columnIndex, bool force = false)
    {
      GridCellAutomationPeer cellAutomationPeer = (GridCellAutomationPeer) null;
      if (!this.cellPeers.TryGetValue(columnIndex, out cellAutomationPeer))
      {
        cellAutomationPeer = new GridCellAutomationPeer(this.RowHandle, columnIndex, this.DataControl);
        this.cellPeers[columnIndex] = cellAutomationPeer;
      }
      return cellAutomationPeer;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.Invoke)
        return (object) this;
      if (patternInterface == PatternInterface.ScrollItem)
        return (object) this;
      if (patternInterface == PatternInterface.SelectionItem)
        return (object) this;
      return (object) null;
    }

    protected override string GetNameCore()
    {
      if (this.DataControl == null)
        return string.Empty;
      object boundObject = this.GetBoundObject();
      if (boundObject == null)
        return string.Empty;
      return boundObject.ToString();
    }

    protected virtual object GetBoundObject()
    {
      return this.DataControl.GetRow(this.RowHandle);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.DataItem;
    }

    void IInvokeProvider.Invoke()
    {
      this.Invoke();
    }

    protected virtual void Invoke()
    {
      this.DataControl.viewCore.SetFocusedRowHandle(this.RowHandle);
    }

    void IScrollItemProvider.ScrollIntoView()
    {
      this.DataControl.viewCore.SetFocusedRowHandle(this.RowHandle);
    }

    void ISelectionItemProvider.AddToSelection()
    {
      this.DataControl.DataView.SelectRowCore(this.RowHandle);
    }

    void ISelectionItemProvider.RemoveFromSelection()
    {
      this.DataControl.DataView.UnselectRowCore(this.RowHandle);
    }

    void ISelectionItemProvider.Select()
    {
      this.DataControl.DataView.SetFocusedRowHandle(this.RowHandle);
      if (this.DataControl.SelectionMode == MultiSelectMode.Row)
      {
        this.DataControl.BeginSelection();
        this.DataControl.UnselectAll();
        this.DataControl.DataView.SelectRowCore(this.RowHandle);
        this.DataControl.EndSelection();
      }
      else
      {
        if (this.DataControl.SelectionMode != MultiSelectMode.MultipleRow)
          return;
        this.DataControl.DataView.SelectRowCore(this.RowHandle);
      }
    }
  }
}
