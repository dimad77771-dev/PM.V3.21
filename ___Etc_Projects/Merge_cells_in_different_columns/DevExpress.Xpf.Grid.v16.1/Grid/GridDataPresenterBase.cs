// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridDataPresenterBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Automation;
using DevExpress.Xpf.Grid.Native;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public abstract class GridDataPresenterBase : DataPresenterBase
  {
    protected override void UpdateViewCore()
    {
      ContentControl contentControl = this.Content as ContentControl;
      if (contentControl == null)
        return;
      contentControl.Content = (object) this.View.RootRowsContainer;
    }

    protected override AutomationPeer OnCreateAutomationPeer()
    {
      GridDataViewBase gridDataViewBase = DataControlBase.GetCurrentView((DependencyObject) this) as GridDataViewBase;
      if (gridDataViewBase == null)
        return (AutomationPeer) null;
      return (AutomationPeer) new DataPanelAutomationPeer(gridDataViewBase.DataControl);
    }

    internal override bool IsEnoughExpandingItems()
    {
      VirtualItemsEnumerator virtualItemsEnumerator = new VirtualItemsEnumerator((NodeContainer) this.View.RootNodeContainer);
      while (virtualItemsEnumerator.MoveNext())
      {
        GroupNode groupNode = virtualItemsEnumerator.Current as GroupNode;
        if (groupNode != null && !groupNode.IsFinished)
        {
          bool flag = this.IsElementVisible((RowNode) groupNode);
          if (groupNode.IsExpanding)
            groupNode.CanGenerateItems = flag;
          if (flag)
            return false;
        }
      }
      return true;
    }

    protected override bool IsElementVisible(RowNode node)
    {
      if (node.NodesContainer != null)
      {
        if (node.IsCollapsing)
          return true;
        if (!this.AreAllElementsVisible(node.NodesContainer))
          return false;
      }
      return base.IsElementVisible(node);
    }
  }
}
