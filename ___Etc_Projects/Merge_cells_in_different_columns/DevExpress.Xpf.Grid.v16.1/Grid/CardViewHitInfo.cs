// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewHitInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about the specified element contained within the Card View.
  /// </para>
  ///             </summary>
  public class CardViewHitInfo : GridViewHitInfoBase
  {
    internal static readonly CardViewHitInfo Instance = new CardViewHitInfo((DependencyObject) null, (CardView) null);
    private CardViewHitTest? hitTest;

    /// <summary>
    ///                 <para>Gets the visual element located under the test object.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.CardViewHitTest" /> enumeration value that identifies the visual element located under the test object.
    /// </value>
    public CardViewHitTest HitTest
    {
      get
      {
        return this.hitTest ?? CardViewHitTest.None;
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a card row.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a card row; otherwise, <b>false</b>.
    /// </value>
    public override bool InRow
    {
      get
      {
        if (base.InRow)
          return true;
        return GridViewHitInfoBase.HitInfoInArea<CardViewHitTest>(this.HitTest, new CardViewHitTest[3]{ CardViewHitTest.FieldCaption, CardViewHitTest.CardHeader, CardViewHitTest.CardHeaderButton });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within the Group Panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within the group panel; otherwise, <b>false</b>.
    /// </value>
    public override bool InGroupPanel
    {
      get
      {
        if (base.InGroupPanel)
          return true;
        return GridViewHitInfoBase.HitInfoInArea<CardViewHitTest>(this.HitTest, new CardViewHitTest[1]{ CardViewHitTest.ColumnPanelShowButton });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a card.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a card; otherwise, <b>false</b>.
    /// </value>
    public bool InCardRow
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<CardViewHitTest>(this.HitTest, new CardViewHitTest[2]{ CardViewHitTest.FieldValue, CardViewHitTest.FieldCaption });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object is within a card header.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the test object is within a card header; otherwise, <b>false</b>.
    /// </value>
    public bool InCardHeader
    {
      get
      {
        return GridViewHitInfoBase.HitInfoInArea<CardViewHitTest>(this.HitTest, new CardViewHitTest[2]{ CardViewHitTest.CardHeader, CardViewHitTest.CardHeaderButton });
      }
    }

    /// <summary>
    ///                 <para>Gets whether the test object belongs to the area within a card view which is not occupied by cards.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the test object belongs to the data area; otherwise, <b>false</b>.
    /// </value>
    public override bool IsDataArea
    {
      get
      {
        return this.HitTest == CardViewHitTest.DataArea;
      }
    }

    internal CardViewHitInfo(DependencyObject d, CardView view)
      : base(d, (DataViewBase) view)
    {
    }

    protected override GridViewHitTestVisitorBase CreateDefaultVisitor()
    {
      return (GridViewHitTestVisitorBase) new HitInfoCardViewHitTestVisitor(this);
    }

    internal override void SetHitTest(TableViewHitTest hitTest)
    {
      this.SetCardHitTest(GridViewHitInfoBase.ConvertToCardViewHitTest(hitTest));
    }

    internal void SetCardHitTest(CardViewHitTest hitTest)
    {
      if (this.hitTest.HasValue)
        return;
      this.hitTest = new CardViewHitTest?(hitTest);
    }

    protected override TableViewHitTest GetTableViewHitTest()
    {
      return GridViewHitInfoBase.ConvertToTableViewHitTest(this.HitTest);
    }

    internal override bool IsRowCellCore()
    {
      return this.InCardRow;
    }
  }
}
