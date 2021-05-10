using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class BitmapFunc
	{
		public static Bitmap UnitBitmap(Color color)
		{
			var b = new Bitmap(120, 120);
			for(int i = 0; i < 120; i++)
				for (int j = 0; j < 120; j++)
					b.SetPixel(i, j, color);
			return b;
		}

		public static BitmapImage BitmapToImageSource(Bitmap bitmap)
		{
			using (MemoryStream memory = new MemoryStream())
			{
				bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
				memory.Position = 0;
				BitmapImage bitmapimage = new BitmapImage();
				bitmapimage.BeginInit();
				bitmapimage.StreamSource = memory;
				bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
				bitmapimage.EndInit();

				return bitmapimage;
			}
		}

		public static BitmapImage UnitImageSource(Color color)
		{
			return BitmapToImageSource(UnitBitmap(color));
		}

		public static BitmapImage DXImageBitmap(string arg)
		{
			var a32 = (DXImageInfo)new DXImageConverter().ConvertFromString(arg);
			return new BitmapImage(a32.MakeUri());
		}

		public static String DXImage(string arg)
		{
			var a32 = (DXImageInfo)new DXImageConverter().ConvertFromString(arg);
			return a32.MakeUri().ToString();
		}

	}
}

