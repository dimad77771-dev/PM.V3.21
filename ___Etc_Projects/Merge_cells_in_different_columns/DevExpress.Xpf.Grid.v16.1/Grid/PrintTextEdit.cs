// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintTextEdit
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  [DXToolboxBrowsable(false)]
  public class PrintTextEdit : TextEdit
  {
    public static readonly DependencyProperty IsTopBorderVisibleProperty = DependencyPropertyManager.Register("IsTopBorderVisible", typeof (bool), typeof (PrintTextEdit), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((PrintTextEdit) d).OnIsTopBorderVisibleChanged())));
    public static readonly DependencyProperty IsBottomRowProperty = DependencyPropertyManager.Register("IsBottomRow", typeof (bool), typeof (PrintTextEdit), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((PrintTextEdit) d).OnIsTopBorderVisibleChanged())));
    public static readonly DependencyProperty DetailLevelProperty = DependencyPropertyManager.Register("DetailLevel", typeof (int), typeof (PrintTextEdit), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((PrintTextEdit) d).OnIsTopBorderVisibleChanged())));
    private bool isPageUpdaterCreated;

    public bool IsTopBorderVisible
    {
      get
      {
        return (bool) this.GetValue(PrintTextEdit.IsTopBorderVisibleProperty);
      }
      set
      {
        this.SetValue(PrintTextEdit.IsTopBorderVisibleProperty, (object) value);
      }
    }

    public bool IsBottomRow
    {
      get
      {
        return (bool) this.GetValue(PrintTextEdit.IsBottomRowProperty);
      }
      set
      {
        this.SetValue(PrintTextEdit.IsBottomRowProperty, (object) value);
      }
    }

    public int DetailLevel
    {
      get
      {
        return (int) this.GetValue(PrintTextEdit.DetailLevelProperty);
      }
      set
      {
        this.SetValue(PrintTextEdit.DetailLevelProperty, (object) value);
      }
    }

    private void OnIsTopBorderVisibleChanged()
    {
      if (this.isPageUpdaterCreated)
        this.ClearUpdater();
      IOnPageUpdater onPageUpdater = !this.IsTopBorderVisible ? (IOnPageUpdater) new InfoProviderOnPageUpdater() : (!this.IsBottomRow ? (IOnPageUpdater) new TopBorderOnPageUpdater() : (IOnPageUpdater) new FooterRowTobBorgerOnPageUpdater());
      ((InfoProviderOnPageUpdaterBase) onPageUpdater).DetailLevel = this.DetailLevel;
      ExportSettings.SetOnPageUpdater((DependencyObject) this, onPageUpdater);
      this.isPageUpdaterCreated = true;
    }

    private void ClearUpdater()
    {
      ExportSettings.SetOnPageUpdater((DependencyObject) this, (IOnPageUpdater) null);
      this.isPageUpdaterCreated = false;
    }
  }
}
