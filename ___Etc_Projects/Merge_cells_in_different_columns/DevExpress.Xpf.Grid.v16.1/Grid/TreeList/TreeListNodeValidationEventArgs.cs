// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListNodeValidationEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.ValidateNode" /> event.
  /// </para>
  ///             </summary>
  public class TreeListNodeValidationEventArgs : GridRowValidationEventArgs, IDataRowEventArgs
  {
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
        return ((TreeListView) this.view).GetNodeByRowHandle(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListNodeValidationEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="value">
    /// An object that enumerates the editor's value. This value is assigned to the <see cref="P:DevExpress.Xpf.Editors.ValidationEventArgs.Value" /> property.
    /// 
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that identifies the processed node by its handle.
    /// 
    ///           </param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.TreeListView" /> object that specifies the view.
    /// 
    /// 
    ///           </param>
    public TreeListNodeValidationEventArgs(object value, int rowHandle, TreeListView view)
      : base((object) view, value, rowHandle, (DataViewBase) view)
    {
    }
  }
}
