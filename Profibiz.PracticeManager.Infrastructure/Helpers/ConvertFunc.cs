using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ConvertFunc
	{
		public static BrushConverter brushConverter;
		public static Brush ToBrush(string arg)
		{
			if (brushConverter == null) brushConverter = new BrushConverter();
			return (Brush)(brushConverter.ConvertFromString(arg));
		}

		
		public static Color ToColor(string arg)
		{
			return (Color)(ColorConverter.ConvertFromString(arg));
		}


	}
}

