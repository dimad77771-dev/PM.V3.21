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

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class OneInsuranceProviderViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual InsuranceProvider Entity { get; set; }
		public bool IsNew { get; set; }

		public InsuranceProvidersViewGroupsViewModel GroupsViewModel { get; set; }


		public OneInsuranceProviderViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			LoadData();
		}

		async void LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			InsuranceProvider entity;
			if (!IsNew)
			{
				entity = LookupDataProvider.FindInsuranceProvider(OpenParam.RowId).GetPocoClone(); 
			}
			else
			{
				entity = new InsuranceProvider();
				entity.RowId = Guid.NewGuid();
			}
			Entity = entity;
			GroupsViewModel = ViewModelSource.Create(() => new InsuranceProvidersViewGroupsViewModel(Entity));
			ResetHasChange();


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}




		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.CompanyName, "Company Name", errors);

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		async Task<bool> SaveCore(bool andClose)
		{
			if (IsGroupPage)
			{
				//GroupsViewModel
				var ret2 = await GroupsViewModel.SaveCore(andClose);
				if (!ret2) return false;
			}
			else
			{
				//validate
				if (!Validate())
				{
					return false;
				}

				//updateEntity
				var updateEntity = Entity.GetPocoClone();
				var updateEntities = new[] { updateEntity };

				//save
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await lookupsBusinessService.PutInsuranceProviders(updateEntities);
				await lookupsBusinessService.UpdateAllLookups();
				ShowWaitIndicator.Hide();
				if (!uret.Validate(MessageBoxService))
				{
					return false;
				}

				MessengerHelper.Send(new MsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.InsuranceProviders));
				MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
				IsNew = false;
				ResetHasChange();
			}


			//close
			if (andClose)
			{
				CloseCore(force: true);
			}

			return true;
		}

		public virtual int SelectedIndex { get; set; }
		public PageEnum CurrentPage => GetPage(SelectedIndex);
		public PageEnum GetPage(int index) => EnumFunc.GetValues<PageEnum>()[index];
		public bool IsGroupPage => (CurrentPage == PageEnum.Groups);
		public enum PageEnum { Property, Groups }


		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			if (IsGroupPage)
			{
				return GroupsViewModel.HasChange();
			}
			else
			{
				return (IsNew || Entity.IsChanged);
			}
		}

		void ResetHasChange()
		{
			if (IsGroupPage)
			{
				GroupsViewModel.ResetHasChange();
			}
			else
			{
				Entity.IsChanged = false;
			}
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



		public async void PageSelectionChanging(TabControlSelectionChangingEventArgs arg)
		{
			arg.Cancel = true;
			if (!await OnClose(showOKCancel: true)) return;

			if (GetPage(arg.NewSelectedIndex) == PageEnum.Groups)
			{
				ShowWaitIndicator.Show();
				await GroupsViewModel.LoadData();
				ShowWaitIndicator.Hide();
			}
			SelectedIndex = arg.NewSelectedIndex;
		}






		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
		}


	}

}
