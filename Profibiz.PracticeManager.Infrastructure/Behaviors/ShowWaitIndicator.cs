using Autofac;
using Autofac.Core;
using DevExpress.Mvvm;
using DevExpress.Xpf.Core;
using Microsoft.Practices.ServiceLocation;
using Prism;
using Prism.Interactivity.InteractionRequest;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace Profibiz.PracticeManager.Infrastructure
{
	[ImplementPropertyChanged]
	public class ShowWaitIndicator
	{
		public enum Mode { Load, Save, Custom }
		public bool IsEnabled { get; set; } = true;

		public void Show(Mode mode = Mode.Load, String text = "")
		{
			if (!IsEnabled) return;

			NLog.stack();
			ShowText = 
				mode == Mode.Load ? "Loading..." : 
				mode == Mode.Save ? "Saving..." : 
				text;
			IsShow = true;
		}


		public void Hide()
		{
			if (!IsEnabled) return;

			IsShow = false;
		}

		public bool IsShow { get; set; }
		public string ShowText { get; set; }
	}
}
