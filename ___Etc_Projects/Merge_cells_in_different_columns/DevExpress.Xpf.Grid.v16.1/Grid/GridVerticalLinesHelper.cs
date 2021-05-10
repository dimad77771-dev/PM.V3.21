// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.GridVerticalLinesHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class GridVerticalLinesHelper
  {
    public static readonly DependencyProperty SelectionStateProperty = DependencyPropertyManager.RegisterAttached("SelectionState", typeof (SelectionState), typeof (GridVerticalLinesHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) SelectionState.None, (PropertyChangedCallback) ((d, e) => GridVerticalLinesHelper.OnShowVerticalLinesChanged(d, e))));
    public static readonly DependencyProperty ShowVerticalLinesProperty = DependencyPropertyManager.RegisterAttached("ShowVerticalLines", typeof (bool), typeof (GridVerticalLinesHelper), (PropertyMetadata) new FrameworkPropertyMetadata((object) false, (PropertyChangedCallback) ((d, e) => GridVerticalLinesHelper.OnShowVerticalLinesChanged(d, e))));
    public static readonly DependencyProperty VerticalLinesBrushProperty = DependencyPropertyManager.RegisterAttached("VerticalLinesBrush", typeof (Brush), typeof (GridVerticalLinesHelper), (PropertyMetadata) new FrameworkPropertyMetadata());

    public static SelectionState GetSelectionState(DependencyObject obj)
    {
      return (SelectionState) obj.GetValue(GridVerticalLinesHelper.SelectionStateProperty);
    }

    public static void SetSelectionState(DependencyObject obj, SelectionState value)
    {
      obj.SetValue(GridVerticalLinesHelper.SelectionStateProperty, (object) value);
    }

    public static bool GetShowVerticalLines(DependencyObject obj)
    {
      return (bool) obj.GetValue(GridVerticalLinesHelper.ShowVerticalLinesProperty);
    }

    public static void SetShowVerticalLines(DependencyObject obj, bool value)
    {
      obj.SetValue(GridVerticalLinesHelper.ShowVerticalLinesProperty, (object) value);
    }

    public static Brush GetVerticalLinesBrush(DependencyObject obj)
    {
      return (Brush) obj.GetValue(GridVerticalLinesHelper.VerticalLinesBrushProperty);
    }

    public static void SetVerticalLinesBrush(DependencyObject obj, Brush value)
    {
      obj.SetValue(GridVerticalLinesHelper.VerticalLinesBrushProperty, (object) value);
    }

    private static void OnShowVerticalLinesChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
      Border border = (Border) d;
      bool flag1 = (bool) border.GetValue(GridVerticalLinesHelper.ShowVerticalLinesProperty);
      SelectionState selectionState = (SelectionState) border.GetValue(GridVerticalLinesHelper.SelectionStateProperty);
      Brush brush = (Brush) border.GetValue(GridVerticalLinesHelper.VerticalLinesBrushProperty);
      bool flag2 = flag1 && selectionState != SelectionState.Focused;
      border.BorderBrush = flag2 ? brush : (Brush) null;
    }
  }
}
