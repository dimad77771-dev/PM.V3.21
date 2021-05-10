// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridSearchControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Utils;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  [DXToolboxBrowsable(false)]
  public class GridSearchControl : GridSearchControlBase
  {
    public static readonly DependencyProperty GroupPanelShownProperty;
    public static readonly DependencyProperty ActualShowButtonCloseProperty;
    public static readonly DependencyProperty ShowSearchPanelModeProperty;
    public static readonly DependencyProperty ActualVisibilityProperty;

    internal override bool IsLogicControl
    {
      get
      {
        return false;
      }
    }

    public bool GroupPanelShown
    {
      get
      {
        return (bool) this.GetValue(GridSearchControl.GroupPanelShownProperty);
      }
      set
      {
        this.SetValue(GridSearchControl.GroupPanelShownProperty, (object) value);
      }
    }

    public bool ActualShowButtonClose
    {
      get
      {
        return (bool) this.GetValue(GridSearchControl.ActualShowButtonCloseProperty);
      }
      set
      {
        this.SetValue(GridSearchControl.ActualShowButtonCloseProperty, (object) value);
      }
    }

    public ShowSearchPanelMode ShowSearchPanelMode
    {
      get
      {
        return (ShowSearchPanelMode) this.GetValue(GridSearchControl.ShowSearchPanelModeProperty);
      }
      set
      {
        this.SetValue(GridSearchControl.ShowSearchPanelModeProperty, (object) value);
      }
    }

    public Visibility ActualVisibility
    {
      get
      {
        return (Visibility) this.GetValue(GridSearchControl.ActualVisibilityProperty);
      }
      set
      {
        this.SetValue(GridSearchControl.ActualVisibilityProperty, (object) value);
      }
    }

    static GridSearchControl()
    {
      Type ownerType = typeof (GridSearchControl);
      GridSearchControl.GroupPanelShownProperty = DependencyPropertyManager.Register("GroupPanelShown", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      GridSearchControl.ActualShowButtonCloseProperty = DependencyPropertyManager.Register("ActualShowButtonClose", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, (PropertyChangedCallback) ((d, e) => ((GridSearchControl) d).UpdateCloseButtonVisibility())));
      GridSearchControl.ShowSearchPanelModeProperty = DependencyPropertyManager.Register("ShowSearchPanelMode", typeof (ShowSearchPanelMode), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) ShowSearchPanelMode.Default, (PropertyChangedCallback) ((d, e) => ((GridSearchControl) d).UpdateCloseButtonVisibility())));
      GridSearchControl.ActualVisibilityProperty = DependencyPropertyManager.Register("ActualVisibility", typeof (Visibility), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) Visibility.Collapsed, (PropertyChangedCallback) ((d, e) => ((GridSearchControl) d).OnVisibilityChanged((Visibility) e.OldValue, (Visibility) e.NewValue))));
    }

    public GridSearchControl()
    {
      this.GotFocus += new RoutedEventHandler(this.GridSearchControl_GotFocus);
      this.Loaded += new RoutedEventHandler(this.GridSearchControl_Loaded);
    }

    private void GridSearchControl_GotFocus(object sender, RoutedEventArgs e)
    {
      if (this.View == null)
        return;
      this.View.CommitEditing();
      this.View.SetSearchPanelFocus(true);
    }

    private void UpdateCloseButtonVisibility()
    {
      switch (this.ShowSearchPanelMode)
      {
        case ShowSearchPanelMode.Default:
          this.ShowCloseButton = this.ActualShowButtonClose;
          break;
        case ShowSearchPanelMode.Always:
        case ShowSearchPanelMode.Never:
          this.ShowCloseButton = false;
          break;
      }
    }

    protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
    {
      base.OnIsKeyboardFocusWithinChanged(e);
      if ((bool) e.NewValue || this.View == null)
        return;
      this.View.SetSearchPanelFocus(false);
      if (!this.View.GetIsKeyboardFocusWithin())
        return;
      this.View.Focus();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      if (this.View == null || e.Key != Key.Tab)
        return;
      if (ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
      {
        this.SetFocusOnCellEditor();
        this.ViewInplaceEditorMoveFocus(e);
        e.Handled = true;
      }
      else
      {
        this.SetFocusOnCellEditor();
        e.Handled = true;
      }
    }

    protected override void OnPreviewKeyDown(KeyEventArgs e)
    {
      base.OnPreviewKeyDown(e);
      if (!ModifierKeysHelper.NoModifiers(ModifierKeysHelper.GetKeyboardModifiers(e)))
        return;
      PopupBaseEdit popupBaseEdit = this.GetEditorControl() as PopupBaseEdit;
      switch (e.Key)
      {
        case Key.Escape:
          if (popupBaseEdit == null || popupBaseEdit.IsPopupOpen || (e.Handled || this.View == null) || this.View.ShowSearchPanelMode == ShowSearchPanelMode.Always)
            break;
          this.View.HideSearchPanel();
          e.Handled = true;
          break;
        case Key.Down:
          if (popupBaseEdit != null && popupBaseEdit.IsPopupOpen)
            break;
          this.SetFocusOnCellEditor();
          e.Handled = true;
          break;
      }
    }

    private void SetFocusOnCellEditor()
    {
      if (this.View == null)
        return;
      this.View.SetSearchPanelFocus(false);
      this.View.Focus();
    }

    private void ViewInplaceEditorMoveFocus(KeyEventArgs e)
    {
      this.View.InplaceEditorOwner.MoveFocus(e);
    }

    private void GridSearchControl_Loaded(object sender, RoutedEventArgs e)
    {
      if (this.Visibility == Visibility.Collapsed || this.View == null || !this.View.PostponedSearchControlFocus)
        return;
      this.Focus();
      this.View.PostponedSearchControlFocus = false;
    }

    private void OnVisibilityChanged(Visibility oldValue, Visibility newValue)
    {
      if (oldValue == newValue || this.View == null || this.Visibility != Visibility.Collapsed)
        return;
      this.SetFocusOnCellEditor();
      if (!this.View.SearchPanelClearOnClose || this.View.DataControl.IsDeserializing)
        return;
      this.SearchText = (string) null;
      this.OnFindCommandExecuted();
    }

    private void BindShowGroupPanel(DataViewBase view)
    {
      if (!(view is GridViewBase))
        return;
      Binding binding = new Binding() { Source = (object) view, Path = new PropertyPath(GridViewBase.ShowGroupPanelProperty.GetName(), new object[0]) };
      this.SetBinding(GridSearchControl.GroupPanelShownProperty, (BindingBase) binding);
    }

    protected override void BindSearchPanel(DataViewBase view)
    {
      base.BindSearchPanel(view);
      Binding binding1 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.ShowSearchPanelMRUButtonProperty.GetName(), new object[0]) };
      this.SetBinding(SearchControl.ShowMRUButtonProperty, (BindingBase) binding1);
      Binding binding2 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.ShowSearchPanelFindButtonProperty.GetName(), new object[0]) };
      this.SetBinding(SearchControl.ShowFindButtonProperty, (BindingBase) binding2);
      Binding binding3 = new Binding() { Source = (object) view.Commands, Path = new PropertyPath("HideSearchPanel", new object[0]) };
      this.SetBinding(SearchControl.CloseCommandProperty, (BindingBase) binding3);
      Binding binding4 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.ShowSearchPanelCloseButtonProperty.GetName(), new object[0]) };
      this.SetBinding(GridSearchControl.ActualShowButtonCloseProperty, (BindingBase) binding4);
      this.ShowCloseButton = this.ActualShowButtonClose;
      Binding binding5 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.ShowSearchPanelModeProperty.GetName(), new object[0]) };
      this.SetBinding(GridSearchControl.ShowSearchPanelModeProperty, (BindingBase) binding5);
      Binding binding6 = new Binding() { Source = (object) this, Path = new PropertyPath(UIElement.VisibilityProperty.GetName(), new object[0]) };
      this.SetBinding(GridSearchControl.ActualVisibilityProperty, (BindingBase) binding6);
      Binding binding7 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.SearchPanelImmediateMRUPopupProperty.GetName(), new object[0]) };
      this.SetBinding(SearchControl.ImmediateMRUPopupProperty, (BindingBase) binding7);
      Binding binding8 = new Binding() { Source = (object) view, Path = new PropertyPath(DataViewBase.ShowSearchPanelNavigationButtonsProperty.GetName(), new object[0]) };
      this.SetBinding(SearchControl.ShowSearchPanelNavigationButtonsProperty, (BindingBase) binding8);
      Binding binding9 = new Binding() { Source = (object) view.Commands, Path = new PropertyPath("SearchResultNext", new object[0]) };
      this.SetBinding(SearchControl.NextCommandProperty, (BindingBase) binding9);
      Binding binding10 = new Binding() { Source = (object) view.Commands, Path = new PropertyPath("SearchResultPrev", new object[0]) };
      this.SetBinding(SearchControl.PrevCommandProperty, (BindingBase) binding10);
      this.BindShowGroupPanel(view);
    }

    [Browsable(false)]
    public bool ShouldSerializeMRU(XamlDesignerSerializationManager manager)
    {
      return false;
    }

    [Browsable(false)]
    public bool ShouldSerializeImmediateMRUPopup(XamlDesignerSerializationManager manager)
    {
      return false;
    }
  }
}
