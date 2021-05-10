using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ExpressionFunc
	{
		public static Expression<Func<T, bool>> AndAlso<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
		{
			// need to detect whether they use the same
			// parameter instance; if not, they need fixing
			ParameterExpression param = expr1.Parameters[0];
			ParameterExpression param2 = expr2.Parameters[0];
			if (param.Name != param2.Name) throw new ArgumentException("parametrs name must be eqval");
			//if (ReferenceEquals(param, expr2.Parameters[0]))
			{
				// simple version
				return Expression.Lambda<Func<T, bool>>(
					Expression.AndAlso(expr1.Body, expr2.Body), param);
			}

			//// otherwise, keep expr1 "as is" and invoke expr2
			//return Expression.Lambda<Func<T, bool>>(
			//	Expression.AndAlso(
			//		expr1.Body,
			//		Expression.Invoke(expr2, param)), param);
		}
	}
}
