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
using Microsoft.Practices.ServiceLocation;
using System.ComponentModel;
using System.Collections;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class EmailSendRunViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		#endregion
		public virtual EmailSend Entity { get; set; }
		public virtual ObservableCollection<EmailSendRecipient> Recipients { get; set; }
		public virtual OpenParams OpenParam { get; set; }
		public virtual String WindowTitle { get; set; }
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
	


		public EmailSendRunViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<EmailSendRecipient>>(this, OnMsgRowChange);
		}

		
		public async void OnOpen(OpenParams param)
		{
			OpenParam = param;
			await LoadData();
		}

		bool Validate()
		{
			return true;
		}


		public void Submit()
		{
			DispatcherUIHelper.Run(async () =>
			{
				//validate
				if (!Validate())
				{
					return;
				}

				var updateEntity = Entity.GetPocoClone();
				updateEntity.RowId = Guid.NewGuid();
				updateEntity.EmailSendRecipients = Recipients.Where(q => q.IsChecked && !string.IsNullOrEmpty(q.Email)).ToList();
				updateEntity.EmailSendAttachments = OpenParam.Attachments.ToList();

				//save
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Custom, "Sending email...");
				var uret = await businessService.SendEmail(updateEntity);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(MessageBoxService))
				{
					return;
				}

				MessageBoxService.ShowMessage("Email successfully sent", "Send email", MessageButton.OK, MessageIcon.Information);

				var ret = new ReturnParams
				{
					IsSuccess = true,
				};
				OpenParam.TaskSource.SetResult(ret);
				CloseInteractionRequest.Raise(null);
			});
		}
		public bool CanSubmit()
		{
			return true;// SelectedEntities.Any();
		}

		public void Cancel()
		{
			CloseInteractionRequest.Raise(null);
		}
		public void ClosingEvent(CancelEventArgs arg)
		{
			if (OpenParam.TaskSource.Task.Status != TaskStatus.RanToCompletion)
			{
				var ret = new ReturnParams { IsSuccess = false };
				OpenParam.TaskSource.SetResult(ret);
			}
		}


		public void NewRow()
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneMedicalServiceView,
					Param = "IsNew",
				});

			});
		}

		void OnMsgRowChange(MsgRowChange<EmailSendRecipient> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				await LoadData();
			});
		}


		async Task LoadData()
		{
			await Task.Yield();
			ShowWaitIndicator.Show();

			var rows = OpenParam.Recipients;
			if (OpenParam.RunMode == RunModeEnum.Main)
			{
				WindowTitle = "Send Invoice by Email";
			}
			else throw new ArgumentException();

			Entity = new EmailSend
			{
				InvoiceRowId = OpenParam.InvoiceRowId,
			};

			Recipients = rows
				.OrderBy(q => q.RecipientType)
				.ThenBy(q => q.Name)
				.ToObservableCollection();

			ShowWaitIndicator.Hide();
		}


		public class OpenParams
		{
			public RunModeEnum RunMode { get; set; }
			public Guid? InvoiceRowId { get; set; }
			public IEnumerable<EmailSendRecipient> Recipients { get; set; }
			public IEnumerable<EmailSendAttachment> Attachments { get; set; }
			public TaskCompletionSource<ReturnParams> TaskSource { get; set; }
			public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; }
	}
		public enum RunModeEnum { Main }


		public class ReturnParams
		{
			public Boolean IsSuccess { get; set; }
		}

		public static Task<ReturnParams> Run(OpenParams parm)
		{
			var tcs = new TaskCompletionSource<ReturnParams>();
			parm.TaskSource = tcs;

			parm.ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.EmailSendRunView,
				Param = parm,
			});

			return tcs.Task;
		}

	}
}	
