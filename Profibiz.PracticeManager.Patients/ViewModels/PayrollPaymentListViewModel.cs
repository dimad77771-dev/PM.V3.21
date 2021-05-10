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
using DevExpress.Xpf.LayoutControl;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PayrollPaymentListViewModel : ViewModelBase
	{
		#region Services
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public GridControlBehaviorManager BehaviorGridConrolInvoiceItem { get; set; } = new GridControlBehaviorManager();
		#endregion
		public ObservableCollection<PayrollPayment> PayrollPaymentEntities { get; set; }
		public PayrollPayment SelectedPayrollPaymentEntity { get; set; }
		public OpenParams OpenParam { get; set; }
		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }
		public Guid? ServiceProviderRowId => OpenParam?.ServiceProviderRowId;
		public Boolean IsOneServiceProvider => (ServiceProviderRowId != null);
		//public Boolean IsWindowMode => IsOneServiceProvider;
		public Boolean IsWindowMode
		{
			get
			{
				var a = OpenParam;
				return IsOneServiceProvider;
			}
		}
		public virtual Boolean ShowRibbon { get; set; }
		public LayoutGroupView LayoutGroupView => IsOneServiceProvider ? LayoutGroupView.GroupBox : LayoutGroupView.Group;




		public PayrollPaymentListViewModel() : base()
		{
			MessengerHelper.Register<MsgRowChange<PayrollPayment>>(this, OnMsgRowChange);
			if (!IsOneServiceProvider)
			{
				var ret = GlobalSettings.Instance.Finances.GetFinancesViewDateFilter();
				FilterFrom = ret.FilterFrom;
				FilterTo = ret.FilterTo;
			}
		}


		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			else if (arg.Equals(""))
			{
				OpenParam = new OpenParams();
			}
			//else if (arg is String)
			//{
			//	if ((string)arg == "IsNew")
			//	{
			//		OpenParam = new OpenParams { IsNew = true };
			//	}
			//}

			ShowRibbon = !IsWindowMode;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}


		async public Task LoadData(Guid? selectedRowId = null)
		{
			ShowWaitIndicator.Show();
			var query = IsOneServiceProvider ? 
				"ServiceProviderRowId=" + ServiceProviderRowId.ToWebQuery() :
				"paymentDateFrom=" + FilterFrom.ToWebQuery() + "&paymentDateTo=" + FilterTo.ToWebQuery();
			var task = businessService.GetPayrollPaymentList(query);
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(task);
			PayrollPaymentEntities = rows.OrderByDescending(q => q.PaymentDate).ToObservableCollection();
			PayrollPaymentEntities.ForEach(q => SubscribePayrollPaymentRow(q));
			if (selectedRowId != null)
			{
				SelectedPayrollPaymentEntity = PayrollPaymentEntities.FirstOrDefault(q => q.RowId == selectedRowId);
			}
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}





		public void PayrollPaymentNew()
		{
			//var row = new PayrollPaymentAllocation
			//{
			//	RowId = Guid.NewGuid(),
			//};
			//PayrollPaymentEntities.Add(row);
			//SelectedPayrollPaymentEntity = row;
			//InitAllocationRow(row);
			//CalcPaymentFields();
		}



		public void PayrollPaymentDelete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var row = SelectedPayrollPaymentEntity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = MessageBoxService.ShowMessage("Delete Payroll Payment \"" + row.PaymentDate?.ToString("d") + " - " + row.Amount?.ToString("c") + "\" ?", CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var uret = await businessService.DeletePayrollPayment(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;
					PayrollPaymentEntities.Remove(row);
				}
			});
		}
		public bool CanPayrollPaymentDelete() => (SelectedPayrollPaymentEntity != null);



		public void PayrollPaymentEdit() => AddEditPayrollPaymentRow(SelectedPayrollPaymentEntity);
		public bool CanPayrollPaymentEdit() => (SelectedPayrollPaymentEntity != null);
		public void MouseDoubleClick(PayrollPayment row)
		{
			if (row != null) AddEditPayrollPaymentRow(row);
		}


		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));

		

		bool Validate()
		{
			return true;
		}

		public async Task<bool> SaveCore(bool andClose)
		{
			////validate
			//if (!Validate())
			//{
			//	return false;
			//}

			////updateEntity
			//var updateEntity = Entity.GetPocoClone();
			//updateEntity.PayrollPaymentAllocations = PayrollPaymentEntities.Select(q => q.GetPocoClone()).ToList();

			////save
			//ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			//var uret = IsNew ?
			//	await businessService.PostPayrollPayment(updateEntity) :
			//	await businessService.PutPayrollPayment(updateEntity);
			//ShowWaitIndicator.Hide();
			//if (!uret.Validate(MessageBoxService))
			//{
			//	return false;
			//}

			//MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			//IsNew = false;
			//ResetHasChange();

			////close
			//if (andClose)
			//{
			//	CloseCore(force:true);
			//}

			return true;
		}
		public void Close() => CloseCore();



		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return false;
		}

		void ResetHasChange()
		{
		}


		public async Task<OnCloseReturn> OnClose(bool showOKCancel = false)
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
					return OnCloseReturn.Cancel;
				}
				else if (ret == MessageResult.No)
				{
					return OnCloseReturn.No;
				}
				else if (ret == MessageResult.Yes || ret == MessageResult.OK)
				{
					return await SaveCore(andClose: false) ? OnCloseReturn.Yes : OnCloseReturn.Cancel;
				}
				else throw new ArgumentException();
			}
			else
			{
				return OnCloseReturn.Yes;
			}
		}

		public async void ClosingEvent(CancelEventArgs arg)
		{
			if (forceClose)
			{
				return;
			}
			if (await OnClose() == OnCloseReturn.Cancel)
			{
				arg.Cancel = true;
			}
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
						RowId = ServiceProviderRowId.Value,
					},
				});
			});
		}


		void SubscribePayrollPaymentRow(PayrollPayment row)
		{
			row.OnOpenDetail = () =>
			{
				AddEditPayrollPaymentRow(row);
			};
		}

		void AddEditPayrollPaymentRow(PayrollPayment row)
		{
			var isnew = (row == null);
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.PayrollPaymentOneWindowView,
				Param = new PayrollPaymentOneViewModel.OpenParams
				{
					IsNew = isnew,
					ServiceProviderRowId = row.ServiceProviderRowId,
					RowId = (isnew ? default(Guid) : row.RowId),
					//ReadOnly = true,
				},
			});
		}

		void OnMsgRowChange(MsgRowChange<PayrollPayment> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				await LoadData(selectedRowId: msg.Row.RowId);
			});
		}

		public void Filter(string mode) => FilterCore(mode);
		public void FinanceDateApply(FinanceDateClass preset) => FilterCore(null, preset);

		public void FilterCore(string arg, FinanceDateClass preset = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (preset != null)
				{
					var cret = preset.Get();
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "PreviousMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(-1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}
				else if (arg == "NextMonth")
				{
					var cret = DateTimeHelper.ChangeMonthFromTo(1, FilterFrom, FilterTo);
					FilterFrom = cret.From;
					FilterTo = cret.To;
				}

				GlobalSettings.Instance.Finances.SetFinancesViewDateFilter(FilterFrom, FilterTo);
				await LoadData();
			});
		}



		public class OpenParams
		{
			public Guid? ServiceProviderRowId { get; set; }
		}







	}
}
