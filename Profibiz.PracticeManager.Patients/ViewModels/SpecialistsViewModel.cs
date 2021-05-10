using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.POCO;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using DevExpress.DevAV.Common;
using System.Collections.ObjectModel;
using Prism.Interactivity.InteractionRequest;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using AutoMapper;
using Prism.Regions;
using Autofac;
using System.Collections.Specialized;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class SpecialistsViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		#endregion
		public virtual ObservableCollection<ServiceProvider> Entities { get; set; }
		public virtual ServiceProvider SelectedEntity { get; set; }
		public virtual string CurrentView { get; set; } = "TableView";
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();


		public SpecialistsViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			OpenParmProfessionalAssociationRowId = QueryHelper.ParseGuid(OpenParms["ProfessionalAssociationRowId"]);
			LoadData();
			MessengerHelper.Register<MsgRowChange<ServiceProvider>>(this, OnMsgRowChange);
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;
		Guid? OpenParmProfessionalAssociationRowId;



		async void LoadData()
		{
			ShowWaitIndicator.Show();
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetServiceProviderList(OpenParmQuery));
			Entities = rows.ToObservableCollection();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChange(MsgRowChange<ServiceProvider> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.UpdateEntities(this, Entities, msg.Row, msg.RowAction, (a,b) => a.RowId == b.RowId, () => SelectedEntity);
			});
		}

		public void Delete(ServiceProvider row)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Service Provider"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteServiceProvider(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;
					Entities.Remove(row);
				}
			});
		}
		public bool CanDelete(ServiceProvider row) => (row != null);

		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public void Edit(ServiceProvider row)
		{
			AddEdit(row);
		}
		public bool CanEdit(ServiceProvider row) => (row != null);

		public void New()
		{
			AddEdit(null);
		}

		void AddEdit(ServiceProvider row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneSpecialistView,
				Param = new OneSpecialistViewModel.OpenParams
				{
					IsNew = (row == null),
					RowId = (row == null ? default(Guid) : row.RowId),
				},
			});
		}


		public void ChangeView(string newCurrentView)
		{
			CurrentView = newCurrentView;
		}
		public bool CanChangeView(string newCurrentView)
		{
			return (CurrentView != newCurrentView);
		}
	}
}	
