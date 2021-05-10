// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridPrintingHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Grid.Printing;
using DevExpress.Xpf.Printing.Native;
using DevExpress.Xpf.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraPrinting.DataNodes;
using DevExpress.XtraPrinting.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace DevExpress.Xpf.Grid
{
  public static class GridPrintingHelper
  {
    private static readonly DependencyPropertyKey PrintColumnWidthPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintColumnWidth", typeof (double), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) 0.0, (PropertyChangedCallback) null, new CoerceValueCallback(GridPrintingHelper.CoercePrintColumnWidth)));
    public static readonly DependencyProperty PrintColumnWidthProperty = GridPrintingHelper.PrintColumnWidthPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintGroupRowInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintGroupRowInfo", typeof (PrintGroupRowInfo), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintGroupRowInfoProperty = GridPrintingHelper.PrintGroupRowInfoPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintGroupSummaryInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintGroupSummaryInfo", typeof (PrintGroupSummaryInfo), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintGroupSummaryInfoProperty = GridPrintingHelper.PrintGroupSummaryInfoPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintFixedFooterTextLeftPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintFixedFooterTextLeft", typeof (string), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintFixedFooterTextLeftProperty = GridPrintingHelper.PrintFixedFooterTextLeftPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintFixedFooterTextRightPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintFixedFooterTextRight", typeof (string), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintFixedFooterTextRightProperty = GridPrintingHelper.PrintFixedFooterTextRightPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintCellInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintCellInfo", typeof (PrintCellInfo), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintCellInfoProperty = GridPrintingHelper.PrintCellInfoPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintRowInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintRowInfo", typeof (PrintRowInfo), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintRowInfoProperty = GridPrintingHelper.PrintRowInfoPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintColumnPositionPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintColumnPosition", typeof (ColumnPosition), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) ColumnPosition.Standalone));
    public static readonly DependencyProperty PrintColumnPositionProperty = GridPrintingHelper.PrintColumnPositionPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintHasLeftSiblingPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintHasLeftSibling", typeof (bool), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
    public static readonly DependencyProperty PrintHasLeftSiblingProperty = GridPrintingHelper.PrintHasLeftSiblingPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintHasRightSiblingPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintHasRightSibling", typeof (bool), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false));
    public static readonly DependencyProperty PrintHasRightSiblingProperty = GridPrintingHelper.PrintHasRightSiblingPropertyKey.DependencyProperty;
    private static readonly DependencyPropertyKey PrintBandInfoPropertyKey = DependencyPropertyManager.RegisterAttachedReadOnly("PrintBandInfo", typeof (PrintBandInfo), typeof (GridPrintingHelper), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty PrintBandInfoProperty = GridPrintingHelper.PrintBandInfoPropertyKey.DependencyProperty;
    public const double GroupIndent = 20.0;
    public const double DefaultDetailTopIndent = 4.0;
    public const double DefaultDetailBottomIndent = 10.0;
    public const bool DefaultAllowPrintDetails = true;
    public const bool DefaultAllowPrintEmptyDetails = false;
    public const bool DefaultPrintAllDetails = false;

    public static PrintRowInfo GetPrintRowInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (PrintRowInfo) element.GetValue(GridPrintingHelper.PrintRowInfoProperty);
    }

    internal static void SetPrintRowInfo(DependencyObject element, PrintRowInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintRowInfoPropertyKey, (object) value);
    }

    public static PrintCellInfo GetPrintCellInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (PrintCellInfo) element.GetValue(GridPrintingHelper.PrintCellInfoProperty);
    }

    internal static void SetPrintCellInfo(DependencyObject element, PrintCellInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintCellInfoPropertyKey, (object) value);
    }

    public static PrintGroupRowInfo GetPrintGroupRowInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (PrintGroupRowInfo) element.GetValue(GridPrintingHelper.PrintGroupRowInfoProperty);
    }

    public static PrintGroupSummaryInfo GetPrintGroupSummaryInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (PrintGroupSummaryInfo) element.GetValue(GridPrintingHelper.PrintGroupSummaryInfoProperty);
    }

    public static string GetPrintFixedFooterTextLeft(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (string) element.GetValue(GridPrintingHelper.PrintFixedFooterTextLeftProperty);
    }

    public static string GetPrintFixedFooterTextRight(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (string) element.GetValue(GridPrintingHelper.PrintFixedFooterTextRightProperty);
    }

    public static PrintBandInfo GetPrintBandInfo(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (PrintBandInfo) element.GetValue(GridPrintingHelper.PrintBandInfoProperty);
    }

    internal static void SetPrintGroupRowInfo(DependencyObject element, PrintGroupRowInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintGroupRowInfoPropertyKey, (object) value);
    }

    internal static void SetPrintGroupSummaryInfo(DependencyObject element, PrintGroupSummaryInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintGroupSummaryInfoPropertyKey, (object) value);
    }

    internal static void SetPrintFixedFooterTextLeft(DependencyObject element, string value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintFixedFooterTextLeftPropertyKey, (object) value);
    }

    internal static void SetPrintFixedFooterTextRight(DependencyObject element, string value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintFixedFooterTextRightPropertyKey, (object) value);
    }

    internal static void SetPrintBandInfo(DependencyObject element, PrintBandInfo value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintBandInfoPropertyKey, (object) value);
    }

    private static object CoercePrintColumnWidth(DependencyObject d, object value)
    {
      double num = (double) value;
      if (num < 0.0)
        num = 0.0;
      return (object) num;
    }

    public static double GetPrintColumnWidth(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (double) element.GetValue(GridPrintingHelper.PrintColumnWidthProperty);
    }

    internal static void SetPrintColumnWidth(DependencyObject element, double value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintColumnWidthPropertyKey, (object) value);
    }

    public static bool GetPrintHasLeftSibling(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (bool) element.GetValue(GridPrintingHelper.PrintHasLeftSiblingProperty);
    }

    internal static void SetPrintHasLeftSibling(DependencyObject element, bool value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintHasLeftSiblingPropertyKey, (object) value);
    }

    public static bool GetPrintHasRightSibling(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (bool) element.GetValue(GridPrintingHelper.PrintHasRightSiblingProperty);
    }

    internal static void SetPrintHasRightSibling(DependencyObject element, bool value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintHasRightSiblingPropertyKey, (object) value);
    }

    public static ColumnPosition GetPrintColumnPosition(DependencyObject element)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      return (ColumnPosition) element.GetValue(GridPrintingHelper.PrintColumnPositionProperty);
    }

    internal static void SetPrintColumnPosition(DependencyObject element, ColumnPosition value)
    {
      if (element == null)
        throw new ArgumentNullException("element");
      element.SetValue(GridPrintingHelper.PrintColumnPositionPropertyKey, (object) value);
    }

    private static void CalcPrintColumnsAutoWidthLayout(ITableView view, Size pageSize)
    {
      view.TableViewBehavior.CreatePrintViewInfo().CreateColumnsLayoutCalculator(true).CalcActualLayout(pageSize, PrintLayoutAssigner.Printing, false, false, true);
    }

    private static void CalcPrintColumnsBandsLayout(ITableView view, Size pageSize, BandsLayoutBase bandsLayout)
    {
      view.TableViewBehavior.CreatePrintViewInfo(bandsLayout).CreateColumnsLayoutCalculator(view.PrintAutoWidth).CalcActualLayout(pageSize, PrintLayoutAssigner.Printing, false, false, true);
    }

    private static void CalcPrintColumnWidths(ITableView view)
    {
      foreach (ColumnBase columnBase in (IEnumerable<ColumnBase>) view.ViewBase.VisibleColumnsCore)
        GridPrintingHelper.SetPrintColumnWidth((DependencyObject) columnBase, columnBase.ActualHeaderWidth);
    }

    private static double CalcTotalPrintHeaderWidth(ITableView view, BandsLayoutBase bandsLayout)
    {
      double num = 0.0;
      if (bandsLayout == null)
      {
        foreach (ColumnBase printableColumn in view.ViewBase.PrintableColumns)
          num += GridPrintingHelper.GetPrintColumnWidth((DependencyObject) printableColumn);
      }
      else
      {
        foreach (BandBase visibleBand in bandsLayout.VisibleBands)
          num += GridPrintingHelper.GetPrintColumnWidth((DependencyObject) visibleBand);
      }
      return num;
    }

    public static void UpdatePageBricks(IEnumerator pageBrickEnumerator, Dictionary<IVisualBrick, IOnPageUpdater> pageBrickUpdaters, bool updateTopRowBricks, bool skipUpdateLastRowBricks)
    {
      if (pageBrickUpdaters.Count == 0)
        return;
      Dictionary<int, List<VisualBrick>> dictionary = new Dictionary<int, List<VisualBrick>>();
      VisualBrick visualBrick1 = (VisualBrick) null;
      double num = double.MinValue;
      List<VisualBrick> bricks = new List<VisualBrick>();
      while (pageBrickEnumerator.MoveNext())
      {
        visualBrick1 = pageBrickEnumerator.Current as VisualBrick;
        if (visualBrick1 != null)
        {
          bricks.Add(visualBrick1);
          int key = 0;
          if (visualBrick1.TryGetAttachedValue<int>(BrickAttachedProperties.ParentID, out key))
          {
            if (!dictionary.ContainsKey(key))
              dictionary[key] = new List<VisualBrick>();
            dictionary[key].Add(visualBrick1);
            if (updateTopRowBricks)
            {
              TextBrick textBrick = visualBrick1 as TextBrick;
              if (textBrick != null && (double) textBrick.Rect.Y == 0.0)
                textBrick.Sides |= BorderSide.Top;
            }
            if ((double) visualBrick1.Rect.Y > num)
              num = (double) visualBrick1.Rect.Y;
          }
        }
      }
      GridPrintingHelper.UpdateTopBorders(pageBrickUpdaters, bricks);
      if (visualBrick1 == null)
        return;
      IVisualBrick visualBrick2 = pageBrickUpdaters.Keys.Last<IVisualBrick>();
      foreach (List<VisualBrick> visualBrickList in dictionary.Values.Reverse<List<VisualBrick>>())
      {
        foreach (VisualBrick visualBrick3 in visualBrickList)
        {
          IOnPageUpdater onPageUpdater;
          if ((!skipUpdateLastRowBricks || visualBrick2 != visualBrick3 || (double) visualBrick2.Rect.Y == (double) visualBrick1.Rect.Y) && (pageBrickUpdaters.TryGetValue((IVisualBrick) visualBrick3, out onPageUpdater) && onPageUpdater is LastOnPageUpdater))
          {
            if ((double) visualBrick3.Rect.Bottom < num || Math.Abs((double) visualBrick3.Rect.Bottom - num) <= 0.01)
              return;
            onPageUpdater.Update((IVisualBrick) visualBrick3);
            return;
          }
        }
      }
    }

    public static void UpdateTopBorders(Dictionary<IVisualBrick, IOnPageUpdater> pageBrickUpdaters, List<VisualBrick> bricks)
    {
      foreach (KeyValuePair<IVisualBrick, IOnPageUpdater> pageBrickUpdater in pageBrickUpdaters)
      {
        VisualBrick vb = pageBrickUpdater.Key as VisualBrick;
        IOnPageUpdater footerRowUpdater = (IOnPageUpdater) (pageBrickUpdater.Value as FooterRowTobBorgerOnPageUpdater);
        if (footerRowUpdater != null)
        {
          for (int index = 0; index < bricks.Count; ++index)
          {
            if (bricks[index] == vb)
            {
              float y = bricks[index].Rect.Y;
              GridPrintingHelper.UpdateNeedTopRowIfNeed(vb, footerRowUpdater, bricks, pageBrickUpdaters, y);
              break;
            }
            if (bricks[index] is PanelBrick)
            {
              float y = bricks[index].Rect.Y;
              foreach (VisualBrick brick in (Collection<Brick>) bricks[index].Bricks)
              {
                if (brick == vb)
                {
                  GridPrintingHelper.UpdateNeedTopRowIfNeed(vb, footerRowUpdater, bricks, pageBrickUpdaters, y);
                  break;
                }
              }
            }
          }
        }
        else
        {
          IOnPageUpdater onPageUpdater = (IOnPageUpdater) (pageBrickUpdater.Value as TopBorderOnPageUpdater);
          if (onPageUpdater != null)
            onPageUpdater.Update((IVisualBrick) vb);
        }
      }
    }

    private static void UpdateNeedTopRowIfNeed(VisualBrick vb, IOnPageUpdater footerRowUpdater, List<VisualBrick> bricks, Dictionary<IVisualBrick, IOnPageUpdater> pageBrickUpdaters, float y)
    {
      List<VisualBrick> allBricks = GridPrintingHelper.GetAllBricks(bricks.Where<VisualBrick>((Func<VisualBrick, bool>) (brick => GridPrintingHelper.IsPrevBrick(brick, y))).ToList<VisualBrick>());
      if (allBricks.Count == 0)
        return;
      int detailLevel = ((InfoProviderOnPageUpdaterBase) footerRowUpdater).DetailLevel;
      foreach (VisualBrick visualBrick in allBricks)
      {
        IOnPageUpdater onPageUpdater = (IOnPageUpdater) null;
        if (pageBrickUpdaters.TryGetValue((IVisualBrick) visualBrick, out onPageUpdater))
        {
          InfoProviderOnPageUpdaterBase onPageUpdaterBase = onPageUpdater as InfoProviderOnPageUpdaterBase;
          if (onPageUpdaterBase != null && onPageUpdaterBase.DetailLevel > detailLevel)
          {
            footerRowUpdater.Update((IVisualBrick) vb);
            break;
          }
        }
      }
    }

    private static VisualBrick GetBrick(VisualBrick brick)
    {
      return brick;
    }

    private static List<VisualBrick> GetAllBricks(List<VisualBrick> bricks)
    {
      List<VisualBrick> visualBrickList = new List<VisualBrick>();
      foreach (VisualBrick brick1 in bricks)
      {
        visualBrickList.Add(GridPrintingHelper.GetBrick(brick1));
        PanelBrick panelBrick = brick1 as PanelBrick;
        if (panelBrick != null)
        {
          foreach (VisualBrick brick2 in (Collection<Brick>) panelBrick.Bricks)
            visualBrickList.Add(GridPrintingHelper.GetBrick(brick2));
        }
      }
      return visualBrickList;
    }

    private static bool IsPrevBrick(VisualBrick brick, float y)
    {
      return Math.Floor((double) brick.Rect.Y + (double) brick.Rect.Height) == Math.Floor((double) y);
    }

    internal static IRootDataNode CreatePrintingTreeNode(ITableView view, Size usablePageSize, MasterDetailPrintInfo masterDetailPrintInfo = null, ItemsGenerationStrategyBase itemsGenerationStrategy = null)
    {
      Size pageSize = new Size(usablePageSize.Width, 0.0);
      BandsLayoutBase bandsLayout = view.ViewBase.DataControl.BandsLayoutCore.Return<BandsLayoutBase, BandsLayoutBase>((Func<BandsLayoutBase, BandsLayoutBase>) (layout => layout.CloneAndFillEmptyBands()), (Func<BandsLayoutBase>) null);
      GridPrintingHelper.CalcPrintLayout(view, pageSize, bandsLayout);
      double totalHeaderWidth = GridPrintingHelper.CalcTotalPrintHeaderWidth(view, bandsLayout);
      PrintingDataTreeBuilderBase printingDataTreeBuilder = ((DataViewBase) view).CreatePrintingDataTreeBuilder(totalHeaderWidth, itemsGenerationStrategy, masterDetailPrintInfo, bandsLayout);
      printingDataTreeBuilder.View.layoutUpdatedLocker.DoLockedAction(new Action(printingDataTreeBuilder.GenerateAllItems));
      GridPrintingHelper.DoAfterGenerateNodeTreeAction(printingDataTreeBuilder);
      return (IRootDataNode) new GridRootPrintingNode(printingDataTreeBuilder, usablePageSize);
    }

    private static void CalcPrintLayout(ITableView view, Size pageSize, BandsLayoutBase bandsLayout)
    {
      if (bandsLayout != null)
        GridPrintingHelper.CalcPrintColumnsBandsLayout(view, pageSize, bandsLayout);
      else if (view.PrintAutoWidth)
        GridPrintingHelper.CalcPrintColumnsAutoWidthLayout(view, pageSize);
      else
        GridPrintingHelper.CalcPrintColumnWidths(view);
    }

    private static void DoAfterGenerateNodeTreeAction(PrintingDataTreeBuilderBase treeBuilder)
    {
    }

    internal static void CreatePrintingTreeNodeAsync(TableView view, Size usablePageSize, MasterDetailPrintInfo masterDetailPrintInfo = null)
    {
      ItemsGenerationAsyncServerModeStrategyAsync itemsGenerationStrategy = new ItemsGenerationAsyncServerModeStrategyAsync((DataViewBase) view);
      itemsGenerationStrategy.StartFetchingAllFilteredAndSortedRows((Action) (() => view.RaiseCreateRootNodeCompleted(GridPrintingHelper.CreatePrintingTreeNode((ITableView) view, usablePageSize, masterDetailPrintInfo, (ItemsGenerationStrategyBase) itemsGenerationStrategy))));
    }
  }
}
