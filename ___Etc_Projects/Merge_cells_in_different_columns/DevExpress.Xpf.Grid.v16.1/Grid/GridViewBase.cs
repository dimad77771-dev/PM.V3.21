// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridViewBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Core;
using DevExpress.Data;
using DevExpress.Data.Utils.ServiceModel;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid.Helpers;
using DevExpress.Xpf.Grid.LookUp.Native;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing.BrickCollection;
using DevExpress.Xpf.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media.Animation;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Serves as the base for objects representing Table and Card views in a grid control.
  /// </para>
  ///             </summary>
  [SelectedItemsSourceBrowsable]
  public abstract class GridViewBase : GridDataViewBase
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ClipboardCopyMaxRowCountInServerModeProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.ShowGroupedColumns" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowGroupedColumnsProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.ShowGroupPanel" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowGroupPanelProperty;
    private static readonly DependencyPropertyKey IsGroupPanelVisiblePropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupPanelVisibleProperty;
    protected static readonly DependencyPropertyKey IsGroupPanelTextVisiblePropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupPanelTextVisibleProperty;
    private static readonly DependencyPropertyKey VisibleColumnsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.VisibleColumns" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty VisibleColumnsProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.AllowGrouping" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowGroupingProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.UseAnimationWhenExpanding" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty UseAnimationWhenExpandingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupRowStyleProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupValueContentStyle" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueContentStyleProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupSummaryContentStyle" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryContentStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DefaultGroupSummaryItemTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupSummaryItemTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryItemTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupSummaryItemTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryItemTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualGroupSummaryItemTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.ActualGroupSummaryItemTemplateSelector" /> dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupSummaryItemTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupValueTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupValueTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupRowTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.GroupRowTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupRowTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualGroupRowTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.ActualGroupRowTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupRowTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.IsGroupPanelMenuEnabled" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupPanelMenuEnabledProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupFooterMenuEnabledProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridViewBase.FocusedColumn" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedColumnProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowDateTimeGroupIntervalMenuProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ImmediateUpdateRowPositionProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExpandStoryboardProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CollapseStoryboardProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent RowUpdatedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent RowCanceledEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CellValueChangedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CellValueChangingEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent InvalidRowExceptionEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent SelectionChangedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CopyingToClipboardEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent ShowingEditorEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent ShownEditorEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent HiddenEditorEvent;
    private static readonly DependencyPropertyKey ActualShowCheckBoxSelectorInGroupRowPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualShowCheckBoxSelectorInGroupRowProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty IsGroupRowMenuEnabledProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupColumnSummaryElementStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintGroupRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintGroupRowStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintAllGroupsProperty;
    private Lazy<BarManagerMenuController> groupPanelMenuControllerValue;
    private Lazy<BarManagerMenuController> groupFooterMenuControllerValue;
    private Lazy<BarManagerMenuController> groupRowMenuControllerValue;
    private SelectionAnchor globalSelectionAnchor;
    private SelectionActionBase globalSelectionAction;
    private ActualTemplateSelectorWrapper actualGroupValueTemplateSelector;
    private EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> createRootNodeCompletedHandler;

    /// <summary>
    ///                 <para>Gets or sets the maximum number of rows/cards that can be copied to the clipboard in Server Mode.
    /// 
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the maximum number of rows/cards to be copied to the clipboard.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseClipboardCopyMaxRowCountInServerMode")]
    [Category("Options Copy")]
    public int ClipboardCopyMaxRowCountInServerMode
    {
      get
      {
        return (int) this.GetValue(GridViewBase.ClipboardCopyMaxRowCountInServerModeProperty);
      }
      set
      {
        this.SetValue(GridViewBase.ClipboardCopyMaxRowCountInServerModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the focused column.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the focused column.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Obsolete("Use the DataControlBase.CurrentColumn property instead")]
    [Browsable(false)]
    public GridColumn FocusedColumn
    {
      get
      {
        return (GridColumn) this.GetValue(GridViewBase.FocusedColumnProperty);
      }
      set
      {
        this.SetValue(GridViewBase.FocusedColumnProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para> Gets or sets whether to play animation when expanding group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to play animation when expanding group rows; <b>false</b> to immediately expand/collapse group rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseUseAnimationWhenExpanding")]
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool UseAnimationWhenExpanding
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.UseAnimationWhenExpandingProperty);
      }
      set
      {
        this.SetValue(GridViewBase.UseAnimationWhenExpandingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that specifies whether an end-user can group data. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to group data; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseAllowGrouping")]
    [Category("Options Behavior")]
    public bool AllowGrouping
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.AllowGroupingProperty);
      }
      set
      {
        this.SetValue(GridViewBase.AllowGroupingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can change interval grouping of date-time columns via a column's context menu. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user can change interval grouping of date-time columns via a column's context menu; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseAllowDateTimeGroupIntervalMenu")]
    [Category("Options Behavior")]
    public bool AllowDateTimeGroupIntervalMenu
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.AllowDateTimeGroupIntervalMenuProperty);
      }
      set
      {
        this.SetValue(GridViewBase.AllowDateTimeGroupIntervalMenuProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the list of visible columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns.</value>
    [Category("Options Layout")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseVisibleColumns")]
    public IList<GridColumn> VisibleColumns
    {
      get
      {
        return (IList<GridColumn>) this.GetValue(GridViewBase.VisibleColumnsProperty);
      }
      protected set
      {
        this.SetValue(GridViewBase.VisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the Group Panel is displayed within a View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show the Group Panel; otherwise, <b>false</b>.
    /// </value>
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseShowGroupPanel")]
    [XtraSerializableProperty]
    [GridUIProperty]
    public bool ShowGroupPanel
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.ShowGroupPanelProperty);
      }
      set
      {
        this.SetValue(GridViewBase.ShowGroupPanelProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the Group Panel is displayed within a View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the group panel is displayed within a View; otherwise, <b>false</b>.
    /// </value>
    public bool IsGroupPanelVisible
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.IsGroupPanelVisibleProperty);
      }
      private set
      {
        this.SetValue(GridViewBase.IsGroupPanelVisiblePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the 'Drag a column header here to group by that column' string is displayed within the Group Panel. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the information hint is displayed within the Group Panel; otherwise, <b>false</b>.
    /// </value>
    public bool IsGroupPanelTextVisible
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.IsGroupPanelTextVisibleProperty);
      }
      private set
      {
        this.SetValue(GridViewBase.IsGroupPanelTextVisiblePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the grouped columns are displayed within a view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the grouped columns within a view; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseShowGroupedColumns")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool ShowGroupedColumns
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.ShowGroupedColumnsProperty);
      }
      set
      {
        this.SetValue(GridViewBase.ShowGroupedColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to the border around column values displayed within group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to the border around column values displayed within group rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupValueContentStyle")]
    [Category("Appearance ")]
    public Style GroupValueContentStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewBase.GroupValueContentStyleProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupValueContentStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group summary items. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group summary items.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupSummaryContentStyle")]
    public Style GroupSummaryContentStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewBase.GroupSummaryContentStyleProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupSummaryContentStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupRowStyle")]
    [Category("Appearance ")]
    public Style GroupRowStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewBase.GroupRowStyleProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupRowStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the template that defines the default presentation of summary items displayed within group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the default presentation of summary items displayed within group rows.
    /// </value>
    [Category("Appearance ")]
    [Browsable(false)]
    public DataTemplate DefaultGroupSummaryItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewBase.DefaultGroupSummaryItemTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewBase.DefaultGroupSummaryItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of summary items displayed within group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of summary items displayed within group rows.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupSummaryItemTemplate")]
    public DataTemplate GroupSummaryItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewBase.GroupSummaryItemTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupSummaryItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group summary template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupSummaryItemTemplateSelector")]
    [Category("Appearance ")]
    public DataTemplateSelector GroupSummaryItemTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewBase.GroupSummaryItemTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupSummaryItemTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a group summary item template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseActualGroupSummaryItemTemplateSelector")]
    public DataTemplateSelector ActualGroupSummaryItemTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewBase.ActualGroupSummaryItemTemplateSelectorProperty);
      }
      private set
      {
        this.SetValue(GridViewBase.ActualGroupSummaryItemTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of column values displayed within group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group values.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupValueTemplate")]
    [Category("Appearance ")]
    public DataTemplate GroupValueTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewBase.GroupValueTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupValueTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group row value template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupValueTemplateSelector")]
    public DataTemplateSelector GroupValueTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewBase.GroupValueTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupValueTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupRowTemplate")]
    [Category("Appearance ")]
    public DataTemplate GroupRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewBase.GroupRowTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupRowTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector GroupRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewBase.GroupRowTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupRowTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a group row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseActualGroupRowTemplateSelector")]
    public DataTemplateSelector ActualGroupRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridViewBase.ActualGroupRowTemplateSelectorProperty);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the column context menu is shown when an end-user right-clicks within the Group Panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the group panel context menu; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseIsGroupPanelMenuEnabled")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool IsGroupPanelMenuEnabled
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.IsGroupPanelMenuEnabledProperty);
      }
      set
      {
        this.SetValue(GridViewBase.IsGroupPanelMenuEnabledProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the group row context menu is shown when an end-user right-clicks within the Group Row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the group footer context menu; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool IsGroupRowMenuEnabled
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.IsGroupRowMenuEnabledProperty);
      }
      set
      {
        this.SetValue(GridViewBase.IsGroupRowMenuEnabledProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the group footer context menu is shown when an end-user right-clicks within the Group Footer. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the group footer context menu; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseIsGroupFooterMenuEnabled")]
    public bool IsGroupFooterMenuEnabled
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.IsGroupFooterMenuEnabledProperty);
      }
      set
      {
        this.SetValue(GridViewBase.IsGroupFooterMenuEnabledProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether a row's position is immediately updated according to the current sorting, grouping and filtering settings after the row's data has been modified. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if a row's position is immediately updated after its data has been modified; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseImmediateUpdateRowPosition")]
    [Category("Appearance ")]
    public bool ImmediateUpdateRowPosition
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.ImmediateUpdateRowPositionProperty);
      }
      set
      {
        this.SetValue(GridViewBase.ImmediateUpdateRowPositionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public Storyboard ExpandStoryboard
    {
      get
      {
        return (Storyboard) this.GetValue(GridViewBase.ExpandStoryboardProperty);
      }
      set
      {
        this.SetValue(GridViewBase.ExpandStoryboardProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public Storyboard CollapseStoryboard
    {
      get
      {
        return (Storyboard) this.GetValue(GridViewBase.CollapseStoryboardProperty);
      }
      set
      {
        this.SetValue(GridViewBase.CollapseStoryboardProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to individual text elements in group summary items that is aligned by columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to individual text elements in group summary items that are aligned by columns.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupColumnSummaryElementStyle")]
    public Style GroupColumnSummaryElementStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewBase.GroupColumnSummaryElementStyleProperty);
      }
      set
      {
        this.SetValue(GridViewBase.GroupColumnSummaryElementStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the Selector Column is shown for group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the selector column is shown for group rows; otherwise, <b>false</b>.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public bool ActualShowCheckBoxSelectorInGroupRow
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.ActualShowCheckBoxSelectorInGroupRowProperty);
      }
      internal set
      {
        this.SetValue(GridViewBase.ActualShowCheckBoxSelectorInGroupRowPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of group rows when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group rows when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBasePrintGroupRowTemplate")]
    [Category("Appearance Print")]
    public DataTemplate PrintGroupRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridViewBase.PrintGroupRowTemplateProperty);
      }
      set
      {
        this.SetValue(GridViewBase.PrintGroupRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group rows when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group rows when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    public Style PrintGroupRowStyle
    {
      get
      {
        return (Style) this.GetValue(GridViewBase.PrintGroupRowStyleProperty);
      }
      set
      {
        this.SetValue(GridViewBase.PrintGroupRowStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the grid is printed with all group rows expanded.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print the grid with all group rows expanded; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [Category("Options Print")]
    [XtraSerializableProperty]
    public bool PrintAllGroups
    {
      get
      {
        return (bool) this.GetValue(GridViewBase.PrintAllGroupsProperty);
      }
      set
      {
        this.SetValue(GridViewBase.PrintAllGroupsProperty, (object) value);
      }
    }

    internal override bool AllowGroupingCore
    {
      get
      {
        return this.AllowGrouping;
      }
    }

    internal override ActualTemplateSelectorWrapper ActualGroupValueTemplateSelectorCore
    {
      get
      {
        return this.actualGroupValueTemplateSelector;
      }
    }

    /// <summary>
    ///                 <para>Gets the context menu currently being displayed within a View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridPopupMenu" /> object that represents the context menu displayed within a View. <b>null</b> (<b>Nothing</b> in Visual Basic) if no menu is currently displayed within a View.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGridMenu")]
    public GridPopupMenu GridMenu
    {
      get
      {
        return (GridPopupMenu) this.DataControlMenu;
      }
    }

    internal BarManagerMenuController GroupRowMenuController
    {
      get
      {
        return this.groupRowMenuControllerValue.Value;
      }
    }

    internal BarManagerMenuController GroupPanelMenuController
    {
      get
      {
        return this.groupPanelMenuControllerValue.Value;
      }
    }

    internal BarManagerMenuController GroupFooterMenuController
    {
      get
      {
        return this.groupFooterMenuControllerValue.Value;
      }
    }

    /// <summary>
    ///                 <para>Allows you to customize the <b>Group Panel</b> context menu by adding new menu items or removing existing items.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Bars.BarManagerActionCollection" /> object.
    /// </value>
    [Browsable(false)]
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupPanelMenuCustomizations")]
    public BarManagerActionCollection GroupPanelMenuCustomizations
    {
      get
      {
        return this.GroupPanelMenuController.ActionContainer.Actions;
      }
    }

    /// <summary>
    ///                 <para>Allows you to customize the Group Footer context menu by adding new menu items or removing existing items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Bars.BarManagerActionCollection" /> object.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupFooterMenuCustomizations")]
    [Browsable(false)]
    public BarManagerActionCollection GroupFooterMenuCustomizations
    {
      get
      {
        return this.GroupFooterMenuController.ActionContainer.Actions;
      }
    }

    /// <summary>
    ///                 <para>Allows you to customize the <b>Group Row</b> context menu by adding new menu items or removing existing items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Bars.BarManagerActionCollection" /> object.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupRowMenuCustomizations")]
    [Browsable(false)]
    public BarManagerActionCollection GroupRowMenuCustomizations
    {
      get
      {
        return this.GroupRowMenuController.ActionContainer.Actions;
      }
    }

    internal IDesignTimeGridAdorner DesignTimeGridAdorner
    {
      get
      {
        if (this.Grid == null)
          return (IDesignTimeGridAdorner) EmptyDesignTimeGridAdorner.Instance;
        return this.Grid.DesignTimeGridAdorner;
      }
    }

    internal override int GroupCount
    {
      get
      {
        return this.Grid.GroupCount;
      }
    }

    /// <summary>
    ///                 <para>Gets the list of grouped columns.
    /// </para>
    ///             </summary>
    /// <value>The list containing grouped columns.</value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGroupedColumns")]
    [Category("Options Layout")]
    public IList<GridColumn> GroupedColumns
    {
      get
      {
        return this.Grid.GroupedColumns;
      }
    }

    internal FrameworkElement GroupPanel { get; set; }

    protected internal ClipboardController ClipboardController
    {
      get
      {
        return base.ClipboardController as ClipboardController;
      }
    }

    /// <summary>
    ///                 <para>Gets the grid which owns the current View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridControl" /> object that represents the grid to which the current View belongs.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGrid")]
    public GridControl Grid
    {
      get
      {
        return (GridControl) this.DataControl;
      }
    }

    protected internal virtual GridColumnCollection Columns
    {
      get
      {
        return (GridColumnCollection) this.ColumnsCore;
      }
    }

    protected internal GridSortInfoCollection SortInfo
    {
      get
      {
        return this.Grid.SortInfo;
      }
    }

    internal SelectionAnchor GlobalSelectionAnchor
    {
      get
      {
        return ((GridViewBase) this.RootView).globalSelectionAnchor;
      }
      private set
      {
        ((GridViewBase) this.RootView).globalSelectionAnchor = value;
      }
    }

    internal SelectionActionBase GlobalSelectionAction
    {
      get
      {
        return ((GridViewBase) this.RootView).globalSelectionAction;
      }
      private set
      {
        ((GridViewBase) this.RootView).globalSelectionAction = value;
      }
    }

    /// <summary>
    ///                 <para>Gets view commands.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridViewCommandsBase" /> object that provides access to view commands.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewBaseGridViewCommands")]
    public GridViewCommandsBase GridViewCommands
    {
      get
      {
        return (GridViewCommandsBase) this.Commands;
      }
    }

    internal GridViewBase EventTargetGridView
    {
      get
      {
        return (GridViewBase) this.EventTargetView;
      }
    }

    protected internal virtual bool IsGroupRowOptimized
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///                 <para>Occurs after the changes made within the focused row are posted to a data source.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler RowUpdated
    {
      add
      {
        this.AddHandler(GridViewBase.RowUpdatedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.RowUpdatedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after changes made in a row have been discarded.
    /// 
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler RowCanceled
    {
      add
      {
        this.AddHandler(GridViewBase.RowCanceledEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.RowCanceledEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a cell's value has been changed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event CellValueChangedEventHandler CellValueChanged
    {
      add
      {
        this.AddHandler(GridViewBase.CellValueChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.CellValueChangedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires in response to changing the edit value.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event CellValueChangedEventHandler CellValueChanging
    {
      add
      {
        this.AddHandler(GridViewBase.CellValueChangingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.CellValueChangingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires when a row fails validation or when it cannot be saved to a data source.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event InvalidRowExceptionEventHandler InvalidRowException
    {
      add
      {
        this.AddHandler(GridViewBase.InvalidRowExceptionEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.InvalidRowExceptionEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after grid's selection has been changed.
    /// </para>
    ///             </summary>
    [Obsolete("Use the GridControl.SelectionChanged event instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public event GridSelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.AddHandler(GridViewBase.SelectionChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.SelectionChangedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when grid data is copied to the clipboard, allowing you to manually copy required data.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the GridControl.CopyingToClipboard event instead")]
    public event CopyingToClipboardEventHandler CopyingToClipboard
    {
      add
      {
        this.AddHandler(GridViewBase.CopyingToClipboardEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.CopyingToClipboardEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after the focused cell's editor has been shown.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event EditorEventHandler ShownEditor
    {
      add
      {
        this.AddHandler(GridViewBase.ShownEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.ShownEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to prevent an end-user from activating editors of individual cells.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event ShowingEditorEventHandler ShowingEditor
    {
      add
      {
        this.AddHandler(GridViewBase.ShowingEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.ShowingEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a cell's editor has been closed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event EditorEventHandler HiddenEditor
    {
      add
      {
        this.AddHandler(GridViewBase.HiddenEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridViewBase.HiddenEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify whether the focused row's data is valid, and whether the row can lose focus.
    /// </para>
    ///             </summary>
    public event GridRowValidationEventHandler ValidateRow;

    /// <summary>
    ///                 <para>Enables you to specify whether the focused cell's data is valid, and whether the cell's editor can be closed.
    /// </para>
    ///             </summary>
    public event GridCellValidationEventHandler ValidateCell;

    static GridViewBase()
    {
      Type ownerType = typeof (GridViewBase);
      GridViewSelectionControlWrapper.Register();
      GridViewBase.ClipboardCopyMaxRowCountInServerModeProperty = DependencyPropertyManager.Register("ClipboardCopyMaxRowCountInServerMode", typeof (int), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 1000));
      GridViewBase.ShowGroupedColumnsProperty = DependencyPropertyManager.Register("ShowGroupedColumns", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).RebuildVisibleColumns())));
      GridViewBase.ShowGroupPanelProperty = DependencyPropertyManager.Register("ShowGroupPanel", typeof (bool), ownerType, new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).UpdateMasterDetailViewProperties())));
      GridViewBase.IsGroupPanelVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGroupPanelVisible", typeof (bool), ownerType, new PropertyMetadata((object) true));
      GridViewBase.IsGroupPanelVisibleProperty = GridViewBase.IsGroupPanelVisiblePropertyKey.DependencyProperty;
      GridViewBase.IsGroupPanelTextVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGroupPanelTextVisible", typeof (bool), ownerType, new PropertyMetadata((object) true));
      GridViewBase.IsGroupPanelTextVisibleProperty = GridViewBase.IsGroupPanelTextVisiblePropertyKey.DependencyProperty;
      GridViewBase.VisibleColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("VisibleColumns", typeof (IList<GridColumn>), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      GridViewBase.VisibleColumnsProperty = GridViewBase.VisibleColumnsPropertyKey.DependencyProperty;
      GridViewBase.AllowGroupingProperty = DependencyPropertyManager.Register("AllowGrouping", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsViewInfo)));
      GridViewBase.RowUpdatedEvent = EventManager.RegisterRoutedEvent("RowUpdated", RoutingStrategy.Direct, typeof (RowEventHandler), ownerType);
      GridViewBase.RowCanceledEvent = EventManager.RegisterRoutedEvent("RowCanceled", RoutingStrategy.Direct, typeof (RowEventHandler), ownerType);
      GridViewBase.CellValueChangedEvent = EventManager.RegisterRoutedEvent("CellValueChanged", RoutingStrategy.Direct, typeof (CellValueChangedEventHandler), ownerType);
      GridViewBase.CellValueChangingEvent = EventManager.RegisterRoutedEvent("CellValueChanging", RoutingStrategy.Direct, typeof (CellValueChangedEventHandler), ownerType);
      GridViewBase.InvalidRowExceptionEvent = EventManager.RegisterRoutedEvent("InvalidRowException", RoutingStrategy.Direct, typeof (InvalidRowExceptionEventHandler), ownerType);
      GridViewBase.SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof (GridSelectionChangedEventHandler), ownerType);
      GridViewBase.CopyingToClipboardEvent = EventManager.RegisterRoutedEvent("CopyingToClipboard", RoutingStrategy.Direct, typeof (CopyingToClipboardEventHandler), ownerType);
      GridViewBase.UseAnimationWhenExpandingProperty = DependencyPropertyManager.Register("UseAnimationWhenExpanding", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      GridViewBase.FocusedColumnProperty = DependencyPropertyManager.Register("FocusedColumn", typeof (GridColumn), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnFocusedColumnChanged), (CoerceValueCallback) ((d, e) => (object) ((DataViewBase) d).CoerceFocusedColumn((ColumnBase) e))));
      GridViewBase.DefaultGroupSummaryItemTemplateProperty = DependencyPropertyManager.Register("DefaultGroupSummaryItemTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      GridViewBase.GroupSummaryItemTemplateProperty = DependencyPropertyManager.Register("GroupSummaryItemTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateActualGroupSummaryItemTemplateSelector())));
      GridViewBase.GroupSummaryItemTemplateSelectorProperty = DependencyPropertyManager.Register("GroupSummaryItemTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateActualGroupSummaryItemTemplateSelector())));
      GridViewBase.ActualGroupSummaryItemTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupSummaryItemTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      GridViewBase.ActualGroupSummaryItemTemplateSelectorProperty = GridViewBase.ActualGroupSummaryItemTemplateSelectorPropertyKey.DependencyProperty;
      GridViewBase.GroupValueTemplateProperty = DependencyPropertyManager.Register("GroupValueTemplate", typeof (DataTemplate), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateColumnsActualGroupValueTemplateSelector())));
      GridViewBase.GroupValueTemplateSelectorProperty = DependencyPropertyManager.Register("GroupValueTemplateSelector", typeof (DataTemplateSelector), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateColumnsActualGroupValueTemplateSelector())));
      GridViewBase.GroupRowTemplateProperty = DependencyPropertyManager.Register("GroupRowTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateActualGroupRowTemplateSelector())));
      GridViewBase.GroupRowTemplateSelectorProperty = DependencyPropertyManager.Register("GroupRowTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateActualGroupRowTemplateSelector())));
      GridViewBase.ActualGroupRowTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupRowTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).OnActualGroupRowTemplateSelectorChanged())));
      GridViewBase.ActualGroupRowTemplateSelectorProperty = GridViewBase.ActualGroupRowTemplateSelectorPropertyKey.DependencyProperty;
      GridViewBase.GroupRowStyleProperty = DependencyPropertyManager.Register("GroupRowStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).OnGroupRowStyleChanged())));
      GridViewBase.GroupValueContentStyleProperty = DependencyPropertyManager.Register("GroupValueContentStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).OnGroupValueContentStyleChanged())));
      GridViewBase.GroupSummaryContentStyleProperty = DependencyPropertyManager.Register("GroupSummaryContentStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).OnGroupSummaryContentStyleChanged())));
      GridViewBase.IsGroupPanelMenuEnabledProperty = DependencyPropertyManager.Register("IsGroupPanelMenuEnabled", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      GridViewBase.IsGroupFooterMenuEnabledProperty = DependencyPropertyManager.Register("IsGroupFooterMenuEnabled", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      GridViewBase.IsGroupRowMenuEnabledProperty = DependencyPropertyManager.Register("IsGroupRowMenuEnabled", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      GridViewBase.AllowDateTimeGroupIntervalMenuProperty = DependencyPropertyManager.Register("AllowDateTimeGroupIntervalMenu", typeof (bool), ownerType, new PropertyMetadata((object) true));
      GridViewBase.ImmediateUpdateRowPositionProperty = DependencyPropertyManager.Register("ImmediateUpdateRowPosition", typeof (bool), typeof (GridViewBase), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).RefreshImmediateUpdateRowPositionProperty())));
      GridViewBase.ExpandStoryboardProperty = DependencyPropertyManager.RegisterAttached("ExpandStoryboard", typeof (Storyboard), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ExpandHelper.SetExpandStoryboard(d, (Storyboard) e.NewValue))));
      GridViewBase.CollapseStoryboardProperty = DependencyPropertyManager.RegisterAttached("CollapseStoryboard", typeof (Storyboard), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ExpandHelper.SetCollapseStoryboard(d, (Storyboard) e.NewValue))));
      GridViewBase.ShowingEditorEvent = EventManager.RegisterRoutedEvent("ShowingEditor", RoutingStrategy.Direct, typeof (ShowingEditorEventHandler), ownerType);
      GridViewBase.ShownEditorEvent = EventManager.RegisterRoutedEvent("ShownEditor", RoutingStrategy.Direct, typeof (EditorEventHandler), ownerType);
      GridViewBase.HiddenEditorEvent = EventManager.RegisterRoutedEvent("HiddenEditor", RoutingStrategy.Direct, typeof (EditorEventHandler), ownerType);
      GridViewBase.RegisterClassCommandBindings();
      GridViewBase.ActualShowCheckBoxSelectorInGroupRowPropertyKey = DependencyProperty.RegisterReadOnly("ActualShowCheckBoxSelectorInGroupRow", typeof (bool), ownerType, new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).OnActualShowCheckBoxSelectorInGroupRowChanged())));
      GridViewBase.ActualShowCheckBoxSelectorInGroupRowProperty = GridViewBase.ActualShowCheckBoxSelectorInGroupRowPropertyKey.DependencyProperty;
      GridViewBase.GroupColumnSummaryElementStyleProperty = DependencyPropertyManager.Register("GroupColumnSummaryElementStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).OnSummaryDataChanged())));
      GridViewBase.PrintGroupRowTemplateProperty = DependencyProperty.Register("PrintGroupRowTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      GridViewBase.PrintGroupRowStyleProperty = DependencyProperty.Register("PrintGroupRowStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      GridViewBase.PrintAllGroupsProperty = DependencyProperty.Register("PrintAllGroups", typeof (bool), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) true));
    }

    internal GridViewBase(MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem, DataControlDetailDescriptor detailDescriptor)
      : base(masterRootNode, masterRootDataItem, detailDescriptor)
    {
      this.UpdateColumnsActualGroupValueTemplateSelector();
      this.groupPanelMenuControllerValue = this.CreateMenuControllerLazyValue();
      this.groupFooterMenuControllerValue = this.CreateMenuControllerLazyValue();
      this.groupRowMenuControllerValue = this.CreateMenuControllerLazyValue();
      this.UpdateViewInfo();
    }

    private static void RegisterClassCommandBindings()
    {
      Type type = typeof (GridViewBase);
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.ChangeGroupExpanded, (ExecutedRoutedEventHandler) ((d, e) => ((GridViewBase) d).ChangeGroupExpanded(e.Parameter))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.ExpandAllGroups, (ExecutedRoutedEventHandler) ((d, e) => ((GridViewBase) d).ExpandAllGroups((object) e)), (CanExecuteRoutedEventHandler) ((d, e) => ((GridViewBase) d).OnCanExpandCollapseAll(e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.CollapseAllGroups, (ExecutedRoutedEventHandler) ((d, e) => ((GridViewBase) d).CollapseAllGroups((object) e)), (CanExecuteRoutedEventHandler) ((d, e) => ((GridViewBase) d).OnCanExpandCollapseAll(e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.MoveParentGroupRow, (ExecutedRoutedEventHandler) ((d, e) => ((GridViewBase) d).MoveParentGroupRow()), (CanExecuteRoutedEventHandler) ((d, e) => e.CanExecute = ((GridViewBase) d).CanMoveGroupParentRow())));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.ClearGrouping, (ExecutedRoutedEventHandler) ((d, e) => ((GridViewBase) d).ClearGrouping()), (CanExecuteRoutedEventHandler) ((d, e) => ((GridViewBase) d).OnCanClearGrouping(e))));
    }

    private void OnCanExpandCollapseAll(CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.GetIsGrouped();
    }

    private void OnCanClearGrouping(CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.CanClearGrouping();
    }

    protected static SimpleBridgeList<GridColumn, ColumnBase> ConvertToGridColumnsList(IList<ColumnBase> columns)
    {
      return new SimpleBridgeList<GridColumn, ColumnBase>(columns, (Func<ColumnBase, GridColumn>) null, (Func<GridColumn, ColumnBase>) null);
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
    public bool ShouldSerializeVisibleColumns(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    protected override RowsClipboardController CreateClipboardController()
    {
      return (RowsClipboardController) new ClipboardController(this);
    }

    private void UpdateViewInfo()
    {
    }

    internal override IColumnCollection CreateEmptyColumnCollection()
    {
      return (IColumnCollection) new GridColumnCollection((GridControl) null);
    }

    protected override bool ChangeVisibleRowExpandCore(int rowHandle)
    {
      if (!this.DataProviderBase.IsGroupRow(this.Grid.GetRowVisibleIndexByHandle(rowHandle)))
        return false;
      this.ChangeGroupExpandedWithAnimation(rowHandle, this.Grid.IsRecursiveExpand);
      return true;
    }

    internal override bool IsDataRowNodeExpanded(DataRowNode rowNode)
    {
      GroupNode groupNode = rowNode as GroupNode;
      if (groupNode == null)
        return false;
      return groupNode.IsExpanded;
    }

    internal override bool IsExpandableRowFocused()
    {
      return this.IsGroupRowFocused();
    }

    internal override FrameworkElement CreateRowElement(RowData rowData)
    {
      return (FrameworkElement) new GridRow();
    }

    internal override DependencyProperty GetFocusedColumnProperty()
    {
      return GridViewBase.FocusedColumnProperty;
    }

    protected override void SetVisibleColumns(IList<ColumnBase> columns)
    {
      this.VisibleColumns = (IList<GridColumn>) GridViewBase.ConvertToGridColumnsList(columns);
    }

    internal override DataController GetDataControllerForUnboundColumnsCore()
    {
      if (this.Grid == null)
        return (DataController) null;
      return this.Grid.DataController;
    }

    internal override bool CanCopyRows()
    {
      return this.ActualClipboardCopyAllowed && this.NavigationStyle != GridViewNavigationStyle.None && (!this.FocusedView.IsInvalidFocusedRowHandle || this.SelectionStrategy.GetGlobalSelectedRowCount() > 0) && this.ActiveEditor == null;
    }

    protected internal override void SetSummariesIgnoreNullValues(bool value)
    {
      if (this.Grid == null)
        return;
      this.Grid.DataController.SummariesIgnoreNullValues = value;
    }

    protected internal override void OnDataReset()
    {
      base.OnDataReset();
      this.RefreshImmediateUpdateRowPositionProperty();
    }

    internal void RefreshImmediateUpdateRowPositionProperty()
    {
      if (this.Grid == null || this.Grid.DataController == null)
        return;
      this.Grid.DataController.ImmediateUpdateRowPosition = this.ImmediateUpdateRowPosition;
    }

    protected override void GroupColumn(string fieldName, int index, ColumnSortOrder sortOrder)
    {
      this.SortInfo.GroupByColumn(fieldName, index, sortOrder);
    }

    protected override void UngroupColumn(string fieldName)
    {
      this.SortInfo.UngroupByColumn(fieldName);
    }

    internal override void UpdateMasterDetailViewProperties()
    {
      base.UpdateMasterDetailViewProperties();
      this.UpdateIsGroupPanelVisible();
      this.UpdateIsGroupPanelTextVisible();
    }

    private void UpdateIsGroupPanelTextVisible()
    {
      bool isAnyGridGrouped = false;
      this.UpdateAllOriginationViews((Action<DataViewBase>) (view =>
      {
        if (view.DataControl == null)
          return;
        isAnyGridGrouped |= ((GridControl) view.DataControl).IsGrouped;
      }));
      ((GridViewBase) this.RootView).IsGroupPanelTextVisible = !isAnyGridGrouped;
    }

    private void UpdateIsGroupPanelVisible()
    {
      bool isAnyGroupPanelVisible = false;
      this.UpdateAllOriginationViews((Action<DataViewBase>) (view => isAnyGroupPanelVisible |= ((GridViewBase) view).ShowGroupPanel));
      ((GridViewBase) this.RootView).IsGroupPanelVisible = isAnyGroupPanelVisible;
    }

    protected internal override void RaiseValidateCell(GridRowValidationEventArgs e)
    {
      if (this.ValidateCell == null)
        return;
      this.ValidateCell((object) this, (GridCellValidationEventArgs) e);
    }

    protected internal override bool SupportValidateCell()
    {
      if (this.EventTargetGridView != null)
        return this.EventTargetGridView.ValidateCell != null;
      return false;
    }

    internal override void RaiseHiddenEditor(int rowHandle, ColumnBase column, IBaseEdit editCore)
    {
      EditorEventArgs e = new EditorEventArgs(this, rowHandle, (GridColumn) column, editCore);
      e.RoutedEvent = GridViewBase.HiddenEditorEvent;
      this.RaiseHiddenEditor(e);
    }

    protected internal virtual void RaiseHiddenEditor(EditorEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    internal override void RaiseCellValueChanging(int rowHandle, ColumnBase column, object value, object oldValue)
    {
      this.RaiseCellValueChanging(new CellValueChangedEventArgs(GridViewBase.CellValueChangingEvent, this, rowHandle, (GridColumn) column, value, oldValue));
    }

    protected internal virtual void RaiseCellValueChanging(CellValueChangedEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    internal override void RaiseCellValueChanged(int rowHandle, ColumnBase column, object newValue, object oldValue)
    {
      this.RaiseCellValueChanged(new CellValueChangedEventArgs(GridViewBase.CellValueChangedEvent, this, rowHandle, (GridColumn) column, newValue, oldValue));
    }

    protected internal virtual void RaiseCellValueChanged(CellValueChangedEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    internal override bool RaiseShowingEditor(int rowHanlde, ColumnBase columnBase)
    {
      ShowingEditorEventArgs e = new ShowingEditorEventArgs(this, rowHanlde, (GridColumn) columnBase);
      this.RaiseShowingEditor(e);
      return !e.Cancel;
    }

    protected internal virtual void RaiseShowingEditor(ShowingEditorEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    internal override void RaiseShownEditor(int rowHandle, ColumnBase column, IBaseEdit editCore)
    {
      this.RaiseShownEditor(new EditorEventArgs(this, rowHandle, (GridColumn) column, editCore));
    }

    protected internal virtual void RaiseShownEditor(EditorEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    internal override RowValidationError CreateCellValidationError(object errorContent, Exception exception, ErrorType errorType, int rowHandle, ColumnBase column)
    {
      return (RowValidationError) new GridCellValidationError(errorContent, exception, errorType, rowHandle, (GridColumn) column);
    }

    internal override GridRowValidationEventArgs CreateCellValidationEventArgs(object source, object value, int rowHandle, ColumnBase column)
    {
      return (GridRowValidationEventArgs) new GridCellValidationEventArgs(source, value, rowHandle, (DataViewBase) this, column);
    }

    internal override BaseValidationError CreateCellValidationError(object errorContent, ErrorType errorType, int rowHandle, ColumnBase column)
    {
      return (BaseValidationError) new GridCellValidationError(errorContent, (Exception) null, errorType, rowHandle, (GridColumn) column);
    }

    internal override BaseValidationError CreateRowValidationError(object errorContent, ErrorType errorType, int rowHandle)
    {
      return (BaseValidationError) new GridRowValidationError(errorContent, (Exception) null, errorType, rowHandle);
    }

    internal override string RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string displayText)
    {
      return this.Grid.RaiseCustomDisplayText(rowHandle, listSourceIndex, column, value, displayText);
    }

    internal override bool? RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string originalDisplayText, out string displayText)
    {
      return this.Grid.RaiseCustomDisplayText(rowHandle, listSourceIndex, column, value, originalDisplayText, out displayText);
    }

    internal override DataControlPopupMenu CreatePopupMenu()
    {
      return (DataControlPopupMenu) new GridPopupMenu(this);
    }

    protected override DataIteratorBase CreateDataIterator()
    {
      return (DataIteratorBase) new GridDataIterator(this);
    }

    protected internal virtual void SetWaitIndicator()
    {
      if (this.IsColumnFilterOpened && !this.IsColumnFilterLoaded)
        return;
      this.IsWaitIndicatorVisible = true;
    }

    protected internal virtual void ClearWaitIndicator()
    {
      this.IsWaitIndicatorVisible = false;
    }

    protected internal override void BeforeMoveColumnToChooser(BaseColumn column, HeaderPresenterType sourceType)
    {
      GridColumn gridColumn = column as GridColumn;
      if (gridColumn == null || sourceType != HeaderPresenterType.GroupPanel && !gridColumn.IsGrouped)
        return;
      this.SortInfo.UngroupByColumn(gridColumn.FieldName);
    }

    protected override void NotifyDesignTimeAdornerOnColumnMoved(HeaderPresenterType moveFrom, HeaderPresenterType moveTo)
    {
      if (GridViewBase.IsGroupMoveAction(moveFrom, moveTo))
        this.DesignTimeGridAdorner.OnColumnMovedGroup();
      else
        base.NotifyDesignTimeAdornerOnColumnMoved(moveFrom, moveTo);
    }

    protected internal override void OnDataChanged(bool rebuildVisibleColumns)
    {
      this.DataProviderBase.AutoExpandAllGroups = this.Grid.AutoExpandAllGroups;
      base.OnDataChanged(rebuildVisibleColumns);
    }

    protected internal override void OnSummaryDataChanged()
    {
      base.OnSummaryDataChanged();
      this.UpdateGroupSummary();
    }

    private static bool IsGroupMoveAction(HeaderPresenterType moveFrom, HeaderPresenterType moveTo)
    {
      if (moveFrom != HeaderPresenterType.GroupPanel)
        return moveTo == HeaderPresenterType.GroupPanel;
      return true;
    }

    internal bool IsGroupRowFocused()
    {
      return this.Grid.IsGroupRowHandle(this.FocusedRowHandle);
    }

    protected internal override void UpdateGroupSummary()
    {
      this.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateGroupSummaryData()), true, true);
    }

    private void UpdateActualGroupSummaryItemTemplateSelector()
    {
      this.ActualGroupSummaryItemTemplateSelector = (DataTemplateSelector) new ActualTemplateSelectorWrapper(this.GroupSummaryItemTemplateSelector, this.GroupSummaryItemTemplate);
      this.UpdateGroupSummaryTemplates();
    }

    protected void UpdateGroupSummaryTemplates()
    {
      this.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateClientSummary()), true, true);
    }

    private void UpdateColumnsActualGroupValueTemplateSelector()
    {
      this.actualGroupValueTemplateSelector = new ActualTemplateSelectorWrapper(this.GroupValueTemplateSelector, this.GroupValueTemplate);
      if (this.Grid == null)
        return;
      foreach (GridColumn column in (Collection<GridColumn>) this.Grid.Columns)
        column.UpdateActualGroupValueTemplateSelector();
    }

    private void UpdateActualGroupRowTemplateSelector()
    {
      this.UpdateActualTemplateSelector(GridViewBase.ActualGroupRowTemplateSelectorPropertyKey, this.GroupRowTemplateSelector, this.GroupRowTemplate, (Func<DataTemplateSelector, DataTemplate, ActualTemplateSelectorWrapper>) null);
    }

    protected internal virtual GroupRowData CreateGroupRowDataCore(DataTreeBuilder treeBuilder)
    {
      return new GroupRowData(treeBuilder);
    }

    internal override bool IsColumnVisibleInHeaders(BaseColumn col)
    {
      GridColumn gridColumn = col as GridColumn;
      if (gridColumn == null)
        return base.IsColumnVisibleInHeaders(col);
      if (gridColumn.IsGrouped && !gridColumn.ShowGroupedColumn.GetValue(this.ShowGroupedColumns))
        return this.AllowPartialGroupingCore;
      return true;
    }

    internal bool IsGroupRow(int visibleIndex, int level)
    {
      return this.DataProviderBase.IsGroupRow(this.GetRowParentIndex(visibleIndex, level));
    }

    /// <summary>
    ///                 <para>Deletes the specified data row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row to delete.
    /// 
    ///           </param>
    public virtual void DeleteRow(int rowHandle)
    {
      this.DeleteRowCore(rowHandle);
    }

    internal override bool IsInvisibleGroupRow(RowNode node)
    {
      if (node is GroupNode)
        return !node.IsRowVisible;
      return false;
    }

    internal override bool CanStartDragSingleColumn()
    {
      return this.ShowGroupedColumns;
    }

    protected internal abstract FrameworkElement CreateGroupControl(GroupRowData rowData);

    internal override void SetFocusedRectangleOnGroupRow()
    {
      this.SetFocusedRectangleOnRowData(this.GetGroupRowFocusedRectangleTemplate());
    }

    protected override void HandleGroupMoveAction(ColumnBase source, int newVisibleIndex, HeaderPresenterType moveFrom, HeaderPresenterType moveTo)
    {
      if (!GridViewBase.IsGroupMoveAction(moveFrom, moveTo))
        return;
      this.SortInfo.OnGroupColumnMove(source.FieldName, newVisibleIndex, moveFrom == HeaderPresenterType.GroupPanel, moveTo == HeaderPresenterType.GroupPanel);
    }

    protected internal override bool RaiseCopyingToClipboard(CopyingToClipboardEventArgsBase e)
    {
      e.RoutedEvent = GridViewBase.CopyingToClipboardEvent;
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
      return e.Handled;
    }

    protected internal override void RaiseSelectionChanged(GridSelectionChangedEventArgs e)
    {
      e.RoutedEvent = GridViewBase.SelectionChangedEvent;
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    protected virtual void RaiseInvalidRowException(InvalidRowExceptionEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    protected internal virtual void RaiseValidateRow(GridRowValidationEventArgs e)
    {
      this.EventTargetGridView.RaiseValidateRowCore(e);
    }

    private void RaiseValidateRowCore(GridRowValidationEventArgs e)
    {
      if (this.ValidateRow == null)
        return;
      this.ValidateRow((object) this, e);
    }

    protected internal virtual void RaiseRowUpdated(RowEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    protected internal virtual void RaiseRowCanceled(RowEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    protected internal override object GetGroupDisplayValue(int rowHandle)
    {
      object groupRowValue = this.Grid.GetGroupRowValue(rowHandle);
      string fieldName = this.GetSortInfoBySortLevel(this.Grid.GetRowLevelByRowHandle(rowHandle)).FieldName;
      GridDataColumnSortInfo sortInfo = this.Grid.SortData.GetSortInfo(this.DataProviderBase.Columns[fieldName]);
      object val = groupRowValue;
      if (sortInfo != null)
        val = sortInfo.UpdateGroupDisplayValue(groupRowValue);
      string groupDisplayText = this.GetGroupDisplayText(rowHandle, val, this.Columns[fieldName], sortInfo == null ? (string) null : sortInfo.GetColumnGroupFormatString());
      if (sortInfo != null)
        groupDisplayText = sortInfo.GetGroupDisplayText(val, groupDisplayText);
      return (object) this.Grid.RaiseCustomDisplayText(new int?(rowHandle), new int?(), (ColumnBase) this.Grid.Columns[fieldName], groupRowValue, groupDisplayText);
    }

    protected internal override string GetGroupRowDisplayText(int rowHandle)
    {
      GridColumn gridColumn = (GridColumn) this.GetColumnBySortLevel(this.Grid.GetRowLevelByRowHandle(rowHandle));
      if (gridColumn != null)
        return string.Format("{0}: ", gridColumn.HeaderCaption) + (string) this.GetGroupDisplayValue(rowHandle);
      return string.Empty;
    }

    protected internal override string GetGroupRowHeaderCaption(int rowHandle)
    {
      GridColumn gridColumn = (GridColumn) this.GetColumnBySortLevel(this.Grid.GetRowLevelByRowHandle(rowHandle));
      if (gridColumn != null)
        return gridColumn.HeaderCaption.ToString();
      return string.Empty;
    }

    protected internal override GroupTextHighlightingProperties GetGroupHighlightingProperties(int rowHandle)
    {
      GridColumn gridColumn = (GridColumn) this.GetColumnBySortLevel(this.Grid.GetRowLevelByRowHandle(rowHandle));
      if (gridColumn != null && this.SearchPanelColumnProvider != null)
      {
        TextHighlightingProperties highlightingProperties = this.SearchPanelColumnProvider.GetTextHighlightingProperties((ColumnBase) gridColumn);
        if (highlightingProperties != null)
          return new GroupTextHighlightingProperties(highlightingProperties, gridColumn.ActualEditSettings);
      }
      return (GroupTextHighlightingProperties) null;
    }

    private string GetGroupDisplayText(int rowHandle, object value, GridColumn column, string formatString)
    {
      if (column == null)
        return string.Empty;
      object obj1 = value;
      object obj2;
      if (string.IsNullOrEmpty(formatString))
      {
        obj2 = this.GetDisplayObject(obj1, (ColumnBase) column);
      }
      else
      {
        if (column.GetSortMode() != ColumnSortMode.Value)
          obj1 = this.GetDisplayObject(obj1, (ColumnBase) column, false);
        obj2 = FormatStringConverter.GetFormattedValue(formatString, obj1, CultureInfo.CurrentCulture);
      }
      return this.Grid.RaiseCustomGroupDisplayText(rowHandle, column, value, obj2.ToString());
    }

    protected internal virtual void OnPostRowException(ControllerRowExceptionEventArgs e)
    {
      InvalidRowExceptionEventArgs e1 = new InvalidRowExceptionEventArgs(this, e.RowHandle, e.Exception.Message, this.GetLocalizedString(GridControlStringId.ErrorWindowTitle), e.Exception, ExceptionMode.DisplayError);
      this.RaiseInvalidRowException(e1);
      this.HandleInvalidRowExceptionEventArgs(e, (IInvalidRowExceptionEventArgs) e1);
    }

    /// <summary>
    ///                 <para>Invokes the Runtime Summary Editor.
    /// </para>
    ///             </summary>
    public void ShowGroupSummaryEditor()
    {
      new GridGroupSummaryHelper((DataViewBase) this).ShowSummaryEditor();
    }

    internal bool CanShowGroupSummaryEditor()
    {
      return true;
    }

    internal bool IsGroupRow(DependencyObject obj)
    {
      RowHandle rowHandle = DataViewBase.GetRowHandle(obj);
      if (rowHandle == null)
        return false;
      return this.Grid.IsGroupRowHandle(rowHandle.Value);
    }

    /// <summary>
    ///                 <para>Moves focus to the group row that owns the currently focused row.
    /// </para>
    ///             </summary>
    public virtual void MoveParentGroupRow()
    {
      this.MoveParentRow();
    }

    /// <summary>
    ///                 <para>Collapses the focused group row.
    /// </para>
    ///             </summary>
    /// <returns><b>true</b> if the focused group row has been collapsed; otherwise, <b>false</b>.
    /// </returns>
    public bool CollapseFocusedRow()
    {
      return this.CollapseFocusedRowCore();
    }

    /// <summary>
    ///                 <para>Expands the focused group row.
    /// </para>
    ///             </summary>
    /// <returns><b>true</b> if the group row has been expanded; otherwise, <b>false</b>.
    /// </returns>
    public bool ExpandFocusedRow()
    {
      return this.ExpandFocusedRowCore();
    }

    internal bool CanMoveGroupParentRow()
    {
      if (this.Grid != null)
        return this.HasParentRow(this.DataProviderBase.CurrentIndex);
      return false;
    }

    internal bool GetIsGrouped()
    {
      if (!this.IsRootView && !this.HasClonedExpandedDetails)
        return false;
      return this.GetIsGroupedCore();
    }

    internal bool GetIsGroupedCore()
    {
      if (this.Grid != null)
        return this.Grid.IsGrouped;
      return false;
    }

    internal void ChangeGroupExpanded(object commandParameter)
    {
      this.OnChangeGroupExpanded(commandParameter);
    }

    private void OnChangeGroupExpanded(object commandParameter)
    {
      if (!(commandParameter is int))
        return;
      this.ChangeGroupExpandedWithAnimation((int) commandParameter, ((GridControl) this.DataControl).IsRecursiveExpand);
    }

    internal void ChangeGroupExpandedWithAnimation(int rowHandle, bool recursive)
    {
      if (this.RootDataPresenter == null || !this.CommitEditing() || (!this.DataControl.IsValidRowHandleCore(rowHandle) || !this.Nodes.ContainsKey(rowHandle)) || this.GetRowElementByRowHandle(rowHandle) == null)
        return;
      GroupNode groupNode = (GroupNode) this.Nodes[rowHandle];
      if (this.UseAnimationWhenExpanding && !this.ActualAllowCellMerge)
        this.RootDataPresenter.EnqueueContinousAction((IContinousAction) new ExpandRowWithAnimationAction(this.RootDataPresenter, groupNode, recursive));
      else if (groupNode.IsExpanded)
        this.Grid.CollapseGroupRowWithEvents(groupNode.RowHandle.Value, recursive);
      else
        this.Grid.ExpandGroupRowWithEvents(groupNode.RowHandle.Value, recursive);
      this.RootDataPresenter.InvalidateMeasure();
    }

    internal void ExpandAllGroups(object commandParameter)
    {
      this.Grid.ExpandAllGroups();
    }

    internal void CollapseAllGroups(object commandParameter)
    {
      this.Grid.CollapseAllGroups();
    }

    internal void ClearGrouping()
    {
      this.Grid.ClearGrouping();
    }

    internal bool CanExpandCollapseAll(object commandParameter)
    {
      return this.GetIsGrouped();
    }

    internal bool CanClearGrouping()
    {
      if (this.AllowGrouping)
        return this.GetIsGroupedCore();
      return false;
    }

    /// <summary>
    ///                 <para>Prevents selection updates until the <see cref="M:DevExpress.Xpf.Grid.GridViewBase.EndSelection" /> method is called.
    /// </para>
    ///             </summary>
    [Obsolete("Use the DataControlBase.BeginSelection method instead")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void BeginSelection()
    {
      this.BeginSelectionCore();
    }

    /// <summary>
    ///                 <para>Enables selection updates after calling the <see cref="M:DevExpress.Xpf.Grid.GridViewBase.BeginSelection" /> method, and forces an immediate update.
    /// </para>
    ///             </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.EndSelection method instead")]
    [Browsable(false)]
    public void EndSelection()
    {
      this.EndSelectionCore();
    }

    /// <summary>
    ///                 <para>Selects the specified row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value which specifies the handle of the row to select.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectItem method instead")]
    public void SelectRow(int rowHandle)
    {
      this.SelectRowCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Unselects the specified row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the row by its handle.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.UnselectItem method instead")]
    [Browsable(false)]
    public void UnselectRow(int rowHandle)
    {
      this.UnselectRowCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Unselects any selected rows/cards within a View.
    /// </para>
    ///             </summary>
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.UnselectAll method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ClearSelection()
    {
      this.ClearSelectionCore();
    }

    /// <summary>
    ///                 <para>Selects multiple rows/cards, while preserving the current selection (if any).
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value specifying the row handle at which the selection starts.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value specifying the row handle at which the selection ends.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectRange method instead")]
    public void SelectRange(int startRowHandle, int endRowHandle)
    {
      this.SelectRangeCore(startRowHandle, endRowHandle);
    }

    protected internal override GridSelectionChangedEventArgs CreateSelectionChangedEventArgs(DevExpress.Data.SelectionChangedEventArgs e)
    {
      return new GridSelectionChangedEventArgs((DataViewBase) this, e.Action, e.ControllerRow);
    }

    protected internal override void ResetHeadersChildrenCache()
    {
      if (this.Grid.AutomationPeer == null)
        return;
      this.Grid.AutomationPeer.ResetHeadersChildrenCache();
    }

    internal virtual void PerformUpdateGroupSummaryDataAction(Action action)
    {
    }

    protected override void UpdateGridControlColumnProvider()
    {
      if (this.DataControl == null)
        return;
      GridControlColumnProvider controlColumnProvider = new GridControlColumnProvider();
      controlColumnProvider.FilterByColumnsMode = FilterByColumnsMode.Default;
      controlColumnProvider.AllowColumnsHighlighting = false;
      GridControlColumnProviderBase.SetColumnProvider((DependencyObject) this.DataControl, (GridControlColumnProviderBase) controlColumnProvider);
    }

    protected internal virtual InlineCollectionInfo GetGroupSummaryTextValues(ColumnBase column, int rowHandle, bool groupFooter)
    {
      SummaryInlineInfoCreator inlineInfoCreator = new SummaryInlineInfoCreator();
      Func<GridSummaryItem, Style> itemStyleSelector = (Func<GridSummaryItem, Style>) null;
      if (groupFooter)
      {
        inlineInfoCreator.SeparatorText = Environment.NewLine;
        inlineInfoCreator.DefaultStyle = this.GetGroupSummaryElementStyle(true);
        itemStyleSelector = (Func<GridSummaryItem, Style>) (s => s.GroupColumnFooterElementStyle);
      }
      else
      {
        inlineInfoCreator.SeparatorText = GridControlLocalizer.GetString(GridControlStringId.SummaryItemsSeparator);
        inlineInfoCreator.DefaultStyle = this.GetGroupSummaryElementStyle(false);
        itemStyleSelector = (Func<GridSummaryItem, Style>) (s => s.GroupColumnSummaryElementStyle);
      }
      inlineInfoCreator.GetSummaryStyle = (Func<SummaryItemBase, Style>) (s =>
      {
        GridSummaryItem gridSummaryItem = s as GridSummaryItem;
        if (gridSummaryItem != null)
          return itemStyleSelector(gridSummaryItem);
        return (Style) null;
      });
      string columnDisplayFormat = GroupRowData.GetColumnDisplayFormat(column);
      inlineInfoCreator.GetItemDisplayText = (Func<GridSummaryData, string>) (d =>
      {
        string str = (string) null;
        SummaryItemBase summaryItem = d.Item;
        if (summaryItem != null)
          str = summaryItem.GetGroupColumnDisplayText((IFormatProvider) CultureInfo.CurrentCulture, ColumnBase.GetSummaryDisplayName(this.DataControl.ColumnsCore[summaryItem.FieldName], summaryItem), d.Value, columnDisplayFormat);
        return str;
      });
      return inlineInfoCreator.Create(this.CreateGroupColumnSummaryData(column, rowHandle, groupFooter));
    }

    private IEnumerable<GridSummaryData> CreateGroupColumnSummaryData(ColumnBase column, int rowHandle, bool groupFooter)
    {
      foreach (GridSummaryItem gridSummaryItem in (IEnumerable<SummaryItemBase>) column.GroupSummariesCore)
      {
        object summaryValue = (object) null;
        if (this.DataProviderBase.TryGetGroupSummaryValue(rowHandle, (SummaryItemBase) gridSummaryItem, out summaryValue) && !(groupFooter ^ gridSummaryItem.ShowInGroupColumnFooter == column.FieldName))
          yield return new GridSummaryData((SummaryItemBase) gridSummaryItem, summaryValue, column);
      }
    }

    internal virtual Style GetGroupSummaryElementStyle(bool groupFooter)
    {
      return this.GroupColumnSummaryElementStyle;
    }

    protected internal string GetGroupSummaryText(ColumnBase column, int rowHandle, bool groupFooter)
    {
      InlineCollectionInfo summaryTextValues = this.GetGroupSummaryTextValues(column, rowHandle, groupFooter);
      if (summaryTextValues == null)
        return (string) null;
      return summaryTextValues.TextSource;
    }

    internal bool? AreAllItemsSelected(int groupRowHandle)
    {
      return this.SelectionStrategy.GetAllItemsSelected(groupRowHandle);
    }

    internal void SelectRowRecursively(int groupRowHandle)
    {
      this.SelectionStrategy.SelectRowRecursively(groupRowHandle);
    }

    internal void UnselectRowRecursively(int groupRowHandle)
    {
      this.SelectionStrategy.UnselectRowRecursively(groupRowHandle);
    }

    internal virtual bool IsExpandButton(IDataViewHitInfo hitInfo)
    {
      return false;
    }

    internal void SetSelectionAnchor()
    {
      if (this.RootView.FocusedView != this)
        return;
      this.GlobalSelectionAnchor = new SelectionAnchor(this, this.ViewBehavior.GetValueForSelectionAnchorRowHandle(this.FocusedRowHandle));
    }

    internal void SetSelectionAction(SelectionActionBase action)
    {
      this.GlobalSelectionAction = action;
    }

    internal void ExecuteSelectionAction()
    {
      if (this.RootView.FocusedView != this)
        return;
      if (this.GlobalSelectionAction != null)
        this.GlobalSelectionAction.Execute();
      this.GlobalSelectionAction = (SelectionActionBase) null;
    }

    private void OnActualGroupRowTemplateSelectorChanged()
    {
      this.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateClientGroupRowTemplateSelector()), true, true);
    }

    private void OnGroupRowStyleChanged()
    {
      this.ViewBehavior.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateClientGroupRowStyle()), true, true);
    }

    private void OnActualShowCheckBoxSelectorInGroupRowChanged()
    {
      this.ViewBehavior.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateClientCheckBoxSelector()), true, true);
    }

    private void OnGroupValueContentStyleChanged()
    {
      this.ValidateGroupContentStyle(this.GroupValueContentStyle, "GroupValueContentStyle is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx");
    }

    private void OnGroupSummaryContentStyleChanged()
    {
      this.ValidateGroupContentStyle(this.GroupSummaryContentStyle, "GroupSummaryContentStyle  is not supported in the grid's optimized mode. To learn more about the grid's optimized mode, see http://go.devexpress.com/xpf-optimized-mode.aspx");
    }

    private void ValidateGroupContentStyle(Style style, string errorMessage)
    {
      if (style != null && !(style is DefaultStyle) && (this.IsGroupRowOptimized && !DataViewBase.DisableOptimizedModeVerification) && !this.IsDesignTime)
        throw new InvalidOperationException(errorMessage);
    }

    protected override bool GetCanCreateRootNodeAsync()
    {
      return this.DataProviderBase.IsAsyncServerMode;
    }

    protected override void AddCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler)
    {
      this.createRootNodeCompletedHandler += eventHandler;
    }

    protected override void RemoveCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler)
    {
      this.createRootNodeCompletedHandler -= eventHandler;
    }

    protected override IVisualTreeWalker GetCustomVisualTreeWalker()
    {
      return (IVisualTreeWalker) null;
    }

    internal ItemsGenerationStrategyBase CreateItemsGenerationStrategy()
    {
      if (this.DataProviderBase.IsServerMode)
        return (ItemsGenerationStrategyBase) new ItemsGenerationServerModeStrategy((DataViewBase) this);
      if (this.DataProviderBase.IsAsyncServerMode)
        return (ItemsGenerationStrategyBase) new ItemsGenerationAsyncServerModeStrategy((DataViewBase) this);
      if (this.PrintSelectedRowsOnly)
        return (ItemsGenerationStrategyBase) new ItemsGenerationSelectedRowsStrategy((DataViewBase) this);
      if (this.PrintAllGroups)
        return (ItemsGenerationStrategyBase) new ItemsGenerationPrintAllGroupsStrategy((DataViewBase) this);
      return (ItemsGenerationStrategyBase) new ItemsGenerationSimpleStrategy((DataViewBase) this);
    }

    protected internal virtual void RaiseCreateRootNodeCompleted(IRootDataNode rootNode)
    {
      if (this.createRootNodeCompletedHandler == null)
        return;
      this.createRootNodeCompletedHandler((object) this, new ScalarOperationCompletedEventArgs<IRootDataNode>((object) rootNode, (Exception) null, false, (object) null));
    }
  }
}
