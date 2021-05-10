// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListColumnCollection
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The treelist's column collection.
  /// 
  /// </para>
  ///             </summary>
  public class TreeListColumnCollection : ColumnCollectionBase<TreeListColumn>
  {
    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListColumnCollection class.
    /// </para>
    ///             </summary>
    /// <param name="treeList">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListControl" /> object that owns the collection of columns.
    /// 
    ///           </param>
    public TreeListColumnCollection(TreeListControl treeList)
      : base((DataControlBase) treeList)
    {
    }
  }
}
