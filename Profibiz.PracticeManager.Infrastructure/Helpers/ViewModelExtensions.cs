using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ViewModelExtensions
	{
		public static bool IsViewModelActive2__(Object viewmodel)
		{
			//if (RuntimeHelper.IsMachineD)
			//{
			//	return true;
			//}
			

			var dt1 = DateTimeHelper.Now;
			var exflag = false;
			var cnt = Application.Current.Windows.Count;
			for (var i = 0; i < cnt; i++)
			{
				var wnd = Application.Current.Windows[i];
				if (LayoutTreeHelper
						.GetVisualChildren(wnd).OfType<FrameworkElement>()
						.Select(q => q.DataContext).Any(q => q == viewmodel)
					)
				{
					exflag = true;
					break;
				}
			}

			var dt2 = DateTimeHelper.Now;
			var z = (dt2 - dt1).TotalMilliseconds;
			NLog.Trace("z11=" + z + ";" + exflag);

			return exflag;
		}


		public static bool IsViewModelActive(Object viewmodel)
		{
			NLog.Trace("IsViewModelActive; viewmodel=" + viewmodel.GetType().FullName + ";" + viewmodel.GetHashCode());

			var wnd = ViewModel2Window(viewmodel);
			if (wnd == null)
			{
				NLog.Trace("IsViewModelActive; ret=false1");
				return false;
			}
			else
			{
				NLog.Trace("IsViewModelActive; ret=true1");
				return true;
			}
			//if (wnd == null)
			//{
			//	NLog.Trace("IsViewModelActive; ret=false1");
			//	return false;
			//}
			//else
			//{
			//	NLog.Trace("IsViewModelActive; wnd=" + NLog.GetObjectInfo(wnd));
			//	var windows = Application.Current.Windows;
			//	for(int i = 0; i < windows.Count; i++)
			//	{
			//		if (windows[i] == wnd)
			//		{
			//			NLog.Trace("IsViewModelActive; ret=true");
			//			return true;
			//		}
			//	}
			//	NLog.Trace("IsViewModelActive; ret=false2");
			//	return false;
			//}
		}

		static object lockobject = new object();
		public static FrameworkElement ViewModel2FrameworkElement(Object viewmodel)
		{
			lock (lockobject)
			{
				var dt1 = DateTimeHelper.Now;

				var findInCache = FindInCache(viewmodel);
				if (findInCache.IsFind)
				{
					NLog.Trace("findInCache.FindObject=" + NLog.GetObjectInfo(findInCache.FindObject));
					if (findInCache.FindObject != null)
					{
						var parents = LayoutTreeHelper.GetVisualParents(findInCache.FindObject).ToArray();
						NLog.Trace(string.Join("---", parents.Select(q => NLog.GetObjectInfo(q)).ToArray()));
					}
					return findInCache.FindObject;
				}

				FrameworkElement findobj = null;

				var cnt = Application.Current.Windows.Count;

				if (findobj == null)
				{
					for (var i = 0; i < cnt; i++)
					{
						var wnd = Application.Current.Windows[i];
						if (wnd.DataContext == viewmodel)
						{
							findobj = wnd;
							break;
						}
					}
				}

				if (findobj == null)
				{
					for (var j = 0; j < cnt; j++)
					{
						var wnd2 = Application.Current.Windows[j];
						var visobj =
							LayoutTreeHelper
								.GetVisualChildren(wnd2).OfType<FrameworkElement>()
								.FirstOrDefault(q => q.DataContext == viewmodel);
						if (visobj != null)
						{
							findobj = visobj;
							break;
						}
					}
				}
				

				if (findobj != null)
				{
					CacheFindItems.Add(new FindItem { ViewModel = new WeakReference(viewmodel), VisualObject = new WeakReference(findobj) });
				}


				var dt2 = DateTimeHelper.Now;
				var z = (dt2 - dt1).TotalMilliseconds;
				NLog.Trace("z12=" + z + ";");

				return findobj;
			}
		}


		public static Window ViewModel2Window(Object viewmodel)
		{
			var visobj = ViewModel2FrameworkElement(viewmodel);
			if (visobj == null)
			{
				NLog.Trace("ViewModel2Window=return null");
				return null;
			}

			var windows = GetAppWindows();

			var wnd = windows.Find(q => q == visobj);
			if (wnd != null)
			{
				NLog.Trace("ViewModel2Window=return wnd;");
				return wnd;
			}


			foreach (var parent in LayoutTreeHelper.GetVisualParents(visobj))
			{
				var wnd2 = windows.Find(q => q == parent);
				if (wnd2 != null)
				{
					NLog.Trace("ViewModel2Window=return wnd2");
					return wnd2;
				}
			}

			NLog.Trace("ViewModel2Window=return null2");
			return null;
		}

		static List<Window> GetAppWindows()
		{
			var windows = new List<Window>();
			var cnt = Application.Current.Windows.Count;
			for(int k = 0; k < cnt; k++)
			{
				windows.Add(Application.Current.Windows[k]);
			}
			return windows;
		}

		static FindInCacheReturn FindInCache(Object viewmodel)
		{
			NLog.Trace("CacheFindItems.Count_1=" + CacheFindItems.Count);

			var count = CacheFindItems.Count;
			for (int k = count - 1; k >= 0; k--)
			{
				if (!CacheFindItems[k].ViewModel.IsAlive)
				{
					CacheFindItems.RemoveAt(k);
				}
			}

			NLog.Trace("CacheFindItems.Count_2=" + CacheFindItems.Count);

			foreach (var item in CacheFindItems)
			{
				if (item.ViewModel.Target == viewmodel)
				{
					if (item.VisualObject.IsAlive)
					{
						var ret = (FrameworkElement)item.VisualObject.Target;
						return new FindInCacheReturn { IsFind = true, FindObject = ret };
					}
					else
					{
						return new FindInCacheReturn { IsFind = true, FindObject = null };
					}
				}
			}

			return new FindInCacheReturn { IsFind = false };
		}
		class FindInCacheReturn
		{
			public Boolean IsFind { get; set; }
			public FrameworkElement FindObject { get; set; }
		}

		static List<FindItem> CacheFindItems = new List<FindItem>();
		class FindItem
		{
			public WeakReference ViewModel { get; set; }
			public WeakReference VisualObject { get; set; }
		}
	}
}
