// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridScrollablePart
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GridScrollablePart : Control
  {
    public static readonly DependencyProperty FixedLeftContentProperty = DependencyPropertyManager.Register("FixedLeftContent", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new UIPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).UpdateFixedParts())));
    public static readonly DependencyProperty FixedRightContentProperty = DependencyPropertyManager.Register("FixedRightContent", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new UIPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).UpdateFixedParts())));
    public static readonly DependencyProperty FixedNoneContentProperty = DependencyPropertyManager.Register("FixedNoneContent", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new UIPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFixedNoneContentChanged())));
    public static readonly DependencyProperty FitContentProperty = DependencyPropertyManager.Register("FitContent", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new UIPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFitContentChanged())));
    public static readonly DependencyProperty FitLeftContentProperty = DependencyPropertyManager.Register("FitLeftContent", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new UIPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFitLeftContentChanged())));
    public static readonly DependencyProperty ScrollingMarginProperty = DependencyPropertyManager.Register("ScrollingMargin", typeof (Thickness), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) new Thickness(0.0), (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnScrollingMarginChanged())));
    public static readonly DependencyProperty FixedNoneContentWidthProperty = DependencyPropertyManager.Register("FixedNoneContentWidth", typeof (double), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFixedNoneContentWidthChanged())));
    public static readonly DependencyProperty FixedColumnsDelimiterTemplateProperty = DependencyPropertyManager.Register("FixedColumnsDelimiterTemplate", typeof (DataTemplate), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFixedColumnsDelimiterTemplateChanged())));
    public static readonly DependencyProperty FixedLeftVisibleColumnsProperty = DependencyPropertyManager.Register("FixedLeftVisibleColumns", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).UpdateFixedParts())));
    public static readonly DependencyProperty FixedRightVisibleColumnsProperty = DependencyPropertyManager.Register("FixedRightVisibleColumns", typeof (object), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).UpdateFixedParts())));
    public static readonly DependencyProperty FixedLineWidthProperty = DependencyPropertyManager.Register("FixedLineWidth", typeof (double), typeof (GridScrollablePart), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((GridScrollablePart) d).OnFixedLineWidthChanged())));
    private ContentPresenter fitContent;
    private ContentPresenter fitLeftContent;
    private ContentPresenter fixedRightContent;
    private ContentPresenter fixedLeftLineContent;
    private ContentPresenter fixedRightLineContent;
    protected ContentPresenter fixedNoneContentInternal;
    protected ContentPresenter fixedLeftContentInternal;
    protected FrameworkElement fixedNoneContentCellsBorder;
    protected FrameworkElement scrollablePartPanel;

    public object FixedLeftContent
    {
      get
      {
        return this.GetValue(GridScrollablePart.FixedLeftContentProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedLeftContentProperty, value);
      }
    }

    public object FixedRightContent
    {
      get
      {
        return this.GetValue(GridScrollablePart.FixedRightContentProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedRightContentProperty, value);
      }
    }

    public object FixedNoneContent
    {
      get
      {
        return this.GetValue(GridScrollablePart.FixedNoneContentProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedNoneContentProperty, value);
      }
    }

    public object FitContent
    {
      get
      {
        return this.GetValue(GridScrollablePart.FitContentProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FitContentProperty, value);
      }
    }

    public object FitLeftContent
    {
      get
      {
        return this.GetValue(GridScrollablePart.FitLeftContentProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FitLeftContentProperty, value);
      }
    }

    public Thickness ScrollingMargin
    {
      get
      {
        return (Thickness) this.GetValue(GridScrollablePart.ScrollingMarginProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.ScrollingMarginProperty, (object) value);
      }
    }

    public double FixedNoneContentWidth
    {
      get
      {
        return (double) this.GetValue(GridScrollablePart.FixedNoneContentWidthProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedNoneContentWidthProperty, (object) value);
      }
    }

    public DataTemplate FixedColumnsDelimiterTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(GridScrollablePart.FixedColumnsDelimiterTemplateProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedColumnsDelimiterTemplateProperty, (object) value);
      }
    }

    public object FixedLeftVisibleColumns
    {
      get
      {
        return this.GetValue(GridScrollablePart.FixedLeftVisibleColumnsProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedLeftVisibleColumnsProperty, value);
      }
    }

    public object FixedRightVisibleColumns
    {
      get
      {
        return this.GetValue(GridScrollablePart.FixedRightVisibleColumnsProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedRightVisibleColumnsProperty, value);
      }
    }

    public double FixedLineWidth
    {
      get
      {
        return (double) this.GetValue(GridScrollablePart.FixedLineWidthProperty);
      }
      set
      {
        this.SetValue(GridScrollablePart.FixedLineWidthProperty, (object) value);
      }
    }

    protected ContentPresenter FixedNoneContentCore
    {
      get
      {
        return this.fixedNoneContentInternal;
      }
    }

    protected virtual bool HasFixedLeftColumns
    {
      get
      {
        if (this.FixedLeftVisibleColumns is IList)
          return ((ICollection) this.FixedLeftVisibleColumns).Count > 0;
        return false;
      }
    }

    protected bool HasFixedRightColumns
    {
      get
      {
        if (this.FixedRightVisibleColumns is IList)
          return ((ICollection) this.FixedRightVisibleColumns).Count > 0;
        return false;
      }
    }

    public GridScrollablePart()
    {
      this.SetDefaultStyleKey(typeof (GridScrollablePart));
    }

    public override void OnApplyTemplate()
    {
      if (this.fixedLeftContentInternal != null)
        this.fixedLeftContentInternal.Content = (object) null;
      if (this.fitContent != null)
        this.fitContent.Content = (object) null;
      if (this.fitLeftContent != null)
        this.fitLeftContent.Content = (object) null;
      if (this.fixedRightContent != null)
        this.fixedRightContent.Content = (object) null;
      if (this.fixedNoneContentInternal != null)
        this.fixedNoneContentInternal.Content = (object) null;
      this.fixedLeftContentInternal = this.GetTemplateChild("PART_FixedLeftContent") as ContentPresenter;
      this.fitContent = this.GetTemplateChild("PART_FitContent") as ContentPresenter;
      this.fitLeftContent = this.GetTemplateChild("PART_FitLeftContent") as ContentPresenter;
      this.fixedRightContent = this.GetTemplateChild("PART_FixedRightContent") as ContentPresenter;
      this.fixedNoneContentInternal = this.GetTemplateChild("PART_FixedNoneContent") as ContentPresenter;
      this.OnFixedNoneContentChanged();
      this.fixedNoneContentCellsBorder = this.GetTemplateChild("PART_FixedNoneCellsBorder") as FrameworkElement;
      this.OnFixedNoneContentWidthChanged();
      this.OnFitContentChanged();
      this.OnFitLeftContentChanged();
      this.fixedLeftLineContent = this.GetTemplateChild("PART_FixedLeftLinePlaceHolder") as ContentPresenter;
      this.fixedRightLineContent = this.GetTemplateChild("PART_FixedRightLinePlaceHolder") as ContentPresenter;
      this.UpdateFixedParts();
      this.OnFixedLineWidthChanged();
      this.OnScrollingMarginChanged();
      this.OnFixedColumnsDelimiterTemplateChanged();
      this.scrollablePartPanel = this.GetTemplateChild("PART_ScrollablePartPanel") as FrameworkElement;
      base.OnApplyTemplate();
    }

    private void OnFixedNoneContentChanged()
    {
      if (this.fixedNoneContentInternal == null)
        return;
      this.fixedNoneContentInternal.Content = this.FixedNoneContent;
    }

    private void OnFitContentChanged()
    {
      if (this.fitContent == null)
        return;
      this.fitContent.Content = this.FitContent;
    }

    private void OnFitLeftContentChanged()
    {
      if (this.fitLeftContent == null)
        return;
      this.fitLeftContent.Content = this.FitLeftContent;
    }

    protected virtual void UpdateFixedParts()
    {
      if (this.HasFixedLeftColumns)
      {
        if (this.fixedLeftContentInternal != null && this.fixedLeftContentInternal.Content == null)
          this.fixedLeftContentInternal.Content = this.FixedLeftContent;
        if (this.fixedLeftLineContent != null)
          this.fixedLeftLineContent.Visibility = Visibility.Visible;
      }
      else if (this.fixedLeftLineContent != null)
        this.fixedLeftLineContent.Visibility = Visibility.Collapsed;
      if (this.HasFixedRightColumns)
      {
        if (this.fixedRightContent != null && this.fixedRightContent.Content == null)
          this.fixedRightContent.Content = this.FixedRightContent;
        if (this.fixedRightLineContent == null)
          return;
        this.fixedRightLineContent.Visibility = Visibility.Visible;
      }
      else
      {
        if (this.fixedRightLineContent == null)
          return;
        this.fixedRightLineContent.Visibility = Visibility.Collapsed;
      }
    }

    private void OnFixedLineWidthChanged()
    {
      if (this.fixedLeftLineContent != null)
        this.fixedLeftLineContent.Width = this.FixedLineWidth;
      if (this.fixedRightLineContent == null)
        return;
      this.fixedRightLineContent.Width = this.FixedLineWidth;
    }

    protected virtual void OnFixedNoneContentWidthChanged()
    {
      if (this.fixedNoneContentCellsBorder == null)
        return;
      if (this.fixedNoneContentCellsBorder.Width != this.FixedNoneContentWidth && this.scrollablePartPanel != null)
        this.scrollablePartPanel.InvalidateMeasure();
      this.fixedNoneContentCellsBorder.Width = this.FixedNoneContentWidth;
    }

    protected virtual void OnScrollingMarginChanged()
    {
      if (this.fixedNoneContentInternal == null)
        return;
      this.fixedNoneContentInternal.Margin = this.ScrollingMargin;
    }

    private void OnFixedColumnsDelimiterTemplateChanged()
    {
      if (this.fixedLeftLineContent != null)
        this.fixedLeftLineContent.ContentTemplate = this.FixedColumnsDelimiterTemplate;
      if (this.fixedRightLineContent == null)
        return;
      this.fixedRightLineContent.ContentTemplate = this.FixedColumnsDelimiterTemplate;
    }
  }
}
