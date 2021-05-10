// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridGroupValueData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class GridGroupValueData : GridColumnData
  {
    private static readonly DependencyPropertyKey ColumnHeaderPropertyKey = DependencyPropertyManager.RegisterReadOnly("ColumnHeader", typeof (string), typeof (GridGroupValueData), new PropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((GridGroupValueData) d).OnColumnHeaderChanged())));
    public static readonly DependencyProperty ColumnHeaderProperty = GridGroupValueData.ColumnHeaderPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey DisplayTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("DisplayText", typeof (string), typeof (GridGroupValueData), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty DisplayTextProperty = GridGroupValueData.DisplayTextPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey TextPropertyKey = DependencyPropertyManager.RegisterReadOnly("Text", typeof (string), typeof (GridGroupValueData), new PropertyMetadata((object) string.Empty, (PropertyChangedCallback) ((d, e) => ((GridGroupValueData) d).OnTextChanged())));
    public static readonly DependencyProperty TextProperty = GridGroupValueData.TextPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey GroupColumnHeaderTextPropertyKey = DependencyPropertyManager.RegisterReadOnly("GroupColumnHeaderText", typeof (string), typeof (GridGroupValueData), new PropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty GroupColumnHeaderTextProperty = GridGroupValueData.GroupColumnHeaderTextPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey HighlightingPropertiesPropertyKey = DependencyPropertyManager.RegisterReadOnly("HighlightingProperties", typeof (GroupTextHighlightingProperties), typeof (GridGroupValueData), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridGroupValueData) d).OnHighlightingPropertiesChanged())));
    public static readonly DependencyProperty HighlightingPropertiesProperty = GridGroupValueData.HighlightingPropertiesPropertyKey.DependencyProperty;
    [IgnoreDependencyPropertiesConsistencyChecker]
    internal static readonly DependencyProperty DataInternalProperty = DependencyPropertyManager.Register("DataInternal", typeof (object), typeof (GridGroupValueData), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridDataBase) d).Data = e.NewValue)));
    private IGroupValueClient client;

    public GroupRowData RowData
    {
      get
      {
        return (GroupRowData) this.RowDataBase;
      }
    }

    public string ColumnHeader
    {
      get
      {
        return (string) this.GetValue(GridGroupValueData.ColumnHeaderProperty);
      }
      internal set
      {
        this.SetValue(GridGroupValueData.ColumnHeaderPropertyKey, (object) value);
      }
    }

    public string GroupColumnHeaderText
    {
      get
      {
        return (string) this.GetValue(GridGroupValueData.GroupColumnHeaderTextProperty);
      }
      internal set
      {
        this.SetValue(GridGroupValueData.GroupColumnHeaderTextPropertyKey, (object) value);
      }
    }

    public string DisplayText
    {
      get
      {
        return (string) this.GetValue(GridGroupValueData.DisplayTextProperty);
      }
      internal set
      {
        this.SetValue(GridGroupValueData.DisplayTextPropertyKey, (object) value);
      }
    }

    public string Text
    {
      get
      {
        return (string) this.GetValue(GridGroupValueData.TextProperty);
      }
      internal set
      {
        this.SetValue(GridGroupValueData.TextPropertyKey, (object) value);
      }
    }

    public GroupTextHighlightingProperties HighlightingProperties
    {
      get
      {
        return (GroupTextHighlightingProperties) this.GetValue(GridGroupValueData.HighlightingPropertiesProperty);
      }
      internal set
      {
        this.SetValue(GridGroupValueData.HighlightingPropertiesPropertyKey, (object) value);
      }
    }

    public GridGroupValueData(ColumnsRowDataBase rowData)
      : base(rowData)
    {
    }

    internal void SetGroupValueClient(IGroupValueClient client)
    {
      this.client = client;
    }

    private void OnColumnHeaderChanged()
    {
      if (this.client != null)
        this.client.UpdateColumnHeader();
      this.GroupColumnHeaderText = string.Format("{0}: ", (object) this.ColumnHeader);
    }

    private void OnTextChanged()
    {
      this.UpdateText();
    }

    private void OnHighlightingPropertiesChanged()
    {
      this.UpdateText();
    }

    private void UpdateText()
    {
      this.DisplayText = this.GroupColumnHeaderText + this.Text;
      if (this.client == null)
        return;
      this.client.UpdateText();
    }

    protected override void OnColumnChanged(ColumnBase newValue)
    {
      base.OnColumnChanged(newValue);
      GroupRowData rowData = this.RowData;
      if (rowData == null)
        return;
      rowData.UpdateClientGroupValueTemplateSelector();
    }
  }
}
