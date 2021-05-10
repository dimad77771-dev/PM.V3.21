// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.OnlyThisSelectionAction
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;

namespace DevExpress.Xpf.Grid.Native
{
  public class OnlyThisSelectionAction : AddOneRowToSelectionAction
  {
    public OnlyThisSelectionAction(GridViewBase view)
      : base(view)
    {
      view.EditorSetInactiveAfterClick = false;
    }

    public override void Execute()
    {
      this.view.SelectionStrategy.DoMasterDetailSelectionAction((Action) (() =>
      {
        this.GridControl.ClearMasterDetailSelection();
        if (this.FocusedView.GetActualSelectionMode() == MultiSelectMode.MultipleRow)
          return;
        base.Execute();
      }));
    }
  }
}
