// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GroupCardRowControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.HitTest;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GroupCardRowControl : GroupRowControl
  {
    private Thickness borderTopThickness;
    private Border BorderTop;

    private CardView CardView
    {
      get
      {
        return (CardView) this.rowData.View;
      }
    }

    static GroupCardRowControl()
    {
      Type forType = typeof (GroupCardRowControl);
      FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) forType));
      DataControlPopupMenu.GridMenuTypeProperty.OverrideMetadata(forType, (PropertyMetadata) new FrameworkPropertyMetadata((object) GridMenuType.GroupRow));
      GridViewHitInfoBase.HitTestAcceptorProperty.OverrideMetadata(forType, new PropertyMetadata((object) new GroupRowTableViewHitTestAcceptor()));
    }

    public GroupCardRowControl(GroupRowData rowData)
      : base(rowData)
    {
    }

    public override void OnApplyTemplate()
    {
      base.OnApplyTemplate();
      this.BorderTop = (Border) this.GetTemplateChild("BorderTop");
      this.borderTopThickness = this.BorderTop.BorderThickness;
      this.IsPreviewExpandedChanged();
      this.UpdateCardLayoutChanged();
      if (this.FocusOffset <= 0.0)
        return;
      this.bottomLine = (Border) this.GetTemplateChild("BottomLine");
    }

    protected override void IsPreviewExpandedChanged()
    {
      if (this.BorderTop == null)
        return;
      if (!this.rowData.IsPreviewExpanded)
        this.BorderTop.BorderThickness = new Thickness(0.0);
      else
        this.BorderTop.BorderThickness = this.borderTopThickness;
    }

    protected override double CalcLevelOffset()
    {
      if (this.CardView != null)
        return this.CardView.LeftGroupAreaIndent * (double) this.rowData.Level;
      return 0.0;
    }

    protected override void UpdateCardLayoutChanged()
    {
      if (this.CardView == null || this.rootPanel == null)
        return;
      switch (this.CardView.Orientation)
      {
        case Orientation.Horizontal:
          ((FrameworkElement) this.rootPanel).LayoutTransform = (Transform) new RotateTransform(-90.0);
          break;
        case Orientation.Vertical:
          ((FrameworkElement) this.rootPanel).LayoutTransform = (Transform) new RotateTransform(0.0);
          break;
      }
    }
  }
}
