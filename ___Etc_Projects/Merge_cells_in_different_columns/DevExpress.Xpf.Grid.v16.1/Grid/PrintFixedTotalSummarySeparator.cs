// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintFixedTotalSummarySeparator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  [DXToolboxBrowsable(false)]
  public class PrintFixedTotalSummarySeparator : PrintTextEdit
  {
    public static readonly DependencyProperty EditValueLeftSideProperty = DependencyPropertyManager.Register("EditValueLeftSide", typeof (string), typeof (PrintFixedTotalSummarySeparator), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((PrintFixedTotalSummarySeparator) d).OnSideEditValueChanged())));
    public static readonly DependencyProperty EditValueRightSideProperty = DependencyPropertyManager.Register("EditValueRightSide", typeof (string), typeof (PrintFixedTotalSummarySeparator), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((PrintFixedTotalSummarySeparator) d).OnSideEditValueChanged())));
    public static readonly DependencyProperty ActualVisibilityProperty = DependencyPropertyManager.Register("ActualVisibility", typeof (Visibility), typeof (PrintFixedTotalSummarySeparator), new PropertyMetadata((object) Visibility.Visible));

    public string EditValueLeftSide
    {
      get
      {
        return (string) this.GetValue(PrintFixedTotalSummarySeparator.EditValueLeftSideProperty);
      }
      set
      {
        this.SetValue(PrintFixedTotalSummarySeparator.EditValueLeftSideProperty, (object) value);
      }
    }

    public string EditValueRightSide
    {
      get
      {
        return (string) this.GetValue(PrintFixedTotalSummarySeparator.EditValueRightSideProperty);
      }
      set
      {
        this.SetValue(PrintFixedTotalSummarySeparator.EditValueRightSideProperty, (object) value);
      }
    }

    public Visibility ActualVisibility
    {
      get
      {
        return (Visibility) this.GetValue(PrintFixedTotalSummarySeparator.ActualVisibilityProperty);
      }
      set
      {
        this.SetValue(PrintFixedTotalSummarySeparator.ActualVisibilityProperty, (object) value);
      }
    }

    private void OnSideEditValueChanged()
    {
      if (!string.IsNullOrEmpty(this.EditValueLeftSide) && !string.IsNullOrEmpty(this.EditValueRightSide))
        this.ActualVisibility = Visibility.Visible;
      else if (string.IsNullOrEmpty(this.EditValueLeftSide) && string.IsNullOrEmpty(this.EditValueRightSide))
        this.ActualVisibility = Visibility.Visible;
      else
        this.ActualVisibility = Visibility.Collapsed;
    }
  }
}
