// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.EditFormControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.UI.Native;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.EditForm;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class EditFormControl : CachedItemsControl
  {
    public static readonly DependencyProperty LayoutSettingsProperty;

    public EditFormLayoutSettings LayoutSettings
    {
      get
      {
        return (EditFormLayoutSettings) this.GetValue(EditFormControl.LayoutSettingsProperty);
      }
      set
      {
        this.SetValue(EditFormControl.LayoutSettingsProperty, (object) value);
      }
    }

    private System.Windows.Controls.Grid GridPanel
    {
      get
      {
        return this.Panel as System.Windows.Controls.Grid;
      }
    }

    static EditFormControl()
    {
      DependencyPropertyRegistrator<EditFormControl>.New().Register<EditFormLayoutSettings>((System.Linq.Expressions.Expression<Func<EditFormControl, EditFormLayoutSettings>>) (d => d.LayoutSettings), out EditFormControl.LayoutSettingsProperty, EditFormLayoutSettings.Empty, (Action<EditFormControl>) (d => d.UpdateGridPanel()), new FrameworkPropertyMetadataOptions?());
      Type forType = typeof (EditFormControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.UpdateGridPanel();
    }

    private void UpdateGridPanel()
    {
      if (this.GridPanel == null)
        return;
      this.GridPanel.ColumnDefinitions.Clear();
      this.GridPanel.RowDefinitions.Clear();
      for (int index = 0; index < this.LayoutSettings.ColumnCount; ++index)
        this.GridPanel.ColumnDefinitions.Add(new ColumnDefinition()
        {
          Width = new GridLength(1.0, index % 2 == 0 ? GridUnitType.Auto : GridUnitType.Star)
        });
      for (int index = 0; index < this.LayoutSettings.RowCount; ++index)
        this.GridPanel.RowDefinitions.Add(new RowDefinition()
        {
          Height = new GridLength(1.0, GridUnitType.Auto)
        });
    }

    protected override void ValidateElement(FrameworkElement element, object item)
    {
      base.ValidateElement(element, item);
      EditFormCellDataBase formCellDataBase = item as EditFormCellDataBase;
      if (formCellDataBase == null)
        return;
      System.Windows.Controls.Grid.SetColumn((UIElement) element, formCellDataBase.Column);
      System.Windows.Controls.Grid.SetRow((UIElement) element, formCellDataBase.Row);
      System.Windows.Controls.Grid.SetColumnSpan((UIElement) element, formCellDataBase.ColumnSpan);
      System.Windows.Controls.Grid.SetRowSpan((UIElement) element, formCellDataBase.RowSpan);
    }
  }
}
