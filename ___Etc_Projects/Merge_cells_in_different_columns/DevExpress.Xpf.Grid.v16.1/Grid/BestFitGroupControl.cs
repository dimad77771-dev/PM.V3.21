// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.BestFitGroupControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class BestFitGroupControl : BestFitControlBase, IBestFitGroupRow
  {
    public static readonly DependencyProperty IsFirstColumnProperty = DependencyProperty.Register("IsFirstColumn", typeof (bool), typeof (BestFitGroupControl), new PropertyMetadata((object) false));

    public GroupColumnSummaryControl GroupColumnSummaryControl { get; set; }

    public GroupBandSummaryControl GroupBandSummaryControl { get; set; }

    public GridGroupSummaryColumnData SummaryData { get; set; }

    public GroupValueContentPresenter ValuePresenter { get; set; }

    public bool IsFirstColumn
    {
      get
      {
        return (bool) this.GetValue(BestFitGroupControl.IsFirstColumnProperty);
      }
      set
      {
        this.SetValue(BestFitGroupControl.IsFirstColumnProperty, (object) value);
      }
    }

    public BestFitGroupControl(DataViewBase view, ColumnBase column)
      : base(view, column)
    {
      this.SetDefaultStyleKey(typeof (BestFitGroupControl));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.GroupColumnSummaryControl = this.GetTemplateChild("PART_SummaryControlPresenter") as GroupColumnSummaryControl;
      this.GroupBandSummaryControl = this.GetTemplateChild("PART_BandSummaryControlPresenter") as GroupBandSummaryControl;
      this.ValuePresenter = this.GetTemplateChild("groupValueContentPresenter") as GroupValueContentPresenter;
      this.UpdateGroupColumnSummaryControl();
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = base.MeasureOverride(constraint);
      ++size.Width;
      return size;
    }

    private void UpdateGroupColumnSummaryControl()
    {
      if (this.Column.ParentBand == null)
      {
        if (this.GroupColumnSummaryControl == null)
          return;
        this.GroupColumnSummaryControl.DataContext = (object) this.SummaryData;
      }
      else
      {
        if (this.GroupBandSummaryControl == null)
          return;
        this.GroupBandSummaryControl.DataContext = (object) this.SummaryData;
      }
    }

    protected override void SetRowData(DataViewBase view)
    {
    }

    public void SetRowData(GroupRowData rowData)
    {
      this.RowData = (RowData) rowData;
      RowData.SetRowDataInternal((DependencyObject) this, this.RowData);
      this.DataContext = (object) rowData;
    }

    public void UpdateContent()
    {
      this.UpdateGroupColumnSummaryControl();
      if (this.RowData == null || this.ValuePresenter == null)
        return;
      this.ValuePresenter.Content = (object) ((GroupRowData) this.RowData).GroupValue;
    }
  }
}
