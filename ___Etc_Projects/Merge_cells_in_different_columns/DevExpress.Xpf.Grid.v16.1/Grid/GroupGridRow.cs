// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupGridRow
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;

namespace DevExpress.Xpf.Grid
{
  public class GroupGridRow : GroupGridControl, IFixedGroupElement
  {
    private DevExpress.Xpf.Grid.FixedGroupElement fixedGroupElementCore;

    private IFixedGroupElement FixedGroupElement
    {
      get
      {
        if (this.fixedGroupElementCore == null)
          this.fixedGroupElementCore = new DevExpress.Xpf.Grid.FixedGroupElement((Func<GroupRowData>) (() => this.DataContext as GroupRowData));
        return (IFixedGroupElement) this.fixedGroupElementCore;
      }
    }

    public GroupGridRow()
    {
      this.SetDefaultStyleKey(typeof (GroupGridRow));
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
