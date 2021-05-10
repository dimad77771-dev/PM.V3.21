// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowExpanded" />, <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowCollapsed" />, <see cref="E:DevExpress.Xpf.Grid.GridViewBase.RowUpdated" /> and <see cref="E:DevExpress.Xpf.Grid.GridViewBase.RowCanceled" /> events.
  /// </para>
  ///             </summary>
  public class RowEventArgs : RoutedEventArgs, IDataRowEventArgs
  {
    protected GridViewBase view;
    protected object forcedRow;

    protected GridControl Grid
    {
      get
      {
        return this.view.Grid;
      }
    }

    /// <summary>
    ///                 <para>Gets the processed row's handle.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the processed row's handle.</value>
    public int RowHandle { get; protected set; }

    /// <summary>
    ///                 <para>Gets the processed row.
    /// </para>
    ///             </summary>
    /// <value>An object that represents the processed row.</value>
    public object Row
    {
      get
      {
        return this.forcedRow ?? this.Grid.GetRow(this.RowHandle);
      }
    }

    /// <summary>
    ///                 <para>Gets the View that raised the event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that raised the event.
    /// </value>
    public GridViewBase Source
    {
      get
      {
        return this.view;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the RowEventArgs class with the specified settings.
    /// </para>
    ///             </summary>
    /// <param name="routedEvent">The routed event.</param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> descendant that represents the grid's view.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the processed row's handle. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    public RowEventArgs(RoutedEvent routedEvent, GridViewBase view, int rowHandle)
      : base(routedEvent, (object) view)
    {
      this.view = view;
      this.RowHandle = rowHandle;
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the RowEventArgs class with the specified settings.
    /// </para>
    ///             </summary>
    /// <param name="routedEvent">The routed event.</param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewBase" /> descendant that represents the grid's view.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the processed row's handle. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    /// <param name="forcedRow">
    /// An object that represents the data object corresponding to the processed row.
    /// 
    ///           </param>
    public RowEventArgs(RoutedEvent routedEvent, GridViewBase view, int rowHandle, object forcedRow)
      : this(routedEvent, view, rowHandle)
    {
      this.forcedRow = forcedRow;
    }
  }
}
