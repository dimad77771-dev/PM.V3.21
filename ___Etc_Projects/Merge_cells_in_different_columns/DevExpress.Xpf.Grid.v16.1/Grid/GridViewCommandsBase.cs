// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridViewCommandsBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides access to View commands.
  /// </para>
  ///             </summary>
  public abstract class GridViewCommandsBase : DataViewCommandsBase
  {
    /// <summary>
    ///                 <para>Toggles the specified group row's expanded state.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseChangeGroupExpanded")]
    public ICommand ChangeGroupExpanded { get; private set; }

    /// <summary>
    ///                 <para>Expands all group rows.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseExpandAllGroups")]
    public ICommand ExpandAllGroups { get; private set; }

    /// <summary>
    ///                 <para>Collapses all group rows.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseCollapseAllGroups")]
    public ICommand CollapseAllGroups { get; private set; }

    /// <summary>
    ///                 <para>Moves focus to the group row that owns the currently focused row.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseMoveParentGroupRow")]
    public ICommand MoveParentGroupRow { get; private set; }

    /// <summary>
    ///                 <para>Ungroups the grid.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseClearGrouping")]
    public ICommand ClearGrouping { get; private set; }

    /// <summary>
    ///                 <para>Invokes the Runtime Summary Editor.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridViewCommandsBaseShowGroupSummaryEditor")]
    public ICommand ShowGroupSummaryEditor { get; private set; }

    protected GridViewCommandsBase(GridViewBase view)
      : base((DataViewBase) view)
    {
      this.ChangeGroupExpanded = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ChangeGroupExpanded(o)));
      this.ExpandAllGroups = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ExpandAllGroups(o)), (Func<object, bool>) (o => view.CanExpandCollapseAll(o)));
      this.CollapseAllGroups = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.CollapseAllGroups(o)), (Func<object, bool>) (o => view.CanExpandCollapseAll(o)));
      this.MoveParentGroupRow = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.MoveParentGroupRow()), (Func<object, bool>) (o => view.CanMoveGroupParentRow()));
      this.ClearGrouping = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ClearGrouping()), (Func<object, bool>) (o => view.CanClearGrouping()));
      this.ShowGroupSummaryEditor = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ShowGroupSummaryEditor()), (Func<object, bool>) (o => view.CanShowGroupSummaryEditor()));
    }
  }
}
