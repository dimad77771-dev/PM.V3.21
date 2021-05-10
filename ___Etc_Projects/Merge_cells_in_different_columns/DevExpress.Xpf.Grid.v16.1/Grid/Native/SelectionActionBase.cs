// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.SelectionActionBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;

namespace DevExpress.Xpf.Grid.Native
{
  public abstract class SelectionActionBase : IAction
  {
    protected readonly int oldRowHandle;
    protected readonly GridViewBase view;

    protected GridControl GridControl
    {
      get
      {
        return (GridControl) this.view.DataControl;
      }
    }

    protected GridControl FocusedGrid
    {
      get
      {
        return (GridControl) this.FocusedView.DataControl;
      }
    }

    protected GridViewBase FocusedView
    {
      get
      {
        return (GridViewBase) this.view.FocusedView;
      }
    }

    public bool CanFocusChangeDeleteAction { get; set; }

    protected SelectionActionBase(GridViewBase view)
    {
      this.view = view;
      this.oldRowHandle = view.GlobalSelectionAnchor.RowHandle;
      this.CanFocusChangeDeleteAction = true;
    }

    public abstract void Execute();
  }
}
