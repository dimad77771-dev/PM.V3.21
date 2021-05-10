// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.MouseMoveSelectionCardBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid.Native
{
  public abstract class MouseMoveSelectionCardBase
  {
    public abstract bool CanScrollHorizontally { get; }

    public abstract bool CanScrollVertically { get; }

    public abstract bool AllowNavigation { get; }

    public abstract void OnMouseDown(DataViewBase cardView, IDataViewHitInfo hitInfo);

    public abstract void UpdateSelection(DataViewBase cardView);

    public abstract void OnMouseUp(DataViewBase cardView);

    public abstract void CaptureMouse(DataViewBase cardView);

    public virtual void ReleaseMouseCapture(DataViewBase cardView)
    {
      DataViewBase rootView = cardView.RootView;
      if (MouseHelper.Captured != rootView)
        return;
      MouseHelper.ReleaseCapture((UIElement) rootView);
    }
  }
}
