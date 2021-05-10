// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Core.ConditionalFormattingManager.GridManagerFactory
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Utils;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Grid.Native;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
  public class GridManagerFactory : IGridManagerFactory
  {
    private const string AppliesToFieldname = "AppliesTo";
    private const string ApplyToRowFieldname = "ApplyToRow";
    private const string RowNameFieldName = "RowName";
    private const string ColumnNameFieldName = "ColumnName";
    private const string IsEnabledFieldname = "IsEnabled";

    UIElement IGridManagerFactory.CreateGrid()
    {
      GridControl grid = new GridControl();
      this.Setup(grid);
      return (UIElement) grid;
    }

    ContentControl IGridManagerFactory.CreatePreviewControl()
    {
      return (ContentControl) new FormatPreviewControl();
    }

    private void Setup(GridControl grid)
    {
      TableView view = new TableView();
      this.SetupView(view);
      grid.View = (DataViewBase) view;
      this.SetupColumns(grid);
      this.SetupDefaultStyles(grid);
      grid.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
      view.CellValueChanging += new CellValueChangedEventHandler(this.OnCellValueChanging);
      view.PreviewMouseDoubleClick += new MouseButtonEventHandler(this.OnRowDoubleClick);
      DesignerProperties.SetIsInDesignMode((DependencyObject) grid, false);
    }

    private void SetupColumns(GridControl grid)
    {
      GridColumn gridColumn1 = new GridColumn();
      gridColumn1.FieldName = "Rule";
      gridColumn1.Header = (object) this.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Rule);
      gridColumn1.AllowFocus = false;
      grid.Columns.Add(gridColumn1);
      GridColumn gridColumn2 = new GridColumn();
      gridColumn2.FieldName = "PreviewFormat";
      gridColumn2.Header = (object) this.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Format);
      gridColumn2.AllowFocus = false;
      gridColumn2.CellTemplate = this.CreatePreviewTemplate();
      grid.Columns.Add(gridColumn2);
      GridColumn gridColumn3 = new GridColumn();
      gridColumn3.FieldName = "ApplyToRow";
      gridColumn3.Header = (object) this.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_ApplyToRow);
      gridColumn3.AllowEditing = DefaultBoolean.True;
      gridColumn3.CellTemplate = this.CreateApplyToRowTemplate();
      grid.Columns.Add(gridColumn3);
      GridColumn gridColumn4 = new GridColumn();
      gridColumn4.FieldName = "RowName";
      gridColumn4.AllowEditing = DefaultBoolean.True;
      GridColumn gridColumn5 = gridColumn4;
      ComboBoxEditSettings comboBoxEditSettings1 = new ComboBoxEditSettings();
      comboBoxEditSettings1.IsTextEditable = new bool?(false);
      comboBoxEditSettings1.DisplayMember = "Caption";
      comboBoxEditSettings1.ValueMember = "FieldName";
      ComboBoxEditSettings comboBoxEditSettings2 = comboBoxEditSettings1;
      gridColumn5.EditSettings = (BaseEditSettings) comboBoxEditSettings2;
      gridColumn4.Visible = false;
      grid.Columns.Add(gridColumn4);
      GridColumn gridColumn6 = new GridColumn();
      gridColumn6.FieldName = "ColumnName";
      gridColumn6.AllowEditing = DefaultBoolean.True;
      GridColumn gridColumn7 = gridColumn6;
      ComboBoxEditSettings comboBoxEditSettings3 = new ComboBoxEditSettings();
      comboBoxEditSettings3.IsTextEditable = new bool?(false);
      comboBoxEditSettings3.DisplayMember = "Caption";
      comboBoxEditSettings3.ValueMember = "FieldName";
      ComboBoxEditSettings comboBoxEditSettings4 = comboBoxEditSettings3;
      gridColumn7.EditSettings = (BaseEditSettings) comboBoxEditSettings4;
      gridColumn6.Visible = false;
      grid.Columns.Add(gridColumn6);
      GridColumn gridColumn8 = new GridColumn();
      gridColumn8.FieldName = "AppliesTo";
      GridColumn gridColumn9 = gridColumn8;
      ComboBoxEditSettings comboBoxEditSettings5 = new ComboBoxEditSettings();
      comboBoxEditSettings5.IsTextEditable = new bool?(false);
      comboBoxEditSettings5.DisplayMember = "Caption";
      comboBoxEditSettings5.ValueMember = "FieldName";
      ComboBoxEditSettings comboBoxEditSettings6 = comboBoxEditSettings5;
      gridColumn9.EditSettings = (BaseEditSettings) comboBoxEditSettings6;
      gridColumn8.AllowEditing = DefaultBoolean.True;
      grid.Columns.Add(gridColumn8);
      GridColumn gridColumn10 = new GridColumn();
      gridColumn10.FieldName = "IsEnabled";
      gridColumn10.Header = (object) this.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_IsEnabled);
      gridColumn10.EditSettings = (BaseEditSettings) new CheckEditSettings();
      gridColumn10.AllowEditing = DefaultBoolean.True;
      grid.Columns.Add(gridColumn10);
    }

    private DataTemplate CreatePreviewTemplate()
    {
      return XamlReader.Parse("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:dxg=\"http://schemas.devexpress.com/winfx/2008/xaml/grid\"><Border Padding=\"0,3,8,3\"><dxg:" + typeof (FormatPreviewControl).Name + " Content=\"{Binding Value}\"/></Border></DataTemplate>") as DataTemplate;
    }

    private DataTemplate CreateApplyToRowTemplate()
    {
      return XamlReader.Parse("<DataTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:dxe=\"http://schemas.devexpress.com/winfx/2008/xaml/editors\"><dxe:" + typeof (CheckEdit).Name + " Name=\"PART_Editor\" IsEnabled=\"{Binding RowData.Row.CanApplyToRow}\"/></DataTemplate>") as DataTemplate;
    }

    private void SetupView(TableView view)
    {
      view.ShowGroupPanel = false;
      view.AllowResizing = false;
      view.AllowColumnFiltering = false;
      view.IsColumnMenuEnabled = false;
      view.AllowGrouping = false;
      view.AllowSorting = false;
      view.AutoWidth = true;
      view.AllowColumnMoving = false;
      view.ShowIndicator = false;
      view.ShowVerticalLines = false;
      view.AllowEditing = false;
      view.AllowPerPixelScrolling = true;
      view.RowMinHeight = 35.0;
      view.EditorButtonShowMode = EditorButtonShowMode.ShowAlways;
      view.NavigationStyle = GridViewNavigationStyle.Cell;
      view.ShowSearchPanelMode = ShowSearchPanelMode.Never;
    }

    private void SetupSource(GridControl grid, ManagerViewModel viewModel)
    {
      grid.SetBinding(DataControlBase.ItemsSourceProperty, (BindingBase) new Binding("Items")
      {
        Source = (object) viewModel
      });
      grid.SetBinding(DataControlBase.CurrentItemProperty, (BindingBase) new Binding("SelectedItem")
      {
        Source = (object) viewModel
      });
      grid.Columns["AppliesTo"].EditSettings.SetBinding(LookUpEditSettingsBase.ItemsSourceProperty, (BindingBase) new Binding("FieldNames")
      {
        Source = (object) viewModel
      });
      grid.Columns["AppliesTo"].SetBinding(BaseColumn.HeaderProperty, (BindingBase) new Binding("ApplyToFieldNameCaption")
      {
        Source = (object) viewModel
      });
      grid.Columns["RowName"].SetBinding(BaseColumn.HeaderProperty, (BindingBase) new Binding("ApplyToPivotRowCaption")
      {
        Source = (object) viewModel
      });
      grid.Columns["ColumnName"].SetBinding(BaseColumn.HeaderProperty, (BindingBase) new Binding("ApplyToPivotColumnCaption")
      {
        Source = (object) viewModel
      });
      if (viewModel == null || !viewModel.IsPivot)
      {
        grid.Columns["RowName"].Visible = false;
        grid.Columns["ColumnName"].Visible = false;
      }
      else
      {
        grid.Columns["ApplyToRow"].Visible = false;
        grid.Columns["RowName"].Visible = true;
        grid.Columns["ColumnName"].Visible = true;
        grid.Columns["RowName"].EditSettings.SetBinding(LookUpEditSettingsBase.ItemsSourceProperty, (BindingBase) new Binding("PivotSpecialFieldNames")
        {
          Source = (object) viewModel
        });
        grid.Columns["ColumnName"].EditSettings.SetBinding(LookUpEditSettingsBase.ItemsSourceProperty, (BindingBase) new Binding("PivotSpecialFieldNames")
        {
          Source = (object) viewModel
        });
      }
    }

    private void SetupDefaultStyles(GridControl grid)
    {
      grid.Resources.Add((object) typeof (ScrollBar), (object) new Style()
      {
        TargetType = typeof (ScrollBar)
      });
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      GridControl grid = (GridControl) sender;
      (grid.DataContext as ManagerViewModel).Do<ManagerViewModel>((Action<ManagerViewModel>) (x => this.SetupSource(grid, x)));
    }

    private void OnRowDoubleClick(object sender, MouseButtonEventArgs e)
    {
      TableView tableView = sender as TableView;
      ITableViewHitInfo tableViewHitInfo = (ITableViewHitInfo) tableView.CalcHitInfo(e.OriginalSource as DependencyObject);
      if (tableViewHitInfo == null || !tableViewHitInfo.InRow)
        return;
      ManagerViewModel managerViewModel = tableView.DataContext as ManagerViewModel;
      if (managerViewModel == null || managerViewModel.SelectedItem == null || e.ChangedButton != MouseButton.Left)
        return;
      managerViewModel.ShowEditDialog(managerViewModel.SelectedItem);
      e.Handled = true;
    }

    private void OnCellValueChanging(object sender, CellValueChangedEventArgs e)
    {
      if (!(e.Column.FieldName == "AppliesTo") && !(e.Column.FieldName == "ApplyToRow") && (!(e.Column.FieldName == "ColumnName") && !(e.Column.FieldName == "RowName")) && !(e.Column.FieldName == "IsEnabled"))
        return;
      ((DataViewBase) sender).PostEditor();
    }

    private string GetLocalizedString(ConditionalFormattingStringId id)
    {
      return ConditionalFormattingLocalizer.GetString(id);
    }
  }
}
