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

namespace Profibiz.PracticeManager.BL
{
	public static class MapperReflectionHelper
	{
		public static PropertyInfo FindProperty(LambdaExpression lambdaExpression)
		{
			Expression expressionToCheck = lambdaExpression;

			bool done = false;

			while (!done)
			{
				switch (expressionToCheck.NodeType)
				{
					case ExpressionType.Convert:
						expressionToCheck = ((UnaryExpression)expressionToCheck).Operand;
						break;
					case ExpressionType.Lambda:
						expressionToCheck = ((LambdaExpression)expressionToCheck).Body;
						break;
					case ExpressionType.MemberAccess:
						var memberExpression = ((MemberExpression)expressionToCheck);

						if (memberExpression.Expression.NodeType != ExpressionType.Parameter &&
							memberExpression.Expression.NodeType != ExpressionType.Convert)
						{
							throw new ArgumentException(string.Format("Expression '{0}' must resolve to top-level member and not any child object's properties. Use a custom resolver on the child type or the AfterMap option instead.", lambdaExpression), "lambdaExpression");
						}

						MemberInfo member = memberExpression.Member;

						if (!(member is PropertyInfo))
						{
							throw new Exception("Not a property");
						}

						return (PropertyInfo)member;
					default:
						done = true;
						break;
				}
			}

			throw new Exception("Custom configuration for members is only supported for top-level individual members on a type.");
		}

		public static Type GetMemberType(this MemberInfo memberInfo)
		{
			if (memberInfo is MethodInfo)
				return ((MethodInfo)memberInfo).ReturnType;
			if (memberInfo is PropertyInfo)
				return ((PropertyInfo)memberInfo).PropertyType;
			if (memberInfo is FieldInfo)
				return ((FieldInfo)memberInfo).FieldType;
			return null;
		}
	}
}