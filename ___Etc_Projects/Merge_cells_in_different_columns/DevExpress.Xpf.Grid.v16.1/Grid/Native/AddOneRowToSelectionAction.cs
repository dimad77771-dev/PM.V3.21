﻿// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.AddOneRowToSelectionAction
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.Native
{
  public class AddOneRowToSelectionAction : SelectionActionBase
  {
    public AddOneRowToSelectionAction(GridViewBase view)
      : base(view)
    {
      view.EditorSetInactiveAfterClick = true;
    }

    public override void Execute()
    {
      this.FocusedGrid.SelectItem(this.FocusedView.FocusedRowHandle);
      this.FocusedView.SetSelectionAnchor();
    }
  }
}
