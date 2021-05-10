// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.LightweightCellEditorBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>Serves as the base for classes that display the content of a data cell in the optimized mode.
  /// 
  /// </para>
  ///             </summary>
  public abstract class LightweightCellEditorBase : CellEditor
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty ForegroundProperty = TextBlock.ForegroundProperty.AddOwner(typeof (LightweightCellEditorBase));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FontSizeProperty = TextBlock.FontSizeProperty.AddOwner(typeof (LightweightCellEditorBase));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns>
    /// </returns>
    public static readonly DependencyProperty FontFamilyProperty = TextBlock.FontFamilyProperty.AddOwner(typeof (LightweightCellEditorBase));

    /// <summary>
    ///                 <para>Gets or sets the foreground color.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Drawing.Color" /> value representing the foreground color.
    /// </value>
    public Brush Foreground
    {
      get
      {
        return (Brush) this.GetValue(LightweightCellEditorBase.ForegroundProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditorBase.ForegroundProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the size of the text font for the grid cells.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Double" /> value representing the font size for the grid cells.
    /// </value>
    public double FontSize
    {
      get
      {
        return (double) this.GetValue(LightweightCellEditorBase.FontSizeProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditorBase.FontSizeProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the font family name for the grid cells. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Media.FontFamily" /> object representing the font family.
    /// </value>
    public FontFamily FontFamily
    {
      get
      {
        return (FontFamily) this.GetValue(LightweightCellEditorBase.FontFamilyProperty);
      }
      set
      {
        this.SetValue(LightweightCellEditorBase.FontFamilyProperty, (object) value);
      }
    }
  }
}
