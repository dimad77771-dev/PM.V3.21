// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.AddRowsToSelectionAction
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.Native
{
  public class AddRowsToSelectionAction : SelectionActionBase
  {
    private readonly int startCommonVisibleIndex;

    public AddRowsToSelectionAction(GridViewBase view)
      : base(view)
    {
      this.startCommonVisibleIndex = view.GlobalSelectionAnchor.View.DataControl.GetCommonVisibleIndex(view.GlobalSelectionAnchor.RowHandle);
    }

    public override void Execute()
    {
      this.FocusedView.SelectionStrategy.SelectOnlyThisMasterDetailRange(this.startCommonVisibleIndex, this.FocusedView.DataControl.GetCommonVisibleIndex(this.FocusedView.FocusedRowHandle));
    }
  }
}
