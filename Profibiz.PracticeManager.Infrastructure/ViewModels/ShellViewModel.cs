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

namespace Profibiz.PracticeManager.Infrastructure
{
	[POCOViewModel]
	public class ShellViewModel : ViewModelBase
	{
		public static ShellViewModel Instance { get; set; }
		public ShellViewModel() : base()
		{
			Instance = this;
		}

		public ContentControl ToolbarRegionContentControl { get; set; }
		public virtual Boolean ShowLeftNavigationPanelRegion { get; set; }
		public virtual Boolean IsSplashScreenShown { get; set; }
	}
}	
