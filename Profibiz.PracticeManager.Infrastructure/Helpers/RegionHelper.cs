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
	public static class RegionHelper
	{
		public static FrameworkElement OpenViewInRegion(string regionName, object viewCode, Dictionary<string, object> pm = null, object datacontext = null)
		{
			var view = (FrameworkElement)ServiceLocator.Current.GetInstance<object>(viewCode.ToString());
			if (datacontext != null)
			{
				view.DataContext = datacontext;
			}
			var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
			var region = regionManager.Regions[regionName];
			region.RemoveAll();
			var eview = (FrameworkElement)region.Views.FirstOrDefault(q => q.GetType().FullName == view.GetType().FullName);
			if (eview == null)
			{
				NLog.vv(() => viewCode + ";" + view.GetHashCode() + ";" + view.GetType().FullName);
				region.Add(view);
				region.Activate(view);

				var viewmodel = view.DataContext;
				if (viewmodel != null)
				{
					var param = QueryHelper.BuildQuery(pm);
					OnOpenInvoke(viewmodel, param);
				}
				return view;
			}
			else
			{
				region.Activate(eview);
				return eview;
			}
		}

		//public static void OpenViewInRegion(string regionName, object viewCode, Dictionary<string, object> pm = null)
		//{
		//	var ribbonViewCode = GetRibbonViewCode(viewCode.ToString());

		//	var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
		//	var region = regionManager.Regions[regionName];

		//	var view = (FrameworkElement)region.Views.FirstOrDefault(q => q.GetType().Name == viewCode.ToString());
		//	if (view == null)
		//	{

		//		//NLog.vv(() => viewCode + ";" + view.GetHashCode() + ";" + view.GetType().FullName);
		//		view = (FrameworkElement)ServiceLocator.Current.GetInstance<object>(viewCode.ToString());
		//		region.Add(view);
		//		region.Activate(view);

		//		if (regionName == MAIN_REGION)
		//		{
		//			var toolbarRegion = GetToolbarRegion();
		//			var ribbonView = (FrameworkElement)ServiceLocator.Current.GetInstance<object>(ribbonViewCode.ToString());
		//			toolbarRegion.Add(ribbonView);
		//			toolbarRegion.Activate(ribbonView);
		//			ribbonView.DataContext = view.DataContext;
		//		}


		//		var viewmodel = view.DataContext;
		//		if (viewmodel != null)
		//		{
		//			var param = QueryHelper.BuildQuery(pm);
		//			OnOpenInvoke(viewmodel, param);
		//		}
		//	}
		//	else
		//	{
		//		region.Activate(view);
		//		if (regionName == MAIN_REGION)
		//		{
		//			var toolbarRegion = GetToolbarRegion();
		//			var ribbonView = (FrameworkElement)toolbarRegion.Views.FirstOrDefault(q => q.GetType().Name == ribbonViewCode);
		//			toolbarRegion.Activate(ribbonView);
		//		}
		//	}
		//}

		//static string GetRibbonViewCode(string viewcode)
		//{
		//	return "Ribbon" + viewcode;
		//}



		public static FrameworkElement OpenOrActivateViewInMainRegion(object viewCode, Dictionary<string, object> pm = null, object datacontext = null)
		{
			return OpenViewInRegion(MAIN_REGION, viewCode, pm, datacontext);
		}


		private static void OnOpenInvoke(object viewmodel, string openparam)
		{
			var method = viewmodel.GetType().GetMethod("OnOpen");
			if (method != null)
			{
				method.Invoke(viewmodel, new string[] { openparam });
			}
		}

		public static void CloseLeftNavigationPopUp()
		{
			MessengerHelper.Send<MsgLeftNavigationPanelNeedClosePopUp>(new MsgLeftNavigationPanelNeedClosePopUp());
		}


		public static IRegion GetToolbarRegion()
		{
			var regionManager = ServiceHelper.GetInstance<IRegionManager>();
			var region = regionManager.Regions["ToolbarRegion"];
			return region;
		}
		public static Control GetToolbarRegionView()
		{
			var region = GetToolbarRegion();
			if (region.ActiveViews != null && region.ActiveViews.Any())
			{
				return (Control)region.ActiveViews.First();
			}
			return null;
		}


		public static IRegion GetMainRegion()
		{
			var regionManager = ServiceHelper.GetInstance<IRegionManager>();
			var region = regionManager.Regions[MAIN_REGION];
			return region;
		}
		public static Control GetMainRegionView()
		{
			var region = GetMainRegion();
			if (region.ActiveViews != null && region.ActiveViews.Any())
			{
				return (Control)region.ActiveViews.First();
			}
			return null;
		}

		public static Boolean OnCloseRegion()
		{
			var currentViewModel = GetMainRegionView()?.DataContext as IOnCloseView;
			if (currentViewModel != null)
			{
				var canClose = currentViewModel.OnClose();
				if (!canClose)
				{
					return false;
				}
			}
			return true;
		}

		public static string MAIN_REGION = "MainRegion";
	}

	public interface IOnCloseView
	{
		bool OnClose();
	}
}
