// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridRow
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GridRow : Control, IFocusedRowBorderObject, IGridDataRow
  {
    public static readonly DependencyProperty RowPositionProperty = DependencyPropertyManager.Register("RowPosition", typeof (RowPosition), typeof (GridRow), (PropertyMetadata) new FrameworkPropertyMetadata((object) RowPosition.Bottom, (PropertyChangedCallback) ((d, e) => ((GridRow) d).OnRowPositionChanged())));
    private ContentPresenter rowOffsetPresenter;

    public RowPosition RowPosition
    {
      get
      {
        return (RowPosition) this.GetValue(GridRow.RowPositionProperty);
      }
      set
      {
        this.SetValue(GridRow.RowPositionProperty, (object) value);
      }
    }

    public FrameworkElement RowDataContent { get; set; }

    double IFocusedRowBorderObject.LeftIndent
    {
      get
      {
        return 0.0;
      }
    }

    internal GridRowContent DataRowContainer { get; private set; }

    private RowData RowData { get; set; }

    RowData IGridDataRow.RowData
    {
      get
      {
        return this.DataContext as RowData;
      }
    }

    public GridRow()
    {
      this.SetDefaultStyleKey(typeof (GridRow));
      RowData.SetRowHandleBinding((FrameworkElement) this);
    }

    protected virtual void OnRowPositionChanged()
    {
    }

    protected override Size MeasureOverride(Size constraint)
    {
      Size size = base.MeasureOverride(constraint);
      size = new Size(Math.Ceiling(size.Width), Math.Ceiling(size.Height));
      return size;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.DataRowContainer = this.GetTemplateChild("PART_DataRow") as GridRowContent;
      this.RowDataContent = this.GetTemplateChild("PART_DataRowContent") as FrameworkElement;
      this.RowData = this.DataContext as RowData;
      this.RowData.PropertyChanged += new PropertyChangedEventHandler(this.rowData_PropertyChanged);
      this.rowOffsetPresenter = this.GetTemplateChild("PART_RowOffsetPresenter") as ContentPresenter;
      this.UpdateRowOffsetPresenter();
      this.SetBinding(GridRow.RowPositionProperty, (BindingBase) new Binding("RowPosition"));
    }

    private void rowData_PropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "Level"))
        return;
      this.UpdateRowOffsetPresenter();
    }

    private void UpdateRowOffsetPresenter()
    {
      if (this.rowOffsetPresenter == null || this.RowData == null || this.rowOffsetPresenter.Content != null)
        return;
      FrameworkElement frameworkElement = (FrameworkElement) GridRowHelper.CreateRowOffsetContent(this.RowData, (Control) this.DataRowContainer);
      if (frameworkElement == null)
        return;
      this.rowOffsetPresenter.Content = (object) frameworkElement;
    }

    void IGridDataRow.UpdateContentLayout()
    {
      if (this.RowData == null || this.RowData.View == null)
        return;
      this.RowData.View.UpdateContentLayout();
    }
  }
}
