// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.GroupRowLayout;
using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowControl : Control, IGroupRow, IGroupRowStateClient, IRowStateClient, IFixedGroupElement, IFocusedRowBorderObject
  {
    private IGroupValuePresenter groupValuePresenter = (IGroupValuePresenter) new NullGroupValuePresenter();
    private IndexDefinition IndicatorPosition = new IndexDefinition(0, 0, 0);
    private IndexDefinition LeftDetailIndentPosition = new IndexDefinition(0, 0, 1);
    private IndexDefinition GroupOffsetPosition = new IndexDefinition(0, 0, 2);
    private IndexDefinition ContentPresenterPosition = new IndexDefinition(0, 0, 3);
    private IndexDefinition GroupExpandButtonPosition = new IndexDefinition(1, 0, 0);
    private IndexDefinition CheckBoxSelectorPosition = new IndexDefinition(1, 0, 1);
    private IndexDefinition GroupValuePresenterPosition = new IndexDefinition(1, 0, 2);
    protected IndexDefinition ColumnSummaryPosition = new IndexDefinition(1, 1, 0);
    private IndexDefinition FitContentPosition = new IndexDefinition(2, 0, 0);
    private IndexDefinition DefaultSummaryPosition = new IndexDefinition(2, 0, 1);
    public static readonly DependencyProperty FocusOffsetProperty = DependencyProperty.Register("FocusOffset", typeof (double), typeof (GroupRowControl), new PropertyMetadata((object) 0.0));
    public static readonly DependencyProperty SelectionStateProperty = DependencyProperty.Register("SelectionState", typeof (SelectionState), typeof (GroupRowControl), new PropertyMetadata((object) SelectionState.None));
    public static readonly DependencyProperty FadeSelectionProperty = DependencyProperty.Register("FadeSelection", typeof (bool), typeof (GroupRowControl), new PropertyMetadata((object) false));
    public static readonly DependencyProperty RowFitBorderBrushProperty = DependencyProperty.Register("RowFitBorderBrush", typeof (Brush), typeof (GroupRowControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupRowControl) d).UpdateFitContentBorderBrush())));
    public static readonly DependencyProperty BottomLineMarginProperty = DependencyProperty.Register("BottomLineMargin", typeof (Thickness), typeof (GroupRowControl), new PropertyMetadata((object) new Thickness(0.0), (PropertyChangedCallback) ((d, e) => ((GroupRowControl) d).UpdateBottomLineMargin())));
    private bool oldUseTemplate;
    protected GroupRowControlPanel layoutPanel;
    protected UIElement rootPanel;
    private Border backgroundBorder;
    private RowIndicator indicator;
    private FrameworkElement offsetPresenter;
    private GroupRowExpandButton groupExpandButton;
    private GroupRowCheckBoxSelector checkBoxSelector;
    private GroupRowDefaultSummaryControl summaryDefaultControl;
    private SummaryAlignByColumnsController summaryAlignByColumnsController;
    private DetailRowsIndentControl detailLeftIndentControl;
    private RowFitBorder fitContent;
    protected Border bottomLine;
    private ContentPresenter contentPresenter;
    protected readonly GroupRowData rowData;
    private Rect oldGroupSummaryRect;
    private DevExpress.Xpf.Grid.FixedGroupElement fixedGroupElementCore;

    public double FocusOffset
    {
      get
      {
        return (double) this.GetValue(GroupRowControl.FocusOffsetProperty);
      }
      set
      {
        this.SetValue(GroupRowControl.FocusOffsetProperty, (object) value);
      }
    }

    public SelectionState SelectionState
    {
      get
      {
        return (SelectionState) this.GetValue(GroupRowControl.SelectionStateProperty);
      }
      set
      {
        this.SetValue(GroupRowControl.SelectionStateProperty, (object) value);
      }
    }

    public bool FadeSelection
    {
      get
      {
        return (bool) this.GetValue(GroupRowControl.FadeSelectionProperty);
      }
      set
      {
        this.SetValue(GroupRowControl.FadeSelectionProperty, (object) value);
      }
    }

    public Brush RowFitBorderBrush
    {
      get
      {
        return (Brush) this.GetValue(GroupRowControl.RowFitBorderBrushProperty);
      }
      set
      {
        this.SetValue(GroupRowControl.RowFitBorderBrushProperty, (object) value);
      }
    }

    public Thickness BottomLineMargin
    {
      get
      {
        return (Thickness) this.GetValue(GroupRowControl.BottomLineMarginProperty);
      }
      set
      {
        this.SetValue(GroupRowControl.BottomLineMarginProperty, (object) value);
      }
    }

    protected GridViewBase View
    {
      get
      {
        return (GridViewBase) this.rowData.View;
      }
    }

    protected TableView ViewTable
    {
      get
      {
        return this.rowData.View as TableView;
      }
    }

    private GridColumn Column
    {
      get
      {
        return (GridColumn) this.rowData.GroupValue.Column;
      }
    }

    private RowsContainer LogicalItemsContainer
    {
      get
      {
        return this.rowData.RowsContainer;
      }
    }

    internal SummaryAlignByColumnsController SummaryAlignByColumnsController
    {
      get
      {
        return this.summaryAlignByColumnsController;
      }
    }

    FrameworkElement IGroupRow.RowElement
    {
      get
      {
        return (FrameworkElement) this;
      }
    }

    private IFixedGroupElement FixedGroupElement
    {
      get
      {
        if (this.fixedGroupElementCore == null)
          this.fixedGroupElementCore = new DevExpress.Xpf.Grid.FixedGroupElement((Func<GroupRowData>) (() => this.rowData));
        return (IFixedGroupElement) this.fixedGroupElementCore;
      }
    }

    double IFocusedRowBorderObject.LeftIndent
    {
      get
      {
        if (this.indicator != null)
          return this.indicator.ActualWidth;
        return 0.0;
      }
    }

    FrameworkElement IFocusedRowBorderObject.RowDataContent
    {
      get
      {
        if (this.FocusOffset <= 0.0)
          return (FrameworkElement) this;
        return (FrameworkElement) this.backgroundBorder;
      }
    }

    static GroupRowControl()
    {
      Type forType = typeof (GroupRowControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      DataControlPopupMenu.GridMenuTypeProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) GridMenuType.GroupRow));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new GroupRowTableViewHitTestAcceptor()));
    }

    public GroupRowControl(GroupRowData rowData)
    {
      this.rowData = rowData;
      this.SetGroupRowStateClient(rowData);
    }

    protected virtual void SetGroupRowStateClient(GroupRowData rowData)
    {
      rowData.SetGroupRowStateClient((IGroupRowStateClient) this);
    }

    protected override void OnPreviewMouseRightButtonDown(MouseButtonEventArgs e)
    {
      base.OnPreviewMouseRightButtonDown(e);
      this.AssignContextMenu();
    }

    internal void AssignContextMenu()
    {
      BarManager.SetDXContextMenu((UIElement) this, (IPopupControl) this.View.DataControlMenu);
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
      base.OnMouseLeftButtonDown(e);
      if (e.ClickCount != 2)
        return;
      this.RowExpandedCommand();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.rootPanel = (UIElement) this.GetTemplateChild("PART_RootPanel");
      this.layoutPanel = (GroupRowControlPanel) this.GetTemplateChild("PART_LayoutPanel");
      this.backgroundBorder = (Border) this.GetTemplateChild("Background");
      this.UpdateBottomLineMargin((FrameworkElement) this.backgroundBorder);
      if (this.FocusOffset > 0.0)
        this.bottomLine = (Border) this.GetTemplateChild("BottomLine");
      this.CreateContent();
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = base.MeasureOverride(constraint);
      size = new Size(Math.Ceiling(size.Width), Math.Ceiling(size.Height));
      return size;
    }

    protected override Size ArrangeOverride(Size arrangeBounds)
    {
      this.UpdateLayoutSummaryPanelClip(arrangeBounds);
      return base.ArrangeOverride(arrangeBounds);
    }

    protected void AddPanelElement(FrameworkElement element, IndexDefinition index)
    {
      if (element == null || this.layoutPanel == null)
        return;
      this.layoutPanel.Groups.Add(new DevExpress.Xpf.Grid.GroupRowLayout.Column((UIElement) element), index);
      element.SizeChanged += new SizeChangedEventHandler(this.LayoutPanelElementSizeChanged);
    }

    private void LayoutPanelElementSizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.UpdateLayoutSummaryPanelClip(e.NewSize);
    }

    protected void RemovePanelElement(IndexDefinition index)
    {
      if (this.layoutPanel == null)
        return;
      DevExpress.Xpf.Grid.GroupRowLayout.Column column = this.layoutPanel.Groups.Get(index);
      if (column == null)
        return;
      FrameworkElement frameworkElement = column.Element as FrameworkElement;
      if (frameworkElement != null)
        frameworkElement.SizeChanged -= new SizeChangedEventHandler(this.LayoutPanelElementSizeChanged);
      this.layoutPanel.Groups.Remove(index);
    }

    private void CreateContent()
    {
      this.ClearElements();
      this.oldUseTemplate = this.UseGroupRowTemplate();
      if (this.oldUseTemplate)
        this.CreateTemplateContent();
      else
        this.CreateDefaultContent();
    }

    protected virtual void CreateDefaultContent()
    {
      Group child1 = new Group();
      child1.Add(new Layer(), 0);
      Group child2 = new Group();
      child2.Add(new Layer(), 0);
      child2.Add(new Layer(), 1);
      Group child3 = new Group();
      child3.Add(new Layer(), 0);
      this.layoutPanel.Groups.Add(child1, 0);
      this.layoutPanel.Groups.Add(child2, 1);
      this.layoutPanel.Groups.Add(child3, 2);
      this.UpdateIndicator();
      this.UpdateOffsetPresenter();
      this.UpdateCheckBoxSelector();
      this.UpdateGroupValuePresenter();
      this.UpdateSummary();
      this.UpdateGroupExpandButton();
      this.UpdateLeftDetailViewIndents();
    }

    private void CreateTemplateContent()
    {
      Group child = new Group();
      child.Add(new Layer(), 0);
      this.layoutPanel.Groups.Add(child, 0);
      this.UpdateIndicator();
      this.UpdateOffsetPresenter();
      this.UpdateRowIsExpanded();
      GroupGridRowPresenter gridRowPresenter = new GroupGridRowPresenter();
      gridRowPresenter.Content = (object) this.rowData;
      gridRowPresenter.ContentTemplateSelector = this.View.ActualGroupRowTemplateSelector;
      this.contentPresenter = (ContentPresenter) gridRowPresenter;
      this.AddPanelElement((FrameworkElement) this.contentPresenter, this.ContentPresenterPosition);
    }

    private void ClearElements()
    {
      this.layoutPanel.ResetGroups();
      this.summaryAlignByColumnsController = (SummaryAlignByColumnsController) null;
      this.indicator = (RowIndicator) null;
      this.offsetPresenter = (FrameworkElement) null;
      this.groupExpandButton = (GroupRowExpandButton) null;
      this.checkBoxSelector = (GroupRowCheckBoxSelector) null;
      this.groupValuePresenter = (IGroupValuePresenter) null;
      this.summaryDefaultControl = (GroupRowDefaultSummaryControl) null;
      this.detailLeftIndentControl = (DetailRowsIndentControl) null;
      this.contentPresenter = (ContentPresenter) null;
      this.groupValuePresenter = (IGroupValuePresenter) new NullGroupValuePresenter();
      this.fitContent = (RowFitBorder) null;
    }

    private bool UseGroupRowTemplate()
    {
      if (!GroupRowControl.IsNullOrDefaultTemplate(this.View.GroupRowTemplate))
        return true;
      if (this.View.GroupRowTemplateSelector != null)
        return !GroupRowControl.IsNullOrDefaultTemplate(this.View.GroupRowTemplateSelector.SelectTemplate((object) this.rowData, (DependencyObject) null));
      return false;
    }

    private void UpdateContent()
    {
      if (this.layoutPanel == null)
        return;
      if (this.oldUseTemplate != this.UseGroupRowTemplate())
      {
        this.CreateContent();
      }
      else
      {
        if (this.contentPresenter == null)
          return;
        this.contentPresenter.ContentTemplateSelector = this.View.ActualGroupRowTemplateSelector;
      }
    }

    private void UpdateFadeSelection()
    {
      DataViewBase rootView = this.View.RootView;
      if (rootView.DataControl == null)
        return;
      this.FadeSelection = FadeSelectionHelper.IsFadeNeeded(rootView.ActualFadeSelectionOnLostFocus, rootView.DataControl.IsKeyboardFocusWithin) && this.SelectionState != SelectionState.None;
    }

    protected virtual void IsPreviewExpandedChanged()
    {
    }

    protected virtual void UpdateCardLayoutChanged()
    {
    }

    protected virtual void RowPositionChange()
    {
    }

    protected virtual double CalcLevelOffset()
    {
      return this.CalcLevelOffset(this.rowData.Level);
    }

    protected virtual double CalcLevelOffset(int level)
    {
      if (this.ViewTable != null)
        return this.ViewTable.LeftGroupAreaIndent * (double) level - (!this.ViewTable.ActualShowDetailButtons || this.ViewTable.ActualExpandDetailHeaderWidth > this.ViewTable.LeftGroupAreaIndent * (double) this.rowData.Level ? 0.0 : this.ViewTable.ActualExpandDetailHeaderWidth);
      return 0.0;
    }

    private double CalcOffset()
    {
      double focusOffset = this.FocusOffset;
      if (this.indicator != null && this.indicator.Visibility == Visibility.Visible && this.ViewTable != null)
        focusOffset += this.ViewTable.ActualIndicatorWidth;
      return focusOffset;
    }

    private double CalcFullOffset()
    {
      double num = this.CalcOffset();
      if (this.rowData.Level > 0)
        num += this.CalcLevelOffset();
      if (this.detailLeftIndentControl != null)
      {
        foreach (object obj in this.detailLeftIndentControl.ItemsSource)
        {
          DetailIndent detailIndent = obj as DetailIndent;
          if (detailIndent != null)
            num += detailIndent.Width;
        }
      }
      return num;
    }

    protected void UpdateOffsetPresenter()
    {
      if (this.layoutPanel != null && (this.rowData.Level > 0 || this.View.IsRowMarginControlVisible) && this.offsetPresenter == null)
      {
        this.offsetPresenter = (FrameworkElement) new GroupRowOffsetPresenter();
        this.AddPanelElement(this.offsetPresenter, this.GroupOffsetPosition);
      }
      if (this.offsetPresenter != null)
      {
        if (this.rowData.Level > 0)
        {
          this.offsetPresenter.Width = this.CalcLevelOffset();
        }
        else
        {
          this.RemovePanelElement(this.GroupOffsetPosition);
          this.offsetPresenter = (FrameworkElement) null;
        }
      }
      this.UpdateBottomLineOffset();
    }

    protected void UpdateBottomLineOffset()
    {
      if (this.FocusOffset <= 0.0 || this.bottomLine == null)
        return;
      if (this.rowData.IsRowExpanded && this.SelectionState == SelectionState.None)
        this.bottomLine.Margin = new Thickness(this.CalcFullOffset(), this.bottomLine.Margin.Top, this.bottomLine.Margin.Right, this.bottomLine.Margin.Bottom);
      else
        this.bottomLine.Margin = new Thickness(0.0, this.bottomLine.Margin.Top, this.bottomLine.Margin.Right, this.bottomLine.Margin.Bottom);
    }

    private void UpdateIndicator()
    {
      if (this.ViewTable == null)
        return;
      this.indicator = (RowIndicator) new GroupRowIndicator();
      this.UpdateIndicatorWidth();
      if (!this.ViewTable.ActualShowIndicator)
        this.UpdateIndicatorVisibility();
      if (this.rowData.IndicatorState != IndicatorState.None)
        this.UpdateIndicatorState();
      this.AddPanelElement((FrameworkElement) this.indicator, this.IndicatorPosition);
    }

    private void UpdateIndicatorWidth()
    {
      if (this.indicator == null || this.ViewTable == null)
        return;
      this.indicator.Width = this.ViewTable.ActualIndicatorWidth;
    }

    private void UpdateIndicatorVisibility()
    {
      if (this.indicator == null || this.ViewTable == null)
        return;
      this.indicator.Visibility = this.ViewTable.ActualShowIndicator ? Visibility.Visible : Visibility.Collapsed;
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

    protected void UpdateGroupExpandButton()
    {
      if (this.groupExpandButton != null || this.layoutPanel == null)
        return;
      this.groupExpandButton = new GroupRowExpandButton();
      this.groupExpandButton.Command = (ICommand) new DelegateCommand(new Action(this.RowExpandedCommand));
      this.UpdateRowIsExpanded();
      this.AddPanelElement((FrameworkElement) this.groupExpandButton, this.GroupExpandButtonPosition);
    }

    private void RowExpandedCommand()
    {
      GridCommands.ChangeGroupExpanded.Execute((object) this.rowData.RowHandle.Value, (IInputElement) this.View);
    }

    protected void UpdateRowIsExpanded()
    {
      if (this.groupExpandButton != null)
        this.groupExpandButton.IsChecked = this.rowData.IsRowExpanded;
      if (this.LogicalItemsContainer != null)
        this.LogicalItemsContainer.AnimationProgress = this.rowData.IsRowExpanded ? 1.0 : 0.0;
      this.UpdateBottomLineOffset();
    }

    protected void UpdateCheckBoxSelector()
    {
      if (this.checkBoxSelector == null && this.layoutPanel != null && this.View.ActualShowCheckBoxSelectorInGroupRow)
      {
        this.checkBoxSelector = new GroupRowCheckBoxSelector();
        this.AddPanelElement((FrameworkElement) this.checkBoxSelector, this.CheckBoxSelectorPosition);
      }
      if (this.checkBoxSelector == null)
        return;
      this.checkBoxSelector.Visibility = this.View.ActualShowCheckBoxSelectorInGroupRow ? Visibility.Visible : Visibility.Collapsed;
    }

    protected void UpdateGroupValuePresenter()
    {
      bool flag1 = this.UseGroupValueTemplate();
      bool flag2 = flag1;
      bool? useTemplate = this.groupValuePresenter.UseTemplate;
      if ((flag2 != useTemplate.GetValueOrDefault() ? 1 : (!useTemplate.HasValue ? 1 : 0)) != 0)
      {
        this.RemovePanelElement(this.GroupValuePresenterPosition);
        this.groupValuePresenter = !flag1 ? (IGroupValuePresenter) new GroupValuePresenter() : (IGroupValuePresenter) new GroupValueContentPresenter();
        this.AddPanelElement(this.groupValuePresenter.Element, this.GroupValuePresenterPosition);
      }
      this.UpdateGroupValuePresenterContent();
      this.UpdateGroupValuePresenterTemplateSelector();
    }

    private void UpdateGroupValuePresenterContent()
    {
      this.groupValuePresenter.ValueData = this.rowData.GroupValue;
    }

    private void UpdateGroupValuePresenterTemplateSelector()
    {
      if (this.Column == null)
        return;
      this.groupValuePresenter.ContentTemplateSelector = this.Column.ActualGroupValueTemplateSelector;
    }

    private bool UseGroupValueTemplate()
    {
      if (!GroupRowControl.IsNullOrDefaultTemplate(this.View.GroupValueTemplate) || this.View.GroupValueTemplateSelector != null && !GroupRowControl.IsNullOrDefaultTemplate(this.View.GroupValueTemplateSelector.SelectTemplate((object) this.rowData.GroupValue, (DependencyObject) null)))
        return true;
      if (this.Column == null)
        return false;
      if (!GroupRowControl.IsNullOrDefaultTemplate(this.Column.GroupValueTemplate))
        return true;
      if (this.Column.GroupValueTemplateSelector != null)
        return !GroupRowControl.IsNullOrDefaultTemplate(this.Column.GroupValueTemplateSelector.SelectTemplate((object) this.rowData.GroupValue, (DependencyObject) null));
      return false;
    }

    private static bool IsNullOrDefaultTemplate(DataTemplate template)
    {
      if (template != null)
        return template is DefaultDataTemplate;
      return true;
    }

    protected virtual void UpdateSummary()
    {
      if (this.layoutPanel == null || this.rowData.GroupSummaryData == null || this.rowData.GroupSummaryData.Count == 0)
      {
        this.RemoveDefaultSummary();
        this.RemoveSummaryAlignByColumns();
      }
      else
      {
        switch (this.ViewTable != null ? this.ViewTable.GroupSummaryDisplayMode : GroupSummaryDisplayMode.Default)
        {
          case GroupSummaryDisplayMode.Default:
            this.RemoveSummaryAlignByColumns();
            this.UpdateGroupSummaryDefault();
            break;
          case GroupSummaryDisplayMode.AlignByColumns:
            this.RemoveDefaultSummary();
            this.UpdateGroupSummaryAlignByColumns();
            break;
        }
      }
    }

    private void UpdateGroupSummaryDefault()
    {
      if (this.summaryDefaultControl == null)
      {
        this.summaryDefaultControl = new GroupRowDefaultSummaryControl();
        this.AddPanelElement((FrameworkElement) this.summaryDefaultControl, this.DefaultSummaryPosition);
      }
      ActualTemplateSelectorWrapper templateSelectorWrapper = (ActualTemplateSelectorWrapper) this.View.ActualGroupSummaryItemTemplateSelector;
      if (GroupRowControl.IsNullOrDefaultTemplate(templateSelectorWrapper.Template) && templateSelectorWrapper.Selector == null)
        templateSelectorWrapper = (ActualTemplateSelectorWrapper) null;
      this.summaryDefaultControl.ItemTemplateSelector = (DataTemplateSelector) templateSelectorWrapper;
      this.summaryDefaultControl.ItemsSource = (IEnumerable) this.rowData.GroupSummaryData;
    }

    private void UpdateGroupSummaryAlignByColumns()
    {
      if (this.summaryAlignByColumnsController == null)
      {
        this.summaryAlignByColumnsController = new SummaryAlignByColumnsController(this.View.DataControl);
        this.AddPanelElement((FrameworkElement) this.summaryAlignByColumnsController.LayoutPanel, this.ColumnSummaryPosition);
      }
      if (this.fitContent == null)
      {
        this.fitContent = new RowFitBorder();
        this.UpdateFitContent();
        this.AddPanelElement((FrameworkElement) this.fitContent, this.FitContentPosition);
      }
      this.UpdateDateSummaryAlignByColumns();
      this.UpdateFixedNoneContentWidth();
      this.UpdateSummaryScrollingMargin();
      if (this.ViewTable == null)
        return;
      this.summaryAlignByColumnsController.UpdateGroupColumnSummaryItemTemplate(GroupRowControl.IsNullOrDefaultTemplate(this.ViewTable.GroupColumnSummaryItemTemplate) && this.ViewTable.GroupColumnSummaryContentStyle == null);
    }

    protected virtual void UpdateDateSummaryAlignByColumns()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateData(this.rowData);
    }

    private void RemoveDefaultSummary()
    {
      if (this.summaryDefaultControl == null)
        return;
      this.RemovePanelElement(this.DefaultSummaryPosition);
      this.summaryDefaultControl = (GroupRowDefaultSummaryControl) null;
    }

    private void RemoveSummaryAlignByColumns()
    {
      if (this.summaryAlignByColumnsController != null)
      {
        this.RemovePanelElement(this.ColumnSummaryPosition);
        this.summaryAlignByColumnsController = (SummaryAlignByColumnsController) null;
      }
      if (this.fitContent == null)
        return;
      this.RemovePanelElement(this.FitContentPosition);
      this.fitContent = (RowFitBorder) null;
    }

    private void UpdateSummaryScrollingMargin()
    {
      if (this.summaryAlignByColumnsController == null || this.ViewTable == null)
        return;
      this.summaryAlignByColumnsController.SetScrollingMargin(this.ViewTable.ScrollingHeaderVirtualizationMargin);
    }

    private void UpdateFitContent()
    {
      this.UpdateFitContentBorderBrush();
      this.UpdateBottomLineMargin((FrameworkElement) this.fitContent);
    }

    private void UpdateFitContentBorderBrush()
    {
      if (this.fitContent == null)
        return;
      this.fitContent.BorderBrush = this.RowFitBorderBrush;
    }

    private void UpdateBottomLineMargin()
    {
      this.UpdateBottomLineMargin((FrameworkElement) this.backgroundBorder);
      this.UpdateBottomLineMargin((FrameworkElement) this.fitContent);
    }

    private void UpdateBottomLineMargin(FrameworkElement element)
    {
      if (element == null)
        return;
      element.Margin = this.BottomLineMargin;
    }

    protected virtual void UpdateLayoutSummaryPanelClip(Size arrangeBounds)
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      UIElement uiElement = (UIElement) this.summaryAlignByColumnsController.LayoutPanel;
      double summaryPanelLeftIndent = this.GetLayoutSummaryPanelLeftIndent();
      Rect rect = new Rect(summaryPanelLeftIndent, 0.0, Math.Max(0.0, uiElement.DesiredSize.Width - summaryPanelLeftIndent), Math.Max(uiElement.DesiredSize.Height, arrangeBounds.Height));
      if (rect == this.oldGroupSummaryRect)
        return;
      this.oldGroupSummaryRect = rect;
      uiElement.Clip = (Geometry) new RectangleGeometry(rect);
      this.summaryAlignByColumnsController.SetLeftIndent(summaryPanelLeftIndent + this.CalcLevelOffset(this.rowData.Level != 0 ? this.rowData.Level : 1));
    }

    private double GetLayoutSummaryPanelLeftIndent()
    {
      double num = 0.0;
      if (this.groupExpandButton != null)
        num += this.groupExpandButton.DesiredSize.Width;
      if (this.groupValuePresenter.Element != null)
        num += this.groupValuePresenter.Element.DesiredSize.Width;
      if (this.checkBoxSelector != null)
        num += this.checkBoxSelector.DesiredSize.Width;
      return num;
    }

    private void UpdateBands(FixedStyle fixedStyle)
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateBands(fixedStyle);
    }

    protected void UpdateLeftDetailViewIndents()
    {
      IList<DetailIndent> detailIndents = this.rowData.DetailIndents;
      if (detailIndents != null && this.detailLeftIndentControl == null && this.layoutPanel != null)
      {
        this.detailLeftIndentControl = new DetailRowsIndentControl();
        this.AddPanelElement((FrameworkElement) this.detailLeftIndentControl, this.LeftDetailIndentPosition);
      }
      if (this.detailLeftIndentControl == null)
        return;
      this.detailLeftIndentControl.Visibility = DetailMarginVisibilityConverter.GetDetailMarginControlVisibility(detailIndents, Side.Left);
      this.detailLeftIndentControl.ItemsSource = (IEnumerable) detailIndents;
      this.UpdateOffsetPresenter();
    }

    void IGroupRowStateClient.UpdateGroupValue()
    {
      this.UpdateGroupValuePresenterContent();
    }

    void IGroupRowStateClient.UpdateIsRowExpanded()
    {
      this.UpdateRowIsExpanded();
    }

    void IGroupRowStateClient.UpdateSummary()
    {
      this.UpdateSummary();
    }

    void IGroupRowStateClient.UpdateGroupValueTemplateSelector()
    {
      this.UpdateGroupValuePresenter();
    }

    void IGroupRowStateClient.UpdateGroupRowTemplateSelector()
    {
      this.UpdateContent();
    }

    void IGroupRowStateClient.UpdateGroupRowStyle()
    {
      Style style = this.View.GroupRowStyle;
      if (style is DefaultStyle)
        style = (Style) null;
      if (this.Style == style)
        return;
      this.Style = style;
    }

    void IGroupRowStateClient.UpdateCheckBoxSelector()
    {
      this.UpdateCheckBoxSelector();
    }

    void IGroupRowStateClient.UpdateIsReady()
    {
      if (this.summaryDefaultControl == null)
        return;
      this.summaryDefaultControl.UpdateIsReady();
    }

    void IGroupRowStateClient.UpdateIsRowVisible()
    {
      if (this.rootPanel == null)
        return;
      this.rootPanel.Visibility = this.rowData.IsRowVisible ? Visibility.Visible : Visibility.Collapsed;
    }

    void IGroupRowStateClient.UpdateCardLayout()
    {
      this.UpdateCardLayoutChanged();
    }

    void IGroupRowStateClient.UpdateIsPreviewExpanded()
    {
      this.IsPreviewExpandedChanged();
    }

    void IRowStateClient.InvalidateCellsPanel()
    {
    }

    void IRowStateClient.UpdateAlternateBackground()
    {
    }

    void IRowStateClient.UpdateAppearance()
    {
    }

    void IRowStateClient.UpdateCellsPanel()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdatePanel();
    }

    void IRowStateClient.UpdateContent()
    {
    }

    void IRowStateClient.UpdateDetailExpandButtonVisibility()
    {
    }

    void IRowStateClient.UpdateDetailViewIndents()
    {
    }

    void IRowStateClient.UpdateDetails()
    {
      this.UpdateBottomLineMargin();
    }

    void IRowStateClient.UpdateFixedLeftBands()
    {
      this.UpdateBands(FixedStyle.Left);
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateFixedLeftSeparatorVisibility();
    }

    void IRowStateClient.UpdateFixedLeftCellData(IList<GridColumnData> oldValue)
    {
    }

    void IRowStateClient.UpdateFixedLineVisibility()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateFixedSeparatorVisibility();
    }

    void IRowStateClient.UpdateFixedLineWidth()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateFixedSeparatorWidth();
    }

    void IRowStateClient.UpdateFixedNoneBands()
    {
      this.UpdateBands(FixedStyle.None);
    }

    void IRowStateClient.UpdateFixedNoneCellData()
    {
    }

    void IRowStateClient.UpdateFixedNoneContentWidth()
    {
      this.UpdateFixedNoneContentWidth();
    }

    protected virtual void UpdateFixedNoneContentWidth()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      double width = this.rowData.FixedNoneContentWidth;
      if (this.ViewTable != null && !this.ViewTable.TableViewBehavior.HasFixedLeftElements)
        width = Math.Max(0.0, this.rowData.FixedNoneContentWidth - this.CalcLevelOffset() + (this.ViewTable.ActualShowDetailButtons ? this.ViewTable.ActualExpandDetailHeaderWidth : 0.0));
      this.summaryAlignByColumnsController.UpdateFixedNoneContentWidth(width);
    }

    void IRowStateClient.UpdateFixedRightBands()
    {
      this.UpdateBands(FixedStyle.Right);
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateFixedRightSeparatorVisibility();
    }

    void IRowStateClient.UpdateFixedRightCellData(IList<GridColumnData> oldValue)
    {
    }

    void IRowStateClient.UpdateFocusWithinState()
    {
      this.UpdateFadeSelection();
    }

    void IRowStateClient.UpdateHorizontalLineVisibility()
    {
      this.UpdateBottomLineMargin();
    }

    void IRowStateClient.UpdateIndentScrolling()
    {
    }

    void IRowStateClient.UpdateIndicatorContentTemplate()
    {
      this.UpdateIndicatorContentTemplate();
    }

    void IRowStateClient.UpdateIndicatorState()
    {
      this.UpdateIndicatorState();
    }

    void IRowStateClient.UpdateIndicatorWidth()
    {
      this.UpdateIndicatorWidth();
    }

    void IRowStateClient.UpdateIsFocused()
    {
    }

    void IRowStateClient.UpdateLevel()
    {
      this.UpdateIndicatorVisibility();
      this.UpdateOffsetPresenter();
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.InvalidateFixedLeft();
    }

    void IRowStateClient.UpdateMinHeight()
    {
    }

    void IRowStateClient.UpdateRowHandle(RowHandle rowHandle)
    {
      DataViewBase.SetRowHandle((DependencyObject) this, rowHandle);
    }

    void IRowStateClient.UpdateRowPosition()
    {
      this.RowPositionChange();
    }

    void IRowStateClient.UpdateRowStyle()
    {
    }

    void IRowStateClient.UpdateScrollingMargin()
    {
      this.UpdateSummaryScrollingMargin();
    }

    void IRowStateClient.UpdateSelectionState(SelectionState selectionState)
    {
      this.SelectionState = selectionState;
      this.UpdateFadeSelection();
      this.UpdateFitContent();
      this.UpdateBottomLineOffset();
    }

    void IRowStateClient.UpdateShowIndicator()
    {
      this.UpdateIndicatorVisibility();
      this.UpdateOffsetPresenter();
    }

    void IRowStateClient.UpdateValidationError()
    {
    }

    void IRowStateClient.UpdateVerticalLineVisibility()
    {
      if (this.summaryAlignByColumnsController == null)
        return;
      this.summaryAlignByColumnsController.UpdateFixedSeparatorShowVertialLines();
    }

    void IRowStateClient.UpdateView()
    {
    }

    void IRowStateClient.UpdateShowRowBreak()
    {
    }

    void IRowStateClient.UpdateInlineEditForm()
    {
    }

    double IFixedGroupElement.GetLeftMargin(bool drawAdornerUnderWholeGroup)
    {
      return this.FixedGroupElement.GetLeftMargin(drawAdornerUnderWholeGroup);
    }

    double IFixedGroupElement.GetRightMargin(bool drawAdornerUnderWholeGroup)
    {
      return this.FixedGroupElement.GetRightMargin(drawAdornerUnderWholeGroup);
    }
  }
}
