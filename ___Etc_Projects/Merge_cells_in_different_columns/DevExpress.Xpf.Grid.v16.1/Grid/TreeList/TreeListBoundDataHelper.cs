// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListBoundDataHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Access;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevExpress.Xpf.Grid.TreeList
{
  public abstract class TreeListBoundDataHelper : TreeListDataHelperBase
  {
    public override Type ItemType
    {
      get
      {
        return ListBindingHelper.GetListItemType((object) this.ListSource);
      }
    }

    public override bool IsReady
    {
      get
      {
        return this.ListSource != null;
      }
    }

    public override bool IsLoaded
    {
      get
      {
        if (this.IsReady)
          return this.ListSource.Count > 0;
        return false;
      }
    }

    public override bool AllowEdit
    {
      get
      {
        if (this.BindingList != null)
          return this.BindingList.AllowEdit;
        return true;
      }
    }

    public override bool AllowRemove
    {
      get
      {
        if (this.BindingList != null)
          return this.BindingList.AllowRemove;
        return true;
      }
    }

    public IList ListSource { get; private set; }

    protected ICollectionView CollectionViewSource { get; private set; }

    protected ITypedList TypedList
    {
      get
      {
        return this.ListSource as ITypedList;
      }
    }

    protected virtual IBindingList BindingList { get; set; }

    protected internal override bool SupportNotifications
    {
      get
      {
        return this.BindingList != null;
      }
    }

    public TreeListBoundDataHelper(TreeListDataProvider provider, object dataSource)
      : base(provider)
    {
      this.CollectionViewSource = dataSource as ICollectionView;
      this.ListSource = this.ExtractListSource(dataSource);
      this.BindingList = this.ListSource as IBindingList;
    }

    protected virtual IList ExtractListSource(object dataSource)
    {
      if (dataSource is ICollectionView)
        return DataBindingHelper.ExtractDataSourceFromCollectionView((IEnumerable) dataSource);
      return DataBindingHelper.ExtractDataSource(dataSource, true, false);
    }

    public override void PopulateColumns()
    {
      if (!this.IsReady)
        return;
      PropertyDescriptorCollection descriptorCollection = this.GetPropertyDescriptorCollection();
      if (descriptorCollection != null && this.DataProvider.CanUseFastPropertyDescriptors)
        descriptorCollection = DataListDescriptor.GetFastProperties(descriptorCollection);
      if (descriptorCollection != null)
      {
        foreach (PropertyDescriptor descriptor in descriptorCollection)
          this.PopulateColumn(this.GetActualPropertyDescriptor(descriptor));
      }
      ComplexColumnInfoCollection complexColumns = this.DataProvider.GetComplexColumns();
      if (complexColumns != null)
      {
        foreach (ComplexColumnInfo complexColumnInfo in (CollectionBase) complexColumns)
          this.PopulateColumn(this.GetActualComplexPropertyDescriptor(this.DataProvider, this.GetFirstItem(), complexColumnInfo.Name));
      }
      this.PatchColumnCollection(descriptorCollection);
      this.PopulateUnboundColumns();
    }

    protected override bool IsColumnVisible(DataColumnInfo column)
    {
      if (!base.IsColumnVisible(column))
        return false;
      if (!this.View.AutoPopulateServiceColumns)
        return this.IsColumnsVisibleCore(column);
      return true;
    }

    protected virtual bool IsColumnsVisibleCore(DataColumnInfo column)
    {
      if (column.Name != this.View.ImageFieldName)
        return column.Name != this.View.CheckBoxFieldName;
      return false;
    }

    protected virtual PropertyDescriptor GetActualComplexPropertyDescriptor(TreeListDataProvider provider, object obj, string name)
    {
      return (PropertyDescriptor) new TreeListComplexPropertyDescriptor(provider, obj, name);
    }

    protected virtual PropertyDescriptor GetActualPropertyDescriptor(PropertyDescriptor descriptor)
    {
      return descriptor;
    }

    protected virtual PropertyDescriptorCollection GetPropertyDescriptorCollection()
    {
      if (this.ListSource == null)
        return (PropertyDescriptorCollection) null;
      if (this.TypedList != null)
        return this.TypedList.GetItemProperties((PropertyDescriptor[]) null);
      Type itemPropertyType = this.GetListItemPropertyType(this.ListSource);
      if (itemPropertyType != (Type) null && !ExpandoPropertyDescriptor.IsDynamicType(itemPropertyType))
      {
        if (this.DataProvider.UseFirstRowTypeWhenPopulatingColumns(itemPropertyType) && this.ListSource.Count > 0)
          return TypeDescriptor.GetProperties(this.ListSource[0].GetType());
        if (itemPropertyType.Equals(typeof (string)) || itemPropertyType.IsPrimitive || itemPropertyType.IsDefined(typeof (DataPrimitiveAttribute), true))
          return this.CreateSimplePropertyDescriptor();
        if (this.CollectionViewSource != null)
        {
          PropertyDescriptorCollection iitemProperties = this.GetIItemProperties((object) this.CollectionViewSource);
          if (iitemProperties != null)
            return iitemProperties;
        }
        return TypeDescriptor.GetProperties(itemPropertyType);
      }
      object firstItem = this.GetFirstItem();
      if (firstItem == null)
        return (PropertyDescriptorCollection) null;
      if (ExpandoPropertyDescriptor.IsDynamicType(firstItem.GetType()))
        return this.GetExpandoObjectProperties(firstItem);
      return TypeDescriptor.GetProperties(firstItem);
    }

    private PropertyDescriptorCollection GetExpandoObjectProperties(object item)
    {
      if (item == null)
        return (PropertyDescriptorCollection) null;
      IDictionary<string, object> dictionary = item as IDictionary<string, object>;
      if (dictionary == null)
        return (PropertyDescriptorCollection) null;
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (KeyValuePair<string, object> keyValuePair in (IEnumerable<KeyValuePair<string, object>>) dictionary)
        propertyDescriptorList.Add((PropertyDescriptor) new ExpandoPropertyDescriptor((DataControllerBase) null, keyValuePair.Key, keyValuePair.Value == null ? (Type) null : keyValuePair.Value.GetType()));
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    protected PropertyDescriptorCollection GetIItemProperties(object source)
    {
      IItemProperties itemProperties = source as IItemProperties;
      if (itemProperties == null)
        return (PropertyDescriptorCollection) null;
      IList<ItemPropertyInfo> itemPropertyInfoList = (IList<ItemPropertyInfo>) itemProperties.ItemProperties;
      if (itemPropertyInfoList == null)
        return (PropertyDescriptorCollection) null;
      List<PropertyDescriptor> propertyDescriptorList = new List<PropertyDescriptor>();
      foreach (ItemPropertyInfo itemPropertyInfo in (IEnumerable<ItemPropertyInfo>) itemPropertyInfoList)
      {
        if (itemPropertyInfo.Descriptor is PropertyDescriptor)
          propertyDescriptorList.Add((PropertyDescriptor) itemPropertyInfo.Descriptor);
      }
      return new PropertyDescriptorCollection(propertyDescriptorList.ToArray());
    }

    protected Type GetListItemPropertyType(IList list)
    {
      return DataProviderBase.GetListItemPropertyType(list, this.CollectionViewSource);
    }

    private PropertyDescriptorCollection CreateSimplePropertyDescriptor()
    {
      return new PropertyDescriptorCollection(new PropertyDescriptor[1]{ (PropertyDescriptor) new SimpleListPropertyDescriptor() });
    }

    protected object GetFirstItem()
    {
      if (this.ListSource == null || this.ListSource.Count <= 0)
        return (object) null;
      return this.ListSource[0];
    }

    protected virtual void PatchColumnCollection(PropertyDescriptorCollection properties)
    {
    }

    public override void LoadData()
    {
    }

    public override void Dispose()
    {
      base.Dispose();
      this.DataProvider.Nodes.Clear();
      this.CollectionViewSource = (ICollectionView) null;
      this.ListSource = (IList) null;
    }
  }
}
