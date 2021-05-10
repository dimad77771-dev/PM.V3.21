// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TableViewHitInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about the specified element contained within the Table View.
  /// </para>
  ///             </summary>
  public class TableViewHitInfo : GridViewHitInfoBase, ITableViewHitInfo, IDataViewHitInfo
  {
    internal static readonly TableViewHitInfo Instance = new TableViewHitInfo((DependencyObject) null, new Point?(), (ITableView) null);
    private TableViewHitTest? hitTest;

    /// <summary>
    ///                 <para>Gets the visual element located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TableViewHitTest" /> enumeration value that identifies the visual element located under the test object.
    /// </value>
    public TableViewHitTest HitTest
    {
      get
      {
        return this.hitTest ?? TableViewHitTest.None;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a data cell.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a data cell; otherwise, <b>false</b>.
    /// </value>
    public bool InRowCell
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TableViewHitTest>(this.HitTest, new TableViewHitTest[1]{ TableViewHitTest.RowCell });
      }
    }

    bool ITableViewHitInfo.IsRowIndicator
    {
      get
      {
        return this.HitTest == TableViewHitTest.RowIndicator;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object belongs to the area within a table view which is not occupied by rows.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the test object belongs to the data area; otherwise, <b>false</b>.
    /// </value>
    public override bool IsDataArea
    {
      get
      {
        return this.HitTest == TableViewHitTest.DataArea;
      }
    }

    internal TableViewHitInfo(DependencyObject d, Point? relativePoint, ITableView view)
      : base(d, view != null ? view.ViewBase : (DataViewBase) null)
    {
      this.CorrectRowHandleIfNeeded(d, relativePoint);
    }

    internal static TableViewHitInfo CalcHitInfo(DependencyObject d, ITableView view)
    {
      return new TableViewHitInfo(DataViewBase.GetStartHitTestObject(d, view.ViewBase), new Point?(), view);
    }

    internal static TableViewHitInfo CalcHitInfo(Point point, ITableView view)
    {
      return new TableViewHitInfo(DataViewBase.GetStartHitTestObject((DependencyObject) LayoutHelper.HitTest((UIElement) view.ViewBase, point), view.ViewBase), new Point?(point), view);
    }

    private void CorrectRowHandleIfNeeded(DependencyObject d, Point? relativePoint)
    {
      if (this.view == null || !relativePoint.HasValue || (this.RowHandle == -999997 || this.RowHandle == -2147483647))
        return;
      LightweightCellEditor lightweightCellEditor = this.view.GetCellElementByTreeElement(d) as LightweightCellEditor;
      if (lightweightCellEditor == null)
        return;
      relativePoint = new Point?(this.view.TranslatePoint(relativePoint.Value, (UIElement) lightweightCellEditor));
      DataViewBase view = lightweightCellEditor.Column.View;
      if (!view.ActualAllowCellMerge)
        return;
      double actualHeight = lightweightCellEditor.ActualHeight;
      int indexByHandleCore;
      for (indexByHandleCore = view.DataControl.GetRowVisibleIndexByHandleCore(this.RowHandle); view.IsPrevRowCellMerged(indexByHandleCore, (ColumnBase) this.Column, true); --indexByHandleCore)
      {
        FrameworkElement elementByRowHandle = view.GetRowElementByRowHandle(view.DataControl.GetRowHandleByVisibleIndexCore(indexByHandleCore));
        if (relativePoint.Value.Y <= actualHeight - elementByRowHandle.ActualHeight)
          actualHeight -= elementByRowHandle.ActualHeight;
        else
          break;
      }
      this.RowHandle = view.DataControl.GetRowHandleByVisibleIndexCore(indexByHandleCore);
    }

    protected override GridViewHitTestVisitorBase CreateDefaultVisitor()
    {
      return (GridViewHitTestVisitorBase) new HitInfoTableViewHitTestVisitor(this);
    }

    internal override void SetHitTest(TableViewHitTest hitTest)
    {
      if (this.hitTest.HasValue)
        return;
      this.hitTest = new TableViewHitTest?(hitTest);
    }

    protected override TableViewHitTest GetTableViewHitTest()
    {
      return this.HitTest;
    }

    internal override bool IsRowCellCore()
    {
      return this.HitTest == TableViewHitTest.RowCell;
    }
  }
}
