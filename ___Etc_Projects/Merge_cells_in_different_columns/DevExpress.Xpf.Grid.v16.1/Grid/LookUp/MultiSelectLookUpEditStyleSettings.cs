// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LookUp.MultiSelectLookUpEditStyleSettings
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Internal;
using DevExpress.Xpf.Editors.Popups;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid.LookUp
{
  /// <summary>
  ///                 <para>Defines the appearance and behavior of the <b>MultiSelectLookUpEdit</b>.
  /// </para>
  ///             </summary>
  public class MultiSelectLookUpEditStyleSettings : LookUpEditStyleSettings
  {
    protected override SelectionMode GetSelectionMode(LookUpEditBase editor)
    {
      return SelectionMode.Multiple;
    }

    protected override SelectionEventMode GetSelectionEventMode(ISelectorEdit edit)
    {
      return SelectionEventMode.MouseDown;
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
      ((PopupBaseEdit) editor).PopupFooterButtons = new PopupFooterButtons?(PopupFooterButtons.OkCancel);
    }
  }
}
