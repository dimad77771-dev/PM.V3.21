// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.FormatRuleExportWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.ConditionalFormatting.Printing;
using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  public class FormatRuleExportWrapper : IFormatRuleBase
  {
    private readonly FormatConditionRuleBase ruleCore;

    public FormatConditionBase FormatCondition { get; private set; }

    public bool ApplyToRow
    {
      get
      {
        return this.FormatCondition.GetApplyToFieldName() == null;
      }
    }

    public IColumn Column { get; private set; }

    public IColumn ColumnApplyTo { get; private set; }

    public string ColumnName
    {
      get
      {
        return this.Column.Name;
      }
    }

    public bool Enabled
    {
      get
      {
        return true;
      }
    }

    public string Name
    {
      get
      {
        return this.GetHashCode().ToString();
      }
    }

    public IFormatConditionRuleBase Rule
    {
      get
      {
        return (IFormatConditionRuleBase) this.ruleCore;
      }
    }

    public bool StopIfTrue
    {
      get
      {
        return false;
      }
    }

    public object Tag { get; set; }

    public bool IsValid
    {
      get
      {
        return this.ruleCore.IsValid;
      }
    }

    public FormatRuleExportWrapper(FormatConditionBase formatCondition, ColumnWrapper column)
    {
      this.FormatCondition = formatCondition;
      this.ruleCore = this.FormatCondition.CreateExportWrapper();
      if (column != null)
        this.ruleCore.ColumnType = column.ColumnType;
      this.Column = (IColumn) column;
      this.ColumnApplyTo = (IColumn) null;
    }
  }
}
