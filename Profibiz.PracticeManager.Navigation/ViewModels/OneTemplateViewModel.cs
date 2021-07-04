﻿using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
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
using Microsoft.Win32;
using System.IO;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class OneTemplateViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual Template Entity { get; set; }
		public bool IsNew { get; set; }
		public virtual ObservableCollection<Category> AllCategories { get; set; } = LookupDataProvider.Instance.Categories.ToObservableCollection();

		public OneTemplateViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
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
			LoadData();
		}

		async void LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			Template entity;
			if (!IsNew)
			{
				entity = LookupDataProvider.FindTemplate(OpenParam.RowId).GetPocoClone();
			}
			else
			{
				entity = new Template();
				entity.RowId = Guid.NewGuid();
				entity.IsEnabled = true;
			}
			Entity = entity;
			ResetHasChange();


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}


		public void DownloadToFile()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (!await OnClose(showOKCancel: true)) return;

				var ofd = new SaveFileDialog();
				ofd.Title = "Save template file";
				ofd.Filter = "Word files|*.docx";
				if (ofd.ShowDialog() != true)
				{
					return;
				}
				var filename = ofd.FileName;

				ShowWaitIndicator.Show();
				var documentBytes = await lookupsBusinessService.GetTemplateDocumentBytes(Entity.RowId);
				if (documentBytes.DocumentBytes == null)
				{
					documentBytes.DocumentBytes = new byte[0];
				}
				File.WriteAllBytes(filename, documentBytes.DocumentBytes);
				ShowWaitIndicator.Hide();
			});
		}

		public void UploadFromFile()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (!await OnClose(showOKCancel: true)) return;

				var ofd = new OpenFileDialog();
				ofd.Title = "Choose template file";
				ofd.Filter = "Word files|*.docx";
				if (ofd.ShowDialog() != true)
				{
					return;
				}
				var documentBytes = File.ReadAllBytes(ofd.FileName);

				var urow = new TemplateDocumentBytes
				{
					RowId = Entity.RowId,
					DocumentBytes = documentBytes,
				};
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await lookupsBusinessService.PutTemplateDocumentBytes(urow);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(MessageBoxService))
				{
					return;
				}
			});
		}





		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.TemplateType, "Type", errors);
			ValidateHelper.Empty(Entity.Name, "Name", errors);
			ValidateHelper.Empty(Entity.Code, "Code", errors);
			if (Entity.IsTemplate)
			{
				ValidateHelper.Empty(Entity.InvoiceType, "Invoice Type", errors);
			}
			if (Entity.IsForm)
			{
				ValidateHelper.Empty(Entity.FormType, "Form Type", errors);
				if (Entity.IsFormAppointment)
				{
					ValidateHelper.Empty(Entity.CategoryRowId, "Category", errors);
				}
			}

			//ValidateHelper.Empty(Entity.ShortName, "Code", errors);
			//ValidateHelper.Empty(Entity.DisplayOrder, "Display Order", errors);
			//ValidateHelper.Empty(Entity.BackgroundColor, "Background", errors);
			//ValidateHelper.Empty(Entity.ForegroundColor, "Foreground", errors);

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
			//validate
			if (!Validate())
			{
				return false;
			}

			if (Entity.InvoiceType == null)
			{
				Entity.InvoiceType = "";
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			var updateEntities = new[] { updateEntity };

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await lookupsBusinessService.PutTemplates(updateEntities);
			await lookupsBusinessService.UpdateAllLookups();
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.Send(new MsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.Templates));
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
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










		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
		}


	}

}