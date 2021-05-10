// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.Native.GridControlVisualClientOwner
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Editors.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.LookUp.Native
{
  public class GridControlVisualClientOwner : VisualClientOwner
  {
    private Locker selectionChangedLocker = new Locker();
    private WeakReference editorItemsSource;
    private Point lastMousePosition;

    private LookUpEdit Editor
    {
      get
      {
        return base.Editor as LookUpEdit;
      }
    }

    internal GridControl Grid
    {
      get
      {
        return this.InnerEditor as GridControl;
      }
    }

    private DataViewBase View
    {
      get
      {
        if (this.Grid == null)
          return (DataViewBase) null;
        return this.Grid.View;
      }
    }

    private SearchControl SearchControl
    {
      get
      {
        if (this.View == null)
          return (SearchControl) null;
        return this.View.SearchControl;
      }
    }

    private LookUpEditStyleSettings StyleSettings
    {
      get
      {
        return (LookUpEditStyleSettings) ActualPropertyProvider.GetProperties((DependencyObject) this.Editor).StyleSettings;
      }
    }

    protected override bool IsLoaded
    {
      get
      {
        if (base.IsLoaded)
          return this.Grid != null;
        return false;
      }
    }

    private LookUpEditBasePropertyProvider PropertyProvider
    {
      get
      {
        return ActualPropertyProvider.GetProperties((DependencyObject) this.Editor) as LookUpEditBasePropertyProvider;
      }
    }

    private bool ShouldSyncProperties
    {
      get
      {
        if (this.IsLoaded)
          return this.Editor.EditMode != EditMode.InplaceInactive;
        return false;
      }
    }

    private bool IsInAutoFilterRowEditing
    {
      get
      {
        if (this.IsLoaded && this.View.FocusedRowHandle == -999997)
          return this.View.ActiveEditor != null;
        return false;
      }
    }

    private bool IsInnerPopupOpened
    {
      get
      {
        if (!this.IsLoaded)
          return false;
        if (!this.View.IsContextMenuOpened)
          return this.View.IsColumnFilterOpened;
        return true;
      }
    }

    internal bool IsSearchControlFocused
    {
      get
      {
        if (this.SearchControl != null)
          return this.SearchControl.IsKeyboardFocusWithin;
        return false;
      }
    }

    internal bool IsSearchTextEmpty
    {
      get
      {
        if (this.SearchControl == null)
          return true;
        this.SearchControl.DoValidate();
        return string.IsNullOrEmpty(this.SearchControl.SearchText);
      }
    }

    private bool IsServerMode
    {
      get
      {
        return LookUpEditHelper.GetIsServerMode((LookUpEditBase) this.Editor);
      }
    }

    private IList<object> TotalSelectedItems { get; set; }

    private bool SelectionChangedManually { get; set; }

    public GridControlVisualClientOwner(PopupBaseEdit editor)
      : base(editor)
    {
    }

    protected override void SubscribeEvents()
    {
      if (this.Grid == null)
        return;
      this.Grid.MouseLeftButtonUp += new MouseButtonEventHandler(this.GridMouseUp);
      this.Grid.View.FocusedRowHandleChanged += new FocusedRowHandleChangedEventHandler(this.FocusedRowChanged);
      this.Grid.CurrentItemChanged += new CurrentItemChangedEventHandler(this.GridOnCurrentItemChanged);
      this.Grid.SelectionChanged += new GridSelectionChangedEventHandler(this.GridSelectionChanged);
      base.SubscribeEvents();
    }

    protected override void UnsubscribeEvents()
    {
      if (this.Grid == null)
        return;
      this.Grid.MouseLeftButtonUp -= new MouseButtonEventHandler(this.GridMouseUp);
      this.Grid.View.FocusedRowHandleChanged -= new FocusedRowHandleChangedEventHandler(this.FocusedRowChanged);
      this.Grid.CurrentItemChanged -= new CurrentItemChangedEventHandler(this.GridOnCurrentItemChanged);
      this.Grid.SelectionChanged -= new GridSelectionChangedEventHandler(this.GridSelectionChanged);
      base.UnsubscribeEvents();
    }

    private void GridOnCurrentItemChanged(object sender, CurrentItemChangedEventArgs e)
    {
      if (!LookUpEditHelper.GetIsAsyncServerMode((LookUpEditBase) this.Editor) || this.selectionChangedLocker.IsLocked)
        return;
      this.SelectionChangedManually = true;
    }

    private void GridSelectionChanged(object sender, GridSelectionChangedEventArgs e)
    {
      if (LookUpEditHelper.GetIsSingleSelection((LookUpEditBase) this.Editor))
        return;
      List<object> objectList1 = (List<object>) null;
      List<object> objectList2 = (List<object>) null;
      if (!this.Grid.IsGroupRowHandle(e.ControllerRow))
      {
        if (e.Action == CollectionChangeAction.Add)
        {
          objectList1 = new List<object>()
          {
            this.GetRowKey(e.ControllerRow)
          };
          this.TotalSelectedItems = (IList<object>) this.TotalSelectedItems.Union<object>(CustomItem.FilterCustomItems((IEnumerable<object>) objectList1)).ToList<object>();
        }
        else if (e.Action == CollectionChangeAction.Remove)
        {
          objectList2 = new List<object>()
          {
            this.GetRowKey(e.ControllerRow)
          };
          this.TotalSelectedItems = (IList<object>) this.TotalSelectedItems.Except<object>(CustomItem.FilterCustomItems((IEnumerable<object>) objectList2)).ToList<object>();
        }
        else if (e.Action == CollectionChangeAction.Refresh)
          this.TotalSelectedItems = (IList<object>) ((IEnumerable<int>) this.Grid.GetSelectedRowHandles()).Select<int, object>(new Func<int, object>(this.GetRowKey)).ToList<object>();
      }
      LookUpEditHelper.RaisePopupContentSelectionChangedEvent((LookUpEditBase) this.Editor, (IList) objectList2, (IList) objectList1);
    }

    public override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers)
    {
      if (!this.IsInnerPopupOpened)
        return base.IsClosePopupWithCancelGesture(key, modifiers);
      return false;
    }

    public override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers)
    {
      if (!this.IsInAutoFilterRowEditing && !this.IsInnerPopupOpened)
        return base.IsClosePopupWithAcceptGesture(key, modifiers);
      return false;
    }

    private void FocusedRowChanged(object sender, FocusedRowHandleChangedEventArgs e)
    {
      LookUpEditHelper.RaisePopupContentSelectionChangedEvent((LookUpEditBase) this.Editor, (IList) new List<object>()
      {
        (object) null
      }, (IList) new List<object>()
      {
        this.Grid.CurrentItem
      });
    }

    private void GridMouseUp(object sender, MouseButtonEventArgs e)
    {
      if (e.LeftButton != MouseButtonState.Released || !this.IsDataRowRowHandle(this.Grid.View.GetRowHandleByMouseEventArgs((MouseEventArgs) e)) || !LookUpEditHelper.GetClosePopupOnMouseUp((LookUpEditBase) this.Editor))
        return;
      this.Editor.ClosePopup();
    }

    private bool IsDataRowRowHandle(int rowHandle)
    {
      if (this.Grid.IsGroupRowHandle(rowHandle) || rowHandle == int.MinValue || rowHandle == -2147483647)
        return false;
      return rowHandle != -999997;
    }

    public override void InnerEditorMouseMove(object sender, MouseEventArgs e)
    {
      if (this.IsDragging(sender) || !this.CalcAllowItemHighlighting((LookUpEditBase) this.Editor) || this.Grid.View.IsColumnChooserVisible)
        return;
      Point position = e.GetPosition((IInputElement) this.Grid);
      if (this.lastMousePosition != position)
      {
        int byMouseEventArgs = this.Grid.View.GetRowHandleByMouseEventArgs(e);
        if (this.IsDataRowRowHandle(byMouseEventArgs) && !this.Grid.View.IsEditing)
          this.SetFocusedRowHandleInternal(byMouseEventArgs);
      }
      this.lastMousePosition = position;
      base.InnerEditorMouseMove(sender, e);
    }

    private bool CalcAllowItemHighlighting(LookUpEditBase editor)
    {
      if (!LookUpEditHelper.GetIsAllowItemHighlighting(editor))
        return false;
      return LookUpEditHelper.GetClosePopupOnMouseUp((LookUpEditBase) this.Editor);
    }

    private bool IsDragging(object sender)
    {
      DependencyObject element = (DependencyObject) LayoutHelper.GetTopLevelVisual((DependencyObject) (sender as FrameworkElement));
      if (element == null)
        return false;
      return DragManager.GetIsDragging(element);
    }

    private void SetFocusedRowHandleInternal(int rowHandle)
    {
      this.View.ScrollIntoViewLocker.DoLockedAction((Action) (() => this.Grid.View.SetFocusedRowHandle(rowHandle)));
    }

    public override void PopupClosed()
    {
      base.PopupClosed();
      LookUpEditHelper.FocusEditCore((LookUpEditBase) this.Editor);
    }

    public override void PopupContentLoaded()
    {
      base.PopupContentLoaded();
      this.SyncProperties(true);
    }

    public override void SyncProperties(bool syncDataSource)
    {
      if (!this.ShouldSyncProperties)
        return;
      if (syncDataSource)
        this.SetupDataSource();
      this.SyncValues(false);
      this.Dispatcher.BeginInvoke((Delegate) (() =>
      {
        if (!this.IsLoaded)
          return;
        this.Grid.InvalidateMeasure();
      }));
      this.SyncSelectedItems(syncDataSource);
    }

    private void SetupDataSource()
    {
      object contentItemsSource = this.Editor.GetPopupContentItemsSource();
      if (this.Grid.ItemsSource != contentItemsSource)
      {
        switch (System.Windows.DependencyPropertyHelper.GetValueSource((DependencyObject) this.Grid, DataControlBase.ItemsSourceProperty).BaseValueSource)
        {
          case BaseValueSource.Default:
          case BaseValueSource.Local:
            this.Grid.ItemsSource = contentItemsSource;
            ((BaseGridController) this.Grid.DataController).Do<BaseGridController>((Action<BaseGridController>) (x => x.KeepFocusedRowOnUpdate = false));
            break;
        }
      }
      if (this.Editor.AutoPopulateColumns && (this.editorItemsSource == null || this.editorItemsSource.Target != this.Editor.ItemsSource))
      {
        this.Grid.PopulateColumns();
        this.editorItemsSource = new WeakReference(this.Editor.ItemsSource);
      }
      this.SelectionChangedManually = false;
    }

    protected override bool ProcessKeyDownInternal(KeyEventArgs e)
    {
      if (e.Handled || !this.Editor.IsPopupOpen && !this.CanClosePopup(e) && this.OpenPopupAndReraiseTextInput(e))
        return true;
      if (this.View != null && !this.View.IsContextMenuOpened && (!this.View.IsColumnFilterOpened && this.IsDataRowRowHandle(this.View.FocusedRowHandle)) && (e.Key == Key.Return && this.Editor.IsPopupOpen))
      {
        e.Handled = true;
        this.Editor.ClosePopup();
        return true;
      }
      if (this.View != null && !this.IsNavigationKey(e.Key))
      {
        this.View.ViewBehavior.ProcessPreviewKeyDown(e);
        if (!e.Handled)
          this.View.ProcessKeyDown(e);
      }
      return true;
    }

    private bool CanClosePopup(KeyEventArgs e)
    {
      if (e.Key != Key.Return && e.Key != Key.Escape)
        return e.Key == Key.F4;
      return true;
    }

    protected virtual bool OpenPopupAndReraiseTextInput(KeyEventArgs e)
    {
      if (this.PropertyProvider.IsTextEditable || !this.Editor.ImmediatePopup || (e.Key == Key.Tab || e.Key == Key.LeftShift) || (e.Key == Key.RightShift || e.Key == Key.System || (e.Key == Key.LeftCtrl || e.Key == Key.RightCtrl)) || (e.Key == Key.NumLock || e.Key == Key.Capital || e.Key == Key.Capital))
        return false;
      this.Editor.ShowPopup();
      return true;
    }

    protected virtual bool IsNavigationKey(Key key)
    {
      if (key != Key.Right && key != Key.Left && (key != Key.Tab && key != Key.Home))
        return key == Key.End;
      return true;
    }

    public override object GetSelectedItem()
    {
      if (!this.IsLoaded)
        return (object) null;
      return this.GetRowKey(this.Grid.View.FocusedRowHandle);
    }

    private object GetRowKey(int rowHandle)
    {
      if (!this.IsServerMode)
        return this.Grid.GetRow(rowHandle);
      if (!string.IsNullOrEmpty(this.Editor.ValueMember))
        return this.Grid.DataController.GetValueEx(rowHandle, this.Editor.ValueMember);
      return this.Grid.DataController.GetRowKey(rowHandle);
    }

    public override void SyncValues(bool resetTotals = false)
    {
      base.SyncValues(resetTotals);
      if (!this.IsLoaded)
        return;
      if (!this.SelectionChangedManually)
        this.selectionChangedLocker.DoLockedAction((Action) (() => this.StyleSettings.SyncValues((LookUpEditBase) this.Editor, this.Grid)));
      this.SyncSelectedItems(resetTotals);
    }

    protected override void BeforeProcessKeyDownInternal()
    {
      this.SelectionChangedManually = false;
    }

    private void SyncSelectedItems(bool updateTotals)
    {
      if (!updateTotals)
        return;
      if (!LookUpEditHelper.GetIsSingleSelection((LookUpEditBase) this.Editor))
      {
        IEnumerable source = LookUpEditHelper.GetEditValue((ISelectorEdit) this.Editor) as IEnumerable;
        IItemsProvider2 provider = this.Editor.ItemsProvider;
        this.TotalSelectedItems = source != null ? (IList<object>) new List<object>(source.Cast<object>().Select<object, object>((Func<object, object>) (x => provider.GetItem(x, this.Editor.EditStrategy.CurrentDataViewHandle)))) : (IList<object>) new List<object>();
      }
      else
        this.TotalSelectedItems = (IList<object>) new List<object>();
    }

    protected override FrameworkElement FindEditor()
    {
      return LayoutHelper.FindElementByName(LookUpEditHelper.GetPopupContentOwner((PopupBaseEdit) this.Editor).Child, "PART_GridControl");
    }

    protected override void SetupEditor()
    {
      if (this.Grid == null)
        return;
      this.InitializeSearchPanel();
      this.Grid.SetValue(TextBlockService.EnableTextHighlightingProperty, (object) false);
      this.Grid.ShowBorder = false;
      this.UpdateViewProperty(DataViewBase.NavigationStyleProperty, (object) GridViewNavigationStyle.Row);
      this.UpdateViewProperty(DataViewBase.IsSynchronizedWithCurrentItemProperty, (object) false);
      this.UpdateViewProperty(DataViewBase.ShowColumnHeadersProperty, (object) this.StyleSettings.ShowColumnHeaders);
      this.UpdateViewProperty(DataViewBase.ShowTotalSummaryProperty, (object) this.StyleSettings.ShowTotalSummary);
      this.UpdateViewProperty(GridViewBase.ShowGroupPanelProperty, (object) this.StyleSettings.ShowGroupPanel);
      this.UpdateViewProperty(DataViewBase.AllowSortingProperty, (object) this.StyleSettings.AllowSorting);
      this.UpdateViewProperty(GridViewBase.AllowGroupingProperty, (object) this.StyleSettings.AllowGrouping);
      this.UpdateViewProperty(DataViewBase.AllowColumnFilteringProperty, (object) this.StyleSettings.AllowColumnFiltering);
      this.UpdateProperty((DependencyObject) this.Grid, DataControlBase.SelectionModeProperty, (object) (MultiSelectMode) (LookUpEditHelper.GetIsSingleSelection((LookUpEditBase) this.Editor) ? 0 : 1));
      this.SyncSelectedItems(true);
    }

    protected virtual void InitializeSearchPanel()
    {
      this.Grid.View.ShowSearchPanelMode = this.StyleSettings.ShowSearchPanel ? ShowSearchPanelMode.Always : ShowSearchPanelMode.Never;
      this.CreateColumnProvider();
      this.Grid.View.SearchPanelFindFilter = this.PropertyProvider.FilterCondition;
      this.Grid.View.SearchPanelFindMode = this.PropertyProvider.FindMode;
      this.Grid.View.ShowSearchPanelFindButton = this.PropertyProvider.GetFindButtonPlacement() != EditorPlacement.None;
      this.Grid.View.SearchPanelImmediateMRUPopup = new bool?(this.Grid.View.SearchPanelImmediateMRUPopup.HasValue && this.Grid.View.SearchPanelImmediateMRUPopup.Value);
      this.Grid.View.SearchString = (string) null;
    }

    protected void CreateColumnProvider()
    {
      if (GridControlColumnProviderBase.GetColumnProvider((DependencyObject) this.Grid) != null)
        return;
      GridControlColumnProvider controlColumnProvider1 = new GridControlColumnProvider();
      controlColumnProvider1.AllowColumnsHighlighting = true;
      controlColumnProvider1.FilterByColumnsMode = this.PropertyProvider.FilterByColumnsMode;
      controlColumnProvider1.IsSearchLookUpMode = true;
      GridControlColumnProvider controlColumnProvider2 = controlColumnProvider1;
      if (this.PropertyProvider.FilterByColumnsMode == FilterByColumnsMode.Custom)
        controlColumnProvider2.CustomColumns = new ObservableCollection<string>(LookUpEditHelper.GetHighlightedColumns((LookUpEditBase) this.Editor));
      GridControlColumnProviderBase.SetColumnProvider((DependencyObject) this.Grid, (GridControlColumnProviderBase) controlColumnProvider2);
    }

    protected void UpdateViewProperty(DependencyProperty property, object value)
    {
      this.UpdateViewProperty((DependencyObject) this.Grid.View, property, value);
    }

    private void UpdateViewProperty(DependencyObject obj, DependencyProperty property, object value)
    {
      if (!(obj is GridViewBase))
        return;
      this.UpdateProperty(obj, property, value);
    }

    private void UpdateProperty(DependencyObject obj, DependencyProperty property, object value)
    {
      ValueSource valueSource = System.Windows.DependencyPropertyHelper.GetValueSource(obj, property);
      if (valueSource.BaseValueSource != BaseValueSource.Unknown && valueSource.BaseValueSource != BaseValueSource.Default)
        return;
      obj.SetValue(property, value);
    }

    public override IEnumerable GetSelectedItems()
    {
      return (IEnumerable) this.TotalSelectedItems ?? (IEnumerable) new List<object>();
    }
  }
}
