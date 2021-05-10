using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Infrastructure;
using Prism.Interactivity.InteractionRequest;
using System.Collections.ObjectModel;
using DevExpress.DevAV.Common;
using DevExpress.Mvvm.POCO;
using System.ComponentModel;
using AutoMapper;
using Newtonsoft.Json;
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using System.ComponentModel.DataAnnotations;
using Microsoft.Practices.ServiceLocation;
using DevExpress.Xpf.Core;
using System.Diagnostics;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class ChargeoutWindowViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual ChargeoutOneViewModel OneModel { get; set; } = ChargeoutOneViewModel.Create(isWindowMode: true);


		public ChargeoutWindowViewModel() : base()
		{
		}

		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			else if (arg is String)
			{
				if ((string)arg == "IsNew")
				{
					OpenParam = new OpenParams { IsNew = true };
				}
			}
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		async Task LoadData()
		{
			await OneModel.LoadData(
				isNew: false,
				rowId: OpenParam.RowId,
				readOnly: OpenParam.ReadOnly,
				selectPaychargeRowId: OpenParam.SelectPaychargeRowId,
				newChargeout: OpenParam.NewChargeout,
				closeInteractionRequest: CloseInteractionRequest);
		}
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();

		public async void ClosingEvent(CancelEventArgs arg)
		{
			await OneModel.ClosingEvent(arg);
		}


		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public bool ReadOnly { get; set; }
			public Chargeout NewChargeout { get; set; }

			public Guid? SelectPaychargeRowId { get; set; }
		}


	}

}
