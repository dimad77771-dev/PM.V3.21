// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListRowData
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Utils;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.TreeList
{
  /// <summary>
  ///                 <para>Contains information about a node.
  /// </para>
  ///             </summary>
  public class TreeListRowData : RowData
  {
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsExpandedProperty;
    private static readonly DependencyPropertyKey IsExpandedPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsButtonVisibleProperty;
    private static readonly DependencyPropertyKey IsButtonVisiblePropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsImageVisibleProperty;
    private static readonly DependencyPropertyKey IsImageVisiblePropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IsCheckBoxVisibleProperty;
    private static readonly DependencyPropertyKey IsCheckBoxVisiblePropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowMarginProperty;
    private static readonly DependencyPropertyKey RowMarginPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty IndentsProperty;
    private static readonly DependencyPropertyKey IndentsPropertyKey;
    /// <summary>
    ///                 <para>Identifies the  dependency property.
    /// </para>
    ///             </summary>
    /// <returns> </returns>
    public static readonly DependencyProperty RowLevelProperty;
    private static readonly DependencyPropertyKey RowLevelPropertyKey;
    private ImageSource image;
    private bool? isChecked;
    private bool isCheckBoxEnabled;

    /// <summary>
    ///                 <para>Gets whether the expand button is shown within the treelist row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the expand button is visible; otherwise, <b>false</b>.
    /// </value>
    public bool IsButtonVisible
    {
      get
      {
        return (bool) this.GetValue(TreeListRowData.IsButtonVisibleProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.IsButtonVisiblePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Indicates whether the image is shown within the treelist row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the image is shown; otherwise, <b>false</b>.
    /// </value>
    public bool IsImageVisible
    {
      get
      {
        return (bool) this.GetValue(TreeListRowData.IsImageVisibleProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.IsImageVisiblePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Indicates whether the treelist row's check box is visible. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the check box is visible; otherwise, <b>false</b>.
    /// </value>
    public bool IsCheckBoxVisible
    {
      get
      {
        return (bool) this.GetValue(TreeListRowData.IsCheckBoxVisibleProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.IsCheckBoxVisiblePropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets whether the treelist row is expanded. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value><b>true</b>, if the treelist row is expanded; otherwise, <b>false</b>.
    /// </value>
    public bool IsExpanded
    {
      get
      {
        return (bool) this.GetValue(TreeListRowData.IsExpandedProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.IsExpandedPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the treelist row's outer margin. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:System.Windows.Thickness" /> object that represents the thickness of a frame around the treelist row.
    /// </value>
    public Thickness RowMargin
    {
      get
      {
        return (Thickness) this.GetValue(TreeListRowData.RowMarginProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.RowMarginPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the list of indents within the treelist row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>A list of indents.</value>
    public List<TreeListIndentType> Indents
    {
      get
      {
        return (List<TreeListIndentType>) this.GetValue(TreeListRowData.IndentsProperty);
      }
      private set
      {
        this.SetValue(TreeListRowData.IndentsPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the nesting level of the treelist row. This is a dependency property.
    /// </para>
    ///             </summary>
    /// <value>An integer value that specifies the row's nesting level.</value>
    public int RowLevel
    {
      get
      {
        return (int) this.GetValue(TreeListRowData.RowLevelProperty);
      }
      internal set
      {
        this.SetValue(TreeListRowData.RowLevelPropertyKey, (object) value);
      }
    }

    /// <summary>
    ///                 <para>Gets the node corresponding to the treelist row.
    /// </para>
    ///             </summary>
    /// <value>A <see cref="T:DevExpress.Xpf.Grid.TreeListNode" /> object.
    /// </value>
    public TreeListNode Node
    {
      get
      {
        return this.View.TreeListDataProvider.GetNodeByRowHandle(this.RowHandle.Value);
      }
    }

    /// <summary>
    ///                 <para>Gets or sets the image that is displayed within the treelist row.
    /// </para>
    ///             </summary>
    /// <value>A System.Windows.Media.ImageSource class descendant.</value>
    public ImageSource Image
    {
      get
      {
        return this.image;
      }
      set
      {
        if (this.Image == value)
          return;
        this.image = value;
        this.NotifyPropertyChanged("Image");
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the treelist row is checked.
    /// </para>
    ///             </summary>
    /// <value>A Boolean value that specifies whether or not the treelist row is checked.</value>
    public bool? IsChecked
    {
      get
      {
        return this.isChecked;
      }
      set
      {
        this.Node.UpdateNodeChecked(value);
        bool? isChecked = this.IsChecked;
        bool? nullable = value;
        if ((isChecked.GetValueOrDefault() != nullable.GetValueOrDefault() ? 0 : (isChecked.HasValue == nullable.HasValue ? 1 : 0)) != 0)
          return;
        this.isChecked = value;
        this.NotifyPropertyChanged("IsChecked");
      }
    }

    /// <summary>
    ///                 <para>Gets or sets whether the checkbox within the treelist row is enabled.
    /// </para>
    ///             </summary>
    /// <value><b>true</b> if the checkbox is enabled; otherwise, <b>false</b>.
    /// </value>
    public bool IsCheckBoxEnabled
    {
      get
      {
        return this.isCheckBoxEnabled;
      }
      set
      {
        if (this.IsCheckBoxEnabled == value)
          return;
        this.isCheckBoxEnabled = value;
        this.NotifyPropertyChanged("IsCheckBoxEnabled");
      }
    }

    protected TreeListView View
    {
      get
      {
        return (TreeListView) base.View;
      }
    }

    static TreeListRowData()
    {
      Type ownerType = typeof (TreeListRowData);
      TreeListRowData.IsExpandedPropertyKey = DependencyPropertyManager.RegisterReadOnly("IsExpanded", typeof (bool), ownerType, new PropertyMetadata((object) false));
      TreeListRowData.IsExpandedProperty = TreeListRowData.IsExpandedPropertyKey.DependencyProperty;
      TreeListRowData.IsButtonVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsButtonVisible", typeof (bool), ownerType, new PropertyMetadata((object) false));
      TreeListRowData.IsButtonVisibleProperty = TreeListRowData.IsButtonVisiblePropertyKey.DependencyProperty;
      TreeListRowData.IsImageVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsImageVisible", typeof (bool), ownerType, new PropertyMetadata((object) false));
      TreeListRowData.IsImageVisibleProperty = TreeListRowData.IsImageVisiblePropertyKey.DependencyProperty;
      TreeListRowData.IsCheckBoxVisiblePropertyKey = DependencyPropertyManager.RegisterReadOnly("IsCheckBoxVisible", typeof (bool), ownerType, new PropertyMetadata((object) false));
      TreeListRowData.IsCheckBoxVisibleProperty = TreeListRowData.IsCheckBoxVisiblePropertyKey.DependencyProperty;
      TreeListRowData.IndentsPropertyKey = DependencyPropertyManager.RegisterReadOnly("Indents", typeof (List<TreeListIndentType>), ownerType, new PropertyMetadata((PropertyChangedCallback) null));
      TreeListRowData.IndentsProperty = TreeListRowData.IndentsPropertyKey.DependencyProperty;
      TreeListRowData.RowMarginPropertyKey = DependencyPropertyManager.RegisterReadOnly("RowMargin", typeof (Thickness), ownerType, new PropertyMetadata((object) new Thickness()));
      TreeListRowData.RowMarginProperty = TreeListRowData.RowMarginPropertyKey.DependencyProperty;
      TreeListRowData.RowLevelPropertyKey = DependencyPropertyManager.RegisterReadOnly("RowLevel", typeof (int), ownerType, new PropertyMetadata((object) 0));
      TreeListRowData.RowLevelProperty = TreeListRowData.RowLevelPropertyKey.DependencyProperty;
    }

    /// <summary>
    ///                 <para>Initializes a new instance of the TreeListRowData class.
    /// </para>
    ///             </summary>
    /// <param name="treeBuilder">
    /// A DevExpress.Xpf.Grid.Native.DataTreeBuilder descendant.
    /// 
    ///           </param>
    public TreeListRowData(DataTreeBuilder treeBuilder)
      : base(treeBuilder, false, true)
    {
    }

    protected internal override void UpdateData()
    {
      base.UpdateData();
      if (this.Row == null)
        return;
      this.IsButtonVisible = this.CanShowExpandButton();
      this.UpdateNodeImage();
      this.IsChecked = this.Node.IsChecked;
      this.IsCheckBoxEnabled = this.Node.IsCheckBoxEnabled;
    }

    protected virtual void UpdateNodeImage()
    {
      this.Image = this.GetImageSource();
    }

    protected override void AssignFromCore(NodeContainer nodeContainer, RowNode rowNode, bool forceUpdate)
    {
      base.AssignFromCore(nodeContainer, rowNode, forceUpdate);
      this.UpdateRowState();
    }

    protected void UpdateRowState()
    {
      this.NextRowLevel = int.MinValue;
      this.IsExpanded = this.Node.IsExpanded;
      this.IsButtonVisible = this.CanShowExpandButton();
      this.IsImageVisible = this.View.ShowNodeImages;
      this.IsCheckBoxVisible = this.View.ShowCheckboxes;
      this.RowLevel = this.Level;
      this.RowMargin = new Thickness((double) this.Level * this.View.RowIndent, 0.0, 0.0, 0.0);
      this.Indents = this.View.TreeLineStyle == TreeListLineStyle.None ? (List<TreeListIndentType>) null : this.CalcNodeIndents();
    }

    protected internal override void UpdateIsSelected(bool forceIsSelected)
    {
      base.UpdateIsSelected(forceIsSelected);
      if (this.IsDirty)
        return;
      this.UpdateNodeImage();
    }

    protected virtual bool CanShowExpandButton()
    {
      if (!this.View.ShowRootIndent && this.Node.ActualLevel == 0 || (!this.View.ShowExpandButtons || !this.Node.IsTogglable))
        return false;
      return this.Node.IsExpandButtonVisible != DefaultBoolean.False;
    }

    protected internal ImageSource GetImageSource()
    {
      if (this.View.NodeImageSelector != null)
      {
        ImageSource imageSource = this.View.NodeImageSelector.CanSelect(this) ? this.View.NodeImageSelector.Select(this) : (ImageSource) null;
        if (imageSource != null)
          return imageSource;
      }
      if (!string.IsNullOrEmpty(this.View.ImageFieldName))
      {
        try
        {
          object obj = this.View.GetNodeValue(this.Node, this.View.ImageFieldName);
          if (obj == null && this.Node != null && this.Node.Content != null)
            obj = this.TryGetValueInternal(this.Node.Content, this.View.ImageFieldName);
          if (obj != null)
          {
            ImageSource imageSource = obj as ImageSource;
            if (imageSource != null)
              return imageSource;
            byte[] numArray = obj as byte[];
            if (numArray != null)
              return new BytesToImageSourceConverter().Convert((object) numArray, (Type) null, (object) null, (CultureInfo) null) as ImageSource;
          }
        }
        catch
        {
          return (ImageSource) null;
        }
      }
      if (this.Node != null)
        return this.Node.Image;
      return (ImageSource) null;
    }

    private object TryGetValueInternal(object obj, string propertyName)
    {
      PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)[propertyName];
      if (propertyDescriptor == null)
        return (object) null;
      return propertyDescriptor.GetValue(obj);
    }

    protected internal override bool GetShowBottomLine()
    {
      return false;
    }

    protected virtual List<TreeListIndentType> CalcNodeIndents()
    {
      List<TreeListIndentType> indents = new List<TreeListIndentType>();
      TreeListUtils.CalcNodeIndents(this.Node, indents, this.View.ShowRootIndent);
      return indents;
    }

    protected override void UpdateEvenRow()
    {
      if (this.RowHandle == null)
        return;
      this.EvenRow = this.treeBuilder.GetRowVisibleIndexByHandleCore(this.RowHandle.Value) % 2 == 0;
    }

    protected internal override double GetRowIndent(bool isFirstColumn)
    {
      if (isFirstColumn)
        return this.GetLeftIndent();
      return base.GetRowIndent(isFirstColumn);
    }

    protected internal override double GetRowLeftMargin(GridColumnData data)
    {
      if (data.VisibleIndex != data.Column.VisibleIndex && data.VisibleIndex == 0 && (this.View.FixedLeftVisibleColumns.Count == 0 && data.Column.Fixed == FixedStyle.None))
        return this.GetLeftIndent();
      return 0.0;
    }

    private double GetLeftIndent()
    {
      return (double) (this.View.TreeListDataProvider.MaxVisibleLevel - (this.Node != null ? this.Node.ActualLevel : this.Level)) * this.View.RowIndent;
    }
  }
}
