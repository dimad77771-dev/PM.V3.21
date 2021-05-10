using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class AssemblyHelper
	{
		public static string GetMainPath()
		{
			var location = Assembly.GetExecutingAssembly().Location;
			var path = Path.GetDirectoryName(location);
			return path;
		}
	}
}
