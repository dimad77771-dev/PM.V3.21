// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridCellAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace DevExpress.Xpf.Grid.Automation
{
  public class GridCellAutomationPeer : GridControlVirtualElementAutomationPeerBase, IGridItemProvider, IValueProvider, ISelectionItemProvider
  {
    protected int RowHandle { get; set; }

    protected int ColumnIndex { get; set; }

    protected ColumnBase Column
    {
      get
      {
        return this.DataControl.viewCore.VisibleColumnsCore[this.ColumnIndex];
      }
    }

    bool IValueProvider.IsReadOnly
    {
      get
      {
        return this.Column.ReadOnly;
      }
    }

    string IValueProvider.Value
    {
      get
      {
        return this.DataControl.DataView.GetColumnDisplayText(this.DataControl.GetCellValue(this.RowHandle, this.Column.FieldName), this.Column, new int?());
      }
    }

    int IGridItemProvider.Column
    {
      get
      {
        return this.ColumnIndex;
      }
    }

    int IGridItemProvider.ColumnSpan
    {
      get
      {
        return 1;
      }
    }

    IRawElementProviderSimple IGridItemProvider.ContainingGrid
    {
      get
      {
        return (IRawElementProviderSimple) this.DataControl.AutomationPeer;
      }
    }

    int IGridItemProvider.Row
    {
      get
      {
        return this.DataControl.GetRowVisibleIndexByHandleCore(this.RowHandle);
      }
    }

    int IGridItemProvider.RowSpan
    {
      get
      {
        return 1;
      }
    }

    bool ISelectionItemProvider.IsSelected
    {
      get
      {
        if (this.DataControl.SelectionMode == MultiSelectMode.Cell)
          return this.DataControl.DataView.SelectionStrategy.IsCellSelected(this.RowHandle, this.Column);
        if (this.DataControl.DataView.FocusedRowHandle == this.RowHandle)
          return this.DataControl.CurrentColumn == this.Column;
        return false;
      }
    }

    IRawElementProviderSimple ISelectionItemProvider.SelectionContainer
    {
      get
      {
        return (IRawElementProviderSimple) this.DataControl.AutomationPeer;
      }
    }

    public GridCellAutomationPeer(int rowHandle, int columnIndex, DataControlBase dataControl)
      : base(dataControl)
    {
      this.RowHandle = rowHandle;
      this.ColumnIndex = columnIndex;
    }

    protected override FrameworkElement GetFrameworkElement()
    {
      return this.DataControl.viewCore.GetCellElementByRowHandleAndColumn(this.RowHandle, this.Column);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      return new List<AutomationPeer>();
    }

    protected override bool HasKeyboardFocusCore()
    {
      if (this.DataControl.DataView.NavigationStyle == GridViewNavigationStyle.Cell && this.DataControl.DataView.FocusedRowHandle == this.RowHandle)
        return this.DataControl.CurrentColumn == this.Column;
      return false;
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.Value)
        return (object) this;
      if (patternInterface == PatternInterface.GridItem)
        return (object) this;
      if (patternInterface == PatternInterface.SelectionItem)
        return (object) this;
      return (object) null;
    }

    protected override string GetNameCore()
    {
      return string.Format(GridControlLocalizer.GetString(GridControlStringId.CellPeerName), this.DataControl.DataProviderBase.GetRowValue(this.RowHandle), (object) this.ColumnIndex, (object) ((IValueProvider) this).Value);
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.Custom;
    }

    void IValueProvider.SetValue(string value)
    {
      if (this.Column.ReadOnly)
        return;
      this.DataControl.SetCellValueCore(this.RowHandle, this.Column.FieldName, (object) value);
    }

    void ISelectionItemProvider.AddToSelection()
    {
      this.DataControl.DataView.SelectionStrategy.SelectCell(this.RowHandle, this.Column);
    }

    void ISelectionItemProvider.RemoveFromSelection()
    {
      this.DataControl.DataView.SelectionStrategy.UnselectCell(this.RowHandle, this.Column);
    }

    void ISelectionItemProvider.Select()
    {
      this.DataControl.CurrentColumn = this.Column;
      this.DataControl.DataView.SetFocusedRowHandle(this.RowHandle);
      if (this.DataControl.SelectionMode != MultiSelectMode.Cell)
        return;
      this.DataControl.BeginSelection();
      this.DataControl.UnselectAll();
      this.DataControl.DataView.SelectionStrategy.SelectCell(this.RowHandle, this.Column);
      this.DataControl.EndSelection();
    }
  }
}
