// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridViewHitTestVisitorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public abstract class GridViewHitTestVisitorBase : DataViewHitTestVisitorBase
  {
    internal GridViewHitTestVisitorBase(IDataViewHitInfo hitInfo)
      : base(hitInfo)
    {
    }

    public virtual void VisitGroupRow(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupRow);
      this.hitInfo.SetRowHandle(rowHandle);
      this.StopHitTestingCore();
    }

    public virtual void VisitGroupFooterSummaryRow(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupFooterRow);
      this.hitInfo.SetRowHandle(rowHandle);
    }

    public virtual void VisitGroupFooterSummary(int rowHandle, ColumnBase column)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupFooterSummary);
      this.hitInfo.SetRowHandle(rowHandle);
      this.hitInfo.SetColumn(column);
    }

    public virtual void VisitGroupRowButton(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupRowButton);
      this.hitInfo.SetRowHandle(rowHandle);
    }

    public virtual void VisitMasterRowButton(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.MasterRowButton);
      this.hitInfo.SetRowHandle(rowHandle);
    }

    public virtual void VisitGroupRowCheckBox(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupRowCheckBox);
      this.hitInfo.SetRowHandle(rowHandle);
    }

    public virtual void VisitGroupPanel()
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupPanel);
    }

    public virtual void VisitGroupPanelColumnHeader(GridColumn column)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupPanelColumnHeader);
      this.hitInfo.SetColumn((ColumnBase) column);
    }

    public virtual void VisitGroupPanelColumnHeaderFilterButton(GridColumn column)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupPanelColumnHeaderFilterButton);
    }

    public virtual void VisitGroupValue(int rowHandle, GridColumnData columnData)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupValue);
    }

    public virtual void VisitGroupSummary(int rowHandle, GridGroupSummaryData summaryData)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.GroupSummary);
    }
  }
}
