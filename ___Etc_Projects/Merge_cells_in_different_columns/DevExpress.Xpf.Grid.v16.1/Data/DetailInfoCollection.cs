// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.DetailInfoCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevExpress.Xpf.Data
{
  public class DetailInfoCollection : RowStateCollection
  {
    public DetailInfoCollection(RowStateController selectionController)
      : base(selectionController)
    {
    }

    protected override void SetSelectionObject(object row, object selectionObject)
    {
      base.SetSelectionObject(row, selectionObject);
      RowDetailContainer rowDetailContainer = selectionObject as RowDetailContainer;
      if (rowDetailContainer == null)
        return;
      rowDetailContainer.MasterListIndex = (int) row;
    }

    public IEnumerable<int> GetRowListIndicesWithExpandedDetails()
    {
      foreach (int key in this.Rows.Keys)
      {
        RowDetailContainer container = (RowDetailContainer) this.Rows[(object) key];
        if (container.RootDetailInfo.IsExpanded)
          yield return key;
      }
    }

    public IEnumerable<RowDetailContainer> GetContainers()
    {
      return this.Rows.Keys.Cast<int>().Select<int, RowDetailContainer>((Func<int, RowDetailContainer>) (x => (RowDetailContainer) this.Rows[(object) x]));
    }

    protected override void OnItemDeleted(int listSourceRow)
    {
      RowDetailContainer rowDetailContainer = this.GetSelectedObject((object) listSourceRow) as RowDetailContainer;
      if (rowDetailContainer != null)
        rowDetailContainer.Detach();
      base.OnItemDeleted(listSourceRow);
    }
  }
}
