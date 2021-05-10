// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.GridServerModeDataControllerPrintInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Xpf.Grid;
using System.Collections.Generic;

namespace DevExpress.Xpf.Data
{
  internal class GridServerModeDataControllerPrintInfo
  {
    public BaseGridController Controller { get; private set; }

    public Dictionary<ColumnBase, string> Summaries { get; private set; }

    public string FixedLeftSummaryText { get; private set; }

    public string FixedRightSummaryText { get; private set; }

    public GridServerModeDataControllerPrintInfo(BaseGridController controller, Dictionary<ColumnBase, string> summaries, string fixedLeftSummaryText, string fixedRightSummaryText)
    {
      this.Controller = controller;
      this.Summaries = summaries;
      this.FixedLeftSummaryText = fixedLeftSummaryText;
      this.FixedRightSummaryText = fixedRightSummaryText;
    }
  }
}
