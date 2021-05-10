// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListNodeExpandButton
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class TreeListNodeExpandButton : GridExpandButtonBase
  {
    public static readonly DependencyProperty IsExpandButtonVisibleProperty = DependencyPropertyManager.Register("IsExpandButtonVisible", typeof (bool), typeof (TreeListNodeExpandButton), new PropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((TreeListNodeExpandButton) d).OnIsVisibleChanged())));

    public bool IsExpandButtonVisible
    {
      get
      {
        return (bool) this.GetValue(TreeListNodeExpandButton.IsExpandButtonVisibleProperty);
      }
      set
      {
        this.SetValue(TreeListNodeExpandButton.IsExpandButtonVisibleProperty, (object) value);
      }
    }

    public TreeListNodeExpandButton()
    {
      this.SetDefaultStyleKey(typeof (TreeListNodeExpandButton));
      GridViewHitInfoBase.SetHitTestAcceptor((DependencyObject) this, (DataViewHitTestAcceptorBase) new TreeListNodeExpandButtonHitTestAcceptor());
    }

    private void OnIsVisibleChanged()
    {
    }
  }
}
