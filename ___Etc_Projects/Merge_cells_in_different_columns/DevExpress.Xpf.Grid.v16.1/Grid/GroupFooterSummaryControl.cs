// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupFooterSummaryControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupFooterSummaryControl : ContentControl, ISupportLoadingAnimation
  {
    public static readonly DependencyProperty HeaderWidthProperty = DependencyPropertyManager.Register("HeaderWidth", typeof (double), typeof (GroupFooterSummaryControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((GroupFooterSummaryControl) d).UpdateWidth())));
    public static readonly DependencyProperty LevelProperty = DependencyPropertyManager.Register("Level", typeof (int), typeof (GroupFooterSummaryControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((GroupFooterSummaryControl) d).UpdateWidth())));
    public static readonly DependencyProperty IsReadyProperty = DependencyPropertyManager.Register("IsReady", typeof (bool), typeof (GroupFooterSummaryControl), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((GroupFooterSummaryControl) d).OnIsReadyChanged())));
    private LoadingAnimationHelper loadingAnimationHelper;

    public double HeaderWidth
    {
      get
      {
        return (double) this.GetValue(GroupFooterSummaryControl.HeaderWidthProperty);
      }
      set
      {
        this.SetValue(GroupFooterSummaryControl.HeaderWidthProperty, (object) value);
      }
    }

    public int Level
    {
      get
      {
        return (int) this.GetValue(GroupFooterSummaryControl.LevelProperty);
      }
      set
      {
        this.SetValue(GroupFooterSummaryControl.LevelProperty, (object) value);
      }
    }

    public bool IsReady
    {
      get
      {
        return (bool) this.GetValue(GroupFooterSummaryControl.IsReadyProperty);
      }
      set
      {
        this.SetValue(GroupFooterSummaryControl.IsReadyProperty, (object) value);
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

    protected GridGroupSummaryColumnData SummaryData
    {
      get
      {
        return this.DataContext as GridGroupSummaryColumnData;
      }
    }

    protected GroupSummaryRowData RowData
    {
      get
      {
        if (this.SummaryData == null)
          return (GroupSummaryRowData) null;
        return this.SummaryData.GroupRowData as GroupSummaryRowData;
      }
    }

    DataViewBase ISupportLoadingAnimation.DataView
    {
      get
      {
        return this.SummaryData.View;
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

    public GroupFooterSummaryControl()
    {
      this.SetDefaultStyleKey(typeof (GroupFooterSummaryControl));
    }

    protected virtual void OnIsReadyChanged()
    {
      if (this.DataContext == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }

    protected virtual void UpdateWidth()
    {
      this.SummaryData.OnActualHeaderWidthChange();
      this.Width = this.SummaryData.ActualWidthFooter;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.SetValue(DataControlPopupMenu.GridMenuTypeProperty, (object) GridMenuType.GroupFooterSummary);
    }
  }
}
