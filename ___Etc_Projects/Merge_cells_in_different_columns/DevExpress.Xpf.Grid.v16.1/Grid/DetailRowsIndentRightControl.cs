// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailRowsIndentRightControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class DetailRowsIndentRightControl : ItemsControl
  {
    public static readonly DependencyProperty ItemsSourceToReverseProperty = DependencyPropertyManager.Register("ItemsSourceToReverse", typeof (IEnumerable), typeof (DetailRowsIndentRightControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DetailRowsIndentRightControl) d).OnItemsSourceToReverseChanged())));

    public IEnumerable ItemsSourceToReverse
    {
      get
      {
        return (IEnumerable) this.GetValue(DetailRowsIndentRightControl.ItemsSourceToReverseProperty);
      }
      set
      {
        this.SetValue(DetailRowsIndentRightControl.ItemsSourceToReverseProperty, (object) value);
      }
    }

    public DetailRowsIndentRightControl()
    {
      this.SetDefaultStyleKey(typeof (DetailRowsIndentRightControl));
    }

    private void OnItemsSourceToReverseChanged()
    {
      IList<DetailIndent> detailIndentList1 = this.ItemsSourceToReverse as IList<DetailIndent>;
      if (detailIndentList1 == null || detailIndentList1.Count < 2)
      {
        this.ItemsSource = this.ItemsSourceToReverse;
      }
      else
      {
        List<DetailIndent> detailIndentList2 = new List<DetailIndent>();
        for (int index = detailIndentList1.Count - 1; index >= 0; --index)
          detailIndentList2.Add(detailIndentList1[index]);
        this.ItemsSource = (IEnumerable) detailIndentList2;
      }
    }
  }
}
