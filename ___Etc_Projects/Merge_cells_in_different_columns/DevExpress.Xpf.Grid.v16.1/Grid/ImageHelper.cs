// Decompiled with JetBrains decompiler
// Type: DevExpress.Xpf.Grid.ImageHelper
// Assembly: DevExpress.Xpf.Grid.v16.1, Version=16.1.6.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a
// MVID: BB681040-0474-4C7D-BB3E-E6E5DFDDD1F8
// Assembly location: H:\DOWNLOADS\T145231\T145231\bin\Debug\DevExpress.Xpf.Grid.v16.1.dll

using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace DevExpress.Xpf.Grid
{
  public class ImageHelper
  {
    private static Dictionary<string, BitmapImage> images = new Dictionary<string, BitmapImage>();

    private static BitmapImage LoadImage(string imageName)
    {
      using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(string.Format("DevExpress.Xpf.Grid.Images.{0}.png", (object) imageName)))
        return DevExpress.Xpf.Core.Native.ImageHelper.CreateImageFromStream(manifestResourceStream);
    }

    public static BitmapImage GetImage(string imageName)
    {
      BitmapImage bitmapImage;
      if (!ImageHelper.images.TryGetValue(imageName, out bitmapImage))
      {
        bitmapImage = ImageHelper.LoadImage(imageName);
        ImageHelper.images.Add(imageName, bitmapImage);
      }
      return bitmapImage;
    }
  }
}
