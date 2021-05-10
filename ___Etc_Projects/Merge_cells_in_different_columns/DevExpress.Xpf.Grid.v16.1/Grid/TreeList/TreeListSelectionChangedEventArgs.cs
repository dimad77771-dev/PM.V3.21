// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListSelectionChangedEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TreeListView.SelectionChanged" /> event.
  /// </para>
  ///             </summary>
  public class TreeListSelectionChangedEventArgs : GridSelectionChangedEventArgs
  {
    /// <summary>
    ///                 <para>Gets a node whose selected state has been changed.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object that is the node whose selected state has been changed.
    /// </value>
    public TreeListNode Node
    {
      get
      {
        return ((TreeListView) this.Source).GetNodeByRowHandle(this.ControllerRow);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListSelectionChangedEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="view">
    /// The <see cref="T:DevExpress.Xpf.Grid.TreeListView" />.
    /// 
    ///           </param>
    /// <param name="action">
    /// A <see cref="T:System.ComponentModel.CollectionChangeAction" /> enumeration value that specifies how the grid's selection has been changed.
    /// 
    ///           </param>
    /// <param name="controllerRow">
    /// A handle of the node whose selected state has been changed.
    /// 
    ///           </param>
    public TreeListSelectionChangedEventArgs(TreeListView view, CollectionChangeAction action, int controllerRow)
      : base((DataViewBase) view, action, controllerRow)
    {
    }
  }
}
