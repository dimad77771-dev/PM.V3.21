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
using System.IO;
using DevExpress.XtraRichEdit.Services;
using DevExpress.Xpf.RichEdit;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class FormDocumentViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public RichEditControlBehaviorManager RichEditConrolManager { get; set; } = new RichEditControlBehaviorManager();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual FormDocument Entity { get; set; }
		public virtual Patient Patient { get; set; }
		public virtual Appointment Appointment { get; set; }
		public bool IsReadOnly { get; set; }
		public bool IsNew { get; set; }
		public bool ShowDeleteButton => !IsNew && !IsReadOnly;

		public FormDocumentViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}


		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			IsReadOnly = OpenParam.IsReadOnly;
			FormDocument entity;
			if (!IsNew)
			{
				entity = (await businessService.GetFormDocumentList("rowid=" + OpenParam.RowId)).First();
			}
			else
			{
				entity = new FormDocument();
				entity.RowId = Guid.NewGuid();
				entity.DocxBytes = new byte[0];
				entity.TemplatePath = OpenParam.TemplatePath;
				entity.TemplateName = OpenParam.TemplateName;
				entity.AppointmentRowId = OpenParam.AppointmentRowId;
				entity.PatientRowId = OpenParam.PatientRowId;
			}
			Entity = entity;
			RichEditConrolManager.Control.DocumentLoaded += Control_DocumentLoaded;
			RichEditConrolManager.ReadOnly = IsReadOnly;
			ResetHasChange();

			if (Entity.AppointmentRowId != null)
			{
				Appointment = (await businessService.GetAppointmentList(rowId: Entity.AppointmentRowId.Value)).FirstOrDefault();
			}

			var patientRowId = Entity.PatientRowId ?? Appointment?.PatientRowId;
			if (patientRowId != null)
			{
				Patient = await businessService.GetPatient(patientRowId.Value);
			}
			

			if (IsNew)
			{
				LoadFromTemplate();
			}


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		public void PasteApply(string arg)
		{
			var oldClipboardText = System.Windows.Clipboard.GetText();

			var document = RichEditConrolManager.Control.Document;
			var text = "";
			byte[] imagebytes = null;

			if (arg == "Patient_Name")
			{
				if (Patient != null)
				{
					text = Patient.FullName ?? "";
				}
			}

			if (arg == "Appointment_Date")
			{
				if (Appointment != null)
				{
					text = Appointment.Start.Date.FormatShortDate();
				}
			}
			if (arg == "Appointment_StartTime")
			{
				if (Appointment != null)
				{
					text = Appointment.Start.FormatHHMM();
				}
			}
			if (arg == "Appointment_FinishTime")
			{
				if (Appointment != null)
				{
					text = Appointment.Finish.FormatHHMM();
				}
			}
			if (arg == "ServiceName")
			{
				if (Appointment != null)
				{
					text = Appointment.MedicalServiceName;
				}
			}

			if (arg == "Patient_Signature")
			{
				if (Patient != null && Patient.Signature != null && Patient.Signature.Length > 0)
				{
					imagebytes = Patient.Signature;
				}
			}


			if (!string.IsNullOrEmpty(text) || imagebytes != null)
			{
				if (!string.IsNullOrEmpty(text))
				{
					System.Windows.Clipboard.SetText(text);
				}
				else if (imagebytes != null)
				{
					using (var ms = new MemoryStream(imagebytes))
					{
						var bitmapImage = new BitmapImage();
						bitmapImage.BeginInit();
						bitmapImage.StreamSource = ms;
						bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
						bitmapImage.EndInit();
						System.Windows.Clipboard.SetImage(bitmapImage);
					}
				}


				try
				{
					document.Paste();
				}
				catch (Exception) { }

				if (!string.IsNullOrEmpty(oldClipboardText))
				{
					System.Windows.Clipboard.SetText(oldClipboardText);
				}
			}
		}


		private void Control_DocumentLoaded(object sender, EventArgs e)
		{
			//var document = RichEditConrolManager.Document;
			//var rangePermissions = document.BeginUpdateRangePermissions();
			//document.CancelUpdateRangePermissions(rangePermissions);

			//if (IsNew)
			//{
			//	foreach (var range in rangePermissions.Select(q => q.Range))
			//	{
			//		document.Delete(range);
			//	}
			//}

			//var gg = document.Sections;
			//var section = document.Sections[0];
			//var footer = section.BeginUpdateFooter();
			//var paragraphs = footer.Paragraphs;
			//var paragraph = paragraphs[0];
			//footer.Replace(paragraph.Range, "TEST TEXT 123456");
			//section.EndUpdateFooter(footer);
			////paragraph.Range.
		}


		//public UserService userService = new UserService();
		//public class UserService : IUserListService
		//{
		//	readonly List<string> users = new List<string>();

		//	public List<string> Users { get { return users; } }

		//	IList<string> IUserListService.GetUsers()
		//	{
		//		return Users;
		//	}
		//	public void Update(List<String> userList)
		//	{
		//		this.users.Clear();
		//		this.users.AddRange(userList);
		//	}
		//}

		void LoadFromTemplate()
		{
			var location = FormDocumentHelper.GetBaseTemplateFolderPath();
			var file = Path.Combine(location, Entity.TemplatePath);
			var bytes = File.ReadAllBytes(file);
			Entity.DocxBytes = bytes;
		}




		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();


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

			//updateEntity
			var updateEntity = Entity.GetPocoClone();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await businessService.PutFormDocument(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

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
			return (IsNew || RichEditConrolManager.Modified);
		}

		void ResetHasChange()
		{
			RichEditConrolManager.Modified = false;
			//Entity.IsChanged = false;
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


		public void Delete()
		{
			//var document = RichEditConrolManager.Document;
			//var gg = document.Sections;
			//var section = document.Sections[0];
			//foreach (var paragraph in section.Paragraphs)
			//{
			//	var text = document.GetText(paragraph.Range);
			//	var b = text;
			//}

			//var table = document.Tables[0];
			//var trow = table.Rows.InsertBefore(1);
			//var trow3 = table.Rows[3];
			

			//return;

			DispatcherUIHelper.Run(async () =>
			{
				var msg = "Do you want to delete this form?";
				var ret = MessageBoxService.ShowMessage(msg, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeleteFormDocument(Entity.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(MessageBoxService)) return;

					MessengerHelper.SendMsgRowChange(Entity, RowAction.Delete);
					CloseInteractionRequest.Raise(null);
				}
			});
		}






		public class OpenParams
		{
			public bool IsNew { get; set; }
			public Guid RowId { get; set; }
			public String TemplatePath { get; set; }
			public String TemplateName { get; set; }
			public bool IsReadOnly { get; set; }
			public Guid? AppointmentRowId { get; set; }
			public Guid? PatientRowId { get; set; }
		}


	}

}
