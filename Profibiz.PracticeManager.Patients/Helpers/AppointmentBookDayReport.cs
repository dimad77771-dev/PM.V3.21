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
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIO;
using Syncfusion.DocToPDFConverter;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public class AppointmentBookDayReport
	{
		public DateTime AppointmentDate { get; set; }
		public AppointmentBook AppointmentBook { get; set; }
		public ServiceProvider ServiceProvider { get; set; }
		public Appointment[] Appointments { get; set; }

		WordDocument document;
		int StartHH = 7;
		int FinishHH = 18;


		public void Run()
		{
			var file = BuildFileFromTemplate();
			document = new WordDocument(file, FormatType.Docx);

			document.ReplaceField("{{AppointmentBook}}", AppointmentBook.Name);
			document.ReplaceField("{{ServiceProvider}}", ServiceProvider.FullNameWithTitle);
			document.ReplaceField("{{AppointmentDate_Full}}", AppointmentDate.ToString("MMMM d, yyyy"));
			document.ReplaceField("{{AppointmentDate_Day}}", AppointmentDate.ToString("d"));
			document.ReplaceField("{{AppointmentDate_DayWeek_1}}", AppointmentDate.ToString("dddd"));
			document.ReplaceField("{{AppointmentDate_DayWeek_2}}", AppointmentDate.ToString("dddd"));

			AppointmentsWrite();

			var converter = new DocToPDFConverter();
			var pdfDocument = converter.ConvertToPDF(document);
			var pdfFilename = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".pdf");
			pdfDocument.Save(pdfFilename);

			document.Close();

			OpenPdf(pdfFilename);
		}

		void AppointmentsWrite()
		{
			var table = FindTable();
			var startRow = 5;
			var tcolumn = 1;
			for(int hh = StartHH; hh <= FinishHH; hh++)
			{
				var appointments = Appointments.Where(q => GetAppointmentHourPart(q).Item1 == hh).ToArray();
				for (int hpart = 0; hpart <= 1; hpart++)
				{
					var trow = startRow + 2 * (hh - StartHH) + hpart;
					var cell = table.Rows[trow].Cells[tcolumn];
					var paragraph = cell.Paragraphs[0];

					var happointments = appointments.Where(q => GetAppointmentHourPart(q).Item2 == hpart).OrderBy(q => q.Start).ToArray();
					var text = string.Join("\n", happointments.Select(q => GetAppointmentText(q)));

					//text = "hh=" + hh + " / " + hpart;

					paragraph.Text = text;
				}
			}
		}

		string GetAppointmentText(Appointment appointment)
		{
			var txt = appointment.Start.ToString("t") + " - " + appointment.Finish.ToString("t") + " " + appointment.Patient.FullName 
				+ " (" + appointment.MedicalServiceName + ")";
			return txt;
		}

		(int,int) GetAppointmentHourPart(Appointment appointment)
		{
			var hh = appointment.Start.Hour;
			var hpart = appointment.Start.Minute < 30 ? 0 : 1;
			if (hh < StartHH)
			{
				hh = StartHH;
				hpart = 0;
			}
			else if (hh > FinishHH)
			{
				hh = FinishHH;
				hpart = 1;
			}

			return (hh, hpart);
		}

		WTable FindTable()
		{
			return document.GetAllTables().Single(q => q.Description == "maintable");
		}

		string BuildFileFromTemplate()
		{
			var location = AssemblyHelper.GetMainPath();
			var template = Path.Combine(location, @"Templates\AppointmentBookDayTemplate.docx");
			var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(path);
			var file = Path.Combine(path, "" + ServiceProvider.FullName + " " + AppointmentDate.ToString("yyyy-MM-dd") + ".docx");
			File.Copy(template, file);
			return file;
		}

		void OpenPdf(string pdfFilename)
		{
			var process = new System.Diagnostics.Process();
			process.StartInfo.FileName = pdfFilename;
			process.Start();
		}

	}
}	
