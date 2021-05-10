// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.LookUpEdit
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Utils.Design.DataAccess;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Native;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.LookUp.Native;
using DevExpress.Xpf.Utils;
using DevExpress.Xpf.Utils.About;
using System;
using System.Collections;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Represents a lookup editor.
  /// </para>
  ///             </summary>
  [DataAccessMetadata("All", EnableInMemoryCollectionViewBinding = false, SupportedProcessingModes = "GridLookup")]
  [ToolboxTabName("DX.16.1: Common Controls")]
  [DXToolboxBrowsable(true)]
  [LicenseProvider(typeof (DX_WPF_LicenseProvider))]
  public class LookUpEdit : LookUpEditBase
  {
    /// <summary>
    ///                 <para>Gets the default minimum allowed height for the editor's dropdown. To specify the dropdown's minimum height, use the <see cref="P:DevExpress.Xpf.Editors.PopupBaseEdit.PopupMinHeight" /> property.
    /// 
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public const double DefaultPopupMinHeight = 300.0;
    /// <summary>
    ///                 <para>Gets the default minimum allowed width for the editor's dropdown. To specify the dropdown's minimum width, use the <see cref="P:DevExpress.Xpf.Editors.PopupBaseEdit.PopupMinWidth" /> property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public const double DefaultPopupMinWidth = 200.0;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsPopupAutoWidthProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AutoPopulateColumnsProperty;

    protected override Type StyleSettingsType
    {
      get
      {
        return typeof (LookUpEditStyleSettings);
      }
    }

    protected internal LookUpEditStrategy EditStrategy
    {
      get
      {
        return base.EditStrategy as LookUpEditStrategy;
      }
      set
      {
        this.EditStrategy = value;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the dropdown's width matches the width of the edit box. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the dropdown's width matches the width of the edit box; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("LookUpEditIsPopupAutoWidth")]
    public bool IsPopupAutoWidth
    {
      get
      {
        return (bool) this.GetValue(LookUpEdit.IsPopupAutoWidthProperty);
      }
      set
      {
        this.SetValue(LookUpEdit.IsPopupAutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether columns should be created automatically for all fields in a data source. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to automatically create columns for all fields in a data source; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("LookUpEditAutoPopulateColumns")]
    public bool AutoPopulateColumns
    {
      get
      {
        return (bool) this.GetValue(LookUpEdit.AutoPopulateColumnsProperty);
      }
      set
      {
        this.SetValue(LookUpEdit.AutoPopulateColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the collection of items that match the text typed by an end-user into the edit box.
    /// </para>
    ///             </summary>
    /// <value>The collection of filtered items.</value>
    [DevExpressXpfGridLocalizedDescription("LookUpEditFilteredItems")]
    public IEnumerable FilteredItems
    {
      get
      {
        return this.ItemsProvider.VisibleListSource;
      }
    }

    protected override bool ShouldApplyPopupSize
    {
      get
      {
        return this.IsPopupAutoWidth;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an object that defines the appearance and behavior of the <see cref="T:DevExpress.Xpf.Grid.LookUp.LookUpEdit" />.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Editors.BaseEditStyleSettings" /> descendant that defines the appearance and behavior of the <see cref="T:DevExpress.Xpf.Grid.LookUp.LookUpEdit" />.
    /// </value>
    [Category("Behavior")]
    [Browsable(true)]
    public new BaseEditStyleSettings StyleSettings
    {
      get
      {
        return base.StyleSettings;
      }
      set
      {
        base.StyleSettings = value;
      }
    }

    internal GridControl GridControl
    {
      get
      {
        return this.GetGridControl();
      }
    }

    static LookUpEdit()
    {
      Type type = typeof (LookUpEdit);
      LookUpEdit.IsPopupAutoWidthProperty = DependencyPropertyManager.Register("IsPopupAutoWidth", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      LookUpEdit.AutoPopulateColumnsProperty = DependencyPropertyManager.Register("AutoPopulateColumns", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true, new PropertyChangedCallback(LookUpEdit.OnAutoPopulateColumnsChanged)));
      EditorSettingsProvider.Default.RegisterUserEditor2(typeof (LookUpEdit), typeof (LookUpEditSettings), (CreateEditorMethod2) (optimized =>
      {
        if (!optimized)
          return (IBaseEdit) new LookUpEdit();
        return (IBaseEdit) new InplaceBaseEdit();
      }), (CreateEditorSettingsMethod) (() => (BaseEditSettings) new LookUpEditSettings()));
      PopupBaseEdit.PopupMinHeightProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 300.0));
      PopupBaseEdit.PopupMinWidthProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 200.0));
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the LookUpEdit class.
    /// </para>
    ///             </summary>
    public LookUpEdit()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (LookUpEdit));
    }

    private static void OnAutoPopulateColumnsChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      ((LookUpEdit) d).OnAutoPopulateColumnsChanged((bool) e.NewValue);
    }

    /// <summary>
    ///                 <para>Returns an embedded <see cref="T:DevExpress.Xpf.Grid.GridControl" />.
    /// </para>
    ///             </summary>
    /// <returns>A <see cref="T:DevExpress.Xpf.Grid.GridControl" /> object that represents the DXGrid control for WPF.
    /// </returns>
    public GridControl GetGridControl()
    {
      return LookUpEditHelper.GetVisualClient((PopupBaseEdit) this).InnerEditor as GridControl;
    }

    protected override void BeforePopupOpened()
    {
      base.BeforePopupOpened();
      this.EditStrategy.SetInitialPopupSize();
    }

    internal void SetInitialPopupSizeInternal()
    {
      if (double.IsNaN(this.PopupWidth) && this.IsPopupAutoWidth)
        this.PopupWidth = this.ActualWidth;
      if (!double.IsNaN(this.PopupHeight))
        return;
      this.PopupHeight = this.PopupMinHeight;
    }

    protected override bool IsClosePopupWithAcceptGesture(Key key, ModifierKeys modifiers)
    {
      if (base.IsClosePopupWithAcceptGesture(key, modifiers))
        return !this.EditStrategy.AllowPopupProcessGestures(key, modifiers);
      return false;
    }

    protected override bool IsClosePopupWithCancelGesture(Key key, ModifierKeys modifiers)
    {
      if (base.IsClosePopupWithCancelGesture(key, modifiers))
        return !this.EditStrategy.AllowPopupProcessGestures(key, modifiers);
      return false;
    }

    protected override bool IsTogglePopupOpenGesture(Key key, ModifierKeys modifiers)
    {
      if (base.IsTogglePopupOpenGesture(key, modifiers))
        return !this.EditStrategy.AllowPopupProcessGestures(key, modifiers);
      return false;
    }

    protected override EditStrategyBase CreateEditStrategy()
    {
      return (EditStrategyBase) new LookUpEditStrategy(this);
    }

    protected override bool CanShowPopupCore()
    {
      return true;
    }

    protected virtual void OnAutoPopulateColumnsChanged(bool newValue)
    {
      if (!newValue || this.GridControl == null)
        return;
      this.GridControl.PopulateColumns();
    }

    protected override void OnPopupIsKeyboardFocusWithinChanged(EditorPopupBase popupBase)
    {
      if (!this.CanClose())
        return;
      base.OnPopupIsKeyboardFocusWithinChanged(popupBase);
    }

    protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
    {
      if (!this.CanClose())
        return;
      base.OnIsKeyboardFocusWithinChanged(e);
    }

    private bool CanClose()
    {
      return this.GridControl == null || this.GridControl.View == null || !this.GridControl.View.IsFilterControlOpened;
    }

    protected override VisualClientOwner CreateVisualClient()
    {
      return (VisualClientOwner) new GridControlVisualClientOwner((PopupBaseEdit) this);
    }
  }
}
