using Autofac;
using Autofac.Core;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class ShowDXWindowsAction : TriggerAction<UIElement>
	{
		protected override void Invoke(object parameter)
		{
			var args = (InteractionRequestedEventArgs)parameter;
			var param = (ShowDXWindowsActionParam)args.Context;
			var viewCode = param.ViewCode;

			var view = ServiceLocator.Current.GetInstance<object>(viewCode.ToString());
			var wnd = (DXWindow)view;
			wnd.Owner = Application.Current.MainWindow;
			//var wnd = (Window)view;

			var viewmodel = wnd.DataContext;
			if (viewmodel != null)
			{
				OnOpenInvoke(viewmodel, param.Param);
			}

			if (param.IsModal)
			{
				wnd.ShowDialog();
			}
			else
			{
				wnd.Show();
			}
		}

		void OnOpenInvoke(object viewmodel, object openparam)
		{
			var method = viewmodel.GetType().GetMethod("OnOpen");
			method.Invoke(viewmodel, new object[] { openparam });
		}

		public void OpenWindow(ShowDXWindowsActionParam parameter)
		{
			Invoke(new InteractionRequestedEventArgs(parameter, null));
		}
	}


	public class ShowDXWindowsActionParam : Confirmation
	{
		public object ViewCode { get; set; }
		public bool IsModal { get; set; } = true;
		public object Param { get; set; }
	}
}
