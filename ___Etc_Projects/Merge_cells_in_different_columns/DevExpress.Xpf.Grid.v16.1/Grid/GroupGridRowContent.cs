// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupGridRowContent
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupGridRowContent : ContentControl
  {
    public static readonly DependencyProperty CurrentHeightProperty = DependencyPropertyManager.Register("CurrentHeight", typeof (double), typeof (GroupGridRowContent), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0));

    public double CurrentHeight
    {
      get
      {
        return (double) this.GetValue(GroupGridRowContent.CurrentHeightProperty);
      }
      set
      {
        this.SetValue(GroupGridRowContent.CurrentHeightProperty, (object) value);
      }
    }

    public GroupGridRowContent()
    {
      this.SetDefaultStyleKey(typeof (GroupGridRowContent));
      this.SizeChanged += new SizeChangedEventHandler(this.GroupGridRowContent_SizeChanged);
    }

    private void GroupGridRowContent_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.CurrentHeight = e.NewSize.Height;
    }
  }
}
