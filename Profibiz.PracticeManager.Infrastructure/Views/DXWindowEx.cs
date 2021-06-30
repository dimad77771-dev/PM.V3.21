using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.Windows.Controls;
using DevExpress.Xpf.Core;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class DXWindowEx : DXWindow
	{
		public static string BusinessName { get; set; }

		public DXWindowEx()
		{
			this.Loaded += (s, e) =>
			{
				if (!string.IsNullOrEmpty(BusinessName))
				{
					this.Title = this.Title + " [" + BusinessName + "]";
				}
			};
		}
	}
}	
