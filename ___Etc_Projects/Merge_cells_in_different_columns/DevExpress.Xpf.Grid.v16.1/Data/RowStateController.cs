// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.RowStateController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Selection;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Data
{
  public class RowStateController : SelectionController
  {
    private static readonly int[] emptyIndices = new int[0];
    private readonly RowStateCollection collapsedRows;
    private readonly DetailInfoCollection detailInfoCollection;

    public RowStateCollection CollapsedRows
    {
      get
      {
        return this.collapsedRows;
      }
    }

    public DetailInfoCollection DetailInfoCollection
    {
      get
      {
        return this.detailInfoCollection;
      }
    }

    public RowStateController(DataController controller)
      : base(controller)
    {
      this.collapsedRows = new RowStateCollection(this);
      this.detailInfoCollection = new DetailInfoCollection(this);
    }

    private static T GetRowInfo<T>(RowStateCollection collection, int controllerRow, Func<T> createInfoDelegate, bool createNewIfNotExist = true) where T : class
    {
      T obj = collection.GetRowSelectedObject(controllerRow) as T;
      if ((object) obj == null && createNewIfNotExist)
      {
        obj = createInfoDelegate();
        collection.SetRowSelected(controllerRow, true, (object) obj);
      }
      return obj;
    }

    public DependencyObject GetRowState(int controllerRow, bool createNewIfNotExist)
    {
      return RowStateController.GetRowInfo<DependencyObject>(this.collapsedRows, controllerRow, (Func<DependencyObject>) (() => (DependencyObject) new RowStateObject()), createNewIfNotExist);
    }

    public RowDetailContainer GetRowDetailInfo(int controllerRow, Func<RowDetailContainer> createContainerDelegate, bool createNewIfNotExist = true)
    {
      return RowStateController.GetRowInfo<RowDetailContainer>((RowStateCollection) this.detailInfoCollection, controllerRow, createContainerDelegate, createNewIfNotExist);
    }

    public IEnumerable<int> GetRowListIndicesWithExpandedDetails()
    {
      return this.detailInfoCollection.GetRowListIndicesWithExpandedDetails();
    }

    protected override List<SelectedRowsCollection> CreateSelectionCollections()
    {
      List<SelectedRowsCollection> selectionCollections = base.CreateSelectionCollections();
      selectionCollections.Add((SelectedRowsCollection) this.collapsedRows);
      selectionCollections.Add((SelectedRowsCollection) this.detailInfoCollection);
      return selectionCollections;
    }

    internal void ClearSelection()
    {
      base.Clear();
    }

    internal void SelectAllRows()
    {
      this.BeginSelection();
      try
      {
        this.ClearSelection();
        for (int visibleIndex = 0; visibleIndex < this.Controller.VisibleCount; ++visibleIndex)
          this.SetSelected(this.Controller.GetControllerRowHandle(visibleIndex), true);
      }
      finally
      {
        this.EndSelection();
      }
    }

    public override void Clear()
    {
      base.Clear();
      this.detailInfoCollection.Clear();
      this.collapsedRows.Clear();
    }

    internal void ClearDetailInfo()
    {
      foreach (RowDetailContainer container in this.detailInfoCollection.GetContainers())
        container.Clear();
      this.detailInfoCollection.Clear();
    }
  }
}
