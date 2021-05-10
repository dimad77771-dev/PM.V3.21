// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.Printing.SparklineInfo
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Data;
using DevExpress.Export.Xl;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.Editors.Settings;
using DevExpress.XtraExport.Helpers;

namespace DevExpress.Xpf.Grid.Printing
{
  public class SparklineInfo : ISparklineInfo
  {
    private readonly SparklineEditSettings EditSettings;
    private SparklineStyleSettings _styleSettings;
    private SparklineViewType? _viewType;

    private SparklineStyleSettings StyleSettings
    {
      get
      {
        if (this._styleSettings == null)
        {
          this._styleSettings = (SparklineStyleSettings) this.EditSettings.StyleSettings;
          if (this._styleSettings == null)
            this._styleSettings = (SparklineStyleSettings) new EmptySparklineStyleSettings();
        }
        return this._styleSettings;
      }
    }

    private EmptySparklineStyleSettings EmptySparkSettings
    {
      get
      {
        return this.StyleSettings as EmptySparklineStyleSettings;
      }
    }

    private LineSparklineStyleSettings LineSparkSettings
    {
      get
      {
        return this.StyleSettings as LineSparklineStyleSettings;
      }
    }

    private BarSparklineStyleSettings BarSparkSettings
    {
      get
      {
        return this.StyleSettings as BarSparklineStyleSettings;
      }
    }

    private WinLossSparklineStyleSettings WinLossSparkSettings
    {
      get
      {
        return this.StyleSettings as WinLossSparklineStyleSettings;
      }
    }

    private SparklineViewType? ViewType
    {
      get
      {
        if (!this._viewType.HasValue)
          this._viewType = this.GetViewType();
        return this._viewType;
      }
    }

    public System.Drawing.Color ColorFirst
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.StartPointBrush);
      }
    }

    public System.Drawing.Color ColorHigh
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.MaxPointBrush);
      }
    }

    public System.Drawing.Color ColorLast
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.EndPointBrush);
      }
    }

    public System.Drawing.Color ColorLow
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.MinPointBrush);
      }
    }

    public System.Drawing.Color ColorMarker
    {
      get
      {
        return this.GetMarkerColor();
      }
    }

    public System.Drawing.Color ColorNegative
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.NegativePointBrush);
      }
    }

    public System.Drawing.Color ColorSeries
    {
      get
      {
        return AppearanceHelper.BrushToColor((System.Windows.Media.Brush) this.StyleSettings.Brush);
      }
    }

    public bool DisplayMarkers
    {
      get
      {
        return this.GetDisplayMarker();
      }
    }

    public bool SpecificSparklineType
    {
      get
      {
        return this.ViewType.Value == SparklineViewType.Area;
      }
    }

    public bool HighlightFirst
    {
      get
      {
        return this.StyleSettings.HighlightStartPoint;
      }
    }

    public bool HighlightHighest
    {
      get
      {
        return this.StyleSettings.HighlightMaxPoint;
      }
    }

    public bool HighlightLast
    {
      get
      {
        return this.StyleSettings.HighlightEndPoint;
      }
    }

    public bool HighlightLowest
    {
      get
      {
        return this.StyleSettings.HighlightMinPoint;
      }
    }

    public bool HighlightNegative
    {
      get
      {
        return this.GetHighlightNegative();
      }
    }

    public double LineWeight
    {
      get
      {
        return this.GetLineWeight();
      }
    }

    public ColumnSortOrder PointSortOrder
    {
      get
      {
        switch (this.EditSettings.PointArgumentSortOrder)
        {
          case SparklineSortOrder.Ascending:
            return ColumnSortOrder.Ascending;
          case SparklineSortOrder.Descending:
            return ColumnSortOrder.Descending;
          default:
            return ColumnSortOrder.None;
        }
      }
    }

    public XlSparklineType SparklineType
    {
      get
      {
        if (!this.ViewType.HasValue)
          return XlSparklineType.Line;
        switch (this.ViewType.Value)
        {
          case SparklineViewType.Line:
          case SparklineViewType.Area:
            return XlSparklineType.Line;
          case SparklineViewType.Bar:
            return XlSparklineType.Column;
          case SparklineViewType.WinLoss:
            return XlSparklineType.WinLoss;
          default:
            return XlSparklineType.Line;
        }
      }
    }

    public SparklineInfo(SparklineEditSettings editSettings)
    {
      this.EditSettings = editSettings;
    }

    private SparklineViewType? GetViewType()
    {
      if (this.StyleSettings is AreaSparklineStyleSettings)
        return new SparklineViewType?(SparklineViewType.Area);
      if (this.StyleSettings is LineSparklineStyleSettings || this.StyleSettings is EmptySparklineStyleSettings)
        return new SparklineViewType?(SparklineViewType.Line);
      if (this.StyleSettings is BarSparklineStyleSettings)
        return new SparklineViewType?(SparklineViewType.Bar);
      if (this.StyleSettings is WinLossSparklineStyleSettings)
        return new SparklineViewType?(SparklineViewType.WinLoss);
      return new SparklineViewType?();
    }

    private System.Drawing.Color GetMarkerColor()
    {
      return AppearanceHelper.BrushToColor(this.GetMarkerBrush());
    }

    private System.Windows.Media.Brush GetMarkerBrush()
    {
      if (!this.ViewType.HasValue)
        return (System.Windows.Media.Brush) null;
      switch (this.ViewType.Value)
      {
        case SparklineViewType.Line:
        case SparklineViewType.Area:
          if (this.LineSparkSettings == null)
            return (System.Windows.Media.Brush) null;
          return (System.Windows.Media.Brush) this.LineSparkSettings.MarkerBrush;
        default:
          return (System.Windows.Media.Brush) null;
      }
    }

    private bool GetDisplayMarker()
    {
      if (!this.ViewType.HasValue)
        return false;
      switch (this.ViewType.Value)
      {
        case SparklineViewType.Line:
        case SparklineViewType.Area:
          if (this.LineSparkSettings == null)
            return false;
          return this.LineSparkSettings.ShowMarkers;
        default:
          return false;
      }
    }

    private bool GetHighlightNegative()
    {
      if (!this.ViewType.HasValue)
        return false;
      switch (this.ViewType.Value)
      {
        case SparklineViewType.Line:
        case SparklineViewType.Area:
          if (this.LineSparkSettings == null)
            return false;
          return this.LineSparkSettings.HighlightNegativePoints;
        case SparklineViewType.Bar:
          return this.BarSparkSettings.HighlightNegativePoints;
        default:
          return false;
      }
    }

    private double GetLineWeight()
    {
      if (!this.ViewType.HasValue)
        return 1.0;
      switch (this.ViewType.Value)
      {
        case SparklineViewType.Line:
        case SparklineViewType.Area:
          return this.LineSparkSettings != null ? (double) this.LineSparkSettings.LineWidth : 1.0;
        default:
          return 1.0;
      }
    }
  }
}
