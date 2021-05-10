// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Automation.ColumnHeaderAutomationPeerBase
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.GridData;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Automation.Peers;

namespace DevExpress.Xpf.Grid.Automation
{
  public class ColumnHeaderAutomationPeerBase : GridControlVirtualElementAutomationPeerBase
  {
    private ColumnBase column;
    private GridColumnHeader header;

    public GridColumnHeader ColumnHeader
    {
      get
      {
        return this.header;
      }
    }

    public ColumnBase Column
    {
      get
      {
        return this.column;
      }
    }

    public IColumnInfo ColumnInfo
    {
      get
      {
        return (IColumnInfo) this.Column;
      }
    }

    public ColumnHeaderAutomationPeerBase(DataControlBase dataControl, GridColumnHeader header)
      : base(dataControl)
    {
      this.header = header;
      this.column = header.Column;
    }

    public ColumnHeaderAutomationPeerBase(DataControlBase dataControl, ColumnBase column)
      : base(dataControl)
    {
      this.column = column;
    }

    protected override AutomationControlType GetAutomationControlTypeCore()
    {
      return AutomationControlType.HeaderItem;
    }

    protected override string GetNameCore()
    {
      if (this.ColumnInfo != null)
        return this.ColumnInfo.FieldName;
      return "";
    }

    protected override List<AutomationPeer> GetChildrenCore()
    {
      FrameworkElement frameworkElement = (FrameworkElement) this.ColumnHeader ?? this.GetFrameworkElement();
      if (frameworkElement != null)
        return DataControlAutomationPeerBase.GetUIChildrenCore((DependencyObject) frameworkElement, (IAutomationPeerCreator) this.DataControl.AutomationPeer);
      return (List<AutomationPeer>) null;
    }

    protected override FrameworkElement GetFrameworkElement()
    {
      return LayoutHelper.FindElement((FrameworkElement) this.DataControl, (Predicate<FrameworkElement>) (current =>
      {
        if (current is BaseGridColumnHeader)
          return ((BaseGridHeader) current).Column == this.Column;
        return false;
      }));
    }

    public override object GetPattern(PatternInterface patternInterface)
    {
      return (object) null;
    }
  }
}
