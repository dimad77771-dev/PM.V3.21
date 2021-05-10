// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.RowIndicator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class RowIndicator : RowIndicatorBase
  {
    public static readonly DependencyProperty NoneContentTemplateProperty = DependencyProperty.Register("NoneContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty FocusedContentTemplateProperty = DependencyProperty.Register("FocusedContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty ChangedContentTemplateProperty = DependencyProperty.Register("ChangedContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty NewItemRowContentTemplateProperty = DependencyProperty.Register("NewItemRowContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty EditingContentTemplateProperty = DependencyProperty.Register("EditingContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty ErrorContentTemplateProperty = DependencyProperty.Register("ErrorContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty FocusedErrorContentTemplateProperty = DependencyProperty.Register("FocusedErrorContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty AutoFilterRowContentTemplateProperty = DependencyProperty.Register("AutoFilterRowContentTemplate", typeof (DataTemplate), typeof (RowIndicator), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnContentTemplateChanged())));
    public static readonly DependencyProperty IndicatorStateProperty = DependencyProperty.Register("IndicatorState", typeof (IndicatorState), typeof (RowIndicator), new PropertyMetadata((object) IndicatorState.None, (PropertyChangedCallback) ((d, _) => ((RowIndicator) d).OnIndicatorStateChanged())));
    public static readonly DependencyProperty ShowRowBreakProperty = DependencyProperty.Register("ShowRowBreak", typeof (bool), typeof (RowIndicator), new PropertyMetadata((object) false));
    private Border contentBorder;
    private DataTemplate contentTemplate;
    private ContentPresenter contentPresenter;

    public DataTemplate NoneContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.NoneContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.NoneContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate FocusedContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.FocusedContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.FocusedContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate ChangedContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.ChangedContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.ChangedContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate NewItemRowContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.NewItemRowContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.NewItemRowContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate EditingContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.EditingContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.EditingContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate ErrorContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.ErrorContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.ErrorContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate FocusedErrorContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.FocusedErrorContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.FocusedErrorContentTemplateProperty, (object) value);
      }
    }

    public DataTemplate AutoFilterRowContentTemplate
    {
      get
      {
        return (DataTemplate) this.GetValue(RowIndicator.AutoFilterRowContentTemplateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.AutoFilterRowContentTemplateProperty, (object) value);
      }
    }

    public IndicatorState IndicatorState
    {
      get
      {
        return (IndicatorState) this.GetValue(RowIndicator.IndicatorStateProperty);
      }
      set
      {
        this.SetValue(RowIndicator.IndicatorStateProperty, (object) value);
      }
    }

    public bool ShowRowBreak
    {
      get
      {
        return (bool) this.GetValue(RowIndicator.ShowRowBreakProperty);
      }
      set
      {
        this.SetValue(RowIndicator.ShowRowBreakProperty, (object) value);
      }
    }

    internal Border ContentBorder
    {
      get
      {
        return this.contentBorder;
      }
      private set
      {
        if (this.contentBorder == value)
          return;
        if (this.contentBorder != null)
          this.contentBorder.Child = (UIElement) null;
        this.contentBorder = value;
      }
    }

    private RowDataBase RowData
    {
      get
      {
        return this.DataContext as RowDataBase;
      }
    }

    private ITableView TableView
    {
      get
      {
        if (this.RowData == null)
          return (ITableView) null;
        return this.RowData.View as ITableView;
      }
    }

    static RowIndicator()
    {
      Type forType = typeof (RowIndicator);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    private void OnIndicatorStateChanged()
    {
      this.UpdateContent();
    }

    private void OnContentTemplateChanged()
    {
      this.UpdateContent();
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.ContentBorder = this.GetTemplateChild("PART_ContentBorder") as Border;
      this.UpdateContent();
    }

    internal void UpdateContent()
    {
      if (this.TableView == null)
        return;
      DataTemplate contentTemplate = this.GetContentTemplate();
      if (this.ContentBorder == null || contentTemplate == this.contentTemplate)
        return;
      this.contentTemplate = contentTemplate;
      if (this.contentTemplate != null && this.contentPresenter == null)
        this.contentPresenter = new ContentPresenter();
      if (this.contentPresenter != null)
      {
        this.contentPresenter.Content = (object) null;
        this.contentPresenter.ContentTemplate = this.contentTemplate;
        if (this.contentTemplate != null)
          this.contentPresenter.Content = (object) this.RowData;
      }
      this.ContentBorder.Child = (UIElement) this.contentPresenter;
    }

    internal DataTemplate GetContentTemplate()
    {
      if (this.TableView.RowIndicatorContentTemplate != null && !(this.TableView.RowIndicatorContentTemplate is DefaultDataTemplate))
        return this.TableView.RowIndicatorContentTemplate;
      switch (this.IndicatorState)
      {
        case IndicatorState.Focused:
          return this.FocusedContentTemplate;
        case IndicatorState.Changed:
          return this.ChangedContentTemplate;
        case IndicatorState.NewItemRow:
          return this.NewItemRowContentTemplate;
        case IndicatorState.Editing:
          return this.EditingContentTemplate;
        case IndicatorState.Error:
          return this.ErrorContentTemplate;
        case IndicatorState.FocusedError:
          return this.FocusedErrorContentTemplate;
        case IndicatorState.AutoFilterRow:
          return this.AutoFilterRowContentTemplate;
        default:
          return this.NoneContentTemplate;
      }
    }
  }
}
