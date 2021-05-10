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

namespace Profibiz.PracticeManager.BL
{
	public static class WebQueryHelper
	{
		public static List<Guid> Guids(string arg)
		{
			if (string.IsNullOrEmpty(arg)) return new List<Guid>();
			arg = arg.Replace(',', ';');
			var rez = arg.Split(';').Select(q => new Guid(q)).ToList();
			return rez;
		}

		public static List<Guid?> GuidsNull(string arg)
		{
			return Guids(arg).Select(q => (Guid?)q).ToList();
		}

	}
}

