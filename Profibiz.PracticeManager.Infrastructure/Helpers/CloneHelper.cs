using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class CloneHelper
	{
		static List<MapTypeInfo> TypeInfoList = new List<MapTypeInfo>();

		//скопировать в новый объект
		public static T Map<T>(Object src)
		{
			Type typeD = typeof(T);
			Type typeS = src.GetType();

			var item = FindMapTypeInfo(typeD, typeS);

			var dst = Activator.CreateInstance<T>();

			var propsS = item.ForType(typeS);
			var propsD = item.ForType(typeD);
			for (int k = 0; k < propsS.Count; k++)
			{
				if (propsS[k].CanRead && propsD[k].CanWrite)
				{
					var val = propsS[k].GetValue(src, null);
					propsD[k].SetValue(dst, val, null);
				}
			}

			return dst;
		}

		//обновить свойства объекта по исходному объекту
		public static void Update<T>(T dst, Object src)
		{
			Type typeD = typeof(T);
			Type typeS = src.GetType();

			var item = FindMapTypeInfo(typeD, typeS);

			var propsS = item.ForType(typeS);
			var propsD = item.ForType(typeD);
			for (int k = 0; k < propsS.Count; k++)
			{
				if (propsS[k].CanRead && propsD[k].CanWrite)
				{
					var val = propsS[k].GetValue(src, null);
					propsD[k].SetValue(dst, val, null);
				}
			}
		}



		//построим MapTypeInfo для данной пары
		static MapTypeInfo BuildMapTypeInfo(Type type1, Type type2)
		{
			var info = new MapTypeInfo()
			{
				Type1 = type1,
				Type2 = type2,
				Props1 = new List<PropertyInfo>(),
				Props2 = new List<PropertyInfo>(),
			};

			var props1 = type1.GetProperties();
			var props2 = type2.GetProperties();

			foreach (var prop1 in props1)
			{
				var prop2 = props2.Where(q => q.Name == prop1.Name).FirstOrDefault();
				if (prop2 != null)
				{
					info.Props1.Add(prop1);
					info.Props2.Add(prop2);
				}
			}

			return info;
		}

		//найдем или построим MapTypeInfo для данной пары
		public static MapTypeInfo FindMapTypeInfo(Type type1, Type type2)
		{
			//пробуем найти в двух порядках
			var item = TypeInfoList.Where(q => q.Type1 == type1 && q.Type2 == type2).FirstOrDefault();
			if (item == null)
			{
				item = TypeInfoList.Where(q => q.Type1 == type2 && q.Type2 == type1).FirstOrDefault();
			}

			//если не нашли -> создаем
			if (item == null)
			{
				item = BuildMapTypeInfo(type1, type2);
				TypeInfoList.Add(item);
			}
			return item;
		}

		public static void Add<T1, T2>(MapTypeInfo info, Expression<Func<T1, object>> propertyExpression1, Expression<Func<T2, object>> propertyExpression2)
		{
			var prop1 = MapperReflectionHelper.FindProperty(propertyExpression1);
			var prop2 = MapperReflectionHelper.FindProperty(propertyExpression2);
			var type1 = typeof(T1);
			var type2 = typeof(T2);

			if (!ExistsPairProperties(info, prop1, prop2))
			{
				var props1 = info.ForType(type1);
				var props2 = info.ForType(type2);

				props1.Add(prop1);
				props2.Add(prop2);
			}
		}

		public static MapTypeInfoExpression<T1, T2> CreateMap<T1, T2>()
		{
			return new MapTypeInfoExpression<T1, T2>();
		}

		static Boolean ExistsPairProperties(MapTypeInfo info, PropertyInfo prop1, PropertyInfo prop2)
		{
			for (int k = 0; k < info.Props1.Count; k++)
			{
				if
				(
					(info.Props1[k] == prop1 && info.Props2[k] == prop2) ||
					(info.Props1[k] == prop2 && info.Props2[k] == prop1)
				)
				{
					return true;
				}
			}

			return false;
		}

		public static List<T> MapList<T>(IEnumerable srcList)
		{
			if (srcList == null)
			{
				return null;
			}

			var rezList = new List<T>();
			foreach (var srcObj in srcList)
			{
				var rezObj = Map<T>(srcObj);
				rezList.Add(rezObj);
			}

			return rezList;
		}

		public static T Copy<T>(T src)
		{
			return Map<T>(src);
		}


		//скопировать в новый объект
		public static T GetPocoClone<T>(this T src, CloneType cloneType = CloneType.PocoDbOnly)
		{
			var type = typeof(T);
			var dst = Activator.CreateInstance<T>();


			var props = type.GetProperties();
			foreach(var prop in props)
			{
				if (!prop.CanRead || !prop.CanWrite)
				{
					continue;
				}
				if (cloneType.HasFlag(CloneType.PocoDbOnly))
				{
					var ptype = prop.PropertyType;
					if (!(
							ptype == typeof(byte[]) ||
							ptype == typeof(string) ||

							ptype == typeof(Nullable<bool>) ||
							ptype == typeof(Nullable<byte>) ||
							ptype == typeof(Nullable<decimal>) ||
							ptype == typeof(Nullable<double>) ||
							ptype == typeof(Nullable<float>) ||
							ptype == typeof(Nullable<int>) ||
							ptype == typeof(Nullable<long>) ||
							ptype == typeof(Nullable<short>) ||
							ptype == typeof(Nullable<DateTime>) ||
							ptype == typeof(Nullable<DateTimeOffset>) ||
							ptype == typeof(Nullable<Guid>) ||
							ptype == typeof(Nullable<TimeSpan>) ||

							ptype == typeof(bool) ||
							ptype == typeof(byte) ||
							ptype == typeof(decimal) ||
							ptype == typeof(double) ||
							ptype == typeof(float) ||
							ptype == typeof(int) ||
							ptype == typeof(long) ||
							ptype == typeof(short) ||
							ptype == typeof(DateTime) ||
							ptype == typeof(DateTimeOffset) ||
							ptype == typeof(Guid) ||
							ptype == typeof(TimeSpan) ||

							false
						))
					{
						continue;
					}
				}

				var val = prop.GetValue(src, null);
				prop.SetValue(dst, val, null);
			}
			return dst;
		}


		public static List<T> GetPocoCloneList<T>(this IEnumerable<T> src, CloneType cloneType = CloneType.PocoDbOnly)
		{
			return src.Select(q => GetPocoClone(q, cloneType)).ToList();
		}

		[Flags]
		public enum CloneType
		{
			PocoDbOnly = 1,
		}
	}

	//информация о конкретной паре классов
	public class MapTypeInfo
	{
		public Type Type1;
		public Type Type2;
		public List<PropertyInfo> Props1;
		public List<PropertyInfo> Props2;

		public List<PropertyInfo> ForType(Type type)
		{
			if (type == Type1)
			{
				return Props1;
			}
			else if (type == Type2)
			{
				return Props2;
			}
			else
			{
				throw new Exception("error 100");
			}
		}
	}

	public class MapTypeInfoExpression<T1, T2>
	{
		public MapTypeInfoExpression()
		{
			MapInfo = CloneHelper.FindMapTypeInfo(typeof(T1), typeof(T2));
		}


		public MapTypeInfoExpression<T1, T2> Add(Expression<Func<T1, object>> propertyExpression1, Expression<Func<T2, object>> propertyExpression2)
		{
			CloneHelper.Add<T1, T2>(MapInfo, propertyExpression1, propertyExpression2);
			return this;
		}

		public MapTypeInfoExpression<T1, T2> ClearAll()
		{
			MapInfo.Props1.Clear();
			MapInfo.Props2.Clear();
			return this;
		}


		MapTypeInfo MapInfo;
	}











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
