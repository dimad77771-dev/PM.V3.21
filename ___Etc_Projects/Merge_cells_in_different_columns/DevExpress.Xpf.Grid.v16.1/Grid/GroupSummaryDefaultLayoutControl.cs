// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryDefaultLayoutControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GroupSummaryDefaultLayoutControl : ItemsControl, ISupportLoadingAnimation
  {
    public static readonly DependencyProperty IsReadyProperty = DependencyProperty.Register("IsReady", typeof (bool), typeof (GroupSummaryDefaultLayoutControl), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((GroupSummaryDefaultLayoutControl) d).OnIsReadyChanged())));
    private LoadingAnimationHelper loadingAnimationHelper;

    public bool IsReady
    {
      get
      {
        return (bool) this.GetValue(GroupSummaryDefaultLayoutControl.IsReadyProperty);
      }
      set
      {
        this.SetValue(GroupSummaryDefaultLayoutControl.IsReadyProperty, (object) value);
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

    public DataViewBase DataView
    {
      get
      {
        return ((RowDataBase) this.DataContext).View;
      }
    }

    public FrameworkElement Element
    {
      get
      {
        return (FrameworkElement) this;
      }
    }

    public bool IsGroupRow
    {
      get
      {
        return true;
      }
    }

    public GroupSummaryDefaultLayoutControl()
    {
      this.SetDefaultStyleKey(typeof (GroupSummaryDefaultLayoutControl));
      this.SetBinding(ItemsControl.ItemsSourceProperty, (BindingBase) new Binding("GroupSummaryData"));
      this.SetBinding(GroupSummaryDefaultLayoutControl.IsReadyProperty, (BindingBase) new Binding("IsReady"));
    }

    private void OnIsReadyChanged()
    {
      if (this.DataContext == null)
        return;
      this.LoadingAnimationHelper.ApplyAnimation();
    }
  }
}
