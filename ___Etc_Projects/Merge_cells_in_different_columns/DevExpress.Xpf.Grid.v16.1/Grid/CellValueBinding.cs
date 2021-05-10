// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CellValueBinding
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  public class CellValueBinding : MarkupExtension
  {
    public string FieldName { get; set; }

    public IValueConverter Converter { get; set; }

    public object ConverterParameter { get; set; }

    public override object ProvideValue(IServiceProvider serviceProvider)
    {
      return (object) new Binding() { Mode = BindingMode.OneWay, RelativeSource = new RelativeSource(RelativeSourceMode.Self), Path = new PropertyPath("Data." + this.FieldName, new object[0]), Converter = this.Converter, ConverterParameter = this.ConverterParameter };
    }
  }
}
