// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardHeaderData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains a card header's data.
  /// </para>
  ///             </summary>
  public class CardHeaderData : GridDataBase
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BindingProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<CardHeaderData, BindingBase>("Binding", (BindingBase) null, (DependencyPropertyChangedCallback<CardHeaderData, BindingBase>) ((d, e) => d.UpdateValue(false)));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ValueProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<CardHeaderData, object>("Value", (object) null, (DependencyPropertyChangedCallback<CardHeaderData, object>) ((d, e) => d.OnContentChanged()));
    [IgnoreDependencyPropertiesConsistencyChecker]
    internal static readonly DependencyProperty DataInternalProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<CardHeaderData, object>("DataInternal", (object) null, (DependencyPropertyChangedCallback<CardHeaderData, object>) ((d, e) => d.Data = e.NewValue));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty RowDataProperty = DevExpress.Xpf.Core.Native.DependencyPropertyHelper.RegisterProperty<CardHeaderData, RowData>("RowData", (RowData) null);

    /// <summary>
    ///                 <para>Gets or sets the binding.
    /// </para>
    ///             </summary>
    /// <value>The binding.</value>
    [DevExpressXpfGridLocalizedDescription("CardHeaderDataBinding")]
    public BindingBase Binding
    {
      get
      {
        return (BindingBase) this.GetValue(CardHeaderData.BindingProperty);
      }
      set
      {
        this.SetValue(CardHeaderData.BindingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the card's value.
    /// </para>
    ///             </summary>
    /// <value>The card's value.</value>
    public new object Value
    {
      get
      {
        return this.GetValue(CardHeaderData.ValueProperty);
      }
      set
      {
        this.SetValue(CardHeaderData.ValueProperty, value);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    public RowData RowData
    {
      get
      {
        return (RowData) this.GetValue(CardHeaderData.RowDataProperty);
      }
      set
      {
        this.SetValue(CardHeaderData.RowDataProperty, (object) value);
      }
    }

    protected internal override void UpdateValue(bool forceUpdate = false)
    {
      this.ClearValue(CardHeaderData.ValueProperty);
      if (this.Binding == null || this.Data == null)
        return;
      BindingOperations.SetBinding((DependencyObject) this, CardHeaderData.ValueProperty, this.Binding);
    }
  }
}
