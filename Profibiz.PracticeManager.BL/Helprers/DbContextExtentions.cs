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
using System.Data.Entity;
using System.Data.Entity.Validation;

namespace Profibiz.PracticeManager.BL
{
	public static class DbContextExtentions
	{
		public static void SaveChangesEx(this DbContext db)
		{
			try
			{
				db.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				var errarr = ex.EntityValidationErrors
					.SelectMany(q => q.ValidationErrors)
					.Select(q => "PropertyName=" + q.PropertyName + "; ErrorMessage=" + q.ErrorMessage)
					.ToArray();
				var errtext = string.Join("--------------------\n", errarr);
				throw new AggregateException("ValidationErrors:\n" + errtext, ex);
			}
		}
	}
}