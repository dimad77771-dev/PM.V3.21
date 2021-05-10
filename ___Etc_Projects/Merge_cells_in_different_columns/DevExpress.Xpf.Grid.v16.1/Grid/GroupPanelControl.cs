// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupPanelControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupPanelControl : Control
  {
    public static readonly DependencyProperty ViewProperty = DependencyPropertyManager.Register("View", typeof (DataViewBase), typeof (GroupPanelControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupPanelControl) d).OnViewChanged())));
    public static readonly DependencyProperty IsGroupedProperty = DependencyPropertyManager.Register("IsGrouped", typeof (bool), typeof (GroupPanelControl), new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GroupPanelControl) d).UpdateGroupPanelDragText())));
    private TextBlock groupPanelDragText;

    public DataViewBase View
    {
      get
      {
        return (DataViewBase) this.GetValue(GroupPanelControl.ViewProperty);
      }
      set
      {
        this.SetValue(GroupPanelControl.ViewProperty, (object) value);
      }
    }

    public bool IsGrouped
    {
      get
      {
        return (bool) this.GetValue(GroupPanelControl.IsGroupedProperty);
      }
      set
      {
        this.SetValue(GroupPanelControl.IsGroupedProperty, (object) value);
      }
    }

    public GroupPanelControl()
    {
      this.SetDefaultStyleKey(typeof (GroupPanelControl));
      DataControlBase.SetCurrentViewChangedListener((DependencyObject) this, (INotifyCurrentViewChanged) new GroupPanelInitializer());
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new GroupPanelTableViewHitTestAcceptor());
      DataControlPopupMenu.SetGridMenuType((DependencyObject) this, new GridMenuType?(GridMenuType.GroupPanel));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.groupPanelDragText = this.GetTemplateChild("PART_GroupPanelDragText") as TextBlock;
      this.UpdateGroupPanelDragText();
    }

    private void UpdateGroupPanelDragText()
    {
      if (this.groupPanelDragText == null)
        return;
      if (this.View != null && this.View.DataControl != null && this.View.DataControl.DetailDescriptorCore != null)
        this.groupPanelDragText.Visibility = Visibility.Collapsed;
      else
        this.groupPanelDragText.Visibility = this.IsGrouped ? Visibility.Collapsed : Visibility.Visible;
    }

    private void OnViewChanged()
    {
      DataControlBase.SetCurrentView((DependencyObject) this, this.View);
    }
  }
}
