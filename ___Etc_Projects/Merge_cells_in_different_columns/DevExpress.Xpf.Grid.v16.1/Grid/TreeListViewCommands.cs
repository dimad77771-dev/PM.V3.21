// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListViewCommands
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using System;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains the commands allowing some frequently used TreeListView actions to be easily programmed via XAML markup.
  /// </para>
  ///             </summary>
  public class TreeListViewCommands : DataViewCommandsBase, IConditionalFormattingCommands
  {
    private readonly TreeListView treeListView;

    /// <summary>
    ///                 <para>Specifies the expanded state of a node (expanded or collapsed).
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsChangeNodeExpanded")]
    public ICommand ChangeNodeExpanded { get; private set; }

    /// <summary>
    ///                 <para>Toggles a node's check state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsChangeNodeCheckState")]
    public ICommand ChangeNodeCheckState { get; private set; }

    /// <summary>
    ///                 <para>Expands all nodes.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsExpandAllNodes")]
    public ICommand ExpandAllNodes { get; private set; }

    /// <summary>
    ///                 <para>Collapses all nodes.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsCollapseAllNodes")]
    public ICommand CollapseAllNodes { get; private set; }

    /// <summary>
    ///                 <para>Expands the parent nodes down to the specified nesting level.
    /// </para>
    ///             </summary>
    /// <value>A command implementing the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsExpandToLevel")]
    public ICommand ExpandToLevel { get; private set; }

    /// <summary>
    ///                 <para>Resizes the column to the minimum width required to display the column's contents completely.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsBestFitColumn")]
    public ICommand BestFitColumn { get; private set; }

    /// <summary>
    ///                 <para>Adjusts the width of columns so that columns fit their contents in an optimal way.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsBestFitColumns")]
    public ICommand BestFitColumns { get; private set; }

    /// <summary>
    ///                 <para>Checks all nodes.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsCheckAllNodes")]
    public ICommand CheckAllNodes { get; private set; }

    /// <summary>
    ///                 <para>Unchecks all nodes.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsUncheckAllNodes")]
    public ICommand UncheckAllNodes { get; private set; }

    /// <summary>
    ///                 <para>Adds a format condition.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TreeListViewCommandsAddFormatCondition")]
    public ICommand AddFormatCondition { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Less Than' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowLessThanFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Greater Than' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowGreaterThanFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Equal To' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowEqualToFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Between' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowBetweenFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Text That Contains' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowTextThatContainsFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'A Date Occuring' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowADateOccurringFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Duplicate Values' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowUniqueDuplicateRuleFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Custom Condition' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowCustomConditionFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Top 10 Items' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowTop10ItemsFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Bottom 10 Items' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowBottom10ItemsFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Top 10 Percent' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowTop10PercentFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Bottom 10 Percent' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowBottom10PercentFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Above Average' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowAboveAverageFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Invokes the 'Below Average' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowBelowAverageFormatConditionDialog { get; private set; }

    /// <summary>
    ///                 <para>Clears format conditions from all columns.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ClearFormatConditionsFromAllColumns { get; private set; }

    /// <summary>
    ///                 <para>Clears format conditions from the specified column.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ClearFormatConditionsFromColumn { get; private set; }

    /// <summary>
    ///                 <para>Shows the conditional formatting rules manager.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowConditionalFormattingManager { get; private set; }

    /// <summary>
    ///                 <para>Shows the edit form in a mode specified by the <see cref="P:DevExpress.Xpf.Grid.TreeListView.EditFormShowMode" /> property.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowEditForm { get; private set; }

    /// <summary>
    ///                 <para>Cancels all changes and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand HideEditForm { get; private set; }

    /// <summary>
    ///                 <para>Posts all changes to the data source and closes the Inline Edit Form.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand CloseEditForm { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListViewCommands class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> object that represents the owner view.
    /// 
    ///           </param>
    public TreeListViewCommands(TreeListView view)
      : base((DataViewBase) view)
    {
      TreeListViewCommands listViewCommands = this;
      this.treeListView = view;
      this.ChangeNodeExpanded = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.OnChangeNodeExpanded(o)), (Func<object, bool>) (o => view.CanChangeNodeExpaned(o)));
      this.ChangeNodeCheckState = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.OnChangeNodeCheckState(o)), (Func<object, bool>) (o => view.CanChangeNodeCheckState(o)));
      this.ExpandAllNodes = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ExpandAllNodes()), (Func<object, bool>) (o => true));
      this.CollapseAllNodes = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.CollapseAllNodes()), (Func<object, bool>) (o => true));
      this.ExpandToLevel = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ExpandToLevel(Convert.ToInt32(o))));
      this.BestFitColumn = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.TreeListViewBehavior.BestFitColumn(o)), (Func<object, bool>) (o => view.TreeListViewBehavior.CanBestFitColumn(o)));
      this.BestFitColumns = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.TreeListViewBehavior.BestFitColumns()), (Func<object, bool>) (o => view.TreeListViewBehavior.CanBestFitColumns()));
      this.CheckAllNodes = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.CheckAllNodes()), (Func<object, bool>) (o => true));
      this.UncheckAllNodes = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.UncheckAllNodes()), (Func<object, bool>) (o => true));
      this.AddFormatCondition = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.AddFormatCondition((FormatConditionBase) x)));
      this.ShowLessThanFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.LessThan)));
      this.ShowGreaterThanFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.GreaterThan)));
      this.ShowEqualToFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.EqualTo)));
      this.ShowBetweenFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Between)));
      this.ShowTextThatContainsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.TextThatContains)));
      this.ShowADateOccurringFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.ADateOccurring)));
      this.ShowUniqueDuplicateRuleFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.UniqueDuplicate)));
      this.ShowCustomConditionFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.CustomCondition)));
      this.ShowTop10ItemsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Top10Items)));
      this.ShowBottom10ItemsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Bottom10Items)));
      this.ShowTop10PercentFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Top10Percent)));
      this.ShowBottom10PercentFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Bottom10Percent)));
      this.ShowAboveAverageFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.AboveAverage)));
      this.ShowBelowAverageFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.BelowAverage)));
      this.ClearFormatConditionsFromAllColumns = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ClearFormatConditionsFromAllColumns()));
      this.ClearFormatConditionsFromColumn = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ClearFormatConditionsFromColumn((ColumnBase) x)));
      this.ShowConditionalFormattingManager = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ShowConditionalFormattingManager((ColumnBase) x)));
      this.ShowEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ShowEditForm()));
      this.HideEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.HideEditForm()));
      this.CloseEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.CloseEditForm()));
    }

    private void ShowFormatConditionDialog(object column, FormatConditionDialogType dialogKind)
    {
      this.treeListView.ShowFormatConditionDialog((ColumnBase) column, dialogKind);
    }
  }
}
