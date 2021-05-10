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

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class PayrollPaymentOneViewModel : ViewModelBase 
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
		public PayrollPayment Entity { get; set; }
		public bool IsNew { get; set; }
		public ObservableCollection<PayrollPaymentAllocation> AllocationEntities { get; set; }
		public PayrollPaymentAllocation SelectedAllocationEntity { get; set; }
        public Boolean ReadOnly { get; set; }
		public OpenParams OpenParam { get; set; }
		public Guid ServiceProviderRowId => OpenParam.ServiceProviderRowId;



		public PayrollPaymentOneViewModel() : base()
		{
		}
        public static PayrollPaymentOneViewModel Create()
        {
            return ViewModelSource.Create<PayrollPaymentOneViewModel>();
        }


		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			//else if (arg is String)
			//{
			//	if ((string)arg == "IsNew")
			//	{
			//		OpenParam = new OpenParams { IsNew = true };
			//	}
			//}
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}

		class PeriodInfo
		{
			public DateTime PeriodStart { get; set; }
			public Decimal SumInvoice { get; set; }
			public Decimal SumPayroll { get; set; }
			public Decimal SumDelta => SumInvoice - SumPayroll;
		}

		async public Task LoadData()
		{
			IsNew = OpenParam.IsNew;
			ReadOnly = OpenParam.ReadOnly;
			ShowWaitIndicator.Show();

			PayrollPayment entity = null;
			if (!IsNew)
			{
				entity = await businessService.GetPayrollPayment(OpenParam.RowId);
				AllocationEntities = entity.PayrollPaymentAllocations.OrderBy(q => q.PeriodStart).ToObservableCollection();
			}
			else
			{
				var query = "ServiceProviderRowId=" + ServiceProviderRowId.ToWebQuery();
				var task0 = lookupsBusinessService.UpdateAllLookups();
				var task1 = businessService.GetPayrollPaymentByDoctorAndPeriod(query);
				var task2 = businessService.GetInvoicePaymentByDoctorAndPeriod(query);
				await Task.WhenAll(task0, task1, task2);
				var payrollPayments = task1.Result;
				var invoicePayments = task2.Result;

				var allPeriods1 = payrollPayments.Select(q => q.PeriodStart);
				var allPeriods2 = invoicePayments.Select(q => q.PaymentPeriod);
				var allPeriods = allPeriods1.Union(allPeriods2).ToArray();
				var allPeriodInfo = allPeriods.Select(q => new PeriodInfo { PeriodStart = q }).ToArray();

				foreach (var periodInfo in allPeriodInfo)
				{
					periodInfo.SumInvoice = invoicePayments.SingleOrDefault(q => q.PaymentPeriod == periodInfo.PeriodStart)?.SumDueToDoctor ?? 0;
					periodInfo.SumPayroll = payrollPayments.SingleOrDefault(q => q.PeriodStart == periodInfo.PeriodStart)?.SumPayrollAmount ?? 0;
				}

				var newinfo = OpenParam.NewRowInfo;
				entity = new PayrollPayment
				{
					RowId = Guid.NewGuid(),
					ServiceProviderRowId = newinfo.ServiceProviderRowId,
					ServiceProviderFullName = LookupDataProvider.ServiceProvider2Name(newinfo.ServiceProviderRowId),
				};
				AllocationEntities = new ObservableCollection<PayrollPaymentAllocation>();

				foreach (var periodInfo in allPeriodInfo.Where(q => q.PeriodStart <= OpenParam.ForNewPeriodFinish).Where(q => q.SumDelta != 0).OrderBy(q => q.PeriodStart))
				{
					var arow = new PayrollPaymentAllocation
					{
						RowId = Guid.NewGuid(),
						PeriodStart = periodInfo.PeriodStart,
						PeriodFinish = periodInfo.PeriodStart.LastDayMonth(),
						Amount = periodInfo.SumDelta,
					};
					AllocationEntities.Add(arow);
				}
			}
			Entity = entity;

			
			AllocationEntities.ForEach(q => InitAllocationRow(q));
			CalcPaymentFields();

   			ResetHasChange();
			ShowWaitIndicator.Hide();
		}





        public void AllocationNew()
        {
			var row = new PayrollPaymentAllocation
			{
				RowId = Guid.NewGuid(),
			};
			AllocationEntities.Add(row);
			SelectedAllocationEntity = row;
			InitAllocationRow(row);
			CalcPaymentFields();
        }



        public void AllocationDelete()
        {
            var row = SelectedAllocationEntity;
            var messageBoxService = this.GetRequiredService<IMessageBoxService>();
            var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
            if (ret == MessageResult.Yes)
            {
                AllocationEntities.Remove(row);
                Entity.IsChanged = true;
                CalcPaymentFields();
            }
        }
        public bool CanAllocationDelete() => (SelectedAllocationEntity != null);




		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));

		

		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.PaymentDate, "Payment Date", errors);
			ValidateHelper.Positive(Entity.Amount, "Amount", errors);

			ValidateHelper.EmptyEnumerable(this, AllocationEntities, (q) => q.EditPeriodStart, "Period", () => SelectedAllocationEntity, errors);
			ValidateHelper.ValidateDuplicate(
				this, AllocationEntities, (q) => q.PeriodStart, 
				(q) => "Period \"" + q.PeriodStart.ToString(@"MM\/yyyy") + "\" " + ValidateHelper.IS_DUPLICATE, 
				() => SelectedAllocationEntity, errors, ignoreEmptyValues: true);
			ValidateHelper.EmptyEnumerable(this, AllocationEntities, (q) => q.Amount, "Allocated Amount", () => SelectedAllocationEntity, errors);


			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}

			return true;
		}

		public async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.PayrollPaymentAllocations = AllocationEntities.Where(q => q.Amount != 0).Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostPayrollPayment(updateEntity) :
				await businessService.PutPayrollPayment(updateEntity);
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
				CloseCore(force:true);
			}

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
			if (Entity == null || AllocationEntities == null)
			{
				return false;
			}

			return (IsNew || Entity.IsChanged || AllocationEntities.Any(q => q.IsChanged));
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			AllocationEntities.ForEach(q => q.IsChanged = false);
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
						RowId = Entity.ServiceProviderRowId,
					},
				});
			});
		}





		void InitAllocationRow(PayrollPaymentAllocation row)
		{
			row.InitEditPeriodStart();
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcPaymentFields();
			};

			row.OnOpenDetail = () =>
			{
				ShowDetail(row);
			};
		}


		public void ShowDetail(PayrollPaymentAllocation row)
		{
			DispatcherUIHelper.Run(() =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.PayrollDetailWindowView,
					Param = new PayrollDetailViewModel.OpenParams
					{
						ServiceProviderRowId = ServiceProviderRowId,
						PeriodStart = row.PeriodStart,
						PeriodFinish = row.PeriodFinish,
					},
				});
			});
		}

		void CalcPaymentFields()
        {
            Entity.Amount =  AllocationEntities.Sum(q => q.Amount);
            BehaviorGridConrolInvoiceItem.UpdateTotalSummary();
        }



		public class OpenParams
		{
			public Boolean IsNew { get; set; }
			public Guid RowId { get; set; }
			public Guid ServiceProviderRowId { get; set; }
			public PayrollInfoResult NewRowInfo { get; set; }
			public Boolean ReadOnly { get; set; }
			public DateTime ForNewPeriodStart { get; set; }
			public DateTime ForNewPeriodFinish { get; set; }
		}







	}
}
