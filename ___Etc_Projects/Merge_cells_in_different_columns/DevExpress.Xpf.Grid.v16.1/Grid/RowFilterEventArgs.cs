// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowFilterEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid
{
  public class RowFilterEventArgs : EventArgs
  {
    private int listSourceRowIndex;
    private bool handled;
    private bool visible;

    public bool Handled
    {
      get
      {
        return this.handled;
      }
      set
      {
        this.handled = value;
      }
    }

    public bool Visible
    {
      get
      {
        return this.visible;
      }
      set
      {
        this.visible = value;
      }
    }

    public int ListSourceRowIndex
    {
      get
      {
        return this.listSourceRowIndex;
      }
    }

    public GridControl Source { get; private set; }

    public RowFilterEventArgs(GridControl source, int listSourceRowIndex, bool fit)
    {
      this.Source = source;
      this.listSourceRowIndex = listSourceRowIndex;
      this.handled = false;
      this.visible = fit;
    }
  }
}
