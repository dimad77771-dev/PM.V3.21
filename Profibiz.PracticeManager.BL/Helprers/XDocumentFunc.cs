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
using System.Xml.Linq;

namespace Profibiz.PracticeManager.BL
{
	public static class XDocumentFunc
	{
		public static IEnumerable<XElement> ParseArray(string xmlstring)
		{
			var xmldoc = XDocument.Parse("<root>" + xmlstring + "</root>").Elements().Single().Elements();
			return xmldoc;
		}

	}
}

