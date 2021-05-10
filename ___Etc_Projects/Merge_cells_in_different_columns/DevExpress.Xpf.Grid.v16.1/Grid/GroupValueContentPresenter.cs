// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupValueContentPresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Utils;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupValueContentPresenter : GridDataContentPresenter, IGroupValuePresenter
  {
    GridGroupValueData IGroupValuePresenter.ValueData
    {
      get
      {
        return this.Content as GridGroupValueData;
      }
      set
      {
        this.Content = (object) value;
      }
    }

    bool? IGroupValuePresenter.UseTemplate
    {
      get
      {
        return new bool?(true);
      }
    }

    FrameworkElement IGroupValuePresenter.Element
    {
      get
      {
        return (FrameworkElement) this;
      }
    }

    public GroupValueContentPresenter()
    {
      this.SetDefaultStyleKey(typeof (GroupValueContentPresenter));
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new GroupValueTableViewHitTestAcceptor());
    }

    [SpecialName]
    DataTemplateSelector IGroupValuePresenter.get_ContentTemplateSelector()
    {
      return this.ContentTemplateSelector;
    }

    [SpecialName]
    void IGroupValuePresenter.set_ContentTemplateSelector([In] DataTemplateSelector obj0)
    {
      this.ContentTemplateSelector = obj0;
    }
  }
}
