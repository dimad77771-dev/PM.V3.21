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
using Profibiz.PracticeManager.Navigation.Views;
using System.Windows;
using Microsoft.Win32;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class LoginViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual User Entity { get; set; }
		public bool IsNew { get; set; }
		public bool IsConnect { get; set; }
		public LoginView View { get; set; }


		public LoginViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen()
		{
			OpenParam = new OpenParams { IsNew = true };
			LoadData();
		}

		async void LoadData()
		{
			var entity = new User()
			{
				//Name = "User 1", Password = "12345",
				//Name = "1", Password = "1",
			};

			var currentUserName = (Registry.GetValue(RuntimeHelper.BaseRegKey, RuntimeHelper.CurrentUserNameKey, (object)"") ?? "").ToString();
			if (!String.IsNullOrEmpty(currentUserName))
			{
				entity.Name = currentUserName;
			}

			if (RuntimeHelper.IsMachineD)
			{
				//entity.Name = "1"; entity.Password = "1";
			}



			Entity = entity;
		}

		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.Name, "Name", errors);
			ValidateHelper.Empty(Entity.Password, "Password", errors);

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		async public Task<bool> OnConnect()
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Custom, "Connecting...");
			var username = (Entity.Name ?? "");
			var uret = await lookupsBusinessService.GetLoginInfo(username, Entity.Password);
			ShowWaitIndicator.Hide();
			if (!uret.IsSuccess)
			{
				DXMessageBox.Show(uret.Error, "Login error", MessageBoxButton.OK, MessageBoxImage.Error);
				return false;
			}

			Registry.SetValue(RuntimeHelper.BaseRegKey, RuntimeHelper.CurrentUserNameKey, (object)username);
			UserManager.UserRowId = uret.UserRowId;
			IsConnect = true;
			View.Close();
			return true;
		}









		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
		}


	}

}
