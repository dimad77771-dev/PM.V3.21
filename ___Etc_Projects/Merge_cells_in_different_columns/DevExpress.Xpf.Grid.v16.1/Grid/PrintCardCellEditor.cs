// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintCardCellEditor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintCardCellEditor : PrintCellEditorBase
  {
    protected override bool OptimizeEditorPerformance
    {
      get
      {
        return false;
      }
    }

    protected override bool ShouldSyncCellContentPresenterProperties
    {
      get
      {
        return false;
      }
    }

    protected override IBaseEdit CreateEditor(BaseEditSettings settings)
    {
      return settings.CreateEditor(false, (IDefaultEditorViewInfo) this.EditorColumn, this.GetEditorOptimizationMode());
    }

    protected override void InitializeBaseEdit(IBaseEdit newEdit, InplaceEditorBase.BaseEditSourceType newBaseEditSourceType)
    {
      base.InitializeBaseEdit(newEdit, newBaseEditSourceType);
      ((FrameworkElement) this.editCore).Style = GridPrintingHelper.GetPrintCellInfo((DependencyObject) this.CellData).PrintCellStyle;
      TextEdit textEdit = newEdit as TextEdit;
      if (textEdit != null)
      {
        textEdit.TextWrapping = TextWrapping.Wrap;
        textEdit.PrintTextWrapping = new TextWrapping?(TextWrapping.Wrap);
      }
      newEdit.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = !((BaseEdit) this.editCore).AllowUpdateTextBlockWhenPrinting;
      if (this.Background == null)
        return;
      this.UpdateBackground();
    }

    protected override void UpdateDisplayTemplate(bool updateForce = false)
    {
    }
  }
}
