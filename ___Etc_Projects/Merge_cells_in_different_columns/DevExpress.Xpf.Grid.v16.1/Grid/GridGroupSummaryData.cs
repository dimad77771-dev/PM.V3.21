// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupSummaryData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about the summary value displayed within a group row.
  /// </para>
  ///             </summary>
  public class GridGroupSummaryData : GridColumnData
  {
    private static readonly DependencyPropertyKey SummaryItemPropertyKey = DependencyPropertyManager.RegisterReadOnly("SummaryItem", typeof (GridSummaryItem), typeof (GridGroupSummaryData), new PropertyMetadata((object) null, new PropertyChangedCallback(EditableDataObject.OnContentChanged)));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SummaryItemProperty = GridGroupSummaryData.SummaryItemPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey SummaryValuePropertyKey = DependencyPropertyManager.RegisterReadOnly("SummaryValue", typeof (object), typeof (GridGroupSummaryData), new PropertyMetadata((object) null, new PropertyChangedCallback(EditableDataObject.OnContentChanged)));
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummaryData.SummaryValue" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SummaryValueProperty = GridGroupSummaryData.SummaryValuePropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey TextPropertyKey = DependencyPropertyManager.RegisterReadOnly("Text", typeof (string), typeof (GridGroupSummaryData), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridGroupSummaryData) d).OnTextAffectingPropertyChanged())));
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridGroupSummaryData.Text" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TextProperty = GridGroupSummaryData.TextPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsLastPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsLast", typeof (bool), typeof (GridGroupSummaryData), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridGroupSummaryData) d).OnTextAffectingPropertyChanged())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsLastProperty = GridGroupSummaryData.IsLastPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsFirstPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsFirst", typeof (bool), typeof (GridGroupSummaryData), new PropertyMetadata((object) false, new PropertyChangedCallback(EditableDataObject.OnContentChanged)));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsFirstProperty = GridGroupSummaryData.IsFirstPropertyKey.DependencyProperty;
    private IGroupValueClient client;

    /// <summary>
    ///                 <para>Gets the summary item.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItem" /> object that represents the summary item.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummaryDataSummaryItem")]
    public GridSummaryItem SummaryItem
    {
      get
      {
        return (GridSummaryItem) this.GetValue(GridGroupSummaryData.SummaryItemProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryData.SummaryItemPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the group row's summary value.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the group row's summary value.</value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummaryDataSummaryValue")]
    public object SummaryValue
    {
      get
      {
        return this.GetValue(GridGroupSummaryData.SummaryValueProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryData.SummaryValuePropertyKey, value);
      }
    }

    /// <summary>
    ///                 <para>Gets the summary text displayed within the group row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that represents the summary text displayed within the group row.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummaryDataText")]
    public string Text
    {
      get
      {
        return (string) this.GetValue(GridGroupSummaryData.TextProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryData.TextPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the group summary is the last visible summary (right most) displayed within a group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the group summary is the last visible summary (right most) displayed within a group row; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummaryDataIsLast")]
    public bool IsLast
    {
      get
      {
        return (bool) this.GetValue(GridGroupSummaryData.IsLastProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryData.IsLastPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the group summary is the first visible summary (left most) displayed within a group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the group summary is the first visible summary (left most) displayed within a group row; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridGroupSummaryDataIsFirst")]
    public bool IsFirst
    {
      get
      {
        return (bool) this.GetValue(GridGroupSummaryData.IsFirstProperty);
      }
      internal set
      {
        this.SetValue(GridGroupSummaryData.IsFirstPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridGroupSummaryData class.
    /// </para>
    ///             </summary>
    /// <param name="rowData">
    /// 
    /// 
    /// </param>
    public GridGroupSummaryData(ColumnsRowDataBase rowData)
      : base(rowData)
    {
    }

    internal void SetGroupValueClient(IGroupValueClient client)
    {
      this.client = client;
    }

    private void OnTextAffectingPropertyChanged()
    {
      this.OnContentChanged();
      if (this.client == null)
        return;
      this.client.UpdateText();
    }
  }
}
