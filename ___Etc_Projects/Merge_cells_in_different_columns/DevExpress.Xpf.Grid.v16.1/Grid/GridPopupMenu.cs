// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridPopupMenu
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///     <para> </para>
  /// </summary>
  public class GridPopupMenu : DataControlPopupMenu
  {
    /// <summary>
    ///                 <para>Initializes a new instance of the GridPopupMenu class with the specified owner.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> object that is a popup menu owner.
    /// 
    ///           </param>
    public GridPopupMenu(GridViewBase view)
      : base((DataViewBase) view)
    {
    }

    protected override MenuInfoBase CreateMenuInfoCore(GridMenuType? type)
    {
      // ISSUE: explicit reference operation
      // ISSUE: variable of a reference type
      GridMenuType?& local = @type;
      // ISSUE: explicit reference operation
      GridMenuType valueOrDefault = (^local).GetValueOrDefault();
      // ISSUE: explicit reference operation
      if ((^local).HasValue)
      {
        switch (valueOrDefault)
        {
          case GridMenuType.Column:
            return (MenuInfoBase) new GridColumnMenuInfo(this);
          case GridMenuType.TotalSummary:
            return (MenuInfoBase) new GridTotalSummaryMenuInfo((DataControlPopupMenu) this);
          case GridMenuType.RowCell:
            return (MenuInfoBase) new GridCellMenuInfo((DataControlPopupMenu) this);
          case GridMenuType.GroupPanel:
            return (MenuInfoBase) new GridGroupPanelMenuInfo(this);
          case GridMenuType.FixedTotalSummary:
            return (MenuInfoBase) new GridTotalSummaryPanelMenuInfo((DataControlPopupMenu) this);
          case GridMenuType.Band:
            return (MenuInfoBase) new GridBandMenuInfo(this);
          case GridMenuType.GroupFooterSummary:
            return (MenuInfoBase) new GridGroupFooterSummaryMenuInfo((DataControlPopupMenu) this);
          case GridMenuType.GroupRow:
            return (MenuInfoBase) new GridGroupRowMenuInfo(this);
        }
      }
      return (MenuInfoBase) null;
    }
  }
}
