// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridViewHitInfoBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.TreeList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Serves as the base for classes providing information about a particular view element.
  /// </para>
  ///             </summary>
  public abstract class GridViewHitInfoBase : HitInfoBase<DataViewHitTestVisitorBase>, IDataViewHitInfo
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty HitTestAcceptorProperty = DependencyProperty.RegisterAttached("HitTestAcceptor", typeof (DataViewHitTestAcceptorBase), typeof (GridViewHitInfoBase), new PropertyMetadata((PropertyChangedCallback) null));
    internal readonly DataViewBase view;
    private readonly bool isEmpty;
    protected ColumnBase columnCore;
    protected BandBase bandCore;

    /// <summary>
    ///                 <para>Gets the handle of a row that contains the test object.
    /// </para>
    ///             </summary>
    /// <value>An integer value the specifies the row's handle.</value>
    public int RowHandle { get; internal set; }

    ColumnBase IDataViewHitInfo.Column
    {
      get
      {
        return this.columnCore;
      }
    }

    /// <summary>
    ///                 <para>Gets a column located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column located under the test object.
    /// </value>
    public GridColumn Column
    {
      get
      {
        return (GridColumn) this.columnCore;
      }
    }

    /// <summary>
    ///                 <para>Gets a band located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.BandBase" /> object that represents the column located under the test object.
    /// </value>
    public BandBase Band
    {
      get
      {
        if (this.columnCore != null)
          return this.columnCore.ParentBand;
        return this.bandCore;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within the Filter Panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test point is within the filter panel; otherwise, <b>false</b>.
    /// </value>
    public bool InFilterPanel
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.FilterPanel, TableViewHitTest.FilterPanelText, TableViewHitTest.FilterPanelCloseButton, TableViewHitTest.FilterPanelCustomizeButton, TableViewHitTest.FilterPanelActiveButton, TableViewHitTest.MRUFilterListComboBox);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within the Group Panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test point is within the group panel; otherwise, <b>false</b>.
    /// </value>
    public virtual bool InGroupPanel
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.GroupPanel, TableViewHitTest.GroupPanelColumnHeader, TableViewHitTest.GroupPanelColumnHeaderFilterButton);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within the column header panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test point is within the column header panel; otherwise, <b>false</b>.
    /// </value>
    public bool InColumnPanel
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.ColumnHeaderPanel, TableViewHitTest.ColumnButton, TableViewHitTest.ColumnHeader, TableViewHitTest.ColumnHeaderFilterButton, TableViewHitTest.ColumnEdge);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within the band panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the test point is inside the band panel; otherwise, <b>false</b>.
    /// 
    /// </value>
    public bool InBandPanel
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.BandHeaderPanel, TableViewHitTest.BandButton, TableViewHitTest.BandHeader, TableViewHitTest.BandEdge);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a group column.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a group column; otherwise, <b>false</b>.
    /// </value>
    public bool InGroupColumn
    {
      get
      {
        return this.HitInfoInArea(new TableViewHitTest[2]{ TableViewHitTest.GroupPanelColumnHeader, TableViewHitTest.GroupPanelColumnHeaderFilterButton });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a column's header.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test point is within a column's header; otherwise, <b>false</b>.
    /// </value>
    public bool InColumnHeader
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.ColumnHeader, TableViewHitTest.ColumnHeaderFilterButton, TableViewHitTest.ColumnEdge);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a row.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a row; otherwise, <b>false</b>.
    /// </value>
    public virtual bool InRow
    {
      get
      {
        return this.HitInfoInArea(TableViewHitTest.RowCell, TableViewHitTest.Row, TableViewHitTest.GroupRow, TableViewHitTest.GroupRowButton, TableViewHitTest.GroupValue, TableViewHitTest.GroupSummary, TableViewHitTest.RowIndicator);
      }
    }

    bool IDataViewHitInfo.IsRowCell
    {
      get
      {
        return this.IsRowCellCore();
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object belongs to the area within a view.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the test object belongs to the data area; otherwise, <b>false</b>.
    /// </value>
    public virtual bool IsDataArea
    {
      get
      {
        return false;
      }
    }

    protected GridViewHitInfoBase(DependencyObject d, DataViewBase view)
      : base(d, (DependencyObject) view)
    {
      this.RowHandle = int.MinValue;
      this.Accept((DataViewHitTestVisitorBase) this.CreateDefaultVisitor());
      this.isEmpty = view == null;
      this.view = view;
    }

    /// <summary>
    ///                 <para>Gets the value of the <see cref="P:DevExpress.Xpf.Grid.GridViewHitInfoBase.HitTestAcceptor" /> attached property for a specified <see cref="T:System.Windows.DependencyObject" />.
    /// </para>
    ///             </summary>
    /// <param name="obj">The element from which the property value is read.</param>
    /// <returns>The <see cref="P:DevExpress.Xpf.Grid.GridViewHitInfoBase.HitTestAcceptor" /> property value for the element.
    /// </returns>
    public static DataViewHitTestAcceptorBase GetHitTestAcceptor(DependencyObject obj)
    {
      return (DataViewHitTestAcceptorBase) obj.GetValue(GridViewHitInfoBase.HitTestAcceptorProperty);
    }

    /// <summary>
    ///                 <para>Sets the value of the <see cref="P:DevExpress.Xpf.Grid.GridViewHitInfoBase.HitTestAcceptor" /> attached property to a specified <see cref="T:System.Windows.DependencyObject" />.
    /// </para>
    ///             </summary>
    /// <param name="obj">The element to which the attached property is written.</param>
    /// <param name="value">
    /// The <see cref="T:DevExpress.Xpf.Grid.HitTest.DataViewHitTestAcceptorBase" /> descendant.
    /// 
    ///           </param>
    public static void SetHitTestAcceptor(DependencyObject obj, DataViewHitTestAcceptorBase value)
    {
      obj.SetValue(GridViewHitInfoBase.HitTestAcceptorProperty, (object) value);
    }

    internal static void SetHitTestAcceptorSafe(DependencyObject obj, DataViewHitTestAcceptorBase value)
    {
      if (obj == null)
        return;
      obj.SetValue(GridViewHitInfoBase.HitTestAcceptorProperty, (object) value);
    }

    protected static bool HitInfoInArea<T>(T value, params T[] areaTypes)
    {
      return ((IEnumerable<T>) areaTypes).Contains<T>(value);
    }

    internal static TreeListViewHitTest ConvertToTreeListViewHitTest(TableViewHitTest hitTest)
    {
      if (!Enum.IsDefined(typeof (TreeListViewHitTest), (object) hitTest.ToString()))
        return TreeListViewHitTest.None;
      return (TreeListViewHitTest) Enum.Parse(typeof (TreeListViewHitTest), hitTest.ToString(), true);
    }

    internal static TableViewHitTest ConvertToTableViewHitTest(TreeListViewHitTest hitTest)
    {
      switch (hitTest)
      {
        case TreeListViewHitTest.NodeIndent:
        case TreeListViewHitTest.ExpandButton:
        case TreeListViewHitTest.NodeImage:
          return TableViewHitTest.Row;
        default:
          if (!Enum.IsDefined(typeof (TableViewHitTest), (object) hitTest.ToString()))
            return TableViewHitTest.None;
          return (TableViewHitTest) Enum.Parse(typeof (TableViewHitTest), hitTest.ToString(), true);
      }
    }

    internal static CardViewHitTest ConvertToCardViewHitTest(TableViewHitTest hitTest)
    {
      switch (hitTest)
      {
        case TableViewHitTest.RowCell:
          return CardViewHitTest.FieldValue;
        case TableViewHitTest.Row:
          return CardViewHitTest.Card;
        default:
          return (CardViewHitTest) Enum.Parse(typeof (CardViewHitTest), hitTest.ToString(), true);
      }
    }

    internal static TableViewHitTest ConvertToTableViewHitTest(CardViewHitTest hitTest)
    {
      switch (hitTest)
      {
        case CardViewHitTest.FieldValue:
          return TableViewHitTest.RowCell;
        case CardViewHitTest.Card:
          return TableViewHitTest.Row;
        default:
          if (!Enum.IsDefined(typeof (TableViewHitTest), (object) hitTest.ToString()))
            return TableViewHitTest.None;
          return (TableViewHitTest) Enum.Parse(typeof (TableViewHitTest), hitTest.ToString(), true);
      }
    }

    internal virtual void SetHitTest(TableViewHitTest hitTest)
    {
    }

    internal void SetColumn(ColumnBase value)
    {
      if (this.isEmpty)
        return;
      this.columnCore = value;
    }

    internal void SetBand(BandBase value)
    {
      if (this.isEmpty)
        return;
      this.bandCore = value;
    }

    protected override sealed HitTestAcceptorBase<DataViewHitTestVisitorBase> GetAcceptor(DependencyObject treeElement)
    {
      return (HitTestAcceptorBase<DataViewHitTestVisitorBase>) GridViewHitInfoBase.GetHitTestAcceptor(treeElement);
    }

    protected abstract GridViewHitTestVisitorBase CreateDefaultVisitor();

    private bool HitInfoInArea(params TableViewHitTest[] areaTypes)
    {
      return GridViewHitInfoBase.HitInfoInArea<TableViewHitTest>(this.GetTableViewHitTest(), areaTypes);
    }

    protected abstract TableViewHitTest GetTableViewHitTest();

    void IDataViewHitInfo.SetHitTest(TableViewHitTest hitTest)
    {
      this.SetHitTest(hitTest);
    }

    void IDataViewHitInfo.SetColumn(ColumnBase column)
    {
      this.SetColumn(column);
    }

    void IDataViewHitInfo.SetBand(BandBase band)
    {
      this.SetBand(band);
    }

    void IDataViewHitInfo.SetRowHandle(int rowHandle)
    {
      this.RowHandle = rowHandle;
    }

    internal abstract bool IsRowCellCore();
  }
}
