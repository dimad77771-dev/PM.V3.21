// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ScrollBarAnnotationsAppearanceDefault
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Editors.Helpers;
using DevExpress.Xpf.Grid.Themes;
using System;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class ScrollBarAnnotationsAppearanceDefault : ScrollBarAnnotationsAppearance
  {
    public ScrollBarAnnotationsAppearanceDefault(DataViewBase view)
    {
      ScrollBarAnnotationInfo barAnnotationInfo1 = new ScrollBarAnnotationInfo();
      barAnnotationInfo1.Alignment = ScrollBarAnnotationAlignment.Full;
      barAnnotationInfo1.MinHeight = 2.0;
      ScrollBarAnnotationInfo barAnnotationInfo2 = barAnnotationInfo1;
      DataViewBase dataViewBase1 = view;
      TableViewThemeKeyExtension themeKeyExtension1 = new TableViewThemeKeyExtension();
      themeKeyExtension1.ResourceKey = TableViewThemeKeys.AnnotationFocusedRowBrush;
      themeKeyExtension1.ThemeName = ThemeHelper.GetEditorThemeName((DependencyObject) view);
      TableViewThemeKeyExtension themeKeyExtension2 = themeKeyExtension1;
      Brush brush1 = (Brush) dataViewBase1.FindResource((object) themeKeyExtension2);
      barAnnotationInfo2.Brush = brush1;
      barAnnotationInfo1.Mode = new ScrollBarAnnotationMode?(ScrollBarAnnotationMode.FocusedRow);
      this.FocusedRow = barAnnotationInfo1;
      ScrollBarAnnotationInfo barAnnotationInfo3 = new ScrollBarAnnotationInfo();
      barAnnotationInfo3.Alignment = ScrollBarAnnotationAlignment.Right;
      barAnnotationInfo3.MinHeight = 3.0;
      barAnnotationInfo3.Width = 4.0;
      ScrollBarAnnotationInfo barAnnotationInfo4 = barAnnotationInfo3;
      DataViewBase dataViewBase2 = view;
      TableViewThemeKeyExtension themeKeyExtension3 = new TableViewThemeKeyExtension();
      themeKeyExtension3.ResourceKey = TableViewThemeKeys.AnnotationErrorBrush;
      themeKeyExtension3.ThemeName = ThemeHelper.GetEditorThemeName((DependencyObject) view);
      TableViewThemeKeyExtension themeKeyExtension4 = themeKeyExtension3;
      Brush brush2 = (Brush) dataViewBase2.FindResource((object) themeKeyExtension4);
      barAnnotationInfo4.Brush = brush2;
      barAnnotationInfo3.Mode = new ScrollBarAnnotationMode?(ScrollBarAnnotationMode.InvalidRows);
      this.InvalidRows = barAnnotationInfo3;
      ScrollBarAnnotationInfo barAnnotationInfo5 = new ScrollBarAnnotationInfo();
      barAnnotationInfo5.Alignment = ScrollBarAnnotationAlignment.Right;
      barAnnotationInfo5.MinHeight = 3.0;
      barAnnotationInfo5.Width = 4.0;
      ScrollBarAnnotationInfo barAnnotationInfo6 = barAnnotationInfo5;
      DataViewBase dataViewBase3 = view;
      TableViewThemeKeyExtension themeKeyExtension5 = new TableViewThemeKeyExtension();
      themeKeyExtension5.ResourceKey = TableViewThemeKeys.AnnotationErrorBrush;
      themeKeyExtension5.ThemeName = ThemeHelper.GetEditorThemeName((DependencyObject) view);
      TableViewThemeKeyExtension themeKeyExtension6 = themeKeyExtension5;
      Brush brush3 = (Brush) dataViewBase3.FindResource((object) themeKeyExtension6);
      barAnnotationInfo6.Brush = brush3;
      barAnnotationInfo5.Mode = new ScrollBarAnnotationMode?(ScrollBarAnnotationMode.InvalidCells);
      this.InvalidCells = barAnnotationInfo5;
      ScrollBarAnnotationInfo barAnnotationInfo7 = new ScrollBarAnnotationInfo();
      barAnnotationInfo7.Alignment = ScrollBarAnnotationAlignment.Left;
      barAnnotationInfo7.MinHeight = 2.0;
      barAnnotationInfo7.Width = 4.0;
      ScrollBarAnnotationInfo barAnnotationInfo8 = barAnnotationInfo7;
      DataViewBase dataViewBase4 = view;
      TableViewThemeKeyExtension themeKeyExtension7 = new TableViewThemeKeyExtension();
      themeKeyExtension7.ResourceKey = TableViewThemeKeys.AnnotationSelectionBrush;
      themeKeyExtension7.ThemeName = ThemeHelper.GetEditorThemeName((DependencyObject) view);
      TableViewThemeKeyExtension themeKeyExtension8 = themeKeyExtension7;
      Brush brush4 = (Brush) dataViewBase4.FindResource((object) themeKeyExtension8);
      barAnnotationInfo8.Brush = brush4;
      barAnnotationInfo7.Mode = new ScrollBarAnnotationMode?(ScrollBarAnnotationMode.Selected);
      this.Selected = barAnnotationInfo7;
      ScrollBarAnnotationInfo barAnnotationInfo9 = new ScrollBarAnnotationInfo();
      barAnnotationInfo9.Alignment = ScrollBarAnnotationAlignment.Center;
      barAnnotationInfo9.MinHeight = 3.0;
      barAnnotationInfo9.Width = 6.0;
      ScrollBarAnnotationInfo barAnnotationInfo10 = barAnnotationInfo9;
      DataViewBase dataViewBase5 = view;
      TableViewThemeKeyExtension themeKeyExtension9 = new TableViewThemeKeyExtension();
      themeKeyExtension9.ResourceKey = TableViewThemeKeys.AnnotationSearchBrush;
      themeKeyExtension9.ThemeName = ThemeHelper.GetEditorThemeName((DependencyObject) view);
      TableViewThemeKeyExtension themeKeyExtension10 = themeKeyExtension9;
      Brush brush5 = (Brush) dataViewBase5.FindResource((object) themeKeyExtension10);
      barAnnotationInfo10.Brush = brush5;
      barAnnotationInfo9.Mode = new ScrollBarAnnotationMode?(ScrollBarAnnotationMode.SearchResult);
      this.SearchResult = barAnnotationInfo9;
      this.Owner = new WeakReference((object) view);
    }
  }
}
