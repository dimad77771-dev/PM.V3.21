// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains information about a card.
  /// </para>
  ///             </summary>
  public class CardData : RowData
  {
    private static readonly DependencyPropertyKey CardHeaderDataPropertyKey = DependencyProperty.RegisterReadOnly("CardHeaderData", typeof (CardHeaderData), typeof (CardData), (PropertyMetadata) null);
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardHeaderDataProperty = CardData.CardHeaderDataPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey IsExpandedPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsExpanded", typeof (bool), typeof (CardData), (PropertyMetadata) new FrameworkPropertyMetadata((object) true, new PropertyChangedCallback(CardData.OnIsExpandedChanged)));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsExpandedProperty = CardData.IsExpandedPropertyKey.DependencyProperty;

    /// <summary>
    ///                 <para>Gets the <see cref="T:DevExpress.Xpf.Grid.CardHeaderData" /> object that contains the card header's data.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.CardHeaderData" /> object that contains the card header's data.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardDataCardHeaderData")]
    public CardHeaderData CardHeaderData
    {
      get
      {
        return (CardHeaderData) this.GetValue(CardData.CardHeaderDataProperty);
      }
      private set
      {
        this.SetValue(CardData.CardHeaderDataPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the card is expanded.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the card is expanded; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardDataIsExpanded")]
    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(CardData.IsExpandedProperty);
      }
      internal set
      {
        this.SetValue(CardData.IsExpandedPropertyKey, (object) value);
      }
    }

    private CardView CardView
    {
      get
      {
        return (CardView) this.View;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the CardData class.
    /// </para>
    ///             </summary>
    /// <param name="treeBuilder">
    /// 
    /// 
    /// </param>
    public CardData(DataTreeBuilder treeBuilder)
      : base(treeBuilder, false, true)
    {
      this.CardHeaderData = new CardHeaderData();
      BindingOperations.SetBinding((DependencyObject) this.CardHeaderData, CardHeaderData.DataInternalProperty, (BindingBase) new Binding("DataContext")
      {
        Source = (object) this
      });
      this.CardHeaderData.Binding = ((CardView) this.View).CardHeaderBinding;
      this.CardHeaderData.RowData = (RowData) this;
    }

    private static void OnIsExpandedChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      CardData cardData = d as CardData;
      if (cardData == null)
        return;
      cardData.RaiseContentChanged();
    }

    internal static void SetIsExpanded(DependencyObject d, bool value)
    {
      d.SetValue(CardData.IsExpandedPropertyKey, (object) value);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified card is expanded.
    /// </para>
    ///             </summary>
    /// <param name="d">
    /// A CardData object that corresponds to the card whose expanded state is returned.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified card is expanded; otherwise, <b>false</b>.
    /// </returns>
    public static bool GetIsExpanded(DependencyObject d)
    {
      return (bool) d.GetValue(CardData.IsExpandedProperty);
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedLeftCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedRightCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedNoneCellData(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    internal void ChangeExpaned()
    {
      this.CardView.ChangeCardExpanded(this.RowHandle.Value);
    }

    internal void UpdateIsExpanded()
    {
      this.IsExpanded = this.GetCardExpanded();
    }

    private bool GetCardExpanded()
    {
      return CardData.GetIsExpanded(this.RowState);
    }

    internal override void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate)
    {
      base.AssignFrom(parentRowsContainer, parentNodeContainer, rowNode, forceUpdate);
      this.UpdateIsExpanded();
    }

    protected override FrameworkElement CreateRowElement()
    {
      return (FrameworkElement) new GridCard();
    }
  }
}
