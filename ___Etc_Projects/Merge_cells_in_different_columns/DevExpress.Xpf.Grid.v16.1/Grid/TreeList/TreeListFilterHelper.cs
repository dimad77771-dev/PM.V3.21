// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListFilterHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using System;
using System.Collections;
using System.Collections.Generic;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListFilterHelper
  {
    protected TreeListDataProvider DataProvider { get; private set; }

    protected TreeListView View
    {
      get
      {
        return this.DataProvider.View;
      }
    }

    protected TreeListDataHelperBase DataHelper
    {
      get
      {
        return this.DataProvider.DataHelper;
      }
    }

    public TreeListFilterHelper(TreeListDataProvider provider)
    {
      this.DataProvider = provider;
    }

    public virtual object[] GetUniqueColumnValuesCore(DataColumnInfo column, bool includeFilteredOut, bool roundDataTime, bool useDisplayText)
    {
      if (!this.DataProvider.IsReady)
        return (object[]) null;
      object[] columnValues = this.GetColumnValues(column, includeFilteredOut, roundDataTime, useDisplayText);
      if (columnValues != null)
      {
        if (columnValues.Length >= 2)
        {
          try
          {
            Array.Sort((Array) columnValues, (IComparer) this.DataProvider.ValueComparer);
          }
          catch
          {
          }
          int length1 = 0;
          int length2 = columnValues.Length;
          object[] objArray1 = new object[length2];
          object y = (object) null;
          for (int index = 0; index < length2; ++index)
          {
            object x = columnValues[index];
            if (index == 0 || this.DataProvider.ValueComparer.Compare(x, y) != 0)
              objArray1[length1++] = x;
            y = x;
          }
          object[] objArray2 = new object[length1];
          Array.Copy((Array) objArray1, 0, (Array) objArray2, 0, length1);
          return objArray2;
        }
      }
      return columnValues;
    }

    public virtual CriteriaOperator CalcColumnFilterCriteriaByValue(DataColumnInfo columnInfo, object columnValue, bool roundDateTime, bool useDisplayText)
    {
      Type type = useDisplayText ? typeof (string) : columnInfo.Type;
      OperandProperty operandProperty = new OperandProperty(columnInfo.Name);
      if (columnValue == null || columnValue is DBNull)
        return (CriteriaOperator) operandProperty.IsNull();
      Type underlyingType = Nullable.GetUnderlyingType(type);
      if (underlyingType != (Type) null)
        type = underlyingType;
      if (roundDateTime && type == typeof (DateTime))
      {
        DateTime dateTime1 = TreeListFilterHelper.ConvertToDate(columnValue);
        dateTime1 = new DateTime(dateTime1.Year, dateTime1.Month, dateTime1.Day);
        try
        {
          DateTime dateTime2 = dateTime1.AddDays(1.0);
          return (CriteriaOperator) ((CriteriaOperator) operandProperty >= (CriteriaOperator) dateTime1) & (CriteriaOperator) ((CriteriaOperator) operandProperty < (CriteriaOperator) dateTime2);
        }
        catch
        {
          return (CriteriaOperator) ((CriteriaOperator) operandProperty >= (CriteriaOperator) dateTime1);
        }
      }
      else
      {
        columnValue = TreeListFilterHelper.CorrectFilterValueType(type, columnValue);
        return (CriteriaOperator) ((CriteriaOperator) operandProperty == (CriteriaOperator) new OperandValue(columnValue));
      }
    }

    protected IList<TreeListNode> GetNodes(bool includeFilteredOut, DataColumnInfo columnInfo)
    {
      List<TreeListNode> treeListNodeList = new List<TreeListNode>();
      foreach (TreeListNode node in (IEnumerable<TreeListNode>) new TreeListNodeIterator(this.DataProvider.Nodes, true))
      {
        if ((node.IsVisible || includeFilteredOut) && this.UserCustomNodeAccounted(node, columnInfo))
          treeListNodeList.Add(node);
      }
      return (IList<TreeListNode>) treeListNodeList;
    }

    private bool UserCustomNodeAccounted(TreeListNode node, DataColumnInfo columnInfo)
    {
      return this.View.RaiseCustomFiterPopupList(node, columnInfo);
    }

    protected virtual object[] GetColumnValues(DataColumnInfo columnInfo, bool includeFilteredOut, bool roundDateTime, bool displayText)
    {
      if (!this.DataProvider.IsReady)
        return (object[]) null;
      int length = 0;
      IList<TreeListNode> nodes = this.GetNodes(includeFilteredOut, columnInfo);
      bool flag = !displayText && (columnInfo.Type.Equals(typeof (DateTime)) || columnInfo.Type.Equals(typeof (DateTime?)));
      int count = nodes.Count;
      object[] objArray1 = new object[count];
      for (int index = 0; index < nodes.Count; ++index)
      {
        TreeListNode node = nodes[index];
        object obj = this.DataHelper.GetValue(node, columnInfo.Name);
        if (displayText)
          obj = (object) this.View.GetNodeDisplayText(node, columnInfo.Name, obj);
        if (obj != null && !(obj is DBNull) && obj is IComparable)
        {
          if (flag & roundDateTime && obj is DateTime)
          {
            DateTime dateTime = (DateTime) obj;
            obj = (object) new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
          }
          objArray1[length++] = obj;
        }
      }
      if (length == count)
        return objArray1;
      if (length == 0)
        return (object[]) null;
      object[] objArray2 = new object[length];
      Array.Copy((Array) objArray1, 0, (Array) objArray2, 0, length);
      return objArray2;
    }

    public static DateTime ConvertToDate(object val)
    {
      DateTime dateTime = DateTime.MinValue;
      if (val == null)
        return dateTime;
      try
      {
        dateTime = val is DateTime ? (DateTime) val : DateTime.Parse(val.ToString());
      }
      catch
      {
      }
      return dateTime;
    }

    public static object CorrectFilterValueType(Type columnFilteredType, object filteredValue)
    {
      if (filteredValue == null || columnFilteredType == (Type) null)
        return filteredValue;
      Type underlyingType = Nullable.GetUnderlyingType(columnFilteredType);
      if (underlyingType != (Type) null)
        columnFilteredType = underlyingType;
      Type type = filteredValue.GetType();
      if (columnFilteredType.IsAssignableFrom(type))
        return filteredValue;
      try
      {
        return Convert.ChangeType(filteredValue, columnFilteredType, (IFormatProvider) null);
      }
      catch
      {
      }
      return filteredValue;
    }
  }
}
