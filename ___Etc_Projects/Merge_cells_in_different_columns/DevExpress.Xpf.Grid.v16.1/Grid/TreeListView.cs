// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListView
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Utils.ServiceModel;
using DevExpress.Export;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI.Native;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Validation;
using DevExpress.Xpf.Grid.EditForm;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Grid.TreeList.Native;
using DevExpress.Xpf.GridData;
using DevExpress.Xpf.Printing.BrickCollection;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>A View that displays information in a Tree hierarchical structure.
  /// 
  /// </para>
  ///             </summary>
  public class TreeListView : GridDataViewBase, ITableView, IFormatsOwner
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormDialogServiceTemplateProperty = AssignableServiceHelper2<TreeListView, IDialogService>.RegisterServiceTemplateProperty("EditFormDialogServiceTemplate");
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormColumnCountProperty = DependencyProperty.Register("EditFormColumnCount", typeof (int), typeof (TreeListView), new PropertyMetadata((object) 3, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormPostModeProperty = DependencyProperty.Register("EditFormPostMode", typeof (EditFormPostMode), typeof (TreeListView), new PropertyMetadata((object) EditFormPostMode.Cached, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormShowModeProperty = DependencyProperty.Register("EditFormShowMode", typeof (EditFormShowMode), typeof (TreeListView), new PropertyMetadata((object) EditFormShowMode.None, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnF2KeyProperty = DependencyProperty.Register("ShowEditFormOnF2Key", typeof (bool), typeof (TreeListView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnEnterKeyProperty = DependencyProperty.Register("ShowEditFormOnEnterKey", typeof (bool), typeof (TreeListView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnDoubleClickProperty = DependencyProperty.Register("ShowEditFormOnDoubleClick", typeof (bool), typeof (TreeListView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormPostConfirmationProperty = DependencyProperty.Register("EditFormPostConfirmation", typeof (PostConfirmationMode), typeof (TreeListView), new PropertyMetadata((object) PostConfirmationMode.YesNoCancel, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormUpdateCancelButtonsProperty = DependencyProperty.Register("ShowEditFormUpdateCancelButtons", typeof (bool), typeof (TreeListView), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormTemplateProperty = DependencyProperty.Register("EditFormTemplate", typeof (DataTemplate), typeof (TreeListView), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).HideEditForm())));
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
    /// <returns> </returns>
    public static readonly DependencyProperty RowDetailsTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowDetailsTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualRowDetailsTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualRowDetailsTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowDetailsVisibilityModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ColumnBandChooserTemplateProperty;
    private static readonly DependencyPropertyKey FixedNoneContentWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedNoneContentWidthProperty;
    private static readonly DependencyPropertyKey TotalSummaryFixedNoneContentWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TotalSummaryFixedNoneContentWidthProperty;
    private static readonly DependencyPropertyKey VerticalScrollBarWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty VerticalScrollBarWidthProperty;
    private static readonly DependencyPropertyKey FixedLeftContentWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedLeftContentWidthProperty;
    private static readonly DependencyPropertyKey FixedRightContentWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedRightContentWidthProperty;
    private static readonly DependencyPropertyKey TotalGroupAreaIndentPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TotalGroupAreaIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExtendScrollBarToFixedColumnsProperty;
    private static readonly DependencyPropertyKey IndicatorHeaderWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IndicatorHeaderWidthProperty;
    private static readonly DependencyPropertyKey ActualDataRowTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualDataRowTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowAutoFilterRowProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowCascadeUpdateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowPerPixelScrollingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ScrollAnimationDurationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ScrollAnimationModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowScrollAnimationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty LeftDataAreaIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RightDataAreaIndentProperty;
    private static readonly DependencyPropertyKey FixedLeftVisibleColumnsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedLeftVisibleColumnsProperty;
    private static readonly DependencyPropertyKey FixedRightVisibleColumnsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedRightVisibleColumnsProperty;
    private static readonly DependencyPropertyKey FixedNoneVisibleColumnsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedNoneVisibleColumnsProperty;
    private static readonly DependencyPropertyKey HorizontalViewportPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty HorizontalViewportProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FixedLineWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowVerticalLinesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowHorizontalLinesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowDecorationTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DefaultDataRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DataRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DataRowTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowIndicatorContentTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowResizingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowHorizontalScrollingVirtualizationProperty;
    private static readonly DependencyPropertyKey ScrollingVirtualizationMarginPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ScrollingVirtualizationMarginProperty;
    private static readonly DependencyPropertyKey ScrollingHeaderVirtualizationMarginPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ScrollingHeaderVirtualizationMarginProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowMinHeightProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty HeaderPanelMinHeightProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoMoveRowFocusProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BestFitMaxRowCountProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BestFitModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BestFitAreaProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowBestFitProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CustomBestFitEvent;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowIndicatorProperty;
    private static readonly DependencyPropertyKey ActualShowIndicatorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualShowIndicatorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IndicatorWidthProperty;
    private static readonly DependencyPropertyKey ActualIndicatorWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualIndicatorWidthProperty;
    private static readonly DependencyPropertyKey ShowTotalSummaryIndicatorIndentPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowTotalSummaryIndicatorIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedRowBorderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty MultiSelectModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty UseIndicatorForSelectionProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowFixedColumnMenuProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowScrollHeadersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowBandsPanelProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowChangeColumnParentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowChangeBandParentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowBandsInCustomizationFormProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowBandMovingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowBandResizingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowAdvancedVerticalNavigationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowAdvancedHorizontalNavigationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ColumnChooserBandsSortOrderComparerProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BandHeaderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BandHeaderTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BandHeaderToolTipTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintBandHeaderStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowBandMultiRowProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty KeyFieldNameProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ParentFieldNameProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty CheckBoxFieldNameProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty CheckBoxValueConverterProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ImageFieldNameProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty NodeImageSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RootValueProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoPopulateServiceColumnsProperty;
    private static readonly DependencyPropertyKey VisibleColumnsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty VisibleColumnsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedNodeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowConditionalFormattingMenuProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowConditionalFormattingManagerProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedFormatsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedColorScaleFormatsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedDataBarFormatsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedIconSetFormatsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FormatConditionDialogServiceTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ConditionalFormattingManagerServiceTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent NodeExpandingEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent NodeExpandedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent NodeCollapsingEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent NodeCollapsedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent NodeCheckStateChangedEvent;
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
    public static readonly RoutedEvent CustomScrollAnimationEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent InvalidNodeExceptionEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent ValidateNodeEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent RowDoubleClickEvent;
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
    public static readonly RoutedEvent StartSortingEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent EndSortingEvent;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FocusedColumnProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowNodeImagesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty NodeImageSizeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ShowCheckboxesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty AllowIndeterminateCheckStateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty AllowRecursiveNodeCheckingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowExpandButtonsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowRootIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TreeLineStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowPresenterMarginProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoFilterRowCellStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FilterModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ChildNodesPathProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty TreeDerivationModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ChildNodesSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EnableDynamicLoadingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoExpandAllNodesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FetchSublevelChildrenOnExpandProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty VerticalScrollbarVisibilityProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty HorizontalScrollbarVisibilityProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintAutoWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintColumnHeadersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintBandHeadersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintColumnHeaderStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintAllNodesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintExpandButtonsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintNodeImagesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowDefaultContentForHierarchicalDataTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AlternateRowBackgroundProperty;
    protected static readonly DependencyPropertyKey ActualAlternateRowBackgroundPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualAlternateRowBackgroundProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EvenRowBackgroundProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty UseEvenRowBackgroundProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AlternationCountProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RestoreFocusOnExpandProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowChildNodeSourceUpdatesProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExpandStateFieldNameProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExpandCollapseNodesOnNavigationProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExpandNodesOnFilteringProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowDataNavigatorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowTreeIndentScrollingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionGeneratorTemplateProperty;
    [IgnoreDependencyPropertiesConsistencyChecker]
    private static readonly DependencyProperty FormatConditionsItemsAttachedBehaviorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionsSourceProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionGeneratorTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ScrollBarAnnotationModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ScrollBarAnnotationsAppearanceProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent ScrollBarAnnotationsCreatingEvent;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowCriteriaInAutoFilterRowProperty;
    protected Locker FocusedNodeSaveLocker;
    private readonly TreeListDataProvider treeListDataProvider;
    private TreeListUnboundColumnDataEventHandler customUnboundColumnData;
    private TreeListNodeChangedEventHandler nodeChanged;
    private TreeListNodeFilterEventHandler customNodeFilter;
    private CustomColumnFilterListEventHandler customFilterPopupList;
    private TreeListCustomColumnSortEventHandler customColumnSort;
    private TreeListCustomColumnDisplayTextEventHandler customColumnDisplayText;
    private TreeListCustomSummaryEventHandler customSummary;
    private System.Windows.Data.Binding expandStateBinding;
    private Lazy<BarManagerMenuController> bandMenuControllerValue;
    private TreeListCustomColumnDisplayTextEventArgs customColumnDisplayTextEventArgs;
    private bool actualAllowTreeIndentScrolling;
    private IClipboardManager<ColumnWrapper, TreeListNodeWrapper> clipboardManager;
    private ScrollBarAnnotationsManager _scrollBarAnnotationsManager;

    internal CsvExportHelper CsvHelper
    {
      get
      {
        return new CsvExportHelper((DataViewBase) this, ExportTarget.Csv, new Action<DataViewBase, Stream>(PrintHelper.ExportToCsv), new Action<DataViewBase, Stream, CsvExportOptions>(PrintHelper.ExportToCsv), new Action<DataViewBase, string>(PrintHelper.ExportToCsv), new Action<DataViewBase, string, CsvExportOptions>(PrintHelper.ExportToCsv));
      }
    }

    internal XlsExportHelper<XlsExportOptions> XlsHelper
    {
      get
      {
        return new XlsExportHelper<XlsExportOptions>((DataViewBase) this, ExportTarget.Xls, new Action<DataViewBase, Stream>(PrintHelper.ExportToXls), new Action<DataViewBase, Stream, XlsExportOptions>(PrintHelper.ExportToXls), new Action<DataViewBase, string>(PrintHelper.ExportToXls), new Action<DataViewBase, string, XlsExportOptions>(PrintHelper.ExportToXls));
      }
    }

    internal XlsExportHelper<XlsxExportOptions> XlsxHelper
    {
      get
      {
        return new XlsExportHelper<XlsxExportOptions>((DataViewBase) this, ExportTarget.Xlsx, new Action<DataViewBase, Stream>(PrintHelper.ExportToXlsx), new Action<DataViewBase, Stream, XlsxExportOptions>(PrintHelper.ExportToXlsx), new Action<DataViewBase, string>(PrintHelper.ExportToXlsx), new Action<DataViewBase, string, XlsxExportOptions>(PrintHelper.ExportToXlsx));
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the treelist's optimized mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><para>A <see cref="T:DevExpress.Xpf.Grid.UseLightweightTemplates" /> enumeration value.</para>
    /// </value>
    [Category("Options View")]
    public DevExpress.Xpf.Grid.UseLightweightTemplates? UseLightweightTemplates
    {
      get
      {
        return (DevExpress.Xpf.Grid.UseLightweightTemplates?) this.GetValue(TreeListView.UseLightweightTemplatesProperty);
      }
      set
      {
        this.SetValue(TreeListView.UseLightweightTemplatesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that is used to display the row details.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate RowDetailsTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.RowDetailsTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowDetailsTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a row details template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [Category("Appearance ")]
    public DataTemplateSelector RowDetailsTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.RowDetailsTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowDetailsTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a row details template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    public DataTemplateSelector ActualRowDetailsTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.ActualRowDetailsTemplateSelectorProperty);
      }
    }

    DependencyPropertyKey ITableView.ActualRowDetailsTemplateSelectorPropertyKey
    {
      get
      {
        return TreeListView.ActualRowDetailsTemplateSelectorPropertyKey;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that indicates when the row details are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowDetailsVisibilityMode" /> enumeration value.
    /// </value>
    public RowDetailsVisibilityMode RowDetailsVisibilityMode
    {
      get
      {
        return (RowDetailsVisibilityMode) this.GetValue(TreeListView.RowDetailsVisibilityModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowDetailsVisibilityModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies a data template which provides a service to display an   edit form popup dialog window. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate EditFormDialogServiceTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.EditFormDialogServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormDialogServiceTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the number of columns in the edit form. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A number of columns in the edit form. By default, <b>3</b>.
    /// </value>
    [Category("Editing")]
    public int EditFormColumnCount
    {
      get
      {
        return (int) this.GetValue(TreeListView.EditFormColumnCountProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormColumnCountProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether changes made to a row in the Inline Edit Form are immediately shown within the grid. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>An <see cref="T:DevExpress.Xpf.Grid.EditFormPostMode" /> enumeration value.
    /// </value>
    [Category("Editing")]
    public EditFormPostMode EditFormPostMode
    {
      get
      {
        return (EditFormPostMode) this.GetValue(TreeListView.EditFormPostModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormPostModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether and how the Inline Edit Form is displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An <see cref="T:DevExpress.Xpf.Grid.EditFormShowMode" /> enumeration value.
    /// </value>
    [Category("Editing")]
    public EditFormShowMode EditFormShowMode
    {
      get
      {
        return (EditFormShowMode) this.GetValue(TreeListView.EditFormShowModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormShowModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to show the Inline Edit Form on pressing the F2 key. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the Inline Edit Form on pressing the F2 key; otherwise, <b>false</b>.
    /// </value>
    [Category("Editing")]
    public bool ShowEditFormOnF2Key
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowEditFormOnF2KeyProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowEditFormOnF2KeyProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to show the Inline Edit Form on pressing the ENTER key. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the Inline Edit Form on pressing the ENTER key; otherwise, <b>false</b>.
    /// </value>
    [Category("Editing")]
    public bool ShowEditFormOnEnterKey
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowEditFormOnEnterKeyProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowEditFormOnEnterKeyProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to show the Inline Edit Form on double clicking a node. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the Inline Edit Form on double clicking a node; otherwise, <b>false</b>.
    /// </value>
    [Category("Editing")]
    public bool ShowEditFormOnDoubleClick
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowEditFormOnDoubleClickProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowEditFormOnDoubleClickProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the response on an end-user's attempt to move the focus from the Inline Edit Form while it contains unsaved changes. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.PostConfirmationMode" /> enumeration value.
    /// </value>
    [Category("Editing")]
    public PostConfirmationMode EditFormPostConfirmation
    {
      get
      {
        return (PostConfirmationMode) this.GetValue(TreeListView.EditFormPostConfirmationProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormPostConfirmationProperty, (object) value);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    [Category("Editing")]
    public BindingBase EditFormCaptionBinding { get; set; }

    /// <summary>
    ///                 <para>Specifies whether the Inline Edit Form should display the <b>Update</b> and <b>Cancel</b> buttons. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display Update and Cancel buttons; otherwise, <b>false</b>.
    /// </value>
    [Category("Editing")]
    public bool ShowEditFormUpdateCancelButtons
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowEditFormUpdateCancelButtonsProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowEditFormUpdateCancelButtonsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the Inline Edit Form. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of the inline edit form.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate EditFormTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.EditFormTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.EditFormTemplateProperty, (object) value);
      }
    }

    internal EditFormManager TreeListViewEditFormManager
    {
      get
      {
        return this.EditFormManager as EditFormManager;
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the horizontally scrollable viewport. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the width of the horizontally scrollable viewport, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedNoneContentWidth")]
    [CloneDetailMode(CloneDetailMode.Force)]
    public double FixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.FixedNoneContentWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedNoneContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This property supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewTotalSummaryFixedNoneContentWidth")]
    public double TotalSummaryFixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.TotalSummaryFixedNoneContentWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.TotalSummaryFixedNoneContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewVerticalScrollBarWidth")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double VerticalScrollBarWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.VerticalScrollBarWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.VerticalScrollBarWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the left fixed content width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that is the left fixed content width in pixels.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Force)]
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedLeftContentWidth")]
    public double FixedLeftContentWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.FixedLeftContentWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedLeftContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the right fixed content width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that is the right fixed content width in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedRightContentWidth")]
    [CloneDetailMode(CloneDetailMode.Force)]
    public double FixedRightContentWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.FixedRightContentWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedRightContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This property supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewTotalGroupAreaIndent")]
    [CloneDetailMode(CloneDetailMode.Force)]
    public double TotalGroupAreaIndent
    {
      get
      {
        return (double) this.GetValue(TreeListView.TotalGroupAreaIndentProperty);
      }
      private set
      {
        this.SetValue(TreeListView.TotalGroupAreaIndentPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the row indicator header. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the width of the row indicator header, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewIndicatorHeaderWidth")]
    public double IndicatorHeaderWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.IndicatorHeaderWidthProperty);
      }
      private set
      {
        this.SetValue(TreeListView.IndicatorHeaderWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a node template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewActualDataRowTemplateSelector")]
    public DataTemplateSelector ActualDataRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.ActualDataRowTemplateSelectorProperty);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a focused node's border. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that specifies the template that displays the border.
    /// 
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewFocusedRowBorderTemplate")]
    public ControlTemplate FocusedRowBorderTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TreeListView.FocusedRowBorderTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.FocusedRowBorderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the column/band chooser. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the template that displays the column/band chooser.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewColumnBandChooserTemplate")]
    [Category("Appearance ")]
    public ControlTemplate ColumnBandChooserTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TreeListView.ColumnBandChooserTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.ColumnBandChooserTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether multiple node/cell selection is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TableViewSelectMode" /> enumeration value that specifies the selection mode.
    /// </value>
    [Category("Options Selection")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [XtraSerializableProperty]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectionMode property instead")]
    public TableViewSelectMode MultiSelectMode
    {
      get
      {
        return (TableViewSelectMode) this.GetValue(TreeListView.MultiSelectModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.MultiSelectModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the TreeListView fetches child nodes of sub-level nodes when their parent node is being expanded. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to fetch child nodes of sub-level nodes when their parent node is being expanded; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool FetchSublevelChildrenOnExpand
    {
      get
      {
        return (bool) this.GetValue(TreeListView.FetchSublevelChildrenOnExpandProperty);
      }
      set
      {
        this.SetValue(TreeListView.FetchSublevelChildrenOnExpandProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether rows can be selected via the Row Indicator Panel. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if rows can be selected via the row indicator; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Selection")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewUseIndicatorForSelection")]
    public bool UseIndicatorForSelection
    {
      get
      {
        return (bool) this.GetValue(TreeListView.UseIndicatorForSelectionProperty);
      }
      set
      {
        this.SetValue(TreeListView.UseIndicatorForSelectionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether menu items used to fix a column to the left or right, are shown within a column's context menu. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show items used to fix a column to the left or right, within a column's context menu; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowFixedColumnMenu")]
    public bool AllowFixedColumnMenu
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowFixedColumnMenuProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowFixedColumnMenuProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column headers are automatically scrolled once a user drags a column header to the View's left or right. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow horizontal scrolling of column headers; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowScrollHeaders")]
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool AllowScrollHeaders
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowScrollHeadersProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowScrollHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display the bands panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the bands panel; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowBandsPanel")]
    public bool ShowBandsPanel
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowBandsPanelProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowBandsPanelProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to change a column's parent band. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow changing a column's parent band; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowChangeColumnParent")]
    [XtraSerializableProperty]
    [Category("Options Bands")]
    public bool AllowChangeColumnParent
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowChangeColumnParentProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowChangeColumnParentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to change the band's parent band. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow changing the parent band; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowChangeBandParent")]
    public bool AllowChangeBandParent
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowChangeBandParentProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowChangeBandParentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to show the 'Bands' tab within the Column Band Chooser.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the 'Bands' tab within the column chooser; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowBandsInCustomizationForm")]
    [XtraSerializableProperty]
    public bool ShowBandsInCustomizationForm
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowBandsInCustomizationFormProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowBandsInCustomizationFormProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to rearrange bands. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow rearranging bands; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowBandMoving")]
    [Category("Options Bands")]
    [XtraSerializableProperty]
    public bool AllowBandMoving
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowBandMovingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowBandMovingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can change band widths by dragging the edges of their headers.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow an end-user to change band widths; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowBandResizing")]
    [XtraSerializableProperty]
    public bool AllowBandResizing
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowBandResizingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowBandResizingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether advanced vertical navigation is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable advanced vertical navigation; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowAdvancedVerticalNavigation")]
    public bool AllowAdvancedVerticalNavigation
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowAdvancedVerticalNavigationProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowAdvancedVerticalNavigationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether advanced horizontal navigation is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable advanced horizontal navigation; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowAdvancedHorizontalNavigation")]
    public bool AllowAdvancedHorizontalNavigation
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowAdvancedHorizontalNavigationProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowAdvancedHorizontalNavigationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the object that compares columns and bands of the Grid to define their sorting within the Column Band Chooser. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An object supporting the <b>IComparer</b> interface.
    /// </value>
    [Browsable(false)]
    public IComparer<BandBase> ColumnChooserBandsSortOrderComparer
    {
      get
      {
        return (IComparer<BandBase>) this.GetValue(TreeListView.ColumnChooserBandsSortOrderComparerProperty);
      }
      set
      {
        this.SetValue(TreeListView.ColumnChooserBandsSortOrderComparerProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of band headers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of band headers.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewBandHeaderTemplate")]
    [XtraSerializableProperty]
    public DataTemplate BandHeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.BandHeaderTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.BandHeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a band header template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that applies a template based on custom logic.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewBandHeaderTemplateSelector")]
    [Category("Options Bands")]
    public DataTemplateSelector BandHeaderTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.BandHeaderTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.BandHeaderTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not the grid displays each data record on a single row when using bands.
    /// </para>
    ///             </summary>
    /// <value><b>false</b>, to make the grid display each data record on a single row when using bands; otherwise, <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowBandMultiRow")]
    [XtraSerializableProperty]
    [Category("Options Bands")]
    public bool AllowBandMultiRow
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowBandMultiRowProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowBandMultiRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of band header tooltips. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of band header tooltips.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewBandHeaderToolTipTemplate")]
    [Category("Options Bands")]
    public DataTemplate BandHeaderToolTipTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.BandHeaderToolTipTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.BandHeaderToolTipTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to band headers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to band headers when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintBandHeaderStyle")]
    public Style PrintBandHeaderStyle
    {
      get
      {
        return (Style) this.GetValue(TreeListView.PrintBandHeaderStyleProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintBandHeaderStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewScrollingVirtualizationMargin")]
    [Browsable(false)]
    public Thickness ScrollingVirtualizationMargin
    {
      get
      {
        return (Thickness) this.GetValue(TreeListView.ScrollingVirtualizationMarginProperty);
      }
      internal set
      {
        this.SetValue(TreeListView.ScrollingVirtualizationMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DevExpressXpfGridLocalizedDescription("TreeListViewScrollingHeaderVirtualizationMargin")]
    public Thickness ScrollingHeaderVirtualizationMargin
    {
      get
      {
        return (Thickness) this.GetValue(TreeListView.ScrollingHeaderVirtualizationMarginProperty);
      }
      internal set
      {
        this.SetValue(TreeListView.ScrollingHeaderVirtualizationMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to data nodes. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to data rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewRowStyle")]
    [Category("Appearance ")]
    public Style RowStyle
    {
      get
      {
        return (Style) this.GetValue(TreeListView.RowStyleProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the Automatic Filter Row is displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show the automatic filter row; otherwise, <b>false</b>. The default is <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowAutoFilterRow")]
    [Category("Options Filter")]
    [XtraSerializableProperty]
    public bool ShowAutoFilterRow
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowAutoFilterRowProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowAutoFilterRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para><para>Gets or sets whether or not to display the criteria selector buttons in the automatic filter row for all columns in the current view.</para>
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the criteria selector buttons in the automatic filter row; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Filter")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowCriteriaInAutoFilterRow")]
    public bool ShowCriteriaInAutoFilterRow
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowCriteriaInAutoFilterRowProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowCriteriaInAutoFilterRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not cascading data updates are allowed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow cascading data updates; otherwise, <b>false</b>. The default is <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowCascadeUpdate")]
    [XtraSerializableProperty]
    [Category("Options View")]
    public bool AllowCascadeUpdate
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowCascadeUpdateProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowCascadeUpdateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether per-pixel scrolling is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable per-pixel scrolling; <b>false</b> to enable row by row scrolling.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowPerPixelScrolling")]
    [XtraSerializableProperty]
    [Category("Options View")]
    public bool AllowPerPixelScrolling
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowPerPixelScrollingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowPerPixelScrollingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the scroll animation length. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the animation length, in milliseconds.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewScrollAnimationDuration")]
    [Category("Options View")]
    public double ScrollAnimationDuration
    {
      get
      {
        return (double) this.GetValue(TreeListView.ScrollAnimationDurationProperty);
      }
      set
      {
        this.SetValue(TreeListView.ScrollAnimationDurationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the per-pixel scrolling mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ScrollAnimationMode" /> enumeration value that specifies the per-pexel scrolling mode.
    /// </value>
    [Category("Options View")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewScrollAnimationMode")]
    public ScrollAnimationMode ScrollAnimationMode
    {
      get
      {
        return (ScrollAnimationMode) this.GetValue(TreeListView.ScrollAnimationModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.ScrollAnimationModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable scroll animation. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable scroll animation; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowScrollAnimation")]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool AllowScrollAnimation
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowScrollAnimationProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowScrollAnimationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether the horizontal scrollbar fills the entire width of the treelist. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the horizontal scrollbar should fill the entire width of the treelist; otherwise, <b>false</b>.
    /// 
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewExtendScrollBarToFixedColumns")]
    [XtraSerializableProperty]
    public bool ExtendScrollBarToFixedColumns
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ExtendScrollBarToFixedColumnsProperty);
      }
      set
      {
        this.SetValue(TreeListView.ExtendScrollBarToFixedColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets an object that is the Auto Filter Row's data.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowData" /> object that specifies the row's data.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAutoFilterRowData")]
    public RowData AutoFilterRowData
    {
      get
      {
        return ((TableViewBehavior) this.ViewBehavior).AutoFilterRowData;
      }
    }

    /// <summary>
    ///                 <para>Gets an object that is the New Item Row's data. This property is reserved for future use.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowData" /> object that is the row's data.
    /// 
    /// </value>
    [Browsable(false)]
    public RowData NewItemRowData { get; private set; }

    /// <summary>
    ///                 <para>Gets or sets a value indicating whether virtualization is enabled for horizontal scrolling. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable virtualization; <b>false</b> to disable it.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowHorizontalScrollingVirtualization")]
    public bool AllowHorizontalScrollingVirtualization
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowHorizontalScrollingVirtualizationProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowHorizontalScrollingVirtualizationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a node's minimum height. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies a row's minimum height.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewRowMinHeight")]
    public double RowMinHeight
    {
      get
      {
        return (double) this.GetValue(TreeListView.RowMinHeightProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowMinHeightProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the minimum height of the Column Header Panel. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that is the minimum height of the Column Header Panel.
    /// </value>
    [Category("Appearance ")]
    public double HeaderPanelMinHeight
    {
      get
      {
        return (double) this.GetValue(TreeListView.HeaderPanelMinHeightProperty);
      }
      set
      {
        this.SetValue(TreeListView.HeaderPanelMinHeightProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the node decoration template. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that is the node decoration template.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewRowDecorationTemplate")]
    [Category("Appearance ")]
    public ControlTemplate RowDecorationTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TreeListView.RowDecorationTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowDecorationTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the template that defines the default presentation of nodes. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the default presentation of nodes.
    /// </value>
    [Category("Appearance ")]
    [Browsable(false)]
    public DataTemplate DefaultDataRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.DefaultDataRowTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.DefaultDataRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of data rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of data rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewDataRowTemplate")]
    [Category("Appearance ")]
    public DataTemplate DataRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.DataRowTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.DataRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a node template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewDataRowTemplateSelector")]
    public DataTemplateSelector DataRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.DataRowTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.DataRowTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a row indicator's content. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of row indicators.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewRowIndicatorContentTemplate")]
    [Category("Appearance ")]
    public DataTemplate RowIndicatorContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.RowIndicatorContentTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowIndicatorContentTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value specifying whether horizontal navigation keys move focus to the next/previous node when the current node's last/first cell is focused. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if horizontal navigation keys can move focus between nodes; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewAutoMoveRowFocus")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool AutoMoveRowFocus
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AutoMoveRowFocusProperty);
      }
      set
      {
        this.SetValue(TreeListView.AutoMoveRowFocusProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the number of records taken into account when calculating the optimal widths required for columns to completely display their contents. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the number of records processed by a View to apply <b>best fit</b>.
    /// </value>
    [Category("BestFit")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewBestFitMaxRowCount")]
    public int BestFitMaxRowCount
    {
      get
      {
        return (int) this.GetValue(TreeListView.BestFitMaxRowCountProperty);
      }
      set
      {
        this.SetValue(TreeListView.BestFitMaxRowCountProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets how the optimal width required for a column to completely display its contents is calculated. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.BestFitMode" /> enumeration value.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewBestFitMode")]
    [Category("BestFit")]
    [XtraSerializableProperty]
    public BestFitMode BestFitMode
    {
      get
      {
        return (BestFitMode) this.GetValue(TreeListView.BestFitModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.BestFitModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets which interface elements are taken into account when calculating optimal width for columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.BestFitArea" /> enumeration value that specifies interface elements that are taken into account when calculating optimal width for columns.
    /// </value>
    [Category("BestFit")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewBestFitArea")]
    [XtraSerializableProperty]
    public BestFitArea BestFitArea
    {
      get
      {
        return (BestFitArea) this.GetValue(TreeListView.BestFitAreaProperty);
      }
      set
      {
        this.SetValue(TreeListView.BestFitAreaProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether it's allowed to calculate optimal widths, and apply them to all columns displayed within a View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow optimal widths to be calculated and applied to all columns displayed within a View; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [Category("BestFit")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowBestFit")]
    [XtraSerializableProperty]
    public bool AllowBestFit
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowBestFitProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowBestFitProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value specifying whether the Indicator panel is displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the node indicator panel; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowIndicator")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool ShowIndicator
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowIndicatorProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowIndicatorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the Row Indicator Panel is shown within a view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the Row Indicator Panel is shown within a view; otherwise, <b>false</b>.
    /// </value>
    public bool ActualShowIndicator
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ActualShowIndicatorProperty);
      }
      protected set
      {
        this.SetValue(TreeListView.ActualShowIndicatorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the width of the indicator panel. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value specifying the width of the indicator panel, in pixels.</value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewIndicatorWidth")]
    [XtraSerializableProperty]
    public double IndicatorWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.IndicatorWidthProperty);
      }
      set
      {
        this.SetValue(TreeListView.IndicatorWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public double ActualIndicatorWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.ActualIndicatorWidthProperty);
      }
      protected set
      {
        this.SetValue(TreeListView.ActualIndicatorWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value><b>0</b> always.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double ExpandDetailButtonWidth
    {
      get
      {
        return 0.0;
      }
    }

    double ITableView.ActualExpandDetailButtonWidth
    {
      get
      {
        return 0.0;
      }
    }

    Thickness ITableView.ActualDetailMargin
    {
      get
      {
        return FillControl.EmptyThickness;
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value><b>0</b> always.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double ActualExpandDetailHeaderWidth
    {
      get
      {
        return 0.0;
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool ShowTotalSummaryIndicatorIndent
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowTotalSummaryIndicatorIndentProperty);
      }
      protected set
      {
        this.SetValue(TreeListView.ShowTotalSummaryIndicatorIndentPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether vertical lines are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display vertical lines; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowVerticalLines")]
    [XtraSerializableProperty]
    [Category("Options View")]
    public bool ShowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowVerticalLinesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether horizontal lines are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display horizontal lines; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowHorizontalLines")]
    [XtraSerializableProperty]
    public bool ShowHorizontalLines
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowHorizontalLinesProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowHorizontalLinesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column widths are automatically changed so that the total columns' width matches the grid's width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the column auto width feature; otherwise, <b>false</b>. The default is <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAutoWidth")]
    public bool AutoWidth
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AutoWidthProperty);
      }
      set
      {
        this.SetValue(TreeListView.AutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can change column widths by dragging the edges of their headers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to change column widths; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAllowResizing")]
    public bool AllowResizing
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowResizingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowResizingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the fixed line's width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the fixed line's width.
    /// </value>
    [XtraSerializableProperty]
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedLineWidth")]
    public double FixedLineWidth
    {
      get
      {
        return (double) this.GetValue(TreeListView.FixedLineWidthProperty);
      }
      set
      {
        this.SetValue(TreeListView.FixedLineWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double LeftDataAreaIndent
    {
      get
      {
        return (double) this.GetValue(TreeListView.LeftDataAreaIndentProperty);
      }
      set
      {
        this.SetValue(TreeListView.LeftDataAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Thickness RowPresenterMargin
    {
      get
      {
        return (Thickness) this.GetValue(TreeListView.RowPresenterMarginProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowPresenterMarginProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double RightDataAreaIndent
    {
      get
      {
        return (double) this.GetValue(TreeListView.RightDataAreaIndentProperty);
      }
      set
      {
        this.SetValue(TreeListView.RightDataAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are fixed to the left. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns fixed to the left.</value>
    [Category("Options Layout")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedLeftVisibleColumns")]
    public IList<ColumnBase> FixedLeftVisibleColumns
    {
      get
      {
        return (IList<ColumnBase>) this.GetValue(TreeListView.FixedLeftVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedLeftVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are fixed to the right. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns fixed to the right.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedRightVisibleColumns")]
    [Category("Options Layout")]
    public IList<ColumnBase> FixedRightVisibleColumns
    {
      get
      {
        return (IList<ColumnBase>) this.GetValue(TreeListView.FixedRightVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedRightVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are not fixed within a View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns that are not fixed within a View.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewFixedNoneVisibleColumns")]
    [Category("Options Layout")]
    public IList<ColumnBase> FixedNoneVisibleColumns
    {
      get
      {
        return (IList<ColumnBase>) this.GetValue(TreeListView.FixedNoneVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TreeListView.FixedNoneVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the view's client area. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the client area's width.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewHorizontalViewport")]
    public double HorizontalViewport
    {
      get
      {
        return (double) this.GetValue(TreeListView.HorizontalViewportProperty);
      }
      private set
      {
        this.SetValue(TreeListView.HorizontalViewportPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to data cells displayed within the Auto Filter Row. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that specifies the style applied to data cells.
    /// 
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAutoFilterRowCellStyle")]
    public Style AutoFilterRowCellStyle
    {
      get
      {
        return (Style) this.GetValue(TreeListView.AutoFilterRowCellStyleProperty);
      }
      set
      {
        this.SetValue(TreeListView.AutoFilterRowCellStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of the 'children' field. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the 'children' field name.
    /// </value>
    [Category("Options Behavior")]
    [XtraResetProperty(ResetPropertyMode.None)]
    [XtraSerializableProperty]
    public string ChildNodesPath
    {
      get
      {
        return (string) this.GetValue(TreeListView.ChildNodesPathProperty);
      }
      set
      {
        this.SetValue(TreeListView.ChildNodesPathProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the tree derivation mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeDerivationMode" /> enumeration value that specifies the tree derivation mode.
    /// </value>
    [XtraSerializableProperty]
    [XtraResetProperty(ResetPropertyMode.None)]
    [Category("Options Behavior")]
    public TreeDerivationMode TreeDerivationMode
    {
      get
      {
        return (TreeDerivationMode) this.GetValue(TreeListView.TreeDerivationModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.TreeDerivationModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to create nodes dynamically on their parent node expansion. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to create nodes dynamically on their parent node expansion; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool EnableDynamicLoading
    {
      get
      {
        return (bool) this.GetValue(TreeListView.EnableDynamicLoadingProperty);
      }
      set
      {
        this.SetValue(TreeListView.EnableDynamicLoadingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a selector that returns the list of child nodes for the processed node. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The child nodes selector.</value>
    [Category("Options Behavior")]
    public IChildNodesSelector ChildNodesSelector
    {
      get
      {
        return (IChildNodesSelector) this.GetValue(TreeListView.ChildNodesSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.ChildNodesSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether all nodes are automatically expanded when the View is being loaded. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to expand all nodes when the View is being loaded; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool AutoExpandAllNodes
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AutoExpandAllNodesProperty);
      }
      set
      {
        this.SetValue(TreeListView.AutoExpandAllNodesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the binding that determines which nodes are expanded.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Data.Binding" /> object specifying which nodes are expanded.
    /// </value>
    [Category("Options Behavior")]
    public System.Windows.Data.Binding ExpandStateBinding
    {
      get
      {
        return this.expandStateBinding;
      }
      set
      {
        if (this.ExpandStateBinding == value)
          return;
        this.expandStateBinding = value;
        this.OnExpandStateBindingChanged();
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of the data field within the items source that determines which nodes are expanded.
    /// </para>
    ///             </summary>
    /// <value>A name of the data field that specifies which nodes are expanded.</value>
    [Category("Options Behavior")]
    public string ExpandStateFieldName
    {
      get
      {
        return (string) this.GetValue(TreeListView.ExpandStateFieldNameProperty);
      }
      set
      {
        this.SetValue(TreeListView.ExpandStateFieldNameProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable expanding/collapsing a focused node using cursor keys. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable expanding/collapsing a focused node using cursor keys; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    public bool? ExpandCollapseNodesOnNavigation
    {
      get
      {
        return (bool?) this.GetValue(TreeListView.ExpandCollapseNodesOnNavigationProperty);
      }
      set
      {
        this.SetValue(TreeListView.ExpandCollapseNodesOnNavigationProperty, (object) value);
      }
    }

    protected internal virtual bool ShouldExpandCollapseNodesOnNavigation
    {
      get
      {
        return this.ExpandCollapseNodesOnNavigation.GetValueOrDefault(this.NavigationStyle == GridViewNavigationStyle.Row);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to expand a node if its child nodes contain the search text. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to expand a node if its child nodes contain the search text; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    public bool ExpandNodesOnFiltering
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ExpandNodesOnFilteringProperty);
      }
      set
      {
        this.SetValue(TreeListView.ExpandNodesOnFilteringProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the view has details.
    /// </para>
    ///             </summary>
    /// <value><b>false</b> always.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool HasDetailViews
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value><b>false</b> always.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool ActualShowDetailButtons
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value><see cref="F:DevExpress.Xpf.Grid.ColumnPosition.Middle" /> always.
    /// </value>
    public ColumnPosition ExpandColumnPosition
    {
      get
      {
        return ColumnPosition.Middle;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the vertical scrollbar's visibility. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ScrollBarVisibility" /> enumeration value that specifies the vertical scrollbar's visibility.
    /// </value>
    public ScrollBarVisibility VerticalScrollbarVisibility
    {
      get
      {
        return (ScrollBarVisibility) this.GetValue(TreeListView.VerticalScrollbarVisibilityProperty);
      }
      set
      {
        this.SetValue(TreeListView.VerticalScrollbarVisibilityProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the horizontal scrollbar's visibility. This is a dependence property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ScrollBarVisibility" /> enumeration value that specifies the horizontal scrollbar's visibility.
    /// </value>
    public ScrollBarVisibility HorizontalScrollbarVisibility
    {
      get
      {
        return (ScrollBarVisibility) this.GetValue(TreeListView.HorizontalScrollbarVisibilityProperty);
      }
      set
      {
        this.SetValue(TreeListView.HorizontalScrollbarVisibilityProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the brush used to paint the background of alternate rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.Brush" /> value.
    /// </value>
    [Category("Appearance ")]
    public Brush AlternateRowBackground
    {
      get
      {
        return (Brush) this.GetValue(TreeListView.AlternateRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TreeListView.AlternateRowBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual brush that is used to the alternate row background. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.Brush" /> value.
    /// </value>
    public Brush ActualAlternateRowBackground
    {
      get
      {
        return (Brush) this.GetValue(TreeListView.ActualAlternateRowBackgroundProperty);
      }
      protected set
      {
        this.SetValue(TreeListView.ActualAlternateRowBackgroundPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Provides access to a theme-dependent brush that is used to alternate the row background. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.Brush" /> value.
    /// </value>
    [Category("Appearance ")]
    public Brush EvenRowBackground
    {
      get
      {
        return (Brush) this.GetValue(TreeListView.EvenRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TreeListView.EvenRowBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to alternate the row background using a theme-dependent brush. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to alternate the row background using a theme-dependent brush; otherwise, <b>false</b>. By default, <b>false</b>.
    /// 
    /// </value>
    [Category("Appearance ")]
    public bool UseEvenRowBackground
    {
      get
      {
        return (bool) this.GetValue(TreeListView.UseEvenRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TreeListView.UseEvenRowBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the alternate row frequency. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that is the alternate row frequency. By default, it's set to 2.</value>
    [Category("Appearance ")]
    public int AlternationCount
    {
      get
      {
        return (int) this.GetValue(TreeListView.AlternationCountProperty);
      }
      set
      {
        this.SetValue(TreeListView.AlternationCountProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the focused column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that is the focused column.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Obsolete("Use the DataControlBase.CurrentColumn property instead")]
    public ColumnBase FocusedColumn
    {
      get
      {
        return (ColumnBase) this.GetValue(TreeListView.FocusedColumnProperty);
      }
      set
      {
        this.SetValue(TreeListView.FocusedColumnProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of the service field in a data source that contains unique values. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the name of the data source key field.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewKeyFieldName")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public string KeyFieldName
    {
      get
      {
        return (string) this.GetValue(TreeListView.KeyFieldNameProperty);
      }
      set
      {
        this.SetValue(TreeListView.KeyFieldNameProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of the service field in a data source that contains parent node values. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that contains parent node values.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewParentFieldName")]
    [Category("Options Behavior")]
    public string ParentFieldName
    {
      get
      {
        return (string) this.GetValue(TreeListView.ParentFieldNameProperty);
      }
      set
      {
        this.SetValue(TreeListView.ParentFieldNameProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of a field in a data source to which check boxes embedded into nodes are bound.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the field in a data source.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCheckBoxFieldName")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public string CheckBoxFieldName
    {
      get
      {
        return (string) this.GetValue(TreeListView.CheckBoxFieldNameProperty);
      }
      set
      {
        this.SetValue(TreeListView.CheckBoxFieldNameProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the name of the field in a data source that contains node images. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the field name.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewImageFieldName")]
    public string ImageFieldName
    {
      get
      {
        return (string) this.GetValue(TreeListView.ImageFieldNameProperty);
      }
      set
      {
        this.SetValue(TreeListView.ImageFieldNameProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a converter used to provide the checkbox value. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>An object that implements the <see cref="T:System.Windows.Data.IValueConverter" /> interface.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewCheckBoxValueConverter")]
    public IValueConverter CheckBoxValueConverter
    {
      get
      {
        return (IValueConverter) this.GetValue(TreeListView.CheckBoxValueConverterProperty);
      }
      set
      {
        this.SetValue(TreeListView.CheckBoxValueConverterProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a selector that chooses a node image based on custom logic. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNodeImageSelector" /> descendant that chooses a node image based on custom logic.
    /// </value>
    [Category("Options View")]
    public TreeListNodeImageSelector NodeImageSelector
    {
      get
      {
        return (TreeListNodeImageSelector) this.GetValue(TreeListView.NodeImageSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.NodeImageSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the root value. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An object that specifies the root value.</value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewRootValue")]
    public object RootValue
    {
      get
      {
        return this.GetValue(TreeListView.RootValueProperty);
      }
      set
      {
        this.SetValue(TreeListView.RootValueProperty, value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not columns are automatically created for service fields in the underlying data source. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to automatically create columns for service fields in the underlying data source; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewAutoPopulateServiceColumns")]
    public bool AutoPopulateServiceColumns
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AutoPopulateServiceColumnsProperty);
      }
      set
      {
        this.SetValue(TreeListView.AutoPopulateServiceColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the node indent. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the node indent, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewRowIndent")]
    [Category("Options View")]
    public double RowIndent
    {
      get
      {
        return (double) this.GetValue(TreeListView.RowIndentProperty);
      }
      set
      {
        this.SetValue(TreeListView.RowIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display node images or not. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow display node images; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowNodeImages")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool ShowNodeImages
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowNodeImagesProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowNodeImagesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the node's image size. This is a dependency property
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Drawing.Size" /> object that represents the height and width of the node's image.
    /// </value>
    [Category("Options View")]
    public Size NodeImageSize
    {
      get
      {
        return (Size) this.GetValue(TreeListView.NodeImageSizeProperty);
      }
      set
      {
        this.SetValue(TreeListView.NodeImageSizeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not to display node check boxes.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display node check boxes; otherwise, <b>false</b>.
    /// </value>
    [Category("Options View")]
    public bool ShowCheckboxes
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowCheckboxesProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowCheckboxesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can set the node's check boxes to three states (checked, unchecked and indeterminate). This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow selecting the indeterminate state of a check box; otherwise, <b>false</b>.
    /// </value>
    [Category("Options View")]
    public bool AllowIndeterminateCheckState
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowIndeterminateCheckStateProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowIndeterminateCheckStateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether recursive node selection is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable recursive selection; otherwise, <b>false</b>.
    /// </value>
    [Category("Options View")]
    public bool AllowRecursiveNodeChecking
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowRecursiveNodeCheckingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowRecursiveNodeCheckingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not to show expand buttons. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show expand buttons; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowExpandButtons")]
    public bool ShowExpandButtons
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowExpandButtonsProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowExpandButtonsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not to show an indent for the root node.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show an indent for the root node; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewShowRootIndent")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool ShowRootIndent
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowRootIndentProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowRootIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style of tree lines used to connect nodes. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListLineStyle" /> enumeration value that specifies the style of tree lines.
    /// </value>
    [Category("Options View")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewTreeLineStyle")]
    public TreeListLineStyle TreeLineStyle
    {
      get
      {
        return (TreeListLineStyle) this.GetValue(TreeListView.TreeLineStyleProperty);
      }
      set
      {
        this.SetValue(TreeListView.TreeLineStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the list of visible columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewVisibleColumns")]
    [Category("Options Layout")]
    public IList<ColumnBase> VisibleColumns
    {
      get
      {
        return (IList<ColumnBase>) this.GetValue(TreeListView.VisibleColumnsProperty);
      }
      protected set
      {
        this.SetValue(TreeListView.VisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the focused node. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the focused node.
    /// </value>
    [Category("Options View")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public TreeListNode FocusedNode
    {
      get
      {
        return (TreeListNode) this.GetValue(TreeListView.FocusedNodeProperty);
      }
      set
      {
        this.SetValue(TreeListView.FocusedNodeProperty, (object) value);
      }
    }

    protected TreeListNode FocusedNodeSave { get; set; }

    /// <summary>
    ///                 <para>Gets or sets whether to use the default content if the <see cref="P:DevExpress.Xpf.Grid.TreeListView.DataRowTemplate" /> and <see cref="P:DevExpress.Xpf.Grid.TreeListView.DataRowTemplateSelector" /> properties return <b>null</b>.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to use the default content; otherwise, <b>false</b>.
    /// </value>
    public bool AllowDefaultContentForHierarchicalDataTemplate
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowDefaultContentForHierarchicalDataTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowDefaultContentForHierarchicalDataTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the way nodes are filtered.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListFilterMode" /> enumeration value that specifies the way nodes are filtered.
    /// </value>
    [Category("Options Filter")]
    public TreeListFilterMode FilterMode
    {
      get
      {
        return (TreeListFilterMode) this.GetValue(TreeListView.FilterModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.FilterModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether focus is restored on a child node after expanding its parent node. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to restore focus; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    public bool RestoreFocusOnExpand
    {
      get
      {
        return (bool) this.GetValue(TreeListView.RestoreFocusOnExpandProperty);
      }
      set
      {
        this.SetValue(TreeListView.RestoreFocusOnExpandProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the TreeList control tracks changes in the child collection. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to track child collection updates; otherwise, <b>false</b>.
    /// 
    /// </value>
    [Category("Options Behavior")]
    public bool AllowChildNodeSourceUpdates
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowChildNodeSourceUpdatesProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowChildNodeSourceUpdatesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to show the data navigator. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the data navigator; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewShowDataNavigator")]
    [XtraSerializableProperty]
    [Category("View")]
    public bool ShowDataNavigator
    {
      get
      {
        return (bool) this.GetValue(TreeListView.ShowDataNavigatorProperty);
      }
      set
      {
        this.SetValue(TreeListView.ShowDataNavigatorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to allow end-users to scroll expanded nodes horizontally together with expand buttons. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow end-users to scroll expanded nodes horizontally together with expand buttons; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool AllowTreeIndentScrolling
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowTreeIndentScrollingProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowTreeIndentScrollingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the type of annotations displayed within the view's scrollbar.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ScrollBarAnnotationMode" /> flag that is the type of scrollbar annotations applied to the <see cref="T:DevExpress.Xpf.Grid.TreeListView" />.
    /// 
    /// </value>
    [Category("Appearance ")]
    public DevExpress.Xpf.Grid.ScrollBarAnnotationMode? ScrollBarAnnotationMode
    {
      get
      {
        return (DevExpress.Xpf.Grid.ScrollBarAnnotationMode?) this.GetValue(TreeListView.ScrollBarAnnotationModeProperty);
      }
      set
      {
        this.SetValue(TreeListView.ScrollBarAnnotationModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the appearance settings for scrollbar annotation marks.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="ScrollBarAnnotationsAppearance" /> object that is the appearance of scrollbar annotation marks.
    /// </value>
    [Category("Appearance ")]
    public ScrollBarAnnotationsAppearance ScrollBarAnnotationsAppearance
    {
      get
      {
        return (ScrollBarAnnotationsAppearance) this.GetValue(TreeListView.ScrollBarAnnotationsAppearanceProperty);
      }
      set
      {
        this.SetValue(TreeListView.ScrollBarAnnotationsAppearanceProperty, (object) value);
      }
    }

    DevExpress.Xpf.Grid.ScrollBarAnnotationMode ITableView.ScrollBarAnnotationModeActual
    {
      get
      {
        return this.ScrollBarAnnotationMode ?? DevExpress.Xpf.Grid.ScrollBarAnnotationMode.None;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of data rows when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of data rows when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintRowTemplate")]
    public DataTemplate PrintRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.PrintRowTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether printed columns' widths are automatically changed, so that the grid fits the width of the report page.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to automatically change the grid's width so that it fits the width of the report page; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintAutoWidth")]
    [Category("Options Print")]
    [XtraSerializableProperty]
    public bool PrintAutoWidth
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintAutoWidthProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintAutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column headers are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print column headers; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintColumnHeaders")]
    [Category("Options Print")]
    [XtraSerializableProperty]
    public bool PrintColumnHeaders
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintColumnHeadersProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintColumnHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether band headers are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to print band headers; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintBandHeaders")]
    [XtraSerializableProperty]
    [Category("Options Print")]
    public bool PrintBandHeaders
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintBandHeadersProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintBandHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the grid is printed with all nodes expanded.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print the grid with all nodes expanded; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [Category("Options Print")]
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintAllNodes")]
    [XtraSerializableProperty]
    public bool PrintAllNodes
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintAllNodesProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintAllNodesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether expand buttons are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print expand buttons; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintExpandButtons")]
    [Category("Appearance Print")]
    [XtraSerializableProperty]
    public bool PrintExpandButtons
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintExpandButtonsProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintExpandButtonsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the node images are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print the node images; otherwise, <b>false</b>. The default is <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintNodeImages")]
    [Category("Appearance Print")]
    public bool PrintNodeImages
    {
      get
      {
        return (bool) this.GetValue(TreeListView.PrintNodeImagesProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintNodeImagesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to column headers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to column headers when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewPrintColumnHeaderStyle")]
    [Category("Appearance Print")]
    public Style PrintColumnHeaderStyle
    {
      get
      {
        return (Style) this.GetValue(TreeListView.PrintColumnHeaderStyleProperty);
      }
      set
      {
        this.SetValue(TreeListView.PrintColumnHeaderStyleProperty, (object) value);
      }
    }

    internal bool IsCustomNodeFilterAssigned
    {
      get
      {
        return this.customNodeFilter != null;
      }
    }

    internal TreeListDataProvider TreeListDataProvider
    {
      get
      {
        return this.treeListDataProvider;
      }
    }

    /// <summary>
    ///                 <para>Gets the collection of root nodes.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNodeCollection" /> object that contains root nodes.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewNodes")]
    [Category("Layout")]
    public TreeListNodeCollection Nodes
    {
      get
      {
        return this.treeListDataProvider.Nodes;
      }
    }

    protected internal override DataProviderBase DataProviderBase
    {
      get
      {
        return (DataProviderBase) this.treeListDataProvider;
      }
    }

    protected internal override bool NeedCellsWidthUpdateOnScrolling
    {
      get
      {
        return true;
      }
    }

    internal BarManagerMenuController BandMenuController
    {
      get
      {
        return this.bandMenuControllerValue.Value;
      }
    }

    /// <summary>
    ///                 <para>Allows you to customize the band context menu by adding new menu items or removing existing items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Bars.BarManagerActionCollection" /> object.
    /// </value>
    [Browsable(false)]
    public BarManagerActionCollection BandMenuCustomizations
    {
      get
      {
        return this.BandMenuController.ActionContainer.Actions;
      }
    }

    /// <summary>
    ///                 <para>Gets the total number of nodes contained within the view.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the total number of nodes.</value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewTotalNodesCount")]
    public int TotalNodesCount
    {
      get
      {
        return this.TreeListDataProvider.TotalNodesCount;
      }
    }

    /// <summary>
    ///                 <para>Provides access to view commands.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListViewCommands" /> object that provides a set of view commands.
    /// </value>
    public TreeListViewCommands TreeListCommands
    {
      get
      {
        return this.Commands as TreeListViewCommands;
      }
    }

    internal override int GroupCount
    {
      get
      {
        return 0;
      }
    }

    internal override bool IsRowMarginControlVisible
    {
      get
      {
        return true;
      }
    }

    protected internal TreeListViewBehavior TreeListViewBehavior
    {
      get
      {
        return (TreeListViewBehavior) this.ViewBehavior;
      }
    }

    protected internal int ServiceIndentsCount
    {
      get
      {
        return (this.ShowRootIndent ? 1 : 0) + (this.ShowNodeImages ? 1 : 0) + (this.ShowCheckboxes ? 1 : 0);
      }
    }

    protected internal TreeListRowsClipboardController ClipboardController
    {
      get
      {
        return base.ClipboardController as TreeListRowsClipboardController;
      }
    }

    internal IDispalyMemberBindingClient DisplayMemberBindingClient
    {
      get
      {
        return (IDispalyMemberBindingClient) this.DataControl;
      }
    }

    protected override bool ShouldChangeRowByTab
    {
      get
      {
        return false;
      }
    }

    protected override int FixedNoneColumnsCount
    {
      get
      {
        return this.FixedNoneVisibleColumns.Count;
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <value> </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public bool AutoDetectColumnTypeInHierarchicalMode { get; set; }

    internal bool HasCustomSummary
    {
      get
      {
        return this.customSummary != null;
      }
    }

    internal bool ActualAllowTreeIndentScrolling
    {
      get
      {
        return this.actualAllowTreeIndentScrolling;
      }
      private set
      {
        if (this.actualAllowTreeIndentScrolling == value)
          return;
        this.actualAllowTreeIndentScrolling = value;
        this.ViewBehavior.UpdateViewRowData((UpdateRowDataDelegate) (x => x.UpdateClientIndentScrolling()));
      }
    }

    double ITableView.LeftGroupAreaIndent
    {
      get
      {
        return this.RowIndent;
      }
    }

    double ITableView.RightGroupAreaIndent
    {
      get
      {
        return 0.0;
      }
    }

    TableViewBehavior ITableView.TableViewBehavior
    {
      get
      {
        return (TableViewBehavior) this.TreeListViewBehavior;
      }
    }

    double ITableView.FixedNoneContentWidth
    {
      get
      {
        return this.FixedNoneContentWidth;
      }
      set
      {
        this.FixedNoneContentWidth = value;
      }
    }

    double ITableView.TotalSummaryFixedNoneContentWidth
    {
      get
      {
        return this.TotalSummaryFixedNoneContentWidth;
      }
      set
      {
        this.TotalSummaryFixedNoneContentWidth = value;
      }
    }

    double ITableView.VerticalScrollBarWidth
    {
      get
      {
        return this.VerticalScrollBarWidth;
      }
      set
      {
        this.VerticalScrollBarWidth = value;
      }
    }

    double ITableView.FixedLeftContentWidth
    {
      get
      {
        return this.FixedLeftContentWidth;
      }
      set
      {
        this.FixedLeftContentWidth = value;
      }
    }

    double ITableView.FixedRightContentWidth
    {
      get
      {
        return this.FixedRightContentWidth;
      }
      set
      {
        this.FixedRightContentWidth = value;
      }
    }

    double ITableView.TotalGroupAreaIndent
    {
      get
      {
        return this.TotalGroupAreaIndent;
      }
      set
      {
        this.TotalGroupAreaIndent = value;
      }
    }

    double ITableView.IndicatorHeaderWidth
    {
      get
      {
        return this.IndicatorHeaderWidth;
      }
      set
      {
        this.IndicatorHeaderWidth = value;
      }
    }

    Thickness ITableView.ScrollingVirtualizationMargin
    {
      get
      {
        return this.ScrollingVirtualizationMargin;
      }
      set
      {
        this.ScrollingVirtualizationMargin = value;
      }
    }

    Thickness ITableView.ScrollingHeaderVirtualizationMargin
    {
      get
      {
        return this.ScrollingHeaderVirtualizationMargin;
      }
      set
      {
        this.ScrollingHeaderVirtualizationMargin = value;
      }
    }

    DependencyPropertyKey ITableView.ActualDataRowTemplateSelectorPropertyKey
    {
      get
      {
        return TreeListView.ActualDataRowTemplateSelectorPropertyKey;
      }
    }

    bool ITableView.IsCheckBoxSelectorColumnVisible
    {
      get
      {
        return false;
      }
    }

    bool ITableView.IsEditing
    {
      get
      {
        return this.IsEditing;
      }
    }

    DataViewBase ITableView.ViewBase
    {
      get
      {
        return (DataViewBase) this;
      }
    }

    bool ITableView.ActualAllowTreeIndentScrolling
    {
      get
      {
        return this.ActualAllowTreeIndentScrolling;
      }
    }

    IList<ColumnBase> ITableView.ViewportVisibleColumns { get; set; }

    bool ITableView.HasCustomCellAppearance
    {
      get
      {
        return this.CustomCellAppearance != null;
      }
    }

    bool ITableView.HasCustomRowAppearance
    {
      get
      {
        return this.CustomRowAppearance != null;
      }
    }

    bool ITableView.NewItemRowIsDisplayed
    {
      get
      {
        return false;
      }
    }

    /// <summary>
    ///                 <para>Contains format conditions.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.FormatConditionCollection" /> object that is the collection of format conditions applied to the view's columns.
    /// </value>
    [XtraResetProperty]
    [GridUIProperty]
    [Category("Appearance ")]
    [XtraSerializableProperty(true, false, false)]
    public FormatConditionCollection FormatConditions
    {
      get
      {
        return this.TreeListViewBehavior.FormatConditions;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the conditional formatting menu. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable the conditional formatting menu; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Conditional Formatting")]
    public bool AllowConditionalFormattingMenu
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowConditionalFormattingMenuProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowConditionalFormattingMenuProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the conditional formatting rules manager. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable the conditional formatting manager; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Conditional Formatting")]
    public bool AllowConditionalFormattingManager
    {
      get
      {
        return (bool) this.GetValue(TreeListView.AllowConditionalFormattingManagerProperty);
      }
      set
      {
        this.SetValue(TreeListView.AllowConditionalFormattingManagerProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Contains predefined formats that are used by conditional formatting rules. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.ConditionalFormatting.FormatInfoCollection" /> object that is the collection of formats.
    /// </value>
    [Category("Conditional Formatting")]
    public FormatInfoCollection PredefinedFormats
    {
      get
      {
        return (FormatInfoCollection) this.GetValue(TreeListView.PredefinedFormatsProperty);
      }
      set
      {
        this.SetValue(TreeListView.PredefinedFormatsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Contains predefined color scales formats that are used to color cells proportional to their values. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.ConditionalFormatting.FormatInfoCollection" /> object that is the collection of color scales formats.
    /// </value>
    [Category("Conditional Formatting")]
    public FormatInfoCollection PredefinedColorScaleFormats
    {
      get
      {
        return (FormatInfoCollection) this.GetValue(TreeListView.PredefinedColorScaleFormatsProperty);
      }
      set
      {
        this.SetValue(TreeListView.PredefinedColorScaleFormatsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Contains predefined data bar formats that are used to fill cells with bars whose lengths are proportional to the cell values. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.ConditionalFormatting.FormatInfoCollection" /> object that is the collection of data bar formats.
    /// </value>
    [Category("Conditional Formatting")]
    public FormatInfoCollection PredefinedDataBarFormats
    {
      get
      {
        return (FormatInfoCollection) this.GetValue(TreeListView.PredefinedDataBarFormatsProperty);
      }
      set
      {
        this.SetValue(TreeListView.PredefinedDataBarFormatsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Contains predefined icon set formats that are used to assign an icon to each cell based on its value. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.ConditionalFormatting.FormatInfoCollection" /> object that is the collection of icon set formats.
    /// </value>
    [Category("Conditional Formatting")]
    public FormatInfoCollection PredefinedIconSetFormats
    {
      get
      {
        return (FormatInfoCollection) this.GetValue(TreeListView.PredefinedIconSetFormatsProperty);
      }
      set
      {
        this.SetValue(TreeListView.PredefinedIconSetFormatsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the format condition dialog. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of cards.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate FormatConditionDialogServiceTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.FormatConditionDialogServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.FormatConditionDialogServiceTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the conditional formatting manager. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of the conditional formatting manager.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate ConditionalFormattingManagerServiceTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.ConditionalFormattingManagerServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.ConditionalFormattingManagerServiceTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a template that describes format conditions. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A format condition template.</value>
    public DataTemplate FormatConditionGeneratorTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TreeListView.FormatConditionGeneratorTemplateProperty);
      }
      set
      {
        this.SetValue(TreeListView.FormatConditionGeneratorTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or set a list of format conditions applied to the treelist view.
    /// </para>
    ///             </summary>
    /// <value>The source from which the grid generates format conditions.</value>
    public IEnumerable FormatConditionsSource
    {
      get
      {
        return (IEnumerable) this.GetValue(TreeListView.FormatConditionsSourceProperty);
      }
      set
      {
        this.SetValue(TreeListView.FormatConditionsSourceProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the data template selector which chooses a template based on the format condition type. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The column template selector.</value>
    public DataTemplateSelector FormatConditionGeneratorTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TreeListView.FormatConditionGeneratorTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TreeListView.FormatConditionGeneratorTemplateSelectorProperty, (object) value);
      }
    }

    internal TreeListViewClipboardHelper ClipboardHelperManager { get; private set; }

    private IClipboardManager<ColumnWrapper, TreeListNodeWrapper> ClipboardManager
    {
      get
      {
        if (this.clipboardManager == null)
          this.clipboardManager = this.CreateClipboardManager();
        return this.clipboardManager;
      }
    }

    ScrollBarAnnotationsManager ITableView.ScrollBarAnnotationsManager
    {
      get
      {
        if (this._scrollBarAnnotationsManager == null)
          this._scrollBarAnnotationsManager = new ScrollBarAnnotationsManager((ITableView) this);
        return this._scrollBarAnnotationsManager;
      }
    }

    /// <summary>
    ///                 <para>Gets a list of objects that correspond to currently displayed scrollbar annotations.
    /// </para>
    ///             </summary>
    /// <value>A list of <see cref="T:DevExpress.Xpf.Grid.ScrollBarAnnotationRowInfo" /> objects that correspond to currently displayed scrollbar annotations.
    /// </value>
    public IEnumerable<ScrollBarAnnotationRowInfo> ScrollBarAnnotationInfoRange
    {
      get
      {
        return ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationInfoRange;
      }
    }

    bool ITableView.NeedCustomScrollBarAnnotation
    {
      get
      {
        return this.ScrollBarCustomRowAnnotation != null;
      }
    }

    /// <summary>
    ///                 <para>Enables you to manually calculate the optimal width for a column(s).
    /// </para>
    ///             </summary>
    [Category("BestFit")]
    public event TreeListCustomBestFitEventHandler CustomBestFit
    {
      add
      {
        this.AddHandler(TreeListView.CustomBestFitEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.CustomBestFitEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Allows creating a set of predefined scrollbar annotations.
    /// 
    /// </para>
    ///             </summary>
    public event ScrollBarAnnotationsCreatingEventHandler ScrollBarAnnotationsCreating
    {
      add
      {
        this.AddHandler(TreeListView.ScrollBarAnnotationsCreatingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.ScrollBarAnnotationsCreatingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Allows creating a new scrollbar annotation based on data row values and a row handle.
    /// 
    /// </para>
    ///             </summary>
    [Category("Appearance ")]
    public event EventHandler<ScrollBarCustomRowAnnotationEventArgs> ScrollBarCustomRowAnnotation;

    /// <summary>
    ///                 <para>Occurs before a node is expanded.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeAllowEventHandler NodeExpanding
    {
      add
      {
        this.AddHandler(TreeListView.NodeExpandingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.NodeExpandingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a node has been expanded.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeEventHandler NodeExpanded
    {
      add
      {
        this.AddHandler(TreeListView.NodeExpandedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.NodeExpandedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs before a node is collapsed and allowing the action to be canceled.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeAllowEventHandler NodeCollapsing
    {
      add
      {
        this.AddHandler(TreeListView.NodeCollapsingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.NodeCollapsingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a node has been collapsed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeEventHandler NodeCollapsed
    {
      add
      {
        this.AddHandler(TreeListView.NodeCollapsedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.NodeCollapsedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs each time a node's checkbox has changed its value.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeEventHandler NodeCheckStateChanged
    {
      add
      {
        this.AddHandler(TreeListView.NodeCheckStateChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.NodeCheckStateChangedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after the node's property has changed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeChangedEventHandler NodeChanged
    {
      add
      {
        this.nodeChanged += value;
      }
      remove
      {
        this.nodeChanged -= value;
      }
    }

    /// <summary>
    ///                 <para>Fires when a node fails validation, or when it cannot be saved to a data source.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListInvalidNodeExceptionEventHandler InvalidNodeException
    {
      add
      {
        this.AddHandler(TreeListView.InvalidNodeExceptionEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.InvalidNodeExceptionEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify whether the focused node's data is valid, and whether the node can lose focus.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeValidationEventHandler ValidateNode
    {
      add
      {
        this.AddHandler(TreeListView.ValidateNodeEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.ValidateNodeEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify whether the focused cell's data is valid, and whether the cell's editor can be closed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListCellValidationEventHandler ValidateCell;

    /// <summary>
    ///                 <para>Enables data to be supplied to unbound columns.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListUnboundColumnDataEventHandler CustomUnboundColumnData
    {
      add
      {
        this.customUnboundColumnData += value;
      }
      remove
      {
        this.customUnboundColumnData -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to filter nodes using custom rules.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListNodeFilterEventHandler CustomNodeFilter
    {
      add
      {
        this.customNodeFilter += value;
      }
      remove
      {
        this.customNodeFilter -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to sort data using custom rules.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListCustomColumnSortEventHandler CustomColumnSort
    {
      add
      {
        this.customColumnSort += value;
      }
      remove
      {
        this.customColumnSort -= value;
      }
    }

    /// <summary>
    ///                 <para>Occurs before a sorting operation is started.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler StartSorting
    {
      add
      {
        this.AddHandler(TreeListView.StartSortingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.StartSortingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a sorting operation has been completed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler EndSorting
    {
      add
      {
        this.AddHandler(TreeListView.EndSortingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.EndSortingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables custom display text to be provided for any data cell.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListCustomColumnDisplayTextEventHandler CustomColumnDisplayText
    {
      add
      {
        this.customColumnDisplayText += value;
      }
      remove
      {
        this.customColumnDisplayText -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to calculate summary values manually.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event TreeListCustomSummaryEventHandler CustomSummary
    {
      add
      {
        this.customSummary += value;
      }
      remove
      {
        this.customSummary -= value;
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    [Category("Events")]
    public event CustomColumnFilterListEventHandler CustomFilterPopupList
    {
      add
      {
        this.customFilterPopupList += value;
      }
      remove
      {
        this.customFilterPopupList -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to filter unique values displayed within a column's Filter Dropdown.
    /// </para>
    ///             </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public event CustomColumnFilterListEventHandler CustomFiterPopupList
    {
      add
      {
        this.customFilterPopupList += value;
      }
      remove
      {
        this.customFilterPopupList -= value;
      }
    }

    /// <summary>
    ///                 <para>Occurs after the focused cell's editor has been shown.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event TreeListEditorEventHandler ShownEditor
    {
      add
      {
        this.AddHandler(TreeListView.ShownEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.ShownEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to prevent an end-user from activating editors of individual cells.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event TreeListShowingEditorEventHandler ShowingEditor
    {
      add
      {
        this.AddHandler(TreeListView.ShowingEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.ShowingEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a cell's editor has been closed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event TreeListEditorEventHandler HiddenEditor
    {
      add
      {
        this.AddHandler(TreeListView.HiddenEditorEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.HiddenEditorEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a cell's value has been changed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event TreeListCellValueChangedEventHandler CellValueChanged
    {
      add
      {
        this.AddHandler(TreeListView.CellValueChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.CellValueChangedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to provide custom animation played when grid data is vertically scrolled (per-pixel).
    /// </para>
    ///             </summary>
    [Category("Options View")]
    public event CustomScrollAnimationEventHandler CustomScrollAnimation
    {
      add
      {
        this.AddHandler(TreeListView.CustomScrollAnimationEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.CustomScrollAnimationEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires in response to changing the edit value.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event TreeListCellValueChangedEventHandler CellValueChanging
    {
      add
      {
        this.AddHandler(TreeListView.CellValueChangingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.CellValueChangingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when a node is double-clicked.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowDoubleClickEventHandler RowDoubleClick
    {
      add
      {
        this.AddHandler(TreeListView.RowDoubleClickEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.RowDoubleClickEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after treelist's selection has been changed.
    /// </para>
    ///             </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the TreeListControl.SelectionChanged event instead")]
    public event TreeListSelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.AddHandler(TreeListView.SelectionChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.SelectionChangedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when data is copied to the clipboard, allowing you to manually copy required data.
    /// </para>
    ///             </summary>
    [Browsable(false)]
    [Category("Behavior")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the TreeList.CopyingToClipboard event instead")]
    public event TreeListCopyingToClipboardEventHandler CopyingToClipboard
    {
      add
      {
        this.AddHandler(TreeListView.CopyingToClipboardEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TreeListView.CopyingToClipboardEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    [Category("Events")]
    public event EventHandler<EditFormShowingEventArgs> EditFormShowing;

    /// <summary>
    ///                 <para>Allows overriding the focused cell background with the background color defined in the conditional formatting rules.
    /// 
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event EventHandler<CustomCellAppearanceEventArgs> CustomCellAppearance;

    /// <summary>
    ///                 <para>Allows overriding the focused row background with the background color defined in the conditional formatting rules.
    /// 
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event EventHandler<CustomRowAppearanceEventArgs> CustomRowAppearance;

    static TreeListView()
    {
      Type type = typeof (TreeListView);
      TreeListViewSelectionControlWrapper.Register();
      TreeListView.ColumnBandChooserTemplateProperty = TableViewBehavior.RegisterColumnBandChooserTemplateProperty(type);
      TreeListView.FocusedColumnProperty = DependencyPropertyManager.Register("FocusedColumn", typeof (ColumnBase), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnFocusedColumnChanged), (CoerceValueCallback) ((d, e) => (object) ((DataViewBase) d).CoerceFocusedColumn((ColumnBase) e))));
      TreeListView.VisibleColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("VisibleColumns", typeof (IList<ColumnBase>), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TreeListView.VisibleColumnsProperty = TreeListView.VisibleColumnsPropertyKey.DependencyProperty;
      TreeListView.FixedNoneContentWidthPropertyKey = TableViewBehavior.RegisterFixedNoneContentWidthProperty(type);
      TreeListView.FixedNoneContentWidthProperty = TreeListView.FixedNoneContentWidthPropertyKey.DependencyProperty;
      TreeListView.TotalSummaryFixedNoneContentWidthPropertyKey = TableViewBehavior.RegisterTotalSummaryFixedNoneContentWidthProperty(type);
      TreeListView.TotalSummaryFixedNoneContentWidthProperty = TreeListView.TotalSummaryFixedNoneContentWidthPropertyKey.DependencyProperty;
      TreeListView.VerticalScrollBarWidthPropertyKey = TableViewBehavior.RegisterVerticalScrollBarWidthProperty(type);
      TreeListView.VerticalScrollBarWidthProperty = TreeListView.VerticalScrollBarWidthPropertyKey.DependencyProperty;
      TreeListView.FixedLeftContentWidthPropertyKey = TableViewBehavior.RegisterFixedLeftContentWidthProperty(type);
      TreeListView.FixedLeftContentWidthProperty = TreeListView.FixedLeftContentWidthPropertyKey.DependencyProperty;
      TreeListView.FixedRightContentWidthPropertyKey = TableViewBehavior.RegisterFixedRightContentWidthProperty(type);
      TreeListView.FixedRightContentWidthProperty = TreeListView.FixedRightContentWidthPropertyKey.DependencyProperty;
      TreeListView.TotalGroupAreaIndentPropertyKey = TableViewBehavior.RegisterTotalGroupAreaIndentProperty(type);
      TreeListView.TotalGroupAreaIndentProperty = TreeListView.TotalGroupAreaIndentPropertyKey.DependencyProperty;
      TreeListView.IndicatorHeaderWidthPropertyKey = TableViewBehavior.RegisterIndicatorHeaderWidthProperty(type);
      TreeListView.IndicatorHeaderWidthProperty = TreeListView.IndicatorHeaderWidthPropertyKey.DependencyProperty;
      TreeListView.ActualDataRowTemplateSelectorPropertyKey = TableViewBehavior.RegisterActualDataRowTemplateSelectorProperty(type);
      TreeListView.ActualDataRowTemplateSelectorProperty = TreeListView.ActualDataRowTemplateSelectorPropertyKey.DependencyProperty;
      TreeListView.BestFitMaxRowCountProperty = DependencyPropertyManager.Register("BestFitMaxRowCount", typeof (int), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) -1, (PropertyChangedCallback) null, (CoerceValueCallback) ((d, baseValue) => (object) DataViewBase.CoerceBestFitMaxRowCount(Convert.ToInt32(baseValue)))));
      TreeListView.BestFitModeProperty = DependencyPropertyManager.Register("BestFitMode", typeof (BestFitMode), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) BestFitMode.Default));
      TreeListView.BestFitAreaProperty = DependencyPropertyManager.Register("BestFitArea", typeof (BestFitArea), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) BestFitArea.All));
      TreeListView.CustomBestFitEvent = EventManager.RegisterRoutedEvent("CustomBestFit", RoutingStrategy.Direct, typeof (TreeListCustomBestFitEventHandler), type);
      TreeListView.FocusedRowBorderTemplateProperty = TableViewBehavior.RegisterFocusedRowBorderTemplateProperty(type);
      TreeListView.AutoWidthProperty = TableViewBehavior.RegisterAutoWidthProperty(type);
      TreeListView.LeftDataAreaIndentProperty = TableViewBehavior.RegisterLeftDataAreaIndentProperty(type);
      TreeListView.RightDataAreaIndentProperty = TableViewBehavior.RegisterRightDataAreaIndentProperty(type);
      TreeListView.ShowAutoFilterRowProperty = TableViewBehavior.RegisterShowAutoFilterRowProperty(type);
      TreeListView.AllowCascadeUpdateProperty = TableViewBehavior.RegisterAllowCascadeUpdateProperty(type);
      TreeListView.AllowPerPixelScrollingProperty = TableViewBehavior.RegisterAllowPerPixelScrollingProperty(type);
      TreeListView.ScrollAnimationDurationProperty = TableViewBehavior.RegisterScrollAnimationDurationProperty(type);
      TreeListView.ScrollAnimationModeProperty = TableViewBehavior.RegisterScrollAnimationModeProperty(type);
      TreeListView.AllowScrollAnimationProperty = TableViewBehavior.RegisterAllowScrollAnimationProperty(type);
      TreeListView.ExtendScrollBarToFixedColumnsProperty = TableViewBehavior.RegisterExtendScrollBarToFixedColumnsProperty(type);
      TreeListView.FixedLeftVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedLeftVisibleColumnsProperty<ColumnBase>(type);
      TreeListView.FixedLeftVisibleColumnsProperty = TreeListView.FixedLeftVisibleColumnsPropertyKey.DependencyProperty;
      TreeListView.FixedRightVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedRightVisibleColumnsProperty<ColumnBase>(type);
      TreeListView.FixedRightVisibleColumnsProperty = TreeListView.FixedRightVisibleColumnsPropertyKey.DependencyProperty;
      TreeListView.FixedNoneVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedNoneVisibleColumnsProperty<ColumnBase>(type);
      TreeListView.FixedNoneVisibleColumnsProperty = TreeListView.FixedNoneVisibleColumnsPropertyKey.DependencyProperty;
      TreeListView.HorizontalViewportPropertyKey = TableViewBehavior.RegisterHorizontalViewportProperty(type);
      TreeListView.HorizontalViewportProperty = TreeListView.HorizontalViewportPropertyKey.DependencyProperty;
      TreeListView.FixedLineWidthProperty = TableViewBehavior.RegisterFixedLineWidthProperty(type);
      TreeListView.ShowVerticalLinesProperty = TableViewBehavior.RegisterShowVerticalLinesProperty(type);
      TreeListView.ShowHorizontalLinesProperty = TableViewBehavior.RegisterShowHorizontalLinesProperty(type);
      TreeListView.RowDecorationTemplateProperty = TableViewBehavior.RegisterRowDecorationTemplateProperty(type);
      TreeListView.DefaultDataRowTemplateProperty = TableViewBehavior.RegisterDefaultDataRowTemplateProperty(type);
      TreeListView.DataRowTemplateProperty = TableViewBehavior.RegisterDataRowTemplateProperty(type);
      TreeListView.DataRowTemplateSelectorProperty = TableViewBehavior.RegisterDataRowTemplateSelectorProperty(type);
      TreeListView.RowIndicatorContentTemplateProperty = TableViewBehavior.RegisterRowIndicatorContentTemplateProperty(type);
      TreeListView.AllowResizingProperty = TableViewBehavior.RegisterAllowResizingProperty(type);
      TreeListView.AllowHorizontalScrollingVirtualizationProperty = TableViewBehavior.RegisterAllowHorizontalScrollingVirtualizationProperty(type);
      TreeListView.RowStyleProperty = TableViewBehavior.RegisterRowStyleProperty(type);
      TreeListView.ScrollingVirtualizationMarginPropertyKey = TableViewBehavior.RegisterScrollingVirtualizationMarginProperty(type);
      TreeListView.ScrollingVirtualizationMarginProperty = TreeListView.ScrollingVirtualizationMarginPropertyKey.DependencyProperty;
      TreeListView.ScrollingHeaderVirtualizationMarginPropertyKey = TableViewBehavior.RegisterScrollingHeaderVirtualizationMarginProperty(type);
      TreeListView.ScrollingHeaderVirtualizationMarginProperty = TreeListView.ScrollingHeaderVirtualizationMarginPropertyKey.DependencyProperty;
      TreeListView.RowMinHeightProperty = TableViewBehavior.RegisterRowMinHeightProperty(type);
      TreeListView.HeaderPanelMinHeightProperty = TableViewBehavior.RegisterHeaderPanelMinHeightProperty(type);
      TreeListView.AutoMoveRowFocusProperty = TableViewBehavior.RegisterAutoMoveRowFocusProperty(type);
      TreeListView.AllowBestFitProperty = TableViewBehavior.RegisterAllowBestFitProperty(type);
      TreeListView.ShowIndicatorProperty = TableViewBehavior.RegisterShowIndicatorProperty(type);
      TreeListView.ActualShowIndicatorPropertyKey = TableViewBehavior.RegisterActualShowIndicatorProperty(type);
      TreeListView.ActualShowIndicatorProperty = TreeListView.ActualShowIndicatorPropertyKey.DependencyProperty;
      TreeListView.IndicatorWidthProperty = TableViewBehavior.RegisterIndicatorWidthProperty(type);
      TreeListView.ActualIndicatorWidthPropertyKey = TableViewBehavior.RegisterActualIndicatorWidthPropertyKey(type);
      TreeListView.ActualIndicatorWidthProperty = TreeListView.ActualIndicatorWidthPropertyKey.DependencyProperty;
      TreeListView.ShowTotalSummaryIndicatorIndentPropertyKey = TableViewBehavior.RegisterShowTotalSummaryIndicatorIndentPropertyKey(type);
      TreeListView.ShowTotalSummaryIndicatorIndentProperty = TreeListView.ShowTotalSummaryIndicatorIndentPropertyKey.DependencyProperty;
      TreeListView.MultiSelectModeProperty = TableViewBehavior.RegisterMultiSelectModeProperty(type);
      TreeListView.UseIndicatorForSelectionProperty = TableViewBehavior.RegisterUseIndicatorForSelectionProperty(type);
      TreeListView.AllowFixedColumnMenuProperty = TableViewBehavior.RegisterAllowFixedColumnMenuProperty(type);
      TreeListView.AllowScrollHeadersProperty = TableViewBehavior.RegisterAllowScrollHeadersProperty(type);
      TreeListView.ShowBandsPanelProperty = TableViewBehavior.RegisterShowBandsPanelProperty(type);
      TreeListView.AllowChangeColumnParentProperty = TableViewBehavior.RegisterAllowChangeColumnParentProperty(type);
      TreeListView.AllowChangeBandParentProperty = TableViewBehavior.RegisterAllowChangeBandParentProperty(type);
      TreeListView.ShowBandsInCustomizationFormProperty = TableViewBehavior.RegisterShowBandsInCustomizationFormProperty(type);
      TreeListView.AllowBandMovingProperty = TableViewBehavior.RegisterAllowBandMovingProperty(type);
      TreeListView.AllowBandResizingProperty = TableViewBehavior.RegisterAllowBandResizingProperty(type);
      TreeListView.AllowAdvancedVerticalNavigationProperty = TableViewBehavior.RegisterAllowAdvancedVerticalNavigationProperty(type);
      TreeListView.AllowAdvancedHorizontalNavigationProperty = TableViewBehavior.RegisterAllowAdvancedHorizontalNavigationProperty(type);
      TreeListView.ColumnChooserBandsSortOrderComparerProperty = TableViewBehavior.RegisterColumnChooserBandsSortOrderComparerProperty(type);
      TreeListView.BandHeaderTemplateProperty = TableViewBehavior.RegisterBandHeaderTemplateProperty(type);
      TreeListView.BandHeaderTemplateSelectorProperty = TableViewBehavior.RegisterBandHeaderTemplateSelectorProperty(type);
      TreeListView.BandHeaderToolTipTemplateProperty = TableViewBehavior.RegisterBandHeaderToolTipTemplateProperty(type);
      TreeListView.PrintBandHeaderStyleProperty = TableViewBehavior.RegisterPrintBandHeaderStyleProperty(type);
      TreeListView.AllowBandMultiRowProperty = TableViewBehavior.RegisterAllowBandMultiRowProperty(type);
      TreeListView.AlternateRowBackgroundProperty = TableViewBehavior.RegisterAlternateRowBackgroundProperty(type);
      TreeListView.ActualAlternateRowBackgroundPropertyKey = TableViewBehavior.RegisterActualAlternateRowBackgroundProperty(type);
      TreeListView.ActualAlternateRowBackgroundProperty = TreeListView.ActualAlternateRowBackgroundPropertyKey.DependencyProperty;
      TreeListView.EvenRowBackgroundProperty = TableViewBehavior.RegisterEvenRowBackgroundProperty(type);
      TreeListView.UseEvenRowBackgroundProperty = TableViewBehavior.RegisterUseEvenRowBackgroundProperty(type);
      TreeListView.AlternationCountProperty = TableViewBehavior.RegisterAlternationCountProperty(type);
      TreeListView.AutoFilterRowCellStyleProperty = DependencyPropertyManager.Register("AutoFilterRowCellStyle", typeof (Style), type, new PropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TreeListView.AllowConditionalFormattingMenuProperty = TableViewBehavior.RegisterAllowConditionalFormattingMenuProperty(type);
      TreeListView.AllowConditionalFormattingManagerProperty = TableViewBehavior.RegisterAllowConditionalFormattingManagerProperty(type);
      TreeListView.PredefinedFormatsProperty = TableViewBehavior.RegisterPredefinedFormatsProperty(type);
      TreeListView.PredefinedColorScaleFormatsProperty = TableViewBehavior.RegisterPredefinedColorScaleFormatsProperty(type);
      TreeListView.PredefinedDataBarFormatsProperty = TableViewBehavior.RegisterPredefinedDataBarFormatsProperty(type);
      TreeListView.PredefinedIconSetFormatsProperty = TableViewBehavior.RegisterPredefinedIconSetFormatsProperty(type);
      TreeListView.FormatConditionDialogServiceTemplateProperty = AssignableServiceHelper2<TreeListView, IDialogService>.RegisterServiceTemplateProperty("FormatConditionDialogServiceTemplate");
      TreeListView.ConditionalFormattingManagerServiceTemplateProperty = AssignableServiceHelper2<TreeListView, IDialogService>.RegisterServiceTemplateProperty("ConditionalFormattingManagerServiceTemplate");
      TreeListView.FormatConditionsItemsAttachedBehaviorProperty = TableViewBehavior.RegisterFormatConditionsItemsAttachedBehaviorProperty<TreeListView>();
      TreeListView.FormatConditionGeneratorTemplateProperty = TableViewBehavior.RegisterFormatConditionGeneratorTemplateProperty<TreeListView>(TreeListView.FormatConditionsItemsAttachedBehaviorProperty);
      TreeListView.FormatConditionGeneratorTemplateSelectorProperty = TableViewBehavior.RegisterFormatConditionGeneratorTemplateSelectorProperty<TreeListView>(TreeListView.FormatConditionsItemsAttachedBehaviorProperty);
      TreeListView.FormatConditionsSourceProperty = TableViewBehavior.RegisterFormatConditionsSourceProperty<TreeListView>(TreeListView.FormatConditionsItemsAttachedBehaviorProperty, TreeListView.FormatConditionGeneratorTemplateProperty, TreeListView.FormatConditionGeneratorTemplateSelectorProperty);
      TreeListView.RowIndentProperty = DependencyPropertyManager.Register("RowIndent", typeof (double), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) =>
      {
        TreeListView treeListView = (TreeListView) d;
        treeListView.RebuildVisibleColumns();
        treeListView.UpdateRows();
      }), (CoerceValueCallback) ((d, e) => (object) Math.Round((double) e))));
      TreeListView.KeyFieldNameProperty = DependencyPropertyManager.Register("KeyFieldName", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).DoRefresh())));
      TreeListView.ParentFieldNameProperty = DependencyPropertyManager.Register("ParentFieldName", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).DoRefresh())));
      TreeListView.CheckBoxFieldNameProperty = DependencyPropertyManager.Register("CheckBoxFieldName", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnCheckBoxFieldNameChanged())));
      TreeListView.CheckBoxValueConverterProperty = DependencyPropertyManager.Register("CheckBoxValueConverter", typeof (IValueConverter), type, new PropertyMetadata((PropertyChangedCallback) null));
      TreeListView.ImageFieldNameProperty = DependencyPropertyManager.Register("ImageFieldName", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).UpdateContentLayout())));
      TreeListView.NodeImageSelectorProperty = DependencyPropertyManager.Register("NodeImageSelector", typeof (TreeListNodeImageSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).UpdateContentLayout())));
      TreeListView.RootValueProperty = DependencyPropertyManager.Register("RootValue", typeof (object), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnRootValueChanged())));
      TreeListView.AutoPopulateServiceColumnsProperty = DependencyPropertyManager.Register("AutoPopulateServiceColumns", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnAutoPopulateServiceColumnsChanged())));
      TreeListView.FilterModeProperty = DependencyPropertyManager.Register("FilterMode", typeof (TreeListFilterMode), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) TreeListFilterMode.Smart, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnFilterModeChanged())));
      TreeListView.FocusedNodeProperty = DependencyPropertyManager.Register("FocusedNode", typeof (TreeListNode), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnFocusedNodeChanged())));
      TreeListView.RowPresenterMarginProperty = DependencyPropertyManager.Register("RowPresenterMargin", typeof (Thickness), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(0.0), (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).UpdateContentLayout())));
      TreeListView.TreeLineStyleProperty = DependencyPropertyManager.Register("TreeLineStyle", typeof (TreeListLineStyle), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) TreeListLineStyle.Solid, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.ShowNodeImagesProperty = DependencyPropertyManager.Register("ShowNodeImages", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.NodeImageSizeProperty = DependencyPropertyManager.Register("NodeImageSize", typeof (Size), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Size(16.0, 16.0), (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.ShowCheckboxesProperty = DependencyPropertyManager.Register("ShowCheckboxes", typeof (bool), type, new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.AllowIndeterminateCheckStateProperty = DependencyPropertyManager.Register("AllowIndeterminateCheckState", typeof (bool), type, new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.AllowRecursiveNodeCheckingProperty = DependencyPropertyManager.Register("AllowRecursiveNodeChecking", typeof (bool), type, new PropertyMetadata((object) false));
      TreeListView.ShowExpandButtonsProperty = DependencyPropertyManager.Register("ShowExpandButtons", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.ShowRootIndentProperty = DependencyPropertyManager.Register("ShowRootIndent", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnIndentItemChanged())));
      TreeListView.AutoExpandAllNodesProperty = DependencyPropertyManager.Register("AutoExpandAllNodes", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.ExpandStateFieldNameProperty = DependencyPropertyManager.Register("ExpandStateFieldName", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).OnExpandStateBindingChanged())));
      TreeListView.ExpandCollapseNodesOnNavigationProperty = DependencyPropertyManager.Register("ExpandCollapseNodesOnNavigation", typeof (bool?), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TreeListView.ExpandNodesOnFilteringProperty = DependencyPropertyManager.Register("ExpandNodesOnFiltering", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.AllowDefaultContentForHierarchicalDataTemplateProperty = DependencyPropertyManager.Register("AllowDefaultContentForHierarchicalDataTemplate", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.NodeExpandingEvent = EventManager.RegisterRoutedEvent("NodeExpanding", RoutingStrategy.Direct, typeof (TreeListNodeAllowEventHandler), type);
      TreeListView.NodeExpandedEvent = EventManager.RegisterRoutedEvent("NodeExpanded", RoutingStrategy.Direct, typeof (TreeListNodeEventHandler), type);
      TreeListView.NodeCollapsingEvent = EventManager.RegisterRoutedEvent("NodeCollapsing", RoutingStrategy.Direct, typeof (TreeListNodeAllowEventHandler), type);
      TreeListView.NodeCollapsedEvent = EventManager.RegisterRoutedEvent("NodeCollapsed", RoutingStrategy.Direct, typeof (TreeListNodeEventHandler), type);
      TreeListView.NodeCheckStateChangedEvent = EventManager.RegisterRoutedEvent("NodeCheckStateChanged", RoutingStrategy.Direct, typeof (TreeListNodeEventHandler), type);
      TreeListView.StartSortingEvent = EventManager.RegisterRoutedEvent("StartSorting", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      TreeListView.EndSortingEvent = EventManager.RegisterRoutedEvent("EndSorting", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      TreeListView.ShowingEditorEvent = EventManager.RegisterRoutedEvent("ShowingEditor", RoutingStrategy.Direct, typeof (TreeListShowingEditorEventHandler), type);
      TreeListView.ShownEditorEvent = EventManager.RegisterRoutedEvent("ShownEditor", RoutingStrategy.Direct, typeof (TreeListEditorEventHandler), type);
      TreeListView.HiddenEditorEvent = EventManager.RegisterRoutedEvent("HiddenEditor", RoutingStrategy.Direct, typeof (TreeListEditorEventHandler), type);
      TreeListView.RowDoubleClickEvent = EventManager.RegisterRoutedEvent("RowDoubleClick", RoutingStrategy.Direct, typeof (RowDoubleClickEventHandler), type);
      TreeListView.SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof (TreeListSelectionChangedEventHandler), type);
      TreeListView.CellValueChangedEvent = EventManager.RegisterRoutedEvent("CellValueChanged", RoutingStrategy.Direct, typeof (TreeListCellValueChangedEventHandler), type);
      TreeListView.CellValueChangingEvent = EventManager.RegisterRoutedEvent("CellValueChanging", RoutingStrategy.Direct, typeof (TreeListCellValueChangedEventHandler), type);
      TreeListView.CustomScrollAnimationEvent = TableViewBehavior.RegisterCustomScrollAnimationEvent(type);
      TreeListView.InvalidNodeExceptionEvent = EventManager.RegisterRoutedEvent("InvalidNodeException", RoutingStrategy.Direct, typeof (TreeListInvalidNodeExceptionEventHandler), type);
      TreeListView.ValidateNodeEvent = EventManager.RegisterRoutedEvent("ValidateNode", RoutingStrategy.Direct, typeof (TreeListNodeValidationEventHandler), type);
      TreeListView.CopyingToClipboardEvent = EventManager.RegisterRoutedEvent("CopyingToClipboard", RoutingStrategy.Direct, typeof (TreeListCopyingToClipboardEventHandler), type);
      TreeListView.TreeDerivationModeProperty = DependencyPropertyManager.Register("TreeDerivationMode", typeof (TreeDerivationMode), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) TreeDerivationMode.Selfreference, (PropertyChangedCallback) ((o, e) => ((TreeListView) o).OnItemsSourceModeChanged())));
      TreeListView.ChildNodesPathProperty = DependencyPropertyManager.Register("ChildNodesPath", typeof (string), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((o, e) => ((TreeListView) o).ChildrenPropertyUpdate())));
      TreeListView.ChildNodesSelectorProperty = DependencyPropertyManager.Register("ChildNodesSelector", typeof (IChildNodesSelector), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((o, e) => ((TreeListView) o).OnChildNodesSelectorChanged())));
      TreeListView.EnableDynamicLoadingProperty = DependencyPropertyManager.Register("EnableDynamicLoading", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.FetchSublevelChildrenOnExpandProperty = DependencyPropertyManager.Register("FetchSublevelChildrenOnExpand", typeof (bool), type, new PropertyMetadata((object) true));
      TreeListView.VerticalScrollbarVisibilityProperty = DependencyPropertyManager.Register("VerticalScrollbarVisibility", typeof (ScrollBarVisibility), type, new PropertyMetadata((object) ScrollBarVisibility.Visible));
      TreeListView.HorizontalScrollbarVisibilityProperty = DependencyPropertyManager.Register("HorizontalScrollbarVisibility", typeof (ScrollBarVisibility), type, new PropertyMetadata((object) ScrollBarVisibility.Auto));
      TreeListView.PrintRowTemplateProperty = DependencyPropertyManager.Register("PrintRowTemplate", typeof (DataTemplate), type, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TreeListView.PrintAutoWidthProperty = DependencyPropertyManager.Register("PrintAutoWidth", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.PrintColumnHeadersProperty = DependencyPropertyManager.Register("PrintColumnHeaders", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.PrintBandHeadersProperty = DependencyPropertyManager.Register("PrintBandHeaders", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.PrintColumnHeaderStyleProperty = DependencyPropertyManager.Register("PrintColumnHeaderStyle", typeof (Style), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TreeListView.PrintAllNodesProperty = DependencyPropertyManager.Register("PrintAllNodes", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.PrintExpandButtonsProperty = DependencyPropertyManager.Register("PrintExpandButtons", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TreeListView.PrintNodeImagesProperty = DependencyPropertyManager.Register("PrintNodeImages", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.RestoreFocusOnExpandProperty = DependencyPropertyManager.Register("RestoreFocusOnExpand", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((s, e) => ((TreeListView) s).OnRestoreFocusOnExpandChanged())));
      TreeListView.AllowChildNodeSourceUpdatesProperty = DependencyPropertyManager.Register("AllowChildNodeSourceUpdates", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.ShowDataNavigatorProperty = DependencyPropertyManager.Register("ShowDataNavigator", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TreeListView.AllowTreeIndentScrollingProperty = DependencyProperty.Register("AllowTreeIndentScrolling", typeof (bool), type, new PropertyMetadata((object) false, (PropertyChangedCallback) ((o, e) => ((TreeListView) o).UpdateActualAllowTreeIndentScrolling())));
      TreeListView.UseLightweightTemplatesProperty = TableViewBehavior.RegisterUseLightweightTemplatesProperty(type);
      TreeListView.RowDetailsTemplateProperty = TableViewBehavior.RegisterRowDetailsTemplateProperty(type);
      TreeListView.RowDetailsTemplateSelectorProperty = TableViewBehavior.RegisterRowDetailsTemplateSelectorProperty(type);
      TreeListView.ActualRowDetailsTemplateSelectorPropertyKey = TableViewBehavior.RegisterActualRowDetailsTemplateSelectorProperty(type);
      TreeListView.ActualRowDetailsTemplateSelectorProperty = TreeListView.ActualRowDetailsTemplateSelectorPropertyKey.DependencyProperty;
      TreeListView.RowDetailsVisibilityModeProperty = TableViewBehavior.RegisterRowDetailsVisibilityModeProperty(type);
      EventManager.RegisterClassHandler(type, DXSerializer.CreateCollectionItemEvent, (Delegate) ((s, e) => ((TreeListView) s).OnDeserializeCreateCollectionItem(e)));
      TreeListView.ScrollBarAnnotationsCreatingEvent = EventManager.RegisterRoutedEvent("ScrollBarAnnotationsCreating", RoutingStrategy.Direct, typeof (ScrollBarAnnotationsCreatingEventHandler), type);
      TreeListView.ScrollBarAnnotationModeProperty = DependencyPropertyManager.RegisterAttached("ScrollBarAnnotationMode", typeof (DevExpress.Xpf.Grid.ScrollBarAnnotationMode?), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).ScrollBarAnnotationModeChanged())));
      TreeListView.ScrollBarAnnotationsAppearanceProperty = DependencyProperty.Register("ScrollBarAnnotationsAppearance", typeof (ScrollBarAnnotationsAppearance), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TreeListView) d).ScrollBarAnnotationsAppearanceChanged(e.NewValue != null))));
      TreeListView.ShowCriteriaInAutoFilterRowProperty = TableViewBehavior.RegisterShowCriteriaInAutoFilterRowProperty(type);
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListView class.
    /// </para>
    ///             </summary>
    public TreeListView()
      : base((MasterNodeContainer) null, (MasterRowsContainer) null, (DataControlDetailDescriptor) null)
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (TreeListView));
      this.treeListDataProvider = this.CreateDataProvider();
      this.FocusedNodeSaveLocker = new Locker();
      this.bandMenuControllerValue = this.CreateMenuControllerLazyValue();
      this.AutoDetectColumnTypeInHierarchicalMode = true;
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
      this.CsvHelper.Export(stream);
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
      this.CsvHelper.Export(stream, options);
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
      this.CsvHelper.Export(filePath);
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
      this.CsvHelper.Export(filePath, options);
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
      this.XlsHelper.Export(stream);
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
      this.XlsHelper.Export(stream, options);
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
      this.XlsHelper.Export(filePath);
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
      this.XlsHelper.Export(filePath, options);
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
      this.XlsxHelper.Export(stream);
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
      this.XlsxHelper.Export(stream, options);
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
      this.XlsxHelper.Export(filePath);
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
      this.XlsxHelper.Export(filePath, options);
    }

    protected internal override IDataAwareExportHelper CreateDataAwareExportHelper(ExportTarget exportTarget, IDataAwareExportOptions options)
    {
      return (IDataAwareExportHelper) new TreeListViewDataAwareExportHelper(this, exportTarget, options);
    }

    bool ITableView.UseRowDetailsTemplate(int rowHandle)
    {
      return this.TreeListViewBehavior.UseRowDetailsTemplate(rowHandle);
    }

    /// <summary>
    ///                 <para>Shows the edit form as a popup dialog window.
    /// </para>
    ///             </summary>
    public void ShowDialogEditForm()
    {
      this.TreeListViewEditFormManager.ShowDialogEditForm();
    }

    /// <summary>
    ///                 <para>Shows the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void ShowInlineEditForm()
    {
      this.TreeListViewEditFormManager.ShowInlineEditForm();
    }

    /// <summary>
    ///                 <para>Shows the edit form in a mode specified by the <see cref="P:DevExpress.Xpf.Grid.TreeListView.EditFormShowMode" /> property.
    /// </para>
    ///             </summary>
    public void ShowEditForm()
    {
      this.TreeListViewEditFormManager.ShowEditForm();
    }

    /// <summary>
    ///                 <para>Cancels all changes and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void HideEditForm()
    {
      this.TreeListViewEditFormManager.HideEditForm();
    }

    /// <summary>
    ///                 <para>Posts all changes to the data source and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void CloseEditForm()
    {
      this.TreeListViewEditFormManager.CloseEditForm();
    }

    protected internal override IEditFormManager CreateEditFormManager()
    {
      return (IEditFormManager) new EditFormManager((ITableView) this);
    }

    protected internal override IEditFormOwner CreateEditFormOwner()
    {
      return (IEditFormOwner) new EditFormOwner((ITableView) this);
    }

    internal override void UpdateActualFadeSelectionOnLostFocus(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TableViewBehavior.OnFadeSelectionOnLostFocusChanged(d, e);
    }

    protected virtual void OnFocusedNodeChanged()
    {
      if (this.FocusedRowHandle == -999997 || this.FocusedRowHandleChangedLocker.IsLocked)
        return;
      this.SetFocusedRowHandle(this.FocusedNode != null ? this.TreeListDataProvider.GetRowHandleByNode(this.FocusedNode) : int.MinValue);
    }

    protected void OnRestoreFocusOnExpandChanged()
    {
      if (this.RestoreFocusOnExpand)
        return;
      this.ClearFocusedNodeSave();
    }

    private void OnAutoPopulateServiceColumnsChanged()
    {
      if (this.DataControl == null || this.DataControl.AutoGenerateColumns == AutoGenerateColumnsMode.None)
        return;
      this.DataControl.PopulateColumns();
    }

    private void OnCheckBoxFieldNameChanged()
    {
      if (this.DataControl != null && !this.DataControl.IsLoading)
        this.TreeListDataProvider.InitNodesIsChecked();
      this.UpdateRows();
    }

    protected internal virtual void OnExpandStateBindingChanged()
    {
      this.TreeListDataProvider.UpdateNodesExpandState(this.Nodes, true);
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedLeftVisibleColumns(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedRightVisibleColumns(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <param name="manager">
    /// 
    /// 
    /// </param>
    /// <returns>
    /// </returns>
    [Browsable(false)]
    public bool ShouldSerializeFixedNoneVisibleColumns(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    protected internal override void UpdateAlternateRowBackground()
    {
      this.ActualAlternateRowBackground = this.AlternateRowBackground ?? (this.UseEvenRowBackground ? this.EvenRowBackground : (Brush) null);
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
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

    protected void ClearFocusedNodeSave()
    {
      this.FocusedNodeSave = (TreeListNode) null;
    }

    ScrollBarAnnotationsCreatingEventArgs ITableView.RaiseScrollBarAnnotationsCreating()
    {
      ScrollBarAnnotationsCreatingEventArgs creatingEventArgs = new ScrollBarAnnotationsCreatingEventArgs(TreeListView.ScrollBarAnnotationsCreatingEvent, (object) this);
      this.RaiseEventInOriginationView((RoutedEventArgs) creatingEventArgs);
      return creatingEventArgs;
    }

    void ITableView.RaiseScrollBarCustomRowAnnotation(ScrollBarCustomRowAnnotationEventArgs e)
    {
      EventHandler<ScrollBarCustomRowAnnotationEventArgs> eventHandler = this.ScrollBarCustomRowAnnotation;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal virtual void RaiseEditFormShowing(EditFormShowingEventArgs e)
    {
      EventHandler<EditFormShowingEventArgs> eventHandler = this.EditFormShowing;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected internal override GridFilterColumn CreateFilterColumn(ColumnBase column, bool useDomainDataSourceRestrictions, bool useWcfSource)
    {
      if (this.TreeDerivationMode != TreeDerivationMode.Selfreference)
        return (GridFilterColumn) new TreeListFilterColumn(column, useDomainDataSourceRestrictions, useWcfSource);
      return base.CreateFilterColumn(column, useDomainDataSourceRestrictions, useWcfSource);
    }

    protected override RowsClipboardController CreateClipboardController()
    {
      return (RowsClipboardController) new TreeListRowsClipboardController(this);
    }

    protected virtual TreeListDataProvider CreateDataProvider()
    {
      return new TreeListDataProvider(this);
    }

    protected override DataIteratorBase CreateDataIterator()
    {
      return (DataIteratorBase) new TreeListDataIterator(this);
    }

    protected override DataViewBehavior CreateViewBehavior()
    {
      return (DataViewBehavior) new TreeListViewBehavior(this);
    }

    protected override DataViewCommandsBase CreateCommandsContainer()
    {
      return (DataViewCommandsBase) new TreeListViewCommands(this);
    }

    protected override SelectionStrategyBase CreateSelectionStrategy()
    {
      if (this.NavigationStyle == GridViewNavigationStyle.None)
        return (SelectionStrategyBase) new SelectionStrategyNavigationNone((DataViewBase) this);
      if (!this.IsMultiSelection)
        return (SelectionStrategyBase) new SelectionStrategyNone((DataViewBase) this);
      if (this.IsMultiRowSelection)
      {
        if (this.DataControl != null && this.DataControl.SelectionMode == DevExpress.Xpf.Grid.MultiSelectMode.Row && this.ShowSelectionRectangle)
          return (SelectionStrategyBase) new TreeListSelectionStrategyRowRange(this);
        return (SelectionStrategyBase) new TreeListSelectionStrategyRow(this);
      }
      if (this.NavigationStyle == GridViewNavigationStyle.Row)
        return (SelectionStrategyBase) new TreeListSelectionStrategyRow(this);
      if (this.ShowSelectionRectangle && this.DataControl != null && this.DataControl.SelectionMode == DevExpress.Xpf.Grid.MultiSelectMode.Cell)
        return (SelectionStrategyBase) new TreeListSelectionStrageryCellRectangle(this);
      return (SelectionStrategyBase) new TreeListSelectionStrageryCell(this);
    }

    protected override bool ChangeVisibleRowExpandCore(int rowHandle)
    {
      return this.ChangeNodeExpanded(rowHandle);
    }

    internal override bool IsDataRowNodeExpanded(DataRowNode rowNode)
    {
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle(rowNode.RowHandle.Value);
      if (nodeByRowHandle != null)
        return nodeByRowHandle.IsExpanded;
      return false;
    }

    protected internal override void MoveColumnToCore(ColumnBase source, int newVisibleIndex, HeaderPresenterType moveFrom, HeaderPresenterType moveTo)
    {
      int newVisibleIndex1 = newVisibleIndex;
      if (source.Fixed == FixedStyle.None && this.FixedNoneVisibleColumns.Count == 0)
        newVisibleIndex1 = this.FixedLeftVisibleColumns.Count;
      else if (source.Fixed == FixedStyle.Left && this.FixedLeftVisibleColumns.Count == 0)
        newVisibleIndex1 = 0;
      else if (source.Fixed == FixedStyle.Right && this.FixedRightVisibleColumns.Count == 0)
        newVisibleIndex1 = this.VisibleColumns.Count;
      base.MoveColumnToCore(source, newVisibleIndex1, moveFrom, moveTo);
    }

    internal override bool IsExpandableRowFocused()
    {
      return false;
    }

    internal override IColumnCollection CreateEmptyColumnCollection()
    {
      return (IColumnCollection) new ColumnCollection((DataControlBase) null);
    }

    internal bool ExpandNodeAndAllChildren()
    {
      this.TreeListDataProvider.ToggleExpandedAllChildNodes(this.TreeListDataProvider.GetNodeByRowHandle(this.FocusedRowHandle), true);
      return true;
    }

    /// <summary>
    ///                 <para>Saves the current nodes state to the memory.
    /// </para>
    ///             </summary>
    public void SaveNodesState()
    {
      this.TreeListDataProvider.SaveNodesState(true);
    }

    /// <summary>
    ///                 <para>Restores the nodes state.
    /// </para>
    ///             </summary>
    public void RestoreNodesState()
    {
      this.TreeListDataProvider.RestoreNodesState(true);
    }

    /// <summary>
    ///                 <para>Expands all nodes.
    /// </para>
    ///             </summary>
    public void ExpandAllNodes()
    {
      this.TreeListDataProvider.ToggleExpandedAllNodes(true);
    }

    /// <summary>
    ///                 <para>Collapses all nodes.
    /// </para>
    ///             </summary>
    public void CollapseAllNodes()
    {
      this.TreeListDataProvider.ToggleExpandedAllNodes(false);
    }

    /// <summary>
    ///                 <para>Expands the parent nodes down to the specified nesting level.
    /// </para>
    ///             </summary>
    /// <param name="level">An integer value that specifies the nesting level.</param>
    public void ExpandToLevel(int level)
    {
      this.TreeListDataProvider.ExpandToLevel(level);
    }

    /// <summary>
    ///                 <para>Expands the specified node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies a node by its handle.
    /// 
    ///           </param>
    public void ExpandNode(int rowHandle)
    {
      this.ChangeNodeExpanded(rowHandle, true);
    }

    /// <summary>
    ///                 <para>Collapses the specified node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies a node by its handle.
    /// 
    ///           </param>
    public void CollapseNode(int rowHandle)
    {
      this.ChangeNodeExpanded(rowHandle, false);
    }

    /// <summary>
    ///                 <para>Specifies the expanded state of a node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies a node by its handle.
    /// 
    ///           </param>
    /// <param name="isExpanded">
    /// <b>true</b> to expand the node; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public void ChangeNodeExpanded(int rowHandle, bool isExpanded)
    {
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return;
      nodeByRowHandle.IsExpanded = isExpanded;
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which contains the cell.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that identifies the column containing the cell by its field name.
    /// 
    ///           </param>
    /// <returns>An object that is the value of the specified cell. <b>null</b> (<b>Nothing</b> in Visual Basic) if the cell was not found.
    /// </returns>
    public object GetNodeValue(TreeListNode node, string fieldName)
    {
      return this.TreeListDataProvider.GetNodeValue(node, fieldName);
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column containing the cell by its field name.
    /// 
    ///           </param>
    /// <returns>An object that is the value of the specified cell. <b>null</b> (<b>Nothing</b> in Visual Basic) if the cell was not found.
    /// </returns>
    public object GetNodeValue(TreeListNode node, ColumnBase column)
    {
      if (column == null)
        return (object) null;
      return this.TreeListDataProvider.GetNodeValue(node, column.FieldName);
    }

    /// <summary>
    ///                 <para>Sets the value of the specified cell in the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node containing the cell.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that identifies the column containing the cell by its field name.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that specifies the specified cell's new value.
    /// 
    ///           </param>
    public void SetNodeValue(TreeListNode node, string fieldName, object value)
    {
      this.TreeListDataProvider.SetNodeValue(node, fieldName, value);
    }

    /// <summary>
    ///                 <para>Sets the value of the specified cell in the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node containing the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column containing the cell.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that specifies the specified cell's new value.
    /// 
    ///           </param>
    public void SetNodeValue(TreeListNode node, ColumnBase column, object value)
    {
      if (column == null)
        return;
      this.TreeListDataProvider.SetNodeValue(node, column.FieldName, value);
    }

    /// <summary>
    ///                 <para>Returns a node with the specified handle.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">An integer value that specifies the row handle.</param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node with the specified handle. <b>null</b> (<b>Nothing</b> in Visual Basic) if the node was not found.
    /// </returns>
    public TreeListNode GetNodeByRowHandle(int rowHandle)
    {
      return this.TreeListDataProvider.GetNodeByRowHandle(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns a node with the specified visible index.
    /// </para>
    ///             </summary>
    /// <param name="visibleIndex">
    /// An integer value that specifies the node's position within a View.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node with the specified visible index. <b>null</b> (<b>Nothing</b> in Visual Basic) if the node was not found.
    /// </returns>
    public TreeListNode GetNodeByVisibleIndex(int visibleIndex)
    {
      return this.TreeListDataProvider.GetNodeByVisibleIndex(visibleIndex);
    }

    /// <summary>
    ///                 <para>Returns the node's position within a View among visible nodes.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node whose visible index is returned.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the node's position among visible nodes. <b>-1</b> if the specified node is hidden.
    /// </returns>
    public int GetNodeVisibleIndex(TreeListNode node)
    {
      return this.TreeListDataProvider.GetVisibleIndexByNode(node);
    }

    /// <summary>
    ///                 <para>Returns a node with the specified content.
    /// </para>
    ///             </summary>
    /// <param name="content">
    /// An object that is the content of the required node.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node with the specified content. <b>null</b> (<b>Nothing</b> in Visual Basic) if the node was not found.
    /// </returns>
    public TreeListNode GetNodeByContent(object content)
    {
      return this.TreeListDataProvider.FindNodeByValue(content);
    }

    /// <summary>
    ///                 <para>Searches for a node with the specified value within the specified cell, and returns the first found node.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of the column containing the required cell.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that specifies the value of the required cell.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node with the specified value within the specified cell. <b>null</b> (<b>Nothing</b> in Visual Basic) if the node was not found.
    /// </returns>
    public TreeListNode GetNodeByCellValue(string fieldName, object value)
    {
      if (string.IsNullOrEmpty(fieldName))
        return this.TreeListDataProvider.FindNodeByValue(value);
      return this.TreeListDataProvider.FindNodeByValue(fieldName, value);
    }

    /// <summary>
    ///                 <para>Returns the node with the specified key value.
    /// </para>
    ///             </summary>
    /// <param name="keyValue">An object that specifies the key value.</param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node with the specified key value.
    /// </returns>
    public TreeListNode GetNodeByKeyValue(object keyValue)
    {
      return this.TreeListDataProvider.FindNodeByValue(this.KeyFieldName, keyValue);
    }

    /// <summary>
    ///                 <para>Removes the specified node and all its children (if any).
    /// 
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies the node by its handle.
    /// 
    ///           </param>
    public void DeleteNode(int rowHandle)
    {
      this.DeleteNode(rowHandle, true);
    }

    /// <summary>
    ///                 <para>Removes the specified node and optionally, all its children.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies the node by its handle.
    /// 
    ///           </param>
    /// <param name="deleteChildren">
    /// <b>true</b> to remove all child nodes; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public void DeleteNode(int rowHandle, bool deleteChildren)
    {
      this.DeleteNode(this.GetNodeByRowHandle(rowHandle), deleteChildren);
    }

    /// <summary>
    ///                 <para>Removes the specified node and all its children (if any).
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to remove.
    /// 
    ///           </param>
    public void DeleteNode(TreeListNode node)
    {
      this.DeleteNode(node, true);
    }

    /// <summary>
    ///                 <para>Removes the specified node and optionally, all its children.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to remove.
    /// 
    ///           </param>
    /// <param name="deleteChildren">
    /// <b>true</b> to remove all child nodes; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public void DeleteNode(TreeListNode node, bool deleteChildren)
    {
      this.TreeListDataProvider.DeleteNode(node, deleteChildren);
    }

    /// <summary>
    ///                 <para>Checks all nodes.
    /// </para>
    ///             </summary>
    public void CheckAllNodes()
    {
      this.CheckAllNodesCore(true);
    }

    /// <summary>
    ///                 <para>Unchecks all node check boxes.
    /// </para>
    ///             </summary>
    public void UncheckAllNodes()
    {
      this.CheckAllNodesCore(false);
    }

    /// <summary>
    ///                 <para>Reloads the image for the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node whose image is to be reloaded.
    /// 
    ///           </param>
    public void RefreshNodeImage(TreeListNode node)
    {
      this.TreeListDataProvider.UpdateRow(node);
    }

    private void CheckAllNodesCore(bool isChecked)
    {
      foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.Nodes))
      {
        if (node.IsVisible)
        {
          node.SetNodeChecked(node, new bool?(isChecked));
          this.RaiseNodeChanged(node, NodeChangeType.CheckBox);
        }
      }
      this.UpdateRows();
    }

    /// <summary>
    ///                 <para>Moves focus to the node preceding the one currently focused.
    /// </para>
    ///             </summary>
    /// <param name="allowNavigateToAutoFilterRow">
    /// <b>true</b> to allow moving focus to the Auto Filter Row; otherwise, <b>false</b>.
    /// 
    /// 
    ///           </param>
    public void MovePrevRow(bool allowNavigateToAutoFilterRow)
    {
      this.TreeListViewBehavior.MovePrevRow(allowNavigateToAutoFilterRow);
    }

    internal void UpdateFocusedNode()
    {
      this.FocusedNode = this.TreeListDataProvider.GetNodeByRowHandle(this.FocusedRowHandle);
      if (this.FocusedNodeSaveLocker.IsLocked)
        return;
      this.ClearFocusedNodeSave();
    }

    protected internal bool ChangeNodeExpanded(int rowHandle)
    {
      if (!this.CommitEditing())
        return false;
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return false;
      bool isExpanded = nodeByRowHandle.IsExpanded;
      nodeByRowHandle.IsExpanded = !nodeByRowHandle.IsExpanded;
      this.UpdateScrollBarAnnotations();
      return isExpanded != nodeByRowHandle.IsExpanded;
    }

    protected internal bool ChangeNodeCheckState(int rowHandle)
    {
      if (!this.CommitEditing())
        return false;
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle(rowHandle);
      if (nodeByRowHandle == null)
        return false;
      bool? isChecked1 = nodeByRowHandle.IsChecked;
      bool? isChecked2 = nodeByRowHandle.IsChecked;
      if ((!isChecked2.GetValueOrDefault() ? 0 : (isChecked2.HasValue ? 1 : 0)) != 0)
        nodeByRowHandle.IsChecked = !this.AllowIndeterminateCheckState ? new bool?(false) : new bool?();
      else if (!nodeByRowHandle.IsChecked.HasValue)
      {
        nodeByRowHandle.IsChecked = new bool?(false);
      }
      else
      {
        bool? isChecked3 = nodeByRowHandle.IsChecked;
        if ((isChecked3.GetValueOrDefault() ? 0 : (isChecked3.HasValue ? 1 : 0)) != 0)
          nodeByRowHandle.IsChecked = new bool?(true);
      }
      bool? nullable = isChecked1;
      bool? isChecked4 = nodeByRowHandle.IsChecked;
      if (nullable.GetValueOrDefault() == isChecked4.GetValueOrDefault())
        return nullable.HasValue != isChecked4.HasValue;
      return true;
    }

    protected override void OnFocusedRowHandleChangedCore(int oldRowHandle)
    {
      this.UpdateFocusedNode();
      base.OnFocusedRowHandleChangedCore(oldRowHandle);
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.FocusedRow, false);
    }

    protected internal virtual void OnChangeNodeExpanded(object commandParameter)
    {
      this.ChangeNodeExpanded((int) commandParameter);
    }

    protected internal virtual bool CanChangeNodeExpaned(object commandParameter)
    {
      if (commandParameter == null)
        return false;
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle((int) commandParameter);
      if (nodeByRowHandle == null)
        return false;
      return nodeByRowHandle.IsTogglable;
    }

    protected internal virtual void OnChangeNodeCheckState(object commandParameter)
    {
      this.ChangeNodeCheckState((int) commandParameter);
    }

    protected internal virtual bool CanChangeNodeCheckState(object commandParameter)
    {
      if (commandParameter == null)
        return false;
      TreeListNode nodeByRowHandle = this.TreeListDataProvider.GetNodeByRowHandle((int) commandParameter);
      if (nodeByRowHandle == null)
        return false;
      return nodeByRowHandle.IsCheckBoxEnabled;
    }

    protected internal IList<IColumnInfo> GetColumns()
    {
      List<IColumnInfo> columnInfoList = new List<IColumnInfo>();
      if (this.DataControl != null)
      {
        foreach (ColumnBase columnBase in (IEnumerable) this.DataControl.ColumnsCore)
          columnInfoList.Add((IColumnInfo) columnBase);
      }
      return (IList<IColumnInfo>) columnInfoList;
    }

    protected internal virtual void UpdateRows()
    {
      if (this.DataControl != null)
        this.DataControl.UpdateLayoutCore();
      this.RebuildVisibleColumns();
      this.UpdateContentLayout();
    }

    protected internal void DoRefresh()
    {
      this.TreeListDataProvider.DoRefresh(false);
    }

    protected internal void DoRefresh(bool keepNodesState)
    {
      this.TreeListDataProvider.DoRefresh(keepNodesState);
    }

    private void OnItemsSourceModeChanged()
    {
      this.TreeListDataProvider.UpdateDataHelper();
      this.DoRefresh();
    }

    protected internal override void OnDataChanged(bool rebuildVisibleColumns)
    {
      if (this.TreeListDataProvider.IsUnboundMode)
        this.TreeListDataProvider.RePopulateColumns();
      base.OnDataChanged(rebuildVisibleColumns);
      if (!this.IsLoaded)
        this.ForceAutoExpandAllNodes();
      if (this.RootView == null || !((ITableView) this.RootView).ScrollBarAnnotationsManager.GridLoaded)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    protected internal virtual void OnCurrentIndexChanged()
    {
      this.SetFocusOnCurrentControllerRow();
    }

    protected internal virtual string GetNodeDisplayText(TreeListNode node, string fieldName, object value)
    {
      if (this.DataControl == null)
        return (string) null;
      ColumnBase column = this.DataControl.ColumnsCore[fieldName];
      string @string = this.GetDisplayObject(value, column).ToString();
      if (this.customColumnDisplayText == null)
        return @string;
      return this.RaiseCustomColumnDisplayText(node, column, value, @string);
    }

    protected internal virtual bool RaiseNodeExpanding(TreeListNode node)
    {
      TreeListNodeAllowEventArgs nodeAllowEventArgs1 = new TreeListNodeAllowEventArgs(node);
      nodeAllowEventArgs1.RoutedEvent = TreeListView.NodeExpandingEvent;
      TreeListNodeAllowEventArgs nodeAllowEventArgs2 = nodeAllowEventArgs1;
      this.RaiseEvent((RoutedEventArgs) nodeAllowEventArgs2);
      return nodeAllowEventArgs2.Allow;
    }

    protected internal virtual void RaiseNodeExpanded(TreeListNode node)
    {
      TreeListNodeEventArgs listNodeEventArgs = new TreeListNodeEventArgs(node);
      listNodeEventArgs.RoutedEvent = TreeListView.NodeExpandedEvent;
      this.RaiseEvent((RoutedEventArgs) listNodeEventArgs);
    }

    protected internal virtual bool RaiseNodeCollapsing(TreeListNode node)
    {
      TreeListNodeAllowEventArgs nodeAllowEventArgs1 = new TreeListNodeAllowEventArgs(node);
      nodeAllowEventArgs1.RoutedEvent = TreeListView.NodeCollapsingEvent;
      TreeListNodeAllowEventArgs nodeAllowEventArgs2 = nodeAllowEventArgs1;
      this.RaiseEvent((RoutedEventArgs) nodeAllowEventArgs2);
      return nodeAllowEventArgs2.Allow;
    }

    protected internal virtual void RaiseNodeCollapsed(TreeListNode node)
    {
      TreeListNodeEventArgs listNodeEventArgs = new TreeListNodeEventArgs(node);
      listNodeEventArgs.RoutedEvent = TreeListView.NodeCollapsedEvent;
      this.RaiseEvent((RoutedEventArgs) listNodeEventArgs);
    }

    protected internal virtual void RaiseNodeCheckStateChanged(TreeListNode node)
    {
      TreeListNodeEventArgs listNodeEventArgs = new TreeListNodeEventArgs(node);
      listNodeEventArgs.RoutedEvent = TreeListView.NodeCheckStateChangedEvent;
      this.RaiseEvent((RoutedEventArgs) listNodeEventArgs);
    }

    protected internal virtual void RaiseNodeChanged(TreeListNode node, NodeChangeType changeType)
    {
      if (this.nodeChanged == null)
        return;
      this.nodeChanged((object) this, new TreeListNodeChangedEventArgs(node, changeType));
    }

    protected virtual string RaiseCustomColumnDisplayText(TreeListNode node, ColumnBase column, object value, string displayText)
    {
      if (this.customColumnDisplayTextEventArgs == null)
        this.customColumnDisplayTextEventArgs = new TreeListCustomColumnDisplayTextEventArgs(node, column, value, displayText);
      else
        this.customColumnDisplayTextEventArgs.SetArgs(node, column, value, displayText);
      if (this.customColumnDisplayText != null)
        this.customColumnDisplayText((object) this, this.customColumnDisplayTextEventArgs);
      this.customColumnDisplayTextEventArgs.Clear();
      return this.customColumnDisplayTextEventArgs.DisplayText;
    }

    protected internal virtual object RaiseCustomUnboundColumnData(object p, string propName, object value, bool isGetAction)
    {
      if (this.DataControl == null)
        return (object) null;
      TreeListUnboundColumnDataEventArgs e = new TreeListUnboundColumnDataEventArgs(this.DataControl.ColumnsCore[propName], p as TreeListNode, value, isGetAction);
      if (this.customUnboundColumnData != null)
        this.customUnboundColumnData((object) this, e);
      return e.Value;
    }

    protected internal virtual bool? RaiseCustomNodeFilter(TreeListNode node)
    {
      TreeListNodeFilterEventArgs e = new TreeListNodeFilterEventArgs(node);
      if (this.customNodeFilter != null)
        this.customNodeFilter((object) this, e);
      if (!e.Handled)
        return new bool?();
      return new bool?(e.Visible);
    }

    protected internal bool RaiseCustomFiterPopupList(TreeListNode node, DataColumnInfo columnInfo)
    {
      CustomColumnFilterListEventArgs e = new CustomColumnFilterListEventArgs(node, this.ColumnsCore[columnInfo.Name] as TreeListColumn);
      if (this.customFilterPopupList != null)
        this.customFilterPopupList((object) this, e);
      return e.Visible;
    }

    protected internal virtual void RaiseCustomColumnSort(TreeListCustomColumnSortEventArgs e)
    {
      if (this.customColumnSort == null)
        return;
      this.customColumnSort((object) this, e);
    }

    protected internal virtual void RaiseCustomSummary(TreeListCustomSummaryEventArgs e)
    {
      if (this.customSummary == null)
        return;
      this.customSummary((object) this, e);
    }

    protected internal virtual void RaiseInvalidNodeException(TreeListNode node, ControllerRowExceptionEventArgs args)
    {
      TreeListInvalidNodeExceptionEventArgs exceptionEventArgs1 = new TreeListInvalidNodeExceptionEventArgs(node, args.Exception.Message, this.GetLocalizedString(GridControlStringId.ErrorWindowTitle), args.Exception, ExceptionMode.DisplayError);
      exceptionEventArgs1.RoutedEvent = TreeListView.InvalidNodeExceptionEvent;
      TreeListInvalidNodeExceptionEventArgs exceptionEventArgs2 = exceptionEventArgs1;
      this.RaiseEvent((RoutedEventArgs) exceptionEventArgs2);
      this.HandleInvalidRowExceptionEventArgs(args, (IInvalidRowExceptionEventArgs) exceptionEventArgs2);
    }

    protected internal virtual bool RaiseValidateNode(int rowHandle, object value)
    {
      if (this.DataControl == null)
        return true;
      TreeListNodeValidationEventArgs validationEventArgs1 = new TreeListNodeValidationEventArgs(value, rowHandle, this);
      validationEventArgs1.RoutedEvent = TreeListView.ValidateNodeEvent;
      TreeListNodeValidationEventArgs validationEventArgs2 = validationEventArgs1;
      try
      {
        this.RaiseEvent((RoutedEventArgs) validationEventArgs2);
      }
      catch (Exception ex)
      {
        this.DataControl.SetRowStateError(rowHandle, (RowValidationError) new GridRowValidationError((object) ex.Message, ex, ErrorType.Default, rowHandle));
        throw ex;
      }
      if (validationEventArgs2.IsValid)
      {
        this.DataControl.SetRowStateError(rowHandle, (RowValidationError) null);
        return validationEventArgs2.IsValid;
      }
      string message = validationEventArgs2.ErrorContent != null ? validationEventArgs2.ErrorContent.ToString() : string.Empty;
      this.DataControl.SetRowStateError(rowHandle, (RowValidationError) new GridRowValidationError((object) message, (Exception) null, ErrorType.Default, rowHandle));
      throw new WarningException(message);
    }

    protected internal virtual void ChildrenPropertyUpdate()
    {
      if (this.TreeDerivationMode != TreeDerivationMode.ChildNodesSelector || this.ChildNodesSelector != null)
        return;
      this.DoRefresh();
      this.ForceAutoExpandAllNodes();
    }

    protected internal virtual void OnChildNodesSelectorChanged()
    {
      if (this.TreeDerivationMode != TreeDerivationMode.ChildNodesSelector)
        return;
      this.DoRefresh();
      this.ForceAutoExpandAllNodes();
    }

    protected override bool CanSortDataColumnInfo(DataColumnInfo columnInfo)
    {
      if (TreeListDataProvider.IsUnitypeColumn(columnInfo))
        return true;
      return base.CanSortDataColumnInfo(columnInfo);
    }

    protected internal RowMarginControl FindRowMarginControl(DependencyObject obj)
    {
      for (DependencyObject dependencyObject = obj; dependencyObject != null && DataViewBase.GetRowHandle(dependencyObject) == null; dependencyObject = LayoutHelper.GetParent(dependencyObject, false))
      {
        if (dependencyObject is RowMarginControl)
          return (RowMarginControl) dependencyObject;
      }
      return (RowMarginControl) null;
    }

    internal override DataControlPopupMenu CreatePopupMenu()
    {
      return (DataControlPopupMenu) new TreeListPopupMenu(this);
    }

    internal override FrameworkElement CreateRowElement(RowData rowData)
    {
      return this.TreeListViewBehavior.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new RowControl(rowData)), (Func<FrameworkElement>) (() => (FrameworkElement) new GridRow()), DevExpress.Xpf.Grid.UseLightweightTemplates.Row);
    }

    protected internal override object GetGroupDisplayValue(int rowHandle)
    {
      throw new NotImplementedException();
    }

    protected internal override string GetGroupRowDisplayText(int rowHandle)
    {
      throw new NotImplementedException();
    }

    protected internal override string GetGroupRowHeaderCaption(int rowHandle)
    {
      throw new NotImplementedException();
    }

    protected internal override GroupTextHighlightingProperties GetGroupHighlightingProperties(int rowHandle)
    {
      throw new NotImplementedException();
    }

    internal override DependencyProperty GetFocusedColumnProperty()
    {
      return TreeListView.FocusedColumnProperty;
    }

    protected override void SetVisibleColumns(IList<ColumnBase> columns)
    {
      this.VisibleColumns = columns;
    }

    protected internal void CheckFocusedNodeOnCollapse(TreeListNode treeListNode)
    {
      if (this.FocusedNode == null || !this.FocusedNode.IsDescendantOf(treeListNode))
        return;
      if (this.RestoreFocusOnExpand)
      {
        this.FocusedNodeSaveLocker.Lock();
        if (this.FocusedNodeSave == null)
          this.FocusedNodeSave = this.FocusedNode;
      }
      this.FocusedNode = treeListNode;
      this.FocusedNodeSaveLocker.Unlock();
    }

    protected internal void CheckFocusedNodeOnExpand(TreeListNode treeListNode)
    {
      if (!this.RestoreFocusOnExpand || this.FocusedNodeSave == null || (this.FocusedNode == this.FocusedNodeSave || !this.FocusedNodeSave.IsDescendantOf(treeListNode)))
        return;
      this.FocusedNodeSaveLocker.Lock();
      this.FocusedNode = this.FindTopVisibleParentNode(this.FocusedNodeSave, treeListNode);
      this.FocusedNodeSaveLocker.Unlock();
      if (this.FocusedNode != this.FocusedNodeSave)
        return;
      this.ClearFocusedNodeSave();
    }

    private TreeListNode FindTopVisibleParentNode(TreeListNode startNode, TreeListNode stopNode)
    {
      TreeListNode treeListNode1 = startNode;
      TreeListNode treeListNode2 = (TreeListNode) null;
      for (; treeListNode1 != null && treeListNode1 != stopNode; treeListNode1 = treeListNode1.ParentNode)
      {
        if (!treeListNode1.ParentNode.IsExpanded)
          treeListNode2 = (TreeListNode) null;
        else if (treeListNode2 == null)
          treeListNode2 = treeListNode1;
      }
      return treeListNode2;
    }

    internal override DataController GetDataControllerForUnboundColumnsCore()
    {
      return (DataController) null;
    }

    protected internal void OnDataSourceChanged()
    {
      if (this.DataControl != null)
        this.DataControl.PopulateColumnsIfNeeded((DataProviderBase) null);
      this.ForceAutoExpandAllNodes();
    }

    internal void ForceAutoExpandAllNodes()
    {
      if (!this.AutoExpandAllNodes || !this.TreeListDataProvider.IsReady || this.IsLoading)
        return;
      this.ExpandAllNodes();
    }

    protected internal override void ResetHeadersChildrenCache()
    {
      if (this.DataControl.AutomationPeer == null)
        return;
      this.DataControl.AutomationPeer.ResetHeadersChildrenCache();
    }

    protected internal virtual void OnFilterModeChanged()
    {
      this.TreeListDataProvider.OnFilterModeChanged();
    }

    private void OnIndentItemChanged()
    {
      this.RebuildVisibleColumns();
      this.UpdateRows();
    }

    protected override ControlTemplate GetRowFocusedRectangleTemplate()
    {
      return this.FocusedRowBorderTemplate;
    }

    protected internal override void ResetDataProvider()
    {
      this.TreeListDataProvider.DataSource = (object) null;
    }

    internal void CalcMinWidth()
    {
      if (this.FixedLeftVisibleColumns.Count > 0 || this.FixedRightVisibleColumns.Count == 0)
        return;
      double num = 0.0;
      foreach (ColumnBase rightVisibleColumn in (IEnumerable<ColumnBase>) this.FixedRightVisibleColumns)
        num += rightVisibleColumn.ActualHeaderWidth;
      GridViewInfo viewInfo = ((TableViewBehavior) this.ViewBehavior).ViewInfo;
      this.ScrollableAreaMinWidth = num + ((this.ShowIndicator ? this.IndicatorHeaderWidth : 0.0) + this.FixedLineWidth + viewInfo.TotalGroupAreaIndent);
    }

    protected internal IEnumerable<TreeListNode> GetNodesFromRowHandles(IEnumerable<int> rowHandles)
    {
      if (rowHandles == null)
        return (IEnumerable<TreeListNode>) null;
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      foreach (int rowHandle in rowHandles)
      {
        TreeListNode nodeByRowHandle = this.GetNodeByRowHandle(rowHandle);
        if (nodeByRowHandle != null)
          treeListNodeList.Add(nodeByRowHandle);
      }
      return (IEnumerable<TreeListNode>) treeListNodeList.ToArray();
    }

    protected internal IEnumerable<int> GetRowHandlesFromNodes(IEnumerable<TreeListNode> nodes)
    {
      if (nodes == null)
        return (IEnumerable<int>) null;
      List<int> intList = new List<int>();
      foreach (TreeListNode node in nodes)
      {
        int rowHandleByNode = this.TreeListDataProvider.GetRowHandleByNode(node);
        if (rowHandleByNode >= 0)
          intList.Add(rowHandleByNode);
      }
      return (IEnumerable<int>) intList;
    }

    protected internal override DevExpress.Xpf.Grid.MultiSelectMode GetSelectionMode()
    {
      return SelectionModeHelper.ConvertToMultiSelectMode((TableViewSelectMode) this.GetValue(TreeListView.MultiSelectModeProperty));
    }

    protected virtual void OnRootValueChanged()
    {
      this.DoRefresh();
      this.OnDataSourceReset();
    }

    protected internal override void OnColumnCollectionChanged(NotifyCollectionChangedEventArgs e)
    {
      this.TreeListDataProvider.OnColumnCollectionChanged(e);
    }

    private void UpdateActualAllowTreeIndentScrolling()
    {
      this.ActualAllowTreeIndentScrolling = this.TreeListViewBehavior.AllowTreeIndentScrolling;
    }

    protected override bool OnVisibleColumnsAssigned(bool changed)
    {
      bool flag = base.OnVisibleColumnsAssigned(changed);
      this.UpdateActualAllowTreeIndentScrolling();
      return flag;
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the treelist view.
    /// </para>
    ///             </summary>
    /// <param name="d">
    /// A <see cref="T:System.Windows.DependencyObject" /> object that is the element contained within the treelist view.
    /// 
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeList.TreeListViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public TreeListViewHitInfo CalcHitInfo(DependencyObject d)
    {
      return TreeListViewHitInfo.CalcHitInfo(d, (ITableView) this);
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the treelist view.
    /// </para>
    ///             </summary>
    /// <param name="hitTestPoint">
    /// A <see cref="T:System.Drawing.Point" /> structure which specifies the test point coordinates relative to the map's top-left corner.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TreeList.TreeListViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public TreeListViewHitInfo CalcHitInfo(Point hitTestPoint)
    {
      return TreeListViewHitInfo.CalcHitInfo(hitTestPoint, (ITableView) this);
    }

    internal override IDataViewHitInfo CalcHitInfoCore(DependencyObject source)
    {
      return (IDataViewHitInfo) this.CalcHitInfo(source);
    }

    /// <summary>
    ///                 <para>Resizes the specified column to the minimum width required to display the column's contents completely.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> that is the column whose width should be optimized.
    /// 
    /// 
    ///           </param>
    public void BestFitColumn(ColumnBase column)
    {
      this.TreeListViewBehavior.BestFitColumn(column);
    }

    /// <summary>
    ///                 <para>Resizes all visible columns to optimally fit their contents.
    /// </para>
    ///             </summary>
    public void BestFitColumns()
    {
      this.TreeListViewBehavior.BestFitColumns();
    }

    /// <summary>
    ///                 <para>Returns the column's optimal (minimum) width required to display its contents completely.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListColumn" /> object that is the treelist column.
    /// 
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Double" /> value that specifies the column's optimal (minimum) width required to display its contents completely.
    /// </returns>
    public double CalcColumnBestFitWidth(ColumnBase column)
    {
      return this.TreeListViewBehavior.CalcColumnBestFitWidthCore(column);
    }

    void ITableView.SetHorizontalViewport(double value)
    {
      this.HorizontalViewport = value;
    }

    void ITableView.SetFixedLeftVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedLeftVisibleColumns = columns;
    }

    void ITableView.SetFixedNoneVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedNoneVisibleColumns = columns;
    }

    void ITableView.SetFixedRightVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedRightVisibleColumns = columns;
    }

    void ITableView.CopyCellsToClipboard(IEnumerable<CellBase> gridCells)
    {
      this.CopyCellsToClipboard((IEnumerable<TreeListCell>) new SimpleEnumerableBridge<TreeListCell, CellBase>(gridCells));
    }

    CellBase ITableView.CreateGridCell(int rowHandle, ColumnBase column)
    {
      return (CellBase) new TreeListCell(rowHandle, column);
    }

    ITableViewHitInfo ITableView.CalcHitInfo(DependencyObject d)
    {
      return (ITableViewHitInfo) TreeListViewHitInfo.CalcHitInfo(d, (ITableView) this);
    }

    void ITableView.SetActualShowIndicator(bool showIndicator)
    {
      this.ActualShowIndicator = showIndicator;
    }

    void ITableView.SetActualIndicatorWidth(double indicatorWidth)
    {
      this.ActualIndicatorWidth = indicatorWidth;
    }

    void ITableView.SetActualExpandDetailHeaderWidth(double expandDetailButtonWidth)
    {
    }

    void ITableView.SetActualDetailMargin(Thickness detailMargin)
    {
    }

    void ITableView.SetShowTotalSummaryIndicatorIndent(bool showTotalSummaryIndicatorIndent)
    {
      this.ShowTotalSummaryIndicatorIndent = showTotalSummaryIndicatorIndent;
    }

    void ITableView.SetActualFadeSelectionOnLostFocus(bool fadeSelectionOnLostFocus)
    {
      this.ActualFadeSelectionOnLostFocus = fadeSelectionOnLostFocus;
    }

    void ITableView.RaiseRowDoubleClickEvent(ITableViewHitInfo hitInfo, MouseButton changedButton)
    {
      RowDoubleClickEventArgs doubleClickEventArgs = new RowDoubleClickEventArgs((GridViewHitInfoBase) hitInfo, changedButton, (DataViewBase) this);
      doubleClickEventArgs.RoutedEvent = TreeListView.RowDoubleClickEvent;
      this.RaiseEvent((RoutedEventArgs) doubleClickEventArgs);
    }

    void ITableView.SetExpandColumnPosition(ColumnPosition position)
    {
    }

    void ITableView.RaiseCustomCellAppearance(CustomCellAppearanceEventArgs args)
    {
      this.RaiseCustomCellAppearance(args);
    }

    void ITableView.RaiseCustomRowAppearance(CustomRowAppearanceEventArgs args)
    {
      this.RaiseCustomRowAppearance(args);
    }

    void ITableView.RaiseEditFormShowing(EditFormShowingEventArgs args)
    {
      this.RaiseEditFormShowing(args);
    }

    internal override bool RaiseShowingEditor(int rowHanlde, ColumnBase columnBase)
    {
      TreeListShowingEditorEventArgs e = new TreeListShowingEditorEventArgs(this, rowHanlde, columnBase);
      this.RaiseShowingEditor(e);
      return !e.Cancel;
    }

    protected internal virtual void RaiseShowingEditor(TreeListShowingEditorEventArgs e)
    {
      this.RaiseEvent((RoutedEventArgs) e);
    }

    internal override void RaiseShownEditor(int rowHandle, ColumnBase column, IBaseEdit editCore)
    {
      this.RaiseShownEditor(new TreeListEditorEventArgs(this, rowHandle, column, editCore));
    }

    protected internal virtual void RaiseShownEditor(TreeListEditorEventArgs e)
    {
      this.RaiseEvent((RoutedEventArgs) e);
    }

    internal override void RaiseHiddenEditor(int rowHandle, ColumnBase column, IBaseEdit editCore)
    {
      TreeListEditorEventArgs e = new TreeListEditorEventArgs(this, rowHandle, column, editCore);
      e.RoutedEvent = TreeListView.HiddenEditorEvent;
      this.RaiseHiddenEditor(e);
    }

    protected internal virtual void OnStartSorting()
    {
      this.RaiseEvent(new RoutedEventArgs(TreeListView.StartSortingEvent));
    }

    protected internal virtual void OnEndSorting()
    {
      this.RaiseEvent(new RoutedEventArgs(TreeListView.EndSortingEvent));
    }

    protected internal virtual void RaiseHiddenEditor(TreeListEditorEventArgs e)
    {
      this.RaiseEvent((RoutedEventArgs) e);
    }

    protected internal override void RaiseValidateCell(GridRowValidationEventArgs e)
    {
      if (this.ValidateCell == null)
        return;
      this.ValidateCell((object) this, (TreeListCellValidationEventArgs) e);
    }

    protected internal override bool SupportValidateCell()
    {
      return this.ValidateCell != null;
    }

    internal override void RaiseCellValueChanging(int rowHandle, ColumnBase column, object value, object oldValue)
    {
      TreeListCellValueChangedEventArgs changedEventArgs = new TreeListCellValueChangedEventArgs(this.GetNodeByRowHandle(rowHandle), column, value, oldValue);
      changedEventArgs.RoutedEvent = TreeListView.CellValueChangingEvent;
      this.RaiseEvent((RoutedEventArgs) changedEventArgs);
    }

    internal override void RaiseCellValueChanged(int rowHandle, ColumnBase column, object newValue, object oldValue)
    {
      TreeListCellValueChangedEventArgs changedEventArgs = new TreeListCellValueChangedEventArgs(this.GetNodeByRowHandle(rowHandle), column, newValue, oldValue);
      changedEventArgs.RoutedEvent = TreeListView.CellValueChangedEvent;
      this.RaiseEvent((RoutedEventArgs) changedEventArgs);
    }

    protected internal override void RaiseCustomScrollAnimation(CustomScrollAnimationEventArgs e)
    {
      e.RoutedEvent = TreeListView.CustomScrollAnimationEvent;
      base.RaiseCustomScrollAnimation(e);
    }

    internal override RowValidationError CreateCellValidationError(object errorContent, Exception exception, ErrorType errorType, int rowHandle, ColumnBase column)
    {
      return (RowValidationError) new TreeListCellValidationError(errorContent, exception, errorType, rowHandle, this.GetNodeByRowHandle(rowHandle), column);
    }

    internal override GridRowValidationEventArgs CreateCellValidationEventArgs(object source, object value, int rowHandle, ColumnBase column)
    {
      return (GridRowValidationEventArgs) new TreeListCellValidationEventArgs(source, value, rowHandle, this, column);
    }

    internal override BaseValidationError CreateCellValidationError(object errorContent, ErrorType errorType, int rowHandle, ColumnBase column)
    {
      return (BaseValidationError) new TreeListCellValidationError(errorContent, (Exception) null, errorType, rowHandle, this.GetNodeByRowHandle(rowHandle), column);
    }

    internal override BaseValidationError CreateRowValidationError(object errorContent, ErrorType errorType, int rowHandle)
    {
      return (BaseValidationError) new TreeListNodeValidationError(errorContent, (Exception) null, errorType, rowHandle, this.GetNodeByRowHandle(rowHandle));
    }

    internal override string RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string displayText)
    {
      if (!rowHandle.HasValue)
        return displayText;
      return this.RaiseCustomColumnDisplayText(this.GetNodeByRowHandle(rowHandle.Value), column, value, displayText);
    }

    internal override bool? RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string originalDisplayText, out string displayText)
    {
      displayText = this.RaiseCustomDisplayText(rowHandle, listSourceIndex, column, value, originalDisplayText);
      if (this.customColumnDisplayText == null)
        return new bool?(false);
      if (this.customColumnDisplayTextEventArgs.ShowAsNullText)
        return new bool?();
      return new bool?(true);
    }

    /// <summary>
    ///                 <para>Prevents selection updates until the <see cref="M:DevExpress.Xpf.Grid.TreeListView.EndSelection" /> method is called.
    /// </para>
    ///             </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.BeginSelection method instead")]
    [Browsable(false)]
    public void BeginSelection()
    {
      this.SelectionStrategy.BeginSelection();
    }

    /// <summary>
    ///                 <para>Enables selection updates after calling the <see cref="M:DevExpress.Xpf.Grid.TreeListView.BeginSelection" /> method, and forces an immediate update.
    /// </para>
    ///             </summary>
    [Obsolete("Use the DataControlBase.EndSelection method instead")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void EndSelection()
    {
      this.SelectionStrategy.EndSelection();
    }

    /// <summary>
    ///                 <para>Selects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value which specifies the handle of the node to select.
    /// 
    ///           </param>
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectItem method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SelectNode(int rowHandle)
    {
      this.SelectionStrategy.SelectRow(rowHandle);
    }

    /// <summary>
    ///                 <para>Selects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to select.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectItem method instead")]
    public void SelectNode(TreeListNode node)
    {
      this.SelectNodeCore(node);
    }

    internal void SelectNodeCore(TreeListNode node)
    {
      this.SelectionStrategy.SelectRow(this.TreeListDataProvider.GetRowHandleByNode(node));
    }

    /// <summary>
    ///                 <para>Unselects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the node by its handle.
    /// 
    ///           </param>
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.UnselectItem method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void UnselectNode(int rowHandle)
    {
      this.SelectionStrategy.UnselectRow(rowHandle);
    }

    internal void UnselectNodeCore(TreeListNode node)
    {
      this.SelectionStrategy.UnselectRow(this.TreeListDataProvider.GetRowHandleByNode(node));
    }

    /// <summary>
    ///                 <para>Unselects the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node to unselect.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.UnselectItem method instead")]
    [Browsable(false)]
    public void UnselectNode(TreeListNode node)
    {
      this.UnselectNodeCore(node);
    }

    /// <summary>
    ///                 <para>Selects multiple nodes, while preserving the current selection (if any).
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
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectRange method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void SelectRange(int startRowHandle, int endRowHandle)
    {
      this.SelectRangeCore(startRowHandle, endRowHandle);
    }

    /// <summary>
    ///                 <para>Selects multiple nodes, while preserving the current selection (if any).
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node at which the selection starts.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node at which the selection ends.
    /// 
    ///           </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.SelectRange method instead")]
    public void SelectRange(TreeListNode startNode, TreeListNode endNode)
    {
      this.SelectRangeCore(startNode, endNode);
    }

    internal void SelectRangeCore(TreeListNode startNode, TreeListNode endNode)
    {
      this.SelectRangeCore(this.TreeListDataProvider.GetRowHandleByNode(startNode), this.TreeListDataProvider.GetRowHandleByNode(endNode));
    }

    /// <summary>
    ///                 <para>Unselects any selected nodes.
    /// </para>
    ///             </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [Obsolete("Use the DataControlBase.UnselectAll method instead")]
    public void ClearSelection()
    {
      this.SelectionStrategy.ClearSelection();
    }

    /// <summary>
    ///                 <para>Reloads child nodes of the specified node.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value which specifies the handle of the node whose child nodes are to be reloaded.
    /// 
    ///           </param>
    public void ReloadChildNodes(int rowHandle)
    {
      this.ReloadChildNodes(this.GetNodeByRowHandle(rowHandle));
    }

    /// <summary>
    ///                 <para>Reloads child nodes of the specified node.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node whose child nodes are to be reloaded.
    /// 
    ///           </param>
    public void ReloadChildNodes(TreeListNode node)
    {
      this.TreeListDataProvider.ReloadChildNodes(node);
    }

    protected internal override GridSelectionChangedEventArgs CreateSelectionChangedEventArgs(DevExpress.Data.SelectionChangedEventArgs e)
    {
      return (GridSelectionChangedEventArgs) new TreeListSelectionChangedEventArgs(this, e.Action, e.ControllerRow);
    }

    protected internal override void RaiseSelectionChanged(GridSelectionChangedEventArgs e)
    {
      e.RoutedEvent = TreeListView.SelectionChangedEvent;
      this.RaiseEvent((RoutedEventArgs) e);
    }

    /// <summary>
    ///                 <para>Selects the cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row where the cell is located.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column which contains the cell.
    /// 
    /// 
    ///           </param>
    public void SelectCell(int rowHandle, ColumnBase column)
    {
      this.TreeListViewBehavior.SelectCell(rowHandle, column);
    }

    /// <summary>
    ///                 <para>Selects the cell.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the node which contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column which contains the cell.
    /// 
    ///           </param>
    public void SelectCell(TreeListNode node, ColumnBase column)
    {
      this.SelectCell(node.RowHandle, column);
    }

    /// <summary>
    ///                 <para>Unselects the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row, containing the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column, containing the cell.
    /// 
    ///           </param>
    public void UnselectCell(int rowHandle, ColumnBase column)
    {
      this.TreeListViewBehavior.UnselectCell(rowHandle, column);
    }

    /// <summary>
    ///                 <para>Unselects the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="node">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the node, containing the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column, containing the cell.
    /// 
    ///           </param>
    public void UnselectCell(TreeListNode node, ColumnBase column)
    {
      this.UnselectCell(node.RowHandle, column);
    }

    /// <summary>
    ///                 <para>Selects multiple cells.
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    public void SelectCells(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
    {
      this.TreeListViewBehavior.SelectCells(startRowHandle, startColumn, endRowHandle, endColumn);
    }

    /// <summary>
    ///                 <para>Selects multiple cells.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the node containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the node containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    public void SelectCells(TreeListNode startNode, ColumnBase startColumn, TreeListNode endNode, ColumnBase endColumn)
    {
      this.SelectCells(startNode.RowHandle, startColumn, endNode.RowHandle, endColumn);
    }

    /// <summary>
    ///                 <para>Unselects the specified cells.
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    public void UnselectCells(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
    {
      this.TreeListViewBehavior.UnselectCells(startRowHandle, startColumn, endRowHandle, endColumn);
    }

    /// <summary>
    ///                 <para>Unselects the specified cells.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    public void UnselectCells(TreeListNode startNode, ColumnBase startColumn, TreeListNode endNode, ColumnBase endColumn)
    {
      this.UnselectCells(startNode.RowHandle, startColumn, endNode.RowHandle, endColumn);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified cell is selected.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column which contains the cell.
    /// 
    /// 
    ///           </param>
    /// <returns><b>true</b>, if the specified cell is selected; otherwise, <b>false</b>.
    /// </returns>
    public bool IsCellSelected(int rowHandle, ColumnBase column)
    {
      return this.TreeListViewBehavior.IsCellSelected(rowHandle, column);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified cell is selected.
    /// </para>
    ///             </summary>
    /// <param name="nodes">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node which contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column which contains the cell.
    /// 
    ///           </param>
    /// <returns><b>true</b>, if the specified cell is selected; otherwise, <b>false</b>.
    /// </returns>
    public bool IsCellSelected(TreeListNode nodes, ColumnBase column)
    {
      return this.IsCellSelected(nodes.RowHandle, column);
    }

    /// <summary>
    ///                 <para>Returns the selected data cells.
    /// </para>
    ///             </summary>
    /// <returns>The list of TreeListCell objects that contain cell coordinates (node and column).
    /// </returns>
    public IList<TreeListCell> GetSelectedCells()
    {
      return (IList<TreeListCell>) new SimpleBridgeList<TreeListCell, CellBase>(this.TreeListViewBehavior.GetSelectedCells(), (Func<CellBase, TreeListCell>) null, (Func<TreeListCell, CellBase>) null);
    }

    protected virtual void RaiseCustomCellAppearance(CustomCellAppearanceEventArgs e)
    {
      EventHandler<CustomCellAppearanceEventArgs> eventHandler = this.CustomCellAppearance;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected virtual void RaiseCustomRowAppearance(CustomRowAppearanceEventArgs e)
    {
      EventHandler<CustomRowAppearanceEventArgs> eventHandler = this.CustomRowAppearance;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    ///                 <para>Adds a format condition to the format condition collection.
    /// </para>
    ///             </summary>
    /// <param name="formatCondition">
    /// A <see cref="T:DevExpress.Xpf.Grid.FormatConditionBase" /> object that is the new format condition.
    /// 
    ///           </param>
    public void AddFormatCondition(FormatConditionBase formatCondition)
    {
      this.TreeListViewBehavior.AddFormatConditionCore(formatCondition);
    }

    /// <summary>
    ///                 <para>Invokes the format condition dialog.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column to which you want to add a formatting rule.
    /// 
    ///           </param>
    /// <param name="dialogKind">
    /// A <see cref="T:DevExpress.Xpf.Core.ConditionalFormatting.FormatConditionDialogType" /> object that is the dialog type.
    /// 
    ///           </param>
    public void ShowFormatConditionDialog(ColumnBase column, FormatConditionDialogType dialogKind)
    {
      this.TreeListViewBehavior.ShowFormatConditionDialogCore(column, dialogKind);
    }

    /// <summary>
    ///                 <para>Removes all format conditions.
    /// </para>
    ///             </summary>
    public void ClearFormatConditionsFromAllColumns()
    {
      this.TreeListViewBehavior.ClearFormatConditionsFromAllColumnsCore();
    }

    /// <summary>
    ///                 <para>Clears format conditions for the specified column.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column for which format conditions should be removed.
    /// 
    ///           </param>
    public void ClearFormatConditionsFromColumn(ColumnBase column)
    {
      this.TreeListViewBehavior.ClearFormatConditionsFromColumnCore(column);
    }

    /// <summary>
    ///                 <para>Shows the conditional formatting rules manager.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that is the column for which to invoke the conditional formatting manager.
    /// 
    ///           </param>
    public void ShowConditionalFormattingManager(ColumnBase column)
    {
      this.TreeListViewBehavior.ShowConditionalFormattingManagerCore(column);
    }

    protected virtual void OnDeserializeCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
    {
      if (!(e.CollectionName == "FormatConditions"))
        return;
      this.TreeListViewBehavior.OnDeserializeCreateFormatCondition(e);
    }

    protected override void OnDeserializeStart(StartDeserializingEventArgs e)
    {
      base.OnDeserializeStart(e);
      this.TreeListViewBehavior.OnDeserializeFormatConditionsStart();
    }

    protected override void OnDeserializeEnd(EndDeserializingEventArgs e)
    {
      base.OnDeserializeEnd(e);
      this.TreeListViewBehavior.OnDeserializeFormatConditionsEnd();
    }

    internal override bool CanCopyRows()
    {
      return this.ActualClipboardCopyAllowed && this.NavigationStyle != GridViewNavigationStyle.None && (!this.IsInvalidFocusedRowHandle || this.DataControl.HasSelectedItems()) && this.ActiveEditor == null;
    }

    [Browsable(false)]
    [Obsolete("Use the TreeListControl.CopyRowsToClipboard method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CopyRowsToClipboard(IEnumerable<TreeListNode> nodes)
    {
      this.CopyRowsToClipboardCore(nodes);
    }

    internal void CopyRowsToClipboardCore(IEnumerable<TreeListNode> nodes)
    {
      this.ClipboardController.CopyRowsToClipboard(this.GetRowHandlesFromNodes(nodes));
    }

    /// <summary>
    ///                 <para>Copies the values displayed within the specified range of nodes, to the clipboard.
    /// </para>
    ///             </summary>
    /// <param name="startNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the first node in the range.
    /// 
    ///           </param>
    /// <param name="endNode">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the last node in the range.
    /// 
    ///           </param>
    [Browsable(false)]
    [Obsolete("Use the TreeListControl.CopyRangeToClipboard method instead")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void CopyRangeToClipboard(TreeListNode startNode, TreeListNode endNode)
    {
      this.CopyRangeToClipboardCore(startNode, endNode);
    }

    internal void CopyRangeToClipboardCore(TreeListNode startNode, TreeListNode endNode)
    {
      this.ClipboardController.CopyRangeToClipboard(this.TreeListDataProvider.GetRowHandleByNode(startNode), this.TreeListDataProvider.GetRowHandleByNode(endNode));
    }

    protected internal override bool RaiseCopyingToClipboard(CopyingToClipboardEventArgsBase e)
    {
      e.RoutedEvent = TreeListView.CopyingToClipboardEvent;
      this.RaiseEvent((RoutedEventArgs) e);
      return e.Handled;
    }

    /// <summary>
    ///                 <para>Copies the values displayed within selected cells to the clipboard.
    /// </para>
    ///             </summary>
    public void CopySelectedCellsToClipboard()
    {
      if (this.DataControl.SelectionMode == DevExpress.Xpf.Grid.MultiSelectMode.Row || this.DataControl.SelectionMode == DevExpress.Xpf.Grid.MultiSelectMode.MultipleRow)
        this.DataControl.CopySelectedItemsToClipboard();
      else
        this.ClipboardController.CopyCellsToClipboard((IEnumerable<TreeListCell>) this.GetSelectedCells());
    }

    public void CopyCellsToClipboard(IEnumerable<TreeListCell> cells)
    {
      this.ClipboardController.CopyCellsToClipboard(cells);
    }

    /// <summary>
    ///                 <para>Copies the values displayed within the specified block of cells to the clipboard.
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the starting point.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the starting point.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> object that represents the column containing the cell that identifies the end point.
    /// 
    ///           </param>
    public void CopyCellsToClipboard(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn)
    {
      this.TreeListViewBehavior.CopyCellsToClipboard(startRowHandle, startColumn, endRowHandle, endColumn);
    }

    protected virtual IClipboardManager<ColumnWrapper, TreeListNodeWrapper> CreateClipboardManager()
    {
      this.ClipboardHelperManager = this.DataControl.BandsCore.Count > 0 ? (TreeListViewClipboardHelper) new BandedTreeListViewClipboardHelper(this, ExportTarget.Xlsx) : new TreeListViewClipboardHelper(this, ExportTarget.Xlsx);
      return (IClipboardManager<ColumnWrapper, TreeListNodeWrapper>) PrintHelper.ClipboardExportManagerInstance(typeof (ColumnWrapper), typeof (TreeListNodeWrapper), (object) this.ClipboardHelperManager);
    }

    protected internal override bool SetDataAwareClipboardData()
    {
      try
      {
        this.SetActualClipboardOptions(this.OptionsClipboard);
        if (this.ClipboardManager != null && this.ClipboardHelperManager != null && !this.ClipboardHelperManager.CanCopyToClipboard())
          return false;
        System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject();
        this.ClipboardHelperManager.ResetNodesProvider();
        this.ClipboardManager.AssignOptions(this.OptionsClipboard);
        this.ClipboardManager.SetClipboardData(dataObject);
        if (((IEnumerable<string>) dataObject.GetFormats()).Count<string>() == 0)
          return false;
        System.Windows.Clipboard.SetDataObject((object) dataObject);
        return true;
      }
      catch
      {
        return false;
      }
    }

    protected override IRootDataNode CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      return GridPrintingHelper.CreatePrintingTreeNode((ITableView) this, usablePageSize, (MasterDetailPrintInfo) null, (ItemsGenerationStrategyBase) null);
    }

    protected override IVisualTreeWalker GetCustomVisualTreeWalker()
    {
      return (IVisualTreeWalker) null;
    }

    protected override void PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters)
    {
      bool flag = this.PrintColumnHeaders && this.ShowColumnHeaders;
      GridPrintingHelper.UpdatePageBricks(pageBrickEnumerator, brickUpdaters, !flag, this.PrintTotalSummary && this.ShowTotalSummary || this.PrintFixedTotalSummary && this.ShowFixedTotalSummary);
    }

    protected override bool GetCanCreateRootNodeAsync()
    {
      return false;
    }

    protected override void CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      throw new NotImplementedException();
    }

    protected override void AddCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler)
    {
    }

    protected override void RemoveCreateRootNodeCompletedEvent(EventHandler<ScalarOperationCompletedEventArgs<IRootDataNode>> eventHandler)
    {
    }

    protected internal override PrintingDataTreeBuilderBase CreatePrintingDataTreeBuilder(double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy, MasterDetailPrintInfo masterDetailPrintInfo, BandsLayoutBase bandsLayout)
    {
      return (PrintingDataTreeBuilderBase) new TreeListPrintingDataTreeBuilder(this, totalHeaderWidth, bandsLayout);
    }

    protected internal override DataTemplate GetPrintRowTemplate()
    {
      return this.PrintRowTemplate;
    }

    protected override void OnCustomShouldSerializeProperty(CustomShouldSerializePropertyEventArgs e)
    {
      base.OnCustomShouldSerializeProperty(e);
      if (e.DependencyProperty != TreeListView.TreeDerivationModeProperty)
        return;
      e.CustomShouldSerialize = new bool?(true);
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
    public bool ShouldSerializeColumnChooserBandsSortOrderComparer(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    private void ScrollBarAnnotationModeChanged()
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
      this.UpdateFilterGrid();
    }

    ScrollBarAnnotationsAppearance ITableView.CreateDefaultScrollBarAnnotationsAppearance()
    {
      return (ScrollBarAnnotationsAppearance) new ScrollBarAnnotationsAppearanceDefault((DataViewBase) this);
    }

    void ITableView.ScrollBarAnnotationInfoRangeChanged()
    {
      this.RaisePropertyChanged("ScrollBarAnnotationInfoRange");
    }

    private void ScrollBarAnnotationsAppearanceChanged(bool generation)
    {
      this._scrollBarAnnotationsManager = (ScrollBarAnnotationsManager) null;
      ((ITableView) this).ScrollBarAnnotationsManager.GridLoaded = true;
      if (this.ScrollBarAnnotationsAppearance != null)
        this.ScrollBarAnnotationsAppearance.Owner = new WeakReference((object) this);
      if (!generation)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    protected internal override void OnSelectionChanged(DevExpress.Data.SelectionChangedEventArgs e)
    {
      base.OnSelectionChanged(e);
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.Selected, false);
    }

    protected internal override Func<int, bool> CreateFilterFitPredicate()
    {
      if (this.SearchControl == null || object.ReferenceEquals((object) this.SearchControl.FilterCriteria, (object) null))
        return (Func<int, bool>) null;
      return this.CreateFilterFitPredicate(this.SearchControl.FilterCriteria);
    }

    protected internal override Func<int, bool> CreateFilterFitPredicate(CriteriaOperator criteria)
    {
      Func<object, bool> func = this.TreeListDataProvider.CreateFilterFitPredicate(criteria);
      if (func == null)
        return (Func<int, bool>) null;
      return (Func<int, bool>) (rowHandle => func((object) this.TreeListDataProvider.GetNodeByRowHandle(rowHandle)));
    }

    protected override void ResetIncrementalSearch()
    {
      base.ResetIncrementalSearch();
      if (this.ActualIncrementalSearchMode != IncrementalSearchMode.Enabled)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.SearchResult, false);
    }

    protected override void UpdateAfterIncrementalSearch()
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.SearchResult, false);
    }

    protected internal override void UpdateFilterGrid()
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.SearchResult, false);
      this.UpdateSearchResult(true);
    }

    internal override void CurrentRowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationChanged(changedType, newHandle, oldRowHandle);
      base.CurrentRowChanged(changedType, newHandle, oldRowHandle);
    }

    internal override bool NeedWatchRowChanged()
    {
      if (!base.NeedWatchRowChanged())
      {
        if (!((ITableView) this).ScrollBarAnnotationModeActual.HasAnyFlag((Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.InvalidCells, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.InvalidRows, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.SearchResult, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.Custom))
        {
          if (this.SearchControl != null)
            return this.ShowSearchPanelNavigationButtons;
          return false;
        }
      }
      return true;
    }

    protected internal override void UpdateScrollBarAnnotations()
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    [SpecialName]
    bool ITableView.get_PrintTotalSummary()
    {
      return this.PrintTotalSummary;
    }

    [SpecialName]
    bool ITableView.get_PrintFixedTotalSummary()
    {
      return this.PrintFixedTotalSummary;
    }

    [SpecialName]
    Style ITableView.get_PrintCellStyle()
    {
      return this.PrintCellStyle;
    }

    [SpecialName]
    Style ITableView.get_PrintTotalSummaryStyle()
    {
      return this.PrintTotalSummaryStyle;
    }

    [SpecialName]
    Style ITableView.get_PrintFixedTotalSummaryStyle()
    {
      return this.PrintFixedTotalSummaryStyle;
    }

    [SpecialName]
    Style ITableView.get_PrintRowIndentStyle()
    {
      return this.PrintRowIndentStyle;
    }
  }
}
