// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListViewHitTestVisitorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;

namespace DevExpress.Xpf.Grid.TreeList
{
  public abstract class TreeListViewHitTestVisitorBase : TableViewHitTestVisitorBase
  {
    private TreeListViewHitInfo TreeListHitInfo
    {
      get
      {
        return (TreeListViewHitInfo) this.hitInfo;
      }
    }

    protected TreeListViewHitTestVisitorBase()
      : base((GridViewHitInfoBase) TreeListViewHitInfo.Instance)
    {
    }

    internal TreeListViewHitTestVisitorBase(GridViewHitInfoBase hitInfo)
      : base(hitInfo)
    {
    }

    public void VisitTreeListNodeExpandButton(TreeListNodeExpandButton expandButton)
    {
      this.TreeListHitInfo.SetTreeListHitTest(TreeListViewHitTest.ExpandButton);
    }

    public void VisitTreeListRowMarginControl(RowMarginControl marginControl)
    {
      this.TreeListHitInfo.SetTreeListHitTest(TreeListViewHitTest.NodeIndent);
    }

    public void VisitTreeListNodeImage(RowHandle rowHandle)
    {
      this.TreeListHitInfo.SetTreeListHitTest(TreeListViewHitTest.NodeImage);
    }

    public void VisitTreeListNodeCheckbox(RowHandle rowHandle)
    {
      this.TreeListHitInfo.SetTreeListHitTest(TreeListViewHitTest.NodeCheckbox);
    }
  }
}
