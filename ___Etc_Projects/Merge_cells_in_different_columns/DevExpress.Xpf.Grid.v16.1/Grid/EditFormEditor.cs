// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.EditFormEditor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.EditForm;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DevExpress.Xpf.Grid
{
  public class EditFormEditor : ContentPresenter
  {
    public static readonly DependencyProperty EditFormCellDataProperty;
    public static readonly DependencyProperty FieldNameProperty;
    private IBaseEdit editorCore;

    public EditFormCellData EditFormCellData
    {
      get
      {
        return (EditFormCellData) this.GetValue(EditFormEditor.EditFormCellDataProperty);
      }
      set
      {
        this.SetValue(EditFormEditor.EditFormCellDataProperty, (object) value);
      }
    }

    public string FieldName
    {
      get
      {
        return (string) this.GetValue(EditFormEditor.FieldNameProperty);
      }
      set
      {
        this.SetValue(EditFormEditor.FieldNameProperty, (object) value);
      }
    }

    internal IBaseEdit Editor
    {
      get
      {
        return this.editorCore;
      }
      private set
      {
        if (this.editorCore == value)
          return;
        this.OnEditorChanged(this.editorCore, value);
        this.editorCore = value;
      }
    }

    static EditFormEditor()
    {
      DependencyPropertyRegistrator<EditFormEditor>.New().Register<EditFormCellData>((System.Linq.Expressions.Expression<Func<EditFormEditor, EditFormCellData>>) (d => d.EditFormCellData), out EditFormEditor.EditFormCellDataProperty, (EditFormCellData) null, new Action<EditFormEditor, DependencyPropertyChangedEventArgs>(EditFormEditor.OnEditFormCellDataChanged), new FrameworkPropertyMetadataOptions?()).Register<string>((System.Linq.Expressions.Expression<Func<EditFormEditor, string>>) (d => d.FieldName), out EditFormEditor.FieldNameProperty, (string) null, new Action<EditFormEditor, DependencyPropertyChangedEventArgs>(EditFormEditor.OnFieldNameChanged), new FrameworkPropertyMetadataOptions?());
      Type forType = typeof (EditFormEditor);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
    }

    public EditFormEditor()
    {
      this.Loaded += new RoutedEventHandler(this.OnLoaded);
      this.DataContextChanged += new DependencyPropertyChangedEventHandler(this.OnDataContextChanged);
    }

    private static void OnEditFormCellDataChanged(EditFormEditor sender, DependencyPropertyChangedEventArgs args)
    {
      sender.OnEditFormCellDataChanged(args.OldValue as EditFormCellData);
    }

    private static void OnFieldNameChanged(EditFormEditor sender, DependencyPropertyChangedEventArgs args)
    {
      sender.InvalidateFieldData();
    }

    private void OnEditFormCellDataChanged(EditFormCellData oldValue)
    {
      if (oldValue != null)
        oldValue.PropertyChanged -= new PropertyChangedEventHandler(this.OnDataPropertyChanged);
      if (this.EditFormCellData != null)
        this.EditFormCellData.PropertyChanged += new PropertyChangedEventHandler(this.OnDataPropertyChanged);
      this.OnDataChanged();
    }

    private void OnDataPropertyChanged(object sender, PropertyChangedEventArgs e)
    {
      if (!(e.PropertyName == "ValidationError"))
        return;
      this.ValidateEditor();
    }

    protected override void OnKeyDown(KeyEventArgs e)
    {
      base.OnKeyDown(e);
      this.ProcessKey(e.Key);
    }

    protected void ProcessKey(Key key)
    {
      if (key != Key.Return || this.Editor == null || !this.Editor.IsEditorActive)
        return;
      this.MoveNext();
    }

    internal virtual void MoveNext()
    {
      this.MoveFocus(false);
    }

    internal virtual void MovePrev()
    {
      this.MoveFocus(true);
    }

    private void MoveFocus(bool isReverse)
    {
      MoveFocusHelper.MoveFocus((FrameworkElement) this, isReverse);
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
      if (this.Editor == null || this.EditFormCellData == null || this.EditFormCellData.VisibleIndex != 0)
        return;
      this.Editor.Focus();
    }

    private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
      this.InvalidateFieldData();
    }

    private void InvalidateFieldData()
    {
      if (string.IsNullOrEmpty(this.FieldName))
        return;
      EditFormRowData editFormRowData = this.DataContext as EditFormRowData;
      if (editFormRowData == null)
        return;
      EditFormCellData editorData = editFormRowData.GetEditorData(this.FieldName);
      if (editorData == null)
        return;
      this.EditFormCellData = editorData;
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      if (this.ContentTemplate == null)
        return;
      IBaseEdit baseEdit = this.ContentTemplate.FindName("PART_Editor", (FrameworkElement) this) as IBaseEdit;
      if (baseEdit == null)
        return;
      this.Editor = baseEdit;
    }

    private void OnDataChanged()
    {
      if (this.EditFormCellData == null)
        this.Reset();
      else if (this.EditFormCellData.EditorTemplate != null)
      {
        this.Content = (object) this.EditFormCellData;
        this.ContentTemplate = this.EditFormCellData.EditorTemplate;
      }
      else
      {
        this.Reset();
        this.Editor = this.EditFormCellData.EditSettings.With<BaseEditSettings, IBaseEdit>((Func<BaseEditSettings, IBaseEdit>) (s => s.CreateEditor(EditorOptimizationMode.Disabled)));
        this.Content = (object) this.Editor;
      }
    }

    private void Reset()
    {
      if (this.Content != null)
        this.Content = (object) null;
      if (this.ContentTemplate == null)
        return;
      this.ContentTemplate = (DataTemplate) null;
    }

    private void OnEditorChanged(IBaseEdit oldValue, IBaseEdit newValue)
    {
      if (oldValue != null)
        oldValue.EditValueChanged -= new EditValueChangedEventHandler(this.OnEditValueChanged);
      if (newValue == null)
        return;
      newValue.EditValueChanged += new EditValueChangedEventHandler(this.OnEditValueChanged);
      newValue.EditValue = this.EditFormCellData.Value;
      if (this.EditFormCellData.ReadOnly)
        newValue.IsReadOnly = true;
      this.ValidateEditorCore(newValue);
    }

    private void ValidateEditor()
    {
      this.ValidateEditorCore(this.Editor);
    }

    private void ValidateEditorCore(IBaseEdit editor)
    {
      if (editor == null || this.EditFormCellData == null)
        return;
      editor.ValidationError = this.EditFormCellData.ValidationError;
    }

    private void OnEditValueChanged(object sender, EditValueChangedEventArgs e)
    {
      IBaseEdit baseEdit = (IBaseEdit) sender;
      if (this.EditFormCellData == null)
        return;
      this.EditFormCellData.HasInnerError = !baseEdit.DoValidate();
      if (baseEdit.EditValue == this.EditFormCellData.Value)
        return;
      this.EditFormCellData.Value = baseEdit.EditValue;
    }
  }
}
