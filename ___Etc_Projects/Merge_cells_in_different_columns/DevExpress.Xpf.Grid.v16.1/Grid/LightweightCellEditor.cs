// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LightweightCellEditor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Displays the content of a data cell in the optimized mode.
  /// </para>
  ///             </summary>
  public class LightweightCellEditor : LightweightCellEditorBase, IGridCellEditorOwner, IChrome, ISupportLoadingAnimation, IOrderPanelElement, IConditionalFormattingClient<LightweightCellEditor>, ISupportHorizonalContentAlignment
  {
    private int visibleIndex = -1;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty BackgroundProperty = DependencyProperty.Register("Background", typeof (Brush), typeof (LightweightCellEditor), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((LightweightCellEditor) d).UpdateBackgroundFromStyle())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty BorderBrushProperty = DependencyProperty.Register("BorderBrush", typeof (Brush), typeof (LightweightCellEditor), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((LightweightCellEditor) d).UpdateBorderFromStyle())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty PaddingProperty = DependencyProperty.Register("Padding", typeof (Thickness), typeof (LightweightCellEditor), new PropertyMetadata((object) new Thickness(), (PropertyChangedCallback) ((d, e) => ((LightweightCellEditor) d).UpdatePaddingFromStyle())));
    private static readonly DependencyPropertyKey SelectionStatePropertyKey = DependencyProperty.RegisterReadOnly("SelectionState", typeof (SelectionState), typeof (LightweightCellEditor), new PropertyMetadata((object) SelectionState.None));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty SelectionStateProperty = LightweightCellEditor.SelectionStatePropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey RowSelectionStatePropertyKey = DependencyProperty.RegisterReadOnly("RowSelectionState", typeof (SelectionState), typeof (LightweightCellEditor), new PropertyMetadata((object) SelectionState.None));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty RowSelectionStateProperty = LightweightCellEditor.RowSelectionStatePropertyKey.DependencyProperty;
    private static bool DataContextCoerceCallbackRegistered = false;
    internal static readonly ServiceSummaryItem[] EmptySummaries = new ServiceSummaryItem[0];
    internal readonly CellsControl cellsControl;
    private static readonly RenderTemplate backgroundTemplate;
    private bool isMenuAssigned;
    private Style styleCore;
    private Thickness childMargin;
    private readonly ConditionalFormattingHelper<LightweightCellEditor> formattingHelper;
    private LoadingAnimationHelper loadingAnimationHelper;
    private readonly RenderBorderContext border;
    private readonly ConditionalFormatContentRenderHelper<LightweightCellEditor> conditionalFormatContentRenderHelper;
    private DataBarFormatInfo info;
    private bool hasCustomAppearance;

    /// <summary>
    ///                 <para>Gets or sets the background color.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Drawing.Color" /> value representing the background color.
    /// </value>
    public Brush Background
    {
      get
      {
        return (Brush) this.GetValue(LightweightCellEditor.BackgroundProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditor.BackgroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the brush used to paint the borders of cells. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.Brush" /> value.
    /// </value>
    public Brush BorderBrush
    {
      get
      {
        return (Brush) this.GetValue(LightweightCellEditor.BorderBrushProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditor.BorderBrushProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the amount of space between the editor's borders and its contents. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> value representing padding values.
    /// </value>
    public Thickness Padding
    {
      get
      {
        return (Thickness) this.GetValue(LightweightCellEditor.PaddingProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditor.PaddingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets a value that indicates the cell's selection state. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.SelectionState" /> enumeration value that specifies the cell's selection state.
    /// </value>
    public SelectionState SelectionState
    {
      get
      {
        return (SelectionState) this.GetValue(LightweightCellEditor.SelectionStateProperty);
      }
      private set
      {
        this.SetValue(LightweightCellEditor.SelectionStatePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets a value that indicates the row's selection state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.SelectionState" /> enumeration value that specifies the row's selection state.
    /// </value>
    public SelectionState RowSelectionState
    {
      get
      {
        return (SelectionState) this.GetValue(LightweightCellEditor.RowSelectionStateProperty);
      }
      private set
      {
        this.SetValue(LightweightCellEditor.RowSelectionStatePropertyKey, (object) value);
      }
    }

    internal InplaceBaseEdit InplaceBaseEdit
    {
      get
      {
        return this.Content as InplaceBaseEdit;
      }
    }

    private bool HasStyle
    {
      get
      {
        return this.styleCore != null;
      }
    }

    bool IGridCellEditorOwner.CanRefreshContent
    {
      get
      {
        return this.CanRefreshContentCore;
      }
    }

    protected virtual bool CanRefreshContentCore
    {
      get
      {
        return LayoutHelper.IsChildElement((DependencyObject) this.RowData.RowElement, (DependencyObject) this);
      }
    }

    DependencyObject IGridCellEditorOwner.EditorRoot
    {
      get
      {
        return (DependencyObject) this;
      }
    }

    ColumnBase IGridCellEditorOwner.AssociatedColumn
    {
      get
      {
        return this.Column;
      }
    }

    internal LoadingAnimationHelper LoadingAnimationHelper
    {
      get
      {
        if (this.loadingAnimationHelper == null)
          this.loadingAnimationHelper = new LoadingAnimationHelper((ISupportLoadingAnimation) this);
        return this.loadingAnimationHelper;
      }
    }

    DataViewBase ISupportLoadingAnimation.DataView
    {
      get
      {
        return this.View;
      }
    }

    private FrameworkElement Child
    {
      get
      {
        if (this.InplaceBaseEdit != null)
          return (FrameworkElement) this.InplaceBaseEdit;
        if (this.VisualChildrenCount > 0)
          return this.GetVisualChild(0) as FrameworkElement;
        return (FrameworkElement) null;
      }
    }

    FrameworkElement ISupportLoadingAnimation.Element
    {
      get
      {
        return this.Child;
      }
    }

    bool ISupportLoadingAnimation.IsGroupRow
    {
      get
      {
        return false;
      }
    }

    bool ISupportLoadingAnimation.IsReady
    {
      get
      {
        return this.RowData.IsReady;
      }
    }

    FrameworkRenderElementContext IChrome.Root
    {
      get
      {
        return (FrameworkRenderElementContext) null;
      }
    }

    int IOrderPanelElement.VisibleIndex
    {
      get
      {
        return this.visibleIndex;
      }
      set
      {
        this.visibleIndex = value;
      }
    }

    ConditionalFormattingHelper<LightweightCellEditor> IConditionalFormattingClient<LightweightCellEditor>.FormattingHelper
    {
      get
      {
        return this.formattingHelper;
      }
    }

    bool IConditionalFormattingClient<LightweightCellEditor>.IsSelected
    {
      get
      {
        if (this.IsEditorVisible || this.RowSelectionState != SelectionState.None)
          return true;
        if (this.SelectionState != SelectionState.None)
          return this.SelectionState != SelectionState.CellMerge;
        return false;
      }
    }

    bool IConditionalFormattingClient<LightweightCellEditor>.IsReady
    {
      get
      {
        return this.RowData.IsReady;
      }
    }

    Locker IConditionalFormattingClient<LightweightCellEditor>.Locker
    {
      get
      {
        return this.RowData.conditionalFormattingLocker;
      }
    }

    bool IConditionalFormattingClient<LightweightCellEditor>.HasCustomAppearance
    {
      get
      {
        ITableView tableView = this.View as ITableView;
        if (tableView != null)
          return tableView.HasCustomCellAppearance;
        return false;
      }
    }

    HorizontalAlignment ISupportHorizonalContentAlignment.HorizonalContentAlignment
    {
      get
      {
        return this.Edit.HorizontalContentAlignment;
      }
    }

    protected override InplaceEditorBase ReraiseMouseEventEditor
    {
      get
      {
        return this.Owner.CurrentCellEditor;
      }
    }

    static LightweightCellEditor()
    {
      Type forType = typeof (LightweightCellEditor);
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new RowCellTableViewHitTestAcceptor()));
      DataControlPopupMenu.GridMenuTypeProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) GridMenuType.RowCell));
      LightweightCellEditor.backgroundTemplate = new RenderTemplate()
      {
        RenderTree = (FrameworkRenderElement) new RenderBorder()
      };
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the LightweightCellEditor class.
    /// </para>
    ///             </summary>
    /// <param name="cellsControl">A DevExpress.Xpf.Grid.CellsControl instance.</param>
    public LightweightCellEditor(CellsControl cellsControl)
    {
      this.formattingHelper = new ConditionalFormattingHelper<LightweightCellEditor>(this, (DependencyProperty) null);
      this.cellsControl = cellsControl;
      this.GridCellEditorOwner = (IGridCellEditorOwner) this;
      this.border = (RenderBorderContext) ChromeHelper.CreateContext((IChrome) this, LightweightCellEditor.backgroundTemplate);
      this.conditionalFormatContentRenderHelper = new ConditionalFormatContentRenderHelper<LightweightCellEditor>(this);
      this.UpdateBorderFromBrushSet();
    }

    private static void RegisterDataContextCoerceCallback()
    {
      if (LightweightCellEditor.DataContextCoerceCallbackRegistered)
        return;
      LightweightCellEditor.DataContextCoerceCallbackRegistered = true;
      FrameworkElement.DataContextProperty.OverrideMetadata(typeof (LightweightCellEditor), (PropertyMetadata) new FrameworkPropertyMetadata(FrameworkElement.DataContextProperty.DefaultMetadata.DefaultValue, (PropertyChangedCallback) null, (CoerceValueCallback) ((d, v) => ((LightweightCellEditor) d).CoerceDataContext(v))));
    }

    private object CoerceDataContext(object baseValue)
    {
      if (!this.HasStyle)
        return baseValue;
      return (object) this.CellData ?? baseValue;
    }

    protected override void NullEditorInEditorDataContext()
    {
      base.NullEditorInEditorDataContext();
      this.UpdateDataContext(false);
    }

    protected override void SetEditorInEditorDataContext()
    {
      base.SetEditorInEditorDataContext();
      this.UpdateDataContext(false);
    }

    private void UpdateDataContext(bool force)
    {
      if (!force && !this.HasStyle)
        return;
      LightweightCellEditor.RegisterDataContextCoerceCallback();
      this.CoerceValue(FrameworkElement.DataContextProperty);
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <param name="oldValue">
    /// 
    /// 
    /// </param>
    /// <param name="newValue">
    /// 
    /// 
    /// </param>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public override void OnColumnChanged(ColumnBase oldValue, ColumnBase newValue)
    {
      base.OnColumnChanged(oldValue, newValue);
      this.UpdateStyle();
      this.UpdateConditionalAppearance();
    }

    protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseRightButtonDown(e);
      if (this.isMenuAssigned)
        return;
      this.AssignContextMenu();
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      base.OnPreviewKeyDown(e);
      if (this.isMenuAssigned || !this.IsContextMenuKey(e))
        return;
      this.AssignContextMenu();
    }

    protected virtual bool IsContextMenuKey(KeyEventArgs e)
    {
      if (e.Key == Key.Apps)
        return true;
      if (e.Key == Key.System && e.SystemKey == Key.F10)
        return ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e));
      return false;
    }

    internal void AssignContextMenu()
    {
      BarManager.SetDXContextMenu((UIElement) this, (IPopupControl) this.View.DataControlMenu);
      BarManager.SetDXContextMenuPlacement((UIElement) this, PlacementMode.Bottom);
      this.isMenuAssigned = true;
    }

    protected override void OnColumnContentChanged(object sender, ColumnContentChangedEventArgs e)
    {
      base.OnColumnContentChanged(sender, e);
      if (e.Property != ColumnBase.ActualCellStyleProperty)
        return;
      this.UpdateStyle();
    }

    private void UpdateStyle()
    {
      Style style = this.Column != null ? this.Column.ActualCellStyle : (Style) null;
      if (style is DefaultStyle)
        style = (Style) null;
      if (style == this.styleCore)
        return;
      this.styleCore = style;
      if (style != null)
        this.UpdateDataContext(true);
      this.Style = style;
      if (style == null)
        this.UpdateDataContext(true);
      this.UpdateBackgroundFromBrushSet();
      this.UpdateBorderFromBrushSet();
      this.UpdateForegroundFromBrushSet();
      this.UpdateBackgroundFromStyle();
      this.UpdateBorderFromStyle();
      this.UpdateForegroundFromStyle();
      this.UpdatePaddingFromStyle();
    }

    protected override EditorOptimizationMode GetEditorOptimizationMode()
    {
      return EditorOptimizationMode.Simple;
    }

    internal void SetBorderState(GridCellData cellData, SelectionState rowSelectionState)
    {
      Thickness cellBorderThickness = this.GetCellBorderThickness(cellData);
      this.border.BorderThickness = new Thickness?(cellBorderThickness);
      this.UpdateChildMargin();
      this.conditionalFormatContentRenderHelper.SetMargin(new Thickness((this.RowData == null || this.Column == null ? 0.0 : this.RowData.GetRowIndent(this.Column)) + cellBorderThickness.Left, cellBorderThickness.Top, cellBorderThickness.Right, cellBorderThickness.Bottom));
      if (this.RowSelectionState == rowSelectionState)
        return;
      this.RowSelectionState = rowSelectionState;
      this.UpdateBorderFromBrushSet();
      this.UpdateConditionalAppearance();
    }

    private Thickness GetCellBorderThickness(GridCellData cellData)
    {
      Thickness thickness = new Thickness();
      ITableView tableView = (ITableView) cellData.View;
      if (tableView.ShowVerticalLines)
      {
        if (cellData.Column.HasLeftSibling && cellData.Column.ColumnPosition == ColumnPosition.Left)
          thickness.Left = 1.0;
        if (cellData.Column.HasRightSibling)
          thickness.Right = 1.0;
      }
      if (tableView.ShowHorizontalLines && cellData.Column.HasTopElement)
        thickness.Top = 1.0;
      return thickness;
    }

    private void UpdateChildMargin()
    {
      if (!this.border.BorderThickness.HasValue)
        return;
      Thickness thickness = this.border.BorderThickness.Value;
      if (!(this.childMargin != thickness))
        return;
      this.childMargin = thickness;
      this.InvalidateMeasure();
    }

    protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved)
    {
      base.OnVisualChildrenChanged(visualAdded, visualRemoved);
      this.UpdateChildMargin();
    }

    private void UpdatePaddingFromStyle()
    {
      if (!this.HasStyle)
        return;
      this.border.Padding = new Thickness?(this.Padding);
    }

    private void UpdateBorderFromStyle()
    {
      if (!this.HasStyle)
        return;
      this.border.BorderBrush = this.BorderBrush;
    }

    private void UpdateBackgroundFromStyle()
    {
      if (!this.HasStyle)
        return;
      this.border.Background = this.formattingHelper.CoerceBackground(this.Background);
    }

    protected override void UpdateConditionalAppearance()
    {
      base.UpdateConditionalAppearance();
      this.formattingHelper.UpdateConditionalAppearance();
    }

    private void UpdateForegroundFromStyle()
    {
      if (!this.HasStyle)
        return;
      this.ClearValue(TextBlock.ForegroundProperty);
    }

    private void UpdateBorderFromBrushSet()
    {
      if (this.HasStyle)
        return;
      this.border.BorderBrush = this.GetBorderBrushFromBrushSet();
    }

    private Brush GetBorderBrushFromBrushSet()
    {
      string brushName = "BorderBrush";
      if (this.RowSelectionState == SelectionState.Focused && !this.hasCustomAppearance)
        brushName += "FocusedRow";
      return this.cellsControl.RowControl.CellBackgroundBrushes.GetBrush(brushName);
    }

    private void UpdateBackgroundFromBrushSet()
    {
      if (this.HasStyle)
        return;
      this.border.Background = this.formattingHelper.CoerceBackground(this.cellsControl.RowControl.CellBackgroundBrushes.GetBrush(this.SelectionState.ToString()));
    }

    private void UpdateForegroundFromBrushSet()
    {
      if (this.HasStyle)
        return;
      this.cellsControl.RowControl.CellForegroundBrushes.ApplyForeground((DependencyObject) this, this.SelectionState.ToString());
    }

    protected override void OnEditorPreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
      if (e.NewFocus == null)
        e.Handled = true;
      else
        base.OnEditorPreviewLostKeyboardFocus(sender, e);
    }

    protected override void OnEditorActivated(object sender, RoutedEventArgs e)
    {
      base.OnEditorActivated(sender, e);
      this.UpdateDataBarFormatInfo();
      this.RowData.RaiseResetEvents();
    }

    protected override void OnShowEditor()
    {
      base.OnShowEditor();
      this.UpdateConditionalAppearance();
    }

    protected override void OnHiddenEditor(bool closeEditor)
    {
      base.OnHiddenEditor(closeEditor);
      this.UpdateDataBarFormatInfo();
    }

    /// <summary>
    ///                 <para>Called after the template is completely generated and attached to the visual tree.
    /// </para>
    ///             </summary>
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      InplaceBaseEdit inplaceBaseEdit = this.GetTemplateChild("PART_Editor") as InplaceBaseEdit;
      if (inplaceBaseEdit == null || this.Column.EditSettings == null)
        return;
      inplaceBaseEdit.SetSettings(this.Column.EditSettings);
    }

    void IGridCellEditorOwner.SynProperties(GridCellData cellData)
    {
    }

    void IGridCellEditorOwner.UpdateCellState()
    {
    }

    void IGridCellEditorOwner.UpdateIsReady()
    {
      this.LoadingAnimationHelper.ApplyAnimation();
      this.UpdateConditionalAppearance();
    }

    void IGridCellEditorOwner.OnViewChanged()
    {
    }

    void IGridCellEditorOwner.SetSelectionState(SelectionState state)
    {
      if (this.SelectionState == state)
        return;
      this.SelectionState = state;
      this.UpdateBackgroundFromBrushSet();
      this.UpdateForegroundFromBrushSet();
    }

    void IGridCellEditorOwner.SetIsFocusedCell(bool isFocusedCell)
    {
      this.IsFocusedCell = isFocusedCell;
    }

    void IGridCellEditorOwner.UpdateCellBackgroundAppearance()
    {
      this.UpdateBackgroundFromBrushSet();
      this.UpdateBorderFromBrushSet();
    }

    void IGridCellEditorOwner.UpdateCellForegroundAppearance()
    {
      this.UpdateForegroundFromBrushSet();
    }

    protected override Size MeasureOverride(Size constraint)
    {
      this.conditionalFormatContentRenderHelper.Measure(constraint);
      double verticalMargin = this.GetVerticalMargin(this.childMargin);
      double horizontalMargin = this.GetHorizontalMargin(this.childMargin);
      Size size = base.MeasureOverride(new Size(Math.Max(0.0, constraint.Width - horizontalMargin), Math.Max(0.0, constraint.Height - verticalMargin)));
      return new Size(size.Width + horizontalMargin, size.Height + verticalMargin);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      this.border.Arrange(new Rect(0.0, 0.0, finalSize.Width, finalSize.Height));
      this.conditionalFormatContentRenderHelper.Arrange(finalSize);
      if (this.Child == null)
        return base.ArrangeOverride(finalSize);
      this.Child.Arrange(new Rect(this.childMargin.Left, this.childMargin.Top, Math.Max(0.0, finalSize.Width - this.GetHorizontalMargin(this.childMargin)), Math.Max(0.0, finalSize.Height - this.GetVerticalMargin(this.childMargin))));
      return finalSize;
    }

    private double GetVerticalMargin(Thickness margin)
    {
      return margin.Top + margin.Bottom;
    }

    private double GetHorizontalMargin(Thickness thickness)
    {
      return thickness.Left + thickness.Right;
    }

    protected override void OnRender(DrawingContext dc)
    {
      this.border.Render(dc);
      this.conditionalFormatContentRenderHelper.Render(dc);
    }

    void IChrome.GoToState(string stateName)
    {
    }

    void IChrome.AddChild(FrameworkElement element)
    {
      throw new NotSupportedException();
    }

    void IChrome.RemoveChild(FrameworkElement element)
    {
      throw new NotSupportedException();
    }

    IList<FormatConditionBaseInfo> IConditionalFormattingClient<LightweightCellEditor>.GetRelatedConditions()
    {
      ITableView tableView = (ITableView) this.View;
      if (tableView == null || this.Column == null)
        return (IList<FormatConditionBaseInfo>) null;
      return tableView.FormatConditions.GetInfoByFieldName(this.Column.FieldName);
    }

    FormatValueProvider? IConditionalFormattingClient<LightweightCellEditor>.GetValueProvider(string fieldName)
    {
      if (this.DataControl == null)
        return new FormatValueProvider?();
      return new FormatValueProvider?(this.RowData.GetValueProvider(fieldName));
    }

    void IConditionalFormattingClient<LightweightCellEditor>.UpdateBackground()
    {
      this.UpdateBackgroundFromStyle();
      this.UpdateBackgroundFromBrushSet();
    }

    void IConditionalFormattingClient<LightweightCellEditor>.UpdateDataBarFormatInfo(DataBarFormatInfo info)
    {
      if (object.Equals((object) this.info, (object) info))
        return;
      this.info = info;
      this.UpdateDataBarFormatInfo();
    }

    private void UpdateDataBarFormatInfo()
    {
      this.conditionalFormatContentRenderHelper.UpdateDataBarFormatInfo(this.IsEditorVisible ? (DataBarFormatInfo) null : this.info);
    }

    void IConditionalFormattingClient<LightweightCellEditor>.UpdateCustomAppearance(CustomAppearanceEventArgs args)
    {
      ITableView tableView = this.View as ITableView;
      if (tableView != null)
      {
        CustomCellAppearanceEventArgs args1 = new CustomCellAppearanceEventArgs(args);
        args1.Column = this.Column;
        args1.RowHandle = this.RowHandle;
        args1.RowSelectionState = this.RowSelectionState;
        args1.CellSelectionState = this.SelectionState;
        args1.IsEditorVisible = this.IsEditorVisible;
        tableView.RaiseCustomCellAppearance(args1);
        args1.SetActualResult(args);
        this.hasCustomAppearance = args1.Handled;
      }
      else
        this.hasCustomAppearance = false;
    }

    [SpecialName]
    double IGridCellEditorOwner.get_ActualHeight()
    {
      return this.ActualHeight;
    }

    void IChrome.InvalidateMeasure()
    {
      this.InvalidateMeasure();
    }

    void IChrome.InvalidateArrange()
    {
      this.InvalidateArrange();
    }

    void IChrome.InvalidateVisual()
    {
      this.InvalidateVisual();
    }
  }
}
