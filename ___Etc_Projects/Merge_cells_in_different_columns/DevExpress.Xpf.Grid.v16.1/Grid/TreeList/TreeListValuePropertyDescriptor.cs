// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListValuePropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System;
using System.ComponentModel;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListValuePropertyDescriptor : PropertyDescriptor
  {
    private TreeListDataProvider dataProvider;
    private PropertyDescriptor propDescriptor;

    public TreeListDataProvider DataProvider
    {
      get
      {
        return this.dataProvider;
      }
    }

    public override bool IsReadOnly
    {
      get
      {
        return true;
      }
    }

    public override string Category
    {
      get
      {
        return string.Empty;
      }
    }

    public override Type PropertyType
    {
      get
      {
        return this.propDescriptor.PropertyType;
      }
    }

    public override Type ComponentType
    {
      get
      {
        return this.propDescriptor.ComponentType;
      }
    }

    public TreeListValuePropertyDescriptor(TreeListDataProvider dataProvider, PropertyDescriptor descriptor)
      : base(descriptor.Name, (Attribute[]) null)
    {
      this.propDescriptor = descriptor;
      this.dataProvider = dataProvider;
    }

    public override void ResetValue(object component)
    {
    }

    public override bool CanResetValue(object component)
    {
      return false;
    }

    public override object GetValue(object component)
    {
      return this.GetValueCore((TreeListNode) component);
    }

    protected virtual object GetValueCore(TreeListNode node)
    {
      return this.DataProvider.DataHelper.GetValue(node, this.propDescriptor);
    }

    public override void SetValue(object component, object value)
    {
    }

    public override bool ShouldSerializeValue(object component)
    {
      return false;
    }
  }
}
