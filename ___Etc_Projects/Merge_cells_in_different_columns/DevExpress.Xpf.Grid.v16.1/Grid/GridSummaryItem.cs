// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridSummaryItem
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents a summary item.
  /// </para>
  ///             </summary>
  public class GridSummaryItem : SummaryItemBase, IDetailElement<SummaryItemBase>, IGroupFooterSummaryItem
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowInGroupColumnFooterProperty = DependencyPropertyManager.Register("ShowInGroupColumnFooter", typeof (string), typeof (GridSummaryItem), new PropertyMetadata((object) "", (PropertyChangedCallback) ((d, e) => ((SummaryItemBase) d).OnSummaryChanged(e)), (CoerceValueCallback) ((d, baseValue) =>
    {
      if (baseValue != null)
        return baseValue;
      return (object) string.Empty;
    })));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupColumnSummaryElementStyleProperty = DependencyProperty.Register("GroupColumnSummaryElementStyle", typeof (Style), typeof (GridSummaryItem), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((SummaryItemBase) d).OnSummaryChanged(e))));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty GroupColumnFooterElementStyleProperty = DependencyProperty.Register("GroupColumnFooterElementStyle", typeof (Style), typeof (GridSummaryItem), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((SummaryItemBase) d).OnSummaryChanged(e))));

    /// <summary>
    ///                 <para>Gets or sets the name of a data source field whose values are used to calculate a summary that is displayed within group footers. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A string value specifying the name of a data source field whose values are used for summary calculation.
    /// </value>
    [XtraSerializableProperty]
    public string ShowInGroupColumnFooter
    {
      get
      {
        return (string) this.GetValue(GridSummaryItem.ShowInGroupColumnFooterProperty);
      }
      set
      {
        this.SetValue(GridSummaryItem.ShowInGroupColumnFooterProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to individual text elements in the group summary item that is displayed within the group row and aligned by a column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to individual text elements in the group summary item that is aligned by a column.
    /// </value>
    public Style GroupColumnSummaryElementStyle
    {
      get
      {
        return (Style) this.GetValue(GridSummaryItem.GroupColumnSummaryElementStyleProperty);
      }
      set
      {
        this.SetValue(GridSummaryItem.GroupColumnSummaryElementStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to individual text elements in the group summary item that is displayed within the group footer. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to individual text elements in the group summary item that is displayed within the group footer.
    /// </value>
    public Style GroupColumnFooterElementStyle
    {
      get
      {
        return (Style) this.GetValue(GridSummaryItem.GroupColumnFooterElementStyleProperty);
      }
      set
      {
        this.SetValue(GridSummaryItem.GroupColumnFooterElementStyleProperty, (object) value);
      }
    }

    internal override string ActualShowInColumn
    {
      get
      {
        if (!string.IsNullOrEmpty(this.ShowInGroupColumnFooter))
          return this.ShowInGroupColumnFooter;
        return base.ActualShowInColumn;
      }
    }

    SummaryItemBase IDetailElement<SummaryItemBase>.CreateNewInstance(params object[] args)
    {
      return (SummaryItemBase) new GridSummaryItem();
    }
  }
}
