// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailTabControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  [DXToolboxBrowsable(false)]
  public class DetailTabControl : DXTabControl
  {
    public static readonly DependencyProperty RowDataProperty = DependencyPropertyManager.Register("RowData", typeof (TabsDetailInfo.TabHeadersRowData), typeof (DetailTabControl), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DetailTabControl) d).OnRowDataChanged((TabsDetailInfo.TabHeadersRowData) e.OldValue))));
    public static readonly DependencyProperty ActualSelectedIndexProperty = DependencyProperty.Register("ActualSelectedIndex", typeof (int), typeof (DetailTabControl), new PropertyMetadata((object) -1, (PropertyChangedCallback) ((d, e) => ((DetailTabControl) d).OnActualSelectedIndexChanged())));

    public TabsDetailInfo.TabHeadersRowData RowData
    {
      get
      {
        return (TabsDetailInfo.TabHeadersRowData) this.GetValue(DetailTabControl.RowDataProperty);
      }
      set
      {
        this.SetValue(DetailTabControl.RowDataProperty, (object) value);
      }
    }

    public int ActualSelectedIndex
    {
      get
      {
        return (int) this.GetValue(DetailTabControl.ActualSelectedIndexProperty);
      }
      set
      {
        this.SetValue(DetailTabControl.ActualSelectedIndexProperty, (object) value);
      }
    }

    public DetailTabControl()
    {
      this.SelectionChanging += new TabControlSelectionChangingEventHandler(this.OnSelectionChanging);
      this.SelectionChanged += new TabControlSelectionChangedEventHandler(this.OnSelectionChanged);
    }

    private void OnSelectionChanging(object sender, TabControlSelectionChangingEventArgs e)
    {
      if (this.RowData == null)
        return;
      this.RowData.SelectedIndexLocker.DoIfNotLocked((Action) (() => e.Cancel = !this.RowData.CurrentTabsDetailInfo.MasterDataControl.DataView.CommitEditing()));
    }

    private void OnSelectionChanged(object sender, TabControlSelectionChangedEventArgs e)
    {
      if (this.RowData == null)
        return;
      this.ActualSelectedIndex = e.NewSelectedIndex;
    }

    private void OnActualSelectedIndexChanged()
    {
      this.SelectItem(this.ActualSelectedIndex, false);
    }

    private void OnRowDataChanged(TabsDetailInfo.TabHeadersRowData oldRowData)
    {
      if (oldRowData != null)
        BindingOperations.ClearBinding((DependencyObject) oldRowData, TabsDetailInfo.TabHeadersRowData.SelectedTabIndexProperty);
      if (this.RowData == null)
        return;
      this.RowData.SelectedIndexLocker.DoLockedAction((Action) (() => this.SelectItem(this.RowData.SelectedTabIndex, false)));
      BindingOperations.SetBinding((DependencyObject) this.RowData, TabsDetailInfo.TabHeadersRowData.SelectedTabIndexProperty, (BindingBase) new Binding("ActualSelectedIndex")
      {
        Mode = BindingMode.TwoWay,
        Source = (object) this
      });
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      LayoutHelper.FindElementByName((FrameworkElement) this, "PART_ContentContainer").Height = 1.0;
    }

    protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
    {
      base.PrepareContainerForItemOverride(element, item);
      DXTabItem dxTabItem = (DXTabItem) element;
      dxTabItem.Focusable = false;
      dxTabItem.IsTabStop = false;
    }
  }
}
