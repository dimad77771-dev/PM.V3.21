using DevExpress.Mvvm;
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

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class ServiceHelper
	{
		public static T GetInstance<T>()
		{
			return ServiceLocator.Current.GetInstance<T>();
		}

		public static IRegionManager GetRegionManager()
		{
			return ServiceHelper.GetInstance<IRegionManager>();
		}

	}
}
