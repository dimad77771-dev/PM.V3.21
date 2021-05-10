// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupPanelColumnHeaderDropTargetFactory
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupPanelColumnHeaderDropTargetFactory : ColumnHeaderDropTargetFactory, IDropTargetFactoryEx
  {
    protected override IDropTarget CreateDropTargetCore(Panel panel)
    {
      return (IDropTarget) new GroupPanelDropTarget(panel, true);
    }

    protected override bool IsCompatibleDataControl(DataControlBase sourceDataControl, DataControlBase targetDataControl)
    {
      return sourceDataControl.GetRootDataControl() == targetDataControl.GetRootDataControl();
    }

    IDropTarget IDropTargetFactoryEx.CreateDropTarget(UIElement dropTargetElement, UIElement sourceElement)
    {
      GroupPanelControl parentObject = LayoutHelper.FindParentObject<GroupPanelControl>((DependencyObject) dropTargetElement);
      if (parentObject == null)
        return (IDropTarget) new GroupPanelDropTarget((Panel) dropTargetElement, true);
      BaseGridColumnHeader header = (BaseGridColumnHeader) sourceElement;
      if (!((GridViewBase) header.GridView).ShowGroupPanel)
        return DevExpress.Xpf.Core.EmptyDropTarget.Instance;
      Panel panel = (Panel) LayoutHelper.FindElement((FrameworkElement) ((DetailControlPartContainer) LayoutHelper.FindElement((FrameworkElement) parentObject, (Predicate<FrameworkElement>) (e =>
      {
        GroupPanelContainer groupPanelContainer = e as GroupPanelContainer;
        if (groupPanelContainer != null && groupPanelContainer.View != null)
          return groupPanelContainer.View.EventTargetView == header.GridView.EventTargetView;
        return false;
      }))).GetTemplateChildInternal("groupPanelItemsControl"), (Predicate<FrameworkElement>) (e => e is Panel));
      return (IDropTarget) new GroupPanelDropTarget(panel, panel == dropTargetElement);
    }
  }
}
