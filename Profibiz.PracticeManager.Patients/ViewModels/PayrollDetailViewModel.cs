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
using System.Windows;
using System.Windows.Media;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Windows.Media.Animation;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Bars;
using DevExpress.Mvvm.UI;
using System.Windows.Input;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PayrollDetailViewModel : ViewModelBase 
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolForPrint { get; set; } = new GridControlBehaviorManager();
		public RichEditControlBehaviorManager RichEditConrolManager { get; set; } = new RichEditControlBehaviorManager();
		#endregion
		public OpenParams OpenParam { get; set; }
		public PayrollInfoResult Entity { get; set; }
		public ObservableCollection<InvoicePaymentByDoctors> PaymentEntities { get; set; }
		public InvoicePaymentByDoctors PaymentEntitySelected { get; set; }

		public DateTime PeriodStart => Entity.PeriodStart;
		public DateTime PeriodFinish => Entity.PeriodFinish;
		public Guid ServiceProviderRowId => Entity.ServiceProviderRowId;
		public String WindowTitle => "Payroll Details " + Entity?.PeriodStartFinishText;




		public PayrollDetailViewModel() : base()
		{
		}
        public static PayrollDetailViewModel Create()
        {
            return ViewModelSource.Create<PayrollDetailViewModel>();
        }

		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}


		async public Task LoadData()
		{
            ShowWaitIndicator.Show();

			if (OpenParam.PayrollInfoResult == null)
			{
				var qry2 = "PeriodStart=" + OpenParam.PeriodStart.ToWebQuery()
					+ "&PeriodFinish=" + OpenParam.PeriodFinish.ToWebQuery()
					+ "&ServiceProviderRowId=" + OpenParam.ServiceProviderRowId.ToWebQuery();
				OpenParam.PayrollInfoResult = (await businessService.GetPayrollInfo(qry2))[0];
			}

			Entity = OpenParam.PayrollInfoResult;

			var qry = "PeriodStart=" + PeriodStart.ToWebQuery() + "&PeriodFinish=" + PeriodFinish.ToWebQuery() + "&serviceProviderRowId=" + ServiceProviderRowId.ToWebQuery();
			var rows = await businessService.GetPayrollDetail(qry);
			PaymentEntities = rows.OrderBy(q => q.Invoice.PatientFullName).ThenBy(q => q.InvoiceRowId).ToObservableCollection();
			PaymentEntities.ForEach(q => SubscribeListRow(q));
			//PaymentEntities = rows.OrderBy(q => q.InvoiceRowId).ToObservableCollection();


			ShowWaitIndicator.Hide();
		}


		public void OpenSpecialist()
		{
			DispatcherUIHelper.Run(() =>
			{

				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneSpecialistView,
					Param = new OneSpecialistViewModel.OpenParams
					{
						IsNew = false,
						RowId = Entity.ServiceProviderRowId,
					},
				});
			});
		}


		public void CustomColumnSort(CustomColumnSortEventArgs e)
		{
			//var b1 = e.Value1;
			//var b2 = e.Value2;
			//var z = e.Result;
			////e.Result = dayIndex1.CompareTo(dayIndex2);
			//e.Handled = true;
		}


		public void CellMerge(CellMergeEventArgs e)
		{
			var row1 = BehaviorGridConrol.GetRow<InvoicePaymentByDoctors>(e.RowHandle1);
			var row2 = BehaviorGridConrol.GetRow<InvoicePaymentByDoctors>(e.RowHandle2);

			e.Merge = (row1.InvoiceRowId == row2.InvoiceRowId);
			e.Handled = true;
		}

		public void Close() => CloseCore();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}



		void SubscribeListRow(InvoicePaymentByDoctors row)
		{
			row.OnOpenDetail = () =>
			{
				var uiElement = Mouse.DirectlyOver as UIElement;

				var popup = new PopupMenu();
				var barInvoice = new BarButtonItem { Content = "Invoice" };
				var barPayment = new BarButtonItem { Content = "Payment" };
				barInvoice.ItemClick += (s, e) => OpenInvoiceDetail(row);
				barPayment.ItemClick += (s, e) => OpenPaymentDetail(row);
				popup.ItemLinks.Add(barInvoice);
				popup.ItemLinks.Add(barPayment);
				popup.ShowPopup(uiElement);
			};
		}


		void OpenInvoiceDetail(InvoicePaymentByDoctors row)
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.InvoiceWindowView,
					Param = new InvoiceWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.InvoiceRowId,
						ReadOnly = true,
					},
				});
			});
		}


		void OpenPaymentDetail(InvoicePaymentByDoctors row)
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PaymentWindowView,
					Param = new PaymentWindowViewModel.OpenParams
					{
						IsNew = false,
						RowId = row.PaymentRowId,
						SelectInvoiceRowId = row.InvoiceRowId,
					},
				});
			});
		}

		public void MouseDoubleClick(MouseButtonEventArgs e)
		{
			var hitInfo = BehaviorGridConrol.GetCalcHitInfo(e);
			if (hitInfo == null) return;

			var row = BehaviorGridConrol.GetRow<InvoicePaymentByDoctors>(hitInfo.RowHandle);
			if (hitInfo.Column.AllowCellMerge == true)
			{
				OpenInvoiceDetail(row);
			}
			else
			{
				OpenPaymentDetail(row);
			}
		}

		public void Print()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var pmodel = new PayrollDetailPrintViewModel();
				pmodel.Model = this;
				pmodel.RichEditConrol = RichEditConrolManager.Control;
				pmodel.GridControl = BehaviorGridConrolForPrint.Control;
				await pmodel.LoadData();
			});
		}


		public class OpenParams
		{
			public PayrollInfoResult PayrollInfoResult { get; set; }

			public Guid ServiceProviderRowId { get; set; }
			public DateTime PeriodStart { get; set; }
			public DateTime PeriodFinish { get; set; }

		}





	}
}
