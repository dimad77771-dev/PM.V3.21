using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web;

namespace Profibiz.PracticeManager.WebApi.Controllers
{
    public static class QueryHelper
    {
		public static Guid? GetGuid(string query, string name)
		{
			var val = GetParm(query, name);
			if (String.IsNullOrEmpty(val))
			{
				return null;
			}
			return Guid.Parse(val);
		}

		public static String GetParm(string query, string name)
		{
			var allvalues = HttpUtility.ParseQueryString(query);
			return allvalues[name];
		}
	}
}