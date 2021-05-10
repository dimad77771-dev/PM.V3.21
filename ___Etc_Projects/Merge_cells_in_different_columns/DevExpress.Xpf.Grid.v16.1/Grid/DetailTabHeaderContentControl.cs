// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.DetailTabHeaderContentControl
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Utils;
using System.Windows;
using System.Windows.Controls;

namespace DevExpress.Xpf.Grid
{
  public class DetailTabHeaderContentControl : ContentControl
  {
    public DetailTabHeaderContentControl()
    {
      ControlExtensions.SetDefaultStyleKey(this, typeof (DetailTabHeaderContentControl));
    }

    protected override Size MeasureOverride(Size constraint)
    {
      return MeasurePixelSnapperHelper.MeasureOverride(base.MeasureOverride(constraint), SnapperType.Ceil);
    }
  }
}
