// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridCommands
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.ComponentModel;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides access to grid commands.
  /// </para>
  ///             </summary>
  public static class GridCommands
  {
    private static readonly RoutedCommand changeGroupExpanded = new RoutedCommand("ChangeGroupExpanded", typeof (GridCommands));
    private static readonly RoutedCommand expandAllGroups = new RoutedCommand("ExpandAllGroups", typeof (GridCommands));
    private static readonly RoutedCommand collapseAllGroups = new RoutedCommand("CollapseAllGroups", typeof (GridCommands));
    private static readonly RoutedCommand moveParentGroupRow = new RoutedCommand("MoveParentGroupRow", typeof (GridCommands));
    private static readonly RoutedCommand clearGrouping = new RoutedCommand("ClearGrouping", typeof (GridCommands));
    private static readonly RoutedCommand changeCardExpanded = new RoutedCommand("ChangeCardExpanded", typeof (GridCommands));
    private static readonly RoutedCommand bestFitColumn = new RoutedCommand("BestFitColumn", typeof (GridCommands));
    private static readonly RoutedCommand bestFitColumns = new RoutedCommand("BestFitColumns", typeof (GridCommands));
    private static readonly RoutedCommand addNewRow = new RoutedCommand("AddNewRow", typeof (GridCommands));

    /// <summary>
    ///                 <para>Toggles the specified group row's expanded state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsChangeGroupExpanded")]
    public static RoutedCommand ChangeGroupExpanded
    {
      get
      {
        return GridCommands.changeGroupExpanded;
      }
    }

    /// <summary>
    ///                 <para>Expands all group rows.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsExpandAllGroups")]
    public static RoutedCommand ExpandAllGroups
    {
      get
      {
        return GridCommands.expandAllGroups;
      }
    }

    /// <summary>
    ///                 <para>Collapses all group rows.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsCollapseAllGroups")]
    public static RoutedCommand CollapseAllGroups
    {
      get
      {
        return GridCommands.collapseAllGroups;
      }
    }

    /// <summary>
    ///                 <para>Toggles the specified card's expanded state.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsChangeCardExpanded")]
    public static RoutedCommand ChangeCardExpanded
    {
      get
      {
        return GridCommands.changeCardExpanded;
      }
    }

    /// <summary>
    ///                 <para>Ungroups the grid.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsClearGrouping")]
    public static RoutedCommand ClearGrouping
    {
      get
      {
        return GridCommands.clearGrouping;
      }
    }

    /// <summary>
    ///                 <para>Resizes the column to the minimum width required to display the column's contents completely.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsBestFitColumn")]
    public static RoutedCommand BestFitColumn
    {
      get
      {
        return GridCommands.bestFitColumn;
      }
    }

    /// <summary>
    ///                 <para>Adjusts the width of columns so that columns fit their contents in an optimal way.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsBestFitColumns")]
    public static RoutedCommand BestFitColumns
    {
      get
      {
        return GridCommands.bestFitColumns;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the group row that owns the currently focused row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveParentGroupRow")]
    public static RoutedCommand MoveParentGroupRow
    {
      get
      {
        return GridCommands.moveParentGroupRow;
      }
    }

    /// <summary>
    ///                 <para>Adds a new row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsAddNewRow")]
    public static RoutedCommand AddNewRow
    {
      get
      {
        return GridCommands.addNewRow;
      }
    }

    /// <summary>
    ///                 <para>Toggles a column's sort order.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsChangeColumnSortOrder")]
    public static RoutedCommand ChangeColumnSortOrder
    {
      get
      {
        return DataControlCommands.ChangeColumnSortOrder;
      }
    }

    /// <summary>
    ///                 <para>Removes the filter condition applied to a column.
    /// </para>
    ///             </summary>
    /// <value>The command which removes the filter condition applied to a column.</value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsClearColumnFilter")]
    public static RoutedCommand ClearColumnFilter
    {
      get
      {
        return DataControlCommands.ClearColumnFilter;
      }
    }

    /// <summary>
    ///                 <para>Removes the filter condition applied to a column.
    /// </para>
    ///             </summary>
    /// <value>The command which removes the filter condition applied to a column.</value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Instead use the ClearColumnFilter property.")]
    public static RoutedCommand ClearFilterColumn
    {
      get
      {
        return DataControlCommands.ClearColumnFilter;
      }
    }

    /// <summary>
    ///                 <para>Invokes the Filter Editor.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsShowFilterEditor")]
    public static RoutedCommand ShowFilterEditor
    {
      get
      {
        return DataControlCommands.ShowFilterEditor;
      }
    }

    /// <summary>
    ///                 <para>Invokes the Column Band Chooser.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsShowColumnChooser")]
    public static RoutedCommand ShowColumnChooser
    {
      get
      {
        return DataControlCommands.ShowColumnChooser;
      }
    }

    /// <summary>
    ///                 <para>Hides the Column Band Chooser.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsHideColumnChooser")]
    public static RoutedCommand HideColumnChooser
    {
      get
      {
        return DataControlCommands.HideColumnChooser;
      }
    }

    /// <summary>
    ///                 <para>Focuses the preceding cell before the focused cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMovePrevCell")]
    public static RoutedCommand MovePrevCell
    {
      get
      {
        return DataControlCommands.MovePrevCell;
      }
    }

    /// <summary>
    ///                 <para>Focuses the next cell after the focused cell.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveNextCell")]
    public static RoutedCommand MoveNextCell
    {
      get
      {
        return DataControlCommands.MoveNextCell;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the row or card preceding the one currently focused.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMovePrevRow")]
    public static RoutedCommand MovePrevRow
    {
      get
      {
        return DataControlCommands.MovePrevRow;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the row or card following the one currently focused.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveNextRow")]
    public static RoutedCommand MoveNextRow
    {
      get
      {
        return DataControlCommands.MoveNextRow;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the first visible row or card within a View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveFirstRow")]
    public static RoutedCommand MoveFirstRow
    {
      get
      {
        return DataControlCommands.MoveFirstRow;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the last visible row or card within a View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveLastRow")]
    public static RoutedCommand MoveLastRow
    {
      get
      {
        return DataControlCommands.MoveLastRow;
      }
    }

    /// <summary>
    ///                 <para>Moves focus backward by the number of rows or cards displayed onscreen within a View.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMovePrevPage")]
    public static RoutedCommand MovePrevPage
    {
      get
      {
        return DataControlCommands.MovePrevPage;
      }
    }

    /// <summary>
    ///                 <para>Moves focus forward by the number of rows or cards displayed onscreen within a View.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveNextPage")]
    public static RoutedCommand MoveNextPage
    {
      get
      {
        return DataControlCommands.MoveNextPage;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the first cell displayed within the first visible row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveFirstCell")]
    public static RoutedCommand MoveFirstCell
    {
      get
      {
        return DataControlCommands.MoveFirstCell;
      }
    }

    /// <summary>
    ///                 <para>Moves focus to the last cell displayed within the last visible row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsMoveLastCell")]
    public static RoutedCommand MoveLastCell
    {
      get
      {
        return DataControlCommands.MoveLastCell;
      }
    }

    /// <summary>
    ///                 <para>Clears the grid's filter expression.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsClearFilter")]
    public static RoutedCommand ClearFilter
    {
      get
      {
        return DataControlCommands.ClearFilter;
      }
    }

    /// <summary>
    ///                 <para>Deletes the focused data row or card.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsDeleteFocusedRow")]
    public static RoutedCommand DeleteFocusedRow
    {
      get
      {
        return DataControlCommands.DeleteFocusedRow;
      }
    }

    /// <summary>
    ///                 <para>Activates the focused cell's inplace editor.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsEditFocusedRow")]
    public static RoutedCommand EditFocusedRow
    {
      get
      {
        return DataControlCommands.EditFocusedRow;
      }
    }

    /// <summary>
    ///                 <para>Hides the active editor, discarding all the changes made within the focused row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsCancelEditFocusedRow")]
    public static RoutedCommand CancelEditFocusedRow
    {
      get
      {
        return DataControlCommands.CancelEditFocusedRow;
      }
    }

    /// <summary>
    ///                 <para>Hides the active editor and posts all the changes made within the focused row to a data source if the row values are valid.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsEndEditFocusedRow")]
    public static RoutedCommand EndEditFocusedRow
    {
      get
      {
        return DataControlCommands.EndEditFocusedRow;
      }
    }

    /// <summary>
    ///                 <para>Invokes the Expression Editor.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Input.RoutedCommand" /> object that defines a command implementing the <see cref="T:System.Windows.Input.ICommand" />, and is routed through the element tree.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridCommandsShowUnboundExpressionEditor")]
    public static RoutedCommand ShowUnboundExpressionEditor
    {
      get
      {
        return DataControlCommands.ShowUnboundExpressionEditor;
      }
    }
  }
}
