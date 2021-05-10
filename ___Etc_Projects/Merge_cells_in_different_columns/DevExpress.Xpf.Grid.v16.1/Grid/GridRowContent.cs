// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridRowContent
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Displays the content of a data row.
  /// </para>
  ///             </summary>
  public class GridRowContent : ContentControl
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty CurrentHeightProperty = DependencyPropertyManager.Register("CurrentHeight", typeof (double), typeof (GridRowContent), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0));

    /// <summary>
    ///                 <para>Gets or sets the row's actual height. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value that specifies the row's height.
    /// </value>
    [DevExpressXpfGridLocalizedDescription("GridRowContentCurrentHeight")]
    public double CurrentHeight
    {
      get
      {
        return (double) this.GetValue(GridRowContent.CurrentHeightProperty);
      }
      set
      {
        this.SetValue(GridRowContent.CurrentHeightProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the GridRowContent class.
    /// </para>
    ///             </summary>
    public GridRowContent()
    {
      this.SetDefaultStyleKey(typeof (GridRowContent));
      this.SizeChanged += new SizeChangedEventHandler(this.GridRowContent_SizeChanged);
    }

    private void GridRowContent_SizeChanged(object sender, SizeChangedEventArgs e)
    {
      this.CurrentHeight = e.NewSize.Height;
    }
  }
}
