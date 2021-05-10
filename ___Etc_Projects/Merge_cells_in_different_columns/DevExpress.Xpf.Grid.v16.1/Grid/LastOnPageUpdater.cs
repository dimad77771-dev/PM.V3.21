// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LastOnPageUpdater
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Printing.Native;
using DevExpress.XtraPrinting;
using System.Collections.ObjectModel;

namespace DevExpress.Xpf.Grid
{
  public class LastOnPageUpdater : IOnPageUpdater
  {
    void IOnPageUpdater.Update(IVisualBrick brick)
    {
      PanelBrick panelBrick = brick as PanelBrick;
      if (panelBrick != null)
      {
        foreach (VisualBrick brick1 in (Collection<Brick>) panelBrick.Bricks)
          brick1.Sides = BorderSide.None;
      }
      brick.Sides = BorderSide.None;
    }
  }
}
