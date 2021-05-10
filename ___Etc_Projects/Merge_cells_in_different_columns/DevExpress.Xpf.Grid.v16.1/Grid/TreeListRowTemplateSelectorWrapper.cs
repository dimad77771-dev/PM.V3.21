// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.TreeListRowTemplateSelectorWrapper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.TreeList;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class TreeListRowTemplateSelectorWrapper : ActualTemplateSelectorWrapper
  {
    public TreeListRowTemplateSelectorWrapper(DataTemplateSelector dataTemplateSelector, DataTemplate dataTemplate)
      : base(dataTemplateSelector, dataTemplate)
    {
    }

    public override DataTemplate SelectTemplate(object item, DependencyObject container)
    {
      return this.SelectTemplateCore(item, container, true);
    }

    internal DataTemplate SelectTemplateCore(object item, DependencyObject container, bool checkActual)
    {
      DataTemplate dataTemplate1 = base.SelectTemplate(item, container);
      DataTemplate dataTemplate2 = (DataTemplate) null;
      TreeListRowData treeListRowData = item as TreeListRowData;
      TreeListView treeListView = (TreeListView) null;
      if (treeListRowData != null)
      {
        treeListView = ((RowDataBase) treeListRowData).View as TreeListView;
        if (!object.ReferenceEquals((object) dataTemplate1, (object) treeListView.DefaultDataRowTemplate))
          dataTemplate2 = dataTemplate1;
        else if (treeListRowData.Node != null)
        {
          DataTemplate template = treeListRowData.Node.Template;
          if (template != null)
            dataTemplate2 = template;
        }
      }
      DataTemplate dataTemplate3 = dataTemplate2 ?? dataTemplate1;
      if (treeListView != null && treeListView.AllowDefaultContentForHierarchicalDataTemplate && (treeListView.TreeDerivationMode == TreeDerivationMode.HierarchicalDataTemplate && dataTemplate3 != null) && (checkActual && dataTemplate3.Template == null))
        return treeListView.DefaultDataRowTemplate;
      return dataTemplate3;
    }
  }
}
