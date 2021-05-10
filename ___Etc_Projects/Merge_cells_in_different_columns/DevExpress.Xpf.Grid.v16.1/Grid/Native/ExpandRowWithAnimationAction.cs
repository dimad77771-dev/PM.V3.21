// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Native.ExpandRowWithAnimationAction
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Grid.Hierarchy;
using System;
using System.Threading;
using System.Windows;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace DevExpress.Xpf.Grid.Native
{
  public class ExpandRowWithAnimationAction : IContinousAction, IAction
  {
    private const double AnimationEndLagTime = 50.0;
    private GridDataPresenterBase dataPresenter;
    private GroupNode groupNode;
    private ExpandRowWithAnimationAction.ExpandState expandState;
    private bool isDone;
    private bool inProgress;
    private Storyboard storyboard;
    private bool recursive;
    private bool postponeForceComplete;

    protected FrameworkElement GroupRowElement { get; private set; }

    private SizeHelperBase SizeHelper
    {
      get
      {
        return SizeHelperBase.GetDefineSizeHelper(this.View.OrientationCore);
      }
    }

    private GridViewBase View
    {
      get
      {
        return this.groupNode.GridView;
      }
    }

    private GridControl Grid
    {
      get
      {
        return this.groupNode.Grid;
      }
    }

    private GroupRowData groupRowData
    {
      get
      {
        return this.GroupRowElement.DataContext as GroupRowData;
      }
    }

    private RowsContainer LogicalItemsContainer
    {
      get
      {
        return this.groupRowData.RowsContainer;
      }
    }

    private Size RenderSize
    {
      get
      {
        return ((IItemsContainer) this.LogicalItemsContainer).RenderSize;
      }
    }

    private DependencyObject AnimationTarget
    {
      get
      {
        return (DependencyObject) this.groupRowData.RowsContainer;
      }
    }

    bool IContinousAction.IsDone
    {
      get
      {
        return this.isDone;
      }
    }

    public ExpandRowWithAnimationAction(DataPresenterBase dataPresenter, GroupNode groupNode, bool recursive)
    {
      this.dataPresenter = (GridDataPresenterBase) dataPresenter;
      this.groupNode = groupNode;
      this.recursive = recursive;
      this.GroupRowElement = this.View.GetRowElementByRowHandle(groupNode.RowHandle.Value);
    }

    private void OnCollapsed()
    {
      this.groupNode.IsCollapsing = false;
      this.ChangeGroupExpanded();
      this.isDone = true;
      this.SetVisibleSize(ExpandHelper.DefaultVisibleSize);
      this.Grid.OnGroupRowCollapsed(this.groupNode.RowHandle.Value);
      this.View.UpdateRowData((UpdateRowDataDelegate) (rowData => rowData.EnsureRowLoaded()), true, true);
    }

    private void OnExpanded()
    {
      this.groupNode.CanGenerateItems = true;
      this.groupNode.IsExpanding = false;
      this.groupNode.ResumeUpdateState();
      this.groupNode.IsExpanded = true;
      this.View.VisualDataTreeBuilder.SynchronizeMasterNode();
      this.isDone = true;
      this.SetVisibleSize(ExpandHelper.DefaultVisibleSize);
      this.View.EnqueueImmediateAction(new Action(((DataPresenterBase) this.dataPresenter).ClearInvisibleItems));
      this.Grid.RaiseGroupRowExpanded(this.groupNode.RowHandle.Value);
    }

    private void ExecuteCore()
    {
      double defineSize = Math.Max(0.0, Math.Min(this.SizeHelper.GetDefineSize(this.dataPresenter.LastConstraint) - this.SizeHelper.GetDefinePoint(LayoutHelper.GetRelativeElementRect((UIElement) this.GroupRowElement, (UIElement) this.dataPresenter).BottomRight()), this.GetItemsContainerDefineSize()));
      this.SetVisibleSize(this.SizeHelper.CreateSize(defineSize, this.SizeHelper.GetSecondarySize(ExpandHelper.DefaultVisibleSize)));
      double speedRatio = defineSize == 0.0 ? 10000.0 : ExpandHelper.GetExpandSpeed((DependencyObject) this.View) / defineSize;
      if (this.expandState == ExpandRowWithAnimationAction.ExpandState.Expanding)
        this.BeginAnimation(this.GetStoryboard(ExpandHelper.ExpandStoryboardProperty), speedRatio);
      else
        this.BeginAnimation(this.GetStoryboard(ExpandHelper.CollapseStoryboardProperty), speedRatio);
      if (!this.postponeForceComplete)
        return;
      this.ForceComplete(false);
    }

    protected virtual void SetVisibleSize(Size value)
    {
      ExpandHelper.SetVisibleSize((DependencyObject) this.LogicalItemsContainer, value);
    }

    protected virtual void SetAnimationProgress(double animationProgress)
    {
      this.LogicalItemsContainer.AnimationProgress = animationProgress;
    }

    private Storyboard GetStoryboard(DependencyProperty property)
    {
      return (Storyboard) this.View.GetValue(property);
    }

    private double GetItemsContainerDefineSize()
    {
      return this.SizeHelper.GetDefineSize(this.RenderSize);
    }

    private void BeginAnimation(Storyboard initialStoryboard, double speedRatio)
    {
      if (initialStoryboard == null)
        return;
      this.storyboard = initialStoryboard.Clone();
      Storyboard.SetTarget((DependencyObject) this.storyboard, this.AnimationTarget);
      this.storyboard.SpeedRatio = speedRatio;
      this.storyboard.Completed += new EventHandler(this.OnStoryboardCompleted);
      this.storyboard.Begin();
    }

    private void OnStoryboardCompleted(object sender, EventArgs e)
    {
      this.ForceComplete(true);
    }

    private void ForceComplete(bool delayedCollapse)
    {
      this.storyboard.Completed -= new EventHandler(this.OnStoryboardCompleted);
      if (this.isDone)
        return;
      if (this.expandState == ExpandRowWithAnimationAction.ExpandState.Expanding)
      {
        this.SetAnimationProgress(1.0);
        this.storyboard.Stop();
        this.OnExpanded();
      }
      else if (delayedCollapse)
      {
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        dispatcherTimer.Tick += new EventHandler(this.timer_Tick);
        dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50.0);
        dispatcherTimer.Start();
      }
      else
        this.ForceCompleteCollapseAnimation();
    }

    private void timer_Tick(object sender, EventArgs e)
    {
      ((DispatcherTimer) sender).Tick -= new EventHandler(this.timer_Tick);
      if (this.isDone)
        return;
      this.ForceCompleteCollapseAnimation();
    }

    private void ForceCompleteCollapseAnimation()
    {
      this.storyboard.Stop();
      this.OnCollapsed();
    }

    void IContinousAction.ForceComplete()
    {
      if (this.storyboard == null)
        this.postponeForceComplete = true;
      else
        this.ForceComplete(false);
    }

    void IContinousAction.Prepare()
    {
      if (this.groupNode.IsExpanded)
      {
        if (!this.Grid.RaiseGroupRowCollapsing(this.groupNode.RowHandle.Value))
        {
          this.isDone = true;
        }
        else
        {
          this.groupNode.IsCollapsing = true;
          this.expandState = ExpandRowWithAnimationAction.ExpandState.Collapsing;
          this.groupNode.CanGenerateItems = false;
          this.dataPresenter.CollapseBufferSize = this.GetItemsContainerDefineSize();
        }
      }
      else if (!this.Grid.RaiseGroupRowExpanding(this.groupNode.RowHandle.Value))
      {
        this.isDone = true;
      }
      else
      {
        this.groupNode.IsExpanding = true;
        this.View.VisualDataTreeBuilder.SynchronizeMasterNode();
        this.expandState = ExpandRowWithAnimationAction.ExpandState.Expanding;
        this.groupNode.SupressUpdateState();
        this.ChangeGroupExpanded();
        this.groupNode.CanGenerateItems = true;
      }
    }

    private void ChangeGroupExpanded()
    {
      this.Grid.ChangeGroupExpandedCore(this.groupNode.RowHandle.Value, this.recursive);
    }

    void IAction.Execute()
    {
      if (this.inProgress)
        return;
      this.inProgress = true;
      this.dataPresenter.CollapseBufferSize = 0.0;
      this.View.Dispatcher.BeginInvoke(DispatcherPriority.Render, (Delegate) new ThreadStart(this.ExecuteCore));
    }

    private enum ExpandState
    {
      Expanding,
      Collapsing,
    }
  }
}
