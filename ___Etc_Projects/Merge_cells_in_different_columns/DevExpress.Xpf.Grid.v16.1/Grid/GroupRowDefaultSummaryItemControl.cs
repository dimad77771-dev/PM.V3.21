// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowDefaultSummaryItemControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowDefaultSummaryItemControl : Control, IDefaultGroupSummaryItem
  {
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (GroupRowDefaultSummaryItemControl), new PropertyMetadata((PropertyChangedCallback) null));
    private GroupRowDefaultSummaryItemController controller;

    public string Text
    {
      get
      {
        return (string) this.GetValue(GroupRowDefaultSummaryItemControl.TextProperty);
      }
      set
      {
        this.SetValue(GroupRowDefaultSummaryItemControl.TextProperty, (object) value);
      }
    }

    GridGroupSummaryData IDefaultGroupSummaryItem.ValueData
    {
      get
      {
        return this.controller.ValueData;
      }
      set
      {
        this.controller.ValueData = value;
      }
    }

    static GroupRowDefaultSummaryItemControl()
    {
      Type forType = typeof (GroupRowDefaultSummaryItemControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new GroupSummaryTableViewHitTestAcceptor()));
    }

    public GroupRowDefaultSummaryItemControl()
    {
      this.controller = new GroupRowDefaultSummaryItemController(this);
    }
  }
}
