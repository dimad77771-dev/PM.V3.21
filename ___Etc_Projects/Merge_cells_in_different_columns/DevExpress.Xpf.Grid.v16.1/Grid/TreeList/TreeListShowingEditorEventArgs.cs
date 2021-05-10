// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListShowingEditorEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.ShowingEditor" /> event.
  /// </para>
  ///             </summary>
  public class TreeListShowingEditorEventArgs : ShowingEditorEventArgsBase
  {
    /// <summary>
    ///                 <para>Gets the node that contains a cell whose editor is about to be invoked.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the node for which an event has been raised.
    /// 
    /// </value>
    public TreeListNode Node
    {
      get
      {
        return ((TreeListView) this.view).TreeListDataProvider.GetNodeByRowHandle(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListShowingEditorEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> object that specifies the view.
    /// 
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that identifies the node by its handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column containing the cell.$
    /// 
    /// 
    ///           </param>
    public TreeListShowingEditorEventArgs(TreeListView view, int rowHandle, ColumnBase column)
      : base(TreeListView.ShowingEditorEvent, (DataViewBase) view, rowHandle, column)
    {
    }
  }
}
