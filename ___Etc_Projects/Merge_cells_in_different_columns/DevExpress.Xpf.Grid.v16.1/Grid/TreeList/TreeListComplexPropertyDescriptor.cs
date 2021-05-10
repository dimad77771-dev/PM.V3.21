// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListComplexPropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Access;
using System;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListComplexPropertyDescriptor : ComplexPropertyDescriptorReflection
  {
    protected TreeListDataProvider Provider;

    public TreeListComplexPropertyDescriptor(TreeListDataProvider provider, object sourceObject, string path)
      : base(sourceObject, path)
    {
      this.Provider = provider;
    }

    public TreeListComplexPropertyDescriptor(TreeListDataProvider provider, DataControllerBase controller, string path)
      : base(controller, path)
    {
      this.Provider = provider;
    }

    protected override PropertyDescriptor GetDescriptor(string name, object obj, Type type)
    {
      if (this.Provider != null)
      {
        DataColumnInfo dataColumnInfo = this.Provider.Columns[name];
        if (dataColumnInfo != null)
          return dataColumnInfo.PropertyDescriptor;
      }
      return base.GetDescriptor(name, obj, type);
    }
  }
}
