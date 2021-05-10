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
	public class PrintdocProccessing
	{
		public Guid InvoiceRowId;// = new Guid("92349B5F-B05E-4AAD-808C-4C9517A15638");
		public PrintDocument[] PrintDocuments;
		public PrintDialog PrintDialog;
		public ShowWaitIndicator ShowWaitIndicator;
		public Boolean IsPrintMode { get; set; }
		public Boolean IsPdfMode { get; set; }


		PrintdocInfo PrintdocInfo;
		RichEditControl RichEditControl;
		Document Document;

		const string STR_FALSE = "\uF070";
		const string STR_TRUE = "\uF078";

		public PrintdocProccessing() { }

		public List<OnePdfFile> OutputPdfFiles { get; set; } = new List<OnePdfFile>();
		public class OnePdfFile
		{
			public string FileName { get; set; }
			public byte[] PdfBytes { get; set; }
		}

		public async Task LoadDataFromServer()
		{
			var businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
			PrintdocInfo = await businessService.GetPrintdocInfo("invoiceRowId=" + InvoiceRowId.ToWebQuery());

			//if (RuntimeHelper.IsMachineD)
			//{
			//	PrintdocInfo.Appointments.AddRange(PrintdocInfo.Appointments);
			//	PrintdocInfo.Appointments.AddRange(PrintdocInfo.Appointments);
			//	PrintdocInfo.Appointments.AddRange(PrintdocInfo.Appointments);
			//	PrintdocInfo.Appointments.AddRange(PrintdocInfo.Appointments);
			//}
		}

		public async Task Run()
		{
			ShowWait("Loading data...");
			await LoadDataFromServer();

			var location = AssemblyHelper.GetMainPath();

			var cnt = 0;
			foreach (var printDocument in PrintDocuments)
			{
				cnt++;
				ShowWait("" + cnt + " of " + PrintDocuments.Length);

				var templatefile = Path.Combine(location, printDocument.TemplateFile);
				OneDocument(templatefile, printDocument);
			}
			HideWait();

			//for(int i = 1; i <= 18; i++)
			////for (int i = 9; i <= 9; i++)
			//{
			//	if (i == 12) continue;

			//	var file = @"E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Patients\Templates\Docs753\753(" + i + ").docx";
			//	var file2 = @"E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Patients\Templates\Docs753\__out\out_753(" + i + ").docx";
			//	var file3 = @"E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Patients\Templates\Docs753\__out\out_753(" + i + ")--1.pdf";
			//	var file4 = @"E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Patients\Templates\Docs753\__out\out_753(" + i + ")--2.pdf";
			//	OneDocument(file, file2, file3, file4);
			//}
		}


		//public void OneDocument(string file, string file2, string file3, string file4)
		public void OneDocument(string file, PrintDocument printDocument)
		{
			RichEditControl = new RichEditControl();
			LoadFromTemplate(file);
			Document = RichEditControl.Document;
			isProcAppointments = false;


			var bookmarksName = Document.Bookmarks.Where(q => q.Name != "_GoBack").OrderBy(q => q.Range.Start).Select(q => q.Name).ToArray();
			///var zzz11 = string.Join("\n", bookmarksName);
			foreach (var bookmarkName in bookmarksName)
			{
				var bookmark = Document.Bookmarks[bookmarkName];
				var range = bookmark.Range;

				var p1 = bookmarkName.IndexOf("__");
				if (p1 < 0) throw new PrintdocProccessingException("invalid bookmark:" + bookmarkName);
				var category = bookmarkName.Substring(0, p1);
				var field = bookmarkName.Substring(p1 + 2);
				var extext = Document.GetText(range);
				var value = GetField(category, field);
				var text = (value == null ? "" : value.ToString());

				if (category == "Appointment")
				{
					ProcAppointments(bookmark);
				}
				else if (extext == "M")
				{
					var printdocMultiRowsWriter = new PrintdocMultiRowsWriter
					{
						Text = text,//"123456 98765432 11234 999999999 1111111111 222-33333 12345 4444444444444 55555555555 7777777777777",
						Bookmark = bookmark,
						RichEditControl = RichEditControl,
						Document = Document,
						//MaxRows = 2,
					};
					printdocMultiRowsWriter.Run();
				}
				else
				{
					if (extext == STR_FALSE || extext == STR_TRUE)
					{
						var boolval = (bool?)value;
						text = (boolval == true ? STR_TRUE : boolval == false ? STR_FALSE : "");
						Document.Replace(range, text);
					}
					else
					{
						if ((value is DateTime || value is DateTime?) && (value != null))
						{
							text = (value as DateTime?).FormatShortDate();
						}
					}
					Document.Replace(range, text);
				}
			}

			if (IsPrintMode)
			{
				var xpfRichEditPrinter = new CustomXpfRichEditPrinter(RichEditControl);
				xpfRichEditPrinter.PrintToMyPrinter(PrintDialog, printDocument.Name);
			}

			if (IsPdfMode)
			{
				var pdfFileName = printDocument.Name.Replace(" ", "_") + ".pdf";
				OutputPdfFiles.Add(PrintdocProccessingFunc.GetPdfFromRichEditControl(RichEditControl, pdfFileName));
			}


			////File.WriteAllBytes(@"E:\PROJECTS\Profibiz.PracticeManager\111.docx", RichEditControl.Document.OpenXmlBytes);
			////RichEditControl.SaveDocument(@"E:\PROJECTS\Profibiz.PracticeManager\Profibiz.PracticeManager.Patients\Templates\Docs753\__out\112.docx", DocumentFormat.OpenXml);
			//RichEditControl.SaveDocument(file2, DocumentFormat.OpenXml);
			////PrintDirectWithWord(file2);
			//ConvertToPdfWithDevexpress(file2, file3);
			//ConvertToPdfWithWord(file2, file4);
		}

		void PrintDirectWithWord(string file2)
		{
			ProcessStartInfo info = new ProcessStartInfo(file2);
			//info.Verb = "PrintTo"; info.Arguments = "\"" + "Foxit Editor PDF Printer" + "\"";
			info.Verb = "Print";
			info.CreateNoWindow = true;
			info.WindowStyle = ProcessWindowStyle.Hidden;
			info.UseShellExecute = true;
			Process.Start(info);
		}

		// Convert a Word 2008 .docx to Word 2003 .doc
		void ConvertToPdfWithWord(string input, string output)
		{
			// Create an instance of Word.exe
			var oWord = new Microsoft.Office.Interop.Word.Application();

			// Make this instance of word invisible (Can still see it in the taskmgr).
			oWord.Visible = false;

			// Interop requires objects.
			object oMissing = System.Reflection.Missing.Value;
			object isVisible = true;
			object readOnly = false;
			object oInput = input;
			object oOutput = output;
			object oFormat = Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatPDF;

			// Load a document into our instance of word.exe
			var oDoc = oWord.Documents.Open(ref oInput, ref oMissing, ref readOnly, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref isVisible, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			// Make this document the active document.
			oDoc.Activate();

			// Save this document in Word 2003 format.
			oDoc.SaveAs(ref oOutput, ref oFormat, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing, ref oMissing);

			// Always close Word.exe.
			oWord.Quit(ref oMissing, ref oMissing, ref oMissing);
		}

		void ConvertToPdfWithDevexpress(string input, string output)
		{
			//var printer = new XpfRichEditPrinter(RichEditControl as IRichEditControl);
			RichEditDocumentServer server = new RichEditDocumentServer();
			server.LoadDocument(input);
			server.ExportToPdf(output);
		}

		void LoadFromTemplate(string file)
		{
			var location = AssemblyHelper.GetMainPath();
			var bytes = File.ReadAllBytes(file);

			var stream = new MemoryStream(bytes);
			RichEditControl.LoadDocument(stream, DocumentFormat.OpenXml);
			stream.Dispose();
		}

		object GetField(string category, string field)
		{
			if (category == "Patient") return GetFieldPatient(field);
			else if (category == "MH") return GetFieldMedicalHistory(field);
			else if (category == "Common") return GetFieldCommon(field);
			else if (category == "Invoice") return GetFieldInvoice(field);
			else if (category == "Appointment") return "";
			else throw new ArgumentException(category);
		}

		object GetFieldPatient(string col)
		{
			var patient = PrintdocInfo.Patient;

			if (col == "Name" || col == "Name2") return patient.FullName;
			else if (col == "IsSexM") return (patient.Sex == TypeHelper.Sex.Male ? true : (bool?)null);
			else if (col == "IsSexF") return (patient.Sex == TypeHelper.Sex.Female ? true : (bool?)null);
			else if (col == "PhoneH") return patient.HomePhoneNumber;
			else if (col == "PhoneB") return patient.WorkPhone;
			else if (col == "Cell") return patient.MobileNumber;
			else if (col == "Fax") return patient.Fax;
			else if (col == "Email") return patient.EmailAddress;
			else if (col == "Address") return patient.GetAddress();
			else if (col == "AddressLine") return patient.GetAddressLine();
			else if (col == "AddressCity") return patient.GetAddressCity();
			else if (col == "AddressProvince") return patient.GetAddressProvince();
			else if (col == "AddressPostcode") return patient.GetAddressPostcode();
			else if (col == "BirthDate") return patient.BirthDate.FormatShortDate();
			else if (col == "BirthDateDDMMYY") return patient.BirthDate.FormatDDMMYY();
			else if (col == "BirthDateDDMMYY2") return patient.BirthDate.FormatDDMMYY2();
			else if (col == "Age") return patient.GetAge();
			else if (col == "Occupation") return patient.Occupation;

			//else if (col == "SourceReferral") return LookupDataProvider.Referrer2Name(patient.ReferrerRowId);
			//else if (col == "SourceReferralAddress") return LookupDataProvider.Referrer2Address(patient.ReferrerRowId);
			else if (col == "SourceReferral") return "";            //укзание OneNote 17.07.2017
			else if (col == "SourceReferralAddress") return "";     //укзание OneNote 17.07.2017


			else throw new ArgumentException(col);
		}

		object GetFieldCommon(string col)
		{
			//if (col == "Date" || col == "Date2") return DateTime.Today;
			if (col == "Date" || col == "Date2")
			{
				return PrintdocInfo.FirstAppointmentStart?.Date;
			}
			else throw new ArgumentException(col);
		}


		object GetFieldInvoice(string col)
		{
			if (col == "CategoriesList" || col == "Treatment") return PrintdocInfo.Invoice.CategoriesList;
			if (col == "ServiceProvidersList") return PrintdocInfo.Invoice.ServiceProvidersList;
			else throw new ArgumentException(col);
		}
		Type typeMedicalHistoryRecord = typeof(MedicalHistoryRecord);
		object GetFieldMedicalHistory(string col)
		{
			var row = PrintdocInfo.MedicalHistoryRecords.FirstOrDefault();
			if (row == null) row = new MedicalHistoryRecord { RecordDate = DateTime.Today };

			var prop = typeMedicalHistoryRecord.GetProperty(col);
			var value = prop.GetValue(row);
			return value;

			//if (prop.PropertyType == typeof(bool))
			//{

			//}
			//return "";
		}

		

		string ProcAppointmentsGetValue(string col, Appointment appointment)
		{
			if (col == "Appointment__StartDate") return appointment.Start.FormatShortDate();
			else if (col == "Appointment__StartTime") return appointment.Start.FormatHHMM();
			else if (col == "Appointment__Treatment") return appointment.MedicalServiceName;
			else if (col == "Appointment__Duration") return appointment.DurationInMinutes + " min.";
			else throw new PrintdocProccessingException(col);
		}

		bool isProcAppointments;
		void ProcAppointments(Bookmark bookmark0)
		{
			if (isProcAppointments) return;

			var cell0 = Document.Tables.GetTableCell(bookmark0.Range.Start);
			var table = cell0.Table;
			var row0 = cell0.Row;
			var rowindex0 = row0.Index;

			var bookmarkNameOrder = "Appointment__OrderNum";
			var bookmarks = Document.Bookmarks.Where(q => q.Name.StartsWith("Appointment__") && q.Name != bookmarkNameOrder).ToArray();
			var colindexs = bookmarks.Select(q => Document.Tables.GetTableCell(q.Range.Start).Index).ToArray();

			var appointments = PrintdocInfo.Appointments.OrderBy(q => q.Start).ToArray();
			for(int r = 0; r < appointments.Length; r++)
			{
				var appointment = appointments[r];

				for (int i = 0; i < bookmarks.Length; i++)
				{
					if (bookmarks[i].Name == bookmarkNameOrder) continue;

					var value = ProcAppointmentsGetValue(bookmarks[i].Name, appointment);
					var rowindex = rowindex0 + r;
					if (rowindex >= table.Rows.Count)
					{
						var trow = table.Rows.InsertAfter(table.Rows.Last().Index);
						var colindex9 = Document.Tables.GetTableCell(Document.Bookmarks[bookmarkNameOrder].Range.Start).Index;
						var cell9 = trow[colindex9];
						Document.InsertText(cell9.Range.Start, "" + (r + 1));
					}
					var row = table.Rows[rowindex];
					var colindex = colindexs[i];
					var cell = row[colindex];
					Document.InsertText(cell.Range.Start, value);
				}
			}

			isProcAppointments = true;
		}

		void ShowWait(string text)
		{
			if (ShowWaitIndicator != null)
			{
				text = "Printing. " + text;
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Custom, text);
			}
		}

		void HideWait()
		{
			if (ShowWaitIndicator != null)
			{
				ShowWaitIndicator.Hide();
			}
		}




	}

	public class PrintdocMultiRowsWriter
	{
		public String Text { get; set; }
		public Bookmark Bookmark { get; set; }
		public Int32 MaxRows { get; set; } = Int32.MaxValue;
		public RichEditControl RichEditControl { get; set; }
		public Document Document { get; set; }


		string[] lines;
		List<TableCell> tableCells = new List<TableCell>();
		CharacterPropertiesBase CharacterProperties;
		Table table;
		int curCellIndex = 0;
		bool isFirstRow = true;
		int col0index;

		public void Run()
		{
			BuildTableCells();
			BuildCharacterProperties();

			lines = (Text ?? "").Replace("\r", "").Split('\n');
			lines.ForEach(q => RunLine(q));
		}

		void BuildCharacterProperties()
		{
			CharacterProperties = Document.BeginUpdateCharacters(Bookmark.Range);
		}

		void BuildTableCells()
		{
			var cell = Document.Tables.GetTableCell(Bookmark.Range.Start);
			tableCells.Add(cell);

			table = cell.Table;
			var rowindex = cell.Row.Index;
			col0index = cell.Index;
			while (true)
			{
				rowindex++;
				if (rowindex >= table.Rows.Count || tableCells.Count >= MaxRows)
				{
					break;
				}
				var ncells = table.Rows[rowindex].Cells;
				//if (ncells.Count != 1) throw new PrintdocProccessingException("ncells.Count != 1:" + Bookmark.Name);
				var ncolindex = col0index < ncells.Count ? col0index : 0;
				var ncell = ncells[ncolindex];
				tableCells.Add(ncell);
			}
		}

		void AddNewTableCell()
		{
			var lastCell = tableCells.Last();
			var trow = table.Rows.InsertAfter(lastCell.Row.Index);
			var cells = trow.Cells;
			//if (cells.Count != 1) throw new PrintdocProccessingException("cells.Count != 1:NEW");
			var ncolindex = col0index < cells.Count ? col0index : 0;
			tableCells.Add(cells[ncolindex]);
		}

		void RunLine(string line)
		{
			if (curCellIndex >= tableCells.Count)
			{
				AddNewTableCell();
			}
			var cell = tableCells[curCellIndex];

			if (cell.PreferredWidthType != WidthType.Fixed) throw new PrintdocProccessingException("wrong PreferredWidthType");
			var delta = 10f; // изменил с 0.5f из-за разницы в rendering
			var cellWidth = cell.PreferredWidth - cell.LeftPadding - cell.RightPadding - delta;

			var delIndexes = GetDelimitedIndexes(line);
			var position = delIndexes[0];
			foreach (var pos in delIndexes.Reverse())
			{
				var s1 = line.Substring(0, pos);
				var s1Width = RichEditControl.MeasureSingleLineString(s1, CharacterProperties).Width;
				if (s1Width <= cellWidth)
				{
					position = pos;
					break;
				}
			}

			var str1 = line.Substring(0, position);
			var str2 = line.Substring(position);
			var range = cell.Range;
			if (isFirstRow)
			{
				Document.Replace(Bookmark.Range, str1);
				isFirstRow = false;
			}
			else
			{
				Document.InsertText(range.Start, str1);
			}
			curCellIndex++;

			if (!string.IsNullOrEmpty(str2))
			{
				RunLine(str2);
			}
		}



		int[] GetDelimitedIndexes(string line)
		{
			var delIndexes = new List<int>();
			var inword = false;
			var isfirstword = true;
			for (int i = 0; i < line.Length; i++)
			{
				var c = line[i];
				//var iswordchar = Char.IsLetterOrDigit(c) || c == '-';
				var iswordchar = !Char.IsWhiteSpace(c);
				if (inword)
				{
					if (!iswordchar)
					{
						inword = false;
					}
				}
				else
				{
					if (iswordchar)
					{
						if (isfirstword)
						{
							isfirstword = false;
						}
						else
						{
							delIndexes.Add(i);
						}
						inword = true;
					}
				}
			}
			delIndexes.Add(line.Length);

			return delIndexes.ToArray();
		}



	}

	public class PrintdocProccessingException : Exception
	{
		public PrintdocProccessingException(string arg) : base(arg)
		{

		}
	}

	public static class PrintdocProccessingFunc
	{
		public static PrintdocProccessing.OnePdfFile GetPdfFromRichEditControl(RichEditControl RichEditControl, string pdfFileName)
		{
			var options = new DevExpress.XtraPrinting.PdfExportOptions();
			options.DocumentOptions.Author = "";
			options.Compressed = false;
			options.ImageQuality = DevExpress.XtraPrinting.PdfJpegImageQuality.High;
			var memstream = new MemoryStream();
			RichEditControl.ExportToPdf(memstream, options);

			var pdfbytes = memstream.ToArray();
			memstream.Dispose();

			var onePdfFile = new PrintdocProccessing.OnePdfFile
			{
				FileName = pdfFileName,
				PdfBytes = pdfbytes,
			};
			return onePdfFile;
		}
	}
}	
