// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.CardViewPrintingHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.DataNodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public static class CardViewPrintingHelper
  {
    private static readonly DependencyPropertyKey PrintCardInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintCardInfo", typeof (CardViewPrintRowInfo), typeof (CardViewPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintCardInfoProperty = CardViewPrintingHelper.PrintCardInfoPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey CardViewPrintCellInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("CardViewPrintCellInfo", typeof (CardViewPrintCellInfo), typeof (CardViewPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty CardViewPrintCellInfoProperty = CardViewPrintingHelper.CardViewPrintCellInfoPropertyKey.DependencyProperty;

    public static Thickness GetActualPrintCardMargin(Thickness printCardMargin, bool isFristCard)
    {
      if (!isFristCard)
        return printCardMargin;
      return new Thickness(0.0, printCardMargin.Top, printCardMargin.Right, printCardMargin.Bottom);
    }

    internal static IRootDataNode CreatePrintingTreeNode(CardView view, Size usablePageSize, ItemsGenerationStrategyBase itemsGenerationStrategy = null)
    {
      Size size = new Size(usablePageSize.Width, 0.0);
      CardViewPrintingDataTreeBuilder printingDataTreeBuilder = view.CreatePrintingDataTreeBuilder(size.Width, itemsGenerationStrategy);
      printingDataTreeBuilder.View.layoutUpdatedLocker.DoLockedAction(new Action(((PrintingDataTreeBuilderBase) printingDataTreeBuilder).GenerateAllItems));
      CardViewPrintingHelper.DoAfterGenerateNodeTreeAction(printingDataTreeBuilder);
      return (IRootDataNode) new CardViewRootPrintingNode(printingDataTreeBuilder, usablePageSize);
    }

    internal static void CreatePrintingTreeNodeAsync(CardView view, Size usablePageSize)
    {
      ItemsGenerationAsyncServerModeStrategyAsync itemsGenerationStrategy = new ItemsGenerationAsyncServerModeStrategyAsync((DataViewBase) view);
      itemsGenerationStrategy.StartFetchingAllFilteredAndSortedRows((Action) (() => view.RaiseCreateRootNodeCompleted(CardViewPrintingHelper.CreatePrintingTreeNode(view, usablePageSize, (ItemsGenerationStrategyBase) itemsGenerationStrategy))));
    }

    private static void DoAfterGenerateNodeTreeAction(CardViewPrintingDataTreeBuilder treeBuilder)
    {
    }

    public static CardViewPrintRowInfo GetPrintCardInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (CardViewPrintRowInfo) element.GetValue(CardViewPrintingHelper.PrintCardInfoProperty);
    }

    internal static void SetPrintCardInfo(DependencyObject element, CardViewPrintRowInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(CardViewPrintingHelper.PrintCardInfoPropertyKey, (object) value);
    }

    public static CardViewPrintCellInfo GetCardViewPrintCellInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (CardViewPrintCellInfo) element.GetValue(CardViewPrintingHelper.CardViewPrintCellInfoProperty);
    }

    internal static void SetCardViewPrintCellInfo(DependencyObject element, CardViewPrintCellInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(CardViewPrintingHelper.CardViewPrintCellInfoPropertyKey, (object) value);
    }

    public static void UpdatePageBricks(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> pageBrickUpdaters, bool updateTopRowBricks, bool skipUpdateLastRowBricks)
    {
      if (pageBrickUpdaters.Count == 0)
        return;
      List<VisualBrick> bricks = new List<VisualBrick>();
      while (pageBrickEnumerator.MoveNext())
      {
        VisualBrick visualBrick = pageBrickEnumerator.Current as VisualBrick;
        if (visualBrick != null)
        {
          bricks.Add(visualBrick);
          if (updateTopRowBricks)
          {
            TextBrick textBrick = visualBrick as TextBrick;
            if (textBrick != null && (double) textBrick.Rect.Y == 0.0)
              textBrick.Sides |= BorderSide.Top;
          }
        }
      }
      GridPrintingHelper.UpdateTopBorders(pageBrickUpdaters, bricks);
    }
  }
}
