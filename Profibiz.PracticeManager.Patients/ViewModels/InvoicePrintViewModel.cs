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
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Diagnostics;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.IO;
using DevExpress.Xpf.Ribbon;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Editors.Settings;
using System.Windows.Data;
using System.Windows.Controls;


namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InvoicePrintViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public RichEditControlBehaviorManager RichEditConrolManager { get; set; } = new RichEditControlBehaviorManager();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual Invoice Entity { get; set; }
		public virtual ObservableCollection<PrintDocument> PrintDocuments { get; set; }
		public virtual RibbonControl MyRibbonControl { get; set; }
		public virtual Boolean IsPrintFormsOnly { get; set; }

		public InvoicePrintViewModel() : base()
		{
		}

		public async Task OnOpen(OpenParams param)
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

			var task1 = businessService.GetInvoice(OpenParam.RowId, includeAppointment: true);
			var task2 = businessService.GetPrintDocuments("");
			await Task.WhenAll(task1, task2);

			Entity = await task1;
			PrintDocuments = (await task2).OrderBy(q => q.OrderNum).ToObservableCollection();
			Invoice.CalcTotalFields(Entity, Entity.InvoiceItems, Entity.InvoiceClaims);
			await GenerateReport();
			CustomizeRibon();

			ShowWaitIndicator.Hide();
		}

		void CustomizeRibon()
		{
			var grpsize = 3;
			var curgrpsize = Int32.MaxValue;
			var rgroup = MyRibbonControl.Categories.SelectMany(q => q.Pages.SelectMany(z => z.Groups)).First(q => q.Name == "xPrintDocuments");
			var rpage = rgroup.Page;
			for (int i = 0; i < PrintDocuments.Count; i++)
			{
				if (curgrpsize >= grpsize)
				{
					//rgroup = new RibbonPageGroup
					//{
					//	Caption = "",// + i,
					//};
					//rpage.Groups.Add(rgroup);
					var barItemSeparator = new BarItemSeparator();
					rgroup.Items.Add(barItemSeparator);
					curgrpsize = 0;
				}
				curgrpsize++;

				var printDocument = PrintDocuments[i];
				var barEditItem = new BarEditItem
				{
					Content = printDocument.Name,
					ClosePopupOnChangingEditValue = true,
					RibbonStyle = RibbonItemStyles.SmallWithText,
					EditSettings = new CheckEditSettings { HorizontalContentAlignment = EditSettingsHorizontalAlignment.Right },
					GlyphAlignment = System.Windows.Controls.Dock.Right,
					//Alignment = BarItemAlignment.Near,
					//EditHorizontalAlignment = System.Windows.HorizontalAlignment.Right,
				};

				var myBinding = new Binding("PrintDocuments[" + i + "].IsChecked");
				myBinding.Source = this;
				myBinding.Mode = BindingMode.TwoWay;
				myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
				BindingOperations.SetBinding(barEditItem, BarEditItem.EditValueProperty, myBinding);

				rgroup.Items.Add(barEditItem);
			}
			//RibbonControl111.AutoHideMode
		}

		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public void Close()
		{
			CloseInteractionRequest.Raise(null);
		}

		async Task GenerateReport()
		{
			RichEditConrolManager.BeginUpdate();

			await LoadFromTemplate();

			var doc = RichEditConrolManager.Document;

			if (Entity.InvoiceType == TypeHelper.InvoiceType.Appointment)
			{
				Entity.InvoiceItems.Sort((x, y) => DateTime.Compare((DateTime)x.ItemDate, (DateTime)y.ItemDate));
			}

			var invoiceItems = Entity.InvoiceItems.OrderBy(q => q.ItemDate).ToList();
			ProcTable(doc, invoiceItems, "{Line");
			List<ServiceProvider> serviceProviders = null;

			if (Entity.InvoiceType == TypeHelper.InvoiceType.Appointment)
			{
				serviceProviders =
					   LookupDataProvider.Instance.ServiceProviders
					   .Where(q => Entity.InvoiceItems.Any(z => z?.Appointment?.ServiceProviderRowId == q.RowId))
					   .OrderBy(q => q.FullName)
					   .ToList();
				// in case we use table for Service Providers i.e John Smith, CMTO # 123
				ProcTable(doc, serviceProviders, "ServiceProvider");
			}


			ReplaceField(doc, "{InvoiceDate}", Entity.InvoiceDate.FormatShortDate());
			ReplaceField(doc, "{InvoiceNumber}", Entity.InvoiceNumber);
			ReplaceField(doc, "{BillTo}", Entity.BillTo);
			ReplaceField(doc, "{BillToAddress1}", Entity.BillToAddress1);
			ReplaceField(doc, "{BillToAddress2}", Entity.BillToAddress2);
			ReplaceField(doc, "{BillToCity}", Entity.BillToCity);
			ReplaceField(doc, "{BillToProvince}", Entity.BillToProvince);
			ReplaceField(doc, "{BillToPostCode}", Entity.BillToPostCode);
			ReplaceField(doc, "{Total}", Entity.Total.FormatMoney());
			ReplaceField(doc, "{Total}", Entity.Total.FormatMoney());
			if (serviceProviders != null)
			{
				if (serviceProviders.Count == 1)
				{
					// ReplaceField(doc, "{ServiceProviderList}", serviceProviders.FirstOrDefault().FullName);
					// ReplaceField(doc, "{AssociationsList}", serviceProviders.FirstOrDefault().AssosiationsList);
					ReplaceField(doc, "{FooterText}", serviceProviders.FirstOrDefault().FooterText);
				}
				else
				{
					// TODO: Look for a table in the end of the document and add as this
					// John Smith:  Ass 1 # 123, Ass 2 #223, Ass3 # 4
					// Ivan Petrov:  Ass 1 # 123, Ass 2 #223, Ass3 # 4

					// ReplaceField(doc, "{ServiceProviderList}", serviceProviders.Select(p => p.FullName).ToArray()));
					// ReplaceField(doc, "{AssociationsList}", string.Join(",", serviceProviders.Select(p => p.AssosiationsList).ToArray()));
				}

			}

			RichEditConrolManager.EndUpdate();
		}

		void ProcTable<T>(Document doc, List<T> rows, string keystr) where T : class
		{
			var trow = FindTableRowWithKey(doc, keystr);
			if (trow == null) return;

			var formatTableTag = "";
			var table = trow.Table;
			var nrow = trow.Index;
			var cells = trow.Cells;
			for (int r = 0; r < rows.Count; r++)
			{
				var k = nrow + r + 1;
				table.Rows.InsertAfter(k - 1);
				for (int i = 0; i < cells.Count; i++)
				{
					var cell = cells[i];
					var range = cell.Range;
					range = doc.CreateRange(range.Start.ToInt(), range.Length - 1);
					var text = doc.GetText(range);
					if (formatTableTag == "")
					{
						formatTableTag = GetFormatTableTag(text);
					}
					text =
						   typeof(T) == typeof(InvoiceItem) ? GetInvoiceItemValue(text, rows[r] as InvoiceItem, r) :
						   typeof(T) == typeof(ServiceProvider) ? GetServiceProviderValue(text, rows[r] as ServiceProvider, r) :
						   "";
					var cell2 = table.Rows[k].Cells[i];
					doc.InsertText(cell2.Range.Start, text);
				}
			}
			table.Rows.RemoveAt(nrow);

			ApplyTableStyle(table, nrow, nrow + rows.Count - 1, formatTableTag);
		}

		void ApplyTableStyle(Table table, int startRow, int endRow, string formatTableTag)
		{
			DateTime dt1 = DateTimeHelper.Now;

			for (int i = startRow; i <= endRow; i++)
			{
				var trow = table.Rows[i];
				var cells = trow.Cells;
				var cellCount = cells.Count;
				for (int j = 0; j < cellCount; j++)
				{
					var cell = cells[j];
					var borders = cell.Borders;

					if (formatTableTag == "[A]")
					{
						SetBorder(borders.Left, true);
						SetBorder(borders.Right, true);
						SetBorder(borders.Top, i == startRow);
						//SetBorder(borders.Bottom, i == endRow);
					}
					else if (formatTableTag == "[B]")
					{
						SetBorder(borders.Left, j == 0);
						SetBorder(borders.Right, j == cellCount - 1);
						SetBorder(borders.Top, i == startRow);
						//SetBorder(borders.Bottom, i == endRow);
					}
					else throw new LogicalException();
				}
			}

			DateTime dt2 = DateTimeHelper.Now;
			NLog.Trace("222=" + (dt2 - dt1).TotalMilliseconds);
		}

		void SetBorder(TableCellBorder border, bool isline)
		{
			var lineColor = System.Drawing.Color.Black;
			var lineThickness = 0.5f;

			if (isline)
			{
				border.LineColor = lineColor;
				border.LineThickness = lineThickness;
				border.LineStyle = TableBorderLineStyle.Single;
			}
			else
			{
				border.LineColor = lineColor;
				border.LineThickness = 0;
				border.LineStyle = TableBorderLineStyle.None;
			}
		}

		async Task LoadFromTemplate()
		{
			//RichEditConrolManager.LoadDocument(@"E:\Tmp\204\INVOICE_TEMPLATE_02.docx", DocumentFormat.OpenXml);

			var printTemplate = Entity.PrintTemplate;
			var isPaid = Entity.IsPaid;
			var bytes = await FormDocumentHelper.GetTemplateDocumentBytesByCode(printTemplate, isPaid, MessageBoxService);
			if (bytes == null) return;

			//var template = LookupDataProvider.Instance.Templates.FirstOrDefault(q => q.IsTemplate && q.Code == printTemplate);
			//var bytes =
			//     printTemplate == "INVOICE_TEMPLATE_02" ? InvoicePrintTemplate.INVOICE_TEMPLATE_02 :
			//     printTemplate == "INVOICE_TEMPLATE_03" ? InvoicePrintTemplate.INVOICE_TEMPLATE_03 :
			//     InvoicePrintTemplate.INVOICE_TEMPLATE_01;

			//var location = AssemblyHelper.GetMainPath();
			//var file = Path.Combine(location, "Templates", printTemplate + ".docx");
			//file = GetTemplateFileName(file);
			//var bytes = File.ReadAllBytes(file);
			//var lookupsBusinessService = ServiceHelper.GetInstance<ILookupsBusinessService>();
			//var bytes = (await lookupsBusinessService.GetTemplateDocumentBytes(template.RowId)).DocumentBytes;

			var stream = new MemoryStream(bytes);
			RichEditConrolManager.LoadDocument(stream, DocumentFormat.OpenXml);
		}

		string GetTemplateFileName(string file)
		{
			if (Entity != null)
			{
				if (Entity.IsPaid)
				{
					var filePaid = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "_paid" + Path.GetExtension(file));
					if (File.Exists(filePaid))
					{
						return filePaid;
					}
				}
			}

			return file;
		}


		void ReplaceField(Document doc, string field, string value)
		{
			doc.ReplaceAll(field, value ?? "", SearchOptions.CaseSensitive);
		}

		String GetInvoiceItemValue(string text, InvoiceItem row, int rowindex)
		{
			text = text ?? "";

			text = text.Replace("{LineNum}", "" + (rowindex + 1));
			//text = text.Replace("{LineDate}", (row.Appointment == null ? "" : row.Appointment.Start.FormatShortDate()));
			text = text.Replace("{LineDate}", (row.Appointment == null ? "" : row.ItemDate.FormatShortDate()));
			text = text.Replace("{LineDescription}", GetDescription(row));
			text = text.Replace("{LinePrice}", row.Price.FormatMoney());
			text = text.Replace("{LineUnits}", row.Units.Format("0.##"));
			text = text.Replace("{LineTotal}", row.LineTotal.FormatMoney());

			text = text.Replace("{LineStartTime}", (row.Appointment == null ? "" : row.Appointment.Start.FormatHHMM()));
			text = text.Replace("{LineDuration}", (row.Appointment == null ? "" : row.Appointment.DurationInMinutes.ToString()));

			text = ReplaceSpecTags(text);

			return text;
		}

		String GetServiceProviderValue(string text, ServiceProvider row, int rowindex)
		{
			text = text ?? "";

			text = text.Replace("{ServiceProvider}", row.FullName);
			text = text.Replace("{PrimaryAssociation}", row.AssociationRowId != null ? row.AssociationName + ", " + row.RegistrationNumber : "");

			text = ReplaceSpecTags(text);

			return text;
		}

		String GetDescription(InvoiceItem row)
		{
			if (Entity.InvoiceType == TypeHelper.InvoiceType.Supply)
			{
				return row.Description;
			}
			else if (Entity.InvoiceType == TypeHelper.InvoiceType.Appointment)
			{
				var appointment = row.Appointment;
				var medicalService = LookupDataProvider.FindMedicalService(appointment.MedicalServicesOrSupplyRowId);
				var serviceProvider = LookupDataProvider.FindServiceProvider(appointment.ServiceProviderRowId);

				var description = appointment.MedicalServiceNameWithPrintLabel;
				return description;
			}
			else throw new LogicalException();
		}

		TableRow FindTableRowWithKey(Document doc, string keystr)
		{
			foreach (var table in doc.Tables)
			{
				foreach (var trow in table.Rows)
				{
					var text = doc.GetText(trow.Range);
					if (text.Contains(keystr))
					{
						return trow;
					}
				}
			}
			return null;
		}

		
		string[] SpecTags => FormatTableTags.ToArray();
		string ReplaceSpecTags(string arg)
		{
			SpecTags.ForEach(q => arg = arg.Replace(q, ""));
			return arg;
		}


		string[] FormatTableTags = { "[A]", "[B]" };
		string GetFormatTableTag(string arg)
		{
			foreach (var tag in FormatTableTags)
			{
				if (arg.Contains(tag))
				{
					return tag;
				}
			}
			return FormatTableTags[0];
		}



		public void PrintDocumentsSelectAll()
		{
			PrintDocuments.ForEach(q => q.IsChecked = true);
		}
		public void PrintDocumentsSelectNone()
		{
			PrintDocuments.ForEach(q => q.IsChecked = false);
		}

		public void Print(bool isQuick)
		{
			var printDialog = new PrintDialog();
			printDialog.PrintTicket.CopyCount = 1;
			if (!isQuick)
			{
				if (printDialog.ShowDialog() != true) return;
			}
			var xpfRichEditPrinter = new CustomXpfRichEditPrinter(RichEditConrolManager.Control);

			if (!IsPrintFormsOnly)
			{
				xpfRichEditPrinter.PrintToMyPrinter(printDialog, "Invoice");
			}

			var printdocProccessing = new PrintdocProccessing
			{
				InvoiceRowId = Entity.RowId,
				IsPrintMode = true,
				PrintDialog = printDialog,
				PrintDocuments = PrintDocuments.Where(q => q.IsChecked).ToArray(),
				ShowWaitIndicator = ShowWaitIndicator,
			};
			if (printdocProccessing.PrintDocuments.Any())
			{
				DispatcherUIHelper.Run(async () =>
				{ 
					await printdocProccessing.Run();
				});
			}
		}
		public void PrintQuick() => Print(isQuick: true);
		public void PrintCustom() => Print(isQuick: false);


		public void EmailSend()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var recipients = new List<EmailSendRecipient>();

				var patient = Entity.Patient;
				if (patient != null)
				{
					var recipient = new EmailSendRecipient
					{
						RowId = Guid.NewGuid(),
						Name = patient.FullName,
						Email = patient.EmailAddress,
						PatientRowId = patient.RowId,
					};
					recipients.Add(recipient);
				}

				var serviceProviderRowIds = Entity.InvoiceItems.Select(q => q.Appointment.ServiceProviderRowId).Distinct().Where(q => q != null).ToArray();
				foreach(var serviceProviderRowId in serviceProviderRowIds)
				{
					var serviceProvider = LookupDataProvider.FindServiceProvider(serviceProviderRowId);

					var recipient = new EmailSendRecipient
					{
						RowId = Guid.NewGuid(),
						Name = serviceProvider.FullName,
						Email = serviceProvider.EmailAddress,
						ServiceProviderRowId = serviceProvider.RowId,
					};
					recipients.Add(recipient);
				}

				for(int k = 0; k < 5; k++)
				{
					var recipient = new EmailSendRecipient
					{
						RowId = Guid.NewGuid(),
						Name = "",
						Email = "",
					};
					recipients.Add(recipient);
				}

				var printdocProccessing = new PrintdocProccessing
				{
					InvoiceRowId = Entity.RowId,
					IsPdfMode = true,
					PrintDocuments = PrintDocuments.Where(q => q.IsChecked).ToArray(),
					ShowWaitIndicator = ShowWaitIndicator,
				};
				var pdfFiles = printdocProccessing.OutputPdfFiles;

				if (!IsPrintFormsOnly)
				{
					var onePdfFile = PrintdocProccessingFunc.GetPdfFromRichEditControl(RichEditConrolManager.Control, "Invoice.pdf");
					pdfFiles.Insert(0, onePdfFile);
				}
				await printdocProccessing.Run();

				var attachments = printdocProccessing.OutputPdfFiles.Select(q => new EmailSendAttachment
				{
					RowId = Guid.NewGuid(),
					FileName = q.FileName,
					FileBytes = q.PdfBytes,
				}).ToList();


				var ret2 = await EmailSendRunViewModel.Run(new EmailSendRunViewModel.OpenParams
				{
					InvoiceRowId = Entity.RowId,
					Recipients = recipients,
					Attachments = attachments,
					ShowDXWindowsInteractionRequest = OpenWindowInteractionRequest,
					RunMode = EmailSendRunViewModel.RunModeEnum.Main,
				});
				if (!ret2.IsSuccess) return;
			});
		}


		public class OpenParams
		{
			public Guid RowId { get; set; }
			public Invoice Row { get; set; }
		}


	}

}
