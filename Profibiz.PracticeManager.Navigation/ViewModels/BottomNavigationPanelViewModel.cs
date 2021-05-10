using Autofac;
using DevExpress.DevAV.ViewModels;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Prism.Commands;
using Prism.Interactivity.InteractionRequest;
using Prism.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class BottomNavigationPanelViewModel : ViewModelBase
	{
		public virtual BehaviorSelectionPreventResolver SelectionPreventResolver { get; set; } = new BehaviorSelectionPreventResolver();

		public BottomNavigationPanelViewModel(IContainer container) : base()
		{
			SelectionPreventResolver.OnResolve += (s, e) =>
			{
				var module = (ProfibizModuleDescription)e.NewModel;
				if (module.IsOpenModalWindow)
				{
					e.Cancel = true;
				}
			};
		}


		
	}
}
