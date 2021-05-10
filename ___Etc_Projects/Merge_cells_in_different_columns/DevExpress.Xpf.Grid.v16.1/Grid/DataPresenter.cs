// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DataPresenter
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid.Hierarchy;
using DevExpress.Xpf.Utils;
using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace DevExpress.Xpf.Grid
{
  public class DataPresenter : DataPresenterManipulation
  {
    public static readonly DependencyProperty AnimationOffsetProperty = DependencyPropertyManager.Register("AnimationOffset", typeof (double), typeof (DataPresenter), (PropertyMetadata) new UIPropertyMetadata((object) 0.0, (PropertyChangedCallback) ((d, e) => ((DataPresenter) d).OnAnimationOffsetChanged((double) e.OldValue))));
    private Storyboard scrollAnimationStoryboard;
    private double oldOffset;

    public double AnimationOffset
    {
      get
      {
        return (double) this.GetValue(DataPresenter.AnimationOffsetProperty);
      }
      set
      {
        this.SetValue(DataPresenter.AnimationOffsetProperty, (object) value);
      }
    }

    public override double ScrollItemOffset
    {
      get
      {
        return this.ActualAnimationOffset % 1.0;
      }
    }

    protected internal override bool CanScrollWithAnimation
    {
      get
      {
        if (this.View == null || !this.View.ViewBehavior.AllowPerPixelScrolling || (this.AdjustmentInProgress || this.View.ScrollAnimationLocker.IsLocked) || !this.View.ViewBehavior.AllowScrollAnimation)
          return false;
        if (this.View.ViewBehavior.ScrollAnimationDuration == 0.0)
          return this.View.ViewBehavior.ScrollAnimationMode == ScrollAnimationMode.Custom;
        return true;
      }
    }

    protected internal override bool IsAnimationInProgress
    {
      get
      {
        if (!this.CanScrollWithAnimation)
          return false;
        return this.AnimationOffset != this.ActualScrollOffset;
      }
    }

    private double ActualAnimationOffset
    {
      get
      {
        if (this.scrollAnimationStoryboard == null)
          return (double) this.GetAnimationBaseValue(DataPresenter.AnimationOffsetProperty);
        return this.AnimationOffset;
      }
    }

    protected internal override int GenerateItemsOffset
    {
      get
      {
        return (int) this.ActualAnimationOffset;
      }
    }

    protected internal override double CurrentOffset
    {
      get
      {
        return this.ActualAnimationOffset;
      }
    }

    private ITableView TableView
    {
      get
      {
        return this.View as ITableView;
      }
    }

    private void OnAnimationOffsetChanged(double oldValue)
    {
      this.InvalidatePanel(Math.Floor(oldValue) == Math.Floor(this.AnimationOffset));
    }

    private void InvalidatePanel(bool updateCellMergingPanels)
    {
      if (this.View == null)
        return;
      this.InvalidateMeasure();
      HierarchyPanel hierarchyPanel = this.Content as HierarchyPanel;
      if (hierarchyPanel != null)
        hierarchyPanel.InvalidateMeasure();
      if (!updateCellMergingPanels)
        return;
      this.View.UpdateCellMergingPanels();
      this.View.RowsStateDirty = true;
    }

    protected override void OnDefineScrollInfoChangedCore()
    {
      if (this.View != null && this.oldOffset != this.ScrollInfoCore.DefineSizeScrollInfo.Offset)
      {
        this.oldOffset = this.ScrollInfoCore.DefineSizeScrollInfo.Offset;
        if (this.scrollAnimationStoryboard != null)
        {
          this.AnimationOffset = this.AnimationOffset;
          this.scrollAnimationStoryboard.Remove();
          this.scrollAnimationStoryboard = (Storyboard) null;
        }
        if (this.CanScrollWithAnimation)
        {
          this.scrollAnimationStoryboard = this.GetStoryboard();
          Storyboard.SetTargetProperty((DependencyObject) this.scrollAnimationStoryboard, new PropertyPath(DataPresenter.AnimationOffsetProperty.GetName(), new object[0]));
          Storyboard.SetTarget((DependencyObject) this.scrollAnimationStoryboard, (DependencyObject) this);
          this.scrollAnimationStoryboard.Begin();
        }
        else
        {
          this.AnimationOffset = this.ScrollInfoCore.DefineSizeScrollInfo.Offset;
          this.InvalidatePanel(false);
        }
      }
      base.OnDefineScrollInfoChangedCore();
    }

    private Storyboard GetStoryboard()
    {
      if (this.View.ViewBehavior.ScrollAnimationMode == ScrollAnimationMode.Custom)
      {
        CustomScrollAnimationEventArgs e = new CustomScrollAnimationEventArgs(this.AnimationOffset, this.oldOffset);
        this.View.RaiseCustomScrollAnimation(e);
        return e.Storyboard;
      }
      Storyboard storyboard = new Storyboard();
      DoubleAnimation doubleAnimation = new DoubleAnimation();
      doubleAnimation.From = new double?(this.AnimationOffset);
      doubleAnimation.To = new double?(this.oldOffset);
      doubleAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(this.View.ViewBehavior.ScrollAnimationDuration));
      doubleAnimation.EasingFunction = this.GetEasingFunction();
      storyboard.Children.Add((Timeline) doubleAnimation);
      return storyboard;
    }

    private IEasingFunction GetEasingFunction()
    {
      switch (this.View.ViewBehavior.ScrollAnimationMode)
      {
        case ScrollAnimationMode.EaseOut:
          return (IEasingFunction) new ExponentialEase() { Exponent = 3.0 };
        case ScrollAnimationMode.Linear:
          return (IEasingFunction) null;
        case ScrollAnimationMode.EaseInOut:
          ExponentialEase exponentialEase = new ExponentialEase();
          exponentialEase.EasingMode = EasingMode.EaseInOut;
          exponentialEase.Exponent = 3.0;
          return (IEasingFunction) exponentialEase;
        default:
          throw new NotImplementedException();
      }
    }

    protected override double GetOffset()
    {
      FrameworkElement firstVisibleRow = this.GetFirstVisibleRow();
      if (firstVisibleRow == null)
        return 0.0;
      return HierarchyPanel.GetScrollElementOffset((UIElement) firstVisibleRow, this.ScrollItemOffset);
    }

    protected override FrameworkElement CreateContent()
    {
      HierarchyPanel hierarchyPanel = new HierarchyPanel();
      hierarchyPanel.VerticalAlignment = VerticalAlignment.Top;
      hierarchyPanel.DataPresenter = (DataPresenterBase) this;
      return (FrameworkElement) hierarchyPanel;
    }

    protected override void UpdateViewCore()
    {
      HierarchyPanel hierarchyPanel = this.Content as HierarchyPanel;
      if (hierarchyPanel == null)
        return;
      hierarchyPanel.ItemsContainer = (IRootItemsContainer) this.View.MasterRootRowsContainer;
    }

    private void InvalidateElements()
    {
      FrameworkElement frameworkElement = (FrameworkElement) (this.Content as HierarchyPanel);
      do
      {
        frameworkElement = (FrameworkElement) VisualTreeHelper.GetParent((DependencyObject) frameworkElement);
        frameworkElement.InvalidateMeasure();
      }
      while (frameworkElement != this);
    }

    protected override void PregenerateItems(Size constraint)
    {
      if (LayoutUpdatedHelper.GlobalLocker.IsLocked || this.rendered || (this.View.RootNodeContainer.ItemCount != 0 || this.View.DataControl == null) || this.View.IsDesignTime && double.IsInfinity(constraint.Height))
        return;
      this.View.DataControl.ForceLoad();
      this.View.ViewBehavior.EnsureSurroundingsActualSize(this.LastConstraint);
      this.OnDefineScrollInfoChangedCore();
      this.View.RootNodeContainer.OnDataChangedCore();
      while (this.View.RootNodeContainer.StartScrollIndex + this.View.RootNodeContainer.ItemCount < this.DataControl.VisibleRowCount)
      {
        this.GenerateItems(DataPresenterBase.GenerateItemsCount, constraint.Height);
        this.BaseMeasureOverride(constraint);
        this.InvalidateElements();
        if (this.GetChildHeight() >= constraint.Height || !double.IsPositiveInfinity(constraint.Height) && this.View.RootNodeContainer.ItemCount > 70)
          break;
      }
      this.UpdateScrollInfo();
    }

    protected override void GenerateItems(int count, double availableHeight)
    {
      if (!this.IsInAction)
      {
        UIElement uiElement = (UIElement) this.GetFirstVisibleRow();
        double num = uiElement != null ? uiElement.DesiredSize.Height : this.TableView.RowMinHeight;
        if (num != 0.0 && !double.IsPositiveInfinity(availableHeight))
          count = Math.Max(Math.Min(count, (int) Math.Ceiling((availableHeight - this.GetChildHeight()) / num) + 1), 1);
      }
      base.GenerateItems(count, availableHeight);
    }

    protected override void CancelAllGetRows()
    {
      base.CancelAllGetRows();
      if (this.DataControl == null || this.DataControl.DataProviderBase == null)
        return;
      this.DataControl.DataProviderBase.CancelAllGetRows();
    }

    protected override void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount)
    {
      base.EnsureAllRowsLoaded(firstRowIndex, rowsCount);
      this.DataControl.DataProviderBase.EnsureAllRowsLoaded(firstRowIndex, rowsCount);
    }
  }
}
