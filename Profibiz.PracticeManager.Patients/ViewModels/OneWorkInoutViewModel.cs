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
	public class OneWorkInoutViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual WorkInout Entity { get; set; }
		public virtual List<WorkInout> TodayRows { get; set; }
		public bool IsNew { get; set; }
		public bool IsSimpleMode => OpenParam.IsSimpleMode;
		public bool HideSaveAndClose => IsSimpleMode;


		public OneWorkInoutViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
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
			DispatcherUIHelper.Run2(LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			WorkInout entity;
			if (!IsNew)
			{
				entity = await businessService.GetWorkInout(OpenParam.RowId);
			}
			else
			{
				entity = new WorkInout();
				entity.RowId = Guid.NewGuid();
			}
			Entity = entity;
			Entity.DateTimePropsBuild();
			if (IsSimpleMode)
			{
				await LoadDataForSimpleMode();
			}


			ResetHasChange();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		async Task LoadDataForSimpleMode()
		{
			var today = DateTime.Today;
			Entity.StartDate = today;
			var query = "workInoutDateFrom=" + today.ToWebQuery() + "&" + "workInoutDateTo=" + today.ToWebQuery() + "&";
			TodayRows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetWorkInoutList(query));

			(Entity as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (e.PropertyName == nameof(Entity.ServiceProviderRowId))
				{
					OnServiceProviderRowIdChanged();
				}
			};
		}

		void OnServiceProviderRowIdChanged()
		{
			if (Entity.ServiceProviderRowId != null)
			{
				var lastRow = TodayRows.Where(q => q.ServiceProviderRowId == Entity.ServiceProviderRowId).OrderByDescending(q => q.Start).FirstOrDefault();
				var isCheckout = (lastRow != null && lastRow.Finish == null);
				if (isCheckout)
				{
					IsNew = false;
					Entity = lastRow;
					Entity.Finish = DateTime.Now;
					Entity.DateTimePropsBuild();
				}
				else
				{
					Entity.StartTime = DateTime.Now;
					Entity.FinishTime = null;
				}
			}
		}


		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));


		bool DateTimePropsUpdate()
		{
			var errors = Entity.DateTimePropsUpdate();
			if (errors.Count > 0)
			{
				ShowErrors(errors);
				return false;
			}

			return true;
		}

		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.ServiceProviderRowId, "Specialist", errors);
			

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		void ShowErrors(IEnumerable<string> errors)
		{
			var err = string.Join("\n\n", errors.ToArray());
			MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
		}

		async Task<bool> SaveCore(bool andClose)
		{
			if (IsSimpleMode)
			{
				andClose = true;
			}

			if (!DateTimePropsUpdate()) return false;
			if (!Validate()) return false;
			

			//updateEntity
			var updateEntity = Entity.GetPocoClone();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostWorkInout(updateEntity) :
				await businessService.PutWorkInout(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			ShowWaitIndicator.Hide();
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}






		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return (IsNew || Entity.IsChanged);
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
		}



		async public Task<bool> OnClose(bool showOKCancel = false)
		{
			if (HasChange())
			{
				var ret = MessageBoxService.ShowMessage(
					(showOKCancel ? CommonResources.Confirmation_Save_And_Continue : CommonResources.Confirmation_Save),
					CommonResources.Confirmation_Caption,
					(showOKCancel ? MessageButton.OKCancel : MessageButton.YesNoCancel),
					MessageIcon.Question);
				if (ret == MessageResult.Cancel)
				{
					return false;
				}
				else if (ret == MessageResult.No)
				{
					return true;
				}
				else if (ret == MessageResult.Yes || ret == MessageResult.OK)
				{
					return await SaveCore(andClose: false);
				}
				else throw new ArgumentException();
			}
			else
			{
				return true;
			}
		}
		public async void ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			if (!await OnClose())
			{
				arg.Cancel = true;
			}
		}

		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneSupplierView,
				Param = "IsNew",
			});
		});


		public class OpenParams
		{
			public Boolean IsNew { get; set; }
			public Guid RowId { get; set; }
			public Boolean ReadOnly { get; set; }
			public Boolean IsSimpleMode { get; set; }
		}
	}
}
