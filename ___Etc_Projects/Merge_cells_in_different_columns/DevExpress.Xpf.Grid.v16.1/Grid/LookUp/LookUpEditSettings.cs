// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.LookUpEditSettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Contains settings specific to a lookup editor.
  /// </para>
  ///             </summary>
  public class LookUpEditSettings : LookUpEditSettingsBase
  {
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

    /// <summary>
    ///                 <para>Gets or sets whether the dropdown's width matches the width of the edit box. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the dropdown's width matches the width of the edit box; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("LookUpEditSettingsIsPopupAutoWidth")]
    public bool IsPopupAutoWidth
    {
      get
      {
        return (bool) this.GetValue(LookUpEditSettings.IsPopupAutoWidthProperty);
      }
      set
      {
        this.SetValue(LookUpEditSettings.IsPopupAutoWidthProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether columns should be created automatically for all fields in a data source. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to automatically create columns for all fields in a data source; otherwise, <b>false</b>.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("LookUpEditSettingsAutoPopulateColumns")]
    public bool AutoPopulateColumns
    {
      get
      {
        return (bool) this.GetValue(LookUpEditSettings.AutoPopulateColumnsProperty);
      }
      set
      {
        this.SetValue(LookUpEditSettings.AutoPopulateColumnsProperty, (object) value);
      }
    }

    static LookUpEditSettings()
    {
      Type type = typeof (LookUpEditSettings);
      LookUpEditSettings.IsPopupAutoWidthProperty = DependencyPropertyManager.Register("IsPopupAutoWidth", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      LookUpEditSettings.AutoPopulateColumnsProperty = DependencyPropertyManager.Register("AutoPopulateColumns", typeof (bool), type, (PropertyMetadata) new FrameworkPropertyMetadata((object) true));
      EditorSettingsProvider.Default.RegisterUserEditor2(typeof (LookUpEdit), typeof (LookUpEditSettings), (CreateEditorMethod2) (optimized =>
      {
        if (!optimized)
          return (IBaseEdit) new LookUpEdit();
        return (IBaseEdit) new InplaceBaseEdit();
      }), (CreateEditorSettingsMethod) (() => (BaseEditSettings) new LookUpEditSettings()));
      PopupBaseEditSettings.PopupMinHeightProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 300.0));
      PopupBaseEditSettings.PopupMinWidthProperty.OverrideMetadata(type, (PropertyMetadata) new FrameworkPropertyMetadata((object) 200.0));
    }

    protected override void AssignToEditCore(IBaseEdit edit)
    {
      base.AssignToEditCore(edit);
      LookUpEdit lookUp = edit as LookUpEdit;
      if (lookUp == null)
        return;
      this.SetValueFromSettings(LookUpEditSettings.IsPopupAutoWidthProperty, (Action) (() => lookUp.IsPopupAutoWidth = this.IsPopupAutoWidth));
      this.SetValueFromSettings(LookUpEditSettings.AutoPopulateColumnsProperty, (Action) (() => lookUp.AutoPopulateColumns = this.AutoPopulateColumns));
    }
  }
}
