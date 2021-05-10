using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;
using System.Linq.Expressions;
using Profibiz.PracticeManager.Model;
using System.Reflection;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using System.Data.Entity;

namespace Profibiz.PracticeManager.BL
{
	public static class DbUpdateRowsHelper
	{
		public static void UpdateList<T>(T[] dbrows, T[] newrows, Func<T, object> keyfunc, DbContext db) where T : class
		{
			var delrows = dbrows.Where(q => !newrows.Any(z => Object.Equals(keyfunc(z), keyfunc(q)))).ToList();
			foreach(var delrow in delrows)
			{
				db.Set<T>().Remove(delrow);
				db.SaveChangesEx();
			}

			var mapper = AutoMapperHelper.GetPocoMapper(typeof(T));
			foreach (var nrow in newrows)
			{
				var row = dbrows.FirstOrDefault(q => Object.Equals(keyfunc(q),  keyfunc(nrow)));
				if (row == null)
				{
					row = nrow;
					db.Set<T>().Add(row);
				}
				else
				{
					mapper.Map(nrow, row);
				}
				db.SaveChangesEx();
			}
		}

	}
}