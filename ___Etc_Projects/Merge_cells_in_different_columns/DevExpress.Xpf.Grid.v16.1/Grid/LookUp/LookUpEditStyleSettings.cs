// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.LookUpEditStyleSettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Editors.Native;
using DevExpress.Xpf.Editors.Popups;
using DevExpress.Xpf.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Defines the native appearance and behavior of the <see cref="T:DevExpress.Xpf.Grid.LookUp.LookUpEdit" />.
  /// </para>
  ///             </summary>
  public class LookUpEditStyleSettings : BaseLookUpStyleSettings
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowTotalSummaryProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowColumnHeadersProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ShowGroupPanelProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowSortingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowGroupingProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty AllowColumnFilteringProperty;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty SelectionModeProperty;

    /// <summary>
    ///                 <para>Gets or sets whether the Summary Panel is displayed within an embedded grid. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the Total Summary Panel within an embedded grid; otherwise, <b>false</b>.
    /// </value>
    public bool ShowTotalSummary
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.ShowTotalSummaryProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.ShowTotalSummaryProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether column headers are displayed within an embedded grid. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display column headers; otherwise, <b>false</b>.
    /// </value>
    public bool ShowColumnHeaders
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.ShowColumnHeadersProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.ShowColumnHeadersProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the Group Panel is displayed within an embedded grid. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to display the Group Panel; otherwise, <b>false</b>.
    /// </value>
    public bool ShowGroupPanel
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.ShowGroupPanelProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.ShowGroupPanelProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a value that specifies whether an end-user can group data within an embedded grid control. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to group data; otherwise, <b>false</b>.
    /// </value>
    public bool AllowGrouping
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.AllowGroupingProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.AllowGroupingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can sort data within an embedded grid. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to sort data; otherwise, <b>false</b>.
    /// </value>
    public bool AllowSorting
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.AllowSortingProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.AllowSortingProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether an end-user can filter data by column. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow filtering data by column; otherwise, <b>false</b>.
    /// </value>
    public bool AllowColumnFiltering
    {
      get
      {
        return (bool) this.GetValue(LookUpEditStyleSettings.AllowColumnFilteringProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.AllowColumnFilteringProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the selection behavior for the <see cref="T:DevExpress.Xpf.Grid.LookUp.LookUpEdit" />. This is a dependency property.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Controls.SelectionMode" /> enumeration value.
    /// </value>
    public SelectionMode SelectionMode
    {
      get
      {
        return (SelectionMode) this.GetValue(LookUpEditStyleSettings.SelectionModeProperty);
      }
      set
      {
        this.SetValue(LookUpEditStyleSettings.SelectionModeProperty, (object) value);
      }
    }

    protected internal virtual bool ShowSearchPanel
    {
      get
      {
        return false;
      }
    }

    protected override FilterByColumnsMode FilterByColumnsMode
    {
      get
      {
        return FilterByColumnsMode.Custom;
      }
    }

    static LookUpEditStyleSettings()
    {
      Type ownerType = typeof (LookUpEditStyleSettings);
      LookUpEditStyleSettings.SelectionModeProperty = DependencyPropertyManager.Register("SelectionMode", typeof (SelectionMode), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) SelectionMode.Single));
      LookUpEditStyleSettings.ShowColumnHeadersProperty = DependencyPropertyManager.Register("ShowColumnHeaders", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      LookUpEditStyleSettings.ShowTotalSummaryProperty = DependencyPropertyManager.Register("ShowTotalSummary", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      LookUpEditStyleSettings.ShowGroupPanelProperty = DependencyPropertyManager.Register("ShowGroupPanel", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
      LookUpEditStyleSettings.AllowGroupingProperty = DependencyPropertyManager.Register("AllowGrouping", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      LookUpEditStyleSettings.AllowSortingProperty = DependencyPropertyManager.Register("AllowSorting", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      LookUpEditStyleSettings.AllowColumnFilteringProperty = DependencyPropertyManager.Register("AllowColumnFiltering", typeof (bool), ownerType, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
    }

    protected override Style GetItemContainerStyle(LookUpEditBase edit)
    {
      return edit.ItemContainerStyle;
    }

    protected override SelectionMode GetSelectionMode(LookUpEditBase editor)
    {
      return this.SelectionMode;
    }

    protected override SelectionEventMode GetSelectionEventMode(ISelectorEdit edit)
    {
      return !((LookUpEditBase) edit).AllowItemHighlighting ? SelectionEventMode.MouseDown : SelectionEventMode.MouseEnter;
    }

    /// <summary>
    ///                 <para>Indicates which buttons are displayed within an editor's popup.
    /// </para>
    ///             </summary>
    /// <param name="editor">
    /// A <see cref="T:DevExpress.Xpf.Editors.PopupBaseEdit" /> descendant that is the dropdown editor.
    /// 
    ///           </param>
    /// <returns>The <see cref="F:DevExpress.Xpf.Editors.PopupFooterButtons.None" /> value.
    /// </returns>
    public override PopupFooterButtons GetPopupFooterButtons(PopupBaseEdit editor)
    {
      return this.SelectionMode != SelectionMode.Single ? PopupFooterButtons.OkCancel : PopupFooterButtons.None;
    }

    protected override bool GetShowSizeGrip(PopupBaseEdit editor)
    {
      return true;
    }

    protected internal virtual void SyncValues(LookUpEditBase editor, GridControl grid)
    {
      if (LookUpEditHelper.GetIsServerMode(editor))
      {
        if (this.GetSelectionMode(editor) == SelectionMode.Single)
          grid.View.SelectRowByValue(editor.ValueMember, LookUpEditHelper.GetEditValue((ISelectorEdit) editor));
        else
          grid.View.SelectRowsByValues(editor.ValueMember, (IEnumerable<object>) LookUpEditHelper.GetEditValue((ISelectorEdit) editor));
      }
      else if (this.GetSelectionMode(editor) == SelectionMode.Single)
      {
        object selectedItem = LookUpEditHelper.GetSelectedItem((ISelectorEdit) editor);
        if (selectedItem != null)
          grid.SetCurrentItemCore(selectedItem);
        else
          grid.View.FocusedRowHandle = grid.GetRowHandleByVisibleIndex(0);
      }
      else
      {
        IEnumerable<object> source = LookUpEditHelper.GetSelectedItems(editor).Cast<object>();
        grid.ResetSelectedItems((IList) source.ToList<object>());
      }
    }

    protected override bool GetIncrementalFiltering()
    {
      return true;
    }
  }
}
