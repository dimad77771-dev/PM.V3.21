// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.AppearanceHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Export.Xl;
using DevExpress.Xpf.Core.ConditionalFormatting.Native;
using DevExpress.Xpf.Grid.Native;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid.Printing
{
  public static class AppearanceHelper
  {
    public static System.Drawing.Color BrushToColor(System.Windows.Media.Brush br)
    {
      if (br == null)
        return System.Drawing.Color.Empty;
      System.Windows.Media.Color color = (System.Windows.Media.Color) br.GetValue(SolidColorBrush.ColorProperty);
      byte a = color.A;
      byte g = color.G;
      byte r = color.R;
      byte b = color.B;
      return System.Drawing.Color.FromArgb((int) a, (int) r, (int) g, (int) b);
    }

    private static IFormatInfoProvider CreateFormatInfoProvider(DataControlBase dataControl, int rowHandle)
    {
      return (IFormatInfoProvider) new DataTreeBuilderFormatInfoProvider<DataControlBase>(dataControl, (Func<DataControlBase, DataTreeBuilder>) (x => (DataTreeBuilder) x.DataView.VisualDataTreeBuilder), (Func<DataControlBase, string, object>) ((x, fieldName) => x.GetCellValue(rowHandle, fieldName)));
    }

    private static FormatValueProvider CalcCondition(DataControlBase dataControl, int rowHandle, string fieldName)
    {
      return AppearanceHelper.CreateFormatInfoProvider(dataControl, rowHandle).GetValueProvider(fieldName);
    }

    private static AppearanceHelper.ConditionalFormattingProperties GetValues(IList<FormatConditionBaseInfo> conditions, DataViewBase view, int rowHandle, string fieldName, AppearanceHelper.ConditionalFormattingProperties oldProp)
    {
      if (conditions == null || conditions.Count <= 0)
        return oldProp;
      AppearanceHelper.ConditionalFormattingProperties formattingProperties = oldProp ?? new AppearanceHelper.ConditionalFormattingProperties();
      foreach (FormatConditionBaseInfo condition in (IEnumerable<FormatConditionBaseInfo>) conditions)
      {
        FormatValueProvider provider = AppearanceHelper.CalcCondition(view.DataControl, rowHandle, condition.ActualFieldName);
        double num = condition.CoerceFontSize(11.0, provider);
        FontWeight fontWeight = condition.CoerceFontWeight(FontWeights.Normal, provider);
        System.Windows.FontStyle fontStyle = condition.CoerceFontStyle(FontStyles.Normal, provider);
        formattingProperties.FontSize = num != 11.0 ? num : formattingProperties.FontSize;
        formattingProperties.FontWeight = fontWeight != FontWeights.Normal ? fontWeight : formattingProperties.FontWeight;
        formattingProperties.FontStyle = fontStyle != FontStyles.Normal ? fontStyle : formattingProperties.FontStyle;
        formattingProperties.Background = condition.CoerceBackground((System.Windows.Media.Brush) null, provider) ?? formattingProperties.Background;
        formattingProperties.Foreground = condition.CoerceForeground((System.Windows.Media.Brush) null, provider) ?? formattingProperties.Foreground;
        formattingProperties.TextDecor = condition.CoerceTextDecorations((TextDecorationCollection) null, provider) ?? formattingProperties.TextDecor;
      }
      return formattingProperties;
    }

    public static XlCellFormatting GetCellAppearance(int rowHandle, ColumnBase col, DataViewBase view, FormatConditionCollection formatConditions)
    {
      AppearanceHelper.ConditionalFormattingProperties oldProp = (AppearanceHelper.ConditionalFormattingProperties) null;
      if (view.ViewBehavior != null && view.ViewBehavior.IsAlternateRow(rowHandle))
      {
        oldProp = new AppearanceHelper.ConditionalFormattingProperties();
        oldProp.Background = view.ViewBehavior.ActualAlternateRowBackground;
      }
      AppearanceHelper.ConditionalFormattingProperties values1 = AppearanceHelper.GetValues(formatConditions.GetInfoByFieldName(string.Empty), view, rowHandle, col.FieldName, oldProp);
      AppearanceHelper.ConditionalFormattingProperties values2 = AppearanceHelper.GetValues(formatConditions.GetInfoByFieldName(col.FieldName), view, rowHandle, col.FieldName, values1);
      if (values2 != null)
        return AppearanceHelper.GetCellFormatting(values2);
      XlCellFormatting xlCellFormatting = new XlCellFormatting();
      xlCellFormatting.Font = new XlFont();
      xlCellFormatting.Fill = (XlFill) null;
      xlCellFormatting.Alignment = new XlCellAlignment();
      return xlCellFormatting;
    }

    private static XlFill SetFill(System.Windows.Media.Brush background)
    {
      XlFill xlFill = new XlFill();
      if (background == null)
        return (XlFill) null;
      XlColor xlColor = (XlColor) AppearanceHelper.BrushToColor(background);
      xlFill.BackColor = xlColor;
      xlFill.ForeColor = xlColor;
      xlFill.PatternType = XlPatternType.Solid;
      return xlFill;
    }

    private static XlCellFormatting GetCellFormatting(AppearanceHelper.ConditionalFormattingProperties properties)
    {
      XlCellFormatting xlCellFormatting1 = new XlCellFormatting();
      XlCellFormatting xlCellFormatting2 = xlCellFormatting1;
      XlFont xlFont1 = new XlFont();
      xlFont1.Bold = properties.FontWeight == FontWeights.Bold;
      xlFont1.Size = properties.FontSize;
      xlFont1.Italic = properties.FontStyle == FontStyles.Italic;
      XlFont xlFont2 = xlFont1;
      xlCellFormatting2.Font = xlFont2;
      if (properties.Foreground != null)
        xlCellFormatting1.Font.Color = (XlColor) AppearanceHelper.BrushToColor(properties.Foreground);
      if (properties.TextDecor != null)
      {
        foreach (TextDecoration textDecoration in properties.TextDecor)
        {
          switch (textDecoration.Location)
          {
            case TextDecorationLocation.Underline:
              xlCellFormatting1.Font.Underline = XlUnderlineType.Single;
              continue;
            case TextDecorationLocation.Strikethrough:
              xlCellFormatting1.Font.StrikeThrough = true;
              continue;
            default:
              continue;
          }
        }
      }
      xlCellFormatting1.Fill = AppearanceHelper.SetFill(properties.Background);
      xlCellFormatting1.Alignment = new XlCellAlignment();
      return xlCellFormatting1;
    }

    private class ConditionalFormattingProperties
    {
      public const int DefaultFontSize = 11;

      public double FontSize { get; set; }

      public FontWeight FontWeight { get; set; }

      public System.Windows.FontStyle FontStyle { get; set; }

      public System.Windows.Media.Brush Background { get; set; }

      public System.Windows.Media.Brush Foreground { get; set; }

      public TextDecorationCollection TextDecor { get; set; }

      public ConditionalFormattingProperties()
      {
        this.FontSize = 11.0;
        this.FontWeight = FontWeights.Normal;
        this.FontStyle = FontStyles.Normal;
        this.Background = (System.Windows.Media.Brush) null;
        this.Foreground = (System.Windows.Media.Brush) null;
        this.TextDecor = (TextDecorationCollection) null;
      }
    }
  }
}
