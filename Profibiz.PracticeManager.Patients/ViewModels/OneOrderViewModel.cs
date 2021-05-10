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

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class OneOrderViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public GridControlBehaviorManager BehaviorGridConrolOrderItem { get; set; } = new GridControlBehaviorManager();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual Order Entity { get; set; }
		public bool IsNew { get; set; }
		public virtual ObservableCollection<OrderItem> OrderItemEntities { get; set; }
		public virtual OrderItem OrderItemSelectedEntity { get; set; }


		public OneOrderViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
		}

		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			else if (arg is String)
			{
				if ((string)arg == "IsNew")
				{
					OpenParam = new OpenParams { IsNew = true };
				}
			}
			DispatcherUIHelper.Run2(LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			IsNew = OpenParam.IsNew;
			Order entity;
			if (!IsNew)
			{
				entity = await businessService.GetOrder(OpenParam.RowId);
			}
			else
			{
				entity = new Order();
				entity.RowId = Guid.NewGuid();
				entity.OrderDate = DateTime.Today;
				//entity.MedicalServiceOrSupplyRowId = OpenParam.NewMedicalServiceOrSupplyRowIdRowId;
			}
			Entity = entity;

			OrderItemEntities = Entity.OrderItems.OrderBy(q => LookupDataProvider.MedicalService2Name(q.MedicalServiceOrSupplyRowId)).ToObservableCollection();

			ResetHasChange();
			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}




		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));




		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.OrderDate, "Order Date", errors);

			if (OrderItemEntities.Count == 0)
			{
				errors.Add("Order Items list cannot be empty");
			}

			ValidateHelper.PositiveEnumerable(this, OrderItemEntities, (q) => q.Qty, "Qty", () => OrderItemSelectedEntity, errors);
			ValidateHelper.PositiveEnumerable(this, OrderItemEntities, (q) => q.Price, "Price", () => OrderItemSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, OrderItemEntities, (q) => q.Tax, "Tax", () => OrderItemSelectedEntity, errors);


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
			//var wnd = ViewModelExtensions.ViewModel2Window(this);
			//var beh = DevExpress.Mvvm.UI.Interactivity.Interaction.GetBehaviors(wnd);
			//var srv = beh.OfType<DXSplashScreenService>().FirstOrDefault();
			//if (srv == null)
			//{
			//	var template = (System.Windows.DataTemplate)System.Windows.Application.Current.FindResource("WaitIndicatorDataTemplate");
			//	srv = new DXSplashScreenService();
			//	srv.ViewTemplate = template;
			//	beh.Add(srv);
			//}
			//var srv2 = srv as ISplashScreenService;
			//srv2.ShowSplashScreen();
			//await Task.Delay(3000);
			//srv2.HideSplashScreen();
			//return true;


			//validate
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.OrderItems = OrderItemEntities.Select(q => q.GetPocoClone()).ToList();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = IsNew ?
				await businessService.PostOrder(updateEntity) :
				await businessService.PutOrder(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			ShowWaitIndicator.Hide();
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
			return (IsNew || Entity.IsChanged);
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
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

		public DelegateCommand<string> AddRowFromPopupCommand => new DelegateCommand<string>((column) =>
		{
			OpenWindowInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneSupplierView,
				Param = "IsNew",
			});
		});

		public void OrderItemNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickMedicalServicesOrSuppliesViewModel.PickRow(
									OpenWindowInteractionRequest,
									PickMedicalServicesOrSuppliesViewModel.PickModeEnum.PickSupply);
				if (!ret2.IsSuccess) return;

				var mrow = ret2.PickRow;
				var row = new OrderItem
				{
					RowId = Guid.NewGuid(),
					OrderRowId = Entity.RowId,
					MedicalServiceOrSupplyRowId = mrow.RowId,
					Price = mrow.UnitPrice ?? 0,
					Tax = mrow.TaxRate ?? 0,
					Qty = 0,
					IsChanged = true,
					IsNew = true,
				};
				row.Description = GetOrderDescription(row);
				OrderItemEntities.Add(row);
				OrderItemSelectedEntity = row;
				SubscribeOrderItemRow(row);
				CalcOrderFields();

				DispatcherUIHelper.Run(() =>
				{
					BehaviorGridConrolOrderItem.SetCurrentColumn("Qty");
					BehaviorGridConrolOrderItem.ShowEditor(true);
				});
			});
		}

		public void OrderItemDelete()
		{
			DispatcherUIHelper.Run(() =>
			{
				var row = OrderItemSelectedEntity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					OrderItemEntities.Remove(row);
					Entity.IsChanged = true;
					CalcOrderFields();
				}
			});
		}
		public bool CanOrderItemDelete() => (OrderItemSelectedEntity != null);


		string GetOrderDescription(OrderItem row)
		{
			return null;
		}
		void SubscribeOrderItemRow(OrderItem row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcOrderFields();
			};
		}
		void CalcOrderFields()
		{
			BehaviorGridConrolOrderItem.UpdateTotalSummary();
		}

		public class OpenParams
		{
			public Boolean IsNew { get; set; }
			public Guid RowId { get; set; }
			public Boolean ReadOnly { get; set; }
		}
	}
}
