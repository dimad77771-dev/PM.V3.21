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

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class PatientNoteStatusesViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public virtual ObservableCollection<PatientNoteStatus> Entities { get; set; }
		public virtual PatientNoteStatus SelectedEntity { get; set; }




		public PatientNoteStatusesViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			OpenParmPatientNoteStatusRowId = QueryHelper.ParseGuid(OpenParms["PatientNoteStatusRowId"]);
			LoadData();
			MessengerHelper.Register<MsgRowChange<PatientNoteStatus>>(this, OnMsgRowChange);
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;
		Guid? OpenParmPatientNoteStatusRowId;



		async void LoadData()
		{
			ShowWaitIndicator.Show();
			await lookupsBusinessService.UpdateAllLookups();
			Entities = LookupDataProvider.Instance.PatientNoteStatuses.OrderBy(q => q.DisplayOrder).ToObservableCollection();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChange(MsgRowChange<PatientNoteStatus> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.UpdateEntities(this, Entities, msg.Row, msg.RowAction, (a, b) => a.RowId == b.RowId, () => SelectedEntity);
			});
		}


		public async void Delete()
		{
			var row = SelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Appointment Status"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await lookupsBusinessService.DeletePatientNoteStatus(row);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;
				Entities.Remove(row);
			}
		}
		public bool CanDelete() => (SelectedEntity != null);


		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public void Edit()
		{
			AddEdit(SelectedEntity);
		}
		public bool CanEdit() => (SelectedEntity != null);

		public void New()
		{
			AddEdit(null);
		}

		void AddEdit(PatientNoteStatus row)
		{
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OnePatientNoteStatusView,
				Param = new OnePatientNoteStatusViewModel.OpenParams { IsNew = (row == null), RowId = (row == null ? default(Guid) : row.RowId) },
			});
		}
	}
}
