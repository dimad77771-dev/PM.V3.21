// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowDefaultSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowDefaultSummaryControl : CachedItemsControl, ISupportLoadingAnimation
  {
    private DataTemplateSelector itemTemplateSelectorCore;
    private LoadingAnimationHelper loadingAnimationHelper;

    internal DataTemplateSelector ItemTemplateSelector
    {
      get
      {
        return this.itemTemplateSelectorCore;
      }
      set
      {
        if (this.itemTemplateSelectorCore == value)
          return;
        this.itemTemplateSelectorCore = value;
        this.OnItemTemplateChanged();
      }
    }

    private bool UseDefaultItemTemplate
    {
      get
      {
        return this.itemTemplateSelectorCore == null;
      }
    }

    private RowData RowData
    {
      get
      {
        return this.DataContext as RowData;
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
        return this.RowData.View;
      }
    }

    FrameworkElement ISupportLoadingAnimation.Element
    {
      get
      {
        return (FrameworkElement) this;
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
        return this.RowData.IsReady;
      }
    }

    static GroupRowDefaultSummaryControl()
    {
      Type forType = typeof (GroupRowDefaultSummaryControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    protected override FrameworkElement CreateChild(object item)
    {
      FrameworkElement frameworkElement;
      if (this.UseDefaultItemTemplate)
      {
        frameworkElement = (FrameworkElement) new GroupRowDefaultSummaryItemControl();
      }
      else
      {
        GroupSummaryContentPresenter contentPresenter = new GroupSummaryContentPresenter();
        contentPresenter.ContentTemplateSelector = this.ItemTemplateSelector;
        frameworkElement = (FrameworkElement) contentPresenter;
      }
      frameworkElement.VerticalAlignment = VerticalAlignment.Center;
      return frameworkElement;
    }

    protected override void ValidateElement(FrameworkElement element, object item)
    {
      base.ValidateElement(element, item);
      ((IDefaultGroupSummaryItem) element).ValueData = (GridGroupSummaryData) item;
    }

    internal void UpdateIsReady()
    {
      if (this.RowData == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }
  }
}
