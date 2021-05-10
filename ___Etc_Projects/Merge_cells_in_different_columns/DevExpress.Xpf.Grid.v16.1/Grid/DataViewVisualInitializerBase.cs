// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DataViewVisualInitializerBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public abstract class DataViewVisualInitializerBase : INotifyCurrentViewChanged
  {
    void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
    {
      DataViewBase currentView = DataControlBase.GetCurrentView(d);
      if (currentView == null)
        return;
      this.Execute(currentView, (FrameworkElement) d);
    }

    protected abstract void Execute(DataViewBase view, FrameworkElement element);
  }
}
