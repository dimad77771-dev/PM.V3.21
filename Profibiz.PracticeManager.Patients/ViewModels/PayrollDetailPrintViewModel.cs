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
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Diagnostics;
using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.API.Native;
using System.IO;
using DevExpress.XtraRichEdit.API.Native.Implementation;
using DevExpress.Xpf.RichEdit;
using DevExpress.Xpf.Grid;
using System.Windows.Controls;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PayrollDetailPrintViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		//public RichEditControlBehaviorManager RichEditConrolManager { get; set; } = new RichEditControlBehaviorManager();
		#endregion
		public OpenParams OpenParam { get; set; }
		public Invoice Entity { get; set; }

		public PayrollDetailViewModel Model { get; set; }
		public PayrollInfoResult ModelEntity => Model.Entity;
		public ObservableCollection<InvoicePaymentByDoctors> PaymentEntities => Model.PaymentEntities;
		public RichEditControl RichEditConrol { get; set; }
		public GridControl GridControl { get; set; }


		public PayrollDetailPrintViewModel() : base()
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

		public async Task LoadData()
		{
			ShowWaitIndicator.Show();

			GenerateReport();

			ShowWaitIndicator.Hide();
		}

		void GenerateReport()
		{
			RichEditConrol.BeginUpdate();

			LoadFromTemplate();

			var doc = RichEditConrol.Document;

			ProcTable(doc, PaymentEntities, "!MAIN!");

			ReplaceField(doc, "{ServiceProviderFullName}", ModelEntity.ServiceProviderFullName);
			ReplaceField(doc, "{PeriodStartFinishText}", ModelEntity.PeriodStartFinishText);
			ReplaceField(doc, "{OpeningBalance}", ModelEntity.OpeningBalance.FormatMoney());
			ReplaceField(doc, "{SumDueToDoctorThisPeriod}", ModelEntity.SumDueToDoctorThisPeriod.FormatMoney());
			ReplaceField(doc, "{SumDueToDoctorAll}", ModelEntity.SumDueToDoctorAll.FormatMoney());
			ReplaceField(doc, "{ClosingBalance}", ModelEntity.ClosingBalance.FormatMoney());

			ReplaceField(doc, "{Sum.Payment.Amount}", PaymentEntities.Sum(q => q.Payment.Amount).FormatMoney());
			ReplaceField(doc, "{Sum.AllocateAmount}", PaymentEntities.Sum(q => q.AllocateAmount).FormatMoney());
			ReplaceField(doc, "{Sum.DueToDoctor}", PaymentEntities.Sum(q => q.DueToDoctor).FormatMoney());

			RichEditConrol.EndUpdate();
			//RichEditConrol.

			////var command = new DevExpress.XtraRichEdit.Commands.QuickPrintCommand(RichEditConrol);
			//var command = new DevExpress.XtraRichEdit.Commands.PrintCommand(RichEditConrol);
			//command.Execute();

			var printDialog = new PrintDialog();
			printDialog.PrintTicket.CopyCount = 1;
			if (printDialog.ShowDialog() != true) return;
			var xpfRichEditPrinter = new CustomXpfRichEditPrinter(RichEditConrol);
			xpfRichEditPrinter.PrintToMyPrinter(printDialog, "Payroll Details");
		}

		void LoadFromTemplate()
		{
			var location = AssemblyHelper.GetMainPath();
			var file = Path.Combine(location, "Templates", "PAYROLL_DETAIL_TEMPLATE_01.docx");
			var bytes = File.ReadAllBytes(file);
			var stream = new MemoryStream(bytes);
			RichEditConrol.LoadDocument(stream, DocumentFormat.OpenXml);
		}

		void ProcTable(Document doc, ObservableCollection<InvoicePaymentByDoctors> rows, string keystr)
		{
			var trow = FindTableRowWithKey(doc, keystr);
			if (trow == null) return;

			var columns = GridControl.Columns;
			var mergedColumns = columns.Where(q => q.AllowCellMerge == true).ToArray();

			var mergedRows = new Dictionary<Guid, List<int>>();

			var table = trow.Table;
			table.BeginUpdate();
			var nrow = trow.Index;
			var cells = trow.Cells;
			for (int r = 0; r < rows.Count; r++)
			{
				bool isMergedRow = false;
				var invoiceRowId = rows[r].Invoice.RowId;
				if (r > 0 && invoiceRowId == rows[r - 1].Invoice.RowId)
				{
					isMergedRow = true;
					if (!mergedRows.ContainsKey(invoiceRowId))
					{
						mergedRows.Add(invoiceRowId, new List<int> { r - 1, r });
					}
					else
					{
						mergedRows[invoiceRowId].Add(r);
					}
				}

				var rowhandle = GridControl.GetRowHandleByListIndex(r);
				var k = nrow + r + 1;
				table.Rows.InsertAfter(k - 1);
				for (int i = 0; i < cells.Count; i++)
				{
					var column = columns[i];
					var cell = cells[i];
					var range = cell.Range;
					range = doc.CreateRange(range.Start.ToInt(), range.Length - 1);
					var text = doc.GetText(range);

					var cell2 = table.Rows[k].Cells[i];
					text = GridControl.GetCellDisplayText(rowhandle, column);
					if (mergedColumns.Contains(column) && isMergedRow)
					{
						text = "";
					}

					doc.InsertText(cell2.Range.Start, text);
				}
			}
			table.Rows.RemoveAt(nrow);

			foreach (var mergedColumn in mergedColumns)
			{
				var i = columns.IndexOf(mergedColumn);
				foreach (var mergedRow in mergedRows.Values)
				{
					var r1 = mergedRow.First();
					var r2 = mergedRow.Last();
					var cell1 = table.Rows[r1 + 1].Cells[i];
					var cell2 = table.Rows[r2 + 1].Cells[i];
					table.MergeCells(cell1, cell2);
				}

			}

			table.EndUpdate();
		}

		
	

		void ReplaceField(Document doc, string field, string value)
		{
			doc.ReplaceAll(field, value ?? "", SearchOptions.CaseSensitive);
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

		






		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public void Close()
		{
			CloseInteractionRequest.Raise(null);
		}
		public class OpenParams
		{
			public Guid RowId { get; set; }
			public Invoice Row { get; set; }
		}


	}

}
