// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.SearchLookUpEditStyleSettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Utils;
using System.Windows;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Defines the appearance and behavior of the <b>SearchLookUpEdit</b>.
  /// </para>
  ///             </summary>
  public class SearchLookUpEditStyleSettings : LookUpEditStyleSettings
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsTextEditableProperty = DependencyPropertyManager.Register("IsTextEditable", typeof (bool?), typeof (SearchLookUpEditStyleSettings), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((SearchLookUpEditStyleSettings) d).IsTextEditableChanged((bool?) e.NewValue))));

    /// <summary>
    ///                 <para>Gets or sets whether an end-user is allowed to edit the text displayed within the edit box. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow an end-user to edit the text displayed within the edit box; otherwise, <b>false</b>.
    /// </value>
    public bool? IsTextEditable
    {
      get
      {
        return (bool?) this.GetValue(SearchLookUpEditStyleSettings.IsTextEditableProperty);
      }
      set
      {
        this.SetValue(SearchLookUpEditStyleSettings.IsTextEditableProperty, (object) value);
      }
    }

    protected override bool ShouldFocusPopup
    {
      get
      {
        return true;
      }
    }

    protected internal override bool ShowSearchPanel
    {
      get
      {
        return true;
      }
    }

    protected override FilterByColumnsMode FilterByColumnsMode
    {
      get
      {
        return FilterByColumnsMode.Default;
      }
    }

    protected override FilterCondition FilterCondition
    {
      get
      {
        return FilterCondition.Contains;
      }
    }

    /// <summary>
    ///                 <para>Assigns the editor settings to the specified editor.
    /// </para>
    ///             </summary>
    /// <param name="editor">
    /// A <see cref="T:DevExpress.Xpf.Editors.BaseEdit" /> class descendant that is the target editor.
    /// 
    ///           </param>
    public override void ApplyToEdit(BaseEdit editor)
    {
      base.ApplyToEdit(editor);
      LookUpEdit o = (LookUpEdit) editor;
      o.IsTextEditable = new bool?(this.IsTextEditable.HasValue && this.IsTextEditable.Value);
      if (o.IsPropertyAssigned(LookUpEditBase.ImmediatePopupProperty))
        return;
      o.ImmediatePopup = true;
    }

    protected override EditorPlacement GetFindButtonPlacement(LookUpEditBase editor)
    {
      return EditorPlacement.Popup;
    }

    protected override EditorPlacement GetNullValueButtonPlacement(LookUpEditBase editor)
    {
      return EditorPlacement.None;
    }

    protected override EditorPlacement GetAddNewButtonPlacement(LookUpEditBase editor)
    {
      return EditorPlacement.None;
    }

    protected virtual void IsTextEditableChanged(bool? newValue)
    {
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <param name="editor">
    /// 
    /// </param>
    /// <returns> </returns>
    public override bool GetIsTextEditable(ButtonEdit editor)
    {
      return false;
    }
  }
}
