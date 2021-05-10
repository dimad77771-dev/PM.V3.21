// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupValuePresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Filtering;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupValuePresenter : Control, IGroupValuePresenter
  {
    public static readonly DependencyProperty ColumnHeaderProperty = DependencyProperty.Register("ColumnHeader", typeof (string), typeof (GroupValuePresenter), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupValuePresenter) d).OnColumnHeaderChanged())));
    public static readonly DependencyProperty GroupColumnHeaderTextProperty = DependencyProperty.Register("GroupColumnHeaderText", typeof (string), typeof (GroupValuePresenter), new PropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty TextProperty = DependencyProperty.Register("Text", typeof (string), typeof (GroupValuePresenter), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupValuePresenter) d).OnTextChanged())));
    public static readonly DependencyProperty HighlightingPropertiesProperty = DependencyProperty.Register("HighlightingProperties", typeof (GroupTextHighlightingProperties), typeof (GroupValuePresenter), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((GroupValuePresenter) d).OnTextChanged())));
    public const string GroupValueTextBlockName = "PART_GroupValue";
    public const string GroupColumnHeaderFormat = "{0}: ";
    private GroupValuePresenterController controller;
    private TextBlock TextElement;

    public string ColumnHeader
    {
      get
      {
        return (string) this.GetValue(GroupValuePresenter.ColumnHeaderProperty);
      }
      set
      {
        this.SetValue(GroupValuePresenter.ColumnHeaderProperty, (object) value);
      }
    }

    public string Text
    {
      get
      {
        return (string) this.GetValue(GroupValuePresenter.TextProperty);
      }
      set
      {
        this.SetValue(GroupValuePresenter.TextProperty, (object) value);
      }
    }

    public GroupTextHighlightingProperties HighlightingProperties
    {
      get
      {
        return (GroupTextHighlightingProperties) this.GetValue(GroupValuePresenter.HighlightingPropertiesProperty);
      }
      set
      {
        this.SetValue(GroupValuePresenter.HighlightingPropertiesProperty, (object) value);
      }
    }

    public string GroupColumnHeaderText
    {
      get
      {
        return (string) this.GetValue(GroupValuePresenter.GroupColumnHeaderTextProperty);
      }
      set
      {
        this.SetValue(GroupValuePresenter.GroupColumnHeaderTextProperty, (object) value);
      }
    }

    GridGroupValueData IGroupValuePresenter.ValueData
    {
      get
      {
        return this.controller.ValueData;
      }
      set
      {
        this.controller.ValueData = value;
      }
    }

    bool? IGroupValuePresenter.UseTemplate
    {
      get
      {
        return new bool?(false);
      }
    }

    FrameworkElement IGroupValuePresenter.Element
    {
      get
      {
        return (FrameworkElement) this;
      }
    }

    DataTemplateSelector IGroupValuePresenter.ContentTemplateSelector
    {
      get
      {
        return (DataTemplateSelector) null;
      }
      set
      {
      }
    }

    static GroupValuePresenter()
    {
      Type forType = typeof (GroupValuePresenter);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new GroupValueTableViewHitTestAcceptor()));
    }

    public GroupValuePresenter()
    {
      this.controller = new GroupValuePresenterController(this);
    }

    private void OnColumnHeaderChanged()
    {
      this.GroupColumnHeaderText = string.Format("{0}: ", (object) this.ColumnHeader);
    }

    private void OnTextChanged()
    {
      if (this.TextElement == null)
        return;
      if (this.HighlightingProperties == null)
      {
        this.TextElement.Inlines.Clear();
        this.TextElement.Text = this.Text;
      }
      else
        TextBlockService.UpdateTextBlock(this.TextElement, this.HighlightingProperties.EditSettings, this.Text, this.HighlightingProperties.TextHighlightingProperties.Text, this.HighlightingProperties.TextHighlightingProperties.FilterCondition == FilterCondition.StartsWith ? HighlightedTextCriteria.StartsWith : HighlightedTextCriteria.Contains);
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdateTextElement();
    }

    public void UpdateTextElement()
    {
      this.TextElement = LayoutHelper.FindElementByName((FrameworkElement) this, "PART_GroupValue") as TextBlock;
      this.OnTextChanged();
    }
  }
}
