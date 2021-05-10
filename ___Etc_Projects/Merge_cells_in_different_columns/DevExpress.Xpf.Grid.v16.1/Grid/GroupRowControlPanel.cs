// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupRowControlPanel
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.GroupRowLayout;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class GroupRowControlPanel : Panel
  {
    private GroupContainer groupsCore;

    public GroupContainer Groups
    {
      get
      {
        if (this.groupsCore == null)
        {
          this.groupsCore = new GroupContainer();
          this.groupsCore.Parent = (IGroupPanelItemOwner) new GroupPanelVisualItemOwner((Panel) this);
        }
        return this.groupsCore;
      }
    }

    public void ResetGroups()
    {
      this.groupsCore = (GroupContainer) null;
      this.Children.Clear();
    }

    protected override Size MeasureOverride(Size availableSize)
    {
      double width = 0.0;
      double height = 0.0;
      foreach (Group group in (IEnumerable<Group>) this.Groups)
      {
        Size size = GroupRowControlPanel.MeasureGroup(group, Math.Max(0.0, availableSize.Width - width), availableSize.Height);
        width += size.Width;
        if (size.Height > height)
          height = size.Height;
      }
      return new Size(width, height);
    }

    private static Size MeasureGroup(Group group, double availableWidth, double availableHeight)
    {
      double width = 0.0;
      double height = 0.0;
      foreach (Layer layer in (IEnumerable<Layer>) group)
      {
        Size size = GroupRowControlPanel.MeasureLayer(layer, availableWidth, availableHeight);
        if (size.Width > width)
          width = size.Width;
        if (size.Height > height)
          height = size.Height;
      }
      return new Size(width, height);
    }

    private static Size MeasureLayer(Layer layer, double availableWidth, double availableHeight)
    {
      double width = 0.0;
      double height = 0.0;
      foreach (Column column in (IEnumerable<Column>) layer)
      {
        UIElement element = column.Element;
        element.Measure(new Size(Math.Max(0.0, availableWidth - width), availableHeight));
        width += element.DesiredSize.Width;
        if (element.DesiredSize.Height > height)
          height = element.DesiredSize.Height;
      }
      return new Size(width, height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
      double x = 0.0;
      int num = 0;
      foreach (Group group in (IEnumerable<Group>) this.Groups)
      {
        ++num;
        x += GroupRowControlPanel.ArrangeGroup(group, x, finalSize, num == this.Groups.Count);
      }
      return finalSize;
    }

    private static double ArrangeGroup(Group group, double x, Size finalSize, bool isLastGroup)
    {
      double num1 = 0.0;
      foreach (Layer layer in (IEnumerable<Layer>) group)
      {
        double num2 = GroupRowControlPanel.ArrangeLayer(layer, x, finalSize, isLastGroup);
        if (num2 > num1)
          num1 = num2;
      }
      return num1;
    }

    private static double ArrangeLayer(Layer layer, double x, Size finalSize, bool isLastGroup)
    {
      double num1 = 0.0;
      int num2 = 0;
      foreach (Column column in (IEnumerable<Column>) layer)
      {
        ++num2;
        UIElement element = column.Element;
        double x1 = x + num1;
        double width = !isLastGroup || num2 != layer.Count ? element.DesiredSize.Width : Math.Max(0.0, finalSize.Width - x1);
        element.Arrange(new Rect(x1, 0.0, width, finalSize.Height));
        num1 += element.DesiredSize.Width;
      }
      return num1;
    }
  }
}
