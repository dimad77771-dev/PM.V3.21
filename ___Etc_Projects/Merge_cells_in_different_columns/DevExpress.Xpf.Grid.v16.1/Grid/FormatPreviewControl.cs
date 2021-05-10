// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.FormatPreviewControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace DevExpress.Xpf.Grid
{
  public class FormatPreviewControl : ContentControl
  {
    public static readonly DependencyProperty FormatProperty = DependencyProperty.Register("Format", typeof (Freezable), typeof (FormatPreviewControl), new PropertyMetadata((object) null, (PropertyChangedCallback) ((d, e) => ((FormatPreviewControl) d).UpdatePreview())));

    public Freezable Format
    {
      get
      {
        return (Freezable) this.GetValue(FormatPreviewControl.FormatProperty);
      }
      set
      {
        this.SetValue(FormatPreviewControl.FormatProperty, (object) value);
      }
    }

    public FormatPreviewControl()
    {
      this.SetDefaultStyleKey(typeof (FormatPreviewControl));
    }

    private void UpdatePreview()
    {
      this.UpdatePreview<FontFamily>(Control.FontFamilyProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontFamilyProperty, (FontFamily) null);
      this.UpdatePreview<Brush>(Control.ForegroundProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.ForegroundProperty, (Brush) null);
      this.UpdatePreview<Brush>(Control.BackgroundProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.BackgroundProperty, (Brush) null);
      this.UpdatePreview<double>(Control.FontSizeProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontSizeProperty, 0.0);
      this.UpdatePreview<FontStyle>(Control.FontStyleProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontStyleProperty, FontStyles.Normal);
      this.UpdatePreview<FontWeight>(Control.FontWeightProperty, DevExpress.Xpf.Core.ConditionalFormatting.Format.FontWeightProperty, FontWeights.Normal);
    }

    private void UpdatePreview<T>(DependencyProperty previewProperty, DependencyProperty formatProperty, T defaultValue)
    {
      DevExpress.Xpf.Core.ConditionalFormatting.Format format = this.Format as DevExpress.Xpf.Core.ConditionalFormatting.Format;
      if (format != null)
      {
        T obj = (T) format.GetValue(formatProperty);
        if (!object.Equals((object) obj, (object) defaultValue))
        {
          this.SetValue(previewProperty, (object) obj);
          return;
        }
      }
      this.ClearValue(previewProperty);
    }
  }
}
