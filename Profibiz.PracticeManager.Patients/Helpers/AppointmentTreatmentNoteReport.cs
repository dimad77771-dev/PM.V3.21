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
	public class AppointmentTreatmentNoteReport
	{
		public Guid RowId { get; set; }

		public AppointmentTreatmentNote Row { get; set; }
		public Appointment Appointment { get; set; }
		public MedicalServicesOrSupply Service { get; set; }
		public ServiceProvider Doctor { get; set; }
		public Patient Patient { get; set; }

		public DateTime AppointmentDate => Appointment.Start;

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

			document.ReplaceField("{{Patient_Name}}", Patient.FullName);
			document.ReplaceField("{{Patient_BirthDate}}", Patient.BirthDate.FormatShortDate());
			document.ReplaceField("{{ServiceName}}", Service.Name);
			document.ReplaceField("{{ServiceProvider_FullName}}", Doctor.FullName);
			document.ReplaceField("{{ServiceProvider_Position}}", Doctor.Position);
			document.ReplaceField("{{Appointment_Date}}", AppointmentDate.FormatShortDate());

			document.ReplaceField("{A1}", yesno(Row.InformedConsentReceived));
			document.ReplaceField("{A2}", yesno(Row.AffectedDailyActivities));
			var hydrotherapy = LookupDataProvider.Instance.HeatIceEnum.SingleOrDefault(q => q.Value == Row.Hydrotherapy)?.Name?.ToUpper();
			document.ReplaceField("{A3}", hydrotherapy);

			for(int n = 1; n <= 10; n++)
			{
				var col = "{B" + (n == 10 ? "0" : n.ToString()) + "}";
				var val = (Row.Subjectiveinfo == n ? "X" : "");
				document.ReplaceField(col, val);
			}

			document.ReplaceField("{C1", check(Row.CervicalSpine));
			document.ReplaceField("{C2", check(Row.ThoracicSpine));
			document.ReplaceField("{C3", check(Row.LumbarSpine));

			document.ReplaceField("{D1", check(Row.AffectedJointsGlenohumeral));
			document.ReplaceField("{D2", check(Row.AffectedJointsElbow));
			document.ReplaceField("{D3", check(Row.AffectedJointsWrist));
			document.ReplaceField("{D4", check(Row.AffectedJointsHip));
			document.ReplaceField("{D5", check(Row.AffectedJointsKnee));
			document.ReplaceField("{D6", check(Row.AffectedJointsAnkle));

			document.ReplaceField("{E1", check(Row.AreasTreatedUpperBody));
			document.ReplaceField("{E2", check(Row.AreasTreatedLowerBody));
			document.ReplaceField("{E3", check(Row.AreasTreatedFullBody));
			document.ReplaceField("{E4", check(Row.AreasTreatedBack));
			document.ReplaceField("{E5", check(Row.AreasTreatedHipArea));
			document.ReplaceField("{E6", check(Row.AreasTreatedNeck));
			document.ReplaceField("{E7", check(Row.AreasTreatedShoulders));
			document.ReplaceField("{E8", check(Row.AreasTreatedAbdominals));
			document.ReplaceField("{E9", check(Row.AreasTreatedChest));
			document.ReplaceField("{F1", check(Row.AreasTreatedFace));
			document.ReplaceField("{F2", check(Row.AreasTreatedScalp));
			document.ReplaceField("{F3", check(Row.AreasTreatedArmLR));
			document.ReplaceField("{F4", check(Row.AreasTreatedLegLR));
			document.ReplaceField("{EOTHER}", Row.AreasTreatedOther);

			document.ReplaceField("{G1", check(Row.StressReductionPain));
			document.ReplaceField("{G2", check(Row.ROMImprovRelaxation));

			document.ReplaceField("{H1", check(Row.TechniquesSwedish));
			document.ReplaceField("{H2", check(Row.TechniquesThaiOil));
			document.ReplaceField("{H3", check(Row.TechniquesThai));
			document.ReplaceField("{H4", check(Row.TechniquesAshiatsu));
			document.ReplaceField("{H5", check(Row.TechniquesLymphaticDrainage));
			document.ReplaceField("{H6", check(Row.TechniquesStroking));
			document.ReplaceField("{H7", check(Row.TechniquesRocking));
			document.ReplaceField("{H8", check(Row.TechniquesEffleurage));
			document.ReplaceField("{H9", check(Row.TechniquesPetrissage));
			document.ReplaceField("{K1", check(Row.TechniquesTriggerPoint));
			document.ReplaceField("{K2", check(Row.TechniquesPressurePoints));
			document.ReplaceField("{K3", check(Row.TechniquesJointMobilization));
			document.ReplaceField("{K4", check(Row.TechniquesFriction));
			document.ReplaceField("{K5", check(Row.TechniquesPassiveStretching));

			document.ReplaceField("{L1", check(Row.TechniquesDeepTissue));
			document.ReplaceField("{L2", check(Row.TechniquesModeratePressure));
			document.ReplaceField("{L3", check(Row.TechniquesLightPressure));

			document.ReplaceField("{M1", check(Row.FeedbackRelaxation));
			document.ReplaceField("{M2", check(Row.FeedbackStressReduction));
			document.ReplaceField("{M3", check(Row.FeedbackMuscleRelaxation));
			document.ReplaceField("{M4", check(Row.FeedbackIncreaseROM));
			document.ReplaceField("{M5", check(Row.FeedbackPainReduction));
			document.ReplaceField("{M6", check(Row.FeedbackPostureImprovement));
			document.ReplaceField("{M7", check(Row.FeedbackTenstionReduction));
			document.ReplaceField("{M8", check(Row.FeedbackStiffnessReduction));

			document.ReplaceField("{N1", check(Row.IFC));
			document.ReplaceField("{N2", check(Row.Hotpack));
			document.ReplaceField("{N3", check(Row.Ultrasound));
			document.ReplaceField("{N4", check(Row.SoftTissueRelease));
			document.ReplaceField("{N5", check(Row.ShockwaveTherapy));
			document.ReplaceField("{N6", check(Row.ColdPack));
			document.ReplaceField("{N7", check(Row.Laser));
			document.ReplaceField("{N8", check(Row.Exercises));
			
			document.ReplaceField("{P1", check(Row.RecommendedHeatPacks));
			document.ReplaceField("{P2", check(Row.RecommendedColdPacks));
			document.ReplaceField("{P3", check(Row.RecommendedContrastHydrotherapy));
			document.ReplaceField("{P4", check(Row.RecommendedHotBaths));
			document.ReplaceField("{P5", check(Row.RecommendedSelfMassage));
			document.ReplaceField("{P6", check(Row.RecommendedStretching));
			document.ReplaceField("{P7", check(Row.RecommendedDiaphragmaticBreathing));
			document.ReplaceField("{P8", check(Row.RecommendedYoga));
			document.ReplaceField("{P9", check(Row.RecommendedPilates));
			document.ReplaceField("{T1", check(Row.RecommendedWalkingSwimmingCycling));
			document.ReplaceField("{T2", check(Row.RecommendedNonWeightBbearing));

			document.ReplaceField("{Comments}", Row.Comments);

			var converter = new DocToPDFConverter();
			var pdfDocument = converter.ConvertToPDF(document);
			var pdfFilename = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + ".pdf");
			pdfDocument.Save(pdfFilename);

			document.Close();

			OpenPdf(pdfFilename);
		}

		string check(bool? arg)
		{
			if (arg == true) return "X";
			else return "";
		}

		string yesno(bool? arg)
		{
			if (arg == true) return "YES";
			else if (arg == false) return "NO";
			else return "";
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
			var template = Path.Combine(location, @"Templates\AppointmentTreatmentNoteReport.docx");
			var path = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
			Directory.CreateDirectory(path);
			var file = Path.Combine(path, "" + "TreatmentNote" + " " + AppointmentDate.ToString("yyyy-MM-dd") + ".docx");
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
