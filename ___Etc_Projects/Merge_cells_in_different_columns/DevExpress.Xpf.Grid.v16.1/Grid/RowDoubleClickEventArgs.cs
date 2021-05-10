// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowDoubleClickEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TableView.RowDoubleClick" /> event handler.
  /// </para>
  ///             </summary>
  public class RowDoubleClickEventArgs : RoutedEventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets the information on a view element located under the mouse pointer.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridViewHitInfoBase" /> descendant which provides information on a view element located under the mouse pointer.
    /// </value>
    public GridViewHitInfoBase HitInfo { get; private set; }

    /// <summary>
    ///                 <para>Gets the View that raised the event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that raised the event.
    /// </value>
    public DataViewBase Source { get; private set; }

    /// <summary>
    ///                 <para>Gets a mouse button that has been pressed twice.
    /// </para>
    ///             </summary>
    /// <value>The mouse button that has been pressed twice.</value>
    public MouseButton ChangedButton { get; private set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the RowDoubleClickEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="hitInfo">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridViewHitInfoBase" /> descendant that contains information about the row.
    /// 
    ///           </param>
    /// <param name="changedButton">
    /// A <see cref="T:System.Windows.Input.MouseButton" /> object that is the mouse button associated with the event.
    /// 
    ///           </param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> object that represents the grid's view.
    /// 
    ///           </param>
    public RowDoubleClickEventArgs(GridViewHitInfoBase hitInfo, MouseButton changedButton, DataViewBase view)
    {
      this.HitInfo = hitInfo;
      this.Source = view;
      this.ChangedButton = changedButton;
    }
  }
}
