// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.SearchTokenLookUpEditStyleSettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Defines the appearance and behavior of the <b>SearchTokenLookUpEdit</b>.
  /// </para>
  ///             </summary>
  public class SearchTokenLookUpEditStyleSettings : SearchLookUpEditStyleSettings, ITokenStyleSettings
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowTokenButtonsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TokenBorderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EnableTokenWrappingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty NewTokenPositionProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TokenTextTrimmingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TokenMaxWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowEditTokensProperty;

    /// <summary>
    ///                 <para>Gets or sets whether to enable editing of existing tokens. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable editing of existing tokens; otherwise, <b>false</b>.
    /// </value>
    public bool? AllowEditTokens
    {
      get
      {
        return (bool?) this.GetValue(SearchTokenLookUpEditStyleSettings.AllowEditTokensProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.AllowEditTokensProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the token's maximum width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the token's maximum width.
    /// </value>
    public double? TokenMaxWidth
    {
      get
      {
        return (double?) this.GetValue(SearchTokenLookUpEditStyleSettings.TokenMaxWidthProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.TokenMaxWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the trimming behavior for tokens. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A System.Windows.TextTrimming enumeration value.</value>
    public TextTrimming? TokenTextTrimming
    {
      get
      {
        return (TextTrimming?) this.GetValue(SearchTokenLookUpEditStyleSettings.TokenTextTrimmingProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.TokenTextTrimmingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the placement of new tokens. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Editors.NewTokenPosition" /> enumeration value.
    /// </value>
    public DevExpress.Xpf.Editors.NewTokenPosition? NewTokenPosition
    {
      get
      {
        return (DevExpress.Xpf.Editors.NewTokenPosition?) this.GetValue(SearchTokenLookUpEditStyleSettings.NewTokenPositionProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.NewTokenPositionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable token wrapping. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable token wrapping; otherwise, <b>false</b>.
    /// </value>
    public bool? EnableTokenWrapping
    {
      get
      {
        return (bool?) this.GetValue(SearchTokenLookUpEditStyleSettings.EnableTokenWrappingProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.EnableTokenWrappingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the template used to represent a gallery item's borders on-screen. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that is the corresponding template.
    /// </value>
    public ControlTemplate TokenBorderTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(SearchTokenLookUpEditStyleSettings.TokenBorderTemplateProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.TokenBorderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to display the delete button within tokens. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the delete button; otherwise, <b>false</b>.
    /// </value>
    public bool? ShowTokenButtons
    {
      get
      {
        return (bool?) this.GetValue(SearchTokenLookUpEditStyleSettings.ShowTokenButtonsProperty);
      }
      set
      {
        this.SetValue(SearchTokenLookUpEditStyleSettings.ShowTokenButtonsProperty, (object) value);
      }
    }

    static SearchTokenLookUpEditStyleSettings()
    {
      Type ownerType = typeof (SearchTokenLookUpEditStyleSettings);
      SearchTokenLookUpEditStyleSettings.EnableTokenWrappingProperty = DependencyProperty.Register("EnableTokenWrapping", typeof (bool?), ownerType);
      SearchTokenLookUpEditStyleSettings.TokenBorderTemplateProperty = DependencyProperty.Register("TokenBorderTemplate", typeof (ControlTemplate), ownerType);
      SearchTokenLookUpEditStyleSettings.ShowTokenButtonsProperty = DependencyProperty.Register("ShowTokenButtons", typeof (bool?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      SearchTokenLookUpEditStyleSettings.NewTokenPositionProperty = DependencyProperty.Register("NewTokenPosition", typeof (DevExpress.Xpf.Editors.NewTokenPosition?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      SearchTokenLookUpEditStyleSettings.TokenTextTrimmingProperty = DependencyProperty.Register("TokenTextTrimming", typeof (TextTrimming?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      SearchTokenLookUpEditStyleSettings.TokenMaxWidthProperty = DependencyProperty.Register("TokenMaxWidth", typeof (double?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      SearchTokenLookUpEditStyleSettings.AllowEditTokensProperty = DependencyProperty.Register("AllowEditTokens", typeof (bool?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public override bool IsTokenStyleSettings()
    {
      return true;
    }

    protected override bool GetActualAllowDefaultButton(ButtonEdit editor)
    {
      return !((LookUpEditBasePropertyProvider) ActualPropertyProvider.GetProperties((DependencyObject) editor)).EnableTokenWrapping;
    }
  }
}
