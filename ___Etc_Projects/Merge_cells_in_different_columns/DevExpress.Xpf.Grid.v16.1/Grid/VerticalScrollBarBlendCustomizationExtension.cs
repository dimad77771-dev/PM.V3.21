// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.VerticalScrollBarBlendCustomizationExtension
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class VerticalScrollBarBlendCustomizationExtension : DependencyObject, INotifyCurrentViewChanged
  {
    public static readonly DependencyProperty ElementVisibilityProperty = DependencyProperty.Register("ElementVisibility", typeof (Visibility), typeof (VerticalScrollBarBlendCustomizationExtension), new PropertyMetadata((object) Visibility.Visible, (PropertyChangedCallback) ((d, e) => (d as VerticalScrollBarBlendCustomizationExtension).UpdateVerticalScrollbarWidth())));

    public Visibility ElementVisibility
    {
      get
      {
        return (Visibility) this.GetValue(VerticalScrollBarBlendCustomizationExtension.ElementVisibilityProperty);
      }
      set
      {
        this.SetValue(VerticalScrollBarBlendCustomizationExtension.ElementVisibilityProperty, (object) value);
      }
    }

    private FrameworkElement Element { get; set; }

    void INotifyCurrentViewChanged.OnCurrentViewChanged(DependencyObject d)
    {
      this.Element = d as FrameworkElement;
      this.Element.SizeChanged += (SizeChangedEventHandler) ((param0, param1) => this.UpdateVerticalScrollbarWidth());
      BindingOperations.SetBinding((DependencyObject) this, VerticalScrollBarBlendCustomizationExtension.ElementVisibilityProperty, (BindingBase) new Binding("Visibility")
      {
        Source = (object) d
      });
      this.UpdateVerticalScrollbarWidth();
    }

    private void UpdateVerticalScrollbarWidth()
    {
      DataViewBase currentView = DataControlBase.GetCurrentView((DependencyObject) this.Element);
      if (currentView == null)
        return;
      currentView.ViewBehavior.SetVerticalScrollBarWidth(ColumnsLayoutParametersValidator.GetVerticalScrollBarWidth(this.Element.Visibility == Visibility.Visible ? this.Element.ActualWidth : 0.0));
    }
  }
}
