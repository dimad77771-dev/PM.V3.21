using Autofac;
using Autofac.Core;
using DevExpress.Mvvm;
using DevExpress.Mvvm.UI;
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
	public class CloseDXWindowsAction : TriggerAction<UIElement>
	{
		protected override void Invoke(object parameter)
		{
			var wnd = this.AssociatedObject as DXWindow;
			if (wnd == null)
			{
				wnd = LayoutTreeHelper.GetVisualParents(this.AssociatedObject).OfType<DXWindow>().FirstOrDefault();
			}
			if (wnd == null) throw new NotSupportedException();
			wnd.Close();
		}
	}
}
