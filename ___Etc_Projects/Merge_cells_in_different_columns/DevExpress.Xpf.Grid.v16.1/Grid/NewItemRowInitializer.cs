// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.NewItemRowInitializer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class NewItemRowInitializer : DataViewVisualInitializerBase
  {
    protected override void Execute(DataViewBase view, FrameworkElement element)
    {
      TableView tableView = view as TableView;
      NewItemRowControl newItemRowControl = element as NewItemRowControl;
      if (tableView == null || newItemRowControl == null)
        return;
      newItemRowControl.SetBinding(UIElement.VisibilityProperty, (BindingBase) new Binding(TableView.NewItemRowPositionProperty.GetName())
      {
        Source = (object) view,
        Converter = (IValueConverter) new NewItemRowPositionToVisibilityConverter()
      });
    }
  }
}
