// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.MouseMoveSelectionCardNone
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.Native
{
  public class MouseMoveSelectionCardNone : MouseMoveSelectionCardBase
  {
    public static readonly MouseMoveSelectionCardNone Instance = new MouseMoveSelectionCardNone();

    public override bool CanScrollHorizontally
    {
      get
      {
        return false;
      }
    }

    public override bool CanScrollVertically
    {
      get
      {
        return false;
      }
    }

    public override bool AllowNavigation
    {
      get
      {
        return true;
      }
    }

    public override void OnMouseDown(DataViewBase cardView, IDataViewHitInfo hitInfo)
    {
    }

    public override void UpdateSelection(DataViewBase cardView)
    {
    }

    public override void OnMouseUp(DataViewBase cardView)
    {
    }

    public override void CaptureMouse(DataViewBase cardView)
    {
    }
  }
}
