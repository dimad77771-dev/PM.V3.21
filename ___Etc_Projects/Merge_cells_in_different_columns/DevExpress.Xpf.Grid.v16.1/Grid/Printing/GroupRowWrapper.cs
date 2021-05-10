// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.GroupRowWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.Printing
{
  public class GroupRowWrapper : RowBaseWrapper, IClipboardGroupRow<RowBaseWrapper>, IGroupRow<RowBaseWrapper>, IRowBase
  {
    private string groupValue;
    private bool isExpanded;
    private IEnumerable<RowBaseWrapper> childRows;
    private TableView view;

    public bool IsCollapsed
    {
      get
      {
        return !this.isExpanded;
      }
    }

    public override bool IsGroupRow
    {
      get
      {
        return true;
      }
    }

    public GroupRowWrapper(int rowHandle, int rowLevel, int dataSourceRowIndex, string groupValue, bool isExpanded, IEnumerable<RowBaseWrapper> childRows, TableView view)
      : base(rowHandle, rowLevel, dataSourceRowIndex)
    {
      this.groupValue = groupValue;
      this.isExpanded = isExpanded;
      this.childRows = childRows;
      this.view = view;
    }

    public string GetGroupRowHeader()
    {
      return this.groupValue;
    }

    public IEnumerable<RowBaseWrapper> GetAllRows()
    {
      return this.childRows;
    }

    public IEnumerable<RowBaseWrapper> GetSelectedRows()
    {
      return (IEnumerable<RowBaseWrapper>) new List<RowBaseWrapper>();
    }

    public bool IsTreeListGroupRow()
    {
      return false;
    }

    public string GetGroupedColumnFieldName()
    {
      int rowLevel = this.GetRowLevel();
      return rowLevel < this.view.GroupCount ? this.view.SortInfo[rowLevel].FieldName : string.Empty;
    }
  }
}
