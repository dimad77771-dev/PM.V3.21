// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardView
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Represents a View that displays data using cards.
  /// </para>
  ///             </summary>
  public class CardView : GridViewBase
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ScrollModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedSizeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty MaxCardCountInRowProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardAlignmentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardMarginProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ShowCardExpandButtonProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardStyleProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualCardTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.ActualCardTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualCardTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardHeaderTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardHeaderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardHeaderTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardHeaderTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualCardHeaderTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.ActualCardHeaderTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualCardHeaderTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardRowTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CardRowTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardRowTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualCardRowTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.ActualCardRowTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualCardRowTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.MinFixedSize" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty MinFixedSizeProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.AllowCardResizing" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowCardResizingProperty;
    private static readonly DependencyPropertyKey IsResizingEnabledPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.IsResizingEnabled" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsResizingEnabledProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.SeparatorTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SeparatorTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.SeparatorThickness" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SeparatorThicknessProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CardLayoutProperty;
    private static readonly DependencyPropertyKey OrientationPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty OrientationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedCardBorderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedCellBorderCardViewTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty VerticalFocusedGroupRowBorderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty MultiSelectModeProperty;
    private static readonly DependencyPropertyKey CollapsedCardOrientationPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.CardView.CollapsedCardOrientation" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CollapsedCardOrientationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty UseLightweightTemplatesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty LeftGroupAreaIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardContentTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardHeaderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardRowIndentTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintCardMarginProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintAutoCardWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintMaximumCardColumnsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintTotalSummarySeparatorStyleProperty;
    private Point lastNavPoint;
    private CardHeaderData cardHeaderData;

    /// <summary>
    ///                 <para>Gets the orientation of collapsed cards. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.CollapsedCardOrientation" /> enumeration member that specifies the orientation of collapsed cards.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewCollapsedCardOrientation")]
    public CollapsedCardOrientation CollapsedCardOrientation
    {
      get
      {
        return (CollapsedCardOrientation) this.GetValue(CardView.CollapsedCardOrientationProperty);
      }
      internal set
      {
        this.SetValue(CardView.CollapsedCardOrientationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a focused card's border. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the template that displays the border.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewFocusedCardBorderTemplate")]
    [Category("Appearance ")]
    public ControlTemplate FocusedCardBorderTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(CardView.FocusedCardBorderTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.FocusedCardBorderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a focused cell's border in a Card View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the template that displays the border.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewFocusedCellBorderCardViewTemplate")]
    [Category("Appearance ")]
    public ControlTemplate FocusedCellBorderCardViewTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(CardView.FocusedCellBorderCardViewTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.FocusedCellBorderCardViewTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the border template of the focused group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The <see cref="T:System.Windows.Controls.ControlTemplate" /> object that defines the presentation of the focused group row's border.
    /// </value>
    [Category("Appearance ")]
    public ControlTemplate VerticalFocusedGroupRowBorderTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(CardView.VerticalFocusedGroupRowBorderTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.VerticalFocusedGroupRowBorderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether multiple card selection is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.CardViewSelectMode" /> enumeration value that specifies the selection mode.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.SelectionMode property instead")]
    [XtraSerializableProperty]
    [Category("Options Selection")]
    [Browsable(false)]
    public CardViewSelectMode MultiSelectMode
    {
      get
      {
        return (CardViewSelectMode) this.GetValue(CardView.MultiSelectModeProperty);
      }
      set
      {
        this.SetValue(CardView.MultiSelectModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a card's style.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents a card's style.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardStyle")]
    public Style CardStyle
    {
      get
      {
        return (Style) this.GetValue(CardView.CardStyleProperty);
      }
      set
      {
        this.SetValue(CardView.CardStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that specifies how cards are arranged within a Card View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.CardLayout" /> enumeration value that specifies how cards are arranged within a Card View.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewCardLayout")]
    [XtraSerializableProperty]
    [Category("Options Card")]
    public CardLayout CardLayout
    {
      get
      {
        return (CardLayout) this.GetValue(CardView.CardLayoutProperty);
      }
      set
      {
        this.SetValue(CardView.CardLayoutProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display the card expand buttons. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the card expand button; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("CardViewShowCardExpandButton")]
    [Category("Options Card")]
    public bool ShowCardExpandButton
    {
      get
      {
        return (bool) this.GetValue(CardView.ShowCardExpandButtonProperty);
      }
      set
      {
        this.SetValue(CardView.ShowCardExpandButtonProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewOrientation")]
    public Orientation Orientation
    {
      get
      {
        return (Orientation) this.GetValue(CardView.OrientationProperty);
      }
      private set
      {
        this.SetValue(CardView.OrientationPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of cards. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of cards.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewCardTemplate")]
    [Category("Appearance ")]
    public DataTemplate CardTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.CardTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.CardTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a card template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewCardTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    public DataTemplateSelector CardTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.CardTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(CardView.CardTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a card template based on custom logic. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewActualCardTemplateSelector")]
    public DataTemplateSelector ActualCardTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.ActualCardTemplateSelectorProperty);
      }
      private set
      {
        this.SetValue(CardView.ActualCardTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of card headers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of card headers.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardHeaderTemplate")]
    public DataTemplate CardHeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.CardHeaderTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.CardHeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a card header template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that applies a template based on custom logic.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardHeaderTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector CardHeaderTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.CardHeaderTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(CardView.CardHeaderTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a card header template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewActualCardHeaderTemplateSelector")]
    public DataTemplateSelector ActualCardHeaderTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.ActualCardHeaderTemplateSelectorProperty);
      }
      private set
      {
        this.SetValue(CardView.ActualCardHeaderTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the data binding for the card header.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Data.BindingBase" /> object instance that specifies the data binding for the card header.
    /// </value>
    [Category("Options Card")]
    [Obsolete("Use the CardHeaderBinding property instead")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DefaultValue(null)]
    public BindingBase CardHeaderDisplayMemberBinding
    {
      get
      {
        return this.CardHeaderBinding;
      }
      set
      {
        this.CardHeaderBinding = value;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the data binding for the card header.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Data.BindingBase" /> object instance that specifies the data binding for the card header.
    /// </value>
    [DefaultValue(null)]
    [DevExpressXpfGridLocalizedDescription("CardViewCardHeaderDisplayMemberBinding")]
    [Category("Options Card")]
    public BindingBase CardHeaderBinding { get; set; }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of card rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of card rows.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardRowTemplate")]
    public DataTemplate CardRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.CardRowTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.CardRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a card row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardRowTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector CardRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.CardRowTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(CardView.CardRowTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a card row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewActualCardRowTemplateSelector")]
    public DataTemplateSelector ActualCardRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(CardView.ActualCardRowTemplateSelectorProperty);
      }
      private set
      {
        this.SetValue(CardView.ActualCardRowTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of card separators. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of card separators.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewSeparatorTemplate")]
    [Category("Options Card")]
    public DataTemplate SeparatorTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.SeparatorTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.SeparatorTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the thickness of card separators. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the thickness of card separators.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewSeparatorThickness")]
    [Category("Options Card")]
    [XtraSerializableProperty]
    public double SeparatorThickness
    {
      get
      {
        return (double) this.GetValue(CardView.SeparatorThicknessProperty);
      }
      set
      {
        this.SetValue(CardView.SeparatorThicknessProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether per-pixel scrolling is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="P:DevExpress.Xpf.Grid.CardView.ScrollMode" /> enumeration value.
    /// </value>
    [XtraSerializableProperty]
    [GridUIProperty]
    [DevExpressXpfGridLocalizedDescription("CardViewScrollMode")]
    [Category("Options View")]
    public ScrollMode ScrollMode
    {
      get
      {
        return (ScrollMode) this.GetValue(CardView.ScrollModeProperty);
      }
      set
      {
        this.SetValue(CardView.ScrollModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a card's width (or height).
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies a card's width or height.
    /// </value>
    [Category("Options Card")]
    [GridUIProperty]
    [DevExpressXpfGridLocalizedDescription("CardViewFixedSize")]
    [XtraSerializableProperty]
    public double FixedSize
    {
      get
      {
        return (double) this.GetValue(CardView.FixedSizeProperty);
      }
      set
      {
        this.SetValue(CardView.FixedSizeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the minimum width (or height if cards are arranged in columns) of cards. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the minimum size of cards.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewMinFixedSize")]
    [XtraSerializableProperty]
    [Category("Options Card")]
    public double MinFixedSize
    {
      get
      {
        return (double) this.GetValue(CardView.MinFixedSizeProperty);
      }
      set
      {
        this.SetValue(CardView.MinFixedSizeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to change the width (or height) of cards. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to resize cards; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewAllowCardResizing")]
    [Category("Options Card")]
    [XtraSerializableProperty]
    public bool AllowCardResizing
    {
      get
      {
        return (bool) this.GetValue(CardView.AllowCardResizingProperty);
      }
      set
      {
        this.SetValue(CardView.AllowCardResizingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether an end-user can resize cards. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if an end-user can resize cards; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewIsResizingEnabled")]
    public bool IsResizingEnabled
    {
      get
      {
        return (bool) this.GetValue(CardView.IsResizingEnabledProperty);
      }
      private set
      {
        this.SetValue(CardView.IsResizingEnabledPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the maximum number of cards in a row (or column).
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the maximum number of cards in a row (or column).</value>
    [Category("Options Card")]
    [DevExpressXpfGridLocalizedDescription("CardViewMaxCardCountInRow")]
    [XtraSerializableProperty]
    public int MaxCardCountInRow
    {
      get
      {
        return (int) this.GetValue(CardView.MaxCardCountInRowProperty);
      }
      set
      {
        this.SetValue(CardView.MaxCardCountInRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the alignment of cards within a view.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.Alignment" /> enumeration value that specifies the alignment of cards within a view.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewCardAlignment")]
    [Category("Options Card")]
    [XtraSerializableProperty]
    public Alignment CardAlignment
    {
      get
      {
        return (Alignment) this.GetValue(CardView.CardAlignmentProperty);
      }
      set
      {
        this.SetValue(CardView.CardAlignmentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the outer margin of a card.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> object that represents the thickness of a frame around a card.
    /// </value>
    [Category("Options Card")]
    [DevExpressXpfGridLocalizedDescription("CardViewCardMargin")]
    [XtraSerializableProperty]
    public Thickness CardMargin
    {
      get
      {
        return (Thickness) this.GetValue(CardView.CardMarginProperty);
      }
      set
      {
        this.SetValue(CardView.CardMarginProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the grid's optimized mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.UseCardLightweightTemplates" /> enumeration value.
    /// </value>
    [Category("Options Card")]
    [DevExpressXpfGridLocalizedDescription("CardViewUseLightweightTemplates")]
    [XtraSerializableProperty]
    public UseCardLightweightTemplates? UseLightweightTemplates
    {
      get
      {
        return (UseCardLightweightTemplates?) this.GetValue(CardView.UseLightweightTemplatesProperty);
      }
      set
      {
        this.SetValue(CardView.UseLightweightTemplatesProperty, (object) value);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double LeftGroupAreaIndent
    {
      get
      {
        return (double) this.GetValue(CardView.LeftGroupAreaIndentProperty);
      }
      set
      {
        this.SetValue(CardView.LeftGroupAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of card rows when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of card rows when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardRowTemplate")]
    public DataTemplate PrintCardRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.PrintCardRowTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of cards when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of cards when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardTemplate")]
    public DataTemplate PrintCardTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.PrintCardTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of individual values in the card values area when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of individual values in the card values area when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardContentTemplate")]
    [Category("Appearance Print")]
    public DataTemplate PrintCardContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.PrintCardContentTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardContentTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of card headers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of card headers when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardHeaderTemplate")]
    public DataTemplate PrintCardHeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.PrintCardHeaderTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardHeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the indent around a row of grouped cards when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of the indent around a row of grouped cards when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardRowIndentTemplate")]
    [Category("Appearance Print")]
    public DataTemplate PrintCardRowIndentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(CardView.PrintCardRowIndentTemplateProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardRowIndentTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the thickness of a frame around a card. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> object that represents the thickness of a frame around a card.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintCardMargin")]
    public Thickness PrintCardMargin
    {
      get
      {
        return (Thickness) this.GetValue(CardView.PrintCardMarginProperty);
      }
      set
      {
        this.SetValue(CardView.PrintCardMarginProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether cards in the print/export output are automatically resized horizontally to fit the report page's width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if cards are automatically resized horizontally to fit the report page's width; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("CardViewPrintAutoCardWidth")]
    [Category("Appearance Print")]
    public bool PrintAutoCardWidth
    {
      get
      {
        return (bool) this.GetValue(CardView.PrintAutoCardWidthProperty);
      }
      set
      {
        this.SetValue(CardView.PrintAutoCardWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the maximum number of card columns for printing/exporting. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The maximum number of printed/exported card columns. The default is <b>-1</b>.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintMaximumCardColumns")]
    public int PrintMaximumCardColumns
    {
      get
      {
        return (int) this.GetValue(CardView.PrintMaximumCardColumnsProperty);
      }
      set
      {
        this.SetValue(CardView.PrintMaximumCardColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style of the separator between summary items, when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style of the separator between summary items, when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("CardViewPrintTotalSummarySeparatorStyle")]
    public Style PrintTotalSummarySeparatorStyle
    {
      get
      {
        return (Style) this.GetValue(CardView.PrintTotalSummarySeparatorStyleProperty);
      }
      set
      {
        this.SetValue(CardView.PrintTotalSummarySeparatorStyleProperty, (object) value);
      }
    }

    protected internal override Orientation OrientationCore
    {
      get
      {
        return this.Orientation;
      }
    }

    protected internal override ScrollingMode ScrollingModeCore
    {
      get
      {
        if (this.ScrollMode == ScrollMode.Pixel)
          return ScrollingMode.Smart;
        return this.ScrollingMode;
      }
    }

    internal override bool IsDesignTimeAdornerPanelLeftAligned
    {
      get
      {
        return true;
      }
    }

    protected internal CardData FocusedCardData
    {
      get
      {
        return (CardData) this.FocusedRowData;
      }
    }

    private Point LastNavPoint
    {
      get
      {
        return this.lastNavPoint;
      }
      set
      {
        this.lastNavPoint = value;
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    public CardViewCommands CardViewCommands
    {
      get
      {
        return (CardViewCommands) this.Commands;
      }
    }

    private ScrollInfoBase ScrollInfo
    {
      get
      {
        return (ScrollInfoBase) this.DataPresenter.ScrollInfoCore.DefineSizeScrollInfo;
      }
    }

    private bool SearchInHorizontalRow
    {
      get
      {
        return this.Orientation == Orientation.Horizontal;
      }
    }

    private bool SearchInVerticalRow
    {
      get
      {
        return this.Orientation == Orientation.Vertical;
      }
    }

    private DependencyObject CurrNavigationObject
    {
      get
      {
        if (this.CurrentCell == null)
          return (DependencyObject) this.FocusedRowElement;
        return this.CurrentCell;
      }
    }

    internal double CardsPanelMaxSize
    {
      get
      {
        return ((CardsHierarchyPanel) LayoutHelper.FindElement(this.DataPresenter.ContentElement, (Predicate<FrameworkElement>) (e => e is CardsHierarchyPanel))).MaxSecondarySize;
      }
    }

    internal double CardsPanelViewPort
    {
      get
      {
        return this.SizeHelper.GetSecondarySize(new Size(this.DataPresenter.ActualWidth, this.DataPresenter.ActualHeight));
      }
    }

    private CardsHierarchyPanel CardsHierarchyPanel
    {
      get
      {
        if (this.DataPresenter == null)
          return (CardsHierarchyPanel) null;
        return this.DataPresenter.Panel as CardsHierarchyPanel;
      }
    }

    protected internal override bool ShouldUpdateCellData
    {
      get
      {
        return true;
      }
    }

    protected internal override bool ForceShowTotalSummaryColumnName
    {
      get
      {
        return true;
      }
    }

    internal List<CardRowInfo> CardRowInfoCollection
    {
      get
      {
        return this.CardsHierarchyPanel.RowsInfo;
      }
    }

    internal double GetRowOffset
    {
      get
      {
        return this.CardsHierarchyPanel.GetRowOffset();
      }
    }

    private CardViewBehavior CardViewBehavior
    {
      get
      {
        return this.ViewBehavior as CardViewBehavior;
      }
    }

    protected internal override bool IsGroupRowOptimized
    {
      get
      {
        if (this.CardViewBehavior != null)
          return this.CardViewBehavior.UseLightweightTemplatesHasFlag(UseCardLightweightTemplates.GroupRow);
        return false;
      }
    }

    static CardView()
    {
      Type type = typeof (CardView);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) type));
      CardView.ScrollModeProperty = DependencyProperty.Register("ScrollMode", typeof (ScrollMode), type, new PropertyMetadata((object) ScrollMode.Pixel, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).OnAllowPerPixelScrollingChanged())));
      CardView.FocusedCardBorderTemplateProperty = DependencyProperty.Register("FocusedCardBorderTemplate", typeof (ControlTemplate), type);
      CardView.FocusedCellBorderCardViewTemplateProperty = DependencyProperty.Register("FocusedCellBorderCardViewTemplate", typeof (ControlTemplate), type);
      CardView.VerticalFocusedGroupRowBorderTemplateProperty = DependencyProperty.Register("VerticalFocusedGroupRowBorderTemplate", typeof (ControlTemplate), type);
      CardView.FixedSizeProperty = CardsPanel.FixedSizeProperty.AddOwner(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) double.NaN, FrameworkPropertyMetadataOptions.None, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateIsResizingEnabled()), (CoerceValueCallback) ((d, baseValue) => (object) Math.Max((double) baseValue, ((CardView) d).MinFixedSize))));
      CardView.MaxCardCountInRowProperty = CardsPanel.MaxCardCountInRowProperty.AddOwner(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) int.MaxValue, FrameworkPropertyMetadataOptions.None, (PropertyChangedCallback) ((d, e) => d.CoerceValue(CardView.CollapsedCardOrientationProperty)), (CoerceValueCallback) ((d, baseValue) => (int) baseValue <= 0 ? (object) ((CardView) d).MaxCardCountInRow : (object) (int) baseValue)));
      CardView.CardAlignmentProperty = CardsPanel.CardAlignmentProperty.AddOwner(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) Alignment.Near, FrameworkPropertyMetadataOptions.None, (PropertyChangedCallback) null, (CoerceValueCallback) null));
      CardView.CardLayoutProperty = DependencyProperty.Register("CardLayout", typeof (CardLayout), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) CardView.OrientationToCardLayout(Orientation.Horizontal), (PropertyChangedCallback) ((d, e) => ((CardView) d).OnCardLayoutChanged())));
      CardView.OrientationPropertyKey = DependencyProperty.RegisterReadOnly("Orientation", typeof (Orientation), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) Orientation.Horizontal));
      CardView.OrientationProperty = CardView.OrientationPropertyKey.DependencyProperty;
      CardView.CardMarginProperty = CardsPanel.CardMarginProperty.AddOwner(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(0.0), FrameworkPropertyMetadataOptions.None, (PropertyChangedCallback) null, (CoerceValueCallback) null));
      CardView.ShowCardExpandButtonProperty = DependencyProperty.Register("ShowCardExpandButton", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      CardView.MinFixedSizeProperty = DependencyProperty.Register("MinFixedSize", typeof (double), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 10.0, (PropertyChangedCallback) null, new CoerceValueCallback(CardView.CoerceMinFixedSize)));
      CardView.AllowCardResizingProperty = DependencyProperty.Register("AllowCardResizing", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateIsResizingEnabled())));
      CardView.IsResizingEnabledPropertyKey = DependencyProperty.RegisterAttachedReadOnly("IsResizingEnabled", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, FrameworkPropertyMetadataOptions.Inherits));
      CardView.IsResizingEnabledProperty = CardView.IsResizingEnabledPropertyKey.DependencyProperty;
      CardView.CardTemplateProperty = DependencyProperty.Register("CardTemplate", typeof (DataTemplate), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardTemplateSelector())));
      CardView.CardTemplateSelectorProperty = DependencyProperty.Register("CardTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardTemplateSelector())));
      CardView.ActualCardTemplateSelectorPropertyKey = DependencyProperty.RegisterReadOnly("ActualCardTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      CardView.ActualCardTemplateSelectorProperty = CardView.ActualCardTemplateSelectorPropertyKey.DependencyProperty;
      CardView.CardHeaderTemplateProperty = DependencyProperty.Register("CardHeaderTemplate", typeof (DataTemplate), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardHeaderTemplateSelector())));
      CardView.CardHeaderTemplateSelectorProperty = DependencyProperty.Register("CardHeaderTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardHeaderTemplateSelector())));
      CardView.ActualCardHeaderTemplateSelectorPropertyKey = DependencyProperty.RegisterReadOnly("ActualCardHeaderTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      CardView.ActualCardHeaderTemplateSelectorProperty = CardView.ActualCardHeaderTemplateSelectorPropertyKey.DependencyProperty;
      CardView.CardRowTemplateProperty = DependencyProperty.Register("CardRowTemplate", typeof (DataTemplate), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardRowTemplateSelector())));
      CardView.CardRowTemplateSelectorProperty = DependencyProperty.Register("CardRowTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardView) d).UpdateActualCardRowTemplateSelector())));
      CardView.ActualCardRowTemplateSelectorPropertyKey = DependencyProperty.RegisterReadOnly("ActualCardRowTemplateSelector", typeof (DataTemplateSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      CardView.ActualCardRowTemplateSelectorProperty = CardView.ActualCardRowTemplateSelectorPropertyKey.DependencyProperty;
      CardView.SeparatorTemplateProperty = DependencyProperty.Register("SeparatorTemplate", typeof (DataTemplate), type);
      CardView.SeparatorThicknessProperty = DependencyProperty.Register("SeparatorThickness", typeof (double), type, new PropertyMetadata((object) 7.0));
      CardView.CardStyleProperty = DependencyProperty.Register("CardStyle", typeof (Style), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      CardView.MultiSelectModeProperty = DependencyProperty.Register("MultiSelectMode", typeof (CardViewSelectMode), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) CardViewSelectMode.None, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).OnMultiSelectModeChanged())));
      CardView.CollapsedCardOrientationPropertyKey = DependencyProperty.RegisterReadOnly("CollapsedCardOrientation", typeof (CollapsedCardOrientation), type, new PropertyMetadata((object) CollapsedCardOrientation.Horizontal, (PropertyChangedCallback) null, new CoerceValueCallback(CardView.CoerceCollapsedCardOrientation)));
      CardView.CollapsedCardOrientationProperty = CardView.CollapsedCardOrientationPropertyKey.DependencyProperty;
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.ChangeCardExpanded, (ExecutedRoutedEventHandler) ((d, e) => ((CardView) d).OnChangeCardExpanded(e))));
      CardView.UseLightweightTemplatesProperty = CardViewBehavior.RegisterUseLightweightTemplatesProperty(type);
      CardView.LeftGroupAreaIndentProperty = DependencyProperty.Register("LeftGroupAreaIndent", typeof (double), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).RebuildVisibleColumns())));
      CardView.PrintCardRowTemplateProperty = DependencyProperty.Register("PrintCardRowTemplate", typeof (DataTemplate), type, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      CardView.PrintCardTemplateProperty = DependencyProperty.Register("PrintCardTemplate", typeof (DataTemplate), type, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      CardView.PrintCardContentTemplateProperty = DependencyProperty.Register("PrintCardContentTemplate", typeof (DataTemplate), type, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      CardView.PrintCardHeaderTemplateProperty = DependencyProperty.Register("PrintCardHeaderTemplate", typeof (DataTemplate), type, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      CardView.PrintCardRowIndentTemplateProperty = DependencyProperty.Register("PrintCardRowIndentTemplate", typeof (DataTemplate), type, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      CardView.PrintCardMarginProperty = DependencyProperty.Register("PrintCardMargin", typeof (Thickness), type, (PropertyMetadata) new UIPropertyMetadata((object) new Thickness(20.0, 0.0, 0.0, 20.0)));
      CardView.PrintAutoCardWidthProperty = DependencyProperty.Register("PrintAutoCardWidth", typeof (bool), type, (PropertyMetadata) new UIPropertyMetadata((object) false));
      CardView.PrintMaximumCardColumnsProperty = DependencyProperty.Register("PrintMaximumCardColumns", typeof (int), type, (PropertyMetadata) new UIPropertyMetadata((object) -1));
      CardView.PrintTotalSummarySeparatorStyleProperty = DependencyProperty.Register("PrintTotalSummarySeparatorStyle", typeof (Style), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the CardView class with default settings.
    /// </para>
    ///             </summary>
    public CardView()
      : base((MasterNodeContainer) null, (MasterRowsContainer) null, (DataControlDetailDescriptor) null)
    {
      this.UpdateIsResizingEnabled();
    }

    private static object CoerceCollapsedCardOrientation(DependencyObject d, object value)
    {
      CardView cardView = (CardView) d;
      CollapsedCardOrientation collapsedCardOrientation = CollapsedCardOrientation.Horizontal;
      if (cardView.CardLayout == CardLayout.Rows && cardView.MaxCardCountInRow != 1 || cardView.CardLayout == CardLayout.Columns && cardView.MaxCardCountInRow == 1)
        collapsedCardOrientation = CollapsedCardOrientation.Vertical;
      if (!CardView.IsDefaultFixedSize(cardView.FixedSize))
      {
        if (cardView.CardLayout == CardLayout.Rows)
          collapsedCardOrientation = CollapsedCardOrientation.Vertical;
        if (cardView.CardLayout == CardLayout.Columns)
          collapsedCardOrientation = CollapsedCardOrientation.Horizontal;
      }
      return (object) collapsedCardOrientation;
    }

    private static bool IsDefaultFixedSize(double size)
    {
      if (double.NaN.IsNotNumber())
        return size.IsNotNumber();
      return size == double.NaN;
    }

    internal static Orientation CardLayoutToOrientation(CardLayout cardLayout)
    {
      return cardLayout != CardLayout.Columns ? Orientation.Vertical : Orientation.Horizontal;
    }

    internal static CardLayout OrientationToCardLayout(Orientation orientation)
    {
      return orientation != Orientation.Horizontal ? CardLayout.Rows : CardLayout.Columns;
    }

    private static object CoerceMinFixedSize(DependencyObject d, object baseValue)
    {
      CardView cardView = (CardView) d;
      double val2 = Math.Max((double) baseValue, cardView.MinFixedSize);
      cardView.FixedSize = Math.Max(cardView.FixedSize, val2);
      return (object) val2;
    }

    protected override DataViewBehavior CreateViewBehavior()
    {
      return (DataViewBehavior) new CardViewBehavior((DataViewBase) this);
    }

    protected override DataViewCommandsBase CreateCommandsContainer()
    {
      return (DataViewCommandsBase) new CardViewCommands(this);
    }

    private void UpdateActualCardRowTemplateSelector()
    {
      this.ActualCardRowTemplateSelector = (DataTemplateSelector) new ActualTemplateSelectorWrapper(this.CardRowTemplateSelector, this.CardRowTemplate);
    }

    private void UpdateActualCardTemplateSelector()
    {
      this.ActualCardTemplateSelector = (DataTemplateSelector) new ActualTemplateSelectorWrapper(this.CardTemplateSelector, this.CardTemplate);
    }

    private void UpdateActualCardHeaderTemplateSelector()
    {
      this.ActualCardHeaderTemplateSelector = (DataTemplateSelector) new ActualTemplateSelectorWrapper(this.CardHeaderTemplateSelector, this.CardHeaderTemplate);
    }

    private void UpdateIsResizingEnabled()
    {
      this.IsResizingEnabled = !double.IsNaN(this.FixedSize) && this.AllowCardResizing;
      this.CoerceValue(CardView.CollapsedCardOrientationProperty);
    }

    protected internal override FrameworkElement CreateGroupControl(GroupRowData rowData)
    {
      return ((CardViewBehavior) this.ViewBehavior).CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new GroupCardRowControl(rowData)), (Func<FrameworkElement>) (() => (FrameworkElement) new GroupCardRow()), UseCardLightweightTemplates.GroupRow);
    }

    private Point GetElemLocation(UIElement elem)
    {
      if (elem == null)
        return new Point(0.0, 0.0);
      Point point = elem.TranslatePoint(new Point(0.0, 0.0), this.ScrollInfoOwner as UIElement);
      if (!this.IsGroupRow((DependencyObject) elem) || ColumnBase.GetNavigationIndex((DependencyObject) elem) != int.MinValue)
        return point;
      if (this.Orientation == Orientation.Vertical)
        point.X = this.LastNavPoint.X;
      else
        point.Y = this.LastNavPoint.Y;
      return point;
    }

    protected virtual bool IsNearer(Point near, Point pt)
    {
      if (this.Orientation == Orientation.Vertical)
      {
        if (pt.Y < near.Y)
          return true;
        if (pt.Y == near.Y)
          return pt.X <= near.X;
        return false;
      }
      if (pt.X < near.X)
        return true;
      if (pt.X == near.X)
        return pt.Y < near.Y;
      return false;
    }

    protected virtual bool ShouldProcessObjectByLocation(Point currLoc, Point loc, CardView.NavObjectDirection dir)
    {
      switch (dir)
      {
        case CardView.NavObjectDirection.Left:
          return currLoc.X > loc.X;
        case CardView.NavObjectDirection.Top:
          return currLoc.Y > loc.Y;
        case CardView.NavObjectDirection.Bottom:
          return currLoc.Y < loc.Y;
        case CardView.NavObjectDirection.Right:
          return currLoc.X < loc.X;
        default:
          return false;
      }
    }

    protected virtual Point GetDistance(Point pt1, Point pt2)
    {
      return new Point((pt2.X - pt1.X) * (pt2.X - pt1.X), (pt2.Y - pt1.Y) * (pt2.Y - pt1.Y));
    }

    protected virtual DependencyObject FindNearNavObject(DependencyObject currObj, CardView.NavObjectDirection dir, bool processCells, bool searchInCurrentCardRow)
    {
      if (currObj == null)
        return (DependencyObject) null;
      Point near = new Point(double.MaxValue, double.MaxValue);
      Point point = new Point();
      int rowIndex;
      int elementIndex;
      if (!this.CardsHierarchyPanel.TryFindRowElement(this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle), out rowIndex, out elementIndex))
        rowIndex = dir == CardView.NavObjectDirection.Left || dir == CardView.NavObjectDirection.Top ? this.CardsHierarchyPanel.RowsInfo.Count - 1 : 0;
      DependencyObject dependencyObject = (DependencyObject) null;
      double definePoint = this.SizeHelper.GetDefinePoint(this.LastNavPoint);
      for (int index = 0; index < rowIndex; ++index)
        definePoint -= this.SizeHelper.GetDefineSize(this.CardsHierarchyPanel.RowsInfo[index].RenderSize);
      for (int index1 = 0; index1 < this.CardsHierarchyPanel.RowsInfo.Count; ++index1)
      {
        CardRowInfo cardRowInfo = this.CardsHierarchyPanel.RowsInfo[index1];
        for (int index2 = 0; index2 < cardRowInfo.Elements.Count; ++index2)
        {
          FrameworkElement element = cardRowInfo.Elements[index2].Element;
          Point elemLocation1 = this.GetElemLocation((UIElement) element);
          CardData cardData = element.DataContext as CardData;
          if (!processCells || element.DataContext is GroupRowData || cardData != null && !cardData.IsExpanded)
          {
            Point distance = this.GetDistance(this.LastNavPoint, this.SizeHelper.CreatePoint(definePoint, this.SizeHelper.GetSecondaryPoint(elemLocation1)));
            if (element.IsVisible && this.ShouldProcessObjectByLocation(this.LastNavPoint, elemLocation1, dir) && this.IsNearer(near, distance))
            {
              dependencyObject = (DependencyObject) element;
              near = distance;
            }
          }
          else if (!searchInCurrentCardRow || rowIndex == index1)
          {
            GridCellsEnumerator gridCellsEnumerator = new GridCellsEnumerator(element);
            while (gridCellsEnumerator.MoveNext())
            {
              if (gridCellsEnumerator.Current != currObj)
              {
                int currentNavigationIndex = gridCellsEnumerator.CurrentNavigationIndex;
                if (currentNavigationIndex < this.VisibleColumns.Count && this.VisibleColumns[currentNavigationIndex].AllowFocus)
                {
                  Point elemLocation2 = this.GetElemLocation(gridCellsEnumerator.Current as UIElement);
                  if (this.ShouldProcessObjectByLocation(this.LastNavPoint, elemLocation2, dir))
                  {
                    Point distance = this.GetDistance(this.LastNavPoint, elemLocation2);
                    if (this.IsNearer(near, distance))
                    {
                      dependencyObject = gridCellsEnumerator.Current;
                      near = distance;
                    }
                  }
                }
              }
            }
          }
        }
        definePoint += this.SizeHelper.GetDefineSize(cardRowInfo.RenderSize);
      }
      return dependencyObject;
    }

    protected virtual DependencyObject FindNearNextRow(DependencyObject row)
    {
      return this.FindNearNavObject(row, this.Orientation == Orientation.Vertical ? CardView.NavObjectDirection.Bottom : CardView.NavObjectDirection.Right, false, false);
    }

    protected virtual DependencyObject FindNearPrevRow(DependencyObject row)
    {
      return this.FindNearNavObject(row, this.Orientation == Orientation.Vertical ? CardView.NavObjectDirection.Top : CardView.NavObjectDirection.Left, false, false);
    }

    protected internal virtual void MoveNextRowCard()
    {
      DependencyObject nearNextRow = this.FindNearNextRow((DependencyObject) this.FocusedRowElement);
      if (nearNextRow != null)
        this.MoveFocusedRow(this.Grid.GetRowVisibleIndexByHandle(DataViewBase.GetRowHandle(nearNextRow).Value));
      else
        this.MoveNextCard();
    }

    private void MoveNextCard()
    {
      if ((double) this.ConvertVisibleIndexToScrollIndex(this.Grid.GetRowVisibleIndexByHandle(this.FocusedRowHandle)) >= this.ScrollInfo.Extent - 1.0)
        return;
      this.MoveFocusedRow(Math.Min((int) this.ScrollInfo.Extent - 1, this.ScrollInfoOwner.Offset + this.ScrollInfoOwner.ItemsOnPage));
    }

    protected internal virtual void MovePrevRowCard()
    {
      DependencyObject nearPrevRow = this.FindNearPrevRow((DependencyObject) this.FocusedRowElement);
      if (nearPrevRow != null)
        this.MoveFocusedRow(this.Grid.GetRowVisibleIndexByHandle(DataViewBase.GetRowHandle(nearPrevRow).Value));
      else
        this.MovePrevCard();
    }

    private bool MovePrevCard()
    {
      if (this.ConvertVisibleIndexToScrollIndex(this.Grid.GetRowVisibleIndexByHandle(this.FocusedRowHandle)) <= 0)
        return false;
      int visibleIndex = Math.Max(0, this.CardsHierarchyPanel.CalcGenerateItemsOffset(this.DataPresenter.ActualScrollOffset - 1.0));
      int handleByVisibleIndex = this.Grid.GetRowHandleByVisibleIndex(visibleIndex);
      if (!this.Grid.IsGroupRowHandle(handleByVisibleIndex))
      {
        int rowIndex;
        int elementIndex;
        if (this.CardsHierarchyPanel.TryFindRowElement(this.Grid.GetRowVisibleIndexByHandle(this.FocusedRowHandle), out rowIndex, out elementIndex) && rowIndex == 0 && this.ViewBehavior.AllowPerPixelScrolling)
          visibleIndex += elementIndex;
        handleByVisibleIndex = this.Grid.GetRowHandleByVisibleIndex(visibleIndex);
      }
      this.MoveFocusedRow(this.Grid.GetRowVisibleIndexByHandle(handleByVisibleIndex));
      return true;
    }

    protected override void MoveNextPageCore()
    {
      int num1 = (int) (this.ScrollInfo.Offset + Math.Max(1.0, this.ScrollInfo.Viewport) - 1.0);
      int num2 = (int) this.CardsHierarchyPanel.CalcExtent(this.SizeHelper.GetDefineSize(this.DataPresenter.LastConstraint), new int?(this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle)));
      if ((double) num2 < this.ScrollInfo.Offset || (double) num2 > this.ScrollInfo.Offset + this.ScrollInfo.Viewport)
        num2 = (int) this.ScrollInfo.Offset;
      if (num2 == num1 && this.ScrollInfo.Offset + this.ScrollInfo.Viewport < this.ScrollInfo.Extent)
      {
        double num3 = this.ScrollInfo.Viewport < 1.0 ? 1.0 : Math.Floor(this.ScrollInfo.Viewport) - this.ScrollInfo.Viewport;
        this.ScrollInfo.SetOffset((double) num1 + num3);
        this.EnqueueImmediateAction((IAction) new FocusFirstRowAfterPageDownCardView((DataViewBase) this));
      }
      else
        this.MoveFocusedRowToLastScrollRow();
    }

    protected internal override void MoveFocusedRowToLastScrollRow()
    {
      int num = (int) (this.ScrollInfo.Offset + Math.Max(1.0, this.ScrollInfo.Viewport) - 1.0);
      int visibleIndex = this.CardsHierarchyPanel.CalcGenerateItemsOffset((double) num);
      int rowIndex;
      int elementIndex;
      if (this.ViewBehavior.AllowPerPixelScrolling && this.CardsHierarchyPanel.TryFindRowElement(this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle), out rowIndex, out elementIndex))
      {
        visibleIndex += elementIndex;
        if (rowIndex + (int) this.ScrollInfo.Offset == num)
          visibleIndex = this.DataControl.VisibleRowCount - 1;
      }
      if (visibleIndex == this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle))
        return;
      this.MoveFocusedRow(visibleIndex);
      this.SelectionStrategy.OnNavigationComplete(false);
    }

    protected override void MovePrevPageCore()
    {
      int num1 = (int) Math.Min(Math.Ceiling(this.ScrollInfo.Offset), this.ScrollInfo.Extent - 1.0);
      int num2 = (int) this.CardsHierarchyPanel.CalcExtent(this.SizeHelper.GetDefineSize(this.DataPresenter.LastConstraint), new int?(this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle)));
      if (num2 <= num1 && this.ScrollInfo.Offset > 0.0 && num2 >= (int) this.ScrollInfo.Offset)
      {
        double num3 = this.ScrollInfo.Viewport < 1.0 ? 1.0 : Math.Floor(this.ScrollInfo.Viewport) - 1.0;
        this.ScrollInfo.SetOffset((double) num1 - num3);
        this.EnqueueImmediateAction((IAction) new ScrollAndFocusFirstAfterPageUpCardView((DataViewBase) this, this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle), 0));
      }
      else
        this.MoveFocusedRowToFirstScrollRow();
    }

    protected internal override void MoveFocusedRowToFirstScrollRow()
    {
      int num = (int) Math.Min(Math.Ceiling(this.ScrollInfo.Offset), this.ScrollInfo.Extent - 1.0);
      int visibleIndex = this.CardsHierarchyPanel.CalcGenerateItemsOffset((double) num);
      int rowIndex;
      int elementIndex;
      if (this.ViewBehavior.AllowPerPixelScrolling && this.CardsHierarchyPanel.TryFindRowElement(this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle), out rowIndex, out elementIndex))
      {
        visibleIndex += elementIndex;
        if (rowIndex + (int) this.ScrollInfo.Offset == num)
          visibleIndex = 0;
      }
      if (visibleIndex == this.DataControl.GetRowVisibleIndexByHandleCore(this.FocusedRowHandle))
        return;
      this.MoveFocusedRow(visibleIndex);
      this.SelectionStrategy.OnNavigationComplete(false);
    }

    protected internal virtual void MovePrevColumnCard()
    {
      this.MovePrevRow();
    }

    protected internal virtual void MoveNextColumnCard()
    {
      this.MoveNextRow();
    }

    protected internal virtual void MoveFirstVisibleRow()
    {
      this.MoveFocusedRow(this.ScrollInfoOwner.Offset);
    }

    protected internal virtual void MoveLastVisibleRow()
    {
      this.MoveFocusedRow(this.ScrollInfoOwner.Offset + this.ScrollInfoOwner.ItemsOnPage - 1);
    }

    protected virtual void UpdateNavigationIndex(DependencyObject dobj, CardView.NavObjectDirection loc)
    {
      if (dobj == null)
      {
        if (this.Orientation == Orientation.Vertical && loc == CardView.NavObjectDirection.Top || this.Orientation == Orientation.Horizontal && loc == CardView.NavObjectDirection.Left)
        {
          if (!this.MovePrevCard())
            return;
          this.NavigationIndex = this.VisibleColumns.Count - 1;
        }
        else
        {
          if ((this.Orientation != Orientation.Vertical || loc != CardView.NavObjectDirection.Bottom) && (this.Orientation != Orientation.Horizontal || loc != CardView.NavObjectDirection.Right))
            return;
          this.MoveNextCard();
        }
      }
      else
      {
        RowHandle rowHandle = DataViewBase.GetRowHandle(DataViewBase.FindParentRow(dobj));
        if (rowHandle != null && rowHandle.Value != DataViewBase.GetRowHandle((DependencyObject) this.FocusedRowElement).Value)
          this.MoveFocusedRow(this.Grid.GetRowVisibleIndexByHandle(rowHandle.Value));
        if (ColumnBase.GetNavigationIndex(dobj) == int.MinValue)
          return;
        this.NavigationIndex = ColumnBase.GetNavigationIndex(dobj);
      }
    }

    protected internal override int ConvertVisibleIndexToScrollIndex(int visibleIndex)
    {
      if (this.CardsHierarchyPanel == null)
        return visibleIndex;
      return (int) this.CardsHierarchyPanel.CalcExtent(this.SizeHelper.GetDefineSize(this.DataPresenter.LastConstraint), new int?(visibleIndex));
    }

    protected internal override void OnDataReset()
    {
      base.OnDataReset();
      if (this.CardsHierarchyPanel == null)
        return;
      this.CardsHierarchyPanel.ClearRows();
    }

    protected virtual DependencyObject GetNearUpCell()
    {
      return this.FindNearNavObject(this.CurrNavigationObject, CardView.NavObjectDirection.Top, true, this.SearchInHorizontalRow);
    }

    protected virtual DependencyObject GetNearDownCell()
    {
      return this.FindNearNavObject(this.CurrNavigationObject, CardView.NavObjectDirection.Bottom, true, this.SearchInHorizontalRow);
    }

    protected virtual DependencyObject GetNearLeftCell()
    {
      return this.FindNearNavObject(this.CurrNavigationObject, CardView.NavObjectDirection.Left, true, this.SearchInVerticalRow);
    }

    protected virtual DependencyObject GetNearRightCell()
    {
      return this.FindNearNavObject(this.CurrNavigationObject, CardView.NavObjectDirection.Right, true, this.SearchInVerticalRow);
    }

    protected internal virtual void MoveUpCell()
    {
      this.UpdateNavigationIndex(this.GetNearUpCell(), CardView.NavObjectDirection.Top);
    }

    protected internal virtual void MoveDownCell()
    {
      this.UpdateNavigationIndex(this.GetNearDownCell(), CardView.NavObjectDirection.Bottom);
    }

    protected internal virtual void MoveLeftCell()
    {
      this.UpdateNavigationIndex(this.GetNearLeftCell(), CardView.NavObjectDirection.Left);
    }

    protected internal virtual void MoveRightCell()
    {
      this.UpdateNavigationIndex(this.GetNearRightCell(), CardView.NavObjectDirection.Right);
    }

    protected internal override bool UpdateRowsState()
    {
      if (this.RowsStateDirty)
        this.CurrentCell = (DependencyObject) null;
      bool flag = base.UpdateRowsState();
      if (this.NavigationStyle == GridViewNavigationStyle.Row || this.DataControl.IsGroupRowHandleCore(this.FocusedRowHandle))
      {
        RowData rowData = this.GetRowData(this.FocusedRowHandle);
        if (rowData != null)
          this.UpdateLastNavPoint((DependencyObject) rowData.WholeRowElement);
      }
      else
        this.UpdateLastNavPoint(this.CurrentCell);
      return flag;
    }

    protected internal override void ForceLayout()
    {
      base.ForceLayout();
      if (this.CardsHierarchyPanel == null)
        return;
      this.CardsHierarchyPanel.InvalidateMeasure();
    }

    protected virtual void UpdateLastNavPoint(DependencyObject obj)
    {
      if (obj == null)
        return;
      if (!this.IsGroupRow(obj) || ColumnBase.GetNavigationIndex(obj) != int.MinValue)
        this.LastNavPoint = this.GetElemLocation(obj as UIElement);
      else if (this.Orientation == Orientation.Vertical)
        this.LastNavPoint = new Point(this.LastNavPoint.X, this.GetElemLocation(obj as UIElement).Y);
      else
        this.LastNavPoint = new Point(this.GetElemLocation(obj as UIElement).X, this.LastNavPoint.Y);
    }

    protected override void OnFocusedRowHandleChangedCore(int oldRowHandle)
    {
      base.OnFocusedRowHandleChangedCore(oldRowHandle);
      this.UpdateLastNavPoint((DependencyObject) this.FocusedRowElement);
    }

    internal override void OnCurrentCellChanged()
    {
      base.OnCurrentCellChanged();
      if (this.CurrentCell == null)
        return;
      this.UpdateLastNavPoint(this.CurrentCell);
    }

    private void OnCardLayoutChanged()
    {
      this.Orientation = CardView.CardLayoutToOrientation(this.CardLayout);
      this.ScrollInfoOwner.Do<IScrollInfoOwner>((Action<IScrollInfoOwner>) (x => x.OnDefineScrollInfoChanged()));
      this.CoerceValue(CardView.CollapsedCardOrientationProperty);
      this.UpdateRowData(new UpdateRowDataDelegate(this.UpdateRowDataChangeLayout), true, true);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified card is expanded.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the card's handle.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified card is expanded; otherwise, <b>false</b>.
    /// </returns>
    public bool IsCardExpanded(int rowHandle)
    {
      DependencyObject rowState = this.Grid.GetRowState(rowHandle, false);
      if (rowState != null)
        return CardData.GetIsExpanded(rowState);
      return true;
    }

    /// <summary>
    ///                 <para>Expands all cards.
    /// </para>
    ///             </summary>
    public void ExpandAllCards()
    {
      this.SetAllCardsExpanded(true);
    }

    /// <summary>
    ///                 <para>Collapses all cards.
    /// </para>
    ///             </summary>
    public void CollapseAllCards()
    {
      this.SetAllCardsExpanded(false);
    }

    protected internal virtual void SetAllCardsExpanded(bool expanded)
    {
      for (int visibleIndex = 0; visibleIndex < this.Grid.VisibleRowCount; ++visibleIndex)
        this.SetCardExpandedCore(this.Grid.GetRowHandleByVisibleIndex(visibleIndex), expanded);
      this.UpdateRowData(new UpdateRowDataDelegate(this.UpdateRowDataIsExpanded), true, true);
    }

    /// <summary>
    ///                 <para>Expands the specified card.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies the card by its handle.
    /// 
    ///           </param>
    public void ExpandCard(int rowHandle)
    {
      this.SetCardExpanded(rowHandle, true);
    }

    /// <summary>
    ///                 <para>Collapses the specified card.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies the card by its handle.
    /// 
    ///           </param>
    public void CollapseCard(int rowHandle)
    {
      this.SetCardExpanded(rowHandle, false);
    }

    protected internal virtual void SetCardExpanded(int rowHandle, bool expanded)
    {
      this.SetCardExpandedCore(rowHandle, expanded);
      this.UpdateRowDataByRowHandle(rowHandle, new UpdateRowDataDelegate(this.UpdateRowDataIsExpanded));
    }

    internal void ChangeCardExpanded(int rowHandle)
    {
      this.SetCardExpanded(rowHandle, !this.IsCardExpanded(rowHandle));
    }

    private void SetCardExpandedCore(int rowHandle, bool expanded)
    {
      DependencyObject rowState = this.Grid.GetRowState(rowHandle, !expanded);
      if (rowState == null)
        return;
      CardData.SetIsExpanded(rowState, expanded);
    }

    private void UpdateRowDataIsExpanded(RowData rowData)
    {
      CardData cardData = rowData as CardData;
      if (cardData == null)
        return;
      cardData.UpdateIsExpanded();
    }

    private void UpdateRowDataChangeLayout(RowData rowData)
    {
      GroupRowData groupRowData = rowData as GroupRowData;
      if (groupRowData == null)
        return;
      groupRowData.UpdateCardLayout();
    }

    private void OnChangeCardExpanded(ExecutedRoutedEventArgs e)
    {
      if (!(e.Parameter is int))
        return;
      this.ChangeCardExpanded((int) e.Parameter);
    }

    internal void GetDataRowTextCore(StringBuilder sb, int rowHandle)
    {
      if (rowHandle == int.MinValue)
        return;
      if (!this.Grid.IsGroupRowHandle(rowHandle))
      {
        string cardHeaderText = this.GetCardHeaderText(rowHandle);
        if (cardHeaderText != null)
        {
          sb.Append(cardHeaderText);
          sb.Append("\r\n");
        }
      }
      for (int visibleColumnIndex = 0; visibleColumnIndex < this.VisibleColumns.Count; ++visibleColumnIndex)
      {
        string str1 = string.Empty;
        string str2 = string.Empty;
        if (this.ActualClipboardCopyWithHeaders && !this.Grid.IsGroupRowHandle(rowHandle))
        {
          sb.Append(this.GetTextForClipboard(int.MinValue, visibleColumnIndex));
          sb.Append("\t");
        }
        sb.Append(this.GetTextForClipboard(rowHandle, visibleColumnIndex));
        sb.Append("\r\n");
        if (this.Grid.IsGroupRowHandle(rowHandle))
          break;
      }
    }

    protected virtual string GetCardHeaderText(int rowHandle)
    {
      if (this.cardHeaderData == null)
        this.cardHeaderData = new CardHeaderData();
      this.cardHeaderData.Data = this.DataProviderBase.GetWpfRow(new RowHandle(rowHandle), -1);
      this.cardHeaderData.Binding = this.CardHeaderBinding;
      this.cardHeaderData.RowData = this.GetRowData(rowHandle);
      return this.cardHeaderData.Value as string;
    }

    protected override SelectionStrategyBase CreateSelectionStrategy()
    {
      if (this.NavigationStyle == GridViewNavigationStyle.None)
        return (SelectionStrategyBase) new SelectionStrategyNavigationNone((DataViewBase) this);
      if (!this.IsMultiRowSelection)
        return (SelectionStrategyBase) new SelectionStrategyNone((DataViewBase) this);
      if (this.DataControl != null && this.ShowSelectionRectangle)
        return (SelectionStrategyBase) new SelectionStrategyRowRange((GridViewBase) this);
      return (SelectionStrategyBase) new SelectionStrategyRow((GridViewBase) this);
    }

    private void SetSizeExpansion(int value)
    {
      if (this.FocusRectPresenter == null)
        return;
      this.FocusRectPresenter.SizeExpansion = value;
    }

    internal override void SetFocusedRectangleOnRow()
    {
      this.SetSizeExpansion(2);
      base.SetFocusedRectangleOnRow();
    }

    internal override void SetFocusedRectangleOnCell()
    {
      FrameworkElement frameworkElement = this.FocusedView.CurrentCellEditor != null ? this.FocusedView.CurrentCell as FrameworkElement : (FrameworkElement) null;
      if (frameworkElement != null && UIElementHelper.IsVisibleInTree((UIElement) frameworkElement, true))
      {
        this.SetSizeExpansion(0);
        base.SetFocusedRectangleOnCell();
      }
      else
        this.SetFocusedRectangleOnRow();
    }

    internal override void SetFocusedRectangleOnGroupRow()
    {
      this.SetSizeExpansion(0);
      base.SetFocusedRectangleOnGroupRow();
    }

    protected override ControlTemplate GetCellFocusedRectangleTemplate()
    {
      return this.FocusedCellBorderCardViewTemplate;
    }

    protected override ControlTemplate GetGroupRowFocusedRectangleTemplate()
    {
      if (this.CardLayout == CardLayout.Columns)
        return this.VerticalFocusedGroupRowBorderTemplate;
      return this.FocusedGroupRowBorderTemplate;
    }

    protected override ControlTemplate GetRowFocusedRectangleTemplate()
    {
      return this.FocusedCardBorderTemplate;
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the card view.
    /// </para>
    ///             </summary>
    /// <param name="d">
    /// A <see cref="T:System.Windows.DependencyObject" /> object that represents the element contained within the card view.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.CardViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public CardViewHitInfo CalcHitInfo(DependencyObject d)
    {
      return new CardViewHitInfo(DataViewBase.GetStartHitTestObject(d, (DataViewBase) this), this);
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the card view.
    /// </para>
    ///             </summary>
    /// <param name="hitTestPoint">
    /// A <see cref="T:System.Drawing.Point" /> structure which specifies the test point coordinates relative to the map's top-left corner.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.CardViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public CardViewHitInfo CalcHitInfo(Point hitTestPoint)
    {
      return this.CalcHitInfo(VisualTreeHelper.HitTest((Visual) this, hitTestPoint).VisualHit);
    }

    internal override IDataViewHitInfo CalcHitInfoCore(DependencyObject source)
    {
      return (IDataViewHitInfo) this.CalcHitInfo(source);
    }

    internal override FrameworkElement GetRowVisibleElement(RowDataBase rowData)
    {
      return ((RowData) rowData).RowElement;
    }

    protected override RowData CreateFocusedRowData()
    {
      return this.ViewBehavior.CreateRowDataCore((DataTreeBuilder) this.VisualDataTreeBuilder, true);
    }

    protected internal override bool ShouldChangeForwardIndex(int rowHandle)
    {
      return this.GetRowElementByVisibleIndex(this.DataControl.GetRowVisibleIndexByHandleCore(rowHandle)) != null;
    }

    private FrameworkElement GetRowElementByVisibleIndex(int visibleIndex)
    {
      return this.GetRowElementByRowHandle(this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex));
    }

    protected internal override double CalcOffsetForward(int rowHandle, bool perPixelScrolling)
    {
      RowData rowData = this.GetRowData(rowHandle);
      if (rowData == null || this.CardsHierarchyPanel == null)
        return 0.0;
      double num1 = this.GetItemInvisibleSize(rowData.WholeRowElement) - this.CardsHierarchyPanel.GetRowOffset();
      double num2 = 0.0;
      for (int index = 0; index < this.CardsHierarchyPanel.RowsInfo.Count; ++index)
      {
        double defineSize = this.SizeHelper.GetDefineSize(this.CardsHierarchyPanel.RowsInfo[index].RenderSize);
        if (defineSize >= num1)
          return num2 + num1 / defineSize;
        num1 -= defineSize;
        ++num2;
      }
      return 0.0;
    }

    private double GetElementDefineOffset(FrameworkElement elem)
    {
      return this.SizeHelper.GetDefinePoint(LayoutHelper.GetRelativeElementRect((UIElement) elem, (UIElement) this.DataPresenter).Location());
    }

    protected internal override DevExpress.Xpf.Grid.MultiSelectMode GetSelectionMode()
    {
      return SelectionModeHelper.ConvertToMultiSelectMode((CardViewSelectMode) this.GetValue(CardView.MultiSelectModeProperty));
    }

    protected internal override DataTemplate GetPrintRowTemplate()
    {
      return this.PrintCardRowTemplate;
    }

    protected override IRootDataNode CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      return CardViewPrintingHelper.CreatePrintingTreeNode(this, usablePageSize, (ItemsGenerationStrategyBase) null);
    }

    protected override void CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      CardViewPrintingHelper.CreatePrintingTreeNodeAsync(this, usablePageSize);
    }

    protected override void PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters)
    {
    }

    protected internal override PrintingDataTreeBuilderBase CreatePrintingDataTreeBuilder(double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy, MasterDetailPrintInfo masterDetailPrintInfo, BandsLayoutBase bandsLayout)
    {
      return (PrintingDataTreeBuilderBase) new CardViewPrintingDataTreeBuilder(this, totalHeaderWidth, itemsGenerationStrategy);
    }

    protected internal virtual CardViewPrintingDataTreeBuilder CreatePrintingDataTreeBuilder(double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy)
    {
      return (CardViewPrintingDataTreeBuilder) this.CreatePrintingDataTreeBuilder(totalHeaderWidth, itemsGenerationStrategy, (MasterDetailPrintInfo) null, (BandsLayoutBase) null);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in CSV format.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created CSV file should be sent.
    /// 
    ///           </param>
    public override void ExportToCsv(Stream stream)
    {
      PrintHelper.ExportToCsv((IPrintableControl) this, stream);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in CSV format using the specified CSV-specific options.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created CSV file should be sent.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.CsvExportOptions" /> object which specifies the CSV export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToCsv(Stream stream, CsvExportOptions options)
    {
      PrintHelper.ExportToCsv((IPrintableControl) this, stream, options);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in CSV format.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created CSV file.
    /// 
    ///           </param>
    public override void ExportToCsv(string filePath)
    {
      PrintHelper.ExportToCsv((IPrintableControl) this, filePath);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in CSV format, using the specified CSV-specific options.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created CSV file.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.CsvExportOptions" /> object which specifies the CSV export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToCsv(string filePath, CsvExportOptions options)
    {
      PrintHelper.ExportToCsv((IPrintableControl) this, filePath, options);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in XLS format.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created XLS file should be sent.
    /// 
    ///           </param>
    public override void ExportToXls(Stream stream)
    {
      PrintHelper.ExportToXls((IPrintableControl) this, stream);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in XLS format, using the specified XLS-specific options.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created XLS file should be sent.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.XlsExportOptions" /> object which specifies the XLS export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToXls(Stream stream, XlsExportOptions options)
    {
      PrintHelper.ExportToXls((IPrintableControl) this, stream, options);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in XLS format.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created XLS file.
    /// 
    ///           </param>
    public override void ExportToXls(string filePath)
    {
      PrintHelper.ExportToXls((IPrintableControl) this, filePath);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in XLS format using the specified XLS-specific options.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created XLS file.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.XlsExportOptions" /> object which specifies the XLS export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToXls(string filePath, XlsExportOptions options)
    {
      PrintHelper.ExportToXls((IPrintableControl) this, filePath, options);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in XLSX format.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created XLSX file should be sent.
    /// 
    ///           </param>
    public override void ExportToXlsx(Stream stream)
    {
      PrintHelper.ExportToXlsx((IPrintableControl) this, stream);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified stream in XLSX format, using the specified XLSX-specific options.
    /// </para>
    ///             </summary>
    /// <param name="stream">
    /// A <see cref="T:System.IO.Stream" /> object to which the created XLSX file should be sent.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.XlsxExportOptions" /> object which specifies the XLSX export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToXlsx(Stream stream, XlsxExportOptions options)
    {
      PrintHelper.ExportToXlsx((IPrintableControl) this, stream, options);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in XLSX format.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created XLSX file.
    /// 
    ///           </param>
    public override void ExportToXlsx(string filePath)
    {
      PrintHelper.ExportToXlsx((IPrintableControl) this, filePath);
    }

    /// <summary>
    ///                 <para>Exports a grid to the specified file path in XLSX format, using the specified XLSX-specific options.
    /// </para>
    ///             </summary>
    /// <param name="filePath">
    /// A <see cref="T:System.String" /> which specifies the file name (including the full path) for the created XLSX file.
    /// 
    ///           </param>
    /// <param name="options">
    /// A <see cref="T:DevExpress.XtraPrinting.XlsxExportOptions" /> object which specifies the XLSX export options to be applied when a grid is exported.
    /// 
    ///           </param>
    public override void ExportToXlsx(string filePath, XlsxExportOptions options)
    {
      PrintHelper.ExportToXlsx((IPrintableControl) this, filePath, options);
    }

    public enum NavObjectDirection
    {
      Left,
      Top,
      Bottom,
      Right,
    }
  }
}
