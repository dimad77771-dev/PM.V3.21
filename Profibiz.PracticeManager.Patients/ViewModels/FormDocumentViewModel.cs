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
using DevExpress.XtraRichEdit.API.Native;
using System.Text.RegularExpressions;

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
		public ServiceProvider ServiceProvider => LookupDataProvider.FindServiceProvider(CurrentAppointment?.ServiceProviderRowId);
		public virtual ObservableCollection<Appointment> AllAppointments { get; set; } = new ObservableCollection<Appointment>();
		public virtual Appointment SelectedAppointment { get; set; }

		public virtual Appointment CurrentAppointment => Appointment ?? SelectedAppointment;
		public bool IsCategoryMode => Entity != null && Entity.CategoryRowId != null;


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
				entity.TemplatePath = OpenParam.TemplateName;
				entity.TemplateName = OpenParam.TemplateName;
				entity.PatientRowId = OpenParam.PatientRowId;
				entity.AppointmentRowId = OpenParam.AppointmentRowId;
				entity.CategoryRowId = OpenParam.CategoryRowId;
				entity.ServiceProviderRowId = OpenParam.ServiceProviderRowId;
			}
			Entity = entity;
			ReadOnlySetup();
			RichEditConrolManager.Control.DocumentLoaded += Control_DocumentLoaded;
			RichEditConrolManager.ReadOnly = IsReadOnly;
			ResetHasChange();

			if (Entity.AppointmentRowId != null)
			{
				Appointment = (await businessService.GetAppointmentList(rowId: Entity.AppointmentRowId.Value)).FirstOrDefault();
			}

			var patientRowId = Entity.PatientRowId ?? CurrentAppointment?.PatientRowId;
			if (patientRowId != null)
			{
				Patient = await businessService.GetPatient(patientRowId.Value);
			}

			if (IsCategoryMode)
			{
				var appointments = (await businessService.GetAppointmentList(patientRowId: Entity.PatientRowId));
				AllAppointments = appointments
					.Where(q => q.ServiceProviderRowId == Entity.ServiceProviderRowId 
										&& LookupDataProvider.FindMedicalService(q.MedicalServicesOrSupplyRowId)?.CategoryRowId == Entity.CategoryRowId)
					.OrderByDescending(q => q.Start)
					.ToObservableCollection();
				SelectedAppointment = AllAppointments.FirstOrDefault();
			}
			

			if (IsNew)
			{
				await LoadFromTemplate();
			}


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();


			var dd = RichEditConrolManager.Control;
			dd.MouseDoubleClick += Dd_MouseDoubleClick;
		}

		void ReadOnlySetup()
		{
			IsReadOnly = OpenParam.IsReadOnly;
			if (!IsNew && Entity.CreatedByUserRowId != null && UserManager.UserRowId != Entity.CreatedByUserRowId)
			{
				IsReadOnly = true;
			}
		}

		const int CHECKBOX_ON = 61523;
		const int CHECKBOX_OFF = 61603;
		const string CHECKBOX_FONTNAME = "Wingdings 2";
		private void Dd_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
		{
			if (IsReadOnly) return;

			var doc = RichEditConrolManager.Document;
			if (doc.Selection == null) return;

			var range = doc.CreateRange(doc.Selection.Start, 1);
			var text = doc.GetText(range);
			
			var characterProperties = doc.BeginUpdateCharacters(range);
			var fontname = characterProperties.FontName;
			doc.EndUpdateCharacters(characterProperties);

			if (fontname == CHECKBOX_FONTNAME)
			{
				var ntext = "";
				var nvalue = (bool?)null;
				if (text == Convert.ToChar(CHECKBOX_OFF).ToString())
				{
					ntext = Convert.ToChar(CHECKBOX_ON).ToString();
					nvalue = true;
				}
				else if (text == Convert.ToChar(CHECKBOX_ON).ToString())
				{
					ntext = Convert.ToChar(CHECKBOX_OFF).ToString();
					nvalue = false;
				}

				if (!string.IsNullOrEmpty(ntext))
				{ 
					doc.Delete(range);
					var nrange = doc.InsertText(range.Start, ntext);
					var ncharacterProperties = doc.BeginUpdateCharacters(nrange);
					ncharacterProperties.FontName = CHECKBOX_FONTNAME;
					doc.EndUpdateCharacters(ncharacterProperties);

					CheckboxYesNo(doc, nrange, nvalue.Value);

					if (doc.Selection.Length > 0)
					{
						doc.Selection = doc.CreateRange(doc.Selection.Start, 0);
					}
				}
			}
		}

		void CheckboxYesNo(Document doc, DocumentRange brange, bool bvalue)
		{
			var pos = brange.Start.ToInt();
			var delta = 30;
			var pos1 = Math.Max(pos - delta, 0);
			var pos2 = pos + delta;

			var charreg = Convert.ToChar(CHECKBOX_ON) + "|" + Convert.ToChar(CHECKBOX_OFF);
			var variants = new[]
			{
				(2, $@"\syes\s*$", $@"^\s*no\s*({charreg})"),
				(1, $@"\syes\s*({charreg})\s*no\s*$", ""),

				(2, "", $@"^\s*yes\s*({charreg})\s*no"),
				(1, $@"({charreg})\s*yes\s*$", $@"^\s*no"),
			};

			foreach (var variant in variants)
			{
				var numregex = variant.Item1;
				var reg1 = variant.Item2;
				var reg2 = variant.Item3;
				var range1 = doc.CreateRange(pos1, pos - pos1);
				var text1 = doc.GetText(range1);
				var regex1 = new Regex(reg1, RegexOptions.IgnoreCase);
				var good1 = true;
				if (!string.IsNullOrEmpty(reg1))
				{
					good1 = regex1.IsMatch(" " + text1);
				}

				var range2 = doc.CreateRange(pos + 1, pos2 - pos);
				var text2 = doc.GetText(range2);
				var regex2 = new Regex(reg2, RegexOptions.IgnoreCase);
				var good2 = true;
				if (!string.IsNullOrEmpty(reg2))
				{
					good2 = regex2.IsMatch(text2);
				}
				

				if (good1 && good2)
				{
					var direction = numregex == 1 ? -1 : 1;
					var curpos = numregex == 1 ? range1.End.ToInt() - 1 : range2.Start.ToInt();
					DocumentRange nrange;
					string text;
					while (true)
					{
						nrange = doc.CreateRange(curpos, 1);
						text = doc.GetText(nrange);
						if (text == Convert.ToChar(CHECKBOX_ON).ToString() || text == Convert.ToChar(CHECKBOX_OFF).ToString())
						{
							break;
						}
						curpos += direction;
					}

					bool exvalue;
					if (text == Convert.ToChar(CHECKBOX_OFF).ToString())
					{
						exvalue = false;
					}
					else if (text == Convert.ToChar(CHECKBOX_ON).ToString())
					{
						exvalue = true;
					}
					else throw new ArgumentException();

					var newvalue = false;

					if (newvalue != exvalue)
					{
						doc.Delete(nrange);
						var ntext = Convert.ToChar(newvalue ? CHECKBOX_ON : CHECKBOX_OFF).ToString();
						var irange = doc.InsertText(nrange.Start, ntext);
						var ncharacterProperties = doc.BeginUpdateCharacters(irange);
						ncharacterProperties.FontName = CHECKBOX_FONTNAME;
						doc.EndUpdateCharacters(ncharacterProperties);
					}

					break;
				}
			}
		}

		void AllFieldReplace()
		{
			var fields = new[] 
			{
				"Patient_Name",
				"Patient_BirthDate",
				"Patient_FamilyDoctor",
				"Patient_Sex",
				"Patient_MobileNumber",
				"Patient_Address",
				"Appointment_Date",
				"Appointment_StartTime",
				"Appointment_FinishTime",
				"ServiceName",
				"ServiceProvider_FullName",
				"ServiceProvider_Position",
				"ServiceProvider_Qualifications",
				"ServiceProvider_FooterText",
				"ServiceName",
				"Patient_Signature",
				"ServiceProvider_Signature",
				"CurrentDateTime",
				"Patient_Occupation",
				"Patient_Age",
				"Service_CategoryName",
			};

			foreach (var field in fields)
			{
				FieldProc(field, isPaste: false);
			}
		}

		public void PasteApply(string arg)
		{
			FieldProc(arg, isPaste: true);
		}

		public void PasteCheckbox(string arg)
		{
			var doc = RichEditConrolManager.Document;
			if (doc.Selection == null) return;

			var ntext = "";
			if (arg == "1")
			{
				ntext = Convert.ToChar(CHECKBOX_ON).ToString();
			}
			else if (arg == "0")
			{
				ntext = Convert.ToChar(CHECKBOX_OFF).ToString();
			}

			if (!string.IsNullOrEmpty(ntext))
			{
				try
				{
					var nrange = doc.InsertText(doc.Selection.Start, ntext);
					var ncharacterProperties = doc.BeginUpdateCharacters(nrange);
					ncharacterProperties.FontName = CHECKBOX_FONTNAME;
					doc.EndUpdateCharacters(ncharacterProperties);
				}
				catch(Exception) { }
			}
		}

		void FieldProc(string arg, bool isPaste)
		{
			var oldClipboardText = System.Windows.Clipboard.GetText();

			var document = RichEditConrolManager.Control.Document;
			var text = "";
			byte[] imagebytes = null;

			if (arg == "CurrentDateTime")
			{
				text = DateTime.Now.ToString("yyyy-MM-dd h:mm tt");
			}
			else if (arg == "Patient_Name")
			{
				if (Patient != null)
				{
					text = Patient.FullName ?? "";
				}
			}
			else if (arg == "Patient_BirthDate")
			{
				if (Patient != null)
				{
					text = Patient.BirthDate.FormatYYYYMMDD();
				}
			}
			else if (arg == "Patient_Age")
			{
				if (Patient != null && Patient.BirthDate != null)
				{
					text = Patient.BirthDate.Value.GetAge().ToString();
				}
			}
			else if (arg == "Patient_FamilyDoctor")
			{
				if (Patient != null)
				{
					text = Patient.FamilyDoctor;
				}
			}
			else if (arg == "Patient_Sex")
			{
				if (Patient != null)
				{
					text = Patient.Sex;
				}
			}
			else if (arg == "Patient_MobileNumber")
			{
				if (Patient != null)
				{
					text = Patient.MobileNumber;
				}
			}
			else if (arg == "Patient_Address")
			{
				if (Patient != null)
				{
					text = Patient.GetAddress();
				}
			}
			else if (arg == "Patient_Occupation")
			{
				if (Patient != null)
				{
					text = Patient.Occupation;
				}
			}
			else if (arg == "Appointment_Date")
			{
				if (CurrentAppointment != null)
				{
					text = CurrentAppointment.Start.Date.FormatShortDate();
				}
			}
			else if (arg == "Appointment_StartTime")
			{
				if (CurrentAppointment != null)
				{
					text = CurrentAppointment.Start.FormatHHMM();
				}
			}
			else if (arg == "Appointment_FinishTime")
			{
				if (CurrentAppointment != null)
				{
					text = CurrentAppointment.Finish.FormatHHMM();
				}
			}
			else if (arg == "ServiceName")
			{
				if (CurrentAppointment != null)
				{
					text = CurrentAppointment.MedicalServiceName;
				}
			}
			else if (arg == "Service_CategoryName")
			{
				if (CurrentAppointment != null)
				{
					text = LookupDataProvider.FindMedicalService(CurrentAppointment.MedicalServicesOrSupplyRowId)?.CategoryName;
				}
			}
			else if (arg == "ServiceProvider_FullName")
			{
				if (ServiceProvider != null)
				{
					text = ServiceProvider.FullNameWithTitle;
				}
			}
			else if (arg == "ServiceProvider_Position")
			{
				if (ServiceProvider != null)
				{
					text = ServiceProvider.Position;
				}
			}
			else if (arg == "ServiceProvider_Qualifications")
			{
				if (ServiceProvider != null)
				{
					text = ServiceProvider.Qualifications;
				}
			}
			else if (arg == "ServiceProvider_FooterText")
			{
				if (ServiceProvider != null)
				{
					text = ServiceProvider.FooterText;
				}
			}
			else if (arg == "ServiceName")
			{
				if (CurrentAppointment != null)
				{
					text = CurrentAppointment.MedicalServiceName;
				}
			}
			else if (arg == "Patient_Signature")
			{
				if (Patient != null && Patient.Signature != null && Patient.Signature.Length > 0)
				{
					imagebytes = Patient.Signature;
				}
			}
			else if (arg == "ServiceProvider_Signature")
			{
				if (ServiceProvider != null && ServiceProvider.Signature != null && ServiceProvider.Signature.Length > 0)
				{
					imagebytes = ServiceProvider.Signature;
				}
			}



			if (isPaste)
			{
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
			else
			{
				var fieldcode = "{{" + arg + "}}";
				if (imagebytes != null)
				{
					var ranges = document.FindAll(fieldcode, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
					foreach(var range in ranges)
					{
						document.Selection = range;

						using (var ms = new MemoryStream(imagebytes))
						{
							var bitmapImage = new BitmapImage();
							bitmapImage.BeginInit();
							bitmapImage.StreamSource = ms;
							bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
							bitmapImage.EndInit();
							System.Windows.Clipboard.SetImage(bitmapImage);
						}

						document.Paste();
					}
				}
				text = text ?? "";
				document.ReplaceAll(fieldcode, text, DevExpress.XtraRichEdit.API.Native.SearchOptions.None);
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

		async Task LoadFromTemplate()
		{
			var bytes = (await lookupsBusinessService.GetTemplateDocumentBytes(OpenParam.TemplateRowId)).DocumentBytes;
			//var location = FormDocumentHelper.GetBaseTemplateFolderPath();
			//var file = Path.Combine(location, Entity.TemplatePath);
			//var bytes = File.ReadAllBytes(file);

			Entity.DocxBytes = bytes;
			DispatcherUIHelper.Run(async () =>
			{
				await Task.Yield();
				AllFieldReplace();
			});
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
			if (IsReadOnly) return true;

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
			//public String TemplatePath { get; set; }
			public Guid TemplateRowId { get; set; }
			public String TemplateName { get; set; }
			public bool IsReadOnly { get; set; }
			public Guid? AppointmentRowId { get; set; }
			public Guid? PatientRowId { get; set; }
			public Guid? CategoryRowId { get; set; }
			public Guid? ServiceProviderRowId { get; set; }
		}


	}

}
