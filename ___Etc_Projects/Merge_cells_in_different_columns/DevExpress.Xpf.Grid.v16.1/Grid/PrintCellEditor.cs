// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.PrintCellEditor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.ConditionalFormatting;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public class PrintCellEditor : PrintCellEditorBase, IConditionalFormattingClient<PrintCellEditor>
  {
    public static readonly DependencyProperty IsTopBorderVisibleProperty = DependencyPropertyManager.Register("IsTopBorderVisible", typeof (bool), typeof (PrintCellEditor), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => ((PrintCellEditor) d).OnIsTopBorderVisibleChanged())));
    public static readonly DependencyProperty DetailLevelProperty = DependencyPropertyManager.Register("DetailLevel", typeof (int), typeof (PrintCellEditor), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0, (PropertyChangedCallback) ((d, e) => ((PrintCellEditor) d).OnIsTopBorderVisibleChanged())));
    internal static readonly ServiceSummaryItem[] EmptySummaries = new ServiceSummaryItem[0];
    private bool isPageUpdaterCreated;
    private ConditionalFormattingHelper<PrintCellEditor> formattingHelper;

    public bool IsTopBorderVisible
    {
      get
      {
        return (bool) this.GetValue(PrintCellEditor.IsTopBorderVisibleProperty);
      }
      set
      {
        this.SetValue(PrintCellEditor.IsTopBorderVisibleProperty, (object) value);
      }
    }

    public int DetailLevel
    {
      get
      {
        return (int) this.GetValue(PrintCellEditor.DetailLevelProperty);
      }
      set
      {
        this.SetValue(PrintCellEditor.DetailLevelProperty, (object) value);
      }
    }

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

    ConditionalFormattingHelper<PrintCellEditor> IConditionalFormattingClient<PrintCellEditor>.FormattingHelper
    {
      get
      {
        return this.formattingHelper;
      }
    }

    bool IConditionalFormattingClient<PrintCellEditor>.IsSelected
    {
      get
      {
        return false;
      }
    }

    bool IConditionalFormattingClient<PrintCellEditor>.IsReady
    {
      get
      {
        return true;
      }
    }

    Locker IConditionalFormattingClient<PrintCellEditor>.Locker
    {
      get
      {
        return this.RowData.conditionalFormattingLocker;
      }
    }

    bool IConditionalFormattingClient<PrintCellEditor>.HasCustomAppearance
    {
      get
      {
        return false;
      }
    }

    public PrintCellEditor()
    {
      this.formattingHelper = new ConditionalFormattingHelper<PrintCellEditor>(this, PrintCellEditorBase.BackgroundProperty);
    }

    private void OnIsTopBorderVisibleChanged()
    {
      if (this.editCore == null)
        return;
      if (this.isPageUpdaterCreated)
        this.ClearUpdater();
      if (this.IsTopBorderVisible)
      {
        BaseEdit baseEdit = (BaseEdit) this.editCore;
        TopBorderOnPageUpdater borderOnPageUpdater1 = new TopBorderOnPageUpdater();
        borderOnPageUpdater1.DetailLevel = this.DetailLevel;
        TopBorderOnPageUpdater borderOnPageUpdater2 = borderOnPageUpdater1;
        ExportSettings.SetOnPageUpdater((DependencyObject) baseEdit, (IOnPageUpdater) borderOnPageUpdater2);
      }
      else
      {
        BaseEdit baseEdit = (BaseEdit) this.editCore;
        InfoProviderOnPageUpdater providerOnPageUpdater1 = new InfoProviderOnPageUpdater();
        providerOnPageUpdater1.DetailLevel = this.DetailLevel;
        InfoProviderOnPageUpdater providerOnPageUpdater2 = providerOnPageUpdater1;
        ExportSettings.SetOnPageUpdater((DependencyObject) baseEdit, (IOnPageUpdater) providerOnPageUpdater2);
      }
      this.isPageUpdaterCreated = true;
    }

    protected override IBaseEdit CreateEditor(BaseEditSettings settings)
    {
      return settings.CreateEditor(false, (IDefaultEditorViewInfo) this.EditorColumn, this.GetEditorOptimizationMode());
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.OnIsTopBorderVisibleChanged();
    }

    private void ClearUpdater()
    {
      ExportSettings.SetOnPageUpdater((DependencyObject) this, (IOnPageUpdater) null);
      this.isPageUpdaterCreated = false;
    }

    protected override void InitializeBaseEdit(IBaseEdit newEdit, InplaceEditorBase.BaseEditSourceType newBaseEditSourceType)
    {
      base.InitializeBaseEdit(newEdit, newBaseEditSourceType);
      ((FrameworkElement) this.editCore).Style = GridPrintingHelper.GetPrintCellInfo((DependencyObject) this.CellData).PrintCellStyle;
      newEdit.ShouldDisableExcessiveUpdatesInInplaceInactiveMode = !((BaseEdit) this.editCore).AllowUpdateTextBlockWhenPrinting;
      if (this.Background != null)
        this.UpdateBackground();
      this.UpdatePrintingMergeValue();
    }

    protected internal override void UpdatePrintingMergeValue()
    {
      if (!this.View.ActualAllowCellMerge)
        return;
      Dictionary<ColumnBase, int> mergeValues = this.RowData.DataRowNode.PrintInfo.MergeValues;
      int num;
      ExportSettings.SetMergeValue(this.editCore as DependencyObject, this.RowData.DataRowNode.PrintInfo.MergeValues.TryGetValue(this.Column, out num) ? (object) num : (object) null);
    }

    protected override void UpdateDisplayTemplate(bool updateForce = false)
    {
    }

    protected override DataTemplate SelectTemplate()
    {
      return (DataTemplate) null;
    }

    protected override void UpdateConditionalAppearance()
    {
      base.UpdateConditionalAppearance();
      this.formattingHelper.UpdateConditionalAppearance();
    }

    IList<FormatConditionBaseInfo> IConditionalFormattingClient<PrintCellEditor>.GetRelatedConditions()
    {
      ITableView tableView = this.View as ITableView;
      if (tableView == null || this.Column == null)
        return (IList<FormatConditionBaseInfo>) null;
      return PrintCellEditor.GetRelatedConditionsCore(tableView, this.Column.FieldName);
    }

    private static IList<FormatConditionBaseInfo> GetRelatedConditionsCore(ITableView tableView, string fieldName)
    {
      IList<FormatConditionBaseInfo> infoByFieldName1 = tableView.FormatConditions.GetInfoByFieldName(string.Empty);
      IList<FormatConditionBaseInfo> infoByFieldName2 = tableView.FormatConditions.GetInfoByFieldName(fieldName);
      if (infoByFieldName1 == null && infoByFieldName2 == null)
        return (IList<FormatConditionBaseInfo>) null;
      if (infoByFieldName1 == null)
        return infoByFieldName2;
      if (infoByFieldName2 == null)
        return infoByFieldName1;
      return (IList<FormatConditionBaseInfo>) infoByFieldName1.Concat<FormatConditionBaseInfo>((IEnumerable<FormatConditionBaseInfo>) infoByFieldName2).ToList<FormatConditionBaseInfo>();
    }

    FormatValueProvider? IConditionalFormattingClient<PrintCellEditor>.GetValueProvider(string fieldName)
    {
      return new FormatValueProvider?(this.RowData.GetValueProvider(fieldName));
    }

    void IConditionalFormattingClient<PrintCellEditor>.UpdateBackground()
    {
    }

    void IConditionalFormattingClient<PrintCellEditor>.UpdateDataBarFormatInfo(DataBarFormatInfo info)
    {
    }

    void IConditionalFormattingClient<PrintCellEditor>.UpdateCustomAppearance(CustomAppearanceEventArgs args)
    {
    }
  }
}
