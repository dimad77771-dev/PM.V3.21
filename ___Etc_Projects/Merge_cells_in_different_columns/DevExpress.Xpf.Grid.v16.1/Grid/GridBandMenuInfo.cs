// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridBandMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about a context menu invoked for a grid band.
  /// </para>
  ///             </summary>
  public class GridBandMenuInfo : GridColumnMenuInfo
  {
    /// <summary>
    ///                 <para>Gets the type of the invoked context menu.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridMenuType" /> object.
    /// </value>
    public override GridMenuType MenuType
    {
      get
      {
        return GridMenuType.Band;
      }
    }

    /// <summary>
    ///                 <para>Gets the menu controller for the invoked context menu.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Bars.BarManagerMenuController" /> object.
    /// </value>
    public override BarManagerMenuController MenuController
    {
      get
      {
        return ((TableView) this.View).BandMenuController;
      }
    }

    /// <summary>
    ///                 <para>Gets the band for which the context menu has been invoked.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.BandBase" /> object.
    /// </value>
    public BandBase Band
    {
      get
      {
        return (BandBase) this.BaseColumn;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridBandMenuInfo class with the specified settings.
    /// </para>
    ///             </summary>
    /// <param name="menu">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridPopupMenu" /> object.
    /// 
    ///           </param>
    public GridBandMenuInfo(GridPopupMenu menu)
      : base(menu)
    {
    }

    protected override void CreateItemsCore()
    {
      this.CreateGroupPanelItem();
      this.CreateColumnChooserItems();
      this.CreateFilterEditorItem(true);
      this.CreateSearchPanelItems();
      this.CreateFixedStyleItems();
    }

    protected override bool CanCreateFixedStyleMenu()
    {
      return this.Band.Owner == this.DataControl.BandsLayoutCore;
    }
  }
}
