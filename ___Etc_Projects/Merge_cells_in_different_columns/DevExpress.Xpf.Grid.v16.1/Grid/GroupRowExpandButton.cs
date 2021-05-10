// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowExpandButton
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowExpandButton : GridExpandButtonBase
  {
    static GroupRowExpandButton()
    {
      Type forType = typeof (GroupRowExpandButton);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new GroupRowButtonTableViewHitTestAcceptor()));
    }

    protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
    {
      base.OnMouseLeftButtonDown(e);
      if (e.ClickCount != 2)
        return;
      e.Handled = true;
    }
  }
}
