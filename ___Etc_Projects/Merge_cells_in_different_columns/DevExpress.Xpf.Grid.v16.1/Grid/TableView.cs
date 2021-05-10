// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TableView
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Core;
using DevExpress.Export;
using DevExpress.Mvvm;
using DevExpress.Mvvm.Native;
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
using DevExpress.Xpf.Grid.EditForm;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraExport.Helpers;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections;
using System.Collections.Generic;
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
  ///                 <para>Represents a View that displays data in a tabular form.
  /// </para>
  ///             </summary>
  public class TableView : GridViewBase, IGridViewFactory<ColumnWrapper, RowBaseWrapper>, IGridViewFactoryBase, ITableView, IFormatsOwner, IGroupSummaryDisplayMode, IDetailElement<DataViewBase>
  {
    internal readonly Locker CellMergeLocker = new Locker();
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowCellMergeProperty = DependencyPropertyManager.Register("AllowCellMerge", typeof (bool), typeof (TableView), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnAllowCellMergeChanged())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowConditionalFormattingMenuProperty = TableViewBehavior.RegisterAllowConditionalFormattingMenuProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowConditionalFormattingManagerProperty = TableViewBehavior.RegisterAllowConditionalFormattingManagerProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedFormatsProperty = TableViewBehavior.RegisterPredefinedFormatsProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedColorScaleFormatsProperty = TableViewBehavior.RegisterPredefinedColorScaleFormatsProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedDataBarFormatsProperty = TableViewBehavior.RegisterPredefinedDataBarFormatsProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PredefinedIconSetFormatsProperty = TableViewBehavior.RegisterPredefinedIconSetFormatsProperty(typeof (TableView));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FormatConditionDialogServiceTemplateProperty = AssignableServiceHelper2<TableView, IDialogService>.RegisterServiceTemplateProperty("FormatConditionDialogServiceTemplate");
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ConditionalFormattingManagerServiceTemplateProperty = AssignableServiceHelper2<TableView, IDialogService>.RegisterServiceTemplateProperty("ConditionalFormattingManagerServiceTemplate");
    [IgnoreDependencyPropertiesConsistencyChecker]
    private static readonly DependencyProperty FormatConditionsItemsAttachedBehaviorProperty = TableViewBehavior.RegisterFormatConditionsItemsAttachedBehaviorProperty<TableView>();
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionGeneratorTemplateProperty = TableViewBehavior.RegisterFormatConditionGeneratorTemplateProperty<TableView>(TableView.FormatConditionsItemsAttachedBehaviorProperty);
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionGeneratorTemplateSelectorProperty = TableViewBehavior.RegisterFormatConditionGeneratorTemplateSelectorProperty<TableView>(TableView.FormatConditionsItemsAttachedBehaviorProperty);
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FormatConditionsSourceProperty = TableViewBehavior.RegisterFormatConditionsSourceProperty<TableView>(TableView.FormatConditionsItemsAttachedBehaviorProperty, TableView.FormatConditionGeneratorTemplateProperty, TableView.FormatConditionGeneratorTemplateSelectorProperty);
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormDialogServiceTemplateProperty = AssignableServiceHelper2<TableView, IDialogService>.RegisterServiceTemplateProperty("EditFormDialogServiceTemplate");
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormColumnCountProperty = DependencyProperty.Register("EditFormColumnCount", typeof (int), typeof (TableView), new PropertyMetadata((object) 3, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormPostModeProperty = DependencyProperty.Register("EditFormPostMode", typeof (EditFormPostMode), typeof (TableView), new PropertyMetadata((object) EditFormPostMode.Cached, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormShowModeProperty = DependencyProperty.Register("EditFormShowMode", typeof (EditFormShowMode), typeof (TableView), new PropertyMetadata((object) EditFormShowMode.None, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnF2KeyProperty = DependencyProperty.Register("ShowEditFormOnF2Key", typeof (bool), typeof (TableView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnEnterKeyProperty = DependencyProperty.Register("ShowEditFormOnEnterKey", typeof (bool), typeof (TableView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormOnDoubleClickProperty = DependencyProperty.Register("ShowEditFormOnDoubleClick", typeof (bool), typeof (TableView), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormPostConfirmationProperty = DependencyProperty.Register("EditFormPostConfirmation", typeof (PostConfirmationMode), typeof (TableView), new PropertyMetadata((object) PostConfirmationMode.YesNoCancel, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowEditFormUpdateCancelButtonsProperty = DependencyProperty.Register("ShowEditFormUpdateCancelButtons", typeof (bool), typeof (TableView), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty EditFormTemplateProperty = DependencyProperty.Register("EditFormTemplate", typeof (DataTemplate), typeof (TableView), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).HideEditForm())));
    internal const string NewItemRowDataPropertyName = "NewItemRowData";
    /// <summary>
    ///                 <para>Return value: DX$CheckboxSelectorColumn
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public const string CheckBoxSelectorColumnName = "DX$CheckboxSelectorColumn";
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
    private GridViewClipboardHelper clipboardHelperManager;
    private IClipboardManager<ColumnWrapper, RowBaseWrapper> _clipboardManager;
    private CellMergeEventArgs cellMergeEventArgs;
    private bool isEditorOpen;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty NewItemRowPositionProperty;
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
    public static readonly DependencyProperty UseGroupShadowIndentProperty;
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
    /// 
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
    public static readonly DependencyProperty ExpandDetailButtonWidthProperty;
    private static readonly DependencyPropertyKey ActualExpandDetailButtonWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualExpandDetailButtonWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DetailMarginProperty;
    private static readonly DependencyPropertyKey ActualDetailMarginPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualDetailMarginProperty;
    private static readonly DependencyPropertyKey ActualExpandDetailHeaderWidthPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualExpandDetailHeaderWidthProperty;
    private static readonly DependencyPropertyKey ExpandColumnPositionPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ExpandColumnPositionProperty;
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
    private static readonly DependencyPropertyKey HasDetailViewsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty HasDetailViewsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowDetailButtonsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowMasterDetailProperty;
    private static readonly DependencyPropertyKey ActualShowDetailButtonsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualShowDetailButtonsProperty;
    private static readonly DependencyPropertyKey IsDetailButtonVisibleBindingContainerPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsDetailButtonVisibleBindingContainerProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent InitNewRowEvent;
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
    public static readonly RoutedEvent RowDoubleClickEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent ShowingGroupFooterEvent;
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
    public static readonly DependencyProperty PrintGroupFooterTemplateProperty;
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
    public static readonly DependencyProperty PrintGroupFootersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowPrintDetailsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowPrintEmptyDetailsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintAllDetailsProperty;
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
    public static readonly DependencyProperty PrintGroupFooterStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintDetailTopIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintDetailBottomIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty LeftGroupAreaIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RightGroupAreaIndentProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty PrintGroupSummaryDisplayModeProperty;
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
    /// <returns> </returns>
    public static readonly DependencyProperty NewItemRowCellStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowFixedGroupsProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryDisplayModeProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupColumnSummaryItemTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupColumnSummaryContentStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty GroupColumnFooterElementStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupBandSummaryContentStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowGroupSummaryCascadeUpdateProperty;
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
    ///                 <para>Gets or sets the style applied to group summary items displayed within a group footer. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <returns>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group summary items.
    /// </returns>
    public static readonly DependencyProperty GroupFooterRowStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupFooterRowTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupFooterRowTemplateSelectorProperty;
    protected static readonly DependencyPropertyKey ActualGroupFooterRowTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupFooterRowTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupFooterSummaryContentStyleProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupFooterSummaryItemTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupFooterSummaryItemTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualGroupFooterSummaryItemTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupFooterSummaryItemTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowGroupFootersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowCheckBoxSelectorInGroupRowProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowCheckBoxSelectorColumnProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CheckBoxSelectorColumnWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CheckBoxSelectorColumnHeaderTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RetainSelectionOnClickOutsideCheckBoxSelectorProperty;
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
    public static readonly DependencyProperty AllowPartialGroupingProperty;
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
    private BindingBase isDetailButtonVisibleBinding;
    private Lazy<BarManagerMenuController> bandMenuControllerValue;
    private NewItemRowPosition actualnewItemRowPositionCore;
    private bool isCheckBoxSelectorColumnVisibleCore;
    private ScrollBarAnnotationsManager _scrollBarAnnotationsManager;

    /// <summary>
    ///                 <para>Gets or sets whether to enable the Cell Merging feature. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable the celll merging feature; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowCellMerge")]
    [XtraSerializableProperty]
    [Category("Options Layout")]
    public bool AllowCellMerge
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowCellMergeProperty);
      }
      set
      {
        this.SetValue(TableView.AllowCellMergeProperty, (object) value);
      }
    }

    internal bool ShouldPrintColumnHeaders
    {
      get
      {
        if (this.ShowColumnHeaders)
          return this.PrintColumnHeaders;
        return false;
      }
    }

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
    ///                 <para>Gets or sets whether to enable the grid's optimized mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><para>A <see cref="T:DevExpress.Xpf.Grid.UseLightweightTemplates" /> enumeration value.</para>
    /// </value>
    [Category("Options View")]
    public DevExpress.Xpf.Grid.UseLightweightTemplates? UseLightweightTemplates
    {
      get
      {
        return (DevExpress.Xpf.Grid.UseLightweightTemplates?) this.GetValue(TableView.UseLightweightTemplatesProperty);
      }
      set
      {
        this.SetValue(TableView.UseLightweightTemplatesProperty, (object) value);
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
        return (DataTemplate) this.GetValue(TableView.RowDetailsTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.RowDetailsTemplateProperty, (object) value);
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
        return (DataTemplateSelector) this.GetValue(TableView.RowDetailsTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.RowDetailsTemplateSelectorProperty, (object) value);
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
        return (DataTemplateSelector) this.GetValue(TableView.ActualRowDetailsTemplateSelectorProperty);
      }
    }

    DependencyPropertyKey ITableView.ActualRowDetailsTemplateSelectorPropertyKey
    {
      get
      {
        return TableView.ActualRowDetailsTemplateSelectorPropertyKey;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that indicates when the row details are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowDetailsVisibilityMode" /> enumeration value.
    /// </value>
    [Category("Master-Detail")]
    public RowDetailsVisibilityMode RowDetailsVisibilityMode
    {
      get
      {
        return (RowDetailsVisibilityMode) this.GetValue(TableView.RowDetailsVisibilityModeProperty);
      }
      set
      {
        this.SetValue(TableView.RowDetailsVisibilityModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Contains format conditions.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.FormatConditionCollection" /> object that is the collection of format conditions applied to the view's columns.
    /// </value>
    [XtraResetProperty]
    [XtraSerializableProperty(true, false, false)]
    [Category("Appearance ")]
    [GridUIProperty]
    public FormatConditionCollection FormatConditions
    {
      get
      {
        return this.TableViewBehavior.FormatConditions;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the conditional formatting menu. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable the conditional formatting menu; otherwise, <b>false</b>.
    /// </value>
    [Category("Conditional Formatting")]
    [XtraSerializableProperty]
    public bool AllowConditionalFormattingMenu
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowConditionalFormattingMenuProperty);
      }
      set
      {
        this.SetValue(TableView.AllowConditionalFormattingMenuProperty, (object) value);
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
        return (bool) this.GetValue(TableView.AllowConditionalFormattingManagerProperty);
      }
      set
      {
        this.SetValue(TableView.AllowConditionalFormattingManagerProperty, (object) value);
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
        return (FormatInfoCollection) this.GetValue(TableView.PredefinedFormatsProperty);
      }
      set
      {
        this.SetValue(TableView.PredefinedFormatsProperty, (object) value);
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
        return (FormatInfoCollection) this.GetValue(TableView.PredefinedColorScaleFormatsProperty);
      }
      set
      {
        this.SetValue(TableView.PredefinedColorScaleFormatsProperty, (object) value);
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
        return (FormatInfoCollection) this.GetValue(TableView.PredefinedDataBarFormatsProperty);
      }
      set
      {
        this.SetValue(TableView.PredefinedDataBarFormatsProperty, (object) value);
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
        return (FormatInfoCollection) this.GetValue(TableView.PredefinedIconSetFormatsProperty);
      }
      set
      {
        this.SetValue(TableView.PredefinedIconSetFormatsProperty, (object) value);
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
        return (DataTemplate) this.GetValue(TableView.FormatConditionDialogServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.FormatConditionDialogServiceTemplateProperty, (object) value);
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
        return (DataTemplate) this.GetValue(TableView.ConditionalFormattingManagerServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.ConditionalFormattingManagerServiceTemplateProperty, (object) value);
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
        return (DataTemplate) this.GetValue(TableView.FormatConditionGeneratorTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.FormatConditionGeneratorTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or set a list of format conditions applied to the table view.
    /// </para>
    ///             </summary>
    /// <value>The source from which the grid generates format conditions.</value>
    public IEnumerable FormatConditionsSource
    {
      get
      {
        return (IEnumerable) this.GetValue(TableView.FormatConditionsSourceProperty);
      }
      set
      {
        this.SetValue(TableView.FormatConditionsSourceProperty, (object) value);
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
        return (DataTemplateSelector) this.GetValue(TableView.FormatConditionGeneratorTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.FormatConditionGeneratorTemplateSelectorProperty, (object) value);
      }
    }

    private IClipboardManager<ColumnWrapper, RowBaseWrapper> ClipboardManager
    {
      get
      {
        if (this._clipboardManager == null)
          this._clipboardManager = this.CreateClipboardManager();
        return this._clipboardManager;
      }
    }

    /// <summary>
    ///                 <para>Specifies a data template which provides a service to display an edit form popup dialog window. This is a dependency property.
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
        return (DataTemplate) this.GetValue(TableView.EditFormDialogServiceTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormDialogServiceTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies the number of columns in the edit form. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>The number of columns in the edit form. By default, <b>3</b>.
    /// 
    /// </value>
    [Category("Editing")]
    public int EditFormColumnCount
    {
      get
      {
        return (int) this.GetValue(TableView.EditFormColumnCountProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormColumnCountProperty, (object) value);
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
        return (EditFormPostMode) this.GetValue(TableView.EditFormPostModeProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormPostModeProperty, (object) value);
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
        return (EditFormShowMode) this.GetValue(TableView.EditFormShowModeProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormShowModeProperty, (object) value);
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
        return (bool) this.GetValue(TableView.ShowEditFormOnF2KeyProperty);
      }
      set
      {
        this.SetValue(TableView.ShowEditFormOnF2KeyProperty, (object) value);
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
        return (bool) this.GetValue(TableView.ShowEditFormOnEnterKeyProperty);
      }
      set
      {
        this.SetValue(TableView.ShowEditFormOnEnterKeyProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to show the Inline Edit Form on double clicking a row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the Inline Edit Form on double clicking a row; otherwise, <b>false</b>.
    /// </value>
    [Category("Editing")]
    public bool ShowEditFormOnDoubleClick
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowEditFormOnDoubleClickProperty);
      }
      set
      {
        this.SetValue(TableView.ShowEditFormOnDoubleClickProperty, (object) value);
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
        return (PostConfirmationMode) this.GetValue(TableView.EditFormPostConfirmationProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormPostConfirmationProperty, (object) value);
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
        return (bool) this.GetValue(TableView.ShowEditFormUpdateCancelButtonsProperty);
      }
      set
      {
        this.SetValue(TableView.ShowEditFormUpdateCancelButtonsProperty, (object) value);
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
        return (DataTemplate) this.GetValue(TableView.EditFormTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.EditFormTemplateProperty, (object) value);
      }
    }

    internal EditFormManager TableViewEditFormManager
    {
      get
      {
        return this.EditFormManager as EditFormManager;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a custom Layout Calculator. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An object that implements the <see cref="T:DevExpress.Xpf.Grid.GridTableViewLayoutCalculatorFactory" /> interface.
    /// </value>
    [Browsable(false)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public GridTableViewLayoutCalculatorFactory LayoutCalculatorFactory
    {
      get
      {
        return this.ViewInfo.LayoutCalculatorFactory;
      }
      set
      {
        this.ViewInfo.LayoutCalculatorFactory = value;
      }
    }

    protected internal TableViewBehavior TableViewBehavior
    {
      get
      {
        return (TableViewBehavior) this.ViewBehavior;
      }
    }

    protected internal GridViewInfo ViewInfo
    {
      get
      {
        return this.TableViewBehavior.ViewInfo;
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the horizontally scrollable viewport. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the width of the horizontally scrollable viewport, in pixels.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Force)]
    [DevExpressXpfGridLocalizedDescription("TableViewFixedNoneContentWidth")]
    public double FixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(TableView.FixedNoneContentWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedNoneContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This property supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewTotalSummaryFixedNoneContentWidth")]
    public double TotalSummaryFixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(TableView.TotalSummaryFixedNoneContentWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.TotalSummaryFixedNoneContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewVerticalScrollBarWidth")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public double VerticalScrollBarWidth
    {
      get
      {
        return (double) this.GetValue(TableView.VerticalScrollBarWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.VerticalScrollBarWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the left fixed content width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that is the left fixed content width in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedLeftContentWidth")]
    [CloneDetailMode(CloneDetailMode.Force)]
    public double FixedLeftContentWidth
    {
      get
      {
        return (double) this.GetValue(TableView.FixedLeftContentWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedLeftContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the right fixed content width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that is the right fixed content width in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedRightContentWidth")]
    [CloneDetailMode(CloneDetailMode.Force)]
    public double FixedRightContentWidth
    {
      get
      {
        return (double) this.GetValue(TableView.FixedRightContentWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedRightContentWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This property supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [CloneDetailMode(CloneDetailMode.Force)]
    [DevExpressXpfGridLocalizedDescription("TableViewTotalGroupAreaIndent")]
    public double TotalGroupAreaIndent
    {
      get
      {
        return (double) this.GetValue(TableView.TotalGroupAreaIndentProperty);
      }
      private set
      {
        this.SetValue(TableView.TotalGroupAreaIndentPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the row indicator header. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the width of the row indicator header, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewIndicatorHeaderWidth")]
    public double IndicatorHeaderWidth
    {
      get
      {
        return (double) this.GetValue(TableView.IndicatorHeaderWidthProperty);
      }
      private set
      {
        this.SetValue(TableView.IndicatorHeaderWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the column/band chooser. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the template that displays the column/band chooser.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewColumnBandChooserTemplate")]
    public ControlTemplate ColumnBandChooserTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TableView.ColumnBandChooserTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.ColumnBandChooserTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a data row template based on custom logic. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewActualDataRowTemplateSelector")]
    public DataTemplateSelector ActualDataRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.ActualDataRowTemplateSelectorProperty);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a focused row's border. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the template that displays the border.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewFocusedRowBorderTemplate")]
    [Category("Appearance ")]
    public ControlTemplate FocusedRowBorderTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TableView.FocusedRowBorderTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.FocusedRowBorderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether multiple row/cell selection is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TableViewSelectMode" /> enumeration value that specifies the selection mode.
    /// </value>
    [Category("Options Selection")]
    [Browsable(false)]
    [XtraSerializableProperty]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Use the DataControlBase.SelectionMode property instead")]
    public TableViewSelectMode MultiSelectMode
    {
      get
      {
        return (TableViewSelectMode) this.GetValue(TableView.MultiSelectModeProperty);
      }
      set
      {
        this.SetValue(TableView.MultiSelectModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether rows can be selected via the Row Indicator Panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if rows can be selected via the row indicator; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewUseIndicatorForSelection")]
    [XtraSerializableProperty]
    [Category("Options Selection")]
    public bool UseIndicatorForSelection
    {
      get
      {
        return (bool) this.GetValue(TableView.UseIndicatorForSelectionProperty);
      }
      set
      {
        this.SetValue(TableView.UseIndicatorForSelectionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether menu items used to fix a column to the left or right, are shown within a column's context menu. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show items used to fix a column to the left or right, within a column's context menu; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowFixedColumnMenu")]
    [XtraSerializableProperty]
    public bool AllowFixedColumnMenu
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowFixedColumnMenuProperty);
      }
      set
      {
        this.SetValue(TableView.AllowFixedColumnMenuProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column headers are automatically scrolled once a user drags a column header to the View's left or right. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow horizontal scrolling of column headers; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowScrollHeaders")]
    public bool AllowScrollHeaders
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowScrollHeadersProperty);
      }
      set
      {
        this.SetValue(TableView.AllowScrollHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display group footers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display group footers; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewShowGroupFooters")]
    [XtraSerializableProperty]
    [Category("Appearance ")]
    public bool ShowGroupFooters
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowGroupFootersProperty);
      }
      set
      {
        this.SetValue(TableView.ShowGroupFootersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to hide the group row for groups that consist of a single row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to hide the group row for groups that consist of a single row; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Behavior")]
    public bool AllowPartialGrouping
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowPartialGroupingProperty);
      }
      set
      {
        this.SetValue(TableView.AllowPartialGroupingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display the bands panel.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the bands panel; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewShowBandsPanel")]
    [XtraSerializableProperty]
    public bool ShowBandsPanel
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowBandsPanelProperty);
      }
      set
      {
        this.SetValue(TableView.ShowBandsPanelProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to show the data navigator. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the data navigator; otherwise, <b>false</b>.
    /// </value>
    [Category("View")]
    [DevExpressXpfGridLocalizedDescription("TableViewShowDataNavigator")]
    [XtraSerializableProperty]
    public bool ShowDataNavigator
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowDataNavigatorProperty);
      }
      set
      {
        this.SetValue(TableView.ShowDataNavigatorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to change a column's parent band. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow changing a column's parent band; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowChangeColumnParent")]
    [XtraSerializableProperty]
    public bool AllowChangeColumnParent
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowChangeColumnParentProperty);
      }
      set
      {
        this.SetValue(TableView.AllowChangeColumnParentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to change the band's parent band. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow changing the parent band; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowChangeBandParent")]
    [Category("Options Bands")]
    [XtraSerializableProperty]
    public bool AllowChangeBandParent
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowChangeBandParentProperty);
      }
      set
      {
        this.SetValue(TableView.AllowChangeBandParentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to show the 'Bands' tab within the Column Band Chooser.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show the 'Bands' tab within the column chooser; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewShowBandsInCustomizationForm")]
    [XtraSerializableProperty]
    public bool ShowBandsInCustomizationForm
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowBandsInCustomizationFormProperty);
      }
      set
      {
        this.SetValue(TableView.ShowBandsInCustomizationFormProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to rearrange bands. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow rearranging bands; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowBandMoving")]
    [Category("Options Bands")]
    [XtraSerializableProperty]
    public bool AllowBandMoving
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowBandMovingProperty);
      }
      set
      {
        this.SetValue(TableView.AllowBandMovingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can change band widths by dragging the edges of their headers.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to allow an end-user to change band widths; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowBandResizing")]
    [XtraSerializableProperty]
    public bool AllowBandResizing
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowBandResizingProperty);
      }
      set
      {
        this.SetValue(TableView.AllowBandResizingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether advanced vertical navigation is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable advanced vertical navigation; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowAdvancedVerticalNavigation")]
    [XtraSerializableProperty]
    public bool AllowAdvancedVerticalNavigation
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowAdvancedVerticalNavigationProperty);
      }
      set
      {
        this.SetValue(TableView.AllowAdvancedVerticalNavigationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether advanced horizontal navigation is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to enable advanced horizontal navigation; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowAdvancedHorizontalNavigation")]
    [Category("Options Bands")]
    public bool AllowAdvancedHorizontalNavigation
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowAdvancedHorizontalNavigationProperty);
      }
      set
      {
        this.SetValue(TableView.AllowAdvancedHorizontalNavigationProperty, (object) value);
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
        return (IComparer<BandBase>) this.GetValue(TableView.ColumnChooserBandsSortOrderComparerProperty);
      }
      set
      {
        this.SetValue(TableView.ColumnChooserBandsSortOrderComparerProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of band headers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of band headers.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewBandHeaderTemplate")]
    [XtraSerializableProperty]
    [Category("Options Bands")]
    public DataTemplate BandHeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.BandHeaderTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.BandHeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a band header template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that applies a template based on custom logic.
    /// </value>
    [Category("Options Bands")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewBandHeaderTemplateSelector")]
    public DataTemplateSelector BandHeaderTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.BandHeaderTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.BandHeaderTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of band header tooltips. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of band header tooltips.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewBandHeaderToolTipTemplate")]
    [Category("Options Bands")]
    public DataTemplate BandHeaderToolTipTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.BandHeaderToolTipTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.BandHeaderToolTipTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to band headers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to band headers when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintBandHeaderStyle")]
    [Category("Appearance Print")]
    public Style PrintBandHeaderStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.PrintBandHeaderStyleProperty);
      }
      set
      {
        this.SetValue(TableView.PrintBandHeaderStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not the grid displays each data record on a single row when using bands.
    /// </para>
    ///             </summary>
    /// <value><b>false</b>, to make the grid display each data record on a single row when using bands; otherwise, <b>true</b>.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Bands")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowBandMultiRow")]
    public bool AllowBandMultiRow
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowBandMultiRowProperty);
      }
      set
      {
        this.SetValue(TableView.AllowBandMultiRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DevExpressXpfGridLocalizedDescription("TableViewScrollingVirtualizationMargin")]
    public Thickness ScrollingVirtualizationMargin
    {
      get
      {
        return (Thickness) this.GetValue(TableView.ScrollingVirtualizationMarginProperty);
      }
      internal set
      {
        this.SetValue(TableView.ScrollingVirtualizationMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DevExpressXpfGridLocalizedDescription("TableViewScrollingHeaderVirtualizationMargin")]
    public Thickness ScrollingHeaderVirtualizationMargin
    {
      get
      {
        return (Thickness) this.GetValue(TableView.ScrollingHeaderVirtualizationMarginProperty);
      }
      internal set
      {
        this.SetValue(TableView.ScrollingHeaderVirtualizationMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to data rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to data rows.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewRowStyle")]
    public Style RowStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.RowStyleProperty);
      }
      set
      {
        this.SetValue(TableView.RowStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the Automatic Filter Row is displayed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show the automatic filter row; otherwise, <b>false</b>. The default is <b>false</b>.
    /// 
    /// </value>
    [Category("Options Filter")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewShowAutoFilterRow")]
    public bool ShowAutoFilterRow
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowAutoFilterRowProperty);
      }
      set
      {
        this.SetValue(TableView.ShowAutoFilterRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para><para>Gets or sets whether or not to display the criteria selector buttons in the automatic filter row for all columns in the current table view.</para>
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the criteria selector buttons in the automatic filter row; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewShowCriteriaInAutoFilterRow")]
    [Category("Options Filter")]
    [XtraSerializableProperty]
    public bool ShowCriteriaInAutoFilterRow
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowCriteriaInAutoFilterRowProperty);
      }
      set
      {
        this.SetValue(TableView.ShowCriteriaInAutoFilterRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not cascading data updates are allowed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow cascading data updates; otherwise, <b>false</b>. The default is <b>false</b>.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowCascadeUpdate")]
    [XtraSerializableProperty]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("Options View")]
    public bool AllowCascadeUpdate
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowCascadeUpdateProperty);
      }
      set
      {
        this.SetValue(TableView.AllowCascadeUpdateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether per-pixel scrolling is enabled. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable per-pixel scrolling; <b>false</b> to enable row by row scrolling.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowPerPixelScrolling")]
    [XtraSerializableProperty]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("Options View")]
    public bool AllowPerPixelScrolling
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowPerPixelScrollingProperty);
      }
      set
      {
        this.SetValue(TableView.AllowPerPixelScrollingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the scroll animation length. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the animation length, in milliseconds.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [XtraSerializableProperty]
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TableViewScrollAnimationDuration")]
    public double ScrollAnimationDuration
    {
      get
      {
        return (double) this.GetValue(TableView.ScrollAnimationDurationProperty);
      }
      set
      {
        this.SetValue(TableView.ScrollAnimationDurationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the per-pixel scrolling mode. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ScrollAnimationMode" /> enumeration value that specifies the per-pixel scrolling mode.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DevExpressXpfGridLocalizedDescription("TableViewScrollAnimationMode")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public ScrollAnimationMode ScrollAnimationMode
    {
      get
      {
        return (ScrollAnimationMode) this.GetValue(TableView.ScrollAnimationModeProperty);
      }
      set
      {
        this.SetValue(TableView.ScrollAnimationModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable scroll animation. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable scroll animation; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowScrollAnimation")]
    [Category("Options View")]
    [XtraSerializableProperty]
    [CloneDetailMode(CloneDetailMode.Skip)]
    public bool AllowScrollAnimation
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowScrollAnimationProperty);
      }
      set
      {
        this.SetValue(TableView.AllowScrollAnimationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether the horizontal scrollbar fills the entire width of the grid. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the horizontal scrollbar should fill the entire width of the grid; otherwise, <b>false</b>.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewExtendScrollBarToFixedColumns")]
    [XtraSerializableProperty]
    [Category("Options View")]
    [CloneDetailMode(CloneDetailMode.Skip)]
    public bool ExtendScrollBarToFixedColumns
    {
      get
      {
        return (bool) this.GetValue(TableView.ExtendScrollBarToFixedColumnsProperty);
      }
      set
      {
        this.SetValue(TableView.ExtendScrollBarToFixedColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets an object that represents the Auto Filter Row's data.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowData" /> object that represents the row's data.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAutoFilterRowData")]
    public RowData AutoFilterRowData
    {
      get
      {
        return ((TableViewBehavior) this.ViewBehavior).AutoFilterRowData;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value indicating whether virtualization is enabled for horizontal scrolling.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable virtualization; <b>false</b> to disable it.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowHorizontalScrollingVirtualization")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool AllowHorizontalScrollingVirtualization
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowHorizontalScrollingVirtualizationProperty);
      }
      set
      {
        this.SetValue(TableView.AllowHorizontalScrollingVirtualizationProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a row's minimum height. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies a row's minimum height.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewRowMinHeight")]
    public double RowMinHeight
    {
      get
      {
        return (double) this.GetValue(TableView.RowMinHeightProperty);
      }
      set
      {
        this.SetValue(TableView.RowMinHeightProperty, (object) value);
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
        return (double) this.GetValue(TableView.HeaderPanelMinHeightProperty);
      }
      set
      {
        this.SetValue(TableView.HeaderPanelMinHeightProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the row decoration template. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ControlTemplate" /> object that represents the row decoration template.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewRowDecorationTemplate")]
    [Category("Appearance ")]
    public ControlTemplate RowDecorationTemplate
    {
      get
      {
        return (ControlTemplate) this.GetValue(TableView.RowDecorationTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.RowDecorationTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the template that defines the default presentation of data rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the default presentation of data rows.
    /// </value>
    [Category("Appearance ")]
    [Browsable(false)]
    public DataTemplate DefaultDataRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.DefaultDataRowTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.DefaultDataRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of data rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of data rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewDataRowTemplate")]
    [Category("Appearance ")]
    public DataTemplate DataRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.DataRowTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.DataRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a data row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewDataRowTemplateSelector")]
    public DataTemplateSelector DataRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.DataRowTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.DataRowTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of a row indicator's content. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of row indicators.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewRowIndicatorContentTemplate")]
    [Category("Appearance ")]
    public DataTemplate RowIndicatorContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.RowIndicatorContentTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.RowIndicatorContentTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value specifying whether horizontal navigation keys move focus to the next/previous row when the current row's last/first cell is focused.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if horizontal navigation keys can move focus between rows; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAutoMoveRowFocus")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool AutoMoveRowFocus
    {
      get
      {
        return (bool) this.GetValue(TableView.AutoMoveRowFocusProperty);
      }
      set
      {
        this.SetValue(TableView.AutoMoveRowFocusProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the number of records taken into account when calculating the optimal widths required for columns to completely display their contents. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the number of records processed by a View to apply <b>best fit</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewBestFitMaxRowCount")]
    [Category("BestFit")]
    [XtraSerializableProperty]
    public int BestFitMaxRowCount
    {
      get
      {
        return (int) this.GetValue(TableView.BestFitMaxRowCountProperty);
      }
      set
      {
        this.SetValue(TableView.BestFitMaxRowCountProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets how the optimal width required for a column to completely display its contents is calculated. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Core.BestFitMode" /> enumeration value.
    /// </value>
    [XtraSerializableProperty]
    [Category("BestFit")]
    [DevExpressXpfGridLocalizedDescription("TableViewBestFitMode")]
    public BestFitMode BestFitMode
    {
      get
      {
        return (BestFitMode) this.GetValue(TableView.BestFitModeProperty);
      }
      set
      {
        this.SetValue(TableView.BestFitModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets which interface elements are taken into account when calculating optimal width for columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.BestFitArea" /> enumeration value that specifies interface elements that are taken into account when calculating optimal width for columns.
    /// </value>
    [Category("BestFit")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewBestFitArea")]
    public BestFitArea BestFitArea
    {
      get
      {
        return (BestFitArea) this.GetValue(TableView.BestFitAreaProperty);
      }
      set
      {
        this.SetValue(TableView.BestFitAreaProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether it's allowed to calculate optimal widths, and apply them to all columns displayed within a View.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow optimal widths to be calculated and applied to all columns displayed within a View; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// 
    /// </value>
    [XtraSerializableProperty]
    [Category("BestFit")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowBestFit")]
    public bool AllowBestFit
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowBestFitProperty);
      }
      set
      {
        this.SetValue(TableView.AllowBestFitProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value specifying whether the Row Indicator panel is displayed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the row indicator panel; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewShowIndicator")]
    [Category("Options View")]
    public bool ShowIndicator
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowIndicatorProperty);
      }
      set
      {
        this.SetValue(TableView.ShowIndicatorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the Row Indicator Panel is shown within a view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the Row Indicator Panel is shown within a view; otherwise, <b>false</b>.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Force)]
    public bool ActualShowIndicator
    {
      get
      {
        return (bool) this.GetValue(TableView.ActualShowIndicatorProperty);
      }
      protected set
      {
        this.SetValue(TableView.ActualShowIndicatorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the width of the row indicator panel.
    /// </para>
    ///             </summary>
    /// <value>An integer value specifying the width of the row indicator panel in pixels.</value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewIndicatorWidth")]
    [XtraSerializableProperty]
    public double IndicatorWidth
    {
      get
      {
        return (double) this.GetValue(TableView.IndicatorWidthProperty);
      }
      set
      {
        this.SetValue(TableView.IndicatorWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [CloneDetailMode(CloneDetailMode.Force)]
    public double ActualIndicatorWidth
    {
      get
      {
        return (double) this.GetValue(TableView.ActualIndicatorWidthProperty);
      }
      protected set
      {
        this.SetValue(TableView.ActualIndicatorWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the detail expand button's width.
    /// </para>
    ///             </summary>
    /// <value>A Double value specifying the detail expand button's width.</value>
    [Category("Master-Detail")]
    public double ExpandDetailButtonWidth
    {
      get
      {
        return (double) this.GetValue(TableView.ExpandDetailButtonWidthProperty);
      }
      set
      {
        this.SetValue(TableView.ExpandDetailButtonWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>A Double value indicating the actual detail expand button's width.</value>
    [CloneDetailMode(CloneDetailMode.Force)]
    public double ActualExpandDetailButtonWidth
    {
      get
      {
        return (double) this.GetValue(TableView.ActualExpandDetailButtonWidthProperty);
      }
      internal set
      {
        this.SetValue(TableView.ActualExpandDetailButtonWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the outer margin of view's details.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> object that represents the thickness of a frame around view's details.
    /// </value>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Thickness DetailMargin
    {
      get
      {
        return (Thickness) this.GetValue(TableView.DetailMarginProperty);
      }
      set
      {
        this.SetValue(TableView.DetailMarginProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual outer margin of view's details.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> object that represents the thickness of a frame around a view's details.
    /// 
    /// </value>
    [Browsable(false)]
    [CloneDetailMode(CloneDetailMode.Force)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Thickness ActualDetailMargin
    {
      get
      {
        return (Thickness) this.GetValue(TableView.ActualDetailMarginProperty);
      }
      internal set
      {
        this.SetValue(TableView.ActualDetailMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>A Double value specifying the actual width of the header displayed above detail expand buttons.
    /// </value>
    public double ActualExpandDetailHeaderWidth
    {
      get
      {
        return (double) this.GetValue(TableView.ActualExpandDetailHeaderWidthProperty);
      }
      protected set
      {
        this.SetValue(TableView.ActualExpandDetailHeaderWidthPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ColumnPosition" /> enumeration value identifying the location of the column that displays detail expand buttons.
    /// </value>
    public ColumnPosition ExpandColumnPosition
    {
      get
      {
        return (ColumnPosition) this.GetValue(TableView.ExpandColumnPositionProperty);
      }
      protected set
      {
        this.SetValue(TableView.ExpandColumnPositionPropertyKey, (object) value);
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
        return (bool) this.GetValue(TableView.ShowTotalSummaryIndicatorIndentProperty);
      }
      protected set
      {
        this.SetValue(TableView.ShowTotalSummaryIndicatorIndentPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether vertical lines are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display vertical lines; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewShowVerticalLines")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool ShowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(TableView.ShowVerticalLinesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether horizontal lines are displayed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display horizontal lines; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [XtraSerializableProperty]
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TableViewShowHorizontalLines")]
    public bool ShowHorizontalLines
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowHorizontalLinesProperty);
      }
      set
      {
        this.SetValue(TableView.ShowHorizontalLinesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column widths are automatically changed so that the total columns' width matches the grid's width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the column auto width feature; otherwise, <b>false</b>. The default is <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewAutoWidth")]
    [Category("Options Behavior")]
    public bool AutoWidth
    {
      get
      {
        return (bool) this.GetValue(TableView.AutoWidthProperty);
      }
      set
      {
        this.SetValue(TableView.AutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can change column widths by dragging the edges of their headers.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to change column widths; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowResizing")]
    public bool AllowResizing
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowResizingProperty);
      }
      set
      {
        this.SetValue(TableView.AllowResizingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the fixed line's width. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the fixed line's width.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedLineWidth")]
    [XtraSerializableProperty]
    [Category("Appearance ")]
    public double FixedLineWidth
    {
      get
      {
        return (double) this.GetValue(TableView.FixedLineWidthProperty);
      }
      set
      {
        this.SetValue(TableView.FixedLineWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>For internal use.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public bool UseGroupShadowIndent
    {
      get
      {
        return (bool) this.GetValue(TableView.UseGroupShadowIndentProperty);
      }
      set
      {
        this.SetValue(TableView.UseGroupShadowIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public double LeftDataAreaIndent
    {
      get
      {
        return (double) this.GetValue(TableView.LeftDataAreaIndentProperty);
      }
      set
      {
        this.SetValue(TableView.LeftDataAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public double RightDataAreaIndent
    {
      get
      {
        return (double) this.GetValue(TableView.RightDataAreaIndentProperty);
      }
      set
      {
        this.SetValue(TableView.RightDataAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are fixed to the left. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns fixed to the left.</value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedLeftVisibleColumns")]
    [Category("Options Layout")]
    public IList<GridColumn> FixedLeftVisibleColumns
    {
      get
      {
        return (IList<GridColumn>) this.GetValue(TableView.FixedLeftVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedLeftVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are fixed to the right. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns fixed to the right.</value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedRightVisibleColumns")]
    [Category("Options Layout")]
    public IList<GridColumn> FixedRightVisibleColumns
    {
      get
      {
        return (IList<GridColumn>) this.GetValue(TableView.FixedRightVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedRightVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Returns the list of visible columns that are not fixed within a View. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of visible columns that are not fixed within a View.</value>
    [DevExpressXpfGridLocalizedDescription("TableViewFixedNoneVisibleColumns")]
    [Category("Options Layout")]
    public IList<GridColumn> FixedNoneVisibleColumns
    {
      get
      {
        return (IList<GridColumn>) this.GetValue(TableView.FixedNoneVisibleColumnsProperty);
      }
      private set
      {
        this.SetValue(TableView.FixedNoneVisibleColumnsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the width of the view's client area.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the client area's width.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewHorizontalViewport")]
    public double HorizontalViewport
    {
      get
      {
        return (double) this.GetValue(TableView.HorizontalViewportProperty);
      }
      private set
      {
        this.SetValue(TableView.HorizontalViewportPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to data cells displayed within the Auto Filter Row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that specifies the style applied to data cells.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAutoFilterRowCellStyle")]
    [Category("Appearance ")]
    public Style AutoFilterRowCellStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.AutoFilterRowCellStyleProperty);
      }
      set
      {
        this.SetValue(TableView.AutoFilterRowCellStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to data cells displayed within the New Item Row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that specifies the style applied to data cells.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewNewItemRowCellStyle")]
    [Category("Appearance ")]
    public Style NewItemRowCellStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.NewItemRowCellStyleProperty);
      }
      set
      {
        this.SetValue(TableView.NewItemRowCellStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the view has details.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the view has details; otherwise, <b>false</b>.
    /// </value>
    public bool HasDetailViews
    {
      get
      {
        return (bool) this.GetValue(TableView.HasDetailViewsProperty);
      }
      private set
      {
        this.SetValue(TableView.HasDetailViewsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display expand buttons within master rows.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display expand buttons within master rows; otherwise, <b>false</b>.
    /// </value>
    [Category("Master-Detail")]
    public DefaultBoolean ShowDetailButtons
    {
      get
      {
        return (DefaultBoolean) this.GetValue(TableView.ShowDetailButtonsProperty);
      }
      set
      {
        this.SetValue(TableView.ShowDetailButtonsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether end-users can access this View's details.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow end-users access detail views; otherwise, <b>false</b>.
    /// </value>
    [Category("Master-Detail")]
    public bool AllowMasterDetail
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowMasterDetailProperty);
      }
      set
      {
        this.SetValue(TableView.AllowMasterDetailProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if detail expand buttons should be displayed; otherwise, <b>false</b>.
    /// </value>
    public bool ActualShowDetailButtons
    {
      get
      {
        return (bool) this.GetValue(TableView.ActualShowDetailButtonsProperty);
      }
      private set
      {
        this.SetValue(TableView.ActualShowDetailButtonsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the control's internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>A BindingContainer object.</value>
    [CloneDetailMode(CloneDetailMode.Force)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public BindingContainer IsDetailButtonVisibleBindingContainer
    {
      get
      {
        return (BindingContainer) this.GetValue(TableView.IsDetailButtonVisibleBindingContainerProperty);
      }
      private set
      {
        this.SetValue(TableView.IsDetailButtonVisibleBindingContainerPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the binding that determines which rows display detail expand buttons.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Data.BindingBase" /> object specifying which rows display detail expand buttons.
    /// </value>
    [DefaultValue(null)]
    [Category("Master-Detail")]
    public BindingBase IsDetailButtonVisibleBinding
    {
      get
      {
        return this.isDetailButtonVisibleBinding;
      }
      set
      {
        if (this.isDetailButtonVisibleBinding == value)
          return;
        this.isDetailButtonVisibleBinding = value;
        this.IsDetailButtonVisibleBindingContainer = this.isDetailButtonVisibleBinding != null ? new BindingContainer(this.isDetailButtonVisibleBinding) : (BindingContainer) null;
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
        return (Brush) this.GetValue(TableView.AlternateRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TableView.AlternateRowBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual brush that is used to alternate row background. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.Brush" /> value.
    /// </value>
    public Brush ActualAlternateRowBackground
    {
      get
      {
        return (Brush) this.GetValue(TableView.ActualAlternateRowBackgroundProperty);
      }
      protected set
      {
        this.SetValue(TableView.ActualAlternateRowBackgroundPropertyKey, (object) value);
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
        return (Brush) this.GetValue(TableView.EvenRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TableView.EvenRowBackgroundProperty, (object) value);
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
        return (bool) this.GetValue(TableView.UseEvenRowBackgroundProperty);
      }
      set
      {
        this.SetValue(TableView.UseEvenRowBackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the alternate row frequency. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that is the alternate row frequency. By default, it's set to 0.</value>
    [Category("Appearance ")]
    public int AlternationCount
    {
      get
      {
        return (int) this.GetValue(TableView.AlternationCountProperty);
      }
      set
      {
        this.SetValue(TableView.AlternationCountProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public double LeftGroupAreaIndent
    {
      get
      {
        return (double) this.GetValue(TableView.LeftGroupAreaIndentProperty);
      }
      set
      {
        this.SetValue(TableView.LeftGroupAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public double RightGroupAreaIndent
    {
      get
      {
        return (double) this.GetValue(TableView.RightGroupAreaIndentProperty);
      }
      set
      {
        this.SetValue(TableView.RightGroupAreaIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of data rows when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of data rows when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintRowTemplate")]
    public DataTemplate PrintRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.PrintRowTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.PrintRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether printed columns' widths are automatically changed, so that the grid fits the width of the report page.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to automatically change the grid's width so that it fits the width of the report page; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintAutoWidth")]
    [Category("Options Print")]
    [XtraSerializableProperty]
    public bool PrintAutoWidth
    {
      get
      {
        return (bool) this.GetValue(TableView.PrintAutoWidthProperty);
      }
      set
      {
        this.SetValue(TableView.PrintAutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column headers are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to print column headers; otherwise, <b>false</b>. The default is <b>true</b>.
    /// 
    /// </value>
    [Category("Options Print")]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintColumnHeaders")]
    [XtraSerializableProperty]
    public bool PrintColumnHeaders
    {
      get
      {
        return (bool) this.GetValue(TableView.PrintColumnHeadersProperty);
      }
      set
      {
        this.SetValue(TableView.PrintColumnHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether band headers are printed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to print column headers; otherwise, <b>false</b>. The default is <b>true</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintBandHeaders")]
    [Category("Options Print")]
    public bool PrintBandHeaders
    {
      get
      {
        return (bool) this.GetValue(TableView.PrintBandHeadersProperty);
      }
      set
      {
        this.SetValue(TableView.PrintBandHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to print the Group Footer's content. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to print group footers; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintGroupFooters")]
    [Category("Options Print")]
    public bool PrintGroupFooters
    {
      get
      {
        return (bool) this.GetValue(TableView.PrintGroupFootersProperty);
      }
      set
      {
        this.SetValue(TableView.PrintGroupFootersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether View's details can be printed. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies whether View's details can be printed.
    /// 
    /// </value>
    [Category("Options Print")]
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowPrintDetails")]
    public DefaultBoolean AllowPrintDetails
    {
      get
      {
        return (DefaultBoolean) this.GetValue(TableView.AllowPrintDetailsProperty);
      }
      set
      {
        this.SetValue(TableView.AllowPrintDetailsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not to print/export empty details. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies whether or not to print/export empty details.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options Print")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowPrintEmptyDetails")]
    public DefaultBoolean AllowPrintEmptyDetails
    {
      get
      {
        return (DefaultBoolean) this.GetValue(TableView.AllowPrintEmptyDetailsProperty);
      }
      set
      {
        this.SetValue(TableView.AllowPrintEmptyDetailsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to print all view's details. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies whether or not to print all the view's details.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewAllowPrintEmptyDetails")]
    [XtraSerializableProperty]
    [Category("Options Print")]
    public DefaultBoolean PrintAllDetails
    {
      get
      {
        return (DefaultBoolean) this.GetValue(TableView.PrintAllDetailsProperty);
      }
      set
      {
        this.SetValue(TableView.PrintAllDetailsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the Group Footer's presentation when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the group footer's presentation when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintGroupFooterTemplate")]
    [Category("Appearance Print")]
    public DataTemplate PrintGroupFooterTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.PrintGroupFooterTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.PrintGroupFooterTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to column headers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to column headers when the grid is printed.
    /// </value>
    [Category("Appearance Print")]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintColumnHeaderStyle")]
    public Style PrintColumnHeaderStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.PrintColumnHeaderStyleProperty);
      }
      set
      {
        this.SetValue(TableView.PrintColumnHeaderStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to the group footers when the grid is printed. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to the group footers when the grid is printed.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintGroupFooterStyle")]
    [Category("Appearance Print")]
    public Style PrintGroupFooterStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.PrintGroupFooterStyleProperty);
      }
      set
      {
        this.SetValue(TableView.PrintGroupFooterStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the top print detail indent. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the top indent, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintDetailTopIndent")]
    [Category("Appearance Print")]
    public double PrintDetailTopIndent
    {
      get
      {
        return (double) this.GetValue(TableView.PrintDetailTopIndentProperty);
      }
      set
      {
        this.SetValue(TableView.PrintDetailTopIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the bottom print detail indent. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the bottom indent, in pixels.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewPrintDetailBottomIndent")]
    [Category("Appearance Print")]
    public double PrintDetailBottomIndent
    {
      get
      {
        return (double) this.GetValue(TableView.PrintDetailBottomIndentProperty);
      }
      set
      {
        this.SetValue(TableView.PrintDetailBottomIndentProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the position of group summaries within a group row when printing. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GroupSummaryDisplayMode" /> enumeration value that specifies the position of group summaries within a group row.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("TableViewPrintGroupSummaryDisplayMode")]
    [Category("Appearance Print")]
    public GroupSummaryDisplayMode PrintGroupSummaryDisplayMode
    {
      get
      {
        return (GroupSummaryDisplayMode) this.GetValue(TableView.PrintGroupSummaryDisplayModeProperty);
      }
      set
      {
        this.SetValue(TableView.PrintGroupSummaryDisplayModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the New Item Row's position within a View.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.NewItemRowPosition" /> enumeration value that specifies the New Item Row's position within a View.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewNewItemRowPosition")]
    [XtraSerializableProperty]
    [Category("Options View")]
    public NewItemRowPosition NewItemRowPosition
    {
      get
      {
        return (NewItemRowPosition) this.GetValue(TableView.NewItemRowPositionProperty);
      }
      set
      {
        this.SetValue(TableView.NewItemRowPositionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the top row for the visible group is always displayed as the user scrolls through grouped data. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies whether to fix group rows when scrolling through grouped data.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options View")]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowFixedGroups")]
    public DefaultBoolean AllowFixedGroups
    {
      get
      {
        return (DefaultBoolean) this.GetValue(TableView.AllowFixedGroupsProperty);
      }
      set
      {
        this.SetValue(TableView.AllowFixedGroupsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the position of group summaries within a group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GroupSummaryDisplayMode" /> enumeration value that specifies the position of group summaries within a group row.
    /// </value>
    [XtraSerializableProperty]
    [Category("Options View")]
    public GroupSummaryDisplayMode GroupSummaryDisplayMode
    {
      get
      {
        return (GroupSummaryDisplayMode) this.GetValue(TableView.GroupSummaryDisplayModeProperty);
      }
      set
      {
        this.SetValue(TableView.GroupSummaryDisplayModeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of group summary items aligned by columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group summary items aligned by columns.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewGroupColumnSummaryItemTemplate")]
    [Category("Appearance ")]
    public DataTemplate GroupColumnSummaryItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.GroupColumnSummaryItemTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.GroupColumnSummaryItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group summary items aligned by bands. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to group summary items aligned by bands.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewGroupBandSummaryContentStyle")]
    [Category("Appearance ")]
    public Style GroupBandSummaryContentStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.GroupBandSummaryContentStyleProperty);
      }
      set
      {
        this.SetValue(TableView.GroupBandSummaryContentStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group summary items aligned by columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to group summary items aligned by columns.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewGroupColumnSummaryContentStyle")]
    public Style GroupColumnSummaryContentStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.GroupColumnSummaryContentStyleProperty);
      }
      set
      {
        this.SetValue(TableView.GroupColumnSummaryContentStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to individual text elements in the group summary item that is displayed within the group footer. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that is the style applied to individual text elements in the group summary item that is displayed within the group footer.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewGroupColumnFooterElementStyle")]
    public Style GroupColumnFooterElementStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.GroupColumnFooterElementStyleProperty);
      }
      set
      {
        this.SetValue(TableView.GroupColumnFooterElementStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether group summaries are asynchronously calculated, one after another, in a background thread. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable asynchronous calculation of group summaries; otherwise, <b>false</b>.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DevExpressXpfGridLocalizedDescription("TableViewAllowGroupSummaryCascadeUpdate")]
    [Category("Options View")]
    [XtraSerializableProperty]
    public bool AllowGroupSummaryCascadeUpdate
    {
      get
      {
        return (bool) this.GetValue(TableView.AllowGroupSummaryCascadeUpdateProperty);
      }
      set
      {
        this.SetValue(TableView.AllowGroupSummaryCascadeUpdateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the vertical scrollbar's visibility. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ScrollBarVisibility" /> enumeration value that specifies the vertical scrollbar's visibility.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    public ScrollBarVisibility VerticalScrollbarVisibility
    {
      get
      {
        return (ScrollBarVisibility) this.GetValue(TableView.VerticalScrollbarVisibilityProperty);
      }
      set
      {
        this.SetValue(TableView.VerticalScrollbarVisibilityProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the horizontal scrollbar's visibility. This is a dependence property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.ScrollBarVisibility" /> enumeration value that specifies the horizontal scrollbar's visibility.
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    public ScrollBarVisibility HorizontalScrollbarVisibility
    {
      get
      {
        return (ScrollBarVisibility) this.GetValue(TableView.HorizontalScrollbarVisibilityProperty);
      }
      set
      {
        this.SetValue(TableView.HorizontalScrollbarVisibilityProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets an object that represents the New Item Row's data.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.RowData" /> object that represents the row's data.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewNewItemRowData")]
    public RowData NewItemRowData { get; private set; }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of group footer rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group footer rows.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewGroupFooterRowTemplate")]
    [Category("Appearance ")]
    public DataTemplate GroupFooterRowTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.GroupFooterRowTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterRowTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group footer row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [Category("Appearance ")]
    [DevExpressXpfGridLocalizedDescription("TableViewGroupFooterRowTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public DataTemplateSelector GroupFooterRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.GroupFooterRowTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterRowTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a group footer row template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    public DataTemplateSelector ActualGroupFooterRowTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.ActualGroupFooterRowTemplateSelectorProperty);
      }
      protected set
      {
        this.SetValue(TableView.ActualGroupFooterRowTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to summary items displayed within group footers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group rows.
    /// </value>
    [Category("Appearance ")]
    public Style GroupFooterSummaryContentStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.GroupFooterSummaryContentStyleProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterSummaryContentStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of group footer summary items. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group footer rows.
    /// </value>
    [Category("Appearance ")]
    public DataTemplate GroupFooterSummaryItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.GroupFooterSummaryItemTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterSummaryItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group footer summary item template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    public DataTemplateSelector GroupFooterSummaryItemTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.GroupFooterSummaryItemTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterSummaryItemTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a group footer summary item template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    public DataTemplateSelector ActualGroupFooterSummaryItemTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(TableView.ActualGroupFooterSummaryItemTemplateSelectorProperty);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the style applied to group footers. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Style" /> object that represents the style applied to group footers.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewGroupFooterRowStyle")]
    [Category("Appearance ")]
    public Style GroupFooterRowStyle
    {
      get
      {
        return (Style) this.GetValue(TableView.GroupFooterRowStyleProperty);
      }
      set
      {
        this.SetValue(TableView.GroupFooterRowStyleProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Provides access to view commands.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TableViewCommands" /> object that provides a set of view commands.
    /// </value>
    public TableViewCommands TableViewCommands
    {
      get
      {
        return (TableViewCommands) this.Commands;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to enable the Selector Column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to enable the Selector Column; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Selection")]
    public bool ShowCheckBoxSelectorColumn
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowCheckBoxSelectorColumnProperty);
      }
      set
      {
        this.SetValue(TableView.ShowCheckBoxSelectorColumnProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the Selector Column can be used to select and deselect groups. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to show checkboxes corresponding to group rows in the Selector Column; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Selection")]
    public bool ShowCheckBoxSelectorInGroupRow
    {
      get
      {
        return (bool) this.GetValue(TableView.ShowCheckBoxSelectorInGroupRowProperty);
      }
      set
      {
        this.SetValue(TableView.ShowCheckBoxSelectorInGroupRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the width of the Selector Column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The width of the selector column.</value>
    [Category("Options Selection")]
    public double CheckBoxSelectorColumnWidth
    {
      get
      {
        return (double) this.GetValue(TableView.CheckBoxSelectorColumnWidthProperty);
      }
      set
      {
        this.SetValue(TableView.CheckBoxSelectorColumnWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the Selector Column header. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of the selector column header.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public DataTemplate CheckBoxSelectorColumnHeaderTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(TableView.CheckBoxSelectorColumnHeaderTemplateProperty);
      }
      set
      {
        this.SetValue(TableView.CheckBoxSelectorColumnHeaderTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the selection made in the check column is retained on a click in the grid outside the check column. This is a dependency property
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if selection is retained; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Selection")]
    public bool RetainSelectionOnClickOutsideCheckBoxSelector
    {
      get
      {
        return (bool) this.GetValue(TableView.RetainSelectionOnClickOutsideCheckBoxSelectorProperty);
      }
      set
      {
        this.SetValue(TableView.RetainSelectionOnClickOutsideCheckBoxSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the type of annotations displayed within the view's scrollbar.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.ScrollBarAnnotationMode" /> flag that is the type of scrollbar annotations applied to the <see cref="T:DevExpress.Xpf.Grid.TableView" />.
    /// </value>
    [Category("Appearance ")]
    public DevExpress.Xpf.Grid.ScrollBarAnnotationMode? ScrollBarAnnotationMode
    {
      get
      {
        return (DevExpress.Xpf.Grid.ScrollBarAnnotationMode?) this.GetValue(TableView.ScrollBarAnnotationModeProperty);
      }
      set
      {
        this.SetValue(TableView.ScrollBarAnnotationModeProperty, (object) value);
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
        return (ScrollBarAnnotationsAppearance) this.GetValue(TableView.ScrollBarAnnotationsAppearanceProperty);
      }
      set
      {
        this.SetValue(TableView.ScrollBarAnnotationsAppearanceProperty, (object) value);
      }
    }

    DevExpress.Xpf.Grid.ScrollBarAnnotationMode ITableView.ScrollBarAnnotationModeActual
    {
      get
      {
        if (this.IsRootView || this.ScrollBarAnnotationMode.HasValue)
          return this.ScrollBarAnnotationMode ?? DevExpress.Xpf.Grid.ScrollBarAnnotationMode.None;
        DataControlBase dataControlBase = this.DataControl != null ? this.DataControl.GetMasterGridCore() : (DataControlBase) null;
        if (dataControlBase == null || dataControlBase.DataView == null)
          return this.ScrollBarAnnotationMode ?? DevExpress.Xpf.Grid.ScrollBarAnnotationMode.None;
        return ((ITableView) dataControlBase.DataView).ScrollBarAnnotationModeActual;
      }
    }

    internal override bool AllowFixedGroupsCore
    {
      get
      {
        return this.AllowFixedGroups.GetValue(((ITableView) this.RootView).AllowPerPixelScrolling || this.DataProviderBase.IsServerMode || this.DataProviderBase.IsAsyncServerMode);
      }
    }

    protected internal GridDataProvider GridDataProvider
    {
      get
      {
        return (GridDataProvider) this.DataProviderBase;
      }
    }

    internal override Style GetNewItemRowCellStyle
    {
      get
      {
        return this.NewItemRowCellStyle;
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

    protected override bool CanNavigateToNewItemRow
    {
      get
      {
        if (this.ActualNewItemRowPosition == NewItemRowPosition.Top)
          return !this.TableViewBehavior.IsNewItemRowFocused;
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

    internal override bool PrintAllGroupsCore
    {
      get
      {
        return this.PrintAllGroups;
      }
    }

    internal NewItemRowPosition ActualNewItemRowPosition
    {
      get
      {
        return this.actualnewItemRowPositionCore;
      }
      set
      {
        if (this.actualnewItemRowPositionCore == value)
          return;
        NewItemRowPosition oldValue = this.actualnewItemRowPositionCore;
        this.actualnewItemRowPositionCore = value;
        this.OnActualNewItemRowPositionChanged(oldValue);
      }
    }

    internal override bool GetAllowGroupSummaryCascadeUpdate
    {
      get
      {
        return this.AllowGroupSummaryCascadeUpdate;
      }
    }

    TableViewBehavior ITableView.TableViewBehavior
    {
      get
      {
        return this.TableViewBehavior;
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
        return TableView.ActualDataRowTemplateSelectorPropertyKey;
      }
    }

    bool ITableView.IsCheckBoxSelectorColumnVisible
    {
      get
      {
        return this.IsCheckBoxSelectorColumnVisibleCore;
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
        return false;
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

    internal override bool AllowPartialGroupingCore
    {
      get
      {
        return this.AllowPartialGrouping;
      }
    }

    protected internal override bool ShowGroupSummaryFooter
    {
      get
      {
        if (this.ShowGroupFooters)
          return this.GridDataProvider.GroupedColumnCount > 0;
        return false;
      }
    }

    internal override bool AllowMasterDetailCore
    {
      get
      {
        return this.AllowMasterDetail;
      }
    }

    protected internal override bool ShouldDisplayBottomRow
    {
      get
      {
        return this.ActualNewItemRowPosition == NewItemRowPosition.Bottom;
      }
    }

    internal GridColumn CheckBoxSelectorColumn { get; private set; }

    private bool IsCheckBoxSelectorColumnVisibleCore
    {
      get
      {
        return this.isCheckBoxSelectorColumnVisibleCore;
      }
      set
      {
        if (this.isCheckBoxSelectorColumnVisibleCore == value)
          return;
        this.isCheckBoxSelectorColumnVisibleCore = value;
        this.OnIsCheckBoxSelectorColumnVisibleChanged();
      }
    }

    protected internal override bool IsGroupRowOptimized
    {
      get
      {
        return this.TableViewBehavior.UseLightweightTemplatesHasFlag(DevExpress.Xpf.Grid.UseLightweightTemplates.GroupRow);
      }
    }

    bool ITableView.NewItemRowIsDisplayed
    {
      get
      {
        return this.ActualNewItemRowPosition != NewItemRowPosition.None;
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
        TableView tableView = this.EventTargetView as TableView;
        if (tableView != null)
          return tableView.ScrollBarCustomRowAnnotation != null;
        return false;
      }
    }

    /// <summary>
    ///                 <para>Provides the ability to customize the cell merging behavior.
    /// </para>
    ///             </summary>
    [Category("Options Layout")]
    public event CellMergeEventHandler CellMerge;

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

    /// <summary>
    ///     <para> </para>
    /// </summary>
    [Category("Events")]
    public event EventHandler<EditFormShowingEventArgs> EditFormShowing;

    /// <summary>
    ///                 <para>Enables you to manually calculate the optimal width for a column(s).
    /// 
    /// </para>
    ///             </summary>
    [Category("BestFit")]
    public event CustomBestFitEventHandler CustomBestFit
    {
      add
      {
        this.AddHandler(TableView.CustomBestFitEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.CustomBestFitEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to initialize new rows with default values.
    /// 
    /// 
    /// </para>
    ///             </summary>
    [Category("Data")]
    public event InitNewRowEventHandler InitNewRow
    {
      add
      {
        this.AddHandler(TableView.InitNewRowEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.InitNewRowEvent, (Delegate) value);
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
        this.AddHandler(TableView.CustomScrollAnimationEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.CustomScrollAnimationEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when a row is double-clicked.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowDoubleClickEventHandler RowDoubleClick
    {
      add
      {
        this.AddHandler(TableView.RowDoubleClickEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.RowDoubleClickEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires before showing a group footer.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event ShowingGroupFooterEventHandler ShowingGroupFooter
    {
      add
      {
        this.AddHandler(TableView.ShowingGroupFooterEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.ShowingGroupFooterEvent, (Delegate) value);
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
        this.AddHandler(TableView.ScrollBarAnnotationsCreatingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(TableView.ScrollBarAnnotationsCreatingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Allows creating a new scrollbar annotation based on data row values and a row handle.
    /// 
    /// </para>
    ///             </summary>
    public event EventHandler<ScrollBarCustomRowAnnotationEventArgs> ScrollBarCustomRowAnnotation;

    static TableView()
    {
      Type ownerType = typeof (TableView);
      TableView.ColumnBandChooserTemplateProperty = TableViewBehavior.RegisterColumnBandChooserTemplateProperty(ownerType);
      TableView.FixedNoneContentWidthPropertyKey = TableViewBehavior.RegisterFixedNoneContentWidthProperty(ownerType);
      TableView.FixedNoneContentWidthProperty = TableView.FixedNoneContentWidthPropertyKey.DependencyProperty;
      TableView.TotalSummaryFixedNoneContentWidthPropertyKey = TableViewBehavior.RegisterTotalSummaryFixedNoneContentWidthProperty(ownerType);
      TableView.TotalSummaryFixedNoneContentWidthProperty = TableView.TotalSummaryFixedNoneContentWidthPropertyKey.DependencyProperty;
      TableView.VerticalScrollBarWidthPropertyKey = TableViewBehavior.RegisterVerticalScrollBarWidthProperty(ownerType);
      TableView.VerticalScrollBarWidthProperty = TableView.VerticalScrollBarWidthPropertyKey.DependencyProperty;
      TableView.FixedLeftContentWidthPropertyKey = TableViewBehavior.RegisterFixedLeftContentWidthProperty(ownerType);
      TableView.FixedLeftContentWidthProperty = TableView.FixedLeftContentWidthPropertyKey.DependencyProperty;
      TableView.FixedRightContentWidthPropertyKey = TableViewBehavior.RegisterFixedRightContentWidthProperty(ownerType);
      TableView.FixedRightContentWidthProperty = TableView.FixedRightContentWidthPropertyKey.DependencyProperty;
      TableView.TotalGroupAreaIndentPropertyKey = TableViewBehavior.RegisterTotalGroupAreaIndentProperty(ownerType);
      TableView.TotalGroupAreaIndentProperty = TableView.TotalGroupAreaIndentPropertyKey.DependencyProperty;
      TableView.IndicatorHeaderWidthPropertyKey = TableViewBehavior.RegisterIndicatorHeaderWidthProperty(ownerType);
      TableView.IndicatorHeaderWidthProperty = TableView.IndicatorHeaderWidthPropertyKey.DependencyProperty;
      TableView.ActualDataRowTemplateSelectorPropertyKey = TableViewBehavior.RegisterActualDataRowTemplateSelectorProperty(ownerType);
      TableView.ActualDataRowTemplateSelectorProperty = TableView.ActualDataRowTemplateSelectorPropertyKey.DependencyProperty;
      TableView.BestFitMaxRowCountProperty = DependencyPropertyManager.Register("BestFitMaxRowCount", typeof (int), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) -1, (PropertyChangedCallback) null, (CoerceValueCallback) ((d, baseValue) => (object) DataViewBase.CoerceBestFitMaxRowCount(Convert.ToInt32(baseValue)))));
      TableView.BestFitModeProperty = DependencyPropertyManager.Register("BestFitMode", typeof (BestFitMode), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) BestFitMode.Default));
      TableView.BestFitAreaProperty = DependencyPropertyManager.Register("BestFitArea", typeof (BestFitArea), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) BestFitArea.All));
      TableView.CustomBestFitEvent = EventManager.RegisterRoutedEvent("CustomBestFit", RoutingStrategy.Direct, typeof (CustomBestFitEventHandler), ownerType);
      TableView.FocusedRowBorderTemplateProperty = TableViewBehavior.RegisterFocusedRowBorderTemplateProperty(ownerType);
      TableView.AutoWidthProperty = TableViewBehavior.RegisterAutoWidthProperty(ownerType);
      TableView.UseGroupShadowIndentProperty = TableViewBehavior.RegisterUseGroupShadowIndentProperty(ownerType);
      TableView.LeftDataAreaIndentProperty = TableViewBehavior.RegisterLeftDataAreaIndentProperty(ownerType);
      TableView.RightDataAreaIndentProperty = TableViewBehavior.RegisterRightDataAreaIndentProperty(ownerType);
      TableView.ShowAutoFilterRowProperty = TableViewBehavior.RegisterShowAutoFilterRowProperty(ownerType);
      TableView.AllowCascadeUpdateProperty = TableViewBehavior.RegisterAllowCascadeUpdateProperty(ownerType);
      TableView.AllowPerPixelScrollingProperty = TableViewBehavior.RegisterAllowPerPixelScrollingProperty(ownerType);
      TableView.ScrollAnimationDurationProperty = TableViewBehavior.RegisterScrollAnimationDurationProperty(ownerType);
      TableView.ScrollAnimationModeProperty = TableViewBehavior.RegisterScrollAnimationModeProperty(ownerType);
      TableView.AllowScrollAnimationProperty = TableViewBehavior.RegisterAllowScrollAnimationProperty(ownerType);
      TableView.ExtendScrollBarToFixedColumnsProperty = TableViewBehavior.RegisterExtendScrollBarToFixedColumnsProperty(ownerType);
      TableView.FixedLeftVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedLeftVisibleColumnsProperty<GridColumn>(ownerType);
      TableView.FixedLeftVisibleColumnsProperty = TableView.FixedLeftVisibleColumnsPropertyKey.DependencyProperty;
      TableView.FixedRightVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedRightVisibleColumnsProperty<GridColumn>(ownerType);
      TableView.FixedRightVisibleColumnsProperty = TableView.FixedRightVisibleColumnsPropertyKey.DependencyProperty;
      TableView.FixedNoneVisibleColumnsPropertyKey = TableViewBehavior.RegisterFixedNoneVisibleColumnsProperty<GridColumn>(ownerType);
      TableView.FixedNoneVisibleColumnsProperty = TableView.FixedNoneVisibleColumnsPropertyKey.DependencyProperty;
      TableView.HorizontalViewportPropertyKey = TableViewBehavior.RegisterHorizontalViewportProperty(ownerType);
      TableView.HorizontalViewportProperty = TableView.HorizontalViewportPropertyKey.DependencyProperty;
      TableView.FixedLineWidthProperty = TableViewBehavior.RegisterFixedLineWidthProperty(ownerType);
      TableView.ShowVerticalLinesProperty = TableViewBehavior.RegisterShowVerticalLinesProperty(ownerType);
      TableView.ShowHorizontalLinesProperty = TableViewBehavior.RegisterShowHorizontalLinesProperty(ownerType);
      TableView.RowDecorationTemplateProperty = TableViewBehavior.RegisterRowDecorationTemplateProperty(ownerType);
      TableView.DefaultDataRowTemplateProperty = TableViewBehavior.RegisterDefaultDataRowTemplateProperty(ownerType);
      TableView.DataRowTemplateProperty = TableViewBehavior.RegisterDataRowTemplateProperty(ownerType);
      TableView.DataRowTemplateSelectorProperty = TableViewBehavior.RegisterDataRowTemplateSelectorProperty(ownerType);
      TableView.RowIndicatorContentTemplateProperty = TableViewBehavior.RegisterRowIndicatorContentTemplateProperty(ownerType);
      TableView.AllowResizingProperty = TableViewBehavior.RegisterAllowResizingProperty(ownerType);
      TableView.AllowHorizontalScrollingVirtualizationProperty = TableViewBehavior.RegisterAllowHorizontalScrollingVirtualizationProperty(ownerType);
      TableView.RowStyleProperty = TableViewBehavior.RegisterRowStyleProperty(ownerType);
      TableView.ScrollingVirtualizationMarginPropertyKey = TableViewBehavior.RegisterScrollingVirtualizationMarginProperty(ownerType);
      TableView.ScrollingVirtualizationMarginProperty = TableView.ScrollingVirtualizationMarginPropertyKey.DependencyProperty;
      TableView.ScrollingHeaderVirtualizationMarginPropertyKey = TableViewBehavior.RegisterScrollingHeaderVirtualizationMarginProperty(ownerType);
      TableView.ScrollingHeaderVirtualizationMarginProperty = TableView.ScrollingHeaderVirtualizationMarginPropertyKey.DependencyProperty;
      TableView.RowMinHeightProperty = TableViewBehavior.RegisterRowMinHeightProperty(ownerType);
      TableView.HeaderPanelMinHeightProperty = TableViewBehavior.RegisterHeaderPanelMinHeightProperty(ownerType);
      TableView.AutoMoveRowFocusProperty = TableViewBehavior.RegisterAutoMoveRowFocusProperty(ownerType);
      TableView.AllowBestFitProperty = TableViewBehavior.RegisterAllowBestFitProperty(ownerType);
      TableView.ShowIndicatorProperty = TableViewBehavior.RegisterShowIndicatorProperty(ownerType);
      TableView.ActualShowIndicatorPropertyKey = TableViewBehavior.RegisterActualShowIndicatorProperty(ownerType);
      TableView.ActualShowIndicatorProperty = TableView.ActualShowIndicatorPropertyKey.DependencyProperty;
      TableView.IndicatorWidthProperty = TableViewBehavior.RegisterIndicatorWidthProperty(ownerType);
      TableView.ActualIndicatorWidthPropertyKey = TableViewBehavior.RegisterActualIndicatorWidthPropertyKey(ownerType);
      TableView.ActualIndicatorWidthProperty = TableView.ActualIndicatorWidthPropertyKey.DependencyProperty;
      TableView.ShowTotalSummaryIndicatorIndentPropertyKey = TableViewBehavior.RegisterShowTotalSummaryIndicatorIndentPropertyKey(ownerType);
      TableView.ShowTotalSummaryIndicatorIndentProperty = TableView.ShowTotalSummaryIndicatorIndentPropertyKey.DependencyProperty;
      TableView.AlternateRowBackgroundProperty = TableViewBehavior.RegisterAlternateRowBackgroundProperty(ownerType);
      TableView.ActualAlternateRowBackgroundPropertyKey = TableViewBehavior.RegisterActualAlternateRowBackgroundProperty(ownerType);
      TableView.ActualAlternateRowBackgroundProperty = TableView.ActualAlternateRowBackgroundPropertyKey.DependencyProperty;
      TableView.EvenRowBackgroundProperty = TableViewBehavior.RegisterEvenRowBackgroundProperty(ownerType);
      TableView.UseEvenRowBackgroundProperty = TableViewBehavior.RegisterUseEvenRowBackgroundProperty(ownerType);
      TableView.AlternationCountProperty = TableViewBehavior.RegisterAlternationCountProperty(ownerType);
      TableView.ExpandDetailButtonWidthProperty = DependencyPropertyManager.Register("ExpandDetailButtonWidth", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 16.0, new PropertyChangedCallback(TableView.OnExpandDetailButtonWidthChanged)));
      TableView.ActualExpandDetailButtonWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualExpandDetailButtonWidth", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 16.0));
      TableView.ActualExpandDetailButtonWidthProperty = TableView.ActualExpandDetailButtonWidthPropertyKey.DependencyProperty;
      TableView.DetailMarginProperty = DependencyPropertyManager.Register("DetailMargin", typeof (Thickness), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(16.0, 0.0, 0.0, 0.0), new PropertyChangedCallback(TableView.OnDetailMarginChanged)));
      TableView.ActualDetailMarginPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualDetailMargin", typeof (Thickness), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(16.0, 0.0, 0.0, 0.0)));
      TableView.ActualDetailMarginProperty = TableView.ActualDetailMarginPropertyKey.DependencyProperty;
      TableView.ActualExpandDetailHeaderWidthPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualExpandDetailHeaderWidth", typeof (double), ownerType, new PropertyMetadata((object) 0.0));
      TableView.ActualExpandDetailHeaderWidthProperty = TableView.ActualExpandDetailHeaderWidthPropertyKey.DependencyProperty;
      TableView.MultiSelectModeProperty = TableViewBehavior.RegisterMultiSelectModeProperty(ownerType);
      TableView.UseIndicatorForSelectionProperty = TableViewBehavior.RegisterUseIndicatorForSelectionProperty(ownerType);
      TableView.AllowFixedColumnMenuProperty = TableViewBehavior.RegisterAllowFixedColumnMenuProperty(ownerType);
      TableView.AllowScrollHeadersProperty = TableViewBehavior.RegisterAllowScrollHeadersProperty(ownerType);
      TableView.ShowBandsPanelProperty = TableViewBehavior.RegisterShowBandsPanelProperty(ownerType);
      TableView.AllowChangeColumnParentProperty = TableViewBehavior.RegisterAllowChangeColumnParentProperty(ownerType);
      TableView.AllowChangeBandParentProperty = TableViewBehavior.RegisterAllowChangeBandParentProperty(ownerType);
      TableView.ShowBandsInCustomizationFormProperty = TableViewBehavior.RegisterShowBandsInCustomizationFormProperty(ownerType);
      TableView.AllowBandMovingProperty = TableViewBehavior.RegisterAllowBandMovingProperty(ownerType);
      TableView.AllowBandResizingProperty = TableViewBehavior.RegisterAllowBandResizingProperty(ownerType);
      TableView.AllowAdvancedVerticalNavigationProperty = TableViewBehavior.RegisterAllowAdvancedVerticalNavigationProperty(ownerType);
      TableView.AllowAdvancedHorizontalNavigationProperty = TableViewBehavior.RegisterAllowAdvancedHorizontalNavigationProperty(ownerType);
      TableView.ColumnChooserBandsSortOrderComparerProperty = TableViewBehavior.RegisterColumnChooserBandsSortOrderComparerProperty(ownerType);
      TableView.BandHeaderTemplateProperty = TableViewBehavior.RegisterBandHeaderTemplateProperty(ownerType);
      TableView.BandHeaderTemplateSelectorProperty = TableViewBehavior.RegisterBandHeaderTemplateSelectorProperty(ownerType);
      TableView.BandHeaderToolTipTemplateProperty = TableViewBehavior.RegisterBandHeaderToolTipTemplateProperty(ownerType);
      TableView.PrintBandHeaderStyleProperty = TableViewBehavior.RegisterPrintBandHeaderStyleProperty(ownerType);
      TableView.AllowBandMultiRowProperty = TableViewBehavior.RegisterAllowBandMultiRowProperty(ownerType);
      TableView.HasDetailViewsPropertyKey = DependencyPropertyManager.RegisterReadOnly("HasDetailViews", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TableView.HasDetailViewsProperty = TableView.HasDetailViewsPropertyKey.DependencyProperty;
      TableView.ShowDetailButtonsProperty = DependencyPropertyManager.Register("ShowDetailButtons", typeof (DefaultBoolean), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) DefaultBoolean.Default, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnShowDetailButtonsChanged())));
      TableView.AllowMasterDetailProperty = DependencyPropertyManager.Register("AllowMasterDetail", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnAllowMasterDetailChanged())));
      TableView.ActualShowDetailButtonsPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualShowDetailButtons", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnActualShowDetailButtonsChanged())));
      TableView.ActualShowDetailButtonsProperty = TableView.ActualShowDetailButtonsPropertyKey.DependencyProperty;
      TableView.IsDetailButtonVisibleBindingContainerPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsDetailButtonVisibleBindingContainer", typeof (BindingContainer), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.IsDetailButtonVisibleBindingContainerProperty = TableView.IsDetailButtonVisibleBindingContainerPropertyKey.DependencyProperty;
      TableView.NewItemRowPositionProperty = DependencyPropertyManager.Register("NewItemRowPosition", typeof (NewItemRowPosition), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) NewItemRowPosition.None, new PropertyChangedCallback(TableView.OnNewItemRowPositionChanged)));
      TableView.InitNewRowEvent = EventManager.RegisterRoutedEvent("InitNewRow", RoutingStrategy.Direct, typeof (InitNewRowEventHandler), ownerType);
      TableView.CustomScrollAnimationEvent = TableViewBehavior.RegisterCustomScrollAnimationEvent(ownerType);
      TableView.RowDoubleClickEvent = EventManager.RegisterRoutedEvent("RowDoubleClick", RoutingStrategy.Direct, typeof (RowDoubleClickEventHandler), ownerType);
      TableView.ShowingGroupFooterEvent = EventManager.RegisterRoutedEvent("ShowingGroupFooter", RoutingStrategy.Direct, typeof (ShowingGroupFooterEventHandler), ownerType);
      TableView.GroupFooterRowStyleProperty = DependencyPropertyManager.Register("GroupFooterRowStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.GroupFooterRowTemplateProperty = DependencyPropertyManager.Register("GroupFooterRowTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).UpdateActualGroupFooterRowTemplateSelector())));
      TableView.GroupFooterRowTemplateSelectorProperty = DependencyPropertyManager.Register("GroupFooterRowTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).UpdateActualGroupFooterRowTemplateSelector())));
      TableView.ActualGroupFooterRowTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupFooterRowTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.ActualGroupFooterRowTemplateSelectorProperty = TableView.ActualGroupFooterRowTemplateSelectorPropertyKey.DependencyProperty;
      TableView.GroupFooterSummaryContentStyleProperty = DependencyPropertyManager.Register("GroupFooterSummaryContentStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.GroupFooterSummaryItemTemplateProperty = DependencyPropertyManager.Register("GroupFooterSummaryItemTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).UpdateActualGroupFooterSummaryItemTemplateSelector())));
      TableView.GroupFooterSummaryItemTemplateSelectorProperty = DependencyPropertyManager.Register("GroupFooterSummaryItemTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).UpdateActualGroupFooterSummaryItemTemplateSelector())));
      TableView.ActualGroupFooterSummaryItemTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupFooterSummaryItemTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.ActualGroupFooterSummaryItemTemplateSelectorProperty = TableView.ActualGroupFooterSummaryItemTemplateSelectorPropertyKey.DependencyProperty;
      TableView.PrintRowTemplateProperty = DependencyPropertyManager.Register("PrintRowTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      TableView.PrintGroupFooterTemplateProperty = DependencyPropertyManager.Register("PrintGroupFooterTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new UIPropertyMetadata((PropertyChangedCallback) null));
      TableView.PrintAutoWidthProperty = DependencyPropertyManager.Register("PrintAutoWidth", typeof (bool), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) true));
      TableView.PrintColumnHeadersProperty = DependencyPropertyManager.Register("PrintColumnHeaders", typeof (bool), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) true));
      TableView.PrintBandHeadersProperty = DependencyPropertyManager.Register("PrintBandHeaders", typeof (bool), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) true));
      TableView.PrintGroupFootersProperty = DependencyPropertyManager.Register("PrintGroupFooters", typeof (bool), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) true));
      TableView.AllowPrintDetailsProperty = DependencyPropertyManager.Register("AllowPrintDetails", typeof (DefaultBoolean), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) DefaultBoolean.Default));
      TableView.AllowPrintEmptyDetailsProperty = DependencyPropertyManager.Register("AllowPrintEmptyDetails", typeof (DefaultBoolean), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) DefaultBoolean.Default));
      TableView.PrintAllDetailsProperty = DependencyPropertyManager.Register("PrintAllDetails", typeof (DefaultBoolean), ownerType, (PropertyMetadata) new UIPropertyMetadata((object) DefaultBoolean.Default));
      TableView.PrintColumnHeaderStyleProperty = DependencyPropertyManager.Register("PrintColumnHeaderStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TableView.PrintGroupFooterStyleProperty = DependencyPropertyManager.Register("PrintGroupFooterStyle", typeof (Style), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
      TableView.PrintDetailTopIndentProperty = DependencyPropertyManager.Register("PrintDetailTopIndent", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 4.0, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TableView.PrintDetailBottomIndentProperty = DependencyPropertyManager.Register("PrintDetailBottomIndent", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 10.0, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TableView.LeftGroupAreaIndentProperty = DependencyPropertyManager.Register("LeftGroupAreaIndent", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).RebuildVisibleColumns())));
      TableView.RightGroupAreaIndentProperty = DependencyPropertyManager.Register("RightGroupAreaIndent", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).RebuildVisibleColumns())));
      TableView.PrintGroupSummaryDisplayModeProperty = DependencyPropertyManager.Register("PrintGroupSummaryDisplayMode", typeof (GroupSummaryDisplayMode), ownerType, new PropertyMetadata((object) GroupSummaryDisplayMode.Default));
      TableView.AutoFilterRowCellStyleProperty = DependencyPropertyManager.Register("AutoFilterRowCellStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TableView.NewItemRowCellStyleProperty = DependencyPropertyManager.Register("NewItemRowCellStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, new PropertyChangedCallback(DataViewBase.OnUpdateColumnsAppearance)));
      TableView.AllowFixedGroupsProperty = DependencyPropertyManager.Register("AllowFixedGroups", typeof (DefaultBoolean), ownerType, new PropertyMetadata((object) DefaultBoolean.Default, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnAllowFixedGroupsChanged())));
      TableView.GroupSummaryDisplayModeProperty = DependencyPropertyManager.Register("GroupSummaryDisplayMode", typeof (GroupSummaryDisplayMode), ownerType, new PropertyMetadata((object) GroupSummaryDisplayMode.Default, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnGroupSummaryDisplayModeChanged())));
      TableView.GroupColumnSummaryItemTemplateProperty = DependencyPropertyManager.Register("GroupColumnSummaryItemTemplate", typeof (DataTemplate), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateGroupSummaryTemplates())));
      TableView.GroupColumnSummaryContentStyleProperty = DependencyPropertyManager.Register("GroupColumnSummaryContentStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridViewBase) d).UpdateGroupSummaryTemplates())));
      TableView.GroupColumnFooterElementStyleProperty = DependencyPropertyManager.Register("GroupColumnFooterElementStyle", typeof (Style), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataViewBase) d).OnSummaryDataChanged())));
      TableView.GroupBandSummaryContentStyleProperty = DependencyPropertyManager.Register("GroupBandSummaryContentStyle", typeof (Style), ownerType, new PropertyMetadata((PropertyChangedCallback) null));
      TableView.AllowGroupSummaryCascadeUpdateProperty = DependencyPropertyManager.Register("AllowGroupSummaryCascadeUpdate", typeof (bool), ownerType, new PropertyMetadata((object) false));
      TableView.VerticalScrollbarVisibilityProperty = DependencyPropertyManager.Register("VerticalScrollbarVisibility", typeof (ScrollBarVisibility), ownerType, new PropertyMetadata((object) ScrollBarVisibility.Visible));
      TableView.HorizontalScrollbarVisibilityProperty = DependencyPropertyManager.Register("HorizontalScrollbarVisibility", typeof (ScrollBarVisibility), ownerType, new PropertyMetadata((object) ScrollBarVisibility.Auto));
      TableView.ExpandColumnPositionPropertyKey = DependencyPropertyManager.RegisterReadOnly("ExpandColumnPosition", typeof (ColumnPosition), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) ColumnPosition.Middle));
      TableView.ExpandColumnPositionProperty = TableView.ExpandColumnPositionPropertyKey.DependencyProperty;
      TableView.ShowGroupFootersProperty = DependencyPropertyManager.Register("ShowGroupFooters", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnGroupSummaryDisplayModeChanged())));
      TableView.AllowPartialGroupingProperty = DependencyPropertyManager.Register("AllowPartialGrouping", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnAllowPartialGroupingChanged())));
      TableView.ShowDataNavigatorProperty = DependencyPropertyManager.Register("ShowDataNavigator", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      TableView.RegisterClassCommandBindings();
      TableView.RegisterSerializationEvents();
      CloneDetailHelper.RegisterKnownPropertyKeys(ownerType, TableView.FixedNoneContentWidthPropertyKey, TableView.ActualShowIndicatorPropertyKey, TableView.ActualIndicatorWidthPropertyKey, TableView.ActualExpandDetailButtonWidthPropertyKey, TableView.IsDetailButtonVisibleBindingContainerPropertyKey, TableView.ActualDetailMarginPropertyKey, TableView.FixedLeftContentWidthPropertyKey, TableView.FixedRightContentWidthPropertyKey, TableView.TotalGroupAreaIndentPropertyKey);
      TableView.UseLightweightTemplatesProperty = TableViewBehavior.RegisterUseLightweightTemplatesProperty(ownerType);
      TableView.RowDetailsTemplateProperty = TableViewBehavior.RegisterRowDetailsTemplateProperty(ownerType);
      TableView.RowDetailsTemplateSelectorProperty = TableViewBehavior.RegisterRowDetailsTemplateSelectorProperty(ownerType);
      TableView.ActualRowDetailsTemplateSelectorPropertyKey = TableViewBehavior.RegisterActualRowDetailsTemplateSelectorProperty(ownerType);
      TableView.ActualRowDetailsTemplateSelectorProperty = TableView.ActualRowDetailsTemplateSelectorPropertyKey.DependencyProperty;
      TableView.RowDetailsVisibilityModeProperty = TableViewBehavior.RegisterRowDetailsVisibilityModeProperty(ownerType);
      TableView.ShowCheckBoxSelectorInGroupRowProperty = DependencyProperty.Register("ShowCheckBoxSelectorInGroupRow", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnShowCheckBoxSelectorInGroupRowChanged())));
      TableView.ShowCheckBoxSelectorColumnProperty = DependencyProperty.Register("ShowCheckBoxSelectorColumn", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((TableView) d).UpdateIsCheckBoxSelectorColumnVisible())));
      TableView.CheckBoxSelectorColumnWidthProperty = DependencyProperty.Register("CheckBoxSelectorColumnWidth", typeof (double), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) 75.0, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnCheckBoxSelectorColumnWidthChanged())));
      TableView.CheckBoxSelectorColumnHeaderTemplateProperty = DependencyProperty.Register("CheckBoxSelectorColumnHeaderTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).OnCheckBoxSelectorColumnHeaderTemplateChanged())));
      TableView.RetainSelectionOnClickOutsideCheckBoxSelectorProperty = DependencyProperty.Register("RetainSelectionOnClickOutsideCheckBoxSelector", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      TableView.ScrollBarAnnotationsCreatingEvent = EventManager.RegisterRoutedEvent("ScrollBarAnnotationsCreating", RoutingStrategy.Direct, typeof (ScrollBarAnnotationsCreatingEventHandler), ownerType);
      TableView.ScrollBarAnnotationModeProperty = DependencyPropertyManager.RegisterAttached("ScrollBarAnnotationMode", typeof (DevExpress.Xpf.Grid.ScrollBarAnnotationMode?), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).ScrollBarAnnotationModeChanged())));
      TableView.ScrollBarAnnotationsAppearanceProperty = DependencyProperty.Register("ScrollBarAnnotationsAppearance", typeof (ScrollBarAnnotationsAppearance), ownerType, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((TableView) d).ScrollBarAnnotationsAppearanceChanged(e.NewValue != null))));
      TableView.ShowCriteriaInAutoFilterRowProperty = TableViewBehavior.RegisterShowCriteriaInAutoFilterRowProperty(ownerType);
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TableView class with default settings.
    /// </para>
    ///             </summary>
    public TableView()
      : this((MasterNodeContainer) null, (MasterRowsContainer) null, (DataControlDetailDescriptor) null)
    {
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TableView class with the specified settings.
    /// </para>
    ///             </summary>
    /// <param name="masterRootNode">
    /// 
    /// 
    /// </param>
    /// <param name="masterRootDataItem">
    /// 
    /// 
    /// </param>
    /// <param name="detailDescriptor">
    /// 
    /// 
    /// </param>
    public TableView(MasterNodeContainer masterRootNode, MasterRowsContainer masterRootDataItem, DataControlDetailDescriptor detailDescriptor)
      : base(masterRootNode, masterRootDataItem, detailDescriptor)
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (TableView));
      AdditionalRowData additionalRowData = new AdditionalRowData((DataTreeBuilder) this.visualDataTreeBuilder);
      additionalRowData.RowHandle = new RowHandle(-2147483647);
      this.NewItemRowData = (RowData) additionalRowData;
      this.bandMenuControllerValue = this.CreateMenuControllerLazyValue();
      this.CheckBoxSelectorColumn = this.CreateCheckBoxSelectorColumn();
    }

    private static void RegisterClassCommandBindings()
    {
      Type type = typeof (TableView);
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.BestFitColumn, (ExecutedRoutedEventHandler) ((d, e) => ((TableView) d).BestFitColumn(e)), (CanExecuteRoutedEventHandler) ((d, e) => ((TableView) d).OnCanBestFitColumn(e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.BestFitColumns, (ExecutedRoutedEventHandler) ((d, e) => ((TableView) d).BestFitColumns()), (CanExecuteRoutedEventHandler) ((d, e) => ((TableView) d).OnCanBestFitColumns(e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) GridCommands.AddNewRow, (ExecutedRoutedEventHandler) ((d, e) => ((TableView) ((DataViewBase) d).MasterRootRowsContainer.FocusedView).AddNewRow()), (CanExecuteRoutedEventHandler) ((d, e) => DataViewBase.CanExecuteWithCheckActualView(e, (Func<bool>) (() => ((DataViewBase) d).MasterRootRowsContainer.FocusedView.CanAddNewRow())))));
    }

    protected internal override int FindRowHandle(DependencyObject element)
    {
      if (this.ActualAllowCellMerge)
      {
        TableView tableView = (TableView) this.RootView;
        Point hitTestPoint = tableView.ScrollContentPresenter.TranslatePoint(tableView.TableViewBehavior.LastMousePosition, (UIElement) tableView);
        TableViewHitInfo tableViewHitInfo = tableView.CalcHitInfo(hitTestPoint);
        if (tableViewHitInfo.InRowCell)
          return tableViewHitInfo.RowHandle;
      }
      return base.FindRowHandle(element);
    }

    private void BestFitColumn(ExecutedRoutedEventArgs e)
    {
      this.TableViewBehavior.BestFitColumn(e.Parameter);
    }

    private void OnCanBestFitColumn(CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.CanBestFitColumn(e.Parameter);
    }

    private void OnCanBestFitColumns(CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.TableViewBehavior.CanBestFitColumns();
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
      return (IDataAwareExportHelper) new TableViewDataAwareExportHelper(this, exportTarget, options);
    }

    internal override FrameworkElement CreateRowElement(RowData rowData)
    {
      return this.TableViewBehavior.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new RowControl(rowData)), (Func<FrameworkElement>) (() => base.CreateRowElement(rowData)), DevExpress.Xpf.Grid.UseLightweightTemplates.Row);
    }

    bool ITableView.UseRowDetailsTemplate(int rowHandle)
    {
      return this.TableViewBehavior.UseRowDetailsTemplate(rowHandle);
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
      this.TableViewBehavior.AddFormatConditionCore(formatCondition);
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
      this.TableViewBehavior.ShowFormatConditionDialogCore(column, dialogKind);
    }

    /// <summary>
    ///                 <para>Removes all format conditions.
    /// </para>
    ///             </summary>
    public void ClearFormatConditionsFromAllColumns()
    {
      this.TableViewBehavior.ClearFormatConditionsFromAllColumnsCore();
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
      this.TableViewBehavior.ClearFormatConditionsFromColumnCore(column);
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
      this.TableViewBehavior.ShowConditionalFormattingManagerCore(column);
    }

    private static void RegisterSerializationEvents()
    {
      EventManager.RegisterClassHandler(typeof (TableView), DXSerializer.CreateCollectionItemEvent, (Delegate) ((s, e) => ((TableView) s).OnDeserializeCreateCollectionItem(e)));
    }

    protected virtual void OnDeserializeCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
    {
      if (!(e.CollectionName == "FormatConditions"))
        return;
      this.TableViewBehavior.OnDeserializeCreateFormatCondition(e);
    }

    protected override void OnDeserializeStart(StartDeserializingEventArgs e)
    {
      base.OnDeserializeStart(e);
      this.TableViewBehavior.OnDeserializeFormatConditionsStart();
    }

    protected override void OnDeserializeEnd(EndDeserializingEventArgs e)
    {
      base.OnDeserializeEnd(e);
      this.TableViewBehavior.OnDeserializeFormatConditionsEnd();
    }

    private IClipboardManager<ColumnWrapper, RowBaseWrapper> CreateClipboardManager()
    {
      this.clipboardHelperManager = this.DataControl.BandsCore.Count > 0 ? (GridViewClipboardHelper) new BandedGridViewClipboardHelper(this, ExportTarget.Xlsx) : new GridViewClipboardHelper(this, ExportTarget.Xlsx);
      return (IClipboardManager<ColumnWrapper, RowBaseWrapper>) PrintHelper.ClipboardExportManagerInstance(typeof (ColumnWrapper), typeof (RowBaseWrapper), (object) this.clipboardHelperManager);
    }

    protected internal override bool SetDataAwareClipboardData()
    {
      try
      {
        this.SetActualClipboardOptions(this.OptionsClipboard);
        if (this.ClipboardManager != null && this.clipboardHelperManager != null && !this.clipboardHelperManager.CanCopyToClipboard())
          return false;
        System.Windows.Forms.DataObject dataObject = new System.Windows.Forms.DataObject();
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

    protected internal virtual void RaiseEditFormShowing(EditFormShowingEventArgs e)
    {
      EventHandler<EditFormShowingEventArgs> eventHandler = this.EditFormShowing;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    /// <summary>
    ///                 <para>Shows the edit form as a popup dialog window.
    /// </para>
    ///             </summary>
    public void ShowDialogEditForm()
    {
      this.TableViewEditFormManager.ShowDialogEditForm();
    }

    /// <summary>
    ///                 <para>Shows the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void ShowInlineEditForm()
    {
      this.TableViewEditFormManager.ShowInlineEditForm();
    }

    /// <summary>
    ///                 <para>Shows the edit form in a mode specified by the <see cref="P:DevExpress.Xpf.Grid.TableView.EditFormShowMode" /> property.
    /// </para>
    ///             </summary>
    public void ShowEditForm()
    {
      this.TableViewEditFormManager.ShowEditForm();
    }

    /// <summary>
    ///                 <para>Cancels all changes and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void HideEditForm()
    {
      this.TableViewEditFormManager.HideEditForm();
    }

    /// <summary>
    ///                 <para>Posts all changes to the data source and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    public void CloseEditForm()
    {
      this.TableViewEditFormManager.CloseEditForm();
    }

    protected internal override IEditFormManager CreateEditFormManager()
    {
      return (IEditFormManager) new EditFormManager((ITableView) this);
    }

    protected internal override IEditFormOwner CreateEditFormOwner()
    {
      return (IEditFormOwner) new EditFormOwner((ITableView) this);
    }

    private void OnAllowCellMergeChanged()
    {
      this.UpdateActualAllowCellMergeCore();
      this.UpdateCellMergingPanels();
    }

    protected internal override void UpdateActualAllowCellMergeCore()
    {
      this.ActualAllowCellMerge = (this.AllowCellMerge || this.DataControl != null && this.DataControl.countColumnCellMerge > 0) && (this.DataControl != null && this.DataControl.BandsLayoutCore == null && !this.IsMultiSelection) && this.NavigationStyle != GridViewNavigationStyle.Row;
    }

    protected override void OnActualAllowCellMergeChanged()
    {
      this.OnMultiSelectModeChanged();
      this.TableViewBehavior.UpdateViewRowData((UpdateRowDataDelegate) (rowData =>
      {
        rowData.UpdateSelectionState();
        rowData.UpdateCellsPanel();
      }));
    }

    protected internal override void UpdateCellMergingPanels()
    {
      this.UpdateAllRowData((UpdateRowDataDelegate) (data =>
      {
        if (!data.View.ActualAllowCellMerge)
          return;
        data.InvalidateCellsPanel();
        data.UpdateIsFocusedCell();
      }));
    }

    private void UpdateAllRowData(UpdateRowDataDelegate updateMethod)
    {
      if (this.DataControl == null)
        return;
      this.DataControl.UpdateAllDetailDataControls((Action<DataControlBase>) (dataControl => dataControl.viewCore.UpdateRowData(updateMethod, false, false)), (Action<DataControlBase>) null);
    }

    protected override void OnFocusedRowHandleChangedCore(int oldRowHandle)
    {
      base.OnFocusedRowHandleChangedCore(oldRowHandle);
      if (this.isEditorOpen || this.RowDetailsVisibilityMode != RowDetailsVisibilityMode.Collapsed && (this.RowDetailsTemplate != null || this.RowDetailsTemplateSelector != null))
        this.UpdateCellMergingPanels();
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.FocusedRow, false);
    }

    protected override bool IsCellMerged(int visibleIndex1, int visibleIndex2, ColumnBase column, bool checkRowData, int checkMDIndex)
    {
      bool result = false;
      this.CellMergeLocker.DoLockedActionIfNotLocked((Action) (() => result = this.IsCellMergeCore(visibleIndex1, visibleIndex2, column, checkRowData, checkMDIndex)));
      return result;
    }

    private bool IsCellMergeCore(int visibleIndex1, int visibleIndex2, ColumnBase column, bool checkRowData, int checkMDIndex)
    {
      if ((column.AllowCellMerge.HasValue ? (!column.AllowCellMerge.Value ? 1 : 0) : (!this.AllowCellMerge ? 1 : 0)) != 0)
        return false;
      int visibleIndexCore1 = this.DataControl.GetRowHandleByVisibleIndexCore(checkMDIndex);
      if (this.DataControl.MasterDetailProvider.IsMasterRowExpanded(visibleIndexCore1, (DetailDescriptorBase) null) || this.ViewBehavior.UseRowDetailsTemplate(visibleIndexCore1))
        return false;
      int visibleIndexCore2 = this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex2);
      if (this.DataControl.IsGroupRowHandleCore(visibleIndexCore2) || !this.DataControl.IsValidRowHandleCore(visibleIndexCore2))
        return false;
      if (checkRowData)
      {
        RowData rowData = this.GetRowData(visibleIndexCore2);
        if (rowData == null || !rowData.IsRowInView())
          return false;
      }
      int visibleIndexCore3 = this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex1);
      if (!this.AllowMergeEditor(column, visibleIndexCore2, visibleIndexCore3) || visibleIndexCore2 == -2147483647 || visibleIndexCore3 == -2147483647)
        return false;
      return this.RaiseCellMerge(column, visibleIndexCore2, visibleIndexCore3, true).Value;
    }

    protected virtual bool AllowMergeEditor(ColumnBase column, int rowHandle1, int rowHandle2)
    {
      if (!this.isEditorOpen || !this.IsKeyboardFocusWithin || this.IsKeyboardFocusInSearchPanel() || (rowHandle1 != this.FocusedRowHandle && rowHandle2 != this.FocusedRowHandle || column != this.DataControl.CurrentColumn))
        return true;
      return !this.CanShowEditor(this.FocusedRowHandle, this.DataControl.CurrentColumn);
    }

    internal bool? RaiseCellMerge(ColumnBase column, int rowHandle1, int rowHandle2, bool checkValues)
    {
      object cellValue1 = this.DataControl.GetCellValue(rowHandle1, column.FieldName);
      object cellValue2 = this.DataControl.GetCellValue(rowHandle2, column.FieldName);
      TableView tableView = this.OriginationView == null ? this : this.OriginationView as TableView;
      if (tableView != null && tableView.CellMerge != null)
      {
        if (this.cellMergeEventArgs == null)
          this.cellMergeEventArgs = new CellMergeEventArgs();
        this.cellMergeEventArgs.SetArgs((GridColumn) column, rowHandle1, rowHandle2, cellValue1, cellValue2);
        tableView.CellMerge((object) this, this.cellMergeEventArgs);
        if (this.cellMergeEventArgs.Handled)
          return new bool?(this.cellMergeEventArgs.Merge);
      }
      if (checkValues)
        return new bool?(object.Equals(cellValue1, cellValue2));
      return new bool?();
    }

    protected internal override void OnOpeningEditor()
    {
      if (!this.ActualAllowCellMerge)
        return;
      this.isEditorOpen = true;
      this.UpdateFocusAndInvalidatePanels();
    }

    protected internal override void OnHideEditor(CellEditorBase editor, bool closeEditor)
    {
      base.OnHideEditor(editor, closeEditor);
      if (!this.ActualAllowCellMerge)
        return;
      this.isEditorOpen = !closeEditor;
      this.UpdateFocusAndInvalidatePanels();
    }

    private void UpdateFocusAndInvalidatePanels()
    {
      this.UpdateCellMergingPanels();
      this.ForceUpdateRowsState();
    }

    IGridView<ColumnWrapper, RowBaseWrapper> IGridViewFactory<ColumnWrapper, RowBaseWrapper>.GetIViewImplementerInstance()
    {
      if (this.DataControl != null && this.DataControl.BandsCore.Count > 0)
        return (IGridView<ColumnWrapper, RowBaseWrapper>) new BandedGridViewReportHelper<ColumnWrapper, RowBaseWrapper>(this, ExportTarget.Xlsx);
      return (IGridView<ColumnWrapper, RowBaseWrapper>) new GridViewReportHelper<ColumnWrapper, RowBaseWrapper>(this, ExportTarget.Xlsx);
    }

    void IGridViewFactory<ColumnWrapper, RowBaseWrapper>.ReleaseIViewImplementerInstance(IGridView<ColumnWrapper, RowBaseWrapper> instance)
    {
      instance.With<IGridView<ColumnWrapper, RowBaseWrapper>, IDisposable>((Func<IGridView<ColumnWrapper, RowBaseWrapper>, IDisposable>) (inst => inst as IDisposable)).Do<IDisposable>((Action<IDisposable>) (disp => disp.Dispose()));
    }

    Type IGridViewFactoryBase.GetViewType()
    {
      return typeof (TableView);
    }

    object IGridViewFactoryBase.GetDataSource()
    {
      return GridReportHelper.GetSource(this);
    }

    string IGridViewFactoryBase.GetDataMember()
    {
      return string.Empty;
    }

    private static void OnNewItemRowPositionChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((TableView) d).OnNewItemRowPositionChanged();
    }

    private static void OnExpandDetailButtonWidthChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TableView tableView = (TableView) d;
      if (tableView.DetailMargin.Left != tableView.ExpandDetailButtonWidth)
        tableView.DetailMargin = new Thickness((double) e.NewValue, 0.0, 0.0, 0.0);
      TableView.OnDetailMarginChanged((ITableView) tableView, e);
    }

    private static void OnDetailMarginChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TableView tableView = (TableView) d;
      if (tableView.ExpandDetailButtonWidth != tableView.DetailMargin.Left)
        tableView.ExpandDetailButtonWidth = tableView.DetailMargin.Left;
      TableView.OnDetailMarginChanged((ITableView) tableView, e);
    }

    private static void OnDetailMarginChanged(ITableView view, DependencyPropertyChangedEventArgs e)
    {
      view.TableViewBehavior.UpdateActualExpandDetailButtonWidth();
      view.TableViewBehavior.UpdateActualDetailMargin();
      if (view.ViewBase.OriginationView == null && view.ViewBase.DataControl != null)
        view.ViewBase.DataControl.UpdateAllDetailViewIndents();
      view.ViewBase.RebuildVisibleColumns();
    }

    internal override void UpdateActualFadeSelectionOnLostFocus(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      TableViewBehavior.OnFadeSelectionOnLostFocusChanged(d, e);
    }

    protected internal override bool CanAdjustScrollbar()
    {
      if (base.CanAdjustScrollbar())
        return !this.Grid.IsAsyncOperationInProgress;
      return false;
    }

    protected internal override void UpdateAlternateRowBackground()
    {
      this.ActualAlternateRowBackground = this.AlternateRowBackground ?? (this.UseEvenRowBackground ? this.EvenRowBackground : (Brush) null);
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
    public bool ShouldSerializeFixedLeftVisibleColumns(XamlDesignerSerializationManager manager)
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
    public bool ShouldSerializeFixedRightVisibleColumns(XamlDesignerSerializationManager manager)
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
    public bool ShouldSerializeFixedNoneVisibleColumns(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    ScrollBarAnnotationsCreatingEventArgs ITableView.RaiseScrollBarAnnotationsCreating()
    {
      ScrollBarAnnotationsCreatingEventArgs creatingEventArgs = new ScrollBarAnnotationsCreatingEventArgs(TableView.ScrollBarAnnotationsCreatingEvent, (object) this);
      this.RaiseEventInOriginationView((RoutedEventArgs) creatingEventArgs);
      return creatingEventArgs;
    }

    void ITableView.RaiseScrollBarCustomRowAnnotation(ScrollBarCustomRowAnnotationEventArgs e)
    {
      TableView tableView = this.EventTargetView as TableView;
      if (tableView == null)
        return;
      EventHandler<ScrollBarCustomRowAnnotationEventArgs> eventHandler = tableView.ScrollBarCustomRowAnnotation;
      if (eventHandler == null)
        return;
      eventHandler((object) this, e);
    }

    protected override DataViewBehavior CreateViewBehavior()
    {
      return (DataViewBehavior) new GridTableViewBehavior(this);
    }

    /// <summary>
    ///                 <para>Moves focus to the row preceding the one currently focused, and optionally allows the Automatic Filter Row to be focused.
    /// </para>
    ///             </summary>
    /// <param name="allowNavigateToAutoFilterRow">
    /// <b>true</b> to allow the Automatic Filter Row to be focused; otherwise, <b>false</b>.
    /// 
    ///           </param>
    public void MovePrevRow(bool allowNavigateToAutoFilterRow)
    {
      this.TableViewBehavior.MovePrevRow(allowNavigateToAutoFilterRow);
    }

    internal override bool IsNewItemRowHandle(int rowHandle)
    {
      return rowHandle == -2147483647;
    }

    private void OnNewItemRowPositionChanged()
    {
      this.ActualNewItemRowPosition = this.NewItemRowPosition;
    }

    protected override bool IsFirstNewRow()
    {
      if (this.NewItemRowPosition == NewItemRowPosition.Top)
        return this.FocusedRowHandle == -2147483647;
      return false;
    }

    private void OnActualNewItemRowPositionChanged(NewItemRowPosition oldValue)
    {
      if (this.FocusedRowHandle == -2147483647 && this.HasValidationError)
        this.CurrentCellEditor.CancelEditInVisibleEditor();
      if (this.DataControl != null)
        this.DataControl.ValidateMasterDetailConsistency();
      if (this.DataProviderBase != null)
        this.DataProviderBase.InvalidateVisibleIndicesCache();
      if (this.TableViewBehavior.IsNewItemRowVisible && this.DataControl != null)
        this.NewItemRowData.UpdateData();
      else if (this.ActualNewItemRowPosition == NewItemRowPosition.None && this.TableViewBehavior.IsNewItemRowFocused && this.Grid != null)
        this.SetFocusedRowHandle(this.Grid.GetRowHandleByVisibleIndex(0));
      if (this.DataPresenter != null && this.DataProviderBase != null)
        this.DataPresenter.ClearInvisibleItems();
      if ((oldValue == NewItemRowPosition.Bottom || this.ActualNewItemRowPosition == NewItemRowPosition.Bottom) && this.DataControl != null)
        this.DataControl.RaiseVisibleRowCountChanged();
      if (this.IsNewItemRowFocused && this.DataControl != null)
        this.OnFocusedRowHandleChangedCore(int.MinValue);
      this.InvalidateParentTree();
    }

    /// <summary>
    ///                 <para>Adds a new record.
    /// </para>
    ///             </summary>
    public void AddNewRow()
    {
      if (this.FocusedRowHandle == -2147483647)
        this.GridDataProvider.EndCurrentRowEdit();
      bool flag = this.ActualNewItemRowPosition != NewItemRowPosition.None;
      if (!flag)
        this.ActualNewItemRowPosition = NewItemRowPosition.Bottom;
      this.GridDataProvider.AddNewRow();
      this.GridDataProvider.BeginCurrentRowEdit();
      this.GetRowData(-2147483647).Do<RowData>((Action<RowData>) (x => x.UpdateData()));
      if (!flag)
        return;
      this.ScrollIntoView(-2147483647);
    }

    private void OnNewItemRowChanged()
    {
      if (!this.IsNewItemRowFocused || this.FocusedRowHandleChangedLocker.IsLocked)
        return;
      this.DataControl.UpdateCurrentItem();
      this.SelectionStrategy.OnFocusedRowDataChanged();
    }

    internal void OnStartNewItemRow()
    {
      this.OnNewItemRowChanged();
      this.RaiseEventInOriginationView((RoutedEventArgs) new InitNewRowEventArgs(TableView.InitNewRowEvent, (DataViewBase) this, -2147483647));
      this.GetRowData(-2147483647).Do<RowData>((Action<RowData>) (data => data.UpdateData()));
    }

    internal void OnEndNewItemRow()
    {
      this.GetRowData(-2147483647).Do<RowData>((Action<RowData>) (data => data.UpdateData()));
      this.OnNewItemRowChanged();
      this.ActualNewItemRowPosition = this.NewItemRowPosition;
    }

    protected internal override void MoveNextNewItemRowCell()
    {
      if (!this.ViewBehavior.NavigationStrategyBase.IsEndNavigationIndex((DataViewBase) this) || !this.CommitEditing())
        return;
      this.MoveFirstNavigationIndexCore(false);
      if (!this.IsBottomNewItemRowFocused)
        return;
      this.ScrollIntoView(this.FocusedRowHandle);
    }

    protected override SelectionStrategyBase CreateSelectionStrategy()
    {
      if (this.ActualAllowCellMerge)
        return (SelectionStrategyBase) new CellMergeSelectionStrategy((DataViewBase) this);
      DataViewBase rootView = this.RootView;
      TableView tableView = this;
      if (rootView.NavigationStyle == GridViewNavigationStyle.None)
        return (SelectionStrategyBase) new SelectionStrategyNavigationNone((DataViewBase) this);
      if (!tableView.IsMultiSelection)
        return (SelectionStrategyBase) new TableViewSelectionStrategyNone((GridViewBase) this);
      if (tableView.GetIsCheckBoxSelectorColumnVisible())
        return (SelectionStrategyBase) new SelectionStrategyCheckBoxRow(this);
      if (tableView.IsMultiRowSelection || rootView.NavigationStyle == GridViewNavigationStyle.Row)
      {
        if (!tableView.ShowSelectionRectangle || tableView.GetActualSelectionMode() == DevExpress.Xpf.Grid.MultiSelectMode.MultipleRow)
          return (SelectionStrategyBase) new SelectionStrategyRow((GridViewBase) this);
        return (SelectionStrategyBase) new SelectionStrategyRowRange((GridViewBase) this);
      }
      if (this.ShowSelectionRectangle && this.DataControl != null && this.DataControl.SelectionMode == DevExpress.Xpf.Grid.MultiSelectMode.Cell)
        return (SelectionStrategyBase) new SelectionStrategyCellRectangle(this);
      return (SelectionStrategyBase) new SelectionStrategyCell(this);
    }

    protected override DataViewCommandsBase CreateCommandsContainer()
    {
      return (DataViewCommandsBase) new TableViewCommands(this);
    }

    protected internal override FrameworkElement CreateGroupControl(GroupRowData rowData)
    {
      return this.TableViewBehavior.CreateElement((Func<FrameworkElement>) (() => (FrameworkElement) new GroupRowControl(rowData)), (Func<FrameworkElement>) (() => (FrameworkElement) new GroupGridRow()), DevExpress.Xpf.Grid.UseLightweightTemplates.GroupRow);
    }

    protected override ControlTemplate GetRowFocusedRectangleTemplate()
    {
      return this.FocusedRowBorderTemplate;
    }

    protected internal override DevExpress.Xpf.Grid.MultiSelectMode GetSelectionMode()
    {
      return SelectionModeHelper.ConvertToMultiSelectMode((TableViewSelectMode) this.GetValue(TableView.MultiSelectModeProperty));
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the table view.
    /// </para>
    ///             </summary>
    /// <param name="d">
    /// A <see cref="T:System.Windows.DependencyObject" /> object that represents the element contained within the table view.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TableViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public TableViewHitInfo CalcHitInfo(DependencyObject d)
    {
      return TableViewHitInfo.CalcHitInfo(d, (ITableView) this);
    }

    /// <summary>
    ///                 <para>Returns information about the specified element contained within the treelist view.
    /// </para>
    ///             </summary>
    /// <param name="hitTestPoint">
    /// A <see cref="T:System.Drawing.Point" /> structure which specifies the test point coordinates relative to the map's top-left corner.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.TableViewHitInfo" /> object that contains information about the specified view element.
    /// </returns>
    public TableViewHitInfo CalcHitInfo(Point hitTestPoint)
    {
      return TableViewHitInfo.CalcHitInfo(hitTestPoint, (ITableView) this);
    }

    internal override IDataViewHitInfo CalcHitInfoCore(DependencyObject source)
    {
      return (IDataViewHitInfo) this.CalcHitInfo(source);
    }

    /// <summary>
    ///                 <para>Returns the specified group footer's UI element.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Windows.FrameworkElement" /> descendant that is the specified group footer row element.
    /// </returns>
    public FrameworkElement GetGroupFooterRowElementByRowHandle(int rowHandle)
    {
      RowData rowData;
      if (this.VisualDataTreeBuilder.GroupSummaryRows.TryGetValue(rowHandle, out rowData))
        return rowData.WholeRowElement;
      return (FrameworkElement) null;
    }

    protected internal FrameworkElement GetGroupFooterSummaryElementByRowHandleAndColumn(int rowHandle, ColumnBase column)
    {
      FrameworkElement elementByRowHandle = this.GetGroupFooterRowElementByRowHandle(rowHandle);
      if (elementByRowHandle == null)
        return (FrameworkElement) null;
      return LayoutHelper.FindElement(elementByRowHandle, (Predicate<FrameworkElement>) (element =>
      {
        if (element is GroupFooterSummaryControl && ((GridColumnData) element.DataContext).Column == column)
          return element.Visibility == Visibility.Visible;
        return false;
      }));
    }

    protected internal override void UpdateUseLightweightTemplates()
    {
      if (!(this.RootView is ITableView))
        return;
      this.UseLightweightTemplates = ((ITableView) this.RootView).UseLightweightTemplates;
      this.TableViewBehavior.UpdateActualDataRowTemplateSelector();
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
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column which contains the cell.
    /// 
    ///           </param>
    public void SelectCell(int rowHandle, GridColumn column)
    {
      this.TableViewBehavior.SelectCell(rowHandle, (ColumnBase) column);
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
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column, containing the cell.
    /// 
    ///           </param>
    public void UnselectCell(int rowHandle, GridColumn column)
    {
      this.TableViewBehavior.UnselectCell(rowHandle, (ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Selects multiple cells.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the ending point of the selection.
    /// 
    ///           </param>
    public void SelectCells(int startRowHandle, GridColumn startColumn, int endRowHandle, GridColumn endColumn)
    {
      this.TableViewBehavior.SelectCells(startRowHandle, (ColumnBase) startColumn, endRowHandle, (ColumnBase) endColumn);
    }

    /// <summary>
    ///                 <para>Unselects the specified cells.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="startRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="startColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the starting point of the selection.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the end point of the selection.
    /// 
    ///           </param>
    public void UnselectCells(int startRowHandle, GridColumn startColumn, int endRowHandle, GridColumn endColumn)
    {
      this.TableViewBehavior.UnselectCells(startRowHandle, (ColumnBase) startColumn, endRowHandle, (ColumnBase) endColumn);
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
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that represents the column which contains the cell.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified cell is selected; otherwise, <b>false</b>.
    /// </returns>
    public bool IsCellSelected(int rowHandle, ColumnBase column)
    {
      return this.TableViewBehavior.IsCellSelected(rowHandle, column);
    }

    /// <summary>
    ///                 <para>Returns the selected data cells.
    /// </para>
    ///             </summary>
    /// <returns>The list of <see cref="T:DevExpress.Xpf.Grid.GridCell" /> objects that contain cell coordinates (row and column).
    /// </returns>
    public IList<GridCell> GetSelectedCells()
    {
      return (IList<GridCell>) new SimpleBridgeList<GridCell, CellBase>(this.TableViewBehavior.GetSelectedCells(), (Func<CellBase, GridCell>) null, (Func<GridCell, CellBase>) null);
    }

    protected internal virtual void ShowNewItemRow(NewItemRowPosition? position)
    {
      this.NewItemRowPosition = position.HasValue ? position.Value : NewItemRowPosition.Top;
      if (this.NewItemRowPosition == NewItemRowPosition.None || this.IsNewItemRowFocused)
        return;
      this.SetFocusedRowHandle(-2147483647);
      if (this.VisibleColumns.Count <= 0 || this.DataControl == null)
        return;
      this.DataControl.CurrentColumn = (ColumnBase) this.VisibleColumns[0];
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
        this.ClipboardController.CopyCellsToClipboard((IEnumerable<GridCell>) this.GetSelectedCells(), true);
    }

    public void CopyCellsToClipboard(IEnumerable<GridCell> gridCells)
    {
      this.ClipboardController.CopyCellsToClipboard(gridCells, false);
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
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the starting point.
    /// 
    ///           </param>
    /// <param name="endRowHandle">
    /// An integer value that specifies the handle of the row containing the cell that identifies the end point.
    /// 
    ///           </param>
    /// <param name="endColumn">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column containing the cell that identifies the end point.
    /// 
    ///           </param>
    public void CopyCellsToClipboard(int startRowHandle, GridColumn startColumn, int endRowHandle, GridColumn endColumn)
    {
      this.TableViewBehavior.CopyCellsToClipboard(startRowHandle, (ColumnBase) startColumn, endRowHandle, (ColumnBase) endColumn);
    }

    protected void CopyCellsToClipboardCore(IEnumerable<CellBase> gridCells)
    {
      this.CopyCellsToClipboard((IEnumerable<GridCell>) new SimpleEnumerableBridge<GridCell, CellBase>(gridCells));
    }

    protected internal override DataTemplate GetPrintRowTemplate()
    {
      return this.PrintRowTemplate;
    }

    protected override IRootDataNode CreateRootNode(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      return GridPrintingHelper.CreatePrintingTreeNode((ITableView) this, usablePageSize, (MasterDetailPrintInfo) null, (ItemsGenerationStrategyBase) null);
    }

    protected override void CreateRootNodeAsync(Size usablePageSize, Size reportHeaderSize, Size reportFooterSize, Size pageHeaderSize, Size pageFooterSize)
    {
      GridPrintingHelper.CreatePrintingTreeNodeAsync(this, usablePageSize, (MasterDetailPrintInfo) null);
    }

    protected override void PagePrintedCallback(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> brickUpdaters)
    {
      GridPrintingHelper.UpdatePageBricks(pageBrickEnumerator, brickUpdaters, false, this.PrintTotalSummary && this.ShowTotalSummary || this.PrintFixedTotalSummary && this.ShowFixedTotalSummary);
    }

    protected internal override PrintingDataTreeBuilderBase CreatePrintingDataTreeBuilder(double totalHeaderWidth, ItemsGenerationStrategyBase itemsGenerationStrategy, MasterDetailPrintInfo masterDetailPrintInfo, BandsLayoutBase bandsLayout)
    {
      return (PrintingDataTreeBuilderBase) new GridPrintingDataTreeBuilder(this, totalHeaderWidth, itemsGenerationStrategy, bandsLayout, masterDetailPrintInfo);
    }

    /// <summary>
    ///                 <para>Resizes the specified column to the minimum width required to display the column's contents completely.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> representing the grid column whose width should be optimized.
    /// 
    ///           </param>
    public void BestFitColumn(GridColumn column)
    {
      this.TableViewBehavior.BestFitColumn((ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Resizes all visible columns to optimally fit their contents.
    /// </para>
    ///             </summary>
    public void BestFitColumns()
    {
      this.TableViewBehavior.BestFitColumns();
    }

    /// <summary>
    ///                 <para>Returns the column's optimal (minimum) width required to display its contents completely.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the grid column.
    /// 
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Double" /> value that specifies the column's optimal (minimum) width required to display its contents completely.
    /// 
    /// </returns>
    public double CalcColumnBestFitWidth(GridColumn column)
    {
      return this.TableViewBehavior.CalcColumnBestFitWidthCore((ColumnBase) column);
    }

    protected internal virtual void RaiseCustomBestFit(CustomBestFitEventArgs e)
    {
      this.RaiseEventInOriginationView((RoutedEventArgs) e);
    }

    protected internal override void RaiseCustomScrollAnimation(CustomScrollAnimationEventArgs e)
    {
      e.RoutedEvent = TableView.CustomScrollAnimationEvent;
      base.RaiseCustomScrollAnimation(e);
    }

    internal bool CanBestFitColumn(object commandParameter)
    {
      GridColumn gridColumn = (GridColumn) this.GetColumnByCommandParameter(commandParameter);
      if (gridColumn != null)
        return this.TableViewBehavior.CanBestFitColumn((ColumnBase) gridColumn);
      return false;
    }

    void ITableView.SetHorizontalViewport(double value)
    {
      this.HorizontalViewport = value;
    }

    void ITableView.SetFixedLeftVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedLeftVisibleColumns = (IList<GridColumn>) GridViewBase.ConvertToGridColumnsList(columns);
    }

    void ITableView.SetFixedNoneVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedNoneVisibleColumns = (IList<GridColumn>) GridViewBase.ConvertToGridColumnsList(columns);
    }

    void ITableView.SetFixedRightVisibleColumns(IList<ColumnBase> columns)
    {
      this.FixedRightVisibleColumns = (IList<GridColumn>) GridViewBase.ConvertToGridColumnsList(columns);
    }

    void ITableView.CopyCellsToClipboard(IEnumerable<CellBase> gridCells)
    {
      this.CopyCellsToClipboardCore(gridCells);
    }

    CellBase ITableView.CreateGridCell(int rowHandle, ColumnBase column)
    {
      return (CellBase) new GridCell(rowHandle, (GridColumn) column);
    }

    ITableViewHitInfo ITableView.CalcHitInfo(DependencyObject d)
    {
      return (ITableViewHitInfo) TableViewHitInfo.CalcHitInfo(d, (ITableView) this);
    }

    void ITableView.SetActualShowIndicator(bool showIndicator)
    {
      this.ActualShowIndicator = showIndicator;
    }

    void ITableView.SetActualIndicatorWidth(double indicatorWidth)
    {
      this.ActualIndicatorWidth = indicatorWidth;
    }

    void ITableView.SetActualExpandDetailHeaderWidth(double expandDetailHeaderWidth)
    {
      this.ActualExpandDetailHeaderWidth = expandDetailHeaderWidth;
    }

    void ITableView.SetActualDetailMargin(Thickness detailMargin)
    {
      this.ActualDetailMargin = detailMargin;
    }

    void ITableView.SetActualFadeSelectionOnLostFocus(bool fadeSelectionOnLostFocus)
    {
      this.ActualFadeSelectionOnLostFocus = fadeSelectionOnLostFocus;
    }

    void ITableView.SetShowTotalSummaryIndicatorIndent(bool showTotalSummaryIndicatorIndent)
    {
      this.ShowTotalSummaryIndicatorIndent = showTotalSummaryIndicatorIndent;
    }

    void ITableView.RaiseRowDoubleClickEvent(ITableViewHitInfo hitInfo, MouseButton changedButton)
    {
      RowDoubleClickEventArgs doubleClickEventArgs = new RowDoubleClickEventArgs((GridViewHitInfoBase) hitInfo, changedButton, (DataViewBase) this);
      doubleClickEventArgs.RoutedEvent = TableView.RowDoubleClickEvent;
      this.RaiseEventInOriginationView((RoutedEventArgs) doubleClickEventArgs);
    }

    void ITableView.SetExpandColumnPosition(ColumnPosition position)
    {
      this.ExpandColumnPosition = position;
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

    private void OnAllowFixedGroupsChanged()
    {
      if (this.Grid == null)
        return;
      this.OnDataReset();
    }

    private void OnGroupSummaryDisplayModeChanged()
    {
      if (this.Grid == null)
        return;
      this.OnDataReset();
    }

    private void OnAllowPartialGroupingChanged()
    {
      if (this.DataControl == null)
        return;
      this.DataControl.UpdateAllowPartialGrouping();
      this.RebuildVisibleColumns();
    }

    internal override void PerformUpdateGroupSummaryDataAction(Action action)
    {
      if (this.GroupSummaryDisplayMode != GroupSummaryDisplayMode.AlignByColumns && !this.ShowGroupFooters)
        return;
      action();
    }

    DataViewBase IDetailElement<DataViewBase>.CreateNewInstance(params object[] args)
    {
      return Activator.CreateInstance(this.GetType(), (object) (MasterNodeContainer) args[0], (object) (MasterRowsContainer) args[1], (object) (DataControlDetailDescriptor) args[2]) as DataViewBase;
    }

    private void OnAllowMasterDetailChanged()
    {
      this.OnShowDetailButtonsChanged();
      if (this.DataControl == null)
        return;
      this.OnDataReset();
    }

    private void OnShowDetailButtonsChanged()
    {
      this.ActualShowDetailButtons = this.AllowMasterDetail && (this.HasDetailViews && this.ShowDetailButtons != DefaultBoolean.False || this.ShowDetailButtons == DefaultBoolean.True);
      if (this.Grid != null)
        this.Grid.UpdateAllDetailViewIndents();
      this.RebuildVisibleColumns();
    }

    private void OnActualShowDetailButtonsChanged()
    {
      this.TableViewBehavior.UpdateViewRowData((UpdateRowDataDelegate) (x => x.UpdateClientDetailExpandButtonVisibility()));
    }

    internal void UpdateHasDetailViews()
    {
      this.HasDetailViews = this.Grid.DetailDescriptor != null;
      this.OnShowDetailButtonsChanged();
    }

    internal override void BindDetailContainerNextLevelItemsControl(ItemsControl itemsControl)
    {
      itemsControl.SetBinding(ItemsControl.ItemsSourceProperty, (BindingBase) new System.Windows.Data.Binding("View.DataControl.DetailDescriptor.DataControlDetailDescriptors")
      {
        RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent)
      });
      itemsControl.SetBinding(UIElement.VisibilityProperty, (BindingBase) new System.Windows.Data.Binding("View.AllowMasterDetail")
      {
        RelativeSource = new RelativeSource(RelativeSourceMode.TemplatedParent),
        Converter = (IValueConverter) new BoolToVisibilityConverter()
      });
    }

    internal override void BindDetailContainerBorderSeparatorControl(FrameworkElement borderSeparator)
    {
      if (this.DataControl == null)
        return;
      borderSeparator.Visibility = this.DataControl.OwnerDetailDescriptor == null ? Visibility.Collapsed : Visibility.Visible;
    }

    internal override void ThrowNotSupportedInMasterDetailException()
    {
      if (this.IsMultiCellSelection || this.GetIsCheckBoxSelectorColumnVisible())
        throw new NotSupportedInMasterDetailException("Multiple cell selection and selection via the checkbox column is not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx");
    }

    internal override void ThrowNotSupportedInDetailException()
    {
      if (this.ShowAutoFilterRow)
        throw new NotSupportedInMasterDetailException("The Auto-Filter Row feature is not supported by detail grids. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx");
    }

    protected void UpdateFixedNoneContentWidth(RowData rowData)
    {
      this.TableViewBehavior.UpdateFixedNoneContentWidth((ColumnsRowDataBase) rowData);
    }

    protected internal override int CalcGroupSummaryVisibleRowCount()
    {
      if (this.ShowGroupSummaryFooter)
        return this.GridDataProvider.VisibleIndicesProvider.VisibleGroupSummaryRowCount;
      return base.CalcGroupSummaryVisibleRowCount();
    }

    protected internal virtual bool RaiseShowingGroupFooter(int rowHandle, int level)
    {
      ShowingGroupFooterEventArgs groupFooterEventArgs = new ShowingGroupFooterEventArgs(TableView.ShowingGroupFooterEvent, (GridViewBase) this, rowHandle, level);
      this.RaiseEventInOriginationView((RoutedEventArgs) groupFooterEventArgs);
      return groupFooterEventArgs.Allow;
    }

    private void UpdateActualGroupFooterRowTemplateSelector()
    {
      this.ActualGroupFooterRowTemplateSelector = (DataTemplateSelector) new ActualTemplateSelectorWrapper(this.GroupFooterRowTemplateSelector, this.GroupFooterRowTemplate);
    }

    private void UpdateActualGroupFooterSummaryItemTemplateSelector()
    {
      DataControlOriginationElementHelper.UpdateActualTemplateSelector((DependencyObject) this, (DependencyObject) this.OriginationView, TableView.ActualGroupFooterSummaryItemTemplateSelectorPropertyKey, this.GroupFooterSummaryItemTemplateSelector, this.GroupFooterSummaryItemTemplate, (Func<DataTemplateSelector, DataTemplate, DataTemplateSelector>) null);
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

    protected override void OnDataControlChanged(DataControlBase oldValue)
    {
      base.OnDataControlChanged(oldValue);
      oldValue.Do<DataControlBase>((Action<DataControlBase>) (dataControl => this.RemoveCheckBoxSelectorColumn(dataControl)));
      this.DataControl.Do<DataControlBase>((Action<DataControlBase>) (dataControl =>
      {
        this.AddCheckBoxSelectorColumn(dataControl);
        dataControl.ValidateMasterDetailConsistency();
      }));
      this.UpdateIsCheckBoxSelectorColumnVisible();
    }

    private void AddCheckBoxSelectorColumn(DataControlBase dataControl)
    {
      if (!this.GetIsCheckBoxSelectorColumnVisible() || this.CheckBoxSelectorColumn.OwnerControl == dataControl)
        return;
      this.CheckBoxSelectorColumn.OwnerControl = dataControl;
      dataControl.AddChild((FrameworkContentElement) this.CheckBoxSelectorColumn);
    }

    private void RemoveCheckBoxSelectorColumn(DataControlBase dataControl)
    {
      dataControl.RemoveChild((FrameworkContentElement) this.CheckBoxSelectorColumn);
      this.CheckBoxSelectorColumn.OwnerControl = (DataControlBase) null;
    }

    internal override ICommand GetColumnCommand(ColumnBase column)
    {
      if (this.CheckBoxSelectorColumn == column)
        return this.TableViewCommands.ToggleRowsSelection;
      return base.GetColumnCommand(column);
    }

    internal override void OnMultiSelectModeChanged()
    {
      this.UpdateIsCheckBoxSelectorColumnVisible();
      this.UpdateActualAllowCellMergeCore();
      base.OnMultiSelectModeChanged();
    }

    internal override bool IsExpandButton(IDataViewHitInfo hitInfo)
    {
      TableViewHitInfo tableViewHitInfo = (TableViewHitInfo) hitInfo;
      if (tableViewHitInfo.HitTest != TableViewHitTest.GroupRowButton)
        return tableViewHitInfo.HitTest == TableViewHitTest.MasterRowButton;
      return true;
    }

    internal override void UpdateColumns(Action<ColumnBase> updateColumnDelegate)
    {
      if (this.IsCheckBoxSelectorColumnVisibleCore)
        updateColumnDelegate((ColumnBase) this.CheckBoxSelectorColumn);
      base.UpdateColumns(updateColumnDelegate);
    }

    internal override Type GetColumnType(ColumnBase column, DataProviderBase dataProvider = null)
    {
      if (column == this.CheckBoxSelectorColumn)
        return typeof (bool);
      return base.GetColumnType(column, dataProvider);
    }

    protected internal override bool CanSortColumnCore(ColumnBase column, string fieldName, bool prohibitColumnProperty)
    {
      if (column == this.CheckBoxSelectorColumn)
        return false;
      return base.CanSortColumnCore(column, fieldName, prohibitColumnProperty);
    }

    internal static bool IsCheckBoxSelectorColumn(string fieldName)
    {
      return fieldName == "DX$CheckboxSelectorColumn";
    }

    private GridColumn CreateCheckBoxSelectorColumn()
    {
      GridColumn gridColumn = new GridColumn();
      gridColumn.FieldName = "DX$CheckboxSelectorColumn";
      gridColumn.Header = (object) this.GetLocalizedString(GridControlStringId.CheckboxSelectorColumnCaption);
      gridColumn.Width = (GridColumnWidth) this.CheckBoxSelectorColumnWidth;
      gridColumn.FixedWidth = true;
      gridColumn.ShowInColumnChooser = false;
      gridColumn.AllowResizing = DefaultBoolean.False;
      gridColumn.AllowSorting = DefaultBoolean.False;
      gridColumn.AllowGrouping = DefaultBoolean.False;
      gridColumn.AllowMoving = DefaultBoolean.False;
      gridColumn.AllowColumnFiltering = DefaultBoolean.False;
      gridColumn.AllowEditing = DefaultBoolean.False;
      gridColumn.AllowConditionalFormattingMenu = new bool?(false);
      gridColumn.FieldType = typeof (bool);
      gridColumn.VisibleIndex = 0;
      gridColumn.HeaderTemplate = this.CheckBoxSelectorColumnHeaderTemplate;
      gridColumn.HorizontalHeaderContentAlignment = System.Windows.HorizontalAlignment.Center;
      gridColumn.AllowPrinting = false;
      gridColumn.CellTemplateSelector = (DataTemplateSelector) new CheckBoxColumnTemplateSelector();
      ControlTemplate controlTemplate = new ControlTemplate();
      controlTemplate.Seal();
      gridColumn.AutoFilterRowDisplayTemplate = controlTemplate;
      gridColumn.AutoFilterRowEditTemplate = controlTemplate;
      return gridColumn;
    }

    internal override void PatchVisibleColumns(IList<ColumnBase> visibleColumns, bool hasFixedLeftColumns)
    {
      if (!this.IsCheckBoxSelectorColumnVisibleCore)
        return;
      if (hasFixedLeftColumns)
        this.CheckBoxSelectorColumn.Fixed = FixedStyle.Left;
      else
        this.CheckBoxSelectorColumn.Fixed = FixedStyle.None;
      visibleColumns.Insert(0, (ColumnBase) this.CheckBoxSelectorColumn);
    }

    protected override int? CompareGroupedColumns(BaseColumn x, BaseColumn y)
    {
      if (!this.AllowPartialGroupingCore)
        return new int?();
      if (x == this.CheckBoxSelectorColumn || y == this.CheckBoxSelectorColumn)
        return new int?();
      GridColumn gridColumn1 = x as GridColumn;
      GridColumn gridColumn2 = y as GridColumn;
      if (gridColumn1 == null || gridColumn2 == null || !gridColumn1.IsGrouped && !gridColumn2.IsGrouped)
        return new int?();
      if (!gridColumn1.IsGrouped)
        return new int?(1);
      if (!gridColumn2.IsGrouped)
        return new int?(-1);
      return new int?(Comparer<int>.Default.Compare(gridColumn1.GroupIndex, gridColumn2.GroupIndex));
    }

    internal override int AdjustVisibleIndex(ColumnBase column, int visibleIndex)
    {
      int num = base.AdjustVisibleIndex(column, visibleIndex);
      if (this.IsCheckBoxSelectorColumnVisibleCore && num == 0)
        num = 1;
      return num;
    }

    private void UpdateIsCheckBoxSelectorColumnVisible()
    {
      this.IsCheckBoxSelectorColumnVisibleCore = this.GetIsCheckBoxSelectorColumnVisible();
    }

    private bool GetIsCheckBoxSelectorColumnVisible()
    {
      if (this.ShowCheckBoxSelectorColumn)
        return this.IsMultiRowSelection;
      return false;
    }

    private void OnIsCheckBoxSelectorColumnVisibleChanged()
    {
      this.UpdateCheckBoxSelectorColumnOwnerControl();
      this.ResetSelectionStrategy();
      this.RebuildVisibleColumns();
      this.UpdateActualShowCheckBoxSelectorInGroupRow();
      if (!this.IsCheckBoxSelectorColumnVisibleCore)
        return;
      this.CheckBoxSelectorColumn.UpdateViewInfo(false);
      this.DataControl.Do<DataControlBase>((Action<DataControlBase>) (x => x.CurrentColumn = (ColumnBase) this.CheckBoxSelectorColumn));
    }

    private void UpdateCheckBoxSelectorColumnOwnerControl()
    {
      if (this.DataControl == null)
        return;
      if (this.IsCheckBoxSelectorColumnVisibleCore)
        this.AddCheckBoxSelectorColumn(this.DataControl);
      else
        this.RemoveCheckBoxSelectorColumn(this.DataControl);
    }

    internal void ToggleRowsSelection()
    {
      if (!this.RequestUIUpdate())
        return;
      this.SelectionStrategy.ToggleRowsSelection();
    }

    private void OnShowCheckBoxSelectorInGroupRowChanged()
    {
      this.UpdateActualShowCheckBoxSelectorInGroupRow();
      if (!this.ShowCheckBoxSelectorInGroupRow || !this.ActualShowCheckBoxSelectorInGroupRow)
        return;
      this.UpdateRowData((UpdateRowDataDelegate) (rowData =>
      {
        if (!(rowData is GroupRowData))
          return;
        ((GroupRowData) rowData).UpdateAllItemsSelected();
      }), true, true);
    }

    private void UpdateActualShowCheckBoxSelectorInGroupRow()
    {
      this.ActualShowCheckBoxSelectorInGroupRow = this.ShowCheckBoxSelectorInGroupRow && this.IsCheckBoxSelectorColumnVisibleCore;
    }

    private void OnCheckBoxSelectorColumnWidthChanged()
    {
      this.CheckBoxSelectorColumn.Width = (GridColumnWidth) this.CheckBoxSelectorColumnWidth;
    }

    private void OnCheckBoxSelectorColumnHeaderTemplateChanged()
    {
      this.CheckBoxSelectorColumn.HeaderTemplate = this.CheckBoxSelectorColumnHeaderTemplate;
    }

    internal override Style GetGroupSummaryElementStyle(bool groupFooter)
    {
      if (groupFooter)
        return this.GroupColumnFooterElementStyle;
      return base.GetGroupSummaryElementStyle(groupFooter);
    }

    private void ScrollBarAnnotationsAppearanceChanged(bool generation)
    {
      this._scrollBarAnnotationsManager = (ScrollBarAnnotationsManager) null;
      ((ITableView) this).ScrollBarAnnotationsManager.GridLoaded = true;
      if (this.ScrollBarAnnotationsAppearance != null)
        this.ScrollBarAnnotationsAppearance.Owner = new WeakReference((object) this);
      if (!this.IsRootView || !generation)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    private void ScrollBarAnnotationModeChanged()
    {
      if (this.IsRootView)
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

    protected internal override void OnSelectionChanged(DevExpress.Data.SelectionChangedEventArgs e)
    {
      base.OnSelectionChanged(e);
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration(DevExpress.Xpf.Grid.ScrollBarAnnotationMode.Selected, false);
    }

    protected internal override void OnDataChanged(bool rebuildVisibleColumns)
    {
      base.OnDataChanged(rebuildVisibleColumns);
      if (this.RootView == null || !((ITableView) this.RootView).ScrollBarAnnotationsManager.GridLoaded)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    internal override void CurrentRowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
    {
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationChanged(changedType, newHandle, oldRowHandle);
      base.CurrentRowChanged(changedType, newHandle, oldRowHandle);
    }

    internal override bool NeedWatchRowChanged()
    {
      if (base.NeedWatchRowChanged())
        return true;
      return ((ITableView) this).ScrollBarAnnotationModeActual.HasAnyFlag((Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.InvalidCells, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.InvalidRows, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.SearchResult, (Enum) DevExpress.Xpf.Grid.ScrollBarAnnotationMode.Custom);
    }

    protected internal override void OnDataReset()
    {
      base.OnDataReset();
      if (!((ITableView) this.RootView).ScrollBarAnnotationsManager.GridLoaded)
        return;
      ((ITableView) this).ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
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
