// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.GridDataControlAutomationPeer
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls.Primitives;

namespace DevExpress.Xpf.Grid.Automation
{
  public abstract class GridDataControlAutomationPeer : DataControlAutomationPeer
  {
    private HeaderPanelAutomationPeerBase headerPanelPeer;
    private DataPanelAutomationPeer dataPanelPeer;
    private FooterPanelAutomationPeer footerPanelPeer;
    private bool isDataPanelPeerChildrenCacheDirty;

    public HeaderPanelAutomationPeerBase HeaderPanelPeer
    {
      get
      {
        return this.headerPanelPeer;
      }
    }

    public DataPanelAutomationPeer DataPanelPeer
    {
      get
      {
        if (this.isDataPanelPeerChildrenCacheDirty && this.dataPanelPeer != null)
        {
          this.dataPanelPeer.ResetChildrenCachePlatformIndependent();
          this.isDataPanelPeerChildrenCacheDirty = false;
        }
        return this.dataPanelPeer;
      }
    }

    public FooterPanelAutomationPeer FooterPanelPeer
    {
      get
      {
        return this.footerPanelPeer;
      }
    }

    public GridDataControlAutomationPeer(DataControlBase dataControl)
      : base(dataControl)
    {
    }

    protected abstract HeaderPanelAutomationPeerBase CreateHeaderPeer();

    protected internal override void ResetDataPanelPeer()
    {
      this.isDataPanelPeerChildrenCacheDirty = true;
    }

    protected internal override void ResetPeers()
    {
      this.dataPanelPeer = (DataPanelAutomationPeer) null;
      this.headerPanelPeer = (HeaderPanelAutomationPeerBase) null;
      this.footerPanelPeer = (FooterPanelAutomationPeer) null;
    }

    protected internal override void ResetHeadersChildrenCache()
    {
      if (this.HeaderPanelPeer == null)
        return;
      foreach (AutomationPeer child in this.HeaderPanelPeer.GetChildren())
        child.ResetChildrenCache();
    }

    protected internal override void ResetDataPanelPeerCache()
    {
      if (!this.isDataPanelPeerChildrenCacheDirty)
        return;
      this.ResetChildrenCache();
      if (this.DataPanelPeer == null)
        return;
      this.DataPanelPeer.ResetChildrenCachePlatformIndependent();
      this.isDataPanelPeerChildrenCacheDirty = false;
    }

    protected internal override void ResetDataPanelChildrenForce()
    {
      if (this.DataPanelPeer == null)
        return;
      this.DataPanelPeer.ResetChildrenCachePlatformIndependent();
    }

    protected virtual DataPanelAutomationPeer CreateDataPanelPeer()
    {
      UIElement element = DataControlAutomationPeerBase.FindObjectInVisualTreeByType((DependencyObject) this.DataControl, typeof (DataPresenter)) as UIElement;
      if (this.DataControl.DataView is CardView)
        element = DataControlAutomationPeerBase.FindObjectInVisualTreeByType((DependencyObject) this.DataControl, typeof (CardDataPresenter)) as UIElement;
      if (element == null)
        return (DataPanelAutomationPeer) null;
      return (DataPanelAutomationPeer) UIElementAutomationPeer.CreatePeerForElement(element);
    }

    protected virtual FooterPanelAutomationPeer CreateFooterPeer()
    {
      FrameworkElement footerPanelUiElement = GridControlAutomationPeerHelper.GetFooterPanelUIElement((FrameworkElement) this.DataControl);
      if (footerPanelUiElement == null)
        return (FooterPanelAutomationPeer) null;
      return new FooterPanelAutomationPeer(this.DataControl, footerPanelUiElement);
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      List<AutomationPeer> automationPeerList = new List<AutomationPeer>();
      if (this.DataControl.DataView.ShowColumnHeaders)
      {
        if (this.headerPanelPeer == null)
          this.headerPanelPeer = this.CreateHeaderPeer();
        automationPeerList.Add((AutomationPeer) this.HeaderPanelPeer);
      }
      this.dataPanelPeer = this.CreateDataPanelPeer();
      if (this.DataPanelPeer != null)
        automationPeerList.Add((AutomationPeer) this.DataPanelPeer);
      if (this.DataControl.DataView.ShowTotalSummary)
      {
        if (this.footerPanelPeer == null)
          this.footerPanelPeer = this.CreateFooterPeer();
        if (this.footerPanelPeer != null)
          automationPeerList.Add((AutomationPeer) this.FooterPanelPeer);
      }
      else
        this.footerPanelPeer = (FooterPanelAutomationPeer) null;
      return automationPeerList;
    }

    protected internal override AutomationPeer GetCellPeer(int rowHandle, ColumnBase column, bool force = false)
    {
      GridRowAutomationPeer rowAutomationPeer = this.GetRowPeer(rowHandle) as GridRowAutomationPeer;
      if (rowAutomationPeer == null)
        return (AutomationPeer) null;
      return (AutomationPeer) rowAutomationPeer.GetCellPeer(column.VisibleIndex, force);
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      if (patternInterface == PatternInterface.Grid)
        return (object) this;
      if (patternInterface == PatternInterface.Scroll && this.DataControl.DataView.DataPresenter != null)
      {
        FrameworkElement frameworkElement = (FrameworkElement) ((IScrollInfo) this.DataControl.DataView.DataPresenter).ScrollOwner;
        if (frameworkElement != null)
          return (object) (UIElementAutomationPeer.CreatePeerForElement((UIElement) frameworkElement) as IScrollProvider);
      }
      return base.GetPattern(patternInterface);
    }
  }
}
