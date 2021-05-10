// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.InitNewRowEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TableView.InitNewRow" /> event.
  /// </para>
  ///             </summary>
  public class InitNewRowEventArgs : RoutedEventArgs
  {
    /// <summary>
    ///                 <para>Gets the handle of the added row.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the added row's handle.</value>
    public int RowHandle { get; private set; }

    /// <summary>
    ///                 <para>Inititalize a new instance of the <see cref="T:DevExpress.Xpf.Grid.InitNewRowEventArgs" /> class.
    /// </para>
    ///             </summary>
    /// <param name="routedEvent">The routed event.</param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that raised the event.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value representing the handle of the added row. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.InitNewRowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    public InitNewRowEventArgs(RoutedEvent routedEvent, DataViewBase view, int rowHandle)
      : base(routedEvent, (object) view)
    {
      this.RowHandle = rowHandle;
    }
  }
}
