// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FixedTotalSummaryItemsTextBlock
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Grid.Native;
using DevExpress.Xpf.Utils;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Markup;

namespace DevExpress.Xpf.Grid
{
  public class FixedTotalSummaryItemsTextBlock : ContentControl, IFixedTotalSummary
  {
    public static readonly DependencyProperty TotalSummariesSourceProperty = DependencyPropertyManager.Register("TotalSummariesSource", typeof (IList<GridTotalSummaryData>), typeof (FixedTotalSummaryItemsTextBlock), (PropertyMetadata) new FrameworkPropertyMetadata((object) null, new PropertyChangedCallback(FixedTotalSummaryItemsTextBlock.OnTotalSummariesSourceChanged)));
    public static readonly DependencyProperty SummaryTextProperty = DependencyPropertyManager.Register("SummaryText", typeof (string), typeof (FixedTotalSummaryItemsTextBlock), (PropertyMetadata) new FrameworkPropertyMetadata((object) string.Empty));
    public static readonly DependencyProperty SummaryTextInfoProperty = DependencyPropertyManager.Register("SummaryTextInfo", typeof (InlineCollectionInfo), typeof (FixedTotalSummaryItemsTextBlock), (PropertyMetadata) new FrameworkPropertyMetadata((PropertyChangedCallback) null));
    public static readonly DependencyProperty FixedTotalSummaryElementStyleProperty = DependencyProperty.Register("FixedTotalSummaryElementStyle", typeof (Style), typeof (FixedTotalSummaryItemsTextBlock), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FixedTotalSummaryItemsTextBlock) d).UpdateSummaryText())));

    public IList<GridTotalSummaryData> TotalSummariesSource
    {
      get
      {
        return (IList<GridTotalSummaryData>) this.GetValue(FixedTotalSummaryItemsTextBlock.TotalSummariesSourceProperty);
      }
      set
      {
        this.SetValue(FixedTotalSummaryItemsTextBlock.TotalSummariesSourceProperty, (object) value);
      }
    }

    public string SummaryText
    {
      get
      {
        return (string) this.GetValue(FixedTotalSummaryItemsTextBlock.SummaryTextProperty);
      }
      set
      {
        this.SetValue(FixedTotalSummaryItemsTextBlock.SummaryTextProperty, (object) value);
      }
    }

    public InlineCollectionInfo SummaryTextInfo
    {
      get
      {
        return (InlineCollectionInfo) this.GetValue(FixedTotalSummaryItemsTextBlock.SummaryTextInfoProperty);
      }
      set
      {
        this.SetValue(FixedTotalSummaryItemsTextBlock.SummaryTextInfoProperty, (object) value);
      }
    }

    public Style FixedTotalSummaryElementStyle
    {
      get
      {
        return (Style) this.GetValue(FixedTotalSummaryItemsTextBlock.FixedTotalSummaryElementStyleProperty);
      }
      set
      {
        this.SetValue(FixedTotalSummaryItemsTextBlock.FixedTotalSummaryElementStyleProperty, (object) value);
      }
    }

    public FixedTotalSummaryItemsTextBlock()
    {
      this.SetDefaultStyleKey(typeof (FixedTotalSummaryItemsTextBlock));
    }

    private static void OnTotalSummariesSourceChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
    {
      FixedTotalSummaryItemsTextBlock summaryItemsTextBlock = o as FixedTotalSummaryItemsTextBlock;
      if (summaryItemsTextBlock == null)
        return;
      summaryItemsTextBlock.OnTotalSummariesSourceChanged(e.OldValue as ObservableCollection<GridTotalSummaryData>, e.NewValue as ObservableCollection<GridTotalSummaryData>);
    }

    private void OnTotalSummariesSourceChanged(ObservableCollection<GridTotalSummaryData> oldList, ObservableCollection<GridTotalSummaryData> newList)
    {
      if (oldList != null)
        oldList.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.FixedTotalSummaryCollectionChanged);
      if (newList == null)
        return;
      newList.CollectionChanged += new NotifyCollectionChangedEventHandler(this.FixedTotalSummaryCollectionChanged);
      this.UpdateSummaryText();
    }

    private void FixedTotalSummaryCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
      this.UpdateSummaryText();
    }

    private void UpdateSummaryText()
    {
      InlineCollectionInfo summariesTextValues = FixedTotalSummaryHelper.GetFixedSummariesTextValues(this.TotalSummariesSource, this.FixedTotalSummaryElementStyle);
      this.SummaryTextInfo = summariesTextValues;
      this.SummaryText = summariesTextValues != null ? summariesTextValues.TextSource : (string) null;
    }

    [Browsable(false)]
    public bool ShouldSerializeTotalSummariesSource(XamlDesignerSerializationManager manager)
    {
      return false;
    }
  }
}
