// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FilterCriteriaControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering.Helpers;
using DevExpress.Utils;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Filtering;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media.Imaging;

namespace DevExpress.Xpf.Grid
{
  public class FilterCriteriaControl : FilterCriteriaControlBase
  {
    private Locker lockColumnChange = new Locker();
    public static readonly DependencyProperty ItemsSourceProperty;
    public static readonly DependencyProperty DefaultIndexProperty;
    public static readonly DependencyProperty ColumnProperty;
    public static readonly DependencyProperty SelectedFilterItemProperty;

    public List<FilterCriteriaControl.FilterItem> ItemsSource
    {
      get
      {
        return (List<FilterCriteriaControl.FilterItem>) this.GetValue(FilterCriteriaControl.ItemsSourceProperty);
      }
      set
      {
        this.SetValue(FilterCriteriaControl.ItemsSourceProperty, (object) value);
      }
    }

    public int DefaultIndex
    {
      get
      {
        return (int) this.GetValue(FilterCriteriaControl.DefaultIndexProperty);
      }
      set
      {
        this.SetValue(FilterCriteriaControl.DefaultIndexProperty, (object) value);
      }
    }

    public ColumnBase Column
    {
      get
      {
        return (ColumnBase) this.GetValue(FilterCriteriaControl.ColumnProperty);
      }
      set
      {
        this.SetValue(FilterCriteriaControl.ColumnProperty, (object) value);
      }
    }

    public FilterCriteriaControl.FilterItem SelectedFilterItem
    {
      get
      {
        return (FilterCriteriaControl.FilterItem) this.GetValue(FilterCriteriaControl.SelectedFilterItemProperty);
      }
      set
      {
        this.SetValue(FilterCriteriaControl.SelectedFilterItemProperty, (object) value);
      }
    }

    static FilterCriteriaControl()
    {
      Type type = typeof (FilterCriteriaControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) type));
      FilterCriteriaControl.ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof (List<FilterCriteriaControl.FilterItem>), type, new PropertyMetadata((PropertyChangedCallback) null));
      FilterCriteriaControl.DefaultIndexProperty = DependencyProperty.Register("DefaultIndex", typeof (int), type, new PropertyMetadata((object) 0));
      FilterCriteriaControl.ColumnProperty = DependencyProperty.Register("Column", typeof (ColumnBase), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FilterCriteriaControl) d).ColumnChanged((ColumnBase) e.OldValue))));
      FilterCriteriaControl.SelectedFilterItemProperty = DependencyProperty.Register("SelectedFilterItem", typeof (FilterCriteriaControl.FilterItem), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FilterCriteriaControl) d).SelectedFilterItemChanged())));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      ComboBoxEdit comboBoxEdit = this.GetTemplateChild("PART_cbFilterItems") as ComboBoxEdit;
      if (comboBoxEdit == null)
        return;
      comboBoxEdit.PopupOpening += new OpenPopupEventHandler(this.cbFilterItems_PopupOpening);
      Binding binding = new Binding() { Source = (object) comboBoxEdit, Path = new PropertyPath(LookUpEditBase.SelectedItemProperty.GetName(), new object[0]), Mode = BindingMode.TwoWay };
      this.SetBinding(FilterCriteriaControl.SelectedFilterItemProperty, (BindingBase) binding);
    }

    private void cbFilterItems_PopupOpening(object sender, OpenPopupEventArgs e)
    {
      if (this.Column == null || this.Column.View == null || !(this.Column.View.InplaceEditorOwner.CurrentCellEditor is FilterRowCellEditor))
        return;
      this.Column.View.InplaceEditorOwner.CurrentCellEditor.Edit.EditMode = EditMode.InplaceActive;
    }

    private void SelectedFilterItemChanged()
    {
      this.lockColumnChange.DoIfNotLocked((Action) (() =>
      {
        if (this.Column == null)
          return;
        this.Column.AutoFilterCriteria = this.SelectedFilterItem != null ? new ClauseType?(this.SelectedFilterItem.ClauseType) : new ClauseType?();
        if (this.Column.View == null || this.Column.View.DataControl == null || (this.Column.View.FocusedRowHandle != -999997 || this.Column != this.Column.View.DataControl.CurrentColumn))
          return;
        if (this.Column.View.InplaceEditorOwner.ActiveEditor == null)
        {
          this.Column.View.ShowEditor();
          this.Column.View.SetActiveEditor();
        }
        if (this.Column.View.InplaceEditorOwner.ActiveEditor != null)
        {
          ((UIElement) BaseEditHelper.GetBaseEdit(this.Column.View.InplaceEditorOwner.ActiveEditor)).Focus();
        }
        else
        {
          if (this.Column.View.InplaceEditorOwner == null || this.Column.View.InplaceEditorOwner.CurrentCellEditor == null)
            return;
          this.Column.View.InplaceEditorOwner.CurrentCellEditor.Edit.EditMode = EditMode.InplaceInactive;
          this.Column.View.ShowEditor();
          this.Column.View.SetActiveEditor();
          if (this.Column.View.InplaceEditorOwner.ActiveEditor == null)
            return;
          ((UIElement) BaseEditHelper.GetBaseEdit(this.Column.View.InplaceEditorOwner.ActiveEditor)).Focus();
        }
      }));
    }

    private void ColumnChanged(ColumnBase oldColumn)
    {
      if (oldColumn != null)
        oldColumn.ContentChanged -= new ColumnContentChangedEventHandler(this.Column_ContentChanged);
      if (this.Column != null)
        this.Column.ContentChanged += new ColumnContentChangedEventHandler(this.Column_ContentChanged);
      this.lockColumnChange.DoLockedAction((Action) (() =>
      {
        this.RefreshItems();
        if (!this.Column.AutoFilterCriteria.HasValue)
          return;
        ClauseType cType = this.Column.AutoFilterCriteria.Value;
        if (this.SelectedFilterItem == null || this.SelectedFilterItem.ClauseType == cType)
          return;
        this.SelectedFilterItem = this.ItemsSource.FirstOrDefault<FilterCriteriaControl.FilterItem>((Func<FilterCriteriaControl.FilterItem, bool>) (x => x.ClauseType == cType));
      }));
    }

    private void Column_ContentChanged(object sender, ColumnContentChangedEventArgs e)
    {
      if (e.Property == ColumnBase.ColumnFilterModeProperty || e.Property == ColumnBase.EditSettingsProperty)
        this.RefreshItems();
      if (e.Property != ColumnBase.AutoFilterCriteriaProperty)
        return;
      this.lockColumnChange.DoLockedAction((Action) (() => this.SelectedFilterItem = this.ItemsSource.FirstOrDefault<FilterCriteriaControl.FilterItem>((Func<FilterCriteriaControl.FilterItem, bool>) (x =>
      {
        ClauseType clauseType = x.ClauseType;
        ClauseType? autoFilterCriteria = this.Column.AutoFilterCriteria;
        if (clauseType == autoFilterCriteria.GetValueOrDefault())
          return autoFilterCriteria.HasValue;
        return false;
      }))));
    }

    private void RefreshItems()
    {
      FilterColumn filterColumn = FilterClauseHelper.CreateFilterColumn(this.Column);
      ClauseType defaultOperation = this.GetDefaultOperation(filterColumn, this.Column);
      int num = 0;
      List<FilterCriteriaControl.FilterItem> filterItemList = new List<FilterCriteriaControl.FilterItem>();
      List<ClauseType> operationsByTypes = this.GetListOperationsByTypes(filterColumn);
      for (int index = 0; index < operationsByTypes.Count; ++index)
      {
        if (operationsByTypes[index] == defaultOperation)
          num = index;
        filterItemList.Add(new FilterCriteriaControl.FilterItem()
        {
          Name = OperationHelper.GetMenuStringByType(operationsByTypes[index]),
          Image = this.Column.View.GetGlyphFilterCriteria(operationsByTypes[index].ToString()),
          ClauseType = operationsByTypes[index]
        });
      }
      this.ItemsSource = filterItemList;
      if (this.Column != null && this.Column.AutoFilterCriteria.HasValue)
      {
        int index = this.ItemsSource.FindIndex((Predicate<FilterCriteriaControl.FilterItem>) (x =>
        {
          ClauseType clauseType = x.ClauseType;
          ClauseType? autoFilterCriteria = this.Column.AutoFilterCriteria;
          if (clauseType == autoFilterCriteria.GetValueOrDefault())
            return autoFilterCriteria.HasValue;
          return false;
        }));
        if (index != -1)
        {
          this.DefaultIndex = index;
          return;
        }
      }
      this.DefaultIndex = num;
    }

    protected internal List<ClauseType> GetListOperationsByTypes(FilterColumn filterColumn)
    {
      List<ClauseType> clauseTypeList = new List<ClauseType>();
      foreach (ClauseType type in typeof (ClauseType).GetValues())
      {
        if (FilterClauseHelper.IsValidClause(filterColumn, type, this.Column.ColumnFilterMode == ColumnFilterMode.DisplayText))
          clauseTypeList.Add(type);
      }
      return clauseTypeList;
    }

    protected internal virtual ClauseType GetDefaultOperation(FilterColumn filterColumn, ColumnBase column)
    {
      if (filterColumn == null)
        return ClauseType.Equals;
      switch (filterColumn.ClauseClass)
      {
        case FilterColumnClauseClass.String:
          return ClauseType.BeginsWith;
        case FilterColumnClauseClass.Blob:
          return ClauseType.IsNotNull;
        default:
          return ClauseType.Equals;
      }
    }

    [Browsable(false)]
    public bool ShouldSerializeItemsSource(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    public class FilterItem
    {
      public string Name { get; set; }

      public BitmapImage Image { get; set; }

      public ClauseType ClauseType { get; set; }
    }
  }
}
