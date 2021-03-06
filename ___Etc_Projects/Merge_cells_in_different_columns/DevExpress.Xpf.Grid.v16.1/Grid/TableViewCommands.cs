// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TableViewCommands
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using System;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Contains the commands allowing some frequently used actions to be easily programmed via XAML markup.
  /// 
  /// </para>
  ///             </summary>
  public class TableViewCommands : GridViewCommandsBase, IConditionalFormattingCommands
  {
    private readonly TableView tableView;

    /// <summary>
    ///                 <para>Resizes the column to the minimum width required to display the column's contents completely.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewCommandsBestFitColumn")]
    public ICommand BestFitColumn { get; private set; }

    /// <summary>
    ///                 <para>Adjusts the width of columns so that columns fit their contents in an optimal way.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("TableViewCommandsBestFitColumns")]
    public ICommand BestFitColumns { get; private set; }

    /// <summary>
    ///                 <para>Toggles a master row's expanded state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    public new ICommand ChangeMasterRowExpanded
    {
      get
      {
        return base.ChangeMasterRowExpanded;
      }
    }

    /// <summary>
    ///                 <para>Expands a master row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    public new ICommand ExpandMasterRow
    {
      get
      {
        return base.ExpandMasterRow;
      }
    }

    /// <summary>
    ///                 <para>Collapses a master row.
    /// </para>
    ///             </summary>
    /// <value>A System.Windows.Input.RoutedCommand object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    public new ICommand CollapseMasterRow
    {
      get
      {
        return base.CollapseMasterRow;
      }
    }

    /// <summary>
    ///                 <para>Shows the New Item Row.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowNewItemRow { get; private set; }

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
    ///                 <para>Invokes the 'Duplicate Values' format condition dialog.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ShowUniqueDuplicateRuleFormatConditionDialog { get; private set; }

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
    ///                 <para>Adds a format condition.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand AddFormatCondition { get; private set; }

    /// <summary>
    ///                 <para>Toggles the row selection.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand ToggleRowsSelection { get; private set; }

    /// <summary>
    ///                 <para>Shows the edit form in a mode specified by the <see cref="P:DevExpress.Xpf.Grid.TableView.EditFormShowMode" /> property.
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
    ///                 <para>Adds a new record.
    /// </para>
    ///             </summary>
    /// <value>A command that implements the <see cref="T:System.Windows.Input.ICommand" />.
    /// </value>
    public ICommand AddNewRow { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the TableViewCommands class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.TableView" /> object that represents the owner view.
    /// 
    ///           </param>
    public TableViewCommands(TableView view)
      : base((GridViewBase) view)
    {
      TableViewCommands tableViewCommands = this;
      this.tableView = view;
      this.BestFitColumn = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.TableViewBehavior.BestFitColumn(o)), (Func<object, bool>) (o => view.CanBestFitColumn(o)));
      this.BestFitColumns = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.TableViewBehavior.BestFitColumns()), (Func<object, bool>) (o => view.TableViewBehavior.CanBestFitColumns()));
      this.ShowNewItemRow = (ICommand) new DelegateCommand<NewItemRowPosition?>(new Action<NewItemRowPosition?>(view.ShowNewItemRow));
      this.ShowLessThanFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.LessThan)));
      this.ShowGreaterThanFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.GreaterThan)));
      this.ShowEqualToFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.EqualTo)));
      this.ShowBetweenFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Between)));
      this.ShowTextThatContainsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.TextThatContains)));
      this.ShowADateOccurringFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.ADateOccurring)));
      this.ShowCustomConditionFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.CustomCondition)));
      this.ShowTop10ItemsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Top10Items)));
      this.ShowBottom10ItemsFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Bottom10Items)));
      this.ShowTop10PercentFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Top10Percent)));
      this.ShowBottom10PercentFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.Bottom10Percent)));
      this.ShowAboveAverageFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.AboveAverage)));
      this.ShowBelowAverageFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.BelowAverage)));
      this.ShowUniqueDuplicateRuleFormatConditionDialog = (ICommand) this.CreateDelegateCommand((Action<object>) (x => this.ShowFormatConditionDialog(x, FormatConditionDialogType.UniqueDuplicate)));
      this.ClearFormatConditionsFromAllColumns = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ClearFormatConditionsFromAllColumns()));
      this.ClearFormatConditionsFromColumn = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ClearFormatConditionsFromColumn((ColumnBase) x)));
      this.ShowConditionalFormattingManager = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.ShowConditionalFormattingManager((ColumnBase) x)));
      this.AddFormatCondition = (ICommand) this.CreateDelegateCommand((Action<object>) (x => view.AddFormatCondition((FormatConditionBase) x)));
      this.ToggleRowsSelection = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ToggleRowsSelection()));
      this.AddNewRow = (ICommand) this.CreateDelegateCommand((Action<object>) (x => ((TableView) view.MasterRootRowsContainer.FocusedView).AddNewRow()), (Func<object, bool>) (x => view.MasterRootRowsContainer.FocusedView.CanAddNewRow()));
      this.ShowEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.ShowEditForm()));
      this.HideEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.HideEditForm()));
      this.CloseEditForm = (ICommand) this.CreateDelegateCommand((Action<object>) (o => view.CloseEditForm()));
    }

    private void ShowFormatConditionDialog(object column, FormatConditionDialogType dialogKind)
    {
      this.tableView.ShowFormatConditionDialog((ColumnBase) column, dialogKind);
    }
  }
}
