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
using LinqKit;

namespace Profibiz.PracticeManager.BL
{
	public static class ExpressionFunc
	{
		public static Expression<Func<T, bool>> True<T>()
		{
			return PredicateBuilder.True<T>();
		}

		public static Expression<Func<T, bool>> False<T>()
		{
			return PredicateBuilder.False<T>();
		}

	}
}