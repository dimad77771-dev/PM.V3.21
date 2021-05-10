// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.DescriptorAndGridControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Data;
using DevExpress.Xpf.Grid.Native;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid.Printing
{
  public class DescriptorAndGridControl : IDescriptorAndDataControlBase
  {
    private int currentRowHandle = int.MinValue;
    private DataControlDetailDescriptor descriptor;
    private BindingBase itemsSourceBinding;
    private GridControl grid;

    public DataControlBase Grid
    {
      get
      {
        return (DataControlBase) this.grid;
      }
    }

    public DetailDescriptorBase Descriptor
    {
      get
      {
        return (DetailDescriptorBase) this.descriptor;
      }
    }

    internal DescriptorAndGridControl(DataControlDetailDescriptor descriptor)
    {
      this.descriptor = descriptor;
    }

    private void InitializeGrid(DataControlDetailInfo detailInfo, PrintingDataTreeBuilderBase treeBuilder)
    {
      if (detailInfo.DataControl == null)
        detailInfo.UpdateDataControl();
      this.itemsSourceBinding = this.descriptor.GetItemsSourceBinding();
      this.grid = (GridControl) this.descriptor.DataControl.CloneDetailForPrint(treeBuilder.View.MasterRootNodeContainer, treeBuilder.View.MasterRootRowsContainer);
      this.descriptor.DataControl.PopulateColumnsIfNeeded(detailInfo.DataControl.DataProviderBase);
      this.descriptor.DataControl.CopyToDetail((DataControlBase) this.grid);
      this.grid.LockUpdateLayout = true;
    }

    private void UpdateClonedDetail(DataControlDetailInfo detailInfo, PrintingDataTreeBuilderBase treeBuilder)
    {
      this.grid.DataContext = treeBuilder.ReusingRowData.Row;
      this.grid.DataControlParent = (IDataControlParent) detailInfo;
      if (this.itemsSourceBinding != null)
        this.grid.SetBinding(DataControlBase.ItemsSourceProperty, this.itemsSourceBinding);
      this.grid.PopulateColumnsIfNeeded((DataProviderBase) this.grid.GridDataProvider);
      this.grid.UpdateTotalSummary();
      this.grid.View.UpdateColumnsViewInfo(false);
    }

    public DataControlBase GetDetailGridControl(PrintingDataTreeBuilderBase treeBuilder, out bool isGenerated)
    {
      isGenerated = true;
      if (treeBuilder.ReusingRowData.RowHandle.Value == this.currentRowHandle)
        return (DataControlBase) this.grid;
      this.currentRowHandle = treeBuilder.ReusingRowData.RowHandle.Value;
      RowDetailInfoBase detailInfoForPrinting = MasterDetailPrintHelper.GetRowDetailInfoForPrinting((GridPrintingDataTreeBuilder) treeBuilder, treeBuilder.ReusingRowData.RowHandle.Value);
      DataControlDetailInfo detailInfo = detailInfoForPrinting as DataControlDetailInfo ?? this.GetDetailInfo(detailInfoForPrinting);
      if (this.grid == null)
        this.InitializeGrid(detailInfo, treeBuilder);
      this.UpdateClonedDetail(detailInfo, treeBuilder);
      return (DataControlBase) this.grid;
    }

    private DataControlDetailInfo GetDetailInfo(RowDetailInfoBase detailInfoBase)
    {
      return this.descriptor.CreateRowDetailInfo(((DetailInfoWithContent) detailInfoBase).container) as DataControlDetailInfo;
    }
  }
}
