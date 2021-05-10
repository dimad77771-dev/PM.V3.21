// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardCellContentPresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class CardCellContentPresenter : CellContentPresenter
  {
    public static readonly DependencyProperty CellStyleProperty = DependencyProperty.Register("CellStyle", typeof (Style), typeof (CardCellContentPresenter), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((CardCellContentPresenter) d).UpdateStyle())));

    public Style CellStyle
    {
      get
      {
        return (Style) this.GetValue(CardCellContentPresenter.CellStyleProperty);
      }
      set
      {
        this.SetValue(CardCellContentPresenter.CellStyleProperty, (object) value);
      }
    }

    public CardCellContentPresenter()
    {
      this.SetDefaultStyleKey(typeof (CardCellContentPresenter));
    }

    protected override void OnColumnChanged()
    {
      base.OnColumnChanged();
      this.UpdateStyle();
    }

    private void UpdateStyle()
    {
      EditGridCellData editGridCellData = this.DataContext as EditGridCellData;
      if (editGridCellData == null || this.Column == null || editGridCellData.View != this.Column.View)
        return;
      this.Style = this.CellStyle;
    }

    protected override Size MeasureOverride(Size constraint)
    {
      this.UpdateStyle();
      return base.MeasureOverride(constraint);
    }
  }
}
