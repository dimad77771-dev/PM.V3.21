// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupPanelInitializer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GroupPanelInitializer : DataViewVisualInitializerBase
  {
    protected override void Execute(DataViewBase view, FrameworkElement element)
    {
      GridViewBase gridViewBase = view as GridViewBase;
      if (gridViewBase == null)
        return;
      gridViewBase.GroupPanel = element;
      if (element == null)
        return;
      element.SetBinding(GroupPanelControl.IsGroupedProperty, (BindingBase) new Binding(GridViewBase.IsGroupPanelTextVisibleProperty.GetName())
      {
        Source = (object) gridViewBase,
        Converter = (IValueConverter) new NegationConverterExtension()
      });
      element.SetBinding(UIElement.VisibilityProperty, (BindingBase) new Binding(GridViewBase.IsGroupPanelVisibleProperty.GetName())
      {
        Source = (object) gridViewBase,
        Converter = (IValueConverter) new BoolToVisibilityConverter()
      });
    }
  }
}
