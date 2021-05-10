// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewHitInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>A class that contains information about what is located at a specific point within the <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> control.
  /// </para>
  ///             </summary>
  public class TreeListViewHitInfo : GridViewHitInfoBase, ITableViewHitInfo, IDataViewHitInfo
  {
    internal static readonly TreeListViewHitInfo Instance = new TreeListViewHitInfo((DependencyObject) null, (ITableView) null);
    private TreeListViewHitTest? hitTest;

    /// <summary>
    ///                 <para>Gets the visual element located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeList.TreeListViewHitTest" /> enumeration value that identifies the visual element located under the test object.
    /// </value>
    public TreeListViewHitTest HitTest
    {
      get
      {
        return this.hitTest ?? TreeListViewHitTest.None;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a node indent.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a node indent; otherwise, <b>false</b>.
    /// </value>
    public bool InNodeIndent
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[1]{ TreeListViewHitTest.NodeIndent });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within an expand button.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within an expand button; otherwise, <b>false</b>.
    /// </value>
    public bool InNodeExpandButton
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[1]{ TreeListViewHitTest.ExpandButton });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a node image.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a node image; otherwise, <b>false</b>.
    /// </value>
    [Obsolete("Use the InNodeImage property instead")]
    public bool InTreeListNodeImage
    {
      get
      {
        return this.InNodeImage;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a node's image.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a node's image; otherwise, <b>false</b>.
    /// </value>
    public bool InNodeImage
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[1]{ TreeListViewHitTest.NodeImage });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a node's check box.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a node's check box; otherwise, <b>false</b>.
    /// </value>
    public bool InNodeCheckbox
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[1]{ TreeListViewHitTest.NodeCheckbox });
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
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[1]{ TreeListViewHitTest.RowCell });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a node.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a node; otherwise, <b>false</b>.
    /// </value>
    public override bool InRow
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<TreeListViewHitTest>(this.HitTest, new TreeListViewHitTest[7]{ TreeListViewHitTest.RowCell, TreeListViewHitTest.Row, TreeListViewHitTest.ExpandButton, TreeListViewHitTest.NodeImage, TreeListViewHitTest.NodeCheckbox, TreeListViewHitTest.NodeIndent, TreeListViewHitTest.RowIndicator });
      }
    }

    /// <summary>
    ///                 <para>Gets a column located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumnBase" /> object that represents the column located under the test object.
    /// </value>
    public GridColumnBase Column
    {
      get
      {
        return this.columnCore as GridColumnBase;
      }
    }

    bool ITableViewHitInfo.IsRowIndicator
    {
      get
      {
        return this.HitTest == TreeListViewHitTest.RowIndicator;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object belongs to the area within a tree view which is not occupied by tree nodes.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the test object belongs to the data area; otherwise, <b>false</b>.
    /// </value>
    public override bool IsDataArea
    {
      get
      {
        return this.HitTest == TreeListViewHitTest.DataArea;
      }
    }

    internal TreeListViewHitInfo(DependencyObject d, ITableView view)
      : base(d, view != null ? view.ViewBase : (DataViewBase) null)
    {
    }

    internal static TreeListViewHitInfo CalcHitInfo(DependencyObject d, ITableView view)
    {
      return new TreeListViewHitInfo(DataViewBase.GetStartHitTestObject(d, view.ViewBase), view);
    }

    internal static TreeListViewHitInfo CalcHitInfo(Point point, ITableView view)
    {
      return new TreeListViewHitInfo(DataViewBase.GetStartHitTestObject((DependencyObject) LayoutHelper.HitTest((UIElement) view.ViewBase, point), view.ViewBase), view);
    }

    protected override GridViewHitTestVisitorBase CreateDefaultVisitor()
    {
      return (GridViewHitTestVisitorBase) new TreeListViewHitTestVisitor((GridViewHitInfoBase) this);
    }

    internal override void SetHitTest(TableViewHitTest hitTest)
    {
      this.SetTreeListHitTest(GridViewHitInfoBase.ConvertToTreeListViewHitTest(hitTest));
    }

    internal void SetHitTest(TreeListViewHitTest hitTest)
    {
      this.SetTreeListHitTest(hitTest);
    }

    internal void SetTreeListHitTest(TreeListViewHitTest hitTest)
    {
      if (this.hitTest.HasValue)
        return;
      this.hitTest = new TreeListViewHitTest?(hitTest);
    }

    protected override TableViewHitTest GetTableViewHitTest()
    {
      return GridViewHitInfoBase.ConvertToTableViewHitTest(this.HitTest);
    }

    internal override bool IsRowCellCore()
    {
      return this.HitTest == TreeListViewHitTest.RowCell;
    }
  }
}
