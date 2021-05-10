// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewHitTestVisitorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  public abstract class CardViewHitTestVisitorBase : GridViewHitTestVisitorBase
  {
    private CardViewHitInfo CardHitInfo
    {
      get
      {
        return (CardViewHitInfo) this.hitInfo;
      }
    }

    protected CardViewHitTestVisitorBase()
      : base((IDataViewHitInfo) CardViewHitInfo.Instance)
    {
    }

    internal CardViewHitTestVisitorBase(GridViewHitInfoBase hitInfo)
      : base((IDataViewHitInfo) hitInfo)
    {
    }

    public virtual void VisitFieldCaption(int rowHandle, GridColumn column)
    {
      this.CardHitInfo.SetCardHitTest(CardViewHitTest.FieldCaption);
      this.hitInfo.SetColumn((ColumnBase) column);
    }

    public virtual void VisitCardHeader(int rowHandle)
    {
      this.CardHitInfo.SetCardHitTest(CardViewHitTest.CardHeader);
    }

    public virtual void VisitCardHeaderButton(int rowHandle)
    {
      this.CardHitInfo.SetCardHitTest(CardViewHitTest.CardHeaderButton);
    }

    public virtual void VisitSeparator()
    {
      this.CardHitInfo.SetCardHitTest(CardViewHitTest.Separator);
    }

    public virtual void VisitColumnPanelShowButton()
    {
      this.CardHitInfo.SetCardHitTest(CardViewHitTest.ColumnPanelShowButton);
    }

    public override sealed void VisitRowCell(int rowHandle, ColumnBase column)
    {
      base.VisitRowCell(rowHandle, column);
      this.VisitFieldValue(rowHandle, column);
    }

    public override sealed void VisitRow(int rowHandle)
    {
      base.VisitRow(rowHandle);
      this.VisitCard(rowHandle);
    }

    public virtual void VisitFieldValue(int rowHandle, ColumnBase column)
    {
    }

    public virtual void VisitCard(int rowHandle)
    {
    }
  }
}
