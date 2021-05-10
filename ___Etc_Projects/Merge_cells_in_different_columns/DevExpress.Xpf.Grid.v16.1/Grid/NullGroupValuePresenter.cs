// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.NullGroupValuePresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  internal class NullGroupValuePresenter : IGroupValuePresenter
  {
    GridGroupValueData IGroupValuePresenter.ValueData
    {
      get
      {
        return (GridGroupValueData) null;
      }
      set
      {
      }
    }

    bool? IGroupValuePresenter.UseTemplate
    {
      get
      {
        return new bool?();
      }
    }

    FrameworkElement IGroupValuePresenter.Element
    {
      get
      {
        return (FrameworkElement) null;
      }
    }

    DataTemplateSelector IGroupValuePresenter.ContentTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) null;
      }
      set
      {
      }
    }
  }
}
