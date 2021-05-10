// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowFixedLineSeparatorControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowFixedLineSeparatorControl : RowFixedLineSeparatorControl
  {
    static GroupRowFixedLineSeparatorControl()
    {
      Type forType = typeof (GroupRowFixedLineSeparatorControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    public GroupRowFixedLineSeparatorControl(Func<TableViewBehavior, IList<ColumnBase>> getFixedColumnsFunc, Func<BandsLayoutBase, IList<BandBase>> getFixedBandsFunc)
      : base(getFixedColumnsFunc, getFixedBandsFunc)
    {
    }
  }
}
