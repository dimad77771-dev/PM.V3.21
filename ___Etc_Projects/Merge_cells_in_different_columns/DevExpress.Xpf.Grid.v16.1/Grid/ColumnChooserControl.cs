// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ColumnChooserControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Collections;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  /// <summary>
  ///                 <para>The <b>Column Chooser</b> control.
  /// </para>
  ///             </summary>
  public class ColumnChooserControl : ColumnChooserControlBase
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty DragTextProperty = DependencyProperty.Register("DragText", typeof (string), typeof (ColumnChooserControl), new PropertyMetadata((object) string.Empty));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ColumnsProperty = DependencyProperty.Register("Columns", typeof (IList), typeof (ColumnChooserControl), new PropertyMetadata((PropertyChangedCallback) null));
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty ItemTemplateProperty = DependencyProperty.Register("ItemTemplate", typeof (DataTemplate), typeof (ColumnChooserControl), new PropertyMetadata((PropertyChangedCallback) null));

    /// <summary>
    ///                 <para>Gets or sets the list of invisible columns. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>The list of invisible columns.</value>
    public IList Columns
    {
      get
      {
        return (IList) this.GetValue(ColumnChooserControl.ColumnsProperty);
      }
      set
      {
        this.SetValue(ColumnChooserControl.ColumnsProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the text displayed within the Column Band Chooser when it's empty.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.String" /> value that specifies the text displayed by the empty column chooser. Default value: Drag a column here to customize the layout
    /// </value>
    public string DragText
    {
      get
      {
        return (string) this.GetValue(ColumnChooserControl.DragTextProperty);
      }
      set
      {
        this.SetValue(ColumnChooserControl.DragTextProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the template that defines the presentation of the column chooser's items. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.DataTemplate" /> object that defines the presentation of data rows.
    /// </value>
    public DataTemplate ItemTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(ColumnChooserControl.ItemTemplateProperty);
      }
      set
      {
        this.SetValue(ColumnChooserControl.ItemTemplateProperty, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the ColumnChooserControl class.
    /// </para>
    ///             </summary>
    public ColumnChooserControl()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (ColumnChooserControl));
    }

    protected override void OnOwnerChanged(ILogicalOwner oldView, ILogicalOwner newView)
    {
      DataControlBase.SetCurrentViewInternal((DependencyObject) this, (DataViewBase) newView);
      base.OnOwnerChanged(oldView, newView);
    }

    protected override ILogicalOwner GetLogicalOwnerCore()
    {
      return (ILogicalOwner) ((DataViewBase) this.Owner).RootView;
    }
  }
}
