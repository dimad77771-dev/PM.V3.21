// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridSummaryItemCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>A collection of summary items.
  /// </para>
  ///             </summary>
  public class GridSummaryItemCollection : SummaryItemCollectionBase<GridSummaryItem>
  {
    /// <summary>
    ///                 <para>Initializes a new instance of the GridSummaryItemCollection class.
    /// </para>
    ///             </summary>
    /// <param name="dataControl">
    /// A <see cref="T:DevExpress.Xpf.Grid.DataControlBase" /> descendant that owns the collection.
    /// 
    ///           </param>
    /// <param name="collectionType">
    /// A <see cref="T:DevExpress.Xpf.Grid.SummaryItemCollectionType" /> enumeration value that specifies the type of summaries contained within the collection (Group or Total).
    /// 
    ///           </param>
    public GridSummaryItemCollection(DataControlBase dataControl, SummaryItemCollectionType collectionType)
      : base(dataControl, collectionType)
    {
    }
  }
}
