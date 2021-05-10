// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BackgroundBorderControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class BackgroundBorderControl : ContentControl
  {
    public static readonly DependencyProperty RowLevelProperty = DependencyPropertyManager.Register("RowLevel", typeof (int), typeof (BackgroundBorderControl), (PropertyMetadata) new UIPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((BackgroundBorderControl) d).UpdateMargin())));
    public static readonly DependencyProperty ItemLevelProperty = DependencyPropertyManager.Register("ItemLevel", typeof (int), typeof (BackgroundBorderControl), (PropertyMetadata) new UIPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((BackgroundBorderControl) d).UpdateMargin())));
    public static readonly DependencyProperty LineLevelProperty = DependencyPropertyManager.Register("LineLevel", typeof (int), typeof (BackgroundBorderControl), (PropertyMetadata) new UIPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((BackgroundBorderControl) d).UpdateMargin())));
    public static readonly DependencyProperty IsMasterRowExpandedProperty = DependencyPropertyManager.Register("IsMasterRowExpanded", typeof (bool), typeof (BackgroundBorderControl), (PropertyMetadata) new UIPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((BackgroundBorderControl) d).UpdateMargin())));

    public int RowLevel
    {
      get
      {
        return (int) this.GetValue(BackgroundBorderControl.RowLevelProperty);
      }
      set
      {
        this.SetValue(BackgroundBorderControl.RowLevelProperty, (object) value);
      }
    }

    public int ItemLevel
    {
      get
      {
        return (int) this.GetValue(BackgroundBorderControl.ItemLevelProperty);
      }
      set
      {
        this.SetValue(BackgroundBorderControl.ItemLevelProperty, (object) value);
      }
    }

    public int LineLevel
    {
      get
      {
        return (int) this.GetValue(BackgroundBorderControl.LineLevelProperty);
      }
      set
      {
        this.SetValue(BackgroundBorderControl.LineLevelProperty, (object) value);
      }
    }

    public bool IsMasterRowExpanded
    {
      get
      {
        return (bool) this.GetValue(BackgroundBorderControl.IsMasterRowExpandedProperty);
      }
      set
      {
        this.SetValue(BackgroundBorderControl.IsMasterRowExpandedProperty, (object) value);
      }
    }

    public BackgroundBorderControl()
    {
      this.SetDefaultStyleKey(typeof (BackgroundBorderControl));
    }

    private void UpdateMargin()
    {
      if (this.RowLevel - this.LineLevel < this.ItemLevel && !this.IsMasterRowExpanded)
        this.Margin = new Thickness(0.0, 0.0, 0.0, 1.0);
      else
        this.Margin = new Thickness(0.0);
    }
  }
}
