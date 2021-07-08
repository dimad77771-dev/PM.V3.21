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
using DevExpress.Xpf.Grid;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using DevExpress.Xpf.RichEdit;
using System.IO;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using DevExpress.XtraRichEdit.Printing;
using System.Windows.Controls;


namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public static class FormDocumentHelper
	{
		public static async Task<TemplateNameModel> PickTemplateName(Appointment appointment, IMessageBoxService messageBoxService, InteractionRequest<ShowDXWindowsActionParam> showDXWindowsInteractionRequest)
		{
			var isPatientMode = (appointment.RowId == default(Guid));
			var error = "";
			//var path = "";
			var wh_template = default(Func<Template, bool>);

			if (isPatientMode)
			{
				wh_template = (q => q.IsFormPatient);
				//var templateFolder = "Patient";
				//path = GetTemplateFolderPath(templateFolder);
				//if (!Directory.Exists(path))
				//{
				//	error = "Directory \"" + path + "\" not found";
				//}
			}
			else
			{
				var medicalServicesOrSupplyRowId = appointment.MedicalServicesOrSupplyRowId;
				if (medicalServicesOrSupplyRowId == null)
				{
					error = "Service not specified";
				}
				else
				{
					var medicalService = LookupDataProvider.Instance.MedicalServices.Single(q => q.RowId == medicalServicesOrSupplyRowId);
					wh_template = (q => q.IsFormAppointment && q.CategoryRowId == medicalService.CategoryRowId);
					//var templateFolder = serviceProvider.TemplateFolder;
					//if (string.IsNullOrEmpty(templateFolder))
					//{
					//	error = "Template Folder for service \"" + serviceProvider + "\" not specified";
					//}
					//else
					//{
					//	path = GetTemplateFolderPath(templateFolder);
					//	if (!Directory.Exists(path))
					//	{
					//		error = "Directory \"" + path + "\" not found";
					//	}
					//}
				}
			}

			if (!string.IsNullOrEmpty(error))
			{
				messageBoxService.ShowError(error);
				return null;
			}



			//var path0 = GetBaseTemplateFolderPath();
			//var files = Directory.GetFiles(path, "*.docx", SearchOption.AllDirectories);
			//var templateFiles = files.Select(q => new TemplateNameModel
			//{
			//	TemplateName = Path.GetFileNameWithoutExtension(q),
			//	TemplatePath = q.Substring(path0.Length + 1),
			//}).OrderBy(q => q.TemplateName).ToArray();

			var templateFiles = LookupDataProvider.Instance.Templates.Where(wh_template).Select(q => new TemplateNameModel
			{
				TemplateName = q.Name,
				TemplateRowId = q.RowId,
			}).OrderBy(q => q.TemplateName).ToArray();

			var result = await PickTemplateNameViewModel.Pick(new PickTemplateNameViewModel.OpenParams
			{
				TemplateFiles = templateFiles,
				ShowDXWindowsInteractionRequest = showDXWindowsInteractionRequest,
			});
			if (!result.IsSuccess)
			{
				return null;
			}

			return result.Row;
		}

		public static string GetTemplateFolderPath(string templateFolder)
		{
			var path = Path.Combine(GetBaseTemplateFolderPath(), "Forms", templateFolder);
			return path;
		}

		public static string GetBaseTemplateFolderPath()
		{
			var location = AssemblyHelper.GetMainPath();
			var path = Path.Combine(location, "Templates");
			return path;
		}

		public static async Task<byte[]> GetTemplateDocumentBytesByCode(string code, bool isPaid, IMessageBoxService MessageBoxService)
		{
			Template template = null;
			if (isPaid)
			{
				var code2 = code + "_paid";
				template = LookupDataProvider.Instance.Templates.FirstOrDefault(q => q.IsTemplate && q.Code.ToLower() == code2.ToLower());
			}
			if (template == null)
			{
				template = LookupDataProvider.Instance.Templates.FirstOrDefault(q => q.IsTemplate && q.Code.ToLower() == code.ToLower());
			}
			if (template == null)
			{
				MessageBoxService.ShowError($@"Template ""{code}"" not found");
				return null;
			}
			var lookupsBusinessService = ServiceHelper.GetInstance<ILookupsBusinessService>();
			var bytes = (await lookupsBusinessService.GetTemplateDocumentBytes(template.RowId))?.DocumentBytes;
			if (bytes == null)
			{
				MessageBoxService.ShowError($@"Template ""{code}"" not found or is empty");
				return null;
			}
			return bytes;
		}
	}
}	

