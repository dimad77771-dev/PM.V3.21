// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Filtering;
using DevExpress.Data.Helpers;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Native.ViewGenerator;
using DevExpress.Utils;
using DevExpress.Utils.Design.DataAccess;
using DevExpress.Utils.Serializing;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Core.Serialization;
using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Automation;
using DevExpress.Xpf.Grid.Helpers;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Grid.TreeList;
using DevExpress.Xpf.GridData;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Utils.About;
using DevExpress.XtraEditors.DXErrorProvider;
using DevExpress.XtraGrid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The grid control.
  /// </para>
  ///             </summary>
  [LicenseProvider(typeof (DX_WPF_ControlRequiredForReports_LicenseProvider))]
  [DXToolboxBrowsable]
  [DataAccessMetadata("All", EnableInMemoryCollectionViewBinding = false, SupportedProcessingModes = "All")]
  [SupportDXTheme]
  public class GridControl : GridDataControlBase, IAddChild, IDataProviderOwner, IDataProviderEvents, IDataControllerVisualClient, ISupportInitialize, IDetailElement<DataControlBase>
  {
    private readonly AsyncOperationWaitHandle asyncOpWaitHandle = new AsyncOperationWaitHandle();
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.View" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ViewProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.StartSorting" /> routed event.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent StartSortingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.EndSorting" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent EndSortingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.StartGrouping" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent StartGroupingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.EndGrouping" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent EndGroupingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowExpanding" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent GroupRowExpandingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowExpanded" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent GroupRowExpandedEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowCollapsing" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent GroupRowCollapsingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowCollapsed" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent GroupRowCollapsedEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.CustomGroupDisplayText" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CustomGroupDisplayTextEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.MasterRowExpanding" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent MasterRowExpandingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.MasterRowCollapsing" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent MasterRowCollapsingEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.MasterRowExpanded" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent MasterRowExpandedEvent;
    /// <summary>
    ///                 <para>Identifies the <see cref="E:DevExpress.Xpf.Grid.GridControl.MasterRowCollapsed" /> routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent MasterRowCollapsedEvent;
    protected static readonly DependencyPropertyKey IsGroupedPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.IsGrouped" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsGroupedProperty;
    private static readonly DependencyPropertyKey ActualGroupCountPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ActualGroupCountProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.DataSource" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    [Obsolete("Instead use the ItemsSource property. For detailed information, see the list of breaking changes in DXperience v2011 vol 1.")]
    public static readonly DependencyProperty DataSourceProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.AutoExpandAllGroups" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoExpandAllGroupsProperty;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.IsRecursiveExpand" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsRecursiveExpandProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowLiveDataShapingProperty;
    private static readonly DependencyPropertyKey IsAsyncOperationInProgressPropertyKey;
    /// <summary>
    ///                 <para>Identifies the <see cref="P:DevExpress.Xpf.Grid.GridControl.IsRecursiveExpand" /> dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsAsyncOperationInProgressProperty;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent AsyncOperationStartedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent AsyncOperationCompletedEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent CopyingToClipboardEvent;
    /// <summary>
    ///                 <para>Identifies the  routed event.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly RoutedEvent SelectionChangedEvent;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummaryGeneratorTemplateProperty;
    [IgnoreDependencyPropertiesConsistencyChecker]
    private static readonly DependencyProperty GroupSummaryItemsAttachedBehaviorProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty GroupSummarySourceProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DetailDescriptorProperty;
    private static readonly DependencyPropertyKey BandsLayoutPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty BandsLayoutProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty OptimizeSummaryCalculationProperty;
    private ObservableCollectionCore<GridColumn> groupedColumns;
    private GridColumnDataEventHandler customUnboundColumnData;
    private EventHandler<SubstituteFilterEventArgs> substituteFilter;
    private EventHandler<SubstituteSortInfoEventArgs> substituteSortInfo;
    private CustomSummaryEventHandler customSummary;
    private CustomSummaryExistEventHandler customSummaryExists;
    private CustomColumnSortEventHandler customColumnSort;
    private CustomColumnSortEventHandler customColumnGroup;
    private RowFilterEventHandler customRowFilter;
    private GridColumnSortData sortData;
    private GridFilterData filterData;
    private GridFilterData searchFilterData;
    private bool isAsyncOperationInProgress;
    private CustomColumnDisplayTextEventArgs customColumnDisplayTextEventArgs;
    private GridControl.VisualClientUpdater visualClientUpdater;

    internal IDesignTimeGridAdorner DesignTimeGridAdorner
    {
      get
      {
        return (IDesignTimeGridAdorner) this.DesignTimeAdorner;
      }
    }

    protected internal override IDesignTimeAdornerBase EmptyDesignTimeAdorner
    {
      get
      {
        return (IDesignTimeAdornerBase) EmptyDesignTimeGridAdorner.Instance;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Thickness BorderThickness
    {
      get
      {
        return base.BorderThickness;
      }
      set
      {
        base.BorderThickness = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Browsable(false)]
    public new Brush Background
    {
      get
      {
        return base.Background;
      }
      set
      {
        base.Background = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    public new Brush BorderBrush
    {
      get
      {
        return base.BorderBrush;
      }
      set
      {
        base.BorderBrush = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public new Brush Foreground
    {
      get
      {
        return base.Foreground;
      }
      set
      {
        base.Foreground = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new Thickness Padding
    {
      get
      {
        return base.Padding;
      }
      set
      {
        base.Padding = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new HorizontalAlignment HorizontalContentAlignment
    {
      get
      {
        return base.HorizontalContentAlignment;
      }
      set
      {
        base.HorizontalContentAlignment = value;
      }
    }

    /// <summary>
    ///                 <para>This member supports the WPF infrastructure, and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    public new VerticalAlignment VerticalContentAlignment
    {
      get
      {
        return base.VerticalContentAlignment;
      }
      set
      {
        base.VerticalContentAlignment = value;
      }
    }

    protected internal GridDataProviderBase GridDataProvider
    {
      get
      {
        return (GridDataProviderBase) this.DataProviderBase;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the source of group summaries. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The source from which the grid generates group summary items.</value>
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("Data")]
    public IEnumerable GroupSummarySource
    {
      get
      {
        return (IEnumerable) this.GetValue(GridControl.GroupSummarySourceProperty);
      }
      set
      {
        this.SetValue(GridControl.GroupSummarySourceProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a template that describes group summaries. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The summary item template.</value>
    [Category("Appearance ")]
    [CloneDetailMode(CloneDetailMode.Skip)]
    public DataTemplate GroupSummaryGeneratorTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridControl.GroupSummaryGeneratorTemplateProperty);
      }
      set
      {
        this.SetValue(GridControl.GroupSummaryGeneratorTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether all group rows are automatically expanded after each grouping operation. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to expand all group rows after each grouping operation; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlAutoExpandAllGroups")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool AutoExpandAllGroups
    {
      get
      {
        return (bool) this.GetValue(GridControl.AutoExpandAllGroupsProperty);
      }
      set
      {
        this.SetValue(GridControl.AutoExpandAllGroupsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether child group rows at all nesting levels are automatically expanded when expanding their parent group row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to expand child group rows at all nesting levels when expanding their parent group row; otherwise, <b>false</b>.
    /// </value>
    [Category("Options Behavior")]
    [DevExpressXpfGridLocalizedDescription("GridControlIsRecursiveExpand")]
    [XtraSerializableProperty]
    public bool IsRecursiveExpand
    {
      get
      {
        return (bool) this.GetValue(GridControl.IsRecursiveExpandProperty);
      }
      set
      {
        this.SetValue(GridControl.IsRecursiveExpandProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether or not the grid recalculates sorting/grouping/filtering/summaries automatically when data in a data source is modified outside the grid.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to recalculate sorting/grouping/filtering/summaries/scrollbar annotations automatically when data in a data source is modified outside the grid; otherwise, <b>false</b>.
    /// </value>
    [XtraSerializableProperty]
    [DevExpressXpfGridLocalizedDescription("GridControlAllowLiveDataShaping")]
    [Category("Options Behavior")]
    public bool? AllowLiveDataShaping
    {
      get
      {
        return (bool?) this.GetValue(GridControl.AllowLiveDataShapingProperty);
      }
      set
      {
        this.SetValue(GridControl.AllowLiveDataShapingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the grid's data is grouped.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if data grouping is applied; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlIsGrouped")]
    public bool IsGrouped
    {
      get
      {
        return (bool) this.GetValue(GridControl.IsGroupedProperty);
      }
    }

    internal AsyncOperationWaitHandle AsyncWaitHandle
    {
      get
      {
        return this.asyncOpWaitHandle;
      }
    }

    protected bool HasCustomRowFilter
    {
      get
      {
        return this.GetOriginationGridControl().customRowFilter != null;
      }
    }

    /// <summary>
    ///                 <para>Gets whether or not data is being loaded in Instant Feedback UI Mode. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if data is being loaded; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlIsAsyncOperationInProgress")]
    public bool IsAsyncOperationInProgress
    {
      get
      {
        return (bool) this.GetValue(GridControl.IsAsyncOperationInProgressProperty);
      }
      private set
      {
        this.SetValue(GridControl.IsAsyncOperationInProgressPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the data controller which implements data-aware operations.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Data.DataController" /> object which represents the data controller.
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public DataController DataController
    {
      get
      {
        return (DataController) this.DataProviderBase.DataController;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the number of grouping columns.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the number of grouping columns.</value>
    [DefaultValue(0)]
    [DevExpressXpfGridLocalizedDescription("GridControlGroupCount")]
    [Category("Data")]
    [XtraSerializableProperty]
    [GridUIProperty]
    [Browsable(false)]
    public int GroupCount
    {
      get
      {
        return this.SortInfo.GroupCount;
      }
      set
      {
        this.SortInfo.GroupCount = value;
      }
    }

    /// <summary>
    ///                 <para>Gets the actual number of grouping columns.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the number of grouping columns.</value>
    [DevExpressXpfGridLocalizedDescription("GridControlActualGroupCount")]
    public int ActualGroupCount
    {
      get
      {
        return (int) this.GetValue(GridControl.ActualGroupCountProperty);
      }
      private set
      {
        this.SetValue(GridControl.ActualGroupCountPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Enables sorting group rows by their summary values.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridGroupSummarySortInfoCollection" /> object that contains the information required to sort group rows by summary values.
    /// </value>
    [Browsable(false)]
    [XtraSerializableProperty(true, false, false, 2147483647)]
    [XtraResetProperty]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [GridUIProperty]
    public GridGroupSummarySortInfoCollection GroupSummarySortInfo
    {
      get
      {
        return this.GridDataProvider.GroupSummarySortInfo;
      }
    }

    /// <summary>
    ///                 <para>Provides access to group summary items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItemCollection" /> object that represents the collection of group summary items.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlGroupSummary")]
    [XtraSerializableProperty(true, false, false)]
    [Category("Data")]
    [GridUIProperty]
    [XtraResetProperty]
    public GridSummaryItemCollection GroupSummary
    {
      get
      {
        return (GridSummaryItemCollection) this.GroupSummaryCore;
      }
    }

    /// <summary>
    ///                 <para>Enables master-detail representation within this <see cref="T:DevExpress.Xpf.Grid.GridControl" />.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> descendant specifying which details to display for this <see cref="T:DevExpress.Xpf.Grid.GridControl" />.
    /// </value>
    [Category("Master-Detail")]
    public DetailDescriptorBase DetailDescriptor
    {
      get
      {
        return (DetailDescriptorBase) this.GetValue(GridControl.DetailDescriptorProperty);
      }
      set
      {
        this.SetValue(GridControl.DetailDescriptorProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>This member supports the internal infrastructure and is not intended to be used directly from your code.
    /// </para>
    ///             </summary>
    /// <value>
    /// </value>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Browsable(false)]
    public GridControlBandsLayout BandsLayout
    {
      get
      {
        return (GridControlBandsLayout) this.GetValue(GridControl.BandsLayoutProperty);
      }
      internal set
      {
        this.SetValue(GridControl.BandsLayoutPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Specifies whether to optimize summary calculation. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, to optimize summary calculation; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlOptimizeSummaryCalculation")]
    [Category("Options Behavior")]
    [XtraSerializableProperty]
    public bool OptimizeSummaryCalculation
    {
      get
      {
        return (bool) this.GetValue(GridControl.OptimizeSummaryCalculationProperty);
      }
      set
      {
        this.SetValue(GridControl.OptimizeSummaryCalculationProperty, (object) value);
      }
    }

    internal override DetailDescriptorBase DetailDescriptorCore
    {
      get
      {
        return this.DetailDescriptor;
      }
    }

    internal override int ActualGroupCountCore
    {
      get
      {
        return this.ActualGroupCount;
      }
      set
      {
        this.ActualGroupCount = value;
      }
    }

    internal IList<GridColumn> GroupedColumns
    {
      get
      {
        if (this.groupedColumns == null)
          this.RebuildGroupSortIndexesAndGroupedColumns();
        return (IList<GridColumn>) this.groupedColumns;
      }
    }

    protected internal GridFilterData FilterData
    {
      get
      {
        if (this.filterData == null)
        {
          this.filterData = new GridFilterData(this);
          this.filterData.OnStart();
        }
        return this.filterData;
      }
    }

    protected internal GridFilterData SearchFilterData
    {
      get
      {
        if (this.searchFilterData == null)
        {
          if (this.View == null)
            return (GridFilterData) null;
          this.searchFilterData = (GridFilterData) new GridSearchFilterData(this);
          this.searchFilterData.OnStart();
        }
        return this.searchFilterData;
      }
    }

    protected internal GridColumnSortData SortData
    {
      get
      {
        if (this.sortData == null)
          this.sortData = new GridColumnSortData(this);
        return this.sortData;
      }
    }

    /// <summary>
    ///                 <para>Obsolete. Gets or sets the grid's data source. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the data source from which the grid retrieves its data.</value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Instead use the ItemsSource property. For detailed information, see the list of breaking changes in DXperience v2011 vol 1.")]
    [Bindable(true)]
    [Category("Data")]
    [DevExpressXpfGridLocalizedDescription("GridControlDataSource")]
    public object DataSource
    {
      get
      {
        return this.GetValue(GridControl.DataSourceProperty);
      }
      set
      {
        this.SetValue(GridControl.DataSourceProperty, value);
      }
    }

    /// <summary>
    ///                 <para>Provides access to the collection of sorted and grouping columns.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridSortInfoCollection" /> collection that contains information on the sorted and grouping columns.
    /// </value>
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    [Browsable(false)]
    [Category("Data")]
    [XtraSerializableProperty(true, false, false)]
    [GridUIProperty]
    [XtraResetProperty]
    public GridSortInfoCollection SortInfo
    {
      get
      {
        return (GridSortInfoCollection) this.SortInfoCore;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the grid's column collection.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumnCollection" /> object that represents a collection of columns within the grid.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridControlColumns")]
    [Category("Data")]
    [XtraSerializableProperty(true, true, true)]
    [GridStoreAlwaysProperty]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    public GridColumnCollection Columns
    {
      get
      {
        return (GridColumnCollection) this.ColumnsCore;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the grid's view. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that specifies the grid view used to display data.
    /// 
    /// </value>
    [XtraSerializableProperty(XtraSerializationVisibility.Content)]
    [GridStoreAlwaysProperty]
    [DevExpressXpfGridLocalizedDescription("GridControlView")]
    [CloneDetailMode(CloneDetailMode.Skip)]
    [Category("View")]
    public DataViewBase View
    {
      get
      {
        return (DataViewBase) this.GetValue(GridControl.ViewProperty);
      }
      set
      {
        this.SetValue(GridControl.ViewProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Provides access to a collection of total summary items.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItemCollection" /> object that contains total summary items.
    /// </value>
    [GridUIProperty]
    [Category("Data")]
    [XtraSerializableProperty(true, false, false)]
    [XtraResetProperty]
    [DevExpressXpfGridLocalizedDescription("GridControlTotalSummary")]
    public GridSummaryItemCollection TotalSummary
    {
      get
      {
        return (GridSummaryItemCollection) this.TotalSummaryCore;
      }
    }

    internal override DataViewBase DataView
    {
      get
      {
        return this.viewCore;
      }
      set
      {
        this.View = value;
      }
    }

    protected internal override Type ColumnType
    {
      get
      {
        return typeof (GridColumn);
      }
    }

    protected internal override Type BandType
    {
      get
      {
        return typeof (GridControlBand);
      }
    }

    bool IDataProviderOwner.IsSynchronizedWithCurrentItem
    {
      get
      {
        if (this.DataView != null)
          return this.DataView.IsSynchronizedWithCurrentItem;
        return false;
      }
    }

    bool IDataProviderOwner.IsDesignTime
    {
      get
      {
        return DesignerProperties.GetIsInDesignMode((DependencyObject) this);
      }
    }

    bool? IDataProviderOwner.AllowLiveDataShaping
    {
      get
      {
        return this.AllowLiveDataShaping;
      }
    }

    NewItemRowPosition IDataProviderOwner.NewItemRowPosition
    {
      get
      {
        TableView tableView = this.View as TableView;
        if (tableView != null)
          return tableView.ActualNewItemRowPosition;
        return NewItemRowPosition.None;
      }
    }

    bool IDataProviderOwner.ShowGroupSummaryFooter
    {
      get
      {
        return this.viewCore.ShowGroupSummaryFooter;
      }
    }

    Type IDataProviderOwner.ItemType
    {
      get
      {
        return this.DataProviderBase.ItemType;
      }
    }

    bool IDataControllerVisualClient.IsInitializing
    {
      get
      {
        return false;
      }
    }

    int IDataControllerVisualClient.PageRowCount
    {
      get
      {
        if (this.viewCore == null)
          return this.DataProviderBase.VisibleCount;
        return this.viewCore.PageVisibleDataRowCount;
      }
    }

    int IDataControllerVisualClient.VisibleRowCount
    {
      get
      {
        return -1;
      }
    }

    int IDataControllerVisualClient.TopRowIndex
    {
      get
      {
        if (this.viewCore == null)
          return 0;
        return this.viewCore.PageVisibleTopRowIndex;
      }
    }

    internal override bool BottomRowBelowOldVisibleRowCount
    {
      get
      {
        return ((IDataControllerVisualClient) this).TopRowIndex + ((IDataControllerVisualClient) this).PageRowCount >= this.View.RootNodeContainer.oldVisibleRowCount;
      }
    }

    protected internal override BandsLayoutBase BandsLayoutCore
    {
      get
      {
        return (BandsLayoutBase) this.BandsLayout;
      }
      set
      {
        this.BandsLayout = (GridControlBandsLayout) value;
      }
    }

    /// <summary>
    ///                 <para>Provides access to the grid's band collection.
    /// </para>
    ///             </summary>
    /// <value>An observable collection of bands within the grid.</value>
    [DevExpressXpfGridLocalizedDescription("GridControlBands")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [Category("Data")]
    [XtraSerializableProperty(true, true, true)]
    [GridStoreAlwaysProperty]
    public ObservableCollectionCore<GridControlBand> Bands
    {
      get
      {
        return (ObservableCollectionCore<GridControlBand>) this.BandsCore;
      }
    }

    /// <summary>
    ///                 <para>Occurs after async data loading has been started.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler AsyncOperationStarted
    {
      add
      {
        this.AddHandler(GridControl.AsyncOperationStartedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.AsyncOperationStartedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after async data loading is complete.
    /// 
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler AsyncOperationCompleted
    {
      add
      {
        this.AddHandler(GridControl.AsyncOperationCompletedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.AsyncOperationCompletedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables data to be supplied to unbound columns.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event GridColumnDataEventHandler CustomUnboundColumnData
    {
      add
      {
        this.customUnboundColumnData += value;
      }
      remove
      {
        this.customUnboundColumnData -= value;
      }
    }

    /// <summary>
    ///                 <para>Allows you to replace a filter applied with another filter.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event EventHandler<SubstituteFilterEventArgs> SubstituteFilter
    {
      add
      {
        this.substituteFilter += value;
      }
      remove
      {
        this.substituteFilter -= value;
      }
    }

    /// <summary>
    ///                 <para>For internal use only.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event EventHandler<SubstituteSortInfoEventArgs> SubstituteSortInfo
    {
      add
      {
        this.substituteSortInfo += value;
      }
      remove
      {
        this.substituteSortInfo -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to calculate summary values manually.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event CustomSummaryEventHandler CustomSummary
    {
      add
      {
        this.customSummary += value;
      }
      remove
      {
        this.customSummary -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify which summaries should be calculated and displayed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event CustomSummaryExistEventHandler CustomSummaryExists
    {
      add
      {
        this.customSummaryExists += value;
      }
      remove
      {
        this.customSummaryExists -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to sort data using custom rules.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event CustomColumnSortEventHandler CustomColumnSort
    {
      add
      {
        this.customColumnSort += value;
      }
      remove
      {
        this.customColumnSort -= value;
      }
    }

    /// <summary>
    ///                 <para>Provides the ability to group data using custom rules.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event CustomColumnSortEventHandler CustomColumnGroup
    {
      add
      {
        this.customColumnGroup += value;
      }
      remove
      {
        this.customColumnGroup -= value;
      }
    }

    /// <summary>
    ///                 <para>Enables you to filter data rows using custom rules.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RowFilterEventHandler CustomRowFilter
    {
      add
      {
        this.customRowFilter += value;
        if (this.IsLoading)
          return;
        this.RefreshData();
      }
      remove
      {
        this.customRowFilter -= value;
      }
    }

    /// <summary>
    ///                 <para>Occurs before a sorting operation is started.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler StartSorting
    {
      add
      {
        this.AddHandler(GridControl.StartSortingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.StartSortingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a sorting operation has been completed.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler EndSorting
    {
      add
      {
        this.AddHandler(GridControl.EndSortingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.EndSortingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs before a grouping operation is started. This is a routed event.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler StartGrouping
    {
      add
      {
        this.AddHandler(GridControl.StartGroupingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.StartGroupingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after the grouping operation has been completed. This is a routed event.
    /// </para>
    ///             </summary>
    [Category("Events")]
    public event RoutedEventHandler EndGrouping
    {
      add
      {
        this.AddHandler(GridControl.EndGroupingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.EndGroupingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when a group row is about to be expanded, allowing cancellation of the action.
    /// 
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowAllowEventHandler GroupRowExpanding
    {
      add
      {
        this.AddHandler(GridControl.GroupRowExpandingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.GroupRowExpandingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a group row has been expanded.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler GroupRowExpanded
    {
      add
      {
        this.AddHandler(GridControl.GroupRowExpandedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.GroupRowExpandedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs when a group row is about to be collapsed, allowing cancellation of the action.
    /// 
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowAllowEventHandler GroupRowCollapsing
    {
      add
      {
        this.AddHandler(GridControl.GroupRowCollapsingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.GroupRowCollapsingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after a group row has been collapsed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler GroupRowCollapsed
    {
      add
      {
        this.AddHandler(GridControl.GroupRowCollapsedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.GroupRowCollapsedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify whether a master row may be expanded.
    /// 
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowAllowEventHandler MasterRowExpanding
    {
      add
      {
        this.AddHandler(GridControl.MasterRowExpandingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.MasterRowExpandingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to specify whether a master row may be collapsed.
    /// 
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowAllowEventHandler MasterRowCollapsing
    {
      add
      {
        this.AddHandler(GridControl.MasterRowCollapsingEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.MasterRowCollapsingEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires immediately after a master row has been expanded.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler MasterRowExpanded
    {
      add
      {
        this.AddHandler(GridControl.MasterRowExpandedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.MasterRowExpandedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Fires immediately after a master row has been collapsed.
    /// </para>
    ///             </summary>
    [Category("Behavior")]
    public event RowEventHandler MasterRowCollapsed
    {
      add
      {
        this.AddHandler(GridControl.MasterRowCollapsedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.MasterRowCollapsedEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables you to provide custom content for group rows.
    /// </para>
    ///             </summary>
    [Category("Data")]
    public event CustomGroupDisplayTextEventHandler CustomGroupDisplayText
    {
      add
      {
        this.AddHandler(GridControl.CustomGroupDisplayTextEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.CustomGroupDisplayTextEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Enables custom display text to be provided for any data cell.
    /// </para>
    ///             </summary>
    [Category("Data")]
    public event CustomColumnDisplayTextEventHandler CustomColumnDisplayText;

    /// <summary>
    ///                 <para>Occurs when grid data is copied to the clipboard, allowing you to manually copy required data.
    /// </para>
    ///             </summary>
    [Category("Options Copy")]
    public event CopyingToClipboardEventHandler CopyingToClipboard
    {
      add
      {
        this.AddHandler(GridControl.CopyingToClipboardEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.CopyingToClipboardEvent, (Delegate) value);
      }
    }

    /// <summary>
    ///                 <para>Occurs after grid's selection has been changed.
    /// </para>
    ///             </summary>
    [Category("Options Selection")]
    public event GridSelectionChangedEventHandler SelectionChanged
    {
      add
      {
        this.AddHandler(GridControl.SelectionChangedEvent, (Delegate) value);
      }
      remove
      {
        this.RemoveHandler(GridControl.SelectionChangedEvent, (Delegate) value);
      }
    }

    static GridControl()
    {
      DevExpress.Xpf.About.CheckLicenseShowNagScreen(typeof (GridControl));
      Type type = typeof (GridControl);
      GridControl.DataSourceProperty = DependencyPropertyManager.Register("DataSource", typeof (object), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataControlBase) d).ItemsSource = e.NewValue)));
      GridControl.ViewProperty = DependencyPropertyManager.Register("View", typeof (DataViewBase), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(DataControlBase.OnViewChanged), new CoerceValueCallback(DataControlBase.OnCoerceView)));
      GridControl.AutoExpandAllGroupsProperty = DependencyPropertyManager.Register("AutoExpandAllGroups", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridControl) d).OnAutoExpandAllGroupsChanged())));
      GridControl.IsRecursiveExpandProperty = DependencyPropertyManager.Register("IsRecursiveExpand", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      GridControl.AllowLiveDataShapingProperty = DependencyPropertyManager.Register("AllowLiveDataShaping", typeof (bool?), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridControl) d).OnAllowLiveDataShapingChanged())));
      GridControl.IsGroupedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsGrouped", typeof (bool), type, new PropertyMetadata((object) false));
      GridControl.IsGroupedProperty = GridControl.IsGroupedPropertyKey.DependencyProperty;
      GridControl.ActualGroupCountPropertyKey = DependencyPropertyManager.RegisterReadOnly("ActualGroupCount", typeof (int), type, new PropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((GridControl) d).OnActualGroupCountChanged())));
      GridControl.ActualGroupCountProperty = GridControl.ActualGroupCountPropertyKey.DependencyProperty;
      GridControl.StartSortingEvent = EventManager.RegisterRoutedEvent("StartSorting", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.EndSortingEvent = EventManager.RegisterRoutedEvent("EndSorting", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.StartGroupingEvent = EventManager.RegisterRoutedEvent("StartGrouping", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.EndGroupingEvent = EventManager.RegisterRoutedEvent("EndGrouping", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.GroupRowExpandingEvent = EventManager.RegisterRoutedEvent("GroupRowExpanding", RoutingStrategy.Direct, typeof (RowAllowEventHandler), type);
      GridControl.GroupRowExpandedEvent = EventManager.RegisterRoutedEvent("GroupRowExpanded", RoutingStrategy.Direct, typeof (RowEventHandler), type);
      GridControl.GroupRowCollapsingEvent = EventManager.RegisterRoutedEvent("GroupRowCollapsing", RoutingStrategy.Direct, typeof (RowAllowEventHandler), type);
      GridControl.GroupRowCollapsedEvent = EventManager.RegisterRoutedEvent("GroupRowCollapsed", RoutingStrategy.Direct, typeof (RowEventHandler), type);
      GridControl.MasterRowExpandingEvent = EventManager.RegisterRoutedEvent("MasterRowExpanding", RoutingStrategy.Direct, typeof (RowAllowEventHandler), type);
      GridControl.MasterRowCollapsingEvent = EventManager.RegisterRoutedEvent("MasterRowCollapsing", RoutingStrategy.Direct, typeof (RowAllowEventHandler), type);
      GridControl.MasterRowExpandedEvent = EventManager.RegisterRoutedEvent("MasterRowExpanded", RoutingStrategy.Direct, typeof (RowEventHandler), type);
      GridControl.MasterRowCollapsedEvent = EventManager.RegisterRoutedEvent("MasterRowCollapsed", RoutingStrategy.Direct, typeof (RowEventHandler), type);
      GridControl.CustomGroupDisplayTextEvent = EventManager.RegisterRoutedEvent("CustomGroupDisplayText", RoutingStrategy.Direct, typeof (CustomGroupDisplayTextEventHandler), type);
      GridControl.IsAsyncOperationInProgressPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsAsyncOperationInProgress", typeof (bool), type, new PropertyMetadata((object) false, new PropertyChangedCallback(GridControl.OnIsAsyncOperationInProgressChanged)));
      GridControl.IsAsyncOperationInProgressProperty = GridControl.IsAsyncOperationInProgressPropertyKey.DependencyProperty;
      GridControl.GroupSummaryGeneratorTemplateProperty = DependencyProperty.Register("GroupSummaryGeneratorTemplate", typeof (DataTemplate), type, new PropertyMetadata(new PropertyChangedCallback(GridControl.OnGroupSummaryItemsGeneratorTemplatePropertyChanged)));
      GridControl.GroupSummaryItemsAttachedBehaviorProperty = DependencyProperty.Register("GroupSummaryItemsAttachedBehavior", typeof (ItemsAttachedBehaviorCore<DataControlBase, SummaryItemBase>), type, new PropertyMetadata((PropertyChangedCallback) null));
      GridControl.GroupSummarySourceProperty = DependencyProperty.Register("GroupSummarySource", typeof (IEnumerable), type, new PropertyMetadata((PropertyChangedCallback) ((d, e) => ItemsAttachedBehaviorCore<DataControlBase, SummaryItemBase>.OnItemsSourcePropertyChanged(d, e, GridControl.GroupSummaryItemsAttachedBehaviorProperty, GridControl.GroupSummaryGeneratorTemplateProperty, (DependencyProperty) null, (DependencyProperty) null, (Func<DataControlBase, IList>) (grid => (IList) grid.GroupSummaryCore), (Func<DataControlBase, SummaryItemBase>) (grid => grid.CreateSummaryItem()), (Action<int, object>) null, (Action<SummaryItemBase>) null, (ISupportInitialize) null, (Action<SummaryItemBase, object>) null, true, true, (Func<SummaryItemBase, bool>) null))));
      GridControl.DetailDescriptorProperty = DependencyPropertyManager.Register("DetailDescriptor", typeof (DetailDescriptorBase), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataControlBase) d).OnDetailDescriptorChanged((DetailDescriptorBase) e.OldValue))));
      GridControl.AsyncOperationStartedEvent = EventManager.RegisterRoutedEvent("AsyncOperationStarted", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.AsyncOperationCompletedEvent = EventManager.RegisterRoutedEvent("AsyncOperationCompleted", RoutingStrategy.Direct, typeof (RoutedEventHandler), type);
      GridControl.BandsLayoutPropertyKey = DependencyPropertyManager.RegisterReadOnly("BandsLayout", typeof (GridControlBandsLayout), type, new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((DataControlBase) d).OnBandsLayoutChanged(e.OldValue as BandsLayoutBase, e.NewValue as BandsLayoutBase))));
      GridControl.BandsLayoutProperty = GridControl.BandsLayoutPropertyKey.DependencyProperty;
      GridControl.OptimizeSummaryCalculationProperty = DependencyPropertyManager.Register("OptimizeSummaryCalculation", typeof (bool), type, new PropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((GridControl) d).OnOptimizeSummaryCalculationChanged())));
      GridControl.CopyingToClipboardEvent = EventManager.RegisterRoutedEvent("CopyingToClipboard", RoutingStrategy.Direct, typeof (CopyingToClipboardEventHandler), type);
      GridControl.SelectionChangedEvent = EventManager.RegisterRoutedEvent("SelectionChanged", RoutingStrategy.Direct, typeof (GridSelectionChangedEventHandler), type);
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.SelectAll, (ExecutedRoutedEventHandler) ((d, e) => ((DataControlBase) d).SelectAllMasterDetail()), (CanExecuteRoutedEventHandler) ((d, e) => ((GridControl) d).CanSelectAll(d, e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.Copy, (ExecutedRoutedEventHandler) ((d, e) => ((GridControl) d).Copy()), (CanExecuteRoutedEventHandler) ((d, e) => ((GridControl) d).CanCopy(d, e))));
      CommandManager.RegisterClassCommandBinding(type, new CommandBinding((ICommand) ApplicationCommands.Paste, (ExecutedRoutedEventHandler) ((d, e) => ((GridControl) d).Paste()), (CanExecuteRoutedEventHandler) ((d, e) => ((GridControl) d).CanPaste(d, e))));
      DXSerializer.SerializationProviderProperty.OverrideMetadata(type, (PropertyMetadata) new UIPropertyMetadata((object) new GridControlSerializationProvider()));
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridControl class with default settings.
    /// </para>
    ///             </summary>
    public GridControl()
      : this((IDataControlOriginationElement) null)
    {
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridControl class.
    /// </para>
    ///             </summary>
    /// <param name="dataControlOriginationElement">
    /// An object that implements the <see cref="T:DevExpress.Xpf.Grid.Native.IDataControlOriginationElement" /> interface.
    /// 
    ///           </param>
    public GridControl(IDataControlOriginationElement dataControlOriginationElement)
      : base(dataControlOriginationElement)
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (GridControl));
      this.GroupSummarySortInfo.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnGroupSummarySortInfoChanged);
      this.GroupSummary.CollectionChanged += new NotifyCollectionChangedEventHandler(((DataControlBase) this).OnGroupSummaryCollectionChanged);
      this.visualClientUpdater = new GridControl.VisualClientUpdater(this);
    }

    void IAddChild.AddChild(object value)
    {
    }

    void IAddChild.AddText(string text)
    {
    }

    private static void ProcessServiceSummary(GridControl grid, CustomSummaryEventArgs e, ServiceSummaryItem serviceSummaryItem)
    {
      switch (serviceSummaryItem.CustomServiceSummaryItemType.Value)
      {
        case CustomServiceSummaryItemType.DateTimeAverage:
          GridControl.CustomDateTimeAverageSummary(grid, serviceSummaryItem, e);
          break;
        case CustomServiceSummaryItemType.SortedList:
          GridControl.CustomSortedListSummary(grid, serviceSummaryItem, e);
          break;
        case CustomServiceSummaryItemType.Unique:
        case CustomServiceSummaryItemType.Duplicate:
          GridControl.CustomUniqueDuplicateSummary(grid, serviceSummaryItem, e);
          break;
      }
    }

    private static void CustomSortedListSummary(GridControl grid, ServiceSummaryItem serviceSummaryItem, CustomSummaryEventArgs e)
    {
      if (e.SummaryProcess != CustomSummaryProcess.Start)
        return;
      DataColumnInfo column = grid.DataController.Columns[serviceSummaryItem.FieldName];
      if (column != null && (typeof (IComparable).IsAssignableFrom(column.GetDataType()) || column.UnboundWithExpression))
      {
        int[] indices = GridControl.FilterSortedListIndicesForNullValues(GridControl.GetSortedListIndices(grid, column), column, (DevExpress.Xpf.Data.GridDataProvider) grid.DataProviderBase);
        e.TotalValue = (object) new SortedIndices(indices);
      }
      e.TotalValueReady = true;
    }

    private static int[] GetSortedListIndices(GridControl grid, DataColumnInfo column)
    {
      VisibleListSourceRowCollection sourceRowCollection = GridControl.GetVisibleListSourceRowCollection(grid);
      sourceRowCollection.SortRows(new DataColumnSortInfo[1]
      {
        new DataColumnSortInfo(column)
      });
      return sourceRowCollection.ToArray();
    }

    private static VisibleListSourceRowCollection GetVisibleListSourceRowCollection(GridControl grid)
    {
      return grid.DataController.GroupInfo.VisibleListSourceRows.CloneThatWouldBeForSureModifiedAndOrForgottenBeforeAnythingHappensToOriginal();
    }

    private static int[] FilterSortedListIndicesForNullValues(int[] sortedListIndices, DataColumnInfo column, DevExpress.Xpf.Data.GridDataProvider provider)
    {
      if (sortedListIndices.Length == 0)
        return sortedListIndices;
      Type type = column.Type;
      if (type.IsValueType && Nullable.GetUnderlyingType(type) == (Type) null && !column.UnboundWithExpression)
        return sortedListIndices;
      object valueByListIndex = provider.GetRowValueByListIndex(sortedListIndices[0], column);
      if (valueByListIndex == null)
      {
        int sourceIndex = -1;
        for (int index = 1; index < sortedListIndices.Length; ++index)
        {
          if (provider.GetRowValueByListIndex(sortedListIndices[index], column) != null)
          {
            sourceIndex = index;
            break;
          }
        }
        if (sourceIndex == -1)
          return new int[0];
        int length = sortedListIndices.Length - sourceIndex;
        int[] numArray = new int[length];
        Array.Copy((Array) sortedListIndices, sourceIndex, (Array) numArray, 0, length);
        return numArray;
      }
      if (provider.ValueComparer.Compare(valueByListIndex, (object) null) <= 0)
        return ((IEnumerable<int>) sortedListIndices).Where<int>((Func<int, bool>) (x => provider.GetRowValueByListIndex(x, column) != null)).ToArray<int>();
      return sortedListIndices;
    }

    private static void CustomDateTimeAverageSummary(GridControl grid, ServiceSummaryItem serviceSummaryItem, CustomSummaryEventArgs e)
    {
      if (e.SummaryProcess == CustomSummaryProcess.Start)
        e.TotalValue = (object) new Tuple<Decimal, int>(new Decimal(0), 0);
      if (e.SummaryProcess == CustomSummaryProcess.Calculate)
      {
        Tuple<Decimal, int> tuple = (Tuple<Decimal, int>) e.TotalValue;
        e.TotalValue = (object) new Tuple<Decimal, int>(tuple.Item1 + (Decimal) ((DateTime) e.FieldValue).Ticks, tuple.Item2 + 1);
      }
      if (e.SummaryProcess != CustomSummaryProcess.Finalize)
        return;
      Tuple<Decimal, int> tuple1 = (Tuple<Decimal, int>) e.TotalValue;
      e.TotalValue = (object) (tuple1.Item1 / (Decimal) tuple1.Item2);
    }

    private static void CustomUniqueDuplicateSummary(GridControl grid, ServiceSummaryItem serviceSummaryItem, CustomSummaryEventArgs e)
    {
      if (e.SummaryProcess != CustomSummaryProcess.Start)
        return;
      VisibleListSourceRowCollection sourceRowCollection = GridControl.GetVisibleListSourceRowCollection(grid);
      DevExpress.Xpf.Data.GridDataProvider gridDataProvider = (DevExpress.Xpf.Data.GridDataProvider) grid.DataProviderBase;
      DataColumnInfo info = grid.DataController.Columns[serviceSummaryItem.FieldName];
      if (info != null)
      {
        CustomServiceSummaryItemType? serviceSummaryItemType = serviceSummaryItem.CustomServiceSummaryItemType;
        UniqueDuplicateSummaryCalculator summaryCalculator = new UniqueDuplicateSummaryCalculator((serviceSummaryItemType.GetValueOrDefault() != CustomServiceSummaryItemType.Unique ? 0 : (serviceSummaryItemType.HasValue ? 1 : 0)) != 0 ? UniqueDuplicateRule.Unique : UniqueDuplicateRule.Duplicate);
        for (int visibleRow = 0; visibleRow < sourceRowCollection.VisibleRowCount; ++visibleRow)
          summaryCalculator.Calculate(gridDataProvider.GetRowValueByListIndex(sourceRowCollection.GetListSourceRow(visibleRow), info));
        e.TotalValue = (object) summaryCalculator.Finish();
      }
      e.TotalValueReady = true;
    }

    private static void OnGroupSummaryItemsGeneratorTemplatePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ItemsAttachedBehaviorCore<DataControlBase, SummaryItemBase>.OnItemsGeneratorTemplatePropertyChanged(d, e, GridControl.GroupSummaryItemsAttachedBehaviorProperty);
    }

    private static void OnIsAsyncOperationInProgressChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((GridControl) d).OnIsAsyncOperationInProgressChanged((bool) e.NewValue);
    }

    internal static GridViewBase CreateDefaultGridView()
    {
      return (GridViewBase) new TableView();
    }

    private void OnGroupSummarySortInfoChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.OnSortInfoChanged(sender, e);
      this.GetOriginationDataControl().syncPropertyLocker.DoLockedActionIfNotLocked((Action) (() => DataControlOriginationElementHelper.EnumerateDependentElements<GridControl>((DataControlBase) this, (Func<DataControlBase, GridControl>) (grid => (GridControl) grid), (Action<GridControl>) (grid => this.CloneGroupSummarySortInfo((DataControlBase) grid)), (Action<GridControl>) null)));
    }

    protected internal override void SetIsGrouped(bool value)
    {
      this.SetValue(GridControl.IsGroupedPropertyKey, (object) value);
      if (this.View == null)
        return;
      this.View.UpdateMasterDetailViewProperties();
    }

    protected virtual void RaiseCustomUnboundColumnData(GridColumnDataEventArgs e)
    {
      this.GetOriginationGridControl().RaiseCustomUnboundColumnDataCore(e);
    }

    private void RaiseCustomUnboundColumnDataCore(GridColumnDataEventArgs e)
    {
      if (this.customUnboundColumnData == null)
        return;
      this.customUnboundColumnData((object) this, e);
    }

    protected virtual void RaiseSubstituteFilter(SubstituteFilterEventArgs e)
    {
      this.GetOriginationGridControl().RaiseSubstituteFilterCore(e);
    }

    private void RaiseSubstituteFilterCore(SubstituteFilterEventArgs e)
    {
      if (this.substituteFilter == null)
        return;
      this.substituteFilter((object) this, e);
    }

    protected virtual void RaiseSubstituteSortInfo(SubstituteSortInfoEventArgs e)
    {
      this.GetOriginationGridControl().RaiseSubstituteSortInfoCore(e);
    }

    private void RaiseSubstituteSortInfoCore(SubstituteSortInfoEventArgs e)
    {
      if (this.substituteSortInfo == null)
        return;
      this.substituteSortInfo((object) this, e);
    }

    protected virtual void RaiseCustomSummaryExists(object sender, CustomSummaryExistEventArgs e)
    {
      this.GetOriginationGridControl().RaiseCustomSummaryExistsCore(sender, e);
    }

    private void RaiseCustomSummaryExistsCore(object sender, CustomSummaryExistEventArgs e)
    {
      if (this.customSummaryExists == null)
        return;
      this.customSummaryExists(sender, e);
    }

    protected virtual void RaiseCustomSummary(object sender, CustomSummaryEventArgs e)
    {
      ServiceSummaryItem serviceSummaryItem = e.Item as ServiceSummaryItem;
      if (serviceSummaryItem != null && serviceSummaryItem.SummaryType == SummaryItemType.Custom)
        GridControl.ProcessServiceSummary(this, e, serviceSummaryItem);
      else
        this.GetOriginationGridControl().RaiseCustomSummaryCore(sender, e);
    }

    private void RaiseCustomSummaryCore(object sender, CustomSummaryEventArgs e)
    {
      if (this.customSummary == null)
        return;
      this.customSummary(sender, e);
    }

    protected internal virtual void RaiseCustomColumnSort(CustomColumnSortEventArgs e)
    {
      this.GetOriginationGridControl().RaiseCustomColumnSortCore(e);
    }

    protected internal override bool RaiseCopyingToClipboard(CopyingToClipboardEventArgsBase e)
    {
      e.RoutedEvent = GridControl.CopyingToClipboardEvent;
      this.RaiseEventInOriginationGrid((RoutedEventArgs) e);
      return e.Handled;
    }

    protected internal override void RaiseSelectionChanged(GridSelectionChangedEventArgs e)
    {
      e.RoutedEvent = GridControl.SelectionChangedEvent;
      this.RaiseEventInOriginationGrid((RoutedEventArgs) e);
    }

    private void RaiseCustomColumnSortCore(CustomColumnSortEventArgs e)
    {
      if (this.customColumnSort == null)
        return;
      this.customColumnSort((object) this, e);
    }

    protected internal virtual void RaiseCustomColumnGroup(CustomColumnSortEventArgs e)
    {
      this.GetOriginationGridControl().RaiseCustomColumnGroupCore(e);
    }

    private void RaiseCustomColumnGroupCore(CustomColumnSortEventArgs e)
    {
      if (this.customColumnGroup == null)
        return;
      this.customColumnGroup((object) this, e);
    }

    protected virtual int RaiseCustomRowFilter(int listSourceRowIndex, bool fit)
    {
      RowFilterEventArgs e = new RowFilterEventArgs(this, listSourceRowIndex, fit);
      this.GetOriginationGridControl().RaiseCustomRowFilterCore(e);
      if (!e.Handled)
        return -1;
      return !e.Visible ? 0 : 1;
    }

    private void RaiseCustomRowFilterCore(RowFilterEventArgs e)
    {
      if (this.customRowFilter == null)
        return;
      this.customRowFilter((object) this, e);
    }

    internal override SortInfoCollectionBase CreateSortInfo()
    {
      return (SortInfoCollectionBase) new GridSortInfoCollection();
    }

    internal override ISummaryItemOwner CreateSummariesCollection(SummaryItemCollectionType collectionType)
    {
      return (ISummaryItemOwner) new GridSummaryItemCollection((DataControlBase) this, collectionType);
    }

    internal override IColumnCollection CreateColumns()
    {
      return (IColumnCollection) new GridColumnCollection(this);
    }

    protected override DataProviderBase CreateDataProvider()
    {
      return (DataProviderBase) new DevExpress.Xpf.Data.GridDataProvider((IDataProviderOwner) this);
    }

    protected internal override DataViewBase CreateDefaultView()
    {
      return (DataViewBase) GridControl.CreateDefaultGridView();
    }

    internal override void ValidateDataProvider(DataViewBase newValue)
    {
      if (this.View is TreeListView)
      {
        this.fDataProvider = this.View.DataProviderBase;
      }
      else
      {
        if (!(this.DataProviderBase is TreeListDataProvider))
          return;
        this.fDataProvider = this.CreateDataProvider();
      }
    }

    protected internal override BandBase CreateBand()
    {
      return (BandBase) new GridControlBand();
    }

    protected internal override IList<SummaryItemBase> GetGroupSummaries()
    {
      return (IList<SummaryItemBase>) new SimpleBridgeList<SummaryItemBase, GridSummaryItem>((IList<GridSummaryItem>) this.GroupSummary, (Func<GridSummaryItem, SummaryItemBase>) (item => (SummaryItemBase) item), (Func<SummaryItemBase, GridSummaryItem>) null);
    }

    internal override SummaryItemBase CreateSummaryItem()
    {
      return (SummaryItemBase) new GridSummaryItem();
    }

    protected override void OnItemsSourceChanged(object oldValue, object newValue)
    {
      base.OnItemsSourceChanged(oldValue, newValue);
      if (!(this.View is GridViewBase))
        return;
      ((GridViewBase) this.View).RefreshImmediateUpdateRowPositionProperty();
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
      if (this.AutomationPeer == null)
        this.AutomationPeer = (DataControlAutomationPeer) new GridControlAutomationPeer(this);
      return (AutomationPeer) this.AutomationPeer;
    }

    protected override PeerCacheBase CreatePeerCache()
    {
      return (PeerCacheBase) new PeerCache();
    }

    protected internal void ChangeGroupExpanded(int visibleIndex)
    {
      this.ChangeGroupExpandedCore(this.GetRowHandleByVisibleIndex(visibleIndex), false);
    }

    protected internal void ExpandGroupRowWithEvents(int rowHandle, bool recursive)
    {
      if (!this.RaiseGroupRowExpanding(rowHandle))
        return;
      this.ChangeGroupExpandedCore(rowHandle, recursive);
      this.RaiseGroupRowExpanded(rowHandle);
    }

    protected internal void CollapseGroupRowWithEvents(int rowHandle, bool recursive)
    {
      if (!this.RaiseGroupRowCollapsing(rowHandle))
        return;
      this.ChangeGroupExpandedCore(rowHandle, recursive);
      this.OnGroupRowCollapsed(rowHandle);
    }

    private void ScrollBarAnnotationsGeneration()
    {
      ITableView tableView = this.View as ITableView;
      if (tableView == null)
        return;
      tableView.ScrollBarAnnotationsManager.ScrollBarAnnotationGeneration();
    }

    protected internal void ChangeGroupExpandedCore(int rowHandle, bool recursive)
    {
      this.PurgeCache(this.GetRowVisibleIndexByHandle(rowHandle));
      this.View.SupressCacheCleanCountLocker.DoLockedAction((Action) (() =>
      {
        this.DataProviderBase.ChangeGroupExpanded(rowHandle, recursive);
        this.ScrollBarAnnotationsGeneration();
      }));
    }

    private void PurgeCache(int visibleIndex)
    {
      if (!this.DataProviderBase.IsServerMode)
        return;
      List<int> intList = new List<int>();
      foreach (KeyValuePair<int, DataRowNode> node in this.DataView.Nodes)
      {
        if (this.GetRowVisibleIndexByHandleCore(node.Key) > visibleIndex)
          intList.Add(node.Key);
      }
      foreach (int key in intList)
        this.DataView.Nodes.Remove(key);
    }

    /// <summary>
    ///                 <para>Updates group summaries.
    /// </para>
    ///             </summary>
    public void UpdateGroupSummary()
    {
      this.DataProviderBase.UpdateGroupSummary();
      if (this.View == null)
        return;
      this.View.UpdateGroupSummary();
    }

    internal override void ReassignGroupedColumns(List<ColumnBase> groupedColumnsList)
    {
      if (this.groupedColumns == null)
        this.groupedColumns = new ObservableCollectionCore<GridColumn>();
      IList<GridColumn> gridColumnList = (IList<GridColumn>) new SimpleBridgeList<GridColumn, ColumnBase>((IList<ColumnBase>) groupedColumnsList, (Func<ColumnBase, GridColumn>) null, (Func<GridColumn, ColumnBase>) null);
      if (ListHelper.AreEqual<GridColumn>((IList<GridColumn>) this.groupedColumns, gridColumnList))
        return;
      this.needsDataReset = true;
      this.groupedColumns.Assign(gridColumnList);
      this.DesignTimeGridAdorner.OnColumnsLayoutChanged();
    }

    internal override void SyncSortBySummaryInfo()
    {
      foreach (ColumnBase column in (Collection<GridColumn>) this.Columns)
        column.IsSortedBySummary = false;
      foreach (GridGroupSummarySortInfo groupSummarySortInfo in (Collection<GridGroupSummarySortInfo>) this.GroupSummarySortInfo)
      {
        GridColumn gridColumn = this.Columns[groupSummarySortInfo.FieldName];
        if (gridColumn != null)
        {
          gridColumn.IsSortedBySummary = true;
          gridColumn.SortOrder = GridSortInfo.GetColumnSortOrder(groupSummarySortInfo.SortOrder);
        }
      }
    }

    internal void ApplyColumnGroupIndex(ColumnBase column)
    {
      this.ApplyGroupSortIndexIfNotLoading(column, new Action<ColumnBase>(this.ApplyColumnGroupIndexWithoutLoadingCheck), (Action<ColumnBase>) null);
    }

    private void ApplyColumnGroupIndexWithoutLoadingCheck(ColumnBase column)
    {
      this.ApplyGroupSortIndexCore(column, (Action<ColumnBase>) (col => this.GroupBy((GridColumn) col, DataControlBase.GetActualSortOrder(col.SortOrder), ((GridColumn) col).GroupIndex)));
    }

    internal override void CroupByCore(ColumnBase column)
    {
      this.GroupBy((GridColumn) column, column.SortOrder, column.GroupIndexCore);
    }

    internal override void ApplyGroupSortIndexIfNotLoadingCore(ColumnBase column)
    {
      this.ApplyGroupSortIndexIfNotLoading(column, new Action<ColumnBase>(this.ApplyColumnGroupIndex), (Action<ColumnBase>) null);
    }

    protected override void RebuildGroupedColumnsInfo(List<ColumnBase> groupedColumns)
    {
      base.RebuildGroupedColumnsInfo(groupedColumns);
      groupedColumns.Sort((Comparison<ColumnBase>) ((column1, column2) => Comparer<int>.Default.Compare(column1.GroupIndexCore, column2.GroupIndexCore)));
      groupedColumns.ForEach(new Action<ColumnBase>(this.ApplyColumnGroupIndexWithoutLoadingCheck));
    }

    private void UpdateInvalidGroupCache()
    {
      this.InvalidGroupCache.Clear();
      if (this.DataProviderBase.CollectionViewSource == null || this.DataProviderBase.CollectionViewSource.GroupDescriptions == null)
        return;
      foreach (GroupDescription groupDescription1 in (Collection<GroupDescription>) this.DataProviderBase.CollectionViewSource.GroupDescriptions)
      {
        PropertyGroupDescription groupDescription2 = groupDescription1 as PropertyGroupDescription;
        if (groupDescription2 != null && this.Columns[groupDescription2.PropertyName] == null)
          this.InvalidGroupCache.Add(groupDescription2.PropertyName, groupDescription2);
      }
    }

    protected void Copy()
    {
      if (this.DataView.MasterRootRowsContainer.FocusedView.ClipboardMode == ClipboardMode.Formatted)
        this.DataView.MasterRootRowsContainer.FocusedView.Do<DataViewBase>((Action<DataViewBase>) (dataView => dataView.SelectionStrategy.CopyMasterDetailToClipboard()));
      else
        this.DataView.Do<DataViewBase>((Action<DataViewBase>) (dataView => dataView.SelectionStrategy.CopyMasterDetailToClipboard()));
    }

    internal void CopyAllSelectedItemsToClipboard()
    {
      IEnumerable<KeyValuePair<DataControlBase, int>> selectedRows = this.GetSelectedRows();
      if (selectedRows.Count<KeyValuePair<DataControlBase, int>>() > 0)
      {
        this.DataView.Do<DataViewBase>((Action<DataViewBase>) (view => view.CopyAllRowsToClipboardCore(selectedRows)));
      }
      else
      {
        if (this.View.FocusedView.FocusedRowHandle == int.MinValue)
          return;
        KeyValuePair<DataControlBase, int> focusedRow = new KeyValuePair<DataControlBase, int>(this.View.FocusedView.DataControl, this.View.FocusedView.FocusedRowHandle);
        this.DataView.Do<DataViewBase>((Action<DataViewBase>) (view => view.CopyAllRowsToClipboardCore((IEnumerable<KeyValuePair<DataControlBase, int>>) new List<KeyValuePair<DataControlBase, int>>()
        {
          focusedRow
        })));
      }
    }

    internal IEnumerable<KeyValuePair<DataControlBase, int>> GetSelectedRows()
    {
      List<KeyValuePair<DataControlBase, int>> keyValuePairList = new List<KeyValuePair<DataControlBase, int>>();
      for (int visibleIndex = 0; visibleIndex < this.VisibleRowCount; ++visibleIndex)
      {
        int handleByVisibleIndex = this.GetRowHandleByVisibleIndex(visibleIndex);
        if (this.DataView.IsRowSelected(handleByVisibleIndex))
          keyValuePairList.Add(new KeyValuePair<DataControlBase, int>((DataControlBase) this, handleByVisibleIndex));
        if (this.IsMasterRowExpanded(handleByVisibleIndex, (DetailDescriptorBase) null))
        {
          GridControl gridControl = (GridControl) this.GetVisibleDetail(handleByVisibleIndex);
          if (gridControl != null)
            keyValuePairList.AddRange(gridControl.GetSelectedRows());
        }
      }
      return (IEnumerable<KeyValuePair<DataControlBase, int>>) keyValuePairList;
    }

    protected internal void Paste()
    {
      if (this.RaisePastingFromClipboard())
        return;
      this.View.RaisePastingFromClipboard();
    }

    protected void CanCopy(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = this.View.CanCopyRows();
    }

    protected void CanPaste(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    protected void CanSelectAll(object sender, CanExecuteRoutedEventArgs e)
    {
      e.CanExecute = true;
    }

    protected internal override void DestroyFilterData()
    {
      this.DestroyFilterDataInternal();
      this.DestroySearchFilterData();
    }

    private void DestroyFilterDataInternal()
    {
      if (this.filterData == null)
        return;
      this.filterData.Dispose();
      this.filterData = (GridFilterData) null;
    }

    private void DestroySearchFilterData()
    {
      if (this.searchFilterData == null)
        return;
      this.searchFilterData.Dispose();
      this.searchFilterData = (GridFilterData) null;
    }

    /// <summary>
    ///                 <para>Disposes the grid's data controller and releases all the allocated resources.
    /// </para>
    ///             </summary>
    [Obsolete("This member is not intended to be used directly from your code.")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public void ForceClearData()
    {
      if (this.IsUnloaded)
        return;
      this.IsUnloaded = true;
      this.DataProviderBase.ForceClearData();
    }

    internal override bool GetIsExpandButtonVisible()
    {
      TableView tableView = this.View as TableView;
      if (tableView != null)
        return tableView.ActualShowDetailButtons;
      return false;
    }

    internal override void ClearGroupSummarySortInfo()
    {
      this.GroupSummarySortInfo.ClearCore();
    }

    protected virtual void RaiseAsyncOperationBegin()
    {
      RoutedEventArgs e = new RoutedEventArgs() { RoutedEvent = GridControl.AsyncOperationStartedEvent };
      this.CheckIsOriginationDataControl();
      this.RaiseEventInOriginationGrid(e);
    }

    protected virtual void RaiseAsyncOperationEnd()
    {
      RoutedEventArgs e = new RoutedEventArgs() { RoutedEvent = GridControl.AsyncOperationCompletedEvent };
      this.CheckIsOriginationDataControl();
      this.RaiseEventInOriginationGrid(e);
    }

    protected internal virtual bool RaiseGroupRowExpanding(int rowHandle)
    {
      return this.RaiseRowAllowEvent(GridControl.GroupRowExpandingEvent, rowHandle);
    }

    protected internal virtual void RaiseGroupRowExpanded(int rowHandle)
    {
      this.RaiseEventInOriginationGrid((RoutedEventArgs) new RowEventArgs(GridControl.GroupRowExpandedEvent, (GridViewBase) this.View, rowHandle));
    }

    protected internal virtual bool RaiseGroupRowCollapsing(int rowHandle)
    {
      return this.RaiseRowAllowEvent(GridControl.GroupRowCollapsingEvent, rowHandle);
    }

    protected internal virtual void RaiseGroupRowCollapsed(int rowHandle)
    {
      this.RaiseEventInOriginationGrid((RoutedEventArgs) new RowEventArgs(GridControl.GroupRowCollapsedEvent, (GridViewBase) this.View, rowHandle));
    }

    protected internal virtual bool RaiseMasterRowExpanding(int rowHandle)
    {
      return this.RaiseRowAllowEvent(GridControl.MasterRowExpandingEvent, rowHandle);
    }

    protected internal virtual bool RaiseMasterRowCollapsing(int rowHandle)
    {
      return this.RaiseRowAllowEvent(GridControl.MasterRowCollapsingEvent, rowHandle);
    }

    protected internal virtual void RaiseMasterRowExpanded(int rowHandle)
    {
      this.RaiseEventInOriginationGrid((RoutedEventArgs) new RowEventArgs(GridControl.MasterRowExpandedEvent, (GridViewBase) this.View, rowHandle));
    }

    protected internal virtual void RaiseMasterRowCollapsed(int rowHandle)
    {
      this.RaiseEventInOriginationGrid((RoutedEventArgs) new RowEventArgs(GridControl.MasterRowCollapsedEvent, (GridViewBase) this.View, rowHandle));
    }

    protected bool RaiseRowAllowEvent(RoutedEvent routedEvent, int rowHandle)
    {
      if (!(this.View is GridViewBase))
        return false;
      RowAllowEventArgs rowAllowEventArgs = new RowAllowEventArgs(routedEvent, (GridViewBase) this.View, rowHandle);
      this.RaiseEventInOriginationGrid((RoutedEventArgs) rowAllowEventArgs);
      return rowAllowEventArgs.Allow;
    }

    internal void OnGroupRowCollapsed(int rowHandle)
    {
      if (this.View != null && this.View.AllowFixedGroupsCore && this.IsRowVisible(rowHandle))
        this.View.ScrollAnimationLocker.DoLockedAction((Action) (() => this.View.ScrollIntoView(rowHandle)));
      this.RaiseGroupRowCollapsed(rowHandle);
    }

    private void OnIsAsyncOperationInProgressChanged(bool newValue)
    {
      GridViewBase gridViewBase = this.View as GridViewBase;
      if (gridViewBase == null)
        return;
      this.isAsyncOperationInProgress = newValue;
      if (newValue)
      {
        gridViewBase.SetWaitIndicator();
        this.RaiseAsyncOperationBegin();
      }
      else
      {
        gridViewBase.ClearWaitIndicator();
        this.RaiseAsyncOperationEnd();
      }
      this.asyncOpWaitHandle.IsInterrupted = !newValue;
    }

    internal void OnAllowLiveDataShapingChanged()
    {
      if (this.DataProviderBase.IsSelfManagedItemsSource)
        return;
      this.DataProviderBase.OnDataSourceChanged();
      if (this.View == null)
        return;
      if (!this.DataProviderBase.SubscribeRowChangedForVisibleRows)
        this.View.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.UnsubcribePropertyChanged(rowData.Row)), true, true);
      else
        this.View.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.SubcribePropertyChanged(rowData.Row)), true, true);
    }

    private void OnOptimizeSummaryCalculationChanged()
    {
      this.DataProviderBase.OnDataSourceChanged();
    }

    private void OnActualGroupCountChanged()
    {
      this.UpdateAllDetailViewIndents();
    }

    protected virtual void OnAutoExpandAllGroupsChanged()
    {
      if (this.DataProviderBase == null || this.DataProviderBase.AutoExpandAllGroups == this.AutoExpandAllGroups)
        return;
      this.DataProviderBase.AutoExpandAllGroups = this.AutoExpandAllGroups;
      if (!this.AutoExpandAllGroups)
        return;
      this.ExpandAllGroups();
    }

    protected internal virtual string RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string displayText)
    {
      if (this.GetOriginationGridControl().CustomColumnDisplayText == null)
        return displayText;
      if (this.customColumnDisplayTextEventArgs == null)
        this.customColumnDisplayTextEventArgs = new CustomColumnDisplayTextEventArgs();
      this.customColumnDisplayTextEventArgs.SetArgs((GridViewBase) this.View, rowHandle, listSourceIndex, (GridColumn) column, value, displayText);
      this.GetOriginationGridControl().RaiseCustomDisplayTextCore(this.customColumnDisplayTextEventArgs);
      return this.customColumnDisplayTextEventArgs.DisplayText;
    }

    protected internal virtual bool? RaiseCustomDisplayText(int? rowHandle, int? listSourceIndex, ColumnBase column, object value, string originalDisplayText, out string displayText)
    {
      displayText = this.RaiseCustomDisplayText(rowHandle, listSourceIndex, column, value, originalDisplayText);
      if (this.GetOriginationGridControl().CustomColumnDisplayText == null)
        return new bool?(false);
      if (this.customColumnDisplayTextEventArgs.ShowAsNullText)
        return new bool?();
      return new bool?(true);
    }

    private void RaiseCustomDisplayTextCore(CustomColumnDisplayTextEventArgs e)
    {
      this.CustomColumnDisplayText((object) this, e);
    }

    protected internal virtual string RaiseCustomGroupDisplayText(int rowHandle, GridColumn column, object value, string displayText)
    {
      CustomGroupDisplayTextEventArgs displayTextEventArgs = new CustomGroupDisplayTextEventArgs((GridViewBase) this.View, rowHandle, column, value, displayText);
      this.RaiseEventInOriginationGrid((RoutedEventArgs) displayTextEventArgs);
      return displayTextEventArgs.DisplayText;
    }

    void IDataProviderOwner.PopulateColumns()
    {
      this.PopulateColumns(false, true, (DataProviderBase) null);
    }

    void IDataProviderOwner.ValidateMasterDetailConsistency()
    {
      this.ValidateMasterDetailConsistency();
    }

    void IDataProviderOwner.OnSelectionChanged(SelectionChangedEventArgs e)
    {
      this.View.OnSelectionChanged(e);
    }

    List<IColumnInfo> IDataProviderOwner.GetColumns()
    {
      List<IColumnInfo> columnInfoList = new List<IColumnInfo>();
      foreach (GridColumn column in (Collection<GridColumn>) this.Columns)
        columnInfoList.Add((IColumnInfo) column);
      return columnInfoList;
    }

    IEnumerable<IColumnInfo> IDataProviderOwner.GetServiceUnboundColumns()
    {
      return this.viewCore.Return<DataViewBase, IEnumerable<IColumnInfo>>((Func<DataViewBase, IEnumerable<IColumnInfo>>) (x => x.ViewBehavior.GetServiceUnboundColumns()), (Func<IEnumerable<IColumnInfo>>) (() => Enumerable.Empty<IColumnInfo>()));
    }

    IEnumerable<SummaryItemBase> IDataProviderOwner.GetServiceSummaries()
    {
      return this.viewCore.Return<DataViewBase, IEnumerable<SummaryItemBase>>((Func<DataViewBase, IEnumerable<SummaryItemBase>>) (x => (IEnumerable<SummaryItemBase>) x.ViewBehavior.GetServiceSummaries()), (Func<IEnumerable<SummaryItemBase>>) (() => Enumerable.Empty<SummaryItemBase>()));
    }

    void IDataProviderOwner.DoActionWithPostponedUpdateLayout(Action action)
    {
      this.LockUpdateLayout = true;
      action();
      this.LockUpdateLayout = false;
      this.View.EnqueueImmediateAction(new Action(((DataControlBase) this).UpdateLayoutCore));
    }

    void IDataProviderOwner.OnCurrentIndexChanged()
    {
      if (this.View == null || this.View.FocusedRowHandleChangedLocker.IsLocked || !this.View.IsFocusedView)
        return;
      if (this.View.IsNewItemRowHandle(this.View.FocusedRowHandle))
      {
        this.DataProviderBase.CurrentControllerRow = -2147483647;
      }
      else
      {
        if (this.View.IsAutoFilterRowFocused)
          return;
        this.View.SetFocusOnCurrentControllerRow();
      }
    }

    void IDataProviderOwner.OnCurrentIndexChanging(int newControllerRow)
    {
      if (this.View == null || this.View.FocusedRowHandle == newControllerRow || newControllerRow == -2147483647)
        return;
      this.CancelRowEditIfNeeded();
    }

    void IDataProviderOwner.OnCurrentRowChanged()
    {
      if (this.View == null || object.Equals(this.CurrentItem, this.DataProviderBase.GetRowValue(this.View.FocusedRowHandle)) || (this.dataResetLocker.IsLocked || this.View.FocusedRowHandleChangedLocker.IsLocked))
        return;
      this.CancelRowEditIfNeeded();
      this.View.UpdateFocusedRowData();
    }

    private void CancelRowEditIfNeeded()
    {
      if (!this.DataProviderBase.IsCurrentRowEditing && !this.View.IsEditing || (this.View.IsNewItemRowFocused || this.View.IsAutoFilterRowFocused))
        return;
      this.View.CancelRowEdit();
    }

    void IDataProviderOwner.OnItemChanged(ListChangedEventArgs e)
    {
      if (e.ListChangedType == ListChangedType.ItemChanged && this.DataProviderBase.CurrentControllerRow == this.GetRowHandleByListIndex(e.OldIndex))
        this.UpdateCurrentCellValue();
      if (this.View == null)
        return;
      this.View.SelectionStrategy.OnItemChanged(e);
    }

    bool IDataProviderOwner.HasCustomRowFilter()
    {
      return this.HasCustomRowFilter;
    }

    bool IDataProviderOwner.RequireDisplayText(DataColumnInfo column)
    {
      return this.FilterData.IsRequired(column);
    }

    string IDataProviderOwner.GetDisplayText(int listSourceIndex, DataColumnInfo column, object value, string columnName)
    {
      GridDataColumnInfo gridDataColumnInfo1 = this.FilterData.GetInfo(column) as GridDataColumnInfo;
      if (gridDataColumnInfo1 != null && gridDataColumnInfo1.Column.FieldName == columnName)
        return gridDataColumnInfo1.GetDisplayText(listSourceIndex, value);
      if (this.SearchFilterData != null)
      {
        GridDataColumnInfo gridDataColumnInfo2 = this.SearchFilterData.GetInfo(column) as GridDataColumnInfo;
        if (gridDataColumnInfo2 != null && columnName.StartsWith("DxFts_"))
          return gridDataColumnInfo2.GetDisplayText(listSourceIndex, value);
      }
      return string.Empty;
    }

    string[] IDataProviderOwner.GetFindToColumnNames()
    {
      if (this.View == null)
        return (string[]) null;
      GridControlColumnProviderBase columnProvider = GridControlColumnProviderBase.GetColumnProvider((DependencyObject) this);
      if (columnProvider == null)
        return new List<string>().ToArray();
      return columnProvider.GetAllSearchColumns().ToArray();
    }

    bool IDataProviderOwner.RequireSortCell(DataColumnInfo sortColumn)
    {
      return this.SortData.IsRequired(sortColumn);
    }

    void IDataProviderOwner.OnListSourceChanged()
    {
      this.PopulateColumnsIfNeeded((DataProviderBase) null);
    }

    void IDataProviderOwner.OnStartNewItemRow()
    {
      TableView tableView = this.View as TableView;
      if (tableView == null)
        return;
      tableView.OnStartNewItemRow();
    }

    void IDataProviderOwner.OnEndNewItemRow()
    {
      TableView tableView = this.View as TableView;
      if (tableView == null)
        return;
      tableView.OnEndNewItemRow();
    }

    void IDataProviderOwner.RaiseCurrentRowUpdated(ControllerRowEventArgs e)
    {
      GridViewBase view = (GridViewBase) this.View;
      RowEventArgs e1 = new RowEventArgs(GridViewBase.RowUpdatedEvent, view, e.RowHandle, e.Row);
      view.RaiseRowUpdated(e1);
    }

    void IDataProviderOwner.RaiseCurrentRowCanceled(ControllerRowEventArgs e)
    {
      GridViewBase view = (GridViewBase) this.View;
      RowEventArgs e1 = new RowEventArgs(GridViewBase.RowCanceledEvent, view, e.RowHandle);
      view.RaiseRowCanceled(e1);
    }

    void IDataProviderOwner.RaiseValidatingCurrentRow(ValidateControllerRowEventArgs e)
    {
      GridViewBase gridViewBase = (GridViewBase) this.View;
      GridRowValidationEventArgs e1 = new GridRowValidationEventArgs((object) this, e.Row, e.RowHandle, (DataViewBase) gridViewBase);
      try
      {
        gridViewBase.RaiseValidateRow(e1);
      }
      catch (Exception ex)
      {
        this.SetRowStateError(e.RowHandle, (RowValidationError) new GridRowValidationError((object) ex.Message, ex, ErrorType.Default, e.RowHandle));
        throw ex;
      }
      if (e1.IsValid)
      {
        this.SetRowStateError(e.RowHandle, (RowValidationError) null);
      }
      else
      {
        string message = e1.ErrorContent != null ? e1.ErrorContent.ToString() : string.Empty;
        this.SetRowStateError(e.RowHandle, (RowValidationError) new GridRowValidationError((object) message, (Exception) null, ErrorType.Default, e.RowHandle));
        throw new WarningException(message);
      }
    }

    void IDataProviderOwner.OnPostRowException(ControllerRowExceptionEventArgs e)
    {
      ((GridViewBase) this.View).OnPostRowException(e);
    }

    void IDataProviderOwner.SynchronizeGroupSortInfo(IList<IColumnInfo> sortList, int groupCount)
    {
      this.SynchronizeSortInfo(sortList, groupCount);
    }

    bool IDataProviderOwner.CanSortColumn(string fieldName)
    {
      if (this.View != null)
        return this.View.CanSortColumn(fieldName);
      return false;
    }

    void IDataProviderOwner.RePopulateDataControllerColumns()
    {
      this.dataResetLocker.DoLockedAction((Action) (() =>
      {
        this.DataProviderBase.RePopulateColumns();
        if (this.View == null)
          return;
        this.View.UpdateColumnsViewInfo(false);
      }));
    }

    void IDataProviderOwner.UpdateIsAsyncOperationInProgress()
    {
      this.UpdateIsAsyncOperationInProgress();
    }

    protected override void UpdateIsAsyncOperationInProgress()
    {
      this.IsAsyncOperationInProgress = this.DataProviderBase.IsAsyncOperationInProgress || this.countColumnInstantFeedback > 0;
    }

    ColumnGroupInterval IDataProviderOwner.GetGroupInterval(string fieldName)
    {
      GridColumn gridColumn = this.Columns[fieldName];
      if (gridColumn == null)
        return ColumnGroupInterval.Default;
      return gridColumn.GroupInterval;
    }

    void IDataProviderOwner.RowChanged(ListChangedType changedType, int newHandle, int? oldRowHandle)
    {
      if (this.View == null)
        return;
      this.View.CurrentRowChanged(changedType, newHandle, oldRowHandle);
    }

    object IDataProviderEvents.GetUnboundData(int listSourceRowIndex, string fieldName, object value)
    {
      GridColumn gridColumn = this.Columns[fieldName];
      if (gridColumn == null)
        return value;
      if (gridColumn.DisplayMemberBindingCalculator != null)
      {
        int handleByListIndex = this.GetRowHandleByListIndex(listSourceRowIndex);
        return gridColumn.DisplayMemberBindingCalculator.GetValue(handleByListIndex, listSourceRowIndex);
      }
      GridColumnDataEventArgs args = this.CreateArgs(listSourceRowIndex, fieldName, value, true);
      this.RaiseCustomUnboundColumnData(args);
      return args.Value;
    }

    void IDataProviderEvents.SetUnboundData(int listSourceRowIndex, string fieldName, object value)
    {
      GridColumn gridColumn = this.Columns[fieldName];
      if (gridColumn == null)
        return;
      if (gridColumn.DisplayMemberBindingCalculator != null)
      {
        int handleByListIndex = this.GetRowHandleByListIndex(listSourceRowIndex);
        gridColumn.DisplayMemberBindingCalculator.SetValue(handleByListIndex, value);
      }
      this.RaiseCustomUnboundColumnData(this.CreateArgs(listSourceRowIndex, fieldName, value, false));
    }

    void IDataProviderEvents.SubstituteFilter(SubstituteFilterEventArgs e)
    {
      this.RaiseSubstituteFilter(e);
    }

    void IDataProviderEvents.SubstituteSortInfo(SubstituteSortInfoEventArgs e)
    {
      this.RaiseSubstituteSortInfo(e);
    }

    private GridColumnDataEventArgs CreateArgs(int listSourceRowIndex, string fieldName, object value, bool isGetAction)
    {
      return new GridColumnDataEventArgs(this, this.Columns[fieldName], listSourceRowIndex, value, isGetAction);
    }

    void IDataProviderEvents.OnCustomSummaryExists(object sender, CustomSummaryExistEventArgs e)
    {
      this.RaiseCustomSummaryExists(sender, e);
    }

    void IDataProviderEvents.OnCustomSummary(object sender, CustomSummaryEventArgs e)
    {
      this.RaiseCustomSummary(sender, e);
    }

    int? IDataProviderEvents.OnCompareSortValues(int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, DataColumnInfo sortColumn, ColumnSortOrder sortOrder)
    {
      GridDataColumnSortInfo sortInfo = this.SortData.GetSortInfo(sortColumn);
      if (sortInfo == null)
        return new int?();
      return sortInfo.CompareSortValues(listSourceRowIndex1, listSourceRowIndex2, value1, value2, sortOrder);
    }

    ExpressiveSortInfo.Cell IDataProviderEvents.GetSortCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType, ColumnSortOrder order)
    {
      GridDataColumnSortInfo sortInfo = this.SortData.GetSortInfo(dataColumnInfo);
      if (sortInfo == null)
        return (ExpressiveSortInfo.Cell) null;
      return sortInfo.GetCompareSortValuesInfo(baseExtractorType, order);
    }

    int? IDataProviderEvents.OnCompareGroupValues(int listSourceRowIndex1, int listSourceRowIndex2, object value1, object value2, DataColumnInfo sortColumn)
    {
      GridDataColumnSortInfo sortInfo = this.SortData.GetSortInfo(sortColumn);
      if (sortInfo == null)
        return new int?();
      return sortInfo.CompareGroupValues(listSourceRowIndex1, listSourceRowIndex2, value1, value2);
    }

    ExpressiveSortInfo.Cell IDataProviderEvents.GetSortGroupCellMethodInfo(DataColumnInfo dataColumnInfo, Type baseExtractorType)
    {
      GridDataColumnSortInfo sortInfo = this.SortData.GetSortInfo(dataColumnInfo);
      if (sortInfo == null)
        return (ExpressiveSortInfo.Cell) null;
      return sortInfo.GetCompareGroupValuesInfo(baseExtractorType);
    }

    void IDataProviderEvents.OnBeforeSorting()
    {
      if (!this.View.AutoScrollOnSorting)
        this.View.ScrollIntoViewLocker.Lock();
      this.SortData.OnStart();
      this.LockUpdateUntilSortingEndInAsyncServerMode();
      this.RaiseGridEventInOriginationGrid(GridControl.StartSortingEvent);
    }

    void IDataProviderEvents.OnAfterSorting()
    {
      this.UnlockUpdateAfterSortingEndInAsyncServerMode();
      if (!this.View.AutoScrollOnSorting)
        this.View.ScrollIntoViewLocker.Unlock();
      this.RaiseGridEventInOriginationGrid(GridControl.EndSortingEvent);
    }

    void IDataProviderEvents.OnBeforeGrouping()
    {
      this.RaiseGridEventInOriginationGrid(GridControl.StartGroupingEvent);
    }

    void IDataProviderEvents.OnAfterGrouping()
    {
      this.RaiseGridEventInOriginationGrid(GridControl.EndGroupingEvent);
    }

    bool? IDataProviderEvents.OnCustomRowFilter(int listSourceRowIndex, bool fit)
    {
      int num = this.RaiseCustomRowFilter(listSourceRowIndex, fit);
      if (num == -1)
        return new bool?();
      return new bool?(num != 0);
    }

    bool IDataProviderEvents.OnShowingGroupFooter(int rowHandle, int level)
    {
      TableView tableView = this.View as TableView;
      if (tableView != null)
        return tableView.RaiseShowingGroupFooter(rowHandle, level);
      return false;
    }

    private void LockUpdateUntilSortingEndInAsyncServerMode()
    {
      if (!this.DataProviderBase.IsAsyncServerMode)
        return;
      this.LockUpdateLayout = true;
    }

    private void UnlockUpdateAfterSortingEndInAsyncServerMode()
    {
      if (!this.DataProviderBase.IsAsyncServerMode)
        return;
      this.LockUpdateLayout = false;
    }

    /// <summary>
    ///                 <para>Sorts data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column within the grid.
    /// 
    ///           </param>
    public void SortBy(GridColumn column)
    {
      this.SortByCore((ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Sorts data by the values of the specified column in the specified order.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column within the grid.
    /// 
    ///           </param>
    /// <param name="sortedOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> enumeration value that specifies the column's sort order.
    /// 
    ///           </param>
    public void SortBy(GridColumn column, ColumnSortOrder sortedOrder)
    {
      this.SortByCore((ColumnBase) column, sortedOrder);
    }

    /// <summary>
    ///                 <para>Sorts data by the values of the specified column in the specified order, and places the column to the specified position among the sorted columns.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column within the grid.
    /// 
    ///           </param>
    /// <param name="sortedOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> enumeration value that specifies the column's sort order.
    /// 
    ///           </param>
    /// <param name="sortedIndex">
    /// An integer value that specifies the zero-based column's index among the sorted columns.
    /// 
    ///           </param>
    public void SortBy(GridColumn column, ColumnSortOrder sortedOrder, int sortedIndex)
    {
      this.SortByCore((ColumnBase) column, sortedOrder, sortedIndex);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    public void GroupBy(string fieldName)
    {
      this.GroupBy(this.Columns[fieldName]);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column by whose values data is grouped.
    /// 
    ///           </param>
    public void GroupBy(GridColumn column)
    {
      this.GroupBy(column, DataControlBase.defaultColumnSortOrder);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column. If several columns are involved in grouping, the specified column will reside at the specified grouping level.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column by whose values data is grouped.
    /// 
    ///           </param>
    /// <param name="groupedIndex">
    /// A zero-based integer value that specifies the grouping level. If negative, an exception is raised.
    /// 
    ///           </param>
    public void GroupBy(GridColumn column, int groupedIndex)
    {
      this.GroupBy(column, DataControlBase.defaultColumnSortOrder, groupedIndex);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column with the specified sort order.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column by whose values data is grouped.
    /// 
    ///           </param>
    /// <param name="sortedOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> enumeration value that specifies the column's sort order.
    /// 
    ///           </param>
    public void GroupBy(GridColumn column, ColumnSortOrder sortedOrder)
    {
      this.GroupBy(column, sortedOrder, this.GroupCount);
    }

    /// <summary>
    ///                 <para>Groups data by the values of the specified column with the specified sort order. If several columns are involved in grouping, the specified column will reside at the specified grouping level.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column by whose values data is grouped.
    /// 
    ///           </param>
    /// <param name="sortedOrder">
    /// A <see cref="T:DevExpress.Data.ColumnSortOrder" /> enumeration value that specifies the column's sort order.
    /// 
    ///           </param>
    /// <param name="groupedIndex">
    /// A zero-based integer value that specifies the grouping level. If negative, an exception is raised.
    /// 
    ///           </param>
    public void GroupBy(GridColumn column, ColumnSortOrder sortedOrder, int groupedIndex)
    {
      this.SortInfo.GroupByColumn(column.FieldName, groupedIndex, sortedOrder);
    }

    /// <summary>
    ///                 <para>Ungroups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    public void UngroupBy(string fieldName)
    {
      this.UngroupBy(this.Columns[fieldName]);
    }

    /// <summary>
    ///                 <para>Ungroups data by the values of the specified column.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column by whose values data grouping is canceled.
    /// 
    ///           </param>
    public void UngroupBy(GridColumn column)
    {
      this.SortInfo.UngroupByColumn(column.FieldName);
    }

    /// <summary>
    ///                 <para>Ungroups the grid.
    /// </para>
    ///             </summary>
    public void ClearGrouping()
    {
      this.SortInfo.BeginUpdate();
      try
      {
        foreach (GridColumn column in (Collection<GridColumn>) this.Columns)
        {
          if (column.IsGrouped)
            this.UngroupBy(column);
        }
      }
      finally
      {
        this.SortInfo.EndUpdate();
      }
    }

    protected override void GroupByColumn(ColumnBase column)
    {
      this.SortInfo.GroupByColumn(column.FieldName, this.GroupCount, column.SortOrder);
    }

    private void DoOnColumnsArePopulated(Action action)
    {
      if (this.Columns.Count > 0)
      {
        action();
      }
      else
      {
        NotifyCollectionChangedEventHandler handler = (NotifyCollectionChangedEventHandler) null;
        handler = (NotifyCollectionChangedEventHandler) ((d, e) =>
        {
          action();
          this.Columns.CollectionChanged -= handler;
        });
        this.Columns.CollectionChanged += handler;
      }
    }

    void IDataControllerVisualClient.RequireSynchronization(IDataSync dataSync)
    {
      this.DoOnColumnsArePopulated((Action) (() =>
      {
        List<GridSortInfo> gridSortInfoList = new List<GridSortInfo>();
        bool flag = false;
        this.InvalidSortCache.Clear();
        foreach (ListSortInfo listSortInfo in dataSync.Sort)
        {
          GridColumn gridColumn = this.Columns[listSortInfo.PropertyName];
          if (gridColumn != null)
            gridSortInfoList.Add(new GridSortInfo(gridColumn.FieldName, listSortInfo.SortDirection));
          else
            this.InvalidSortCache[listSortInfo.PropertyName] = new GridSortInfo(listSortInfo.PropertyName, listSortInfo.SortDirection);
          if (listSortInfo.PropertyName == this.DefaultSorting)
            flag = true;
        }
        this.UpdateInvalidGroupCache();
        if (!flag && !string.IsNullOrEmpty(this.DefaultSorting))
        {
          GridColumn gridColumn = this.Columns[this.DefaultSorting];
          ListSortDirection listSortDirection = gridColumn == null ? ListSortDirection.Ascending : (gridColumn.SortOrder == ColumnSortOrder.Descending ? ListSortDirection.Descending : ListSortDirection.Ascending);
          gridSortInfoList.Add(new GridSortInfo(this.DefaultSorting, listSortDirection));
          this.DataProviderBase.CollectionViewSource.SortDescriptions.Add(new SortDescription(this.DefaultSorting, listSortDirection));
        }
        this.BeginDataUpdate();
        try
        {
          this.SortInfo.ClearAndAddRange(dataSync.GroupCount, gridSortInfoList.ToArray());
          if (dataSync.HasFilter || object.ReferenceEquals((object) this.FilterCriteria, (object) null))
            return;
          this.FilterCriteria = (CriteriaOperator) null;
        }
        finally
        {
          this.EndDataUpdate();
        }
      }));
    }

    void IDataControllerVisualClient.ColumnsRenewed()
    {
      this.PopulateColumnsIfNeeded((DataProviderBase) null);
    }

    protected override EditorsGeneratorBase GetGenerateEditorsWrapper(GenerateBandWrapper bandWrapper)
    {
      return (EditorsGeneratorBase) new DefaultColumnWrapperGenerator(bandWrapper);
    }

    void IDataControllerVisualClient.UpdateRow(int controllerRowHandle)
    {
      this.UpdateRowCore(controllerRowHandle);
    }

    void IDataControllerVisualClient.UpdateRowIndexes(int newTopRowIndex)
    {
      this.visualClientUpdater.ScheduleUpdateRows();
    }

    void IDataControllerVisualClient.UpdateRows(int topRowIndexDelta)
    {
      this.visualClientUpdater.ScheduleUpdateRows();
    }

    void IDataControllerVisualClient.UpdateScrollBar()
    {
      this.visualClientUpdater.ScheduleUpdateScrollbar();
    }

    void IDataControllerVisualClient.UpdateTotalSummary()
    {
      this.UpdateTotalSummaryCore();
      if (this.View == null || !this.View.ViewBehavior.GetServiceSummaries().Any<ServiceSummaryItem>())
        return;
      this.visualClientUpdater.ScheduleUpdateRows();
    }

    void IDataControllerVisualClient.UpdateColumns()
    {
      if (!this.ColumnsCore.IsLockUpdate)
        this.DataProviderBase.SynchronizeSummary();
      this.DataSourceChangingLocker.DoIfNotLocked(new Action(((DataControlBase) this).ClearAndNotify));
    }

    void IDataControllerVisualClient.RequestSynchronization()
    {
      this.RequestSynchronizationCore();
    }

    void IDataControllerVisualClient.UpdateLayout()
    {
      this.UpdateLayoutCore();
      this.UpdateErrorPanel();
    }

    private void UpdateErrorPanel()
    {
      ErrorPanel errorPanel = this.GetTemplateChild("PART_ErrorPanel") as ErrorPanel;
      if (errorPanel == null || this.DataController == null)
        return;
      errorPanel.DataContext = (object) string.Format(GridControlLocalizer.GetString(GridControlStringId.ErrorPanelTextFormatString), (object) this.DataController.LastErrorText);
      errorPanel.Visibility = this.DataController.LastErrorText == "" || !this.IsLoaded ? Visibility.Collapsed : Visibility.Visible;
    }

    protected override void OnLoaded(object sender, RoutedEventArgs e)
    {
      base.OnLoaded(sender, e);
      this.UpdateErrorPanel();
    }

    protected override void OnDeserializeCreateCollectionItem(XtraCreateCollectionItemEventArgs e)
    {
      switch (e.CollectionName)
      {
        case "GroupSummary":
          GridSummaryItem gridSummaryItem = new GridSummaryItem();
          e.CollectionItem = (object) gridSummaryItem;
          this.GroupSummary.Add(gridSummaryItem);
          break;
        case "GroupSummarySortInfo":
          GridGroupSummarySortInfo groupSummarySortInfo = (GridGroupSummarySortInfo) new GridGroupSummarySortInfoDeserializable();
          e.CollectionItem = (object) groupSummarySortInfo;
          this.GroupSummarySortInfo.Add(groupSummarySortInfo);
          break;
        default:
          base.OnDeserializeCreateCollectionItem(e);
          break;
      }
    }

    protected override void BeginUpdateGroupSummary()
    {
      this.GroupSummary.BeginUpdate();
      this.GroupSummarySortInfo.BeginUpdate();
    }

    protected override void EndUpdateGroupSummary()
    {
      this.GroupSummarySortInfo.EndUpdate();
      this.GroupSummary.EndUpdate();
    }

    protected override void OnDeserializeEndBeforeRemoveSummary(int summaryIndex)
    {
      if (summaryIndex >= this.GroupCount)
        return;
      --this.GroupCount;
    }

    protected internal override bool GetAddNewColumns()
    {
      return DataControlSerializationOptions.GetAddNewColumns((DependencyObject) this);
    }

    protected internal override bool GetRemoveOldColumns()
    {
      return DataControlSerializationOptions.GetRemoveOldColumns((DependencyObject) this);
    }

    protected override string GetSerializationAppName()
    {
      return typeof (GridControl).Name;
    }

    /// <summary>
    ///                 <para>Expands all group rows.
    /// </para>
    ///             </summary>
    public void ExpandAllGroups()
    {
      if (!this.RaiseGroupRowExpanding(int.MinValue))
        return;
      this.DataProviderBase.ExpandAll();
      this.ExpandCollapseAllDetailGroups(true);
      this.RaiseGroupRowExpanded(int.MinValue);
      this.ScrollBarAnnotationsGeneration();
    }

    protected void ExpandCollapseAllDetailGroups(bool expanded)
    {
      if (this.View == null || !this.View.HasClonedDetails)
        return;
      DataControlOriginationElementHelper.EnumerateDependentElements<GridControl>((DataControlBase) this, (Func<DataControlBase, GridControl>) (grid => grid as GridControl), (Action<GridControl>) (grid =>
      {
        if (expanded)
          grid.ExpandAllGroups();
        else
          grid.CollapseAllGroups();
      }), (Action<GridControl>) null);
    }

    /// <summary>
    ///                 <para>Collapses all group rows.
    /// </para>
    ///             </summary>
    public void CollapseAllGroups()
    {
      if (!this.RaiseGroupRowCollapsing(int.MinValue))
        return;
      this.DataProviderBase.CollapseAll();
      this.ExpandCollapseAllDetailGroups(false);
      this.RaiseGroupRowCollapsed(int.MinValue);
      this.ScrollBarAnnotationsGeneration();
    }

    /// <summary>
    ///                 <para>Expands the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle. Group row handles are negative (starting from <b>-1</b>).
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified group row has been expanded; otherwise, <b>false</b>.
    /// </returns>
    public bool ExpandGroupRow(int rowHandle)
    {
      return this.ExpandGroupRow(rowHandle, false);
    }

    /// <summary>
    ///                 <para>Expands the specified group row and optionaly, any child group rows at all nesting levels.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle. Group row handles are negative (starting from <b>-1</b>).
    /// 
    ///           </param>
    /// <param name="recursive">
    /// <b>true </b> to expand any child group rows at all nesting levels; otherwise, <b>false</b>.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified group row has been expanded; otherwise, <b>false</b>.
    /// </returns>
    public bool ExpandGroupRow(int rowHandle, bool recursive)
    {
      if (!this.IsGroupRowHandle(rowHandle) || this.DataProviderBase.IsGroupRowExpanded(rowHandle))
        return false;
      this.ExpandGroupRowWithEvents(rowHandle, recursive);
      this.ScrollBarAnnotationsGeneration();
      return true;
    }

    /// <summary>
    ///                 <para>Collapses the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle. Group row handles are negative (starting from <b>-1</b>).
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified group row has been collapsed; otherwise, <b>false</b>.
    /// </returns>
    public bool CollapseGroupRow(int rowHandle)
    {
      return this.CollapseGroupRow(rowHandle, false);
    }

    /// <summary>
    ///                 <para>Collapses the specified group row and optionally any child group rows at all nesting levels.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle. Group row handles are negative (starting from <b>-1</b>).
    /// 
    ///           </param>
    /// <param name="recursive">
    /// <b>true</b> to collapse any child group rows at all nesting levels; otherwise, <b>false</b>.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified group row has been collapsed; otherwise, <b>false</b>.
    /// </returns>
    public bool CollapseGroupRow(int rowHandle, bool recursive)
    {
      if (!this.IsGroupRowHandle(rowHandle) || !this.DataProviderBase.IsGroupRowExpanded(rowHandle))
        return false;
      this.CollapseGroupRowWithEvents(rowHandle, recursive);
      this.ScrollBarAnnotationsGeneration();
      return true;
    }

    /// <summary>
    ///                 <para>Indicates whether the specified group row is expanded.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle. Group row handles are negative (starting from <b>-1</b>).
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified row is expanded; otherwise, <b>false</b>.
    /// </returns>
    public bool IsGroupRowExpanded(int rowHandle)
    {
      return this.DataProviderBase.IsGroupRowExpanded(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns a row's handle by specifying its visible index.
    /// </para>
    ///             </summary>
    /// <param name="visibleIndex">
    /// An integer value that specifies the row's visible position within a View.
    /// 
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the handle of the specified row (group or data).
    /// </returns>
    public int GetRowHandleByVisibleIndex(int visibleIndex)
    {
      return this.GetRowHandleByVisibleIndexCore(visibleIndex);
    }

    /// <summary>
    ///                 <para>Returns the row's position within a View by its handle.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the specified row's position within a View.</returns>
    public int GetRowVisibleIndexByHandle(int rowHandle)
    {
      return this.GetRowVisibleIndexByHandleCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the row's index in a data source by its handle.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the index of the record corresponding to the specified row, in a data source.
    /// </returns>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [Obsolete("Instead use the GetListIndexByRowHandle method. For detailed information, see the list of breaking changes in DXperience v2012 vol 1.")]
    public int GetRowListIndex(int rowHandle)
    {
      return this.GetListIndexByRowHandle(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the specified data row's index in a data source.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the data row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the index of the corresponding record in a data source.
    /// </returns>
    public int GetListIndexByRowHandle(int rowHandle)
    {
      return this.DataProviderBase.GetListIndexByRowHandle(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the data row's handle by its index in a data source.
    /// </para>
    ///             </summary>
    /// <param name="listIndex">
    /// An integer value that specifies the index of the corresponding record in a data source.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the data row's handle.</returns>
    public int GetRowHandleByListIndex(int listIndex)
    {
      return this.DataProviderBase.GetRowHandleByListIndex(listIndex);
    }

    /// <summary>
    ///                 <para>Returns an object that represents the specified row.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// A zero-based integer value that specifies the index of the corresponding record in a data source.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified row. <b>null</b> (<b>Nothing</b> in Visual Basic) if the specified row was not found.
    /// </returns>
    public object GetRowByListIndex(int listSourceRowIndex)
    {
      return this.DataProviderBase.GetRowByListIndex(listSourceRowIndex);
    }

    /// <summary>
    ///                 <para>Returns the specified cell's value.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// An integer value that identifies the record in a data source by its index.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that is the column containing the required data cell.
    /// 
    ///           </param>
    /// <returns>An object that is the specified cell's value.</returns>
    public object GetCellValueByListIndex(int listSourceRowIndex, GridColumn column)
    {
      return this.GetCellValueByListIndex(listSourceRowIndex, column.FieldName);
    }

    /// <summary>
    ///                 <para>Returns the specified cell's value.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// An integer value that identifies the record in a data source by its index.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of a column that contains the required cell.
    /// 
    ///           </param>
    /// <returns>An object that is the specified cell's value.</returns>
    public object GetCellValueByListIndex(int listSourceRowIndex, string fieldName)
    {
      return this.DataProviderBase.GetCellValueByListIndex(listSourceRowIndex, fieldName);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified handle corresponds to a group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified handle corresponds to a group row; otherwise, <b>false</b>.
    /// </returns>
    public bool IsGroupRowHandle(int rowHandle)
    {
      return this.IsGroupRowHandleCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified row handle is valid.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified row handle is valid; otherwise, <b>false</b>.
    /// </returns>
    public bool IsValidRowHandle(int rowHandle)
    {
      return this.IsValidRowHandleCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Indicates whether the specified row/card is visible.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified row/card is visible; otherwise, <b>false</b>.
    /// </returns>
    public bool IsRowVisible(int rowHandle)
    {
      return this.IsRowVisibleCore(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns a row object that corresponds to the focused row.
    /// </para>
    ///             </summary>
    /// <returns>An object that corresponds to the focused row.</returns>
    public object GetFocusedRow()
    {
      return this.DataProviderBase.GetRowValue(this.View != null ? this.View.FocusedRowHandle : int.MinValue);
    }

    /// <summary>
    ///                 <para>Returns the specified row's grouping level.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the row's grouping level.</returns>
    public int GetRowLevelByRowHandle(int rowHandle)
    {
      return this.DataProviderBase.GetRowLevelByControllerRow(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the specified row's grouping level.
    /// </para>
    ///             </summary>
    /// <param name="visibleIndex">
    /// An integer value that specifies the row's visible position within a View.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the row's grouping level.</returns>
    public int GetRowLevelByVisibleIndex(int visibleIndex)
    {
      return this.DataProviderBase.GetRowLevelByVisibleIndex(visibleIndex);
    }

    /// <summary>
    ///                 <para>Returns the value of the specified data cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row that contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that is the column that owns the specified cell.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified cell's value.</returns>
    public object GetCellValue(int rowHandle, GridColumn column)
    {
      return this.GetCellValueCore(rowHandle, (ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column displayed within the grid.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that specifies the text displayed within the specified cell.
    /// </returns>
    public string GetCellDisplayText(int rowHandle, GridColumn column)
    {
      return this.GetCellDisplayTextCore(rowHandle, (ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// An integer value that identifies the record in a data source by its index.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that is the column containing the required data cell.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that is the text displayed within the specified cell.
    /// </returns>
    public string GetCellDisplayTextByListIndex(int listSourceRowIndex, GridColumn column)
    {
      return this.View.GetColumnDisplayText(this.GetCellValueByListIndex(listSourceRowIndex, column), (ColumnBase) column, new int?());
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell.
    /// </para>
    ///             </summary>
    /// <param name="listSourceRowIndex">
    /// An integer value that identifies the record in a data source by its index.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of a column that contains the required cell.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that is the text displayed within the specified cell.
    /// </returns>
    public string GetCellDisplayTextByListIndex(int listSourceRowIndex, string fieldName)
    {
      return this.GetCellDisplayTextByListIndex(listSourceRowIndex, this.Columns[fieldName]);
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column displayed within the grid.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that specifies the text contained within the specified cell.
    /// </returns>
    public string GetFocusedRowCellDisplayText(GridColumn column)
    {
      return this.GetFocusedRowCellDisplayText(column.FieldName);
    }

    /// <summary>
    ///                 <para>Returns the text displayed within the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's filed name.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.String" /> value that specifies the text contained within the specified cell.
    /// </returns>
    public string GetFocusedRowCellDisplayText(string fieldName)
    {
      return this.GetCellDisplayText(this.View.FocusedRowHandle, fieldName);
    }

    /// <summary>
    ///                 <para>Returns the focused cell's value.
    /// </para>
    ///             </summary>
    /// <returns>An object that represents the focused cell's value. <b>null</b> (<b>Nothing</b> in Visual Basic) if the focused cell is not displayed within a View.
    /// </returns>
    public object GetFocusedValue()
    {
      if (this.View == null || this.CurrentColumn == null)
        return (object) null;
      return this.DataProviderBase.GetRowValue(this.View.FocusedRowHandle, this.CurrentColumn.FieldName);
    }

    /// <summary>
    ///                 <para>Sets the value of the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object representing the column that contains the cell.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that represents the specified cell's new value.
    /// 
    ///           </param>
    public void SetFocusedRowCellValue(GridColumn column, object value)
    {
      this.SetCellValue(this.View.FocusedRowHandle, column, value);
    }

    /// <summary>
    ///                 <para>Sets the value of the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    /// <param name="value">
    /// An object that represents the specified cell's new value.
    /// 
    ///           </param>
    public void SetFocusedRowCellValue(string fieldName, object value)
    {
      this.SetCellValue(this.View.FocusedRowHandle, fieldName, value);
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object representing the column that owns the cell.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified cell's value.</returns>
    public object GetFocusedRowCellValue(GridColumn column)
    {
      return this.GetCellValue(this.View.FocusedRowHandle, column);
    }

    /// <summary>
    ///                 <para>Returns the value of the specified cell contained within the focused row.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified cell's value.</returns>
    public object GetFocusedRowCellValue(string fieldName)
    {
      return this.GetCellValue(this.View.FocusedRowHandle, fieldName);
    }

    /// <summary>
    ///                 <para>Sets the specified cell's value.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// A zero based integer value that specifies the handle of the row which contains the cell.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object representing the column that contains the cell.
    /// 
    ///           </param>
    /// <param name="value">An object that represents the cell's new value.</param>
    public void SetCellValue(int rowHandle, GridColumn column, object value)
    {
      this.SetCellValue(rowHandle, column.FieldName, value);
    }

    /// <summary>
    ///                 <para>Sets the specified cell's value.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// A zero-based integer value that specifies the handle of the row that contains the cell.
    /// 
    ///           </param>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    /// <param name="value">An object that represents the cell's new value.</param>
    public void SetCellValue(int rowHandle, string fieldName, object value)
    {
      this.SetCellValueCore(rowHandle, fieldName, value);
    }

    /// <summary>
    ///                 <para>Returns a value of the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle.
    /// 
    /// 
    ///           </param>
    /// <returns>An object that represents the specified group row's value.</returns>
    public object GetGroupRowValue(int rowHandle)
    {
      return this.DataProviderBase.GetGroupRowValue(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns a value of the specified group row in the specified grouping column.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents a grouping column.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified group row's value.</returns>
    public object GetGroupRowValue(int rowHandle, GridColumn column)
    {
      return this.DataProviderBase.GetGroupRowValue(rowHandle, (ColumnBase) column);
    }

    /// <summary>
    ///                 <para>Returns the number of child rows (group or data) contained within the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the number of child rows contained within the specified group row.
    /// 
    /// </returns>
    public int GetChildRowCount(int rowHandle)
    {
      return this.DataProviderBase.GetChildRowCount(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the handle of the row contained within the specified group row, at the specified position.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the group row's handle.
    /// 
    ///           </param>
    /// <param name="childIndex">
    /// A zero-based integer that specifies the child row's position within the specified group row.
    /// 
    ///           </param>
    /// <returns>An integer value that represents the handle of the row contained within the specified group row, at the specified position.
    /// </returns>
    public int GetChildRowHandle(int rowHandle, int childIndex)
    {
      return this.DataProviderBase.GetChildRowHandle(rowHandle, childIndex);
    }

    /// <summary>
    ///                 <para>Returns the handle of the group row that owns the specified row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the child row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the parent group row's handle.</returns>
    public int GetParentRowHandle(int rowHandle)
    {
      return this.DataProviderBase.GetParentRowHandle(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the specified group summary value displayed within the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that identifies the group row by its handle.
    /// 
    ///           </param>
    /// <param name="item">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridSummaryItem" /> object that represents the group summary item.
    /// 
    ///           </param>
    /// <returns>An object that represents the specified group summary value displayed within the specified group row.
    /// </returns>
    public object GetGroupSummaryValue(int rowHandle, GridSummaryItem item)
    {
      object obj = (object) null;
      this.DataProviderBase.TryGetGroupSummaryValue(rowHandle, (SummaryItemBase) item, out obj);
      return obj;
    }

    internal override object GetGroupSummaryValue(int rowHandle, int summaryItemIndex)
    {
      return this.GetGroupSummaryValue(rowHandle, this.GroupSummary[summaryItemIndex]);
    }

    /// <summary>
    ///                 <para>Returns the handle of the first data row contained within the specified group row.
    /// </para>
    ///             </summary>
    /// <param name="groupRowHandle">
    /// An integer value that specifies  the group row's handle.
    /// 
    ///           </param>
    /// <returns>An integer value that specifies the handle of the first data row contained within the specified group row.
    /// </returns>
    public int GetDataRowHandleByGroupRowHandle(int groupRowHandle)
    {
      return this.DataProviderBase.GetControllerRowByGroupRow(groupRowHandle);
    }

    /// <summary>
    ///                 <para>Returns a row object that corresponds to the specified row handle asynchronously.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the row's handle.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> object which <b>Result</b> property is the row object that corresponds to the specified row handle.
    /// </returns>
    public Task<object> GetRowAsync(int rowHandle)
    {
      return new LoadRowAsyncOperation<object>(this, rowHandle, (Func<object>) (() => this.DataProviderBase.DataController.GetRow(rowHandle))).GetTask();
    }

    /// <summary>
    ///                 <para>Returns the value of the specified data cell asynchronously.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value that specifies the handle of the row that contains the cell.
    /// 
    ///           </param>
    /// <param name="columnName">
    /// A <see cref="T:System.String" /> value that specifies the column's field name.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> object which <b>Result</b> property is the specified cell's value.
    /// </returns>
    public Task<object> GetRowValueAsync(int rowHandle, string columnName)
    {
      return new LoadRowAsyncOperation<object>(this, rowHandle, (Func<object>) (() => this.DataProviderBase.DataController.GetRowValue(rowHandle, columnName))).GetTask();
    }

    /// <summary>
    ///                 <para>Returns a list of row objects that corresponds to the specified range of row handles asynchronously.
    /// </para>
    ///             </summary>
    /// <param name="startFrom">
    /// An integer value that is the handle of the first row in the range.
    /// 
    ///           </param>
    /// <param name="count">
    /// An integer value that is the number of rows in the range.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> object for which the <b>Result</b> property is the list of row objects that correspond to the specified range of row handles.
    /// 
    /// </returns>
    public Task<IList> GetRowsAsync(int startFrom, int count)
    {
      return new GetRowsAsyncOperation(this, startFrom, count).GetTask();
    }

    /// <summary>
    ///                 <para>Searches for the value in the column and returns the handle of the corresponding row asynchronously. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <param name="fieldName">
    /// A <see cref="T:System.String" /> value that specifies the field name of the column to be searched.
    /// 
    ///           </param>
    /// <param name="value">An object that is the search value.</param>
    /// <returns> A <see cref="T:System.Threading.Tasks.Task`1" /> object which <b>Result</b> property is the handle of the corresponding row.
    /// </returns>
    public Task<int> FindRowByValueAsync(string fieldName, object value)
    {
      return new FindRowByValueAsyncOperation(this, fieldName, value).GetTask();
    }

    /// <summary>
    ///                 <para>Searches for the value in the column and returns the handle of the corresponding row asynchronously. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <param name="column">The column to be searched.</param>
    /// <param name="value">An object that is the search value.</param>
    /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> object which <b>Result</b> property is an integer value that is the handle of the corresponding row.
    /// </returns>
    public Task<int> FindRowByValueAsync(ColumnBase column, object value)
    {
      return this.FindRowByValueAsync(column.FieldName, value);
    }

    /// <summary>
    ///                 <para>Determines the specified master row's expanded state and, optionally, the specified Detail's visibility.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value specifying the master row by its value. If an invalid row handle is specified, the method returns <b>false</b>.
    /// 
    ///           </param>
    /// <param name="descriptor">
    /// <i>Optional</i>. A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> descendant specifying the detail view whose visibility needs to be checked. If the master row is expanded but a different detail is currently visible, the method returns <b>false</b>. The same happens if you pass an object that doesn't represent the specified master row's detail.
    /// 
    ///           </param>
    /// <returns><b>true</b> if the specified master row is expanded and the specified detail is visible; otherwise, <b>false</b>.
    /// </returns>
    public bool IsMasterRowExpanded(int rowHandle, DetailDescriptorBase descriptor = null)
    {
      return this.MasterDetailProvider.IsMasterRowExpanded(rowHandle, descriptor);
    }

    /// <summary>
    ///                 <para>Changes the expanded state for a specified master row and, optionally, shows a specified Detail.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value specifying the master row by its handle.
    /// 
    ///           </param>
    /// <param name="expand">
    /// <b>true</b> to expand the specified row; <b>false</b> to collapse it.
    /// 
    ///           </param>
    /// <param name="descriptor">
    /// <i>Optional</i>. A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> object specifying the detail section to be made visible when expanding a master row. If the specified object doesn't represent a detail for the specified master row, then this parameter is ignored. The same happens if you pass <b>null</b> (<b>Nothing</b> in Visual Basic).
    /// 
    ///           </param>
    public void SetMasterRowExpanded(int rowHandle, bool expand, DetailDescriptorBase descriptor = null)
    {
      this.MasterDetailProvider.SetMasterRowExpanded(rowHandle, expand, descriptor);
    }

    /// <summary>
    ///                 <para>Expands the specified master row and, optionally, shows the specified Detail.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value specifying the master row by its handle. If the specified row handle is invalid, the method does nothing.
    /// 
    ///           </param>
    /// <param name="descriptor">
    /// <i>Optional</i>. A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> object specifying the detail to be shown. If the specified object doesn't represent the specified row's detail, the method expands the first or previously visible detail, depending on whether the current row was previously expanded.
    /// 
    /// 
    ///           </param>
    public void ExpandMasterRow(int rowHandle, DetailDescriptorBase descriptor = null)
    {
      this.SetMasterRowExpanded(rowHandle, true, descriptor);
    }

    /// <summary>
    ///                 <para>Collapses the detail section for the specified row.
    /// 
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the row by its handle.
    /// 
    ///           </param>
    /// <param name="descriptor">
    /// <i>Optional</i>. A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> object specifying the detail. This parameter is not used and defaults to <b>null</b> (<b>Nothing</b> in Visual Basic). Use the <b>CollapseMasterRow</b> method with only the <i>rowHandle</i> parameter.
    /// 
    ///           </param>
    public void CollapseMasterRow(int rowHandle, DetailDescriptorBase descriptor = null)
    {
      this.SetMasterRowExpanded(rowHandle, false, descriptor);
    }

    /// <summary>
    ///                 <para>Returns the currently visible detail data control identified by its master row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the row by its handle.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.DataControlBase" /> descendant which displays detail data. <b>null</b> (<b>Nothing</b> in Visual Basic) if the currently visible detail is not represented by a <see cref="T:DevExpress.Xpf.Grid.DataControlDetailDescriptor" />.
    /// </returns>
    public DataControlBase GetVisibleDetail(int rowHandle)
    {
      return this.MasterDetailProvider.FindVisibleDetailDataControl(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the Detail Descriptor corresponding to the currently expanded detail of the specified master row.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the master row by its handle.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.DetailDescriptorBase" /> object representing the Detail Descriptor of the currently expanded detail.
    /// </returns>
    public DetailDescriptorBase GetVisibleDetailDescriptor(int rowHandle)
    {
      return this.MasterDetailProvider.FindVisibleDetailDescriptor(rowHandle);
    }

    /// <summary>
    ///                 <para>Returns the detail data control identified by the master row and its Detail Descriptor.
    /// </para>
    ///             </summary>
    /// <param name="rowHandle">
    /// An integer value identifying the master row by its handle.
    /// 
    ///           </param>
    /// <param name="descriptor">
    /// <i>Optional</i>. A <see cref="T:DevExpress.Xpf.Grid.DataControlDetailDescriptor" /> object that identifies the detail in case multiple details are available at this level. If not specified while multiple details are available, the first available detail data control is returned.
    /// 
    ///           </param>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.DataControlBase" /> descendant which displays detail data. <b>null</b> (<b>Nothing</b> in Visual Basic) if no <see cref="T:DevExpress.Xpf.Grid.DataControlDetailDescriptor" /> objects are used as details on this level or if corresponding details are collapsed.
    /// </returns>
    public DataControlBase GetDetail(int rowHandle, DataControlDetailDescriptor descriptor = null)
    {
      return this.MasterDetailProvider.FindDetailDataControl(rowHandle, descriptor);
    }

    /// <summary>
    ///                 <para>Returns the root <see cref="T:DevExpress.Xpf.Grid.GridControl" /> of a master-detail grid.
    /// </para>
    ///             </summary>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.GridControl" /> object that is the root grid.
    /// </returns>
    public GridControl GetMasterGrid()
    {
      return this.GetMasterGridCore() as GridControl;
    }

    /// <summary>
    ///                 <para>Returns the handle of the master row corresponding to the current grid.
    /// </para>
    ///             </summary>
    /// <returns>The handle of the master row.</returns>
    public int GetMasterRowHandle()
    {
      DataViewBase targetView = (DataViewBase) null;
      int targetVisibleIndex = -1;
      if (!this.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex))
        return int.MinValue;
      return targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex);
    }

    private GridControl GetOriginationGridControl()
    {
      return (GridControl) this.GetOriginationDataControl();
    }

    protected internal override bool RaiseMasterRowExpandStateChanging(int rowHandle, bool isExpanded)
    {
      if (isExpanded)
        return this.RaiseMasterRowCollapsing(rowHandle);
      return this.RaiseMasterRowExpanding(rowHandle);
    }

    protected internal override void RaiseMasterRowExpandStateChanged(int rowHandle, bool isExpanded)
    {
      if (isExpanded)
        this.RaiseMasterRowExpanded(rowHandle);
      else
        this.RaiseMasterRowCollapsed(rowHandle);
    }

    internal override void ThrowNotSupportedInDetailException()
    {
    }

    protected override void CloneGroupSummarySortInfo(DataControlBase dataControl)
    {
      GridControl gridControl = (GridControl) dataControl;
      if (this.GroupSummarySortInfo.Count == 0 && gridControl.GroupSummarySortInfo.Count == 0)
        return;
      gridControl.GroupSummarySortInfo.Clear();
      CloneDetailHelper.CloneSimpleCollection<GridGroupSummarySortInfo>((IList) this.GroupSummarySortInfo, (IList) gridControl.GroupSummarySortInfo, new object[1]
      {
        (object) gridControl
      });
    }

    DataControlBase IDetailElement<DataControlBase>.CreateNewInstance(params object[] args)
    {
      return (DataControlBase) Activator.CreateInstance(this.GetType(), new object[1]{ (object) (IDataControlOriginationElement) args[0] });
    }

    protected override void UpdateHasDetailViews()
    {
      base.UpdateHasDetailViews();
      TableView tableView = this.View as TableView;
      if (tableView == null)
        return;
      tableView.UpdateHasDetailViews();
    }

    protected override void RequestSynchronizationCore()
    {
      if (this.GridDataProvider is DevExpress.Xpf.Data.GridDataProvider)
        ((DevExpress.Xpf.Data.GridDataProvider) this.GridDataProvider).DisplayMemberBindingInitialize();
      base.RequestSynchronizationCore();
      if (!(this.View is GridViewBase))
        return;
      ((GridViewBase) this.View).RefreshImmediateUpdateRowPositionProperty();
    }

    internal override void UpdateAllowPartialGrouping()
    {
      bool flag = !this.DataProviderBase.IsAsyncServerMode && this.View.AllowPartialGroupingCore;
      if (this.DataController == null || this.DataController.AllowPartialGrouping == flag)
        return;
      this.DataController.AllowPartialGrouping = flag;
      this.RefreshData();
    }

    internal void DisableAllowPartialGrouping()
    {
      if (this.DataController == null)
        return;
      this.DataController.AllowPartialGrouping = false;
      this.RefreshData();
    }

    protected override IBandsCollection CreateBands()
    {
      return (IBandsCollection) new BandCollection<GridControlBand>();
    }

    protected override BandsLayoutBase CreateBandsLayout()
    {
      return (BandsLayoutBase) new GridControlBandsLayout();
    }

    internal override void AttachToFormatConditions(FormatConditionChangeType changeType)
    {
      base.AttachToFormatConditions(changeType);
      this.visualClientUpdater.ScheduleUpdateRows();
    }

    protected override object GetItemsSource()
    {
      if (DesignerProperties.GetIsInDesignMode((DependencyObject) this))
        return GridControlHelper.GetDesignTimeSource((DataControlBase) this);
      return base.GetItemsSource();
    }

    internal override void SetCellValueCore(int rowHandle, string fieldName, object value)
    {
      if (TableView.IsCheckBoxSelectorColumn(fieldName))
        this.SetCheckBoxSelectorColumnValue(rowHandle, value);
      else
        base.SetCellValueCore(rowHandle, fieldName, value);
    }

    private void SetCheckBoxSelectorColumnValue(int rowHandle, object value)
    {
      if ((bool) value)
        this.DataView.SelectRowCore(rowHandle);
      else
        this.DataView.UnselectRowCore(rowHandle);
    }

    internal override object GetCellValueCore(int rowHandle, string fieldName)
    {
      if (TableView.IsCheckBoxSelectorColumn(fieldName))
        return (object) this.GetCheckBoxSelectorColumnValue(rowHandle);
      return base.GetCellValueCore(rowHandle, fieldName);
    }

    private bool GetCheckBoxSelectorColumnValue(int rowHandle)
    {
      return this.DataView.Return<DataViewBase, bool>((Func<DataViewBase, bool>) (dataView => dataView.IsRowSelected(rowHandle)), (Func<bool>) (() => false));
    }

    protected internal override void BeforeColumnMove(BaseColumn column, HeaderPresenterType moveFrom)
    {
      base.BeforeColumnMove(column, moveFrom);
      (column as GridColumn).Do<GridColumn>((Action<GridColumn>) (col =>
      {
        if (moveFrom != HeaderPresenterType.GroupPanel || !col.IsGrouped)
          return;
        this.UngroupBy(col);
      }));
    }

    [SpecialName]
    Dispatcher IDataProviderOwner.get_Dispatcher()
    {
      return this.Dispatcher;
    }

    private class VisualClientUpdater
    {
      private readonly GridControl grid;
      private GridControl.VisualClientUpdater.UpdateMode updateMode;

      public VisualClientUpdater(GridControl grid)
      {
        this.grid = grid;
      }

      public void ScheduleUpdateScrollbar()
      {
        if (this.grid.BottomRowBelowOldVisibleRowCount)
        {
          this.ScheduleUpdateRows();
        }
        else
        {
          this.SetUpdateMode(GridControl.VisualClientUpdater.UpdateMode.Scrollbar);
          this.EnqueueUpdateAction();
        }
      }

      public void ScheduleUpdateRows()
      {
        this.SetUpdateMode(GridControl.VisualClientUpdater.UpdateMode.Rows);
        this.EnqueueUpdateAction();
      }

      private void SetUpdateMode(GridControl.VisualClientUpdater.UpdateMode newUpdateMode)
      {
        this.updateMode = (GridControl.VisualClientUpdater.UpdateMode) Math.Max((int) this.updateMode, (int) newUpdateMode);
        this.grid.InvalidateDetailScrollInfoCache();
      }

      private void EnqueueUpdateAction()
      {
        if (this.grid.DataView != null && this.grid.DataView.UpdateActionEnqueued)
          return;
        if (this.grid.DataView == null || this.grid.DataView.RootDataPresenter == null || this.IsNewItemRowCommiting())
        {
          this.Update();
        }
        else
        {
          this.grid.DataView.UpdateActionEnqueued = true;
          this.grid.viewCore.EnqueueImmediateAction(new Action(this.Update));
        }
      }

      private bool IsNewItemRowCommiting()
      {
        if (this.grid.DataView.CommitEditingLocker.IsLocked)
          return this.grid.DataView.IsBottomNewItemRowFocused;
        return false;
      }

      public void Update()
      {
        this.grid.DataView.UpdateActionEnqueued = false;
        switch (this.updateMode)
        {
          case GridControl.VisualClientUpdater.UpdateMode.Rows:
            this.grid.UpdateRowsCore(false, false);
            break;
        }
        this.updateMode = GridControl.VisualClientUpdater.UpdateMode.None;
      }

      private enum UpdateMode
      {
        None,
        Scrollbar,
        Rows,
      }
    }
  }
}
