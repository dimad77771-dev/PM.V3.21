// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.EditForm;
using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>A grid row.
  /// </para>
  ///             </summary>
  public class RowControl : Control, IRowStateClient, IFocusedRowBorderObject, IGridDataRow, IConditionalFormattingClient<RowControl>
  {
    private EditFormShowMode appliedEditingMode = EditFormShowMode.None;
    private Locker editFormCloseLocker = new Locker();
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SelectionStateProperty = DependencyProperty.Register("SelectionState", typeof (SelectionState), typeof (RowControl), new PropertyMetadata((object) SelectionState.None));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowHorizontalLineProperty = DependencyProperty.Register("ShowHorizontalLine", typeof (bool), typeof (RowControl), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowVerticalLinesProperty = DependencyProperty.Register("ShowVerticalLines", typeof (bool), typeof (RowControl), new PropertyMetadata((object) true));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsAlternateRowProperty = DependencyProperty.Register("IsAlternateRow", typeof (bool), typeof (RowControl), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty FadeSelectionProperty = DependencyProperty.Register("FadeSelection", typeof (bool), typeof (RowControl), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowBottomLineProperty = DependencyProperty.Register("ShowBottomLine", typeof (bool), typeof (RowControl), new PropertyMetadata((object) false));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowFitBorderBrushProperty = DependencyProperty.Register("RowFitBorderBrush", typeof (Brush), typeof (RowControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((RowControl) d).UpdateFitContentBorderBrush())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowRowBreakProperty = DependencyProperty.Register("ShowRowBreak", typeof (bool), typeof (RowControl), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((RowControl) d).UpdateIndicatorShowRowBreak())));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CellForegroundBrushesProperty = DependencyProperty.Register("CellForegroundBrushes", typeof (BrushSet), typeof (RowControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((RowControl) d).OnCellForegroundBrushesChanged((BrushSet) e.OldValue))));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CellBackgroundBrushesProperty = DependencyProperty.Register("CellBackgroundBrushes", typeof (BrushSet), typeof (RowControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((RowControl) d).OnCellBackgroundBrushesChanged((BrushSet) e.OldValue))));
    private const int IndicatorPosition = 0;
    private const int LeftDetailIndentPosition = 1;
    private const int GroupOffsetPosition = 2;
    private const int DetailExpandButtonPosition = 3;
    private const int FixedLeftPosition = 4;
    private const int FixedLeftDelimiterPosition = 5;
    private const int FixedNonePosition = 6;
    private const int FixedRightDelimiterPosition = 7;
    private const int FixedRightPosition = 8;
    private const int FitContentPosition = 9;
    private const int RightDetailIndentPosition = 10;
    internal readonly RowData rowData;
    private readonly ConditionalFormattingHelper<RowControl> formattingHelper;
    private CellsControl fixedLeftCellsControl;
    private CellsControl fixedRightCellsControl;
    private RowFixedLineSeparatorControl leftSeparator;
    private RowFixedLineSeparatorControl rightSeparator;
    private RowFitBorder fitContent;
    private RowIndicator indicator;
    private FrameworkElement offsetPresenter;
    private GridDetailExpandButtonContainer detailExpandButtonContainer;
    private DetailRowsIndentControl detailLeftIndentControl;
    private DetailRowsIndentRightControl detailRightIndentControl;
    private ContentPresenter contentPresenter;
    private RowDetailsControl detailContentPresenter;
    private IndentScroller indentScroller;
    private EditFormContainer editFormContainerCore;
    private System.Windows.Controls.Grid layoutPanel;
    private Border backgroundBorder;
    private Border bottomLine;
    private bool oldUseTemplate;
    private EditFormRowData editFormDataCore;

    /// <summary>
    ///                 <para>Gets a value that indicates the row's selection state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="SelectionState" /> enumeration value that specifies the row's selection state.
    /// </value>
    public SelectionState SelectionState
    {
      get
      {
        return (SelectionState) this.GetValue(RowControl.SelectionStateProperty);
      }
      set
      {
        this.SetValue(RowControl.SelectionStateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to show horizontal row lines.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to show horizontal grid lines; otherwise, <b>false</b>.
    /// </value>
    public bool ShowHorizontalLine
    {
      get
      {
        return (bool) this.GetValue(RowControl.ShowHorizontalLineProperty);
      }
      set
      {
        this.SetValue(RowControl.ShowHorizontalLineProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool ShowVerticalLines
    {
      get
      {
        return (bool) this.GetValue(RowControl.ShowVerticalLinesProperty);
      }
      set
      {
        this.SetValue(RowControl.ShowVerticalLinesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the current row is alternate.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the row is alternate; otherwise, <b>false</b>.
    /// </value>
    public bool IsAlternateRow
    {
      get
      {
        return (bool) this.GetValue(RowControl.IsAlternateRowProperty);
      }
      set
      {
        this.SetValue(RowControl.IsAlternateRowProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool FadeSelection
    {
      get
      {
        return (bool) this.GetValue(RowControl.FadeSelectionProperty);
      }
      set
      {
        this.SetValue(RowControl.FadeSelectionProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool ShowBottomLine
    {
      get
      {
        return (bool) this.GetValue(RowControl.ShowBottomLineProperty);
      }
      set
      {
        this.SetValue(RowControl.ShowBottomLineProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the brush used to draw the row fit border.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="Brush" /> object that is a brush used to draw the row fit border.
    /// 
    /// </value>
    public Brush RowFitBorderBrush
    {
      get
      {
        return (Brush) this.GetValue(RowControl.RowFitBorderBrushProperty);
      }
      set
      {
        this.SetValue(RowControl.RowFitBorderBrushProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public bool ShowRowBreak
    {
      get
      {
        return (bool) this.GetValue(RowControl.ShowRowBreakProperty);
      }
      set
      {
        this.SetValue(RowControl.ShowRowBreakProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public BrushSet CellForegroundBrushes
    {
      get
      {
        return (BrushSet) this.GetValue(RowControl.CellForegroundBrushesProperty);
      }
      set
      {
        this.SetValue(RowControl.CellForegroundBrushesProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    public BrushSet CellBackgroundBrushes
    {
      get
      {
        return (BrushSet) this.GetValue(RowControl.CellBackgroundBrushesProperty);
      }
      set
      {
        this.SetValue(RowControl.CellBackgroundBrushesProperty, (object) value);
      }
    }

    protected internal CellsControl CellsControl { get; protected set; }

    private EditFormContainer EditFormContainer
    {
      get
      {
        return this.editFormContainerCore;
      }
      set
      {
        if (this.editFormContainerCore == value)
          return;
        this.editFormContainerCore = value;
        this.OnEditFormContainerChanged();
      }
    }

    protected virtual bool AllowTreeIndentScrolling
    {
      get
      {
        return this.TableView.ActualAllowTreeIndentScrolling;
      }
    }

    private bool ShowHorizontalLines
    {
      get
      {
        return this.TableView.ShowHorizontalLines;
      }
    }

    protected virtual bool ShowDetails
    {
      get
      {
        if (this.rowData.RowHandle == null)
          return false;
        return this.TableView.UseRowDetailsTemplate(this.rowData.RowHandle.Value);
      }
    }

    FrameworkElement IFocusedRowBorderObject.RowDataContent
    {
      get
      {
        return (FrameworkElement) this.backgroundBorder;
      }
    }

    double IFocusedRowBorderObject.LeftIndent
    {
      get
      {
        return this.CalculateRowContentIndent();
      }
    }

    RowData IGridDataRow.RowData
    {
      get
      {
        return this.rowData;
      }
    }

    private DataControlBase DataControl
    {
      get
      {
        return this.rowData.View.DataControl;
      }
    }

    internal BandsLayoutBase BandsLayout
    {
      get
      {
        if (this.DataControl == null)
          return (BandsLayoutBase) null;
        return this.DataControl.BandsLayoutCore;
      }
    }

    internal bool IsBandedLayout
    {
      get
      {
        return this.BandsLayout != null;
      }
    }

    private bool UseTemplate
    {
      get
      {
        return this.TableView.TableViewBehavior.UseDataRowTemplate(this.rowData);
      }
    }

    private ITableView TableView
    {
      get
      {
        return (ITableView) this.rowData.View;
      }
    }

    internal bool ShowInlineEditForm
    {
      get
      {
        return this.EditFormData != null;
      }
    }

    private EditFormRowData EditFormData
    {
      get
      {
        return this.editFormDataCore;
      }
      set
      {
        if (this.editFormDataCore == value)
          return;
        this.editFormDataCore = value;
        this.UpdateInlineEditFormContainer();
      }
    }

    ConditionalFormattingHelper<RowControl> IConditionalFormattingClient<RowControl>.FormattingHelper
    {
      get
      {
        return this.formattingHelper;
      }
    }

    bool IConditionalFormattingClient<RowControl>.IsReady
    {
      get
      {
        return true;
      }
    }

    bool IConditionalFormattingClient<RowControl>.IsSelected
    {
      get
      {
        return this.SelectionState != SelectionState.None;
      }
    }

    Locker IConditionalFormattingClient<RowControl>.Locker
    {
      get
      {
        return this.rowData.conditionalFormattingLocker;
      }
    }

    bool IConditionalFormattingClient<RowControl>.HasCustomAppearance
    {
      get
      {
        ITableView tableView = this.TableView;
        if (tableView != null)
          return tableView.HasCustomRowAppearance;
        return false;
      }
    }

    static RowControl()
    {
      Type forType = typeof (RowControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new RowTableViewHitTestAcceptor()));
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the RowControl class with the specified row data.
    /// </para>
    ///             </summary>
    /// <param name="rowData">A row data.</param>
    public RowControl(RowData rowData)
    {
      this.rowData = rowData;
      this.formattingHelper = new ConditionalFormattingHelper<RowControl>(this, Control.BackgroundProperty);
      rowData.SetRowStateClient((IRowStateClient) this);
    }

    private void OnCellForegroundBrushesChanged(BrushSet oldValue)
    {
      if (oldValue == null)
        return;
      this.rowData.UpdateCellForegroundAppearance();
    }

    private void OnCellBackgroundBrushesChanged(BrushSet oldValue)
    {
      if (oldValue == null)
        return;
      this.rowData.UpdateCellBackgroundAppearance();
    }

    void IGridDataRow.UpdateContentLayout()
    {
      this.UpdateCellsState();
    }

    private void UpdateCellsState()
    {
      this.UpdateCellsState(this.CellsControl);
      this.UpdateCellsState(this.fixedLeftCellsControl);
      this.UpdateCellsState(this.fixedRightCellsControl);
    }

    private void UpdateCellsState(CellsControl cellsControl)
    {
      if (cellsControl == null)
        return;
      cellsControl.InvalidateMeasure();
      if (cellsControl.Panel == null)
        return;
      cellsControl.Panel.InvalidateMeasure();
    }

    private void UpdateTriggerErrorState()
    {
    }

    private void UpdateFocusWithinState()
    {
      DataViewBase rootView = this.rowData.View.RootView;
      if (rootView.DataControl == null)
        return;
      this.FadeSelection = FadeSelectionHelper.IsFadeNeeded(rootView.ActualFadeSelectionOnLostFocus, rootView.DataControl.IsKeyboardFocusWithin) && this.SelectionState != SelectionState.None;
    }

    private void UpdateShowBottomLine()
    {
      this.ShowBottomLine = this.rowData.GetShowBottomLine();
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.layoutPanel = (System.Windows.Controls.Grid) this.GetTemplateChild("PART_LayoutPanel");
      this.backgroundBorder = (Border) this.GetTemplateChild("Background");
      this.bottomLine = this.GetTemplateChild("BottomLine") as Border;
      this.UpdateBottomLineMargin((FrameworkElement) this.backgroundBorder, true);
      this.CreateContent();
    }

    private void CreateContent()
    {
      this.ClearElements();
      if (this.UseTemplate)
        this.CreateTemplateContent();
      else
        this.CreateDefaultContent();
      this.oldUseTemplate = this.UseTemplate;
    }

    private void ClearElements()
    {
      this.layoutPanel.Children.Clear();
      this.layoutPanel.ColumnDefinitions.Clear();
      this.layoutPanel.RowDefinitions.Clear();
      this.CellsControl = (CellsControl) null;
      this.fixedLeftCellsControl = (CellsControl) null;
      this.fixedRightCellsControl = (CellsControl) null;
      this.leftSeparator = (RowFixedLineSeparatorControl) null;
      this.rightSeparator = (RowFixedLineSeparatorControl) null;
      this.fitContent = (RowFitBorder) null;
      this.indicator = (RowIndicator) null;
      this.offsetPresenter = (FrameworkElement) null;
      this.detailExpandButtonContainer = (GridDetailExpandButtonContainer) null;
      this.detailLeftIndentControl = (DetailRowsIndentControl) null;
      this.detailRightIndentControl = (DetailRowsIndentRightControl) null;
      this.contentPresenter = (ContentPresenter) null;
      this.detailContentPresenter = (RowDetailsControl) null;
      this.indentScroller = (IndentScroller) null;
      this.EditFormContainer = (EditFormContainer) null;
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = base.MeasureOverride(constraint);
      size = new Size(Math.Ceiling(size.Width), Math.Ceiling(size.Height));
      return size;
    }

    protected virtual void CreateDefaultContent()
    {
      for (int index = 0; index < 9; ++index)
        this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
        {
          Width = GridLength.Auto
        });
      this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
      {
        Width = GridLength.Auto
      });
      if (this.AllowTreeIndentScrolling)
      {
        this.indentScroller = new IndentScroller();
        this.AddPanelElement((FrameworkElement) this.indentScroller, 2);
      }
      this.UpdateIndicator();
      this.UpdateOffsetPresenter();
      this.UpdateFitContent();
      this.CellsControl = this.CreateAndInitFixedNoneCellsControl(6, (Func<RowData, IList<GridColumnData>>) (x => x.FixedNoneCellData), (Func<BandsLayoutBase, IList<BandBase>>) (x => (IList<BandBase>) x.FixedNoneVisibleBands));
      this.UpdateFixedNoneContentWidth();
      this.UpdateFixedLeftCellData((IList<GridColumnData>) null);
      this.UpdateFixedRightCellData((IList<GridColumnData>) null);
      this.UpdateScrollingMargin();
      this.UpdateDetailExpandButton();
      this.UpdateLeftDetailViewIndents();
      this.UpdateRightDetailViewIndents();
      this.UpdateDetails();
      this.UpdateInlineEditFormContainer();
    }

    private void UpdateOffsetPresenter()
    {
      if (this.rowData.Level <= 0 && !this.rowData.View.IsRowMarginControlVisible)
        return;
      this.UpdateOffsetPresenterLevel();
    }

    private void UpdateFitContent()
    {
      this.fitContent = new RowFitBorder();
      DataControlPopupMenu.SetGridMenuType((DependencyObject) this.fitContent, new GridMenuType?(GridMenuType.RowCell));
      this.UpdateFitContentContextMenu();
      this.AddPanelElement((FrameworkElement) this.fitContent, 9);
      this.UpdateFitContentBorderBrush();
      this.UpdateBottomLineMargin((FrameworkElement) this.fitContent, !this.ShowDetails);
    }

    private void UpdateFitContentContextMenu()
    {
      if (this.fitContent == null)
        return;
      BarManager.SetDXContextMenu((UIElement) this.fitContent, (IPopupControl) this.rowData.View.DataControlMenu);
    }

    private void UpdateIndicator()
    {
      this.indicator = new RowIndicator();
      DataControlPopupMenu.SetGridMenuType((DependencyObject) this.indicator, new GridMenuType?(GridMenuType.RowCell));
      System.Windows.Controls.Grid.SetRowSpan((UIElement) this.indicator, 2);
      this.UpdateIndicatorContextMenu();
      this.AddPanelElement((FrameworkElement) this.indicator, 0);
      this.UpdateIndicatorWidth();
      if (!((ITableView) this.rowData.View).ActualShowIndicator)
        this.UpdateIndicatorVisibility();
      if (this.rowData.IndicatorState != IndicatorState.None)
        this.UpdateIndicatorState();
      if (this.rowData.HasValidationErrorInternal)
        this.UpdateRowValidationError();
      this.UpdateIndicatorShowRowBreak();
    }

    private void UpdateIndicatorShowRowBreak()
    {
      if (this.indicator == null)
        return;
      this.indicator.ShowRowBreak = this.ShowRowBreak;
    }

    private void UpdateIndicatorContextMenu()
    {
      if (this.indicator == null)
        return;
      BarManager.SetDXContextMenu((UIElement) this.indicator, (IPopupControl) this.rowData.View.DataControlMenu);
    }

    protected virtual void CreateTemplateContent()
    {
      for (int index = 0; index < 4; ++index)
        this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
        {
          Width = GridLength.Auto
        });
      this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
      {
        Width = new GridLength(1.0, GridUnitType.Star)
      });
      this.UpdateIndicator();
      this.UpdateOffsetPresenter();
      DataContentPresenter contentPresenter = new DataContentPresenter();
      contentPresenter.Content = (object) this.rowData;
      contentPresenter.ContentTemplateSelector = this.TableView.ActualDataRowTemplateSelector;
      this.contentPresenter = (ContentPresenter) contentPresenter;
      this.UpdateBottomLineMargin((FrameworkElement) this.contentPresenter, true);
      this.AddPanelElement((FrameworkElement) this.contentPresenter, 4);
    }

    protected virtual void UpdateOffsetPresenterLevel()
    {
      if (this.offsetPresenter != null || this.layoutPanel == null || this.AllowTreeIndentScrolling && this.indentScroller == null)
        return;
      this.offsetPresenter = (FrameworkElement) GridRowHelper.CreateRowOffsetContent(this.rowData, (Control) this);
      System.Windows.Controls.Grid.SetRowSpan((UIElement) this.offsetPresenter, 2);
      if (this.AllowTreeIndentScrolling)
        this.indentScroller.AddScrollableElement((UIElement) this.offsetPresenter, 0);
      else
        this.AddPanelElement(this.offsetPresenter, 2);
    }

    private void UpdateDetailExpandButton()
    {
      bool showDetailButtons = ((ITableView) this.rowData.View).ActualShowDetailButtons;
      if (showDetailButtons && this.detailExpandButtonContainer == null && this.layoutPanel != null)
      {
        this.detailExpandButtonContainer = new GridDetailExpandButtonContainer();
        this.AddPanelElement((FrameworkElement) this.detailExpandButtonContainer, 3);
      }
      if (this.detailExpandButtonContainer == null)
        return;
      FrameworkElementHelper.SetIsVisible((FrameworkElement) this.detailExpandButtonContainer, showDetailButtons);
    }

    private void UpdateLeftDetailViewIndents()
    {
      IList<DetailIndent> detailIndents = this.rowData.DetailIndents;
      if (detailIndents != null && this.detailLeftIndentControl == null && this.layoutPanel != null)
      {
        this.detailLeftIndentControl = new DetailRowsIndentControl();
        this.AddPanelElement((FrameworkElement) this.detailLeftIndentControl, 1);
      }
      if (this.detailLeftIndentControl == null)
        return;
      this.detailLeftIndentControl.Visibility = DetailMarginVisibilityConverter.GetDetailMarginControlVisibility(detailIndents, Side.Left);
      this.detailLeftIndentControl.ItemsSource = (IEnumerable) detailIndents;
    }

    private void UpdateRightDetailViewIndents()
    {
      IList<DetailIndent> detailIndents = this.rowData.DetailIndents;
      if (detailIndents != null && this.detailRightIndentControl == null && this.layoutPanel != null)
      {
        this.detailRightIndentControl = new DetailRowsIndentRightControl();
        this.AddPanelElement((FrameworkElement) this.detailRightIndentControl, 10);
      }
      if (this.detailRightIndentControl == null)
        return;
      this.detailRightIndentControl.Visibility = DetailMarginVisibilityConverter.GetDetailMarginControlVisibility(detailIndents, Side.Right);
      this.detailRightIndentControl.ItemsSourceToReverse = (IEnumerable) detailIndents;
    }

    private void UpdateDetails()
    {
      if (this.layoutPanel == null)
        return;
      if (this.ShowDetails)
      {
        if (this.detailContentPresenter == null)
        {
          this.UpdateRowDefinitions();
          RowDetailsControl rowDetailsControl = new RowDetailsControl();
          rowDetailsControl.Content = (object) this.rowData;
          this.detailContentPresenter = rowDetailsControl;
          this.UpdateDetailContentPresenterRow();
          System.Windows.Controls.Grid.SetColumnSpan((UIElement) this.detailContentPresenter, 6);
          this.AddPanelElement((FrameworkElement) this.detailContentPresenter, 4);
          this.UpdateBottomLineMargin((FrameworkElement) this.detailContentPresenter, true);
        }
        this.detailContentPresenter.Visibility = Visibility.Visible;
        this.detailContentPresenter.ContentTemplateSelector = this.TableView.ActualRowDetailsTemplateSelector;
      }
      else if (this.detailContentPresenter != null)
        this.detailContentPresenter.Visibility = Visibility.Collapsed;
      this.UpdateBottomLineMargin();
    }

    private void UpdateDetailContentPresenterRow()
    {
      if (this.detailContentPresenter == null)
        return;
      System.Windows.Controls.Grid.SetRow((UIElement) this.detailContentPresenter, this.ShowInlineEditForm ? 2 : 1);
    }

    private void UpdateIndicatorWidth()
    {
      if (this.indicator == null)
        return;
      this.indicator.Width = ((ITableView) this.rowData.View).ActualIndicatorWidth;
    }

    private void UpdateIndicatorVisibility()
    {
      if (this.indicator == null)
        return;
      this.indicator.Visibility = ((ITableView) this.rowData.View).ActualShowIndicator ? Visibility.Visible : Visibility.Collapsed;
    }

    private void UpdateIndicatorState()
    {
      if (this.indicator == null)
        return;
      this.indicator.IndicatorState = this.rowData.IndicatorState;
    }

    private void UpdateIndicatorContentTemplate()
    {
      if (this.indicator == null)
        return;
      this.indicator.UpdateContent();
    }

    private void UpdateRowValidationError()
    {
      BaseEditHelper.SetValidationError((DependencyObject) this, this.rowData.ValidationErrorInternal);
    }

    private void UpdateFixedNoneContentWidth()
    {
      if (this.AllowTreeIndentScrolling)
        this.indentScroller.Do<IndentScroller>((Action<IndentScroller>) (x => x.Width = this.TableView.FixedNoneContentWidth));
      else
        this.CellsControl.Do<CellsControl>((Action<CellsControl>) (x => x.Width = this.rowData.FixedNoneContentWidth));
    }

    protected void AddPanelElement(FrameworkElement element, int position)
    {
      if (element == null || this.layoutPanel == null)
        return;
      this.layoutPanel.Children.Add((UIElement) element);
      System.Windows.Controls.Grid.SetColumn((UIElement) element, position);
    }

    private void UpdateBottomLineMargin()
    {
      bool useMargin = !this.ShowDetails && !this.ShowInlineEditForm;
      this.UpdateBottomLineMargin((FrameworkElement) this.CellsControl, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.fixedLeftCellsControl, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.fixedRightCellsControl, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.leftSeparator, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.rightSeparator, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.backgroundBorder, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.fitContent, useMargin);
      this.UpdateBottomLineMargin((FrameworkElement) this.detailContentPresenter, true);
    }

    private void UpdateBottomLineMargin(FrameworkElement element, bool useMargin)
    {
      if (element == null)
        return;
      double bottom = 0.0;
      if (useMargin && (this.ShowHorizontalLines || this.rowData.ShowBottomLine))
        bottom = (this.bottomLine == null ? new Thickness() : this.bottomLine.BorderThickness).Bottom;
      element.Margin = new Thickness(0.0, 0.0, 0.0, bottom);
    }

    private void UpdateScrollingMargin()
    {
      Thickness offset = ((ITableView) this.rowData.View).ScrollingVirtualizationMargin;
      if (this.AllowTreeIndentScrolling)
        this.indentScroller.Do<IndentScroller>((Action<IndentScroller>) (x => x.SetScrollOffset(offset)));
      else
        this.CellsControl.Do<CellsControl>((Action<CellsControl>) (x => x.SetPanelOffset(offset)));
    }

    private void UpdateView(CellsControl cellsControl)
    {
      if (cellsControl != null)
        DataControlBase.SetCurrentView((DependencyObject) cellsControl, this.rowData.View);
      this.UpdateScrollingMargin();
    }

    private void UpdateCellData(CellsControl cellsControl)
    {
      if (cellsControl == null)
        return;
      cellsControl.UpdateItemsSource();
    }

    private void UpdateCellsPanel(CellsControl cellsControl)
    {
      if (cellsControl == null)
        return;
      cellsControl.UpdatePanel();
    }

    private void InvalidateCellsPanel(CellsControl cellsControl)
    {
      if (cellsControl == null)
        return;
      cellsControl.InvalidateArrange();
      cellsControl.InvalidatePanel();
    }

    private void UpdateBands(CellsControl cellsControl)
    {
      if (cellsControl == null)
        return;
      cellsControl.UpdateBands();
    }

    private void UpdateFixedLeftCellData(IList<GridColumnData> oldValue)
    {
      this.InitFixedLeftCellsControl();
      this.SubscribeFixedCellDataChanged(oldValue, this.rowData.FixedLeftCellData, new NotifyCollectionChangedEventHandler(this.OnFixedLeftCellDataCollectionChanged));
    }

    private void UpdateFixedRightCellData(IList<GridColumnData> oldValue)
    {
      this.InitFixedRightCellsControl();
      this.SubscribeFixedCellDataChanged(oldValue, this.rowData.FixedRightCellData, new NotifyCollectionChangedEventHandler(this.OnFixedRightCellDataCollectionChanged));
    }

    private void SubscribeFixedCellDataChanged(IList<GridColumnData> oldValue, IList<GridColumnData> newValue, NotifyCollectionChangedEventHandler handler)
    {
      if (oldValue != null)
        ((INotifyCollectionChanged) oldValue).CollectionChanged -= handler;
      if (newValue == null)
        return;
      ((INotifyCollectionChanged) newValue).CollectionChanged += handler;
    }

    private void InitFixedLeftCellsControl()
    {
      this.CreateAndInitCellsControl(ref this.fixedLeftCellsControl, 4, ref this.leftSeparator, 5, (Func<RowData, IList<GridColumnData>>) (x => x.FixedLeftCellData), (Func<BandsLayoutBase, IList<BandBase>>) (x => (IList<BandBase>) x.FixedLeftVisibleBands), (Func<TableViewBehavior, IList<ColumnBase>>) (x => x.FixedLeftVisibleColumns), FixedStyle.Left);
    }

    private void InitFixedRightCellsControl()
    {
      this.CreateAndInitCellsControl(ref this.fixedRightCellsControl, 8, ref this.rightSeparator, 7, (Func<RowData, IList<GridColumnData>>) (x => x.FixedRightCellData), (Func<BandsLayoutBase, IList<BandBase>>) (x => (IList<BandBase>) x.FixedRightVisibleBands), (Func<TableViewBehavior, IList<ColumnBase>>) (x => x.FixedRightVisibleColumns), FixedStyle.Right);
    }

    private void CreateAndInitCellsControl(ref CellsControl cellsControl, int position, ref RowFixedLineSeparatorControl separator, int separatorPosition, Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getFixedBandsFunc, Func<TableViewBehavior, IList<ColumnBase>> getFixedColumnsFunc, FixedStyle fixedStyle)
    {
      if (this.layoutPanel == null || this.UseTemplate)
        return;
      IList<GridColumnData> gridColumnDataList = getCellDataFunc(this.rowData);
      IList<BandBase> bandBaseList = this.BandsLayout != null ? getFixedBandsFunc(this.BandsLayout) : (IList<BandBase>) null;
      if (cellsControl != null || (gridColumnDataList == null || gridColumnDataList.Count <= 0) && bandBaseList == null)
        return;
      cellsControl = this.CreateAndInitCellsControl(position, getCellDataFunc, getFixedBandsFunc, fixedStyle);
      TableViewProperties.SetFixedAreaStyle((DependencyObject) cellsControl, fixedStyle);
      separator = new RowFixedLineSeparatorControl(getFixedColumnsFunc, getFixedBandsFunc);
      this.UpdateFixedSeparatorHitTestAcceptor(separator, separatorPosition);
      separator.Width = ((ITableView) this.rowData.View).FixedLineWidth;
      this.UpdateFixedSeparatorShowVertialLines(separator);
      this.UpdateFixedSeparatorWidth(separator);
      this.UpdateFixedSeparatorVisibility(separator);
      this.UpdateBottomLineMargin((FrameworkElement) separator, !this.ShowDetails);
      this.AddPanelElement((FrameworkElement) separator, separatorPosition);
    }

    private void UpdateFixedSeparatorShowVertialLines(RowFixedLineSeparatorControl separator)
    {
      if (separator == null)
        return;
      separator.ShowVerticalLines = ((ITableView) this.rowData.View).ShowVerticalLines;
    }

    private void UpdateShowVertialLines()
    {
      this.ShowVerticalLines = ((ITableView) this.rowData.View).ShowVerticalLines;
    }

    private void UpdateFitContentBorderBrush()
    {
      if (this.fitContent == null)
        return;
      this.fitContent.BorderBrush = this.RowFitBorderBrush;
    }

    private void UpdateFixedSeparatorWidth(RowFixedLineSeparatorControl separator)
    {
      if (separator == null)
        return;
      separator.Width = ((ITableView) this.rowData.View).FixedLineWidth;
    }

    private void UpdateFixedSeparatorVisibility(RowFixedLineSeparatorControl separator)
    {
      if (separator == null)
        return;
      separator.UpdateVisibility(this.rowData.View.DataControl);
    }

    private void UpdateFixedSeparatorHitTestAcceptor(RowFixedLineSeparatorControl separator, int separatorPosition)
    {
      TableViewHitTestAcceptorBase testAcceptorBase = (TableViewHitTestAcceptorBase) null;
      if (separatorPosition == 5)
        testAcceptorBase = (TableViewHitTestAcceptorBase) new FixedLeftDivTableViewHitTestAcceptor();
      else if (separatorPosition == 7)
        testAcceptorBase = (TableViewHitTestAcceptorBase) new FixedRightDivTableViewHitTestAcceptor();
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) separator, (DataViewHitTestAcceptorBase) testAcceptorBase);
    }

    private void OnFixedLeftCellDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.UseTemplate)
        return;
      this.InitFixedLeftCellsControl();
    }

    private void OnFixedRightCellDataCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.UseTemplate)
        return;
      this.InitFixedRightCellsControl();
    }

    private CellsControl CreateAndInitCellsControl(int position, Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc, FixedStyle fixedStyle)
    {
      CellsControl cellsControl = this.CreateCellsControl(getCellDataFunc, getBandsFunc);
      this.UpdateView(cellsControl);
      this.UpdateBottomLineMargin((FrameworkElement) cellsControl, !this.ShowDetails);
      if (this.AllowTreeIndentScrolling && fixedStyle == FixedStyle.None)
        this.indentScroller.AddScrollableElement((UIElement) cellsControl, 1);
      else
        this.AddPanelElement((FrameworkElement) cellsControl, position);
      if (this.IsBandedLayout || this.rowData.View.ActualAllowCellMerge)
        this.UpdateCellsPanel(cellsControl);
      this.UpdateCellData(cellsControl);
      return cellsControl;
    }

    protected virtual CellsControl CreateCellsControl(Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc)
    {
      return new CellsControl(this, getCellDataFunc, getBandsFunc);
    }

    protected CellsControl CreateAndInitFixedNoneCellsControl(int position, Func<RowData, IList<GridColumnData>> getCellDataFunc, Func<BandsLayoutBase, IList<BandBase>> getBandsFunc)
    {
      CellsControl initCellsControl = this.CreateAndInitCellsControl(position, getCellDataFunc, getBandsFunc, FixedStyle.None);
      FocusRectPresenter.SetIsHorizontalScrollHost((DependencyObject) initCellsControl, true);
      return initCellsControl;
    }

    private double CalculateRowContentIndent()
    {
      if (this.layoutPanel == null)
        return 0.0;
      double num1 = 0.0;
      int num2 = 4;
      if (this.indentScroller != null)
        num2 = 2;
      for (int index = 0; index < num2; ++index)
        num1 += this.layoutPanel.ColumnDefinitions[index].ActualWidth;
      if (this.indentScroller != null)
        num1 += this.indentScroller.GetRowIndentWidth();
      return num1;
    }

    private void UpdateEditFormData()
    {
      if (this.rowData.RowHandle != null && this.rowData.View != null)
        this.EditFormData = this.rowData.View.EditFormManager.GetInplaceData(this.rowData.RowHandle.Value);
      else
        this.EditFormData = (EditFormRowData) null;
    }

    private void UpdateInlineEditFormContainer()
    {
      this.editFormCloseLocker.DoLockedAction((Action) (() => this.UpdateInlineEditFormContainerCore()));
    }

    private void UpdateInlineEditFormContainerCore()
    {
      if (this.layoutPanel == null)
        return;
      this.UpdateRowDefinitions();
      if (this.ShowInlineEditForm)
      {
        int num1 = 0;
        int num2 = 1;
        if (this.TableView.EditFormShowMode == EditFormShowMode.InlineHideRow)
        {
          this.ClearElements();
          for (int index = 0; index < 4; ++index)
            this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
            {
              Width = GridLength.Auto
            });
          this.layoutPanel.ColumnDefinitions.Add(new ColumnDefinition()
          {
            Width = new GridLength(1.0, GridUnitType.Star)
          });
          this.UpdateIndicator();
          this.UpdateOffsetPresenter();
        }
        else
        {
          num1 = 1;
          num2 = 6;
        }
        if (this.EditFormContainer == null)
        {
          this.EditFormContainer = new EditFormContainer();
          System.Windows.Controls.Grid.SetRow((UIElement) this.EditFormContainer, num1);
          System.Windows.Controls.Grid.SetColumnSpan((UIElement) this.EditFormContainer, num2);
          this.EditFormContainer.ShowMode = this.TableView.EditFormShowMode;
          this.AddPanelElement((FrameworkElement) this.EditFormContainer, 4);
        }
        this.EditFormContainer.ContentTemplate = this.TableView.EditFormTemplate;
        this.EditFormContainer.Content = (object) this.EditFormData;
        this.EditFormContainer.Visibility = Visibility.Visible;
      }
      else if (this.EditFormContainer != null)
      {
        if (this.appliedEditingMode == EditFormShowMode.InlineHideRow)
        {
          this.CreateContent();
        }
        else
        {
          this.layoutPanel.Children.Remove((UIElement) this.EditFormContainer);
          this.EditFormContainer = (EditFormContainer) null;
        }
      }
      this.appliedEditingMode = this.TableView.EditFormShowMode;
      this.UpdateDetailContentPresenterRow();
      this.UpdateBottomLineMargin();
    }

    private void OnEditFormContainerChanged()
    {
      this.editFormCloseLocker.DoIfNotLocked((Action) (() =>
      {
        if (this.EditFormContainer != null)
          return;
        this.rowData.View.Do<DataViewBase>((Action<DataViewBase>) (x => x.EditFormManager.OnInlineFormClosed(false)));
      }));
    }

    private void UpdateRowDefinitions()
    {
      if (this.layoutPanel == null)
        return;
      int num = 1;
      if (this.ShowInlineEditForm)
        ++num;
      if (this.ShowDetails)
        ++num;
      if (num <= 1)
        return;
      RowDefinitionCollection rowDefinitions = this.layoutPanel.RowDefinitions;
      for (int count = rowDefinitions.Count; count < num; ++count)
        rowDefinitions.Add(new RowDefinition()
        {
          Height = new GridLength(1.0, count == 0 ? GridUnitType.Star : GridUnitType.Auto)
        });
    }

    void IRowStateClient.UpdateRowHandle(RowHandle rowHandle)
    {
      DataViewBase.SetRowHandle((DependencyObject) this, rowHandle);
      this.UpdateEditFormData();
    }

    void IRowStateClient.UpdateSelectionState(SelectionState selectionState)
    {
      this.SelectionState = selectionState;
      this.UpdateCellsState();
      this.UpdateFocusWithinState();
    }

    void IRowStateClient.UpdateIsFocused()
    {
      this.UpdateDetails();
    }

    void IRowStateClient.UpdateScrollingMargin()
    {
      this.UpdateScrollingMargin();
    }

    void IRowStateClient.UpdateFixedNoneCellData()
    {
      this.UpdateCellData(this.CellsControl);
    }

    void IRowStateClient.UpdateView()
    {
      this.UpdateView(this.CellsControl);
      this.UpdateBands(this.CellsControl);
      this.UpdateView(this.fixedLeftCellsControl);
      this.UpdateView(this.fixedRightCellsControl);
      this.UpdateFitContentContextMenu();
      this.UpdateIndicatorContextMenu();
    }

    void IRowStateClient.UpdateFixedLeftCellData(IList<GridColumnData> oldValue)
    {
      this.UpdateFixedLeftCellData(oldValue);
    }

    void IRowStateClient.UpdateFixedRightCellData(IList<GridColumnData> oldValue)
    {
      this.UpdateFixedRightCellData(oldValue);
    }

    void IRowStateClient.UpdateHorizontalLineVisibility()
    {
      this.ShowHorizontalLine = this.ShowHorizontalLines;
      this.UpdateBottomLineMargin();
    }

    void IRowStateClient.UpdateVerticalLineVisibility()
    {
      this.UpdateFixedSeparatorShowVertialLines(this.leftSeparator);
      this.UpdateFixedSeparatorShowVertialLines(this.rightSeparator);
      this.UpdateShowVertialLines();
    }

    void IRowStateClient.UpdateFixedLineWidth()
    {
      this.UpdateFixedSeparatorWidth(this.leftSeparator);
      this.UpdateFixedSeparatorWidth(this.rightSeparator);
    }

    void IRowStateClient.UpdateFixedLineVisibility()
    {
      this.UpdateFixedSeparatorVisibility(this.leftSeparator);
      this.UpdateFixedSeparatorVisibility(this.rightSeparator);
    }

    void IRowStateClient.UpdateFixedNoneContentWidth()
    {
      this.UpdateFixedNoneContentWidth();
    }

    void IRowStateClient.UpdateIndicatorWidth()
    {
      this.UpdateIndicatorWidth();
    }

    void IRowStateClient.UpdateShowIndicator()
    {
      this.UpdateIndicatorVisibility();
    }

    void IRowStateClient.UpdateIndicatorState()
    {
      this.UpdateIndicatorState();
    }

    void IRowStateClient.UpdateIndicatorContentTemplate()
    {
      this.UpdateIndicatorContentTemplate();
    }

    void IRowStateClient.UpdateContent()
    {
      if (this.layoutPanel == null)
        return;
      if (this.oldUseTemplate != this.UseTemplate)
      {
        this.CreateContent();
      }
      else
      {
        if (this.contentPresenter == null)
          return;
        this.contentPresenter.ContentTemplateSelector = this.TableView.ActualDataRowTemplateSelector;
      }
    }

    void IRowStateClient.UpdateValidationError()
    {
      this.UpdateTriggerErrorState();
      this.UpdateRowValidationError();
    }

    void IRowStateClient.UpdateCellsPanel()
    {
      this.UpdateCellsPanel(this.CellsControl);
      this.UpdateCellsPanel(this.fixedLeftCellsControl);
      this.UpdateCellsPanel(this.fixedRightCellsControl);
    }

    void IRowStateClient.InvalidateCellsPanel()
    {
      if (!this.rowData.View.ActualAllowCellMerge)
        return;
      this.InvalidateCellsPanel(this.CellsControl);
      this.InvalidateCellsPanel(this.fixedLeftCellsControl);
      this.InvalidateCellsPanel(this.fixedRightCellsControl);
    }

    void IRowStateClient.UpdateFixedNoneBands()
    {
      this.UpdateBands(this.CellsControl);
    }

    void IRowStateClient.UpdateFixedLeftBands()
    {
      this.InitFixedLeftCellsControl();
      this.UpdateBands(this.fixedLeftCellsControl);
      this.UpdateFixedSeparatorVisibility(this.leftSeparator);
    }

    void IRowStateClient.UpdateFixedRightBands()
    {
      this.InitFixedRightCellsControl();
      this.UpdateBands(this.fixedRightCellsControl);
      this.UpdateFixedSeparatorVisibility(this.rightSeparator);
    }

    void IRowStateClient.UpdateAlternateBackground()
    {
      this.IsAlternateRow = this.rowData.AlternateRow;
    }

    void IRowStateClient.UpdateFocusWithinState()
    {
      this.UpdateFocusWithinState();
    }

    void IRowStateClient.UpdateLevel()
    {
      this.UpdateOffsetPresenterLevel();
      this.UpdateShowBottomLine();
      this.UpdateCellsState(this.fixedLeftCellsControl);
      if (!this.AllowTreeIndentScrolling)
        return;
      this.UpdateCellsState(this.CellsControl);
    }

    void IRowStateClient.UpdateDetailExpandButtonVisibility()
    {
      this.UpdateDetailExpandButton();
    }

    void IRowStateClient.UpdateDetailViewIndents()
    {
    }

    void IRowStateClient.UpdateRowPosition()
    {
      this.UpdateShowBottomLine();
    }

    void IRowStateClient.UpdateMinHeight()
    {
      this.MinHeight = ((ITableView) this.rowData.View).RowMinHeight;
    }

    void IRowStateClient.UpdateRowStyle()
    {
      Style style = ((ITableView) this.rowData.View).RowStyle;
      if (style is DefaultStyle)
        style = (Style) null;
      if (this.Style == style)
        return;
      this.Style = style;
    }

    void IRowStateClient.UpdateAppearance()
    {
      this.formattingHelper.UpdateConditionalAppearance();
    }

    void IRowStateClient.UpdateDetails()
    {
      this.UpdateDetails();
      this.UpdateBottomLineMargin();
    }

    void IRowStateClient.UpdateIndentScrolling()
    {
      this.CreateContent();
    }

    void IRowStateClient.UpdateShowRowBreak()
    {
      this.ShowRowBreak = this.rowData.ShowRowBreak;
    }

    void IRowStateClient.UpdateInlineEditForm()
    {
      this.UpdateEditFormData();
    }

    FormatValueProvider? IConditionalFormattingClient<RowControl>.GetValueProvider(string fieldName)
    {
      if (this.rowData.RowHandle == null || this.DataControl == null)
        return new FormatValueProvider?();
      return new FormatValueProvider?(this.rowData.GetValueProvider(fieldName));
    }

    IList<FormatConditionBaseInfo> IConditionalFormattingClient<RowControl>.GetRelatedConditions()
    {
      ITableView tableView = this.TableView;
      if (tableView == null)
        return (IList<FormatConditionBaseInfo>) null;
      return tableView.FormatConditions.GetInfoByFieldName(string.Empty);
    }

    void IConditionalFormattingClient<RowControl>.UpdateBackground()
    {
    }

    void IConditionalFormattingClient<RowControl>.UpdateDataBarFormatInfo(DataBarFormatInfo info)
    {
    }

    void IConditionalFormattingClient<RowControl>.UpdateCustomAppearance(CustomAppearanceEventArgs args)
    {
      ITableView tableView = this.TableView;
      int num = this.rowData.RowHandle.Return<RowHandle, int>((Func<RowHandle, int>) (r => r.Value), (Func<int>) (() => int.MinValue));
      if (tableView == null || num == int.MinValue)
        return;
      CustomRowAppearanceEventArgs args1 = new CustomRowAppearanceEventArgs(args);
      args1.RowHandle = num;
      args1.RowSelectionState = this.SelectionState;
      tableView.RaiseCustomRowAppearance(args1);
      args1.SetActualResult(args);
    }
  }
}
