// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.ColumnWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Export.Xl;
using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraExport.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace DevExpress.Xpf.Grid.Printing
{
  public class ColumnWrapper : IColumn
  {
    private XlCellFormatting formatingCellCore;
    private XlCellFormatting formatingHeaderCore;

    public ColumnBase Column { get; private set; }

    protected DataViewBase View
    {
      get
      {
        return this.Column.View;
      }
    }

    protected GridColumn GridColumn
    {
      get
      {
        return this.Column as GridColumn;
      }
    }

    public virtual string Name
    {
      get
      {
        return this.Column.GetHashCode().ToString();
      }
    }

    public virtual string FieldName
    {
      get
      {
        return this.Column.FieldName;
      }
    }

    public ISparklineInfo SparklineInfo
    {
      get
      {
        if (this.ColEditType != ColumnEditTypes.Sparkline)
          return (ISparklineInfo) null;
        return (ISparklineInfo) new DevExpress.Xpf.Grid.Printing.SparklineInfo(this.Column.EditSettings as SparklineEditSettings);
      }
    }

    public virtual bool IsGroupColumn
    {
      get
      {
        return false;
      }
    }

    public bool IsCollapsed
    {
      get
      {
        return false;
      }
    }

    public string HyperlinkEditorCaption
    {
      get
      {
        return string.Empty;
      }
    }

    public string HyperlinkTextFormatString
    {
      get
      {
        return string.Empty;
      }
    }

    public IUnboundInfo UnboundInfo
    {
      get
      {
        return (IUnboundInfo) new UnboundColumnInfoWrapper(this.Column);
      }
    }

    public virtual XlCellFormatting Appearance
    {
      get
      {
        if (this.formatingCellCore == null)
        {
          XlCellFormatting xlCellFormatting = new XlCellFormatting();
          xlCellFormatting.Font = new XlFont();
          xlCellFormatting.Alignment = ColumnWrapper.GetReportAlignment(this.Column.ActualHorizontalContentAlignment);
          xlCellFormatting.Fill = (XlFill) null;
          this.formatingCellCore = xlCellFormatting;
        }
        return this.formatingCellCore;
      }
    }

    public virtual XlCellFormatting AppearanceHeader
    {
      get
      {
        if (this.formatingHeaderCore == null)
        {
          XlCellFormatting xlCellFormatting = new XlCellFormatting();
          xlCellFormatting.Font = new XlFont();
          xlCellFormatting.Alignment = ColumnWrapper.GetReportAlignment(this.Column.HorizontalHeaderContentAlignment);
          xlCellFormatting.Fill = (XlFill) null;
          this.formatingHeaderCore = xlCellFormatting;
        }
        return this.formatingHeaderCore;
      }
    }

    public virtual FormatSettings FormatSettings
    {
      get
      {
        return new FormatSettings() { ActualDataType = this.Column.FieldType, FormatType = FormatType.Custom, FormatString = this.GetFormatString() };
      }
    }

    public ColumnSortOrder SortOrder
    {
      get
      {
        return this.Column.SortOrder;
      }
    }

    public ColumnEditTypes ColEditType
    {
      get
      {
        return this.GetColumnEditType();
      }
    }

    public Type ColumnType
    {
      get
      {
        return this.Column.FieldType;
      }
    }

    public int LogicalPosition { get; private set; }

    public virtual int Width
    {
      get
      {
        if (double.IsInfinity(this.Column.ActualHeaderWidth) || double.IsNaN(this.Column.ActualHeaderWidth))
          return 0;
        return Convert.ToInt32(this.Column.ActualHeaderWidth);
      }
    }

    public virtual int VisibleIndex
    {
      get
      {
        return this.Column.VisibleIndex;
      }
    }

    public bool HasGroupIndex
    {
      get
      {
        if (this.GridColumn == null)
          return false;
        return this.GridColumn.GroupIndex >= 0;
      }
    }

    public virtual int GroupIndex
    {
      get
      {
        if (this.GridColumn == null)
          return -1;
        return this.GridColumn.GroupIndex;
      }
    }

    public virtual bool IsVisible
    {
      get
      {
        return this.Column.Visible;
      }
    }

    public IEnumerable<object> DataValidationItems
    {
      get
      {
        return this.GetColumnItemsSource();
      }
    }

    public virtual bool IsFixedLeft
    {
      get
      {
        return this.GetIsFixed();
      }
    }

    public virtual string Header
    {
      get
      {
        return ((IDataColumnInfo) this.Column).Caption;
      }
    }

    public ColumnWrapper(ColumnBase column, int logicalPosition)
    {
      this.Column = column;
      this.LogicalPosition = logicalPosition;
    }

    public virtual IEnumerable<IColumn> GetAllColumns()
    {
      return (IEnumerable<IColumn>) null;
    }

    public virtual int GetColumnGroupLevel()
    {
      return 0;
    }

    public virtual string GetGroupColumnHeader()
    {
      return (string) null;
    }

    private bool GetIsFixed()
    {
      if (this.Column.Fixed == FixedStyle.Left)
        return true;
      if (this.View == null || this.Column.ParentBand == null)
        return false;
      return this.View.DataControl.BandsLayoutCore.GetRootBand(this.Column.ParentBand).Fixed == FixedStyle.Left;
    }

    private IEnumerable<object> GetColumnItemsSource()
    {
      if (this.Column == null)
        return (IEnumerable<object>) null;
      LookUpEditSettingsBase editSettingsBase = this.Column.EditSettings as LookUpEditSettingsBase;
      if (editSettingsBase == null || !(editSettingsBase.ItemsSource is IEnumerable))
        return (IEnumerable<object>) null;
      return ((IEnumerable) editSettingsBase.ItemsSource).Cast<object>().Select<object, object>((Func<object, object>) (item => this.View.GetExportValueFromItem(item, this.Column)));
    }

    private ColumnEditTypes GetColumnEditType()
    {
      if (this.Column == null)
        return ColumnEditTypes.Text;
      if (this.Column.ActualEditSettings is CheckEditSettings)
        return ColumnEditTypes.CheckBox;
      if (this.Column.ActualEditSettings is ImageEditSettings)
        return ColumnEditTypes.Image;
      if (this.Column.ActualEditSettings is LookUpEditSettingsBase)
        return ColumnEditTypes.Lookup;
      if (this.Column.ActualEditSettings is SparklineEditSettings)
        return ColumnEditTypes.Sparkline;
      if (this.Column.ActualEditSettings is ProgressBarEditSettings)
        return ColumnEditTypes.ProgressBar;
      return GridColumnTypeHelper.IsNumericType(this.ColumnType) ? ColumnEditTypes.Number : ColumnEditTypes.Text;
    }

    private string GetFormatString()
    {
      if (this.Column.ActualEditSettings == null)
        return string.Empty;
      string formatStringByMask = this.GetFormatStringByMask(this.Column.ActualEditSettings as TextEditSettings);
      if (string.IsNullOrEmpty(formatStringByMask))
        return this.GetFormatStringByDisplayFormat(this.Column.ActualEditSettings.DisplayFormat);
      if (string.Equals(formatStringByMask, "P"))
        return "0.00\\%";
      return formatStringByMask;
    }

    private string GetFormatStringByMask(TextEditSettings settings)
    {
      if (settings != null)
      {
        bool flag = settings.MaskType == MaskType.DateTime || settings.MaskType == MaskType.DateTimeAdvancingCaret || settings.MaskType == MaskType.Numeric;
        if (settings.MaskUseAsDisplayFormat && flag)
          return settings.Mask;
      }
      return string.Empty;
    }

    private string GetFormatStringByDisplayFormat(string displayFormat)
    {
      string pattern = "{0:.*}";
      if (!Regex.IsMatch(displayFormat, pattern))
        return string.Empty;
      int startIndex = displayFormat.IndexOf(':') + 1;
      int num = displayFormat.IndexOf('}') - 1;
      return displayFormat.Substring(startIndex, num - startIndex + 1);
    }

    internal static XlCellAlignment GetReportAlignment(System.Windows.HorizontalAlignment actualAlignment)
    {
      XlCellAlignment xlCellAlignment = new XlCellAlignment();
      switch (actualAlignment)
      {
        case System.Windows.HorizontalAlignment.Left:
          xlCellAlignment.HorizontalAlignment = XlHorizontalAlignment.Left;
          break;
        case System.Windows.HorizontalAlignment.Center:
          xlCellAlignment.HorizontalAlignment = XlHorizontalAlignment.Center;
          break;
        case System.Windows.HorizontalAlignment.Right:
          xlCellAlignment.HorizontalAlignment = XlHorizontalAlignment.Right;
          break;
        case System.Windows.HorizontalAlignment.Stretch:
          xlCellAlignment.HorizontalAlignment = XlHorizontalAlignment.Fill;
          break;
      }
      xlCellAlignment.VerticalAlignment = XlVerticalAlignment.Center;
      return xlCellAlignment;
    }
  }
}
