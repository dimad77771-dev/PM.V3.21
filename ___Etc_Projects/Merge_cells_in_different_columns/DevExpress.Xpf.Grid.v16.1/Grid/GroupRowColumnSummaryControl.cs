// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowColumnSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowColumnSummaryControl : Control, IGroupRowColumnSummaryClient, ISupportLoadingAnimation
  {
    private InlineTextUpdater updater = new InlineTextUpdater();
    public static readonly DependencyProperty BorderBrushesProperty = DependencyProperty.Register("BorderBrushes", typeof (BrushSet), typeof (GroupRowColumnSummaryControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupRowColumnSummaryControl) d).UpdateBorderBrush())));
    private static readonly Thickness normalThickness = new Thickness(0.0, 0.0, 1.0, 0.0);
    private static readonly Thickness rightThickness = new Thickness(0.0);
    private TextBlock textBlock;
    private Border border;
    private GridGroupSummaryColumnData columnDataCore;
    private bool hasCustomElementStyle;
    private LoadingAnimationHelper loadingAnimationHelper;

    public BrushSet BorderBrushes
    {
      get
      {
        return (BrushSet) this.GetValue(GroupRowColumnSummaryControl.BorderBrushesProperty);
      }
      set
      {
        this.SetValue(GroupRowColumnSummaryControl.BorderBrushesProperty, (object) value);
      }
    }

    public GridGroupSummaryColumnData ColumnData
    {
      get
      {
        return this.columnDataCore;
      }
      internal set
      {
        if (this.columnDataCore == value)
          return;
        this.columnDataCore = value;
        this.columnDataCore.SetColumnSummaryClient((IGroupRowColumnSummaryClient) this);
        this.SyncWithColumnData();
      }
    }

    private DataViewBase View
    {
      get
      {
        if (this.ColumnData == null)
          return (DataViewBase) null;
        return this.ColumnData.View;
      }
    }

    private bool HasCustomElementStyle
    {
      get
      {
        return this.hasCustomElementStyle;
      }
      set
      {
        if (this.hasCustomElementStyle == value)
          return;
        this.hasCustomElementStyle = value;
        this.UpdateTextBlockValue();
      }
    }

    private LoadingAnimationHelper LoadingAnimationHelper
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
        return this.ColumnData.View;
      }
    }

    FrameworkElement ISupportLoadingAnimation.Element
    {
      get
      {
        return (FrameworkElement) this.textBlock;
      }
    }

    bool ISupportLoadingAnimation.IsGroupRow
    {
      get
      {
        return true;
      }
    }

    bool ISupportLoadingAnimation.IsReady
    {
      get
      {
        return this.ColumnData.GroupRowData.IsReady;
      }
    }

    static GroupRowColumnSummaryControl()
    {
      Type forType = typeof (GroupRowColumnSummaryControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    internal void SyncWithColumnData()
    {
      if (this.ColumnData == null)
        return;
      this.UpdateTextBlock();
      this.UpdateBorderThickness();
      this.UpdateBorderWidth();
      this.UpdateBorderBrush();
    }

    private void UpdateBorderThickness()
    {
      if (this.border == null)
        return;
      this.border.BorderThickness = this.ColumnData.HasRightSibling ? GroupRowColumnSummaryControl.normalThickness : GroupRowColumnSummaryControl.rightThickness;
    }

    internal void UpdateBorderWidth()
    {
      if (this.border == null)
        return;
      this.ColumnData.OnActualHeaderWidthChange();
      this.border.Width = this.ColumnData.ActualWidth;
    }

    private void UpdateBorderBrush()
    {
      if (this.border == null || this.BorderBrushes == null || this.View == null)
        return;
      this.border.BorderBrush = this.BorderBrushes.GetBrush(!this.ColumnData.GroupRowData.IsFocused || !this.View.IsKeyboardFocusWithin || !this.View.FadeSelectionOnLostFocus ? "Normal" : "Focused");
    }

    private void UpdateTextBlock()
    {
      if (this.ColumnData.HasSummary)
      {
        if (this.textBlock == null && this.border != null)
        {
          this.textBlock = new TextBlock()
          {
            TextAlignment = TextAlignment.Right,
            TextTrimming = TextTrimming.CharacterEllipsis
          };
          TextBlockService.SetAllowIsTextTrimmed((DependencyObject) this.textBlock, true);
          TextBlockService.AddIsTextTrimmedChangedHandler((DependencyObject) this.textBlock, new RoutedEventHandler(this.OnIsTextTrimmedChanged));
          this.border.Child = (UIElement) this.textBlock;
          this.updater.TextBlock = this.textBlock;
        }
      }
      else
      {
        this.textBlock = (TextBlock) null;
        if (this.border != null)
          this.border.Child = (UIElement) null;
        this.updater.TextBlock = (TextBlock) null;
      }
      this.UpdateTextBlockValue();
    }

    private void UpdateTextBlockValue()
    {
      InlineCollectionInfo summaryTextInfo = this.ColumnData.SummaryTextInfo;
      if (summaryTextInfo == null)
        return;
      this.updater.UseInlines = summaryTextInfo.HasStyle;
      this.updater.Update(summaryTextInfo);
    }

    private void OnIsTextTrimmedChanged(object o, RoutedEventArgs e)
    {
      this.textBlock.ToolTip = TextBlockService.GetIsTextTrimmed(this.textBlock) ? this.ColumnData.Value : DependencyProperty.UnsetValue;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.border = (Border) this.GetTemplateChild("PART_Border");
      this.SyncWithColumnData();
    }

    void IGroupRowColumnSummaryClient.UpdateFocusState()
    {
      this.UpdateBorderBrush();
    }

    void IGroupRowColumnSummaryClient.UpdateIsReady()
    {
      if (this.ColumnData == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }

    void IGroupRowColumnSummaryClient.UpdateHasSummary()
    {
      this.UpdateTextBlock();
    }

    void IGroupRowColumnSummaryClient.UpdateSummaryValue()
    {
      this.UpdateTextBlockValue();
    }
  }
}
