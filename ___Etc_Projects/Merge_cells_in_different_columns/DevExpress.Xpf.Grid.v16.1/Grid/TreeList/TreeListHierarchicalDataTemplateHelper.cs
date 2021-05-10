// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeList.TreeListHierarchicalDataTemplateHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Data;

namespace DevExpress.Xpf.Grid.TreeList
{
  public class TreeListHierarchicalDataTemplateHelper : TreeListHierarchicalDataHelper
  {
    protected Dictionary<Type, DataTemplate> ImplicitTemplatesDictionary { get; set; }

    public TreeListHierarchicalDataTemplateHelper(TreeListDataProvider provider, object dataSource)
      : base(provider, dataSource)
    {
      this.ImplicitTemplatesDictionary = new Dictionary<Type, DataTemplate>();
    }

    protected override IEnumerable GetChildren(TreeListNode node)
    {
      if (node == null)
        return (IEnumerable) null;
      if (node.Content == null)
        return (IEnumerable) null;
      if (node.Template != null)
      {
        HierarchicalDataTemplate hierarchicalDataTemplate = node.Template as HierarchicalDataTemplate;
        if (hierarchicalDataTemplate != null)
          return this.GetChildrenByTemplate(node, hierarchicalDataTemplate);
      }
      if (this.View.DataRowTemplate != null || this.View.DataRowTemplateSelector != null)
      {
        HierarchicalDataTemplate hierarchicalDataTemplate = this.GetActualTemplateForNode(node) as HierarchicalDataTemplate;
        if (hierarchicalDataTemplate != null)
          return this.GetChildrenByTemplate(node, hierarchicalDataTemplate);
      }
      if (this.View.DataRowTemplateSelector == null && this.View.DataRowTemplate == null)
        return this.GetChildrenByImplicitDataTemplate(node);
      return (IEnumerable) null;
    }

    private DataTemplate GetActualTemplateForNode(TreeListNode node)
    {
      return (this.View.ActualDataRowTemplateSelector as TreeListRowTemplateSelectorWrapper).SelectTemplateCore(this.GetRowData(node), (DependencyObject) this.View, false);
    }

    private IEnumerable GetChildrenByTemplate(TreeListNode node, HierarchicalDataTemplate hierarchicalDataTemplate)
    {
      node.ItemTemplate = hierarchicalDataTemplate.ItemTemplate;
      return this.GetBindingValue(hierarchicalDataTemplate.ItemsSource, node.Content);
    }

    private IEnumerable GetBindingValue(BindingBase binding, object content)
    {
      if (binding == null || content == null)
        return (IEnumerable) null;
      return new BindingValueEvaluator(BindingCloneHelper.Clone(binding, content)).Value as IEnumerable;
    }

    protected override void AddNode(TreeListNodeCollection nodes, TreeListNode node)
    {
      this.AssignTemplate(node);
      base.AddNode(nodes, node);
    }

    private void AssignTemplate(TreeListNode node)
    {
      if (node.Template != null)
        return;
      DataTemplate actualTemplateForNode = this.GetActualTemplateForNode(node);
      if (actualTemplateForNode != null && actualTemplateForNode is HierarchicalDataTemplate)
        node.Template = actualTemplateForNode;
      else if (node.ParentNode != null && node.ParentNode.ItemTemplate != null)
        node.Template = node.ParentNode.ItemTemplate;
      else
        this.AsignImplicitDataTemplate(node);
    }

    protected override DataTemplate GetItemTemplate(TreeListNode treeListNode)
    {
      if (treeListNode.ItemTemplate != null)
        return treeListNode.ItemTemplate;
      HierarchicalDataTemplate hierarchicalDataTemplate = treeListNode.Template as HierarchicalDataTemplate;
      if (hierarchicalDataTemplate == null)
        return (DataTemplate) null;
      if (hierarchicalDataTemplate.ItemTemplateSelector == null)
        return hierarchicalDataTemplate.ItemTemplate;
      return hierarchicalDataTemplate.ItemTemplateSelector.SelectTemplate(this.GetRowData(treeListNode), (DependencyObject) this.View) ?? hierarchicalDataTemplate.ItemTemplate;
    }

    protected override void AsignImplicitDataTemplate(TreeListNode node)
    {
      DataTemplate dataTemplate = this.TryFindDataTemplate(node);
      if (dataTemplate == null)
        return;
      node.Template = dataTemplate;
      HierarchicalDataTemplate hierarchicalDataTemplate = dataTemplate as HierarchicalDataTemplate;
      if (hierarchicalDataTemplate == null)
        return;
      node.ItemTemplate = hierarchicalDataTemplate.ItemTemplate;
    }

    private IEnumerable GetChildrenByImplicitDataTemplate(TreeListNode node)
    {
      HierarchicalDataTemplate hierarchicalDataTemplate = this.TryFindDataTemplate(node) as HierarchicalDataTemplate;
      if (hierarchicalDataTemplate != null)
        return new BindingValueEvaluator(hierarchicalDataTemplate.ItemsSource).Value as IEnumerable;
      return (IEnumerable) null;
    }

    protected DataTemplate TryFindDataTemplate(TreeListNode node)
    {
      return this.TryFindDataTemplateCore(node.Content);
    }

    protected DataTemplate TryFindDataTemplateCore(object content)
    {
      if (content == null)
        return (DataTemplate) null;
      Type type = content.GetType();
      if (type == typeof (object))
        return (DataTemplate) null;
      if (this.ImplicitTemplatesDictionary.ContainsKey(type))
        return this.ImplicitTemplatesDictionary[type];
      DataTemplate dataTemplate = DefaultTemplateSelector.Instance.SelectTemplate(content, (DependencyObject) this.View.DataControl);
      if (dataTemplate == null)
        return (DataTemplate) null;
      this.ImplicitTemplatesDictionary.Add(type, dataTemplate);
      return dataTemplate;
    }

    public override void LoadData()
    {
      this.ImplicitTemplatesDictionary.Clear();
      base.LoadData();
    }
  }
}
