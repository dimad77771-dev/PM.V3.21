// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Data.GridSelectionController
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;

namespace DevExpress.Xpf.Data
{
  public class GridSelectionController : ISelectionController
  {
    private readonly DataController controller;

    private RowStateController Selection
    {
      get
      {
        return this.controller.Selection as RowStateController;
      }
    }

    public bool IsSelectionLocked
    {
      get
      {
        return this.Selection.IsSelectionLocked;
      }
    }

    public int Count
    {
      get
      {
        return this.Selection.Count;
      }
    }

    public GridSelectionController(DataController controller)
    {
      this.controller = controller;
    }

    public void BeginSelection()
    {
      this.Selection.BeginSelection();
    }

    public void EndSelection()
    {
      this.Selection.EndSelection();
    }

    public int[] GetSelectedRows()
    {
      return this.Selection.GetSelectedRows();
    }

    public void SetActuallyChanged()
    {
      this.Selection.SetActuallyChanged();
    }

    public void SetSelected(int rowHandle, bool selected)
    {
      this.Selection.SetSelected(rowHandle, selected);
    }

    public void SetSelected(int rowHandle, bool selected, object selectionObject)
    {
      this.Selection.SetSelected(rowHandle, selected, selectionObject);
    }

    public void SelectAll()
    {
      this.Selection.SelectAllRows();
    }

    public void Clear()
    {
      this.Selection.ClearSelection();
    }

    public bool GetSelected(int controllerRow)
    {
      return this.Selection.GetSelected(controllerRow);
    }

    public object GetSelectedObject(int controllerRow)
    {
      return this.Selection.GetSelectedObject(controllerRow);
    }
  }
}
