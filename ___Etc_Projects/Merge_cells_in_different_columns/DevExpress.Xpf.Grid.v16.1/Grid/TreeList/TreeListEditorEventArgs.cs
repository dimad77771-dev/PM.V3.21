// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListEditorEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.ShownEditor" /> and <see cref="E:DevExpress.Xpf.Grid.TreeListView.HiddenEditor" /> events.
  /// </para>
  ///             </summary>
  public class TreeListEditorEventArgs : EditorEventArgsBase
  {
    /// <summary>
    ///                 <para>Gets the processed in-place editor.
    /// </para>
    ///             </summary>
    /// <value>The processed in-place editor.</value>
    public IBaseEdit Editor { get; private set; }

    /// <summary>
    ///                 <para>Gets the processed node.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that specifies the processed node.
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
    ///                 <para>Initializes a new instance of the TreeListEditorEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> object that is the owner View.
    /// 
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that identifies the processed node by its handle.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.ColumnBase" /> descendant that is the column which owns the processed cell.
    /// 
    /// 
    ///           </param>
    /// <param name="editor">
    /// The processed editor. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.TreeList.TreeListEditorEventArgs.Editor" /> property.
    /// 
    ///           </param>
    public TreeListEditorEventArgs(TreeListView view, int rowHandle, ColumnBase column, IBaseEdit editor)
      : base(TreeListView.ShownEditorEvent, (DataViewBase) view, rowHandle, column)
    {
      this.Editor = editor;
    }
  }
}
