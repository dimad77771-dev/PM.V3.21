// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridTotalSummaryMenuInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Data.Summary;
using DevExpress.Mvvm.Native;
using DevExpress.Xpf.Bars;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridTotalSummaryMenuInfo : GridMenuInfo
  {
    protected readonly GridTotalSummaryHelper summaryHelper;
    private int synchronizingSummaries;

    protected GridSummaryItemsEditorController Controller
    {
      get
      {
        return this.summaryHelper.Controller;
      }
    }

    protected virtual ISummaryItemOwner SummaryItemsCore
    {
      get
      {
        return this.DataControl.TotalSummaryCore;
      }
    }

    internal GridTotalSummaryHelper SummaryHelper
    {
      get
      {
        return this.summaryHelper;
      }
    }

    public override GridMenuType MenuType
    {
      get
      {
        return GridMenuType.TotalSummary;
      }
    }

    public override bool CanCreateItems
    {
      get
      {
        if (this.View.IsTotalSummaryMenuEnabled)
          return !this.IsCheckBoxSelectorColumnMenu();
        return false;
      }
    }

    public override BarManagerMenuController MenuController
    {
      get
      {
        return this.View.TotalSummaryMenuController;
      }
    }

    public GridTotalSummaryMenuInfo(DataControlPopupMenu menu)
      : base(menu)
    {
      this.summaryHelper = this.CreateSummaryHelper();
    }

    protected virtual GridTotalSummaryHelper CreateSummaryHelper()
    {
      return new GridTotalSummaryHelper(this.View, (Func<ColumnBase>) (() => this.Column));
    }

    public override bool Initialize(IInputElement value)
    {
      this.SubscribeEvents();
      this.InitializeCore(value);
      return base.Initialize(value);
    }

    protected virtual void SubscribeEvents()
    {
      this.SummaryItemsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSummaryItemsCollectionChanged);
    }

    protected virtual void UnsubcribeEvents()
    {
      this.SummaryItemsCore.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnSummaryItemsCollectionChanged);
    }

    public override void Uninitialize()
    {
      base.Uninitialize();
      this.UnsubcribeEvents();
    }

    protected virtual void OnSummaryItemsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      if (this.synchronizingSummaries > 0)
        return;
      ++this.synchronizingSummaries;
      try
      {
        ISummaryItemOwner summaryItemOwner = (ISummaryItemOwner) sender;
        foreach (SummaryItemBase summaryItemBase in (IEnumerable<SummaryItemBase>) summaryItemOwner)
        {
          if (!this.Controller.Items.Contains((ISummaryItem) summaryItemBase) && summaryItemBase.Visible && this.SummaryHelper.CanUseSummaryItem((ISummaryItem) summaryItemBase))
            this.Controller.Items.Add((ISummaryItem) summaryItemBase);
        }
        foreach (ISummaryItem summaryItem in new List<ISummaryItem>((IEnumerable<ISummaryItem>) this.Controller.Items))
        {
          if (!summaryItemOwner.Contains((object) summaryItem))
            this.Controller.Items.Remove(summaryItem);
        }
      }
      finally
      {
        --this.synchronizingSummaries;
      }
    }

    protected virtual void InitializeCore(IInputElement value)
    {
      FrameworkElement frameworkElement = value as FrameworkElement;
      if (frameworkElement == null)
        return;
      GridColumnData gridColumnData = frameworkElement.DataContext as GridColumnData;
      if (gridColumnData == null)
        return;
      this.BaseColumn = (BaseColumn) gridColumnData.Column;
    }

    protected override void CreateItems()
    {
      this.CreateBarCheckItem("ItemSum", GridControlStringId.MenuFooterSum, new bool?(this.Controller.HasSummary(this.Column.FieldName, SummaryItemType.Sum)), false, (ImageSource) ImageHelper.GetImage("ItemSum"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(this.Column.FieldName, SummaryItemType.Sum)), (Func<bool>) (() => this.Controller.CanApplySummary(SummaryItemType.Sum, this.Column.FieldName))), false);
      this.CreateBarCheckItem("ItemMin", GridControlStringId.MenuFooterMin, new bool?(this.Controller.HasSummary(this.Column.FieldName, SummaryItemType.Min)), false, (ImageSource) ImageHelper.GetImage("ItemMin"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(this.Column.FieldName, SummaryItemType.Min)), (Func<bool>) (() => this.Controller.CanApplySummary(SummaryItemType.Min, this.Column.FieldName))), false);
      this.CreateBarCheckItem("ItemMax", GridControlStringId.MenuFooterMax, new bool?(this.Controller.HasSummary(this.Column.FieldName, SummaryItemType.Max)), false, (ImageSource) ImageHelper.GetImage("ItemMax"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(this.Column.FieldName, SummaryItemType.Max)), (Func<bool>) (() => this.Controller.CanApplySummary(SummaryItemType.Max, this.Column.FieldName))), false);
      this.CreateBarCheckItem("ItemCount", GridControlStringId.MenuFooterCount, new bool?(this.Controller.HasSummary(this.Column.FieldName, SummaryItemType.Count)), false, (ImageSource) ImageHelper.GetImage("ItemCount"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(this.Column.FieldName, SummaryItemType.Count)), (Func<bool>) (() => this.Controller.CanApplySummary(SummaryItemType.Count, this.Column.FieldName))), false);
      this.CreateBarCheckItem("ItemAverage", GridControlStringId.MenuFooterAverage, new bool?(this.Controller.HasSummary(this.Column.FieldName, SummaryItemType.Average)), false, (ImageSource) ImageHelper.GetImage("ItemAverage"), (ICommand) DelegateCommandFactory.Create((Action) (() => this.SetSummary(this.Column.FieldName, SummaryItemType.Average)), (Func<bool>) (() => this.Controller.CanApplySummary(SummaryItemType.Average, this.Column.FieldName))), false);
      this.CreateBarButtonItem("ItemCustomize", GridControlStringId.MenuFooterCustomize, true, (ImageSource) null, (ICommand) DelegateCommandFactory.Create((Action) (() => this.summaryHelper.ShowSummaryEditor()), (Func<bool>) (() => this.Controller.Count > 0), false), (object) null);
    }

    protected virtual BarCheckItem CreateBarCheckItem(string name, string content, bool? isChecked, bool beginGroup, ImageSource image, ICommand command, bool closeMenuOnClick)
    {
      BarCheckItem barCheckItem = this.CreateBarCheckItem(name, content, isChecked, beginGroup, image, command);
      barCheckItem.CloseSubMenuOnClick = new bool?(closeMenuOnClick);
      return barCheckItem;
    }

    protected virtual BarCheckItem CreateBarCheckItem(string name, GridControlStringId id, bool? isChecked, bool beginGroup, ImageSource image, ICommand command, bool closeMenuOnClick)
    {
      BarCheckItem barCheckItem = this.CreateBarCheckItem(name, id, isChecked, beginGroup, image, command);
      barCheckItem.CloseSubMenuOnClick = new bool?(closeMenuOnClick);
      return barCheckItem;
    }

    internal bool IsCheckBoxSelectorColumnMenu()
    {
      if (this.Column != null)
        return TableView.IsCheckBoxSelectorColumn(this.Column.FieldName);
      return false;
    }

    protected virtual void SetSummary(string fieldName, SummaryItemType type)
    {
      ++this.synchronizingSummaries;
      try
      {
        this.Controller.SetSummary(fieldName, type, !this.Controller.HasSummary(fieldName, type));
        this.Controller.Apply();
      }
      finally
      {
        --this.synchronizingSummaries;
      }
    }

    protected override void ExecuteMenuController()
    {
      base.ExecuteMenuController();
      this.Menu.ExecuteOriginationViewMenuController((Func<DataViewBase, BarManagerMenuController>) (view => view.TotalSummaryMenuController));
    }
  }
}
