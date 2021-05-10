// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListCellValidationEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.ValidateCell" /> event.
  /// </para>
  ///             </summary>
  public class TreeListCellValidationEventArgs : GridCellValidationEventArgs, IDataCellEventArgs
  {
    /// <summary>
    ///                 <para>Gets the processed node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that represents the processed node.
    /// </value>
    public TreeListNode Node
    {
      get
      {
        return ((TreeListView) this.view).GetNodeByRowHandle(this.RowHandle);
      }
    }

    /// <summary>
    ///     <para> </para>
    /// </summary>
    /// <param name="source">
    /// 
    /// </param>
    /// <param name="value">
    /// 
    /// </param>
    /// <param name="rowHandle">
    /// 
    /// </param>
    /// <param name="view">
    /// 
    /// </param>
    /// <param name="column">
    /// 
    /// </param>
    public TreeListCellValidationEventArgs(object source, object value, int rowHandle, TreeListView view, ColumnBase column)
      : base(source, value, rowHandle, (DataViewBase) view, column)
    {
    }
  }
}
