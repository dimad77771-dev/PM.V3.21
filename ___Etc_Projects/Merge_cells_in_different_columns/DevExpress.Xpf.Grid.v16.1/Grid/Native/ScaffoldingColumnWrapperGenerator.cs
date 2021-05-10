// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.ScaffoldingColumnWrapperGenerator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Entity.Model;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Native.ViewGenerator;
using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.LookUp;
using System;
using System.ComponentModel;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid.Native
{
  public class ScaffoldingColumnWrapperGenerator : DefaultColumnWrapperGenerator
  {
    public ScaffoldingColumnWrapperGenerator(GenerateBandWrapper bandWrapper)
      : base(bandWrapper)
    {
    }

    public override void Text(IEdmPropertyInfo property, bool multiline)
    {
      if (!multiline)
        base.Text(property, multiline);
      else if (EditorsGeneratorBase.GetMaxLength(property) > 0)
        this.GenerateEditor(property, (Type) null, EditorsGeneratorBase.Initializer.Default, (object) null, (string) null);
      else
        base.GenerateEditor(property, (Type) null, EditorsGeneratorBase.Initializer.Default, (object) null, (string) null);
    }

    public override void LookUp(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
    {
      if (string.IsNullOrEmpty(itemsSource))
        base.GenerateEditor(property, (Type) null, this.LookUpInitializer(property, itemsSource, displayMember, foreignKeyInfo), (object) null, displayMember);
      else
        base.GenerateEditor(property, typeof (LookUpEditSettings), this.LookUpInitializer(property, itemsSource, displayMember, foreignKeyInfo), (object) null, (string) null);
    }

    protected override EditorsGeneratorBase.Initializer LookUpInitializer(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
    {
      if (string.IsNullOrEmpty(itemsSource))
        return new EditorsGeneratorBase.Initializer((Action<IModelItem, IModelItem>) null, (Action<IModelItem>) (container => container.Properties[ColumnBase.ReadOnlyProperty.Name].SetValueIfNotSet((object) true)));
      return new EditorsGeneratorBase.Initializer((Action<IModelItem, IModelItem>) ((container, edit) =>
      {
        edit.Properties[LookUpEditSettingsBase.ItemsSourceProperty.Name].SetValueIfNotSet((object) new Binding(itemsSource));
        edit.Properties[LookUpEditSettingsBase.DisplayMemberProperty.Name].SetValueIfNotSet((object) displayMember);
      }), (Action<IModelItem>) null);
    }

    public override void Image(IEdmPropertyInfo property, bool readOnly)
    {
    }

    protected override EditorsGeneratorBase.Initializer ImageInitializer(IEdmPropertyInfo property, bool readOnly)
    {
      return EditorsGeneratorBase.Initializer.Default;
    }

    protected override void GenerateEditor(IEdmPropertyInfo property, Type editType, EditorsGeneratorBase.Initializer initializer, object resourceKey, string displayMember)
    {
      GenerateColumnWrapper wrapper = new GenerateColumnWrapper(this.BandWrapper.GetNextPropertyIndex(), property.PropertyType, (ColumnWrapperGeneratorBase) this, (PropertyDescriptor) property.ContextObject) { EditorResourceKey = resourceKey, Initializer = initializer };
      this.FillColumnWrapperProperties(wrapper, property, displayMember);
      this.ColumnWrapperCollection.Add(wrapper);
    }

    private void FillColumnWrapperProperties(GenerateColumnWrapper wrapper, IEdmPropertyInfo property, string displayMember)
    {
      wrapper.SetScaffSmartProperty = true;
      IEdmPropertyInfo propertyInfo = property;
      Action<string> setDisplayMember = (Action<string>) (x =>
      {
        string str = x + (string.IsNullOrEmpty(displayMember) ? (string) null : "." + displayMember);
        wrapper.FieldName = str;
        if (!(str != x))
          return;
        wrapper.Properties[BaseColumn.HeaderProperty] = (object) DevExpress.Data.Helpers.SplitStringHelper.SplitPascalCaseString(x);
      });
      Action<string> action1 = (Action<string>) null;
      // ISSUE: variable of the null type
      __Null local1 = null;
      // ISSUE: variable of the null type
      __Null local2 = null;
      Action<string> setDescription = action1;
      Action<bool?> action2 = (Action<bool?>) null;
      Action action3 = (Action) (() => wrapper.Visible = false);
      Action action4 = (Action) (() => wrapper.Visible = false);
      // ISSUE: variable of the null type
      __Null local3 = null;
      Action<bool?> setEditable = action2;
      Action setInvisible = action4;
      Action setHidden = action3;
      // ISSUE: variable of the null type
      __Null local4 = null;
      AttributesApplier.ApplyBaseAttributes(propertyInfo, setDisplayMember, (Action<string>) local1, (Action<string>) local2, setDescription, (Action) local3, setEditable, setInvisible, setHidden, (Action) local4);
    }

    private void FillEditSettingsWrapperProperties(IEdmPropertyInfo property, GenerateColumnWrapper columnWrapper, Type editType)
    {
      GenerateEditSettingsWrapper wrapper = new GenerateEditSettingsWrapper();
      editType.Do<Type>((Action<Type>) (x => wrapper.EditSettingsType = x));
      columnWrapper.EditSettingsWrapper = wrapper;
      AttributesApplier.ApplyDisplayFormatAttributes(property, (Action<string>) (x => wrapper.Properties[BaseEditSettings.NullTextProperty] = (object) x), (Action<string>) (x => wrapper.Properties[BaseEditSettings.DisplayFormatProperty] = (object) x), (Action) null);
    }
  }
}
