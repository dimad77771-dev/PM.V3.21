// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.DefaultColumnWrapperGenerator
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Entity.Model;
using DevExpress.Mvvm.Native;
using DevExpress.Mvvm.UI.Native.ViewGenerator;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.Xpf.Grid.LookUp;
using System;

namespace DevExpress.Xpf.Grid.Native
{
  public class DefaultColumnWrapperGenerator : ColumnWrapperGeneratorBase
  {
    public DefaultColumnWrapperGenerator(GenerateBandWrapper bandWrapper)
      : base(bandWrapper)
    {
    }

    protected override Type GetLookUpEditType()
    {
      return typeof (LookUpEditSettings);
    }

    public override void DateTime(IEdmPropertyInfo property)
    {
      this.GenerateEditor(property, this.Mode == EditorsGeneratorBase.EditorsGeneratorMode.Edit ? typeof (DateEdit) : typeof (DateEditSettings), this.DateTimeInitializer(property, (MaskInfo) null, new bool?(true)), (object) null, (string) null);
    }

    public override void LookUp(IEdmPropertyInfo property, string itemsSource, string displayMember, ForeignKeyInfo foreignKeyInfo)
    {
      Type editType = !string.IsNullOrEmpty(itemsSource) ? this.GetLookUpEditType() : (this.Mode == EditorsGeneratorBase.EditorsGeneratorMode.Edit ? typeof (TextEdit) : (Type) null);
      this.GenerateEditor(property, editType, this.LookUpInitializer(property, itemsSource, displayMember, foreignKeyInfo), (object) null, string.IsNullOrEmpty(itemsSource) ? displayMember : (string) null);
    }

    public override void GenerateEditorFromResources(IEdmPropertyInfo property, object resourceKey, EditorsGeneratorBase.Initializer initializer)
    {
      this.GenerateEditor(property, (Type) null, initializer, resourceKey, (string) null);
    }
  }
}
