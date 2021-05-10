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

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class LinqHelper
	{
		public static IEnumerable<T> PreserveNull<T>(this IEnumerable<T> arr)
		{
			if (arr == null)
			{
				return Enumerable.Empty<T>();
			}
			else
			{
				return arr;
			}
		}

		public static ObservableCollection<T> ToObservableCollection<T>(this IEnumerable<T> arr)
		{
			return new ObservableCollection<T>(arr);
		}

		public static HashSet<T> ToHashset<T>(this IEnumerable<T> arr)
		{
			return new HashSet<T>(arr);
		}

		public static void ForEach<T>(this IEnumerable<T> arr, Action<T> action)
		{
			foreach (var val in arr)
			{
				action(val);
			}
		}


		public static void RemoveRange<T>(this Collection<T> arr, Func<T,bool> wh)
		{
			var del = arr.Where(q => wh(q)).ToArray();
			foreach (var row in del)
			{
				arr.Remove(row);
			}
		}

		public static void RemoveRange<T>(this Collection<T> arr, IEnumerable<T> rows)
		{
			foreach (var row in rows.ToArray())
			{
				arr.Remove(row);
			}
		}

		public static void InsertRange<T>(this Collection<T> arr, int index, IEnumerable<T> rows)
		{
			foreach (var row in rows)
			{
				arr.Insert(index, row);
				index++;
			}
		}



		public static int FindIndex<T>(this IEnumerable<T> arr, Func<T, bool> wh)
		{
			int n = 0;
			foreach (var val in arr)
			{
				if (wh(val))
				{
					return n;
				}
				n++;
			}
			return -1;
		}

		public static bool In<T>(this T arg, params T[] arr)
		{
			foreach (var val in arr)
			{
				if (Object.Equals(arg, val))
				{
					return true;
				}
			}
			return false;
		}

		public static IEnumerable<T> AsDepthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
		{
			yield return head;
			foreach (var node in childrenFunc(head))
			{
				foreach (var child in AsDepthFirstEnumerable(node, childrenFunc))
				{
					yield return child;
				}
			}
		}

		public static IEnumerable<T> AsBreadthFirstEnumerable<T>(this T head, Func<T, IEnumerable<T>> childrenFunc)
		{
			yield return head;
			var last = head;
			foreach (var node in AsBreadthFirstEnumerable(head, childrenFunc))
			{
				foreach (var child in childrenFunc(node))
				{
					yield return child;
					last = child;
				}
				if (last.Equals(node)) yield break;
			}
		}



	}
}

