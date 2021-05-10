// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumnHeader
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Utils;
using System;
using System.ComponentModel;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents a column's header.
  /// </para>
  ///             </summary>
  public class GridColumnHeader : BaseGridColumnHeader
  {
    private Lazy<DataViewHitTestAcceptorBase> defaultFilterHitTestAcceptor = new Lazy<DataViewHitTestAcceptorBase>((Func<DataViewHitTestAcceptorBase>) (() => (DataViewHitTestAcceptorBase) new ColumnHeaderFilterButtonTableViewHitTestAcceptor()));
    private Lazy<DataViewHitTestAcceptorBase> groupPanelFilterHitTestAcceptor = new Lazy<DataViewHitTestAcceptorBase>((Func<DataViewHitTestAcceptorBase>) (() => (DataViewHitTestAcceptorBase) new GroupPanelColumnHeaderFilterButtonTableViewHitTestAcceptor()));

    /// <summary>
    ///                 <para>Initializes a new instance of the GridColumnHeader class with default settings.
    /// </para>
    ///             </summary>
    public GridColumnHeader()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (GridColumnHeader));
    }

    protected override FrameworkElement CreateSortIndicator()
    {
      return (FrameworkElement) new SortIndicatorControl();
    }

    protected override void UpdateSortIndicator(bool isAscending)
    {
      ((SortIndicatorControl) this.SortIndicator).SortOrder = isAscending ? ListSortDirection.Ascending : ListSortDirection.Descending;
    }

    protected override DXThumb CreateGripper()
    {
      return (DXThumb) new GridThumb();
    }

    protected override XPFContentControl CreateCustomHeaderPresenter()
    {
      return (XPFContentControl) new HeaderContentControl();
    }

    protected override FrameworkElement CreateDesignTimeSelectionControl()
    {
      DesignTimeSelectionControl selectionControl = new DesignTimeSelectionControl();
      selectionControl.IsTabStop = false;
      return (FrameworkElement) selectionControl;
    }

    protected override void SetFilterHitTestAcceptor(PopupBaseEdit popup)
    {
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) popup, this.HeaderPresenterType == HeaderPresenterType.GroupPanel ? this.groupPanelFilterHitTestAcceptor.Value : this.defaultFilterHitTestAcceptor.Value);
    }
  }
}
