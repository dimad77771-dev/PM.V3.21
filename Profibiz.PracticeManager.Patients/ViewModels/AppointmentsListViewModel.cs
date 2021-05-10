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
	public class AppointmentsListViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		#endregion
		public virtual ObservableCollection<Appointment> Entities { get; set; }
		public virtual Appointment SelectedEntity { get; set; }
		public virtual ObservableCollection<AppointmentBook> AllAppointmentBooks { get; set; }
		public virtual AppointmentBook SelectedAppointmentBook { get; set; }


		public AppointmentsListViewModel() : base()
		{
			var aa = 999;
		}

		public void OnOpen(string parm)
		{
			OpenParmQuery = parm;
			OpenParms = QueryHelper.ParseString(parm);
			LoadData();
			MessengerHelper.Register<MsgRowChange<Appointment>>(this, OnMsgRowChange);
		}
		String OpenParmQuery;
		NameValueCollection OpenParms;



		async void LoadData()
		{
			ShowWaitIndicator.Show();
			var task = businessService.GetAppointmentList(appointmentBookRowId: SelectedAppointmentBook.RowId);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			Entities = rows.ToObservableCollection();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		void OnMsgRowChange(MsgRowChange<Appointment> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.UpdateEntities(this, Entities, msg.Row, msg.RowAction, (a,b) => a.RowId == b.RowId, () => SelectedEntity);
			});
		}

		public async Task Delete(Appointment row)
		{
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Appointment"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.DeleteAppointment(row.RowId);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(messageBoxService)) return;
				Entities.Remove(row);
			}
		}

		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public void Edit(Appointment row)
		{
			AddEdit(row);
		}

		public void New()
		{
			AddEdit(null);
		}

		void AddEdit(Appointment row)
		{
			//ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			//{
			//	ViewCode = ViewCodes.OneSpecialistView,
			//	Param = new OneSpecialistViewModel.OpenParams
			//	{
			//		IsNew = (row == null),
			//		RowId = (row == null ? default(Guid) : row.RowId),
			//	},
			//});
		}

	}
}	
