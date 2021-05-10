using Microsoft.Practices.ServiceLocation;
using Prism.Regions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class QueryHelper
	{
		public static string CreateQuery(List<string> list)
		{
			return string.Join("&", list.ToArray());
		}

		public static Uri BuildUri(object viewCode, Dictionary<string, object> parms = null)
		{
			var uriBuilder = new UriBuilder();
			uriBuilder.Path = viewCode.ToString();
			if (parms != null)
			{
				var query = HttpUtility.ParseQueryString(uriBuilder.Query);
				foreach (var parm in parms)
				{
					query[parm.Key] = parm.Value.ToString();
				}
				uriBuilder.Query = query.ToString();
			}
			var longurl = uriBuilder.ToString();
			var uri = uriBuilder.Uri;
			return uri;
		}

		public static String BuildQuery(string parm1, object value1)
		{
			var parms = new Dictionary<string, object>();
			parms.Add(parm1, value1);
			return BuildQuery(parms);
		}
		public static String BuildQuery(string parm1, object value1, string parm2, object value2)
		{
			var parms = new Dictionary<string, object>();
			parms.Add(parm1, value1);
			parms.Add(parm2, value2);
			return BuildQuery(parms);
		}

		public static String BuildQuery(Dictionary<string, object> parms = null)
		{
			if (parms == null)
			{
				return "";
			}

			var query = HttpUtility.ParseQueryString("");
			foreach (var parm in parms)
			{
				query[parm.Key] = parm.Value?.ToString();
			}
			return query.ToString();
		}

		public static NameValueCollection ParseString(string parms = null)
		{
			var query = HttpUtility.ParseQueryString(parms);
			return query;
		}

		public static Guid? ParseGuid(string parm)
		{
			if (String.IsNullOrEmpty(parm))
			{
				return null;
			}
			return Guid.Parse(parm);
		}

		public static Boolean IsStringParam(object param)
		{
			return (param is string && !string.IsNullOrEmpty((string)param));
		}
	}
}
