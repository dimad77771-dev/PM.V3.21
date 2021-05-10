// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.XtraExport.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListNodeWrapper : IClipboardGroupRow<TreeListNodeWrapper>, IGroupRow<TreeListNodeWrapper>, IRowBase
  {
    protected TreeListNodeObject NodeObject { get; private set; }

    public TreeListNode Node
    {
      get
      {
        return this.NodeObject.Node;
      }
    }

    public bool IsDataAreaRow
    {
      get
      {
        return true;
      }
    }

    public int DataSourceRowIndex
    {
      get
      {
        return this.Node.Id;
      }
    }

    public FormatSettings FormatSettings
    {
      get
      {
        return (FormatSettings) null;
      }
    }

    public virtual bool IsGroupRow
    {
      get
      {
        return this.NodeObject.Nodes.Count > 0;
      }
    }

    public int LogicalPosition
    {
      get
      {
        return this.Node.RowHandle;
      }
    }

    public bool IsCollapsed
    {
      get
      {
        return !this.IsExpanded;
      }
    }

    protected bool IsExpanded
    {
      get
      {
        for (TreeListNode parentNode = this.Node.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
        {
          if (!parentNode.IsExpanded)
            return false;
        }
        return this.Node.IsExpanded;
      }
    }

    public TreeListNodeWrapper(TreeListNodeObject nodeObject)
    {
      this.NodeObject = nodeObject;
    }

    public int GetRowLevel()
    {
      return this.NodeObject.Level;
    }

    public IEnumerable<TreeListNodeWrapper> GetAllRows()
    {
      return this.NodeObject.Nodes.Select<TreeListNodeObject, TreeListNodeWrapper>((Func<TreeListNodeObject, TreeListNodeWrapper>) (node => new TreeListNodeWrapper(node)));
    }

    public string GetGroupRowHeader()
    {
      return string.Empty;
    }

    public IEnumerable<TreeListNodeWrapper> GetSelectedRows()
    {
      return this.NodeObject.Nodes.Where<TreeListNodeObject>((Func<TreeListNodeObject, bool>) (node => node.Node.DataProvider.TreeListSelection.GetSelected(node.Node))).Select<TreeListNodeObject, TreeListNodeWrapper>((Func<TreeListNodeObject, TreeListNodeWrapper>) (node => new TreeListNodeWrapper(node)));
    }

    public bool IsTreeListGroupRow()
    {
      return true;
    }

    public string GetGroupedColumnFieldName()
    {
      return string.Empty;
    }
  }
}
