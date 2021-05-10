// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BandData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BandData : DependencyObject
  {
    private static readonly DependencyPropertyKey ColumnPropertyKey = DependencyPropertyManager.RegisterReadOnly("Column", typeof (BaseColumn), typeof (BandData), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty ColumnProperty = BandData.ColumnPropertyKey.DependencyProperty;

    public BaseColumn Column
    {
      get
      {
        return (BaseColumn) this.GetValue(BandData.ColumnProperty);
      }
      internal set
      {
        this.SetValue(BandData.ColumnPropertyKey, (object) value);
      }
    }
  }
}
