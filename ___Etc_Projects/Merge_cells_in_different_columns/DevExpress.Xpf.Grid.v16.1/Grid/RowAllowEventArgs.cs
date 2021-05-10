// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowAllowEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowExpanding" /> and <see cref="E:DevExpress.Xpf.Grid.GridControl.GroupRowCollapsing" /> events.
  /// </para>
  ///             </summary>
  public class RowAllowEventArgs : RowEventArgs
  {
    /// <summary>
    ///                 <para>Gets or sets whether the operation is allowed.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> to allow the operation; otherwise, <b>false</b>.
    /// </value>
    public bool Allow { get; set; }

    /// <summary>
    ///                 <para>Initializes a new instance of the RowAllowEventArgs class.
    /// </para>
    ///             </summary>
    /// <param name="routedEvent">The routed event.</param>
    /// <param name="view">
    /// A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that owns the processed row.
    /// 
    ///           </param>
    /// <param name="rowHandle">
    /// An integer value that specifies the processed row's handle. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.RowEventArgs.RowHandle" /> property.
    /// 
    ///           </param>
    public RowAllowEventArgs(RoutedEvent routedEvent, GridViewBase view, int rowHandle)
      : base(routedEvent, view, rowHandle)
    {
      this.Allow = true;
    }
  }
}
