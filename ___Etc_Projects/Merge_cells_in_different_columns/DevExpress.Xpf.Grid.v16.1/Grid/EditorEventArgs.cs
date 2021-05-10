// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.EditorEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridViewBase.ShownEditor" /> and <see cref="E:DevExpress.Xpf.Grid.GridViewBase.HiddenEditor" /> event.
  /// </para>
  ///             </summary>
  public class EditorEventArgs : EditorEventArgsBase
  {
    private GridControl Grid
    {
      get
      {
        return (GridControl) this.view.DataControl;
      }
    }

    /// <summary>
    ///                 <para>Gets the data row for which an event has been raised.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Object" /> specifying the data row.
    /// 
    /// </value>
    public object Row
    {
      get
      {
        return this.Grid.GetRow(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets a grid column, for which an event has been raised.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> instance.
    /// </value>
    public GridColumn Column
    {
      get
      {
        return (GridColumn) base.Column;
      }
    }

    /// <summary>
    ///                 <para>Gets or sets an editor, for which an event has been raised.
    /// </para>
    ///             </summary>
    /// <value>An object that implements the <see cref="T:DevExpress.Xpf.Editors.IBaseEdit" /> interface.
    /// </value>
    public IBaseEdit Editor { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the EditorEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> descendant that represents a view.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// A zero-based integer value that specifies the row handle. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.EditorEventArgsBase.RowHandle" /> property.
    /// 
    ///           </param>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.EditorEventArgsBase.Column" /> property.
    /// 
    ///           </param>
    /// <param name="editor">
    /// The in-place editor for which an event has been raised. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.EditorEventArgs.Editor" /> property.
    /// 
    /// 
    ///           </param>
    public EditorEventArgs(GridViewBase view, int rowHandle, GridColumn column, IBaseEdit editor)
      : base(GridViewBase.ShownEditorEvent, (DataViewBase) view, rowHandle, (ColumnBase) column)
    {
      this.Editor = editor;
    }
  }
}
