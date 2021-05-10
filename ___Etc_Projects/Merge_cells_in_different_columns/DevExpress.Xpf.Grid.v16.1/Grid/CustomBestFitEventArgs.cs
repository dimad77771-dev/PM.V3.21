// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CustomBestFitEventArgs
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Provides data for the <see cref="E:DevExpress.Xpf.Grid.TableView.CustomBestFit" /> event.
  /// </para>
  ///             </summary>
  public class CustomBestFitEventArgs : CustomBestFitEventArgsBase
  {
    /// <summary>
    ///                 <para>Provides access to a grid column whose width should be adjusted.
    /// 
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> object that represents the column, for which this event has been raised.
    /// </value>
    public GridColumn Column
    {
      get
      {
        return (GridColumn) this.ColumnCore;
      }
    }

    /// <summary>
    ///                 <para>Gets the View that raised the event.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.DataViewBase" /> descendant that is the view that raised the event.
    /// </value>
    public DataViewBase Source
    {
      get
      {
        return this.Column.View;
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the CustomBestFitEventArgs class with the specified settings.
    /// </para>
    ///             </summary>
    /// <param name="column">
    /// A <see cref="T:DevExpress.Xpf.Grid.GridColumn" /> that is a column for which an event has been raised.
    /// 
    ///           </param>
    /// <param name="bestFitMode">
    /// A <see cref="T:DevExpress.Xpf.Core.BestFitMode" /> object that specifies how the optimal width required for a column to completely display its contents is calculated. This value is assigned to the <see cref="P:DevExpress.Xpf.Grid.CustomBestFitEventArgsBase.BestFitMode" /> property.
    /// 
    ///           </param>
    public CustomBestFitEventArgs(GridColumn column, BestFitMode bestFitMode)
      : base((ColumnBase) column, bestFitMode)
    {
      this.RoutedEvent = TableView.CustomBestFitEvent;
    }
  }
}
