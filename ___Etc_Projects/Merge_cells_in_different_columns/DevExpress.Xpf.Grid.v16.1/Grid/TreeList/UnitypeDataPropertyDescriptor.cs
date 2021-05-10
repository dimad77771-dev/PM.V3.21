// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.UnitypeDataPropertyDescriptor
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data.Access;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Globalization;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class UnitypeDataPropertyDescriptor : PropertyDescriptor
  {
    private Dictionary<Type, PropertyDescriptor> descriptorCache = new Dictionary<Type, PropertyDescriptor>();
    private Type propertyType = typeof (object);
    private bool isReadOnly;

    public override Type PropertyType
    {
      get
      {
        return this.propertyType;
      }
    }

    public override bool IsReadOnly
    {
      get
      {
        return this.isReadOnly;
      }
    }

    public override string Category
    {
      get
      {
        return string.Empty;
      }
    }

    public override Type ComponentType
    {
      get
      {
        return typeof (object);
      }
    }

    public UnitypeDataPropertyDescriptor(PropertyDescriptor propertyDescriptor, bool usePropertyType)
      : this(propertyDescriptor.Name, propertyDescriptor.IsReadOnly)
    {
      if (propertyDescriptor is ExpandoPropertyDescriptor)
        this.descriptorCache.Add(typeof (ExpandoObject), propertyDescriptor);
      else if (propertyDescriptor.ComponentType != (Type) null && propertyDescriptor.ComponentType != typeof (object))
        this.descriptorCache.Add(propertyDescriptor.ComponentType, propertyDescriptor);
      if (!usePropertyType)
        return;
      this.propertyType = propertyDescriptor.PropertyType;
    }

    public UnitypeDataPropertyDescriptor(string propName, bool isReadOnly)
      : base(propName, (Attribute[]) null)
    {
      this.isReadOnly = isReadOnly;
    }

    public override void ResetValue(object component)
    {
    }

    public override bool CanResetValue(object component)
    {
      return false;
    }

    public override bool ShouldSerializeValue(object component)
    {
      return false;
    }

    public override object GetValue(object component)
    {
      PropertyDescriptor properyDescriptor = this.GetProperyDescriptor(component);
      if (properyDescriptor != null)
        return properyDescriptor.GetValue(component);
      return (object) null;
    }

    public override void SetValue(object component, object value)
    {
      PropertyDescriptor properyDescriptor = this.GetProperyDescriptor(component);
      if (properyDescriptor == null)
        return;
      properyDescriptor.SetValue(component, this.ConvertValue(value, properyDescriptor));
    }

    protected internal virtual PropertyDescriptor GetProperyDescriptor(object component)
    {
      if (component == null)
        return (PropertyDescriptor) null;
      Type type = component.GetType();
      PropertyDescriptor propertyDescriptor = (PropertyDescriptor) null;
      if (this.descriptorCache.TryGetValue(type, out propertyDescriptor))
        return propertyDescriptor;
      propertyDescriptor = TypeDescriptor.GetProperties(type)[this.Name];
      if (propertyDescriptor != null)
        this.descriptorCache[type] = propertyDescriptor;
      return propertyDescriptor;
    }

    protected Type GetDataType(PropertyDescriptor descriptor)
    {
      Type nullableType = descriptor.PropertyType;
      if (nullableType != (Type) null)
      {
        Type underlyingType = Nullable.GetUnderlyingType(nullableType);
        if (underlyingType != (Type) null)
          nullableType = underlyingType;
      }
      return nullableType;
    }

    protected virtual object ConvertValue(object val, PropertyDescriptor descriptor)
    {
      Type dataType = this.GetDataType(descriptor);
      bool flag = val == null || DBNull.Value.Equals(val);
      Type type = !flag ? val.GetType() : (Type) null;
      if (flag || dataType == (Type) null || dataType.IsAssignableFrom(type))
        return val;
      TypeConverter typeConverter = descriptor != null ? descriptor.Converter : (TypeConverter) null;
      if (typeConverter != null)
      {
        if (typeConverter.CanConvertFrom(type))
        {
          try
          {
            return typeConverter.ConvertFrom((ITypeDescriptorContext) null, CultureInfo.InvariantCulture, val);
          }
          catch
          {
          }
        }
      }
      return Convert.ChangeType(val, dataType, (IFormatProvider) CultureInfo.InvariantCulture);
    }
  }
}
