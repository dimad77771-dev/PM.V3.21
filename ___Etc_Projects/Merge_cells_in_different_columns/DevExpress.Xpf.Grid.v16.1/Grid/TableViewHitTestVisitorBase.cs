// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TableViewHitTestVisitorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public abstract class TableViewHitTestVisitorBase : GridViewHitTestVisitorBase
  {
    protected TableViewHitTestVisitorBase()
      : base((IDataViewHitInfo) TableViewHitInfo.Instance)
    {
    }

    internal TableViewHitTestVisitorBase(GridViewHitInfoBase hitInfo)
      : base((IDataViewHitInfo) hitInfo)
    {
    }

    public virtual void VisitColumnButton()
    {
      this.hitInfo.SetHitTest(TableViewHitTest.ColumnButton);
    }

    public virtual void VisitBandButton()
    {
      this.hitInfo.SetHitTest(TableViewHitTest.BandButton);
    }

    public virtual void VisitColumnEdge(BaseColumn column)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.ColumnEdge);
    }

    public virtual void VisitBandEdge(BandBase band)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.BandEdge);
    }

    public virtual void VisitFixedLeftDiv(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.FixedLeftDiv);
    }

    public virtual void VisitFixedRightDiv(int rowHandle)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.FixedRightDiv);
    }

    public virtual void VisitRowIndicator(int rowHandle, IndicatorState indicatorState)
    {
      this.hitInfo.SetHitTest(TableViewHitTest.RowIndicator);
      this.hitInfo.SetRowHandle(rowHandle);
      this.StopHitTestingCore();
    }

    public virtual void VisitDataNavigator()
    {
      this.hitInfo.SetHitTest(TableViewHitTest.DataNavigator);
    }
  }
}
