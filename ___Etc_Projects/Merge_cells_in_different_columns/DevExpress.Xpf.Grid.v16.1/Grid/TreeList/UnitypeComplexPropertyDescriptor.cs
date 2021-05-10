// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.UnitypeComplexPropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using System;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class UnitypeComplexPropertyDescriptor : TreeListComplexPropertyDescriptor
  {
    public UnitypeComplexPropertyDescriptor(TreeListDataProvider provider, object sourceObject, string path)
      : base(provider, sourceObject, path)
    {
    }

    public UnitypeComplexPropertyDescriptor(TreeListDataProvider provider, DataControllerBase controller, string path)
      : base(provider, controller, path)
    {
    }

    protected override PropertyDescriptor GetDescriptor(string name, object obj, Type type)
    {
      PropertyDescriptor descriptor = base.GetDescriptor(name, obj, type);
      if (descriptor != null && this.Provider != null)
        return (PropertyDescriptor) new UnitypeDataPropertyDescriptor(descriptor, this.Provider.View.AutoDetectColumnTypeInHierarchicalMode);
      return (PropertyDescriptor) null;
    }
  }
}
