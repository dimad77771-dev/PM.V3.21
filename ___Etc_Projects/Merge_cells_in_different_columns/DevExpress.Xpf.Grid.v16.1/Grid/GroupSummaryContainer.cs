// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupSummaryContainer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid
{
  public class GroupSummaryContainer : ContentPresenter, INotifyCurrentRowDataChanged, INotifyPositionChanged
  {
    [IgnoreDependencyPropertiesConsistencyChecker]
    private static readonly DependencyProperty GroupSummaryDisplayModeProperty = DependencyProperty.Register("GroupSummaryDisplayMode", typeof (GroupSummaryDisplayMode), typeof (GroupSummaryContainer), new PropertyMetadata((object) GroupSummaryDisplayMode.Default, (PropertyChangedCallback) ((d, e) => ((GroupSummaryContainer) d).OnGroupSummaryDisplayModeChanged())));

    private GroupRowData RowData
    {
      get
      {
        return DevExpress.Xpf.Grid.RowData.GetCurrentRowData((DependencyObject) this) as GroupRowData;
      }
    }

    public GroupSummaryContainer()
    {
      this.ContentTemplate = (DataTemplate) null;
      this.ContentTemplateSelector = (DataTemplateSelector) null;
    }

    private void OnGroupSummaryDisplayModeChanged()
    {
      if (this.RowData == null)
        return;
      this.UpdateContent();
    }

    private void UpdateContent()
    {
      bool flag = this.RowData.View is TableView && ((TableView) this.RowData.View).GroupSummaryDisplayMode == GroupSummaryDisplayMode.AlignByColumns;
      if (flag && !(this.Content is GroupSummaryAlignByColumnsLayoutControl))
        this.Content = (object) new GroupSummaryAlignByColumnsLayoutControl();
      if (flag || this.Content is GroupSummaryDefaultLayoutControl)
        return;
      this.Content = (object) new GroupSummaryDefaultLayoutControl();
    }

    void INotifyCurrentRowDataChanged.OnCurrentRowDataChanged()
    {
      GroupRowData groupRowData = DevExpress.Xpf.Grid.RowData.GetCurrentRowData((DependencyObject) this) as GroupRowData;
      if (groupRowData == null)
        return;
      if (groupRowData.View is TableView)
        this.SetBinding(GroupSummaryContainer.GroupSummaryDisplayModeProperty, (BindingBase) new Binding("View.GroupSummaryDisplayMode")
        {
          Source = (object) groupRowData
        });
      this.UpdateContent();
    }

    void INotifyPositionChanged.OnPositionChanged(Rect newRect)
    {
      GroupSummaryAlignByColumnsLayoutControl columnsLayoutControl = this.Content as GroupSummaryAlignByColumnsLayoutControl;
      if (columnsLayoutControl == null)
        return;
      columnsLayoutControl.SetContentOffset(-newRect.X);
    }
  }
}
