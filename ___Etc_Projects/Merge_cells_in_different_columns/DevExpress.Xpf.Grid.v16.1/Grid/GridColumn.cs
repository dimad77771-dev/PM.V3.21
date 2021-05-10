// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridColumn
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Core;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraGrid;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The grid column.
  /// </para>
  ///             </summary>
  public class GridColumn : GridColumnBase, IDetailElement<BaseColumn>
  {
    protected internal static readonly DependencyPropertyKey IsGroupedPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.IsGrouped" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupedProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.GroupInterval" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupIntervalProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.GroupIndex" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupIndexProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.GroupValueTemplate" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueTemplateProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.GroupValueTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupValueTemplateSelectorProperty;
    private static readonly DependencyPropertyKey ActualGroupValueTemplateSelectorPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.ActualGroupValueTemplateSelector" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupValueTemplateSelectorProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.AllowGrouping" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowGroupingProperty;
    private static readonly DependencyPropertyKey ActualAllowGroupingPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridColumn.ActualAllowGrouping" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualAllowGroupingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowGroupedColumnProperty;

    protected internal override bool ActualAllowGroupingCore
    {
      get
      {
        return this.ActualAllowGrouping;
      }
    }

    internal override int GroupIndexCore
    {
      get
      {
        return this.GroupIndex;
      }
      set
      {
        this.GroupIndex = value;
      }
    }

    /// <summary>
    ///                 <para>Gets whether a View is grouped by the values of this column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if a View is grouped by the values of this column; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnIsGrouped")]
    public bool IsGrouped
    {
      get
      {
        return (bool) this.GetValue(GridColumn.IsGroupedProperty);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets how data rows are grouped when grouping by this column is applied. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.XtraGrid.ColumnGroupInterval" /> enumeration value that specifies how data rows are grouped.
    /// </value>
    [XtraSerializableProperty]
    [GridUIProperty]
    [DefaultValue(ColumnGroupInterval.Default)]
    [DevExpressXpfGridLocalizedDescription("GridColumnGroupInterval")]
    [Category("Data")]
    public ColumnGroupInterval GroupInterval
    {
      get
      {
        return (ColumnGroupInterval) this.GetValue(GridColumn.GroupIntervalProperty);
      }
      set
      {
        this.SetValue(GridColumn.GroupIntervalProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the column's position among grouping columns.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the column's position among grouping columns. <b>-1</b> if a View isn't grouped by the values of this column.
    /// </value>
    [Browsable(false)]
    public int GroupIndex
    {
      get
      {
        return (int) this.GetValue(GridColumn.GroupIndexProperty);
      }
      set
      {
        this.SetValue(GridColumn.GroupIndexProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of column values displayed within group rows. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of group values.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnGroupValueTemplate")]
    [Category("Appearance ")]
    public DataTemplate GroupValueTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridColumn.GroupValueTemplateProperty);
      }
      set
      {
        this.SetValue(GridColumn.GroupValueTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that chooses a group value template based on custom logic. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnGroupValueTemplateSelector")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Category("Appearance ")]
    public DataTemplateSelector GroupValueTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridColumn.GroupValueTemplateSelectorProperty);
      }
      set
      {
        this.SetValue(GridColumn.GroupValueTemplateSelectorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the actual template selector that chooses a group row value template based on custom logic. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.DataTemplateSelector" /> descendant that chooses a template based on custom logic.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnActualGroupValueTemplateSelector")]
    public DataTemplateSelector ActualGroupValueTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) this.GetValue(GridColumn.ActualGroupValueTemplateSelectorProperty);
      }
      private set
      {
        this.SetValue(GridColumn.ActualGroupValueTemplateSelectorPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that specifies whether an end-user can group data by the current column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Utils.DefaultBoolean" /> enumeration value that specifies whether an end-user can group data by the current column.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnAllowGrouping")]
    [Category("Layout")]
    [XtraSerializableProperty]
    public DefaultBoolean AllowGrouping
    {
      get
      {
        return (DefaultBoolean) this.GetValue(GridColumn.AllowGroupingProperty);
      }
      set
      {
        this.SetValue(GridColumn.AllowGroupingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether an end-user can group data by the current column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if an end-user can group data by the current column; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridColumnActualAllowGrouping")]
    public bool ActualAllowGrouping
    {
      get
      {
        return (bool) this.GetValue(GridColumn.ActualAllowGroupingProperty);
      }
      private set
      {
        this.SetValue(GridColumn.ActualAllowGroupingPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether to display the grouped column within the view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to display the grouped column within the view; otherwise, <b>false</b>.
    /// </value>
    [Category("Options View")]
    [XtraSerializableProperty]
    public DefaultBoolean ShowGroupedColumn
    {
      get
      {
        return (DefaultBoolean) this.GetValue(GridColumn.ShowGroupedColumnProperty);
      }
      set
      {
        this.SetValue(GridColumn.ShowGroupedColumnProperty, (object) value);
      }
    }

    private GridControl Grid
    {
      get
      {
        return (GridControl) this.OwnerControl;
      }
    }

    protected override IColumnCollection ParentCollection
    {
      get
      {
        if (TableView.IsCheckBoxSelectorColumn(this.FieldName))
          return (IColumnCollection) null;
        return base.ParentCollection;
      }
    }

    /// <summary>
    ///                 <para>Enables you to validate the active editor's value.
    /// </para>
    ///             </summary>
    public event GridCellValidationEventHandler Validate;

    static GridColumn()
    {
      Type ownerType = typeof (GridColumn);
      GridColumn.IsGroupedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGrouped", typeof (bool), ownerType, new PropertyMetadata((object) false));
      GridColumn.IsGroupedProperty = GridColumn.IsGroupedPropertyKey.DependencyProperty;
      GridColumn.GroupIntervalProperty = DependencyPropertyManager.Register("GroupInterval", typeof (ColumnGroupInterval), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) ColumnGroupInterval.Default, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).OnGroupIntervalChanged())));
      GridColumn.GroupIndexProperty = DependencyPropertyManager.Register("GroupIndex", typeof (int), ownerType, new PropertyMetadata((object) -1, new PropertyChangedCallback(GridColumn.OnGroupIndexChanged)));
      GridColumn.GroupValueTemplateProperty = DependencyPropertyManager.Register("GroupValueTemplate", typeof (DataTemplate), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).UpdateActualGroupValueTemplateSelector())));
      GridColumn.GroupValueTemplateSelectorProperty = DependencyPropertyManager.Register("GroupValueTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).UpdateActualGroupValueTemplateSelector())));
      GridColumn.ActualGroupValueTemplateSelectorPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupValueTemplateSelector", typeof (DataTemplateSelector), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).OnActualGroupValueTemplateSelectorChanged())));
      GridColumn.ActualGroupValueTemplateSelectorProperty = GridColumn.ActualGroupValueTemplateSelectorPropertyKey.DependencyProperty;
      GridColumn.AllowGroupingProperty = DependencyPropertyManager.Register("AllowGrouping", typeof (DefaultBoolean), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) DefaultBoolean.Default, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).UpdateActualAllowGrouping())));
      GridColumn.ActualAllowGroupingPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualAllowGrouping", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      GridColumn.ActualAllowGroupingProperty = GridColumn.ActualAllowGroupingPropertyKey.DependencyProperty;
      GridColumn.ShowGroupedColumnProperty = DependencyPropertyManager.Register("ShowGroupedColumn", typeof (DefaultBoolean), ownerType, new PropertyMetadata((object) DefaultBoolean.Default, (PropertyChangedCallback) ((d, e) => ((GridColumn) d).UpdateActualShowGroupedColumn())));
    }

    private static void OnGroupIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      GridColumn gridColumn = d as GridColumn;
      if (gridColumn == null)
        return;
      gridColumn.OnGroupIndexChanged();
    }

    internal override void UpdateViewInfo(bool updateDataPropertiesOnly = false)
    {
      base.UpdateViewInfo(updateDataPropertiesOnly);
      if (this.Grid == null || updateDataPropertiesOnly)
        return;
      this.UpdateAutoFilterValue();
    }

    protected internal override void SetSortInfo(ColumnSortOrder sortOrder, bool isGrouped)
    {
      base.SetSortInfo(sortOrder, isGrouped);
      this.SetValue(GridColumn.IsGroupedPropertyKey, (object) isGrouped);
    }

    private void OnGroupIndexChanged()
    {
      if (this.Grid == null)
        return;
      this.Grid.ApplyColumnGroupIndex((ColumnBase) this);
    }

    internal void UpdateActualGroupValueTemplateSelector()
    {
      this.UpdateActualTemplateSelector(GridColumn.ActualGroupValueTemplateSelectorPropertyKey, this.GroupValueTemplateSelector, this.GroupValueTemplate, (Func<DataTemplateSelector, DataTemplate, DataTemplateSelector>) ((s, t) => (DataTemplateSelector) ActualTemplateSelectorWrapper.Combine(this.Owner.ActualGroupValueTemplateSelector, s, t)));
    }

    protected override void OnOwnerChanged()
    {
      base.OnOwnerChanged();
      this.UpdateActualGroupValueTemplateSelector();
    }

    protected void OnGroupIntervalChanged()
    {
      if (this.Grid != null)
        this.Grid.NeedSynchronize = true;
      this.OnDataPropertyChanged();
    }

    private bool CalcActualAllowGrouping()
    {
      if (!this.ActualAllowSorting)
        return false;
      return this.GetActualAllowGroupingCore();
    }

    internal override bool GetActualAllowGroupingCore()
    {
      if (this.AllowGrouping.GetValue(this.Owner.AllowGrouping))
        return base.GetActualAllowGroupingCore();
      return false;
    }

    private void UpdateActualAllowGrouping()
    {
      this.ActualAllowGrouping = this.CalcActualAllowGrouping();
    }

    protected override void UpdateActualAllowSorting()
    {
      base.UpdateActualAllowSorting();
      this.UpdateActualAllowGrouping();
    }

    private void UpdateActualShowGroupedColumn()
    {
      if (this.View == null)
        return;
      this.View.RebuildVisibleColumns();
    }

    protected internal override void OnValidation(GridRowValidationEventArgs e)
    {
      ((GridColumn) this.GetEventTargetColumn()).OnValidationCore(e);
    }

    private void OnValidationCore(GridRowValidationEventArgs e)
    {
      if (this.Validate == null)
        return;
      this.Validate((object) this, (GridCellValidationEventArgs) e);
    }

    protected internal override object GetWaitIndicator()
    {
      return (object) new ColumnWaitIndicator();
    }

    protected override void UpdateGroupingCore(string oldFieldName)
    {
      if (!this.IsGrouped || this.Grid == null)
        return;
      int groupIndex = this.GroupIndex;
      ColumnSortOrder sortOrder = this.SortOrder;
      this.Grid.BeginDataUpdate();
      this.Owner.UngroupColumn(oldFieldName);
      if (this.FieldName != null || this.FieldName != string.Empty)
        this.Owner.GroupColumn(this.FieldName, groupIndex, sortOrder);
      this.Grid.EndDataUpdate();
    }

    BaseColumn IDetailElement<BaseColumn>.CreateNewInstance(params object[] args)
    {
      GridColumn gridColumn = Activator.CreateInstance(this.GetType()) as GridColumn;
      gridColumn.IsCloned = true;
      gridColumn.isAutoDetectedUnboundType = this.isAutoDetectedUnboundType;
      return (BaseColumn) gridColumn;
    }

    protected override ColumnFilterInfoBase CreateColumnFilterInfo()
    {
      if (!this.ShouldCreateDateFilter())
        return base.CreateColumnFilterInfo();
      switch (this.FilterPopupMode)
      {
        case FilterPopupMode.Date:
          return (ColumnFilterInfoBase) new DateColumnFilterInfo((ColumnBase) this);
        case FilterPopupMode.DateAlt:
          return (ColumnFilterInfoBase) new DateAltColumnFilterInfo((ColumnBase) this);
        case FilterPopupMode.DateCompact:
          return (ColumnFilterInfoBase) new DateCompactColumnFilterInfo((ColumnBase) this);
        default:
          return (ColumnFilterInfoBase) new DateSmartColumnFilterInfo((ColumnBase) this);
      }
    }

    private bool ShouldCreateDateFilter()
    {
      if ((this.FilterPopupMode != FilterPopupMode.Default || !(this.FieldType == typeof (DateTime)) && !(this.FieldType == typeof (DateTime?))) && (this.FilterPopupMode != FilterPopupMode.Date && this.FilterPopupMode != FilterPopupMode.DateAlt) && this.FilterPopupMode != FilterPopupMode.DateSmart)
        return this.FilterPopupMode == FilterPopupMode.DateCompact;
      return true;
    }

    internal override bool IsServiceColumn()
    {
      return TableView.IsCheckBoxSelectorColumn(this.FieldName);
    }

    private void OnActualGroupValueTemplateSelectorChanged()
    {
      if (this.View == null)
        return;
      this.View.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UpdateClientGroupValueTemplateSelector()), true, true);
    }
  }
}
