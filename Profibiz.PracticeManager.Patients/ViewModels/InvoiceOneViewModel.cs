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
using Profibiz.PracticeManager.SharedCode;
using System.Windows.Input;
using DevExpress.Xpf.Utils;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class InvoiceOneViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; }
		public GridControlBehaviorManager BehaviorGridConrolInvoiceItem { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolInvoicePayment { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolInvoiceClaim { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual Invoice Entity { get; set; }
		public virtual Boolean IsNew { get; set; }
		public virtual Guid? RowId { get; set; }
		public virtual ObservableCollection<InvoiceItem> InvoiceItemEntities { get; set; }
		public virtual InvoiceItem InvoiceItemSelectedEntity { get; set; }
		public virtual ObservableCollection<InvoicePayment> InvoicePaymentEntities { get; set; }
		public virtual InvoicePayment InvoicePaymentSelectedEntity { get; set; }
		public virtual ObservableCollection<InvoiceRefund> InvoiceRefundEntities { get; set; }
		public virtual InvoiceRefund InvoiceRefundSelectedEntity { get; set; }
		public virtual ObservableCollection<InvoiceClaim> InvoiceClaimEntities { get; set; }
		public virtual InvoiceClaim InvoiceClaimSelectedEntity { get; set; }
		public virtual String InvoiceType => Entity.InvoiceType;
		public virtual Boolean ReadOnly { get; set; }
		public virtual Boolean NotReadOnly => !ReadOnly;
		public virtual Boolean FlagAlwaysIsNewHasChanges { get; set; } = true;
		public virtual Boolean IsShowColorRowBackground { get; set; }
		public virtual Int32 ColorRowBackground => (!IsShowColorRowBackground ? 0 : IsNew ? 1 : 2);
		public virtual Boolean IsShowCommandPanel => IsWindowMode;
		public virtual Boolean ShowThirdPartyServiceProvider => (Entity?.InvoiceType == TypeHelper.InvoiceType.ThirdParty);
		public virtual Boolean ShowInvoiceItemDate => (Entity?.InvoiceType != TypeHelper.InvoiceType.Supply);
		public virtual Boolean ShowInvoiceItemDescription => (Entity?.InvoiceType != TypeHelper.InvoiceType.ThirdParty);
		public virtual Boolean ShowInvoiceItemServcieOrSupplyRowId => (Entity?.InvoiceType == TypeHelper.InvoiceType.ThirdParty);
		public virtual Boolean ShowOpenDetailColumn => (Entity?.InvoiceType == TypeHelper.InvoiceType.Appointment);
		public virtual ObservableCollection<Template> InvoiceTemplates => InvoiceTemplateInfo.GetForInvoiceType(Entity?.InvoiceType);
		public virtual Boolean IsCollapsedLayoutPayments { get; set; } = true;
		public virtual Boolean IsCollapsedLayoutRefunds { get; set; } = true;
		public virtual Boolean IsVisibilityUserControl { get; set; } = false;
		public virtual bool IsVisibilityRate { get; set; }
		public virtual bool IsHideLayoutCoordinations => (Entity?.HasNoCoverage == true);
		


		public virtual Boolean IsWindowMode { get; set; }
		public virtual ViewModes ViewMode { get; set; }
		public enum ViewModes { Main, InvoicesBuilder }
		public virtual Boolean IsInvoicesBuilder => (ViewMode == ViewModes.InvoicesBuilder);


		public InvoiceOneViewModel() : base()
		{
			MessengerHelper.RunAction(this, () =>
			{
				MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChange);
			});
		}

		public static InvoiceOneViewModel Create(bool isWindowMode)
		{
			var ret = ViewModelSource.Create<InvoiceOneViewModel>();
			ret.IsWindowMode = isWindowMode;
			return ret;
		}

		async public Task LoadData(bool isNew = false, Guid? rowId = null, Invoice newInvoice = null, Boolean readOnly = false, Guid? selectPaymentRowId = null, Boolean flagAlwaysIsNewHasChanges = true, InteractionRequest<CloseDXWindowsActionParam> closeInteractionRequest = null)
		{
			if (!isNew)
			{
				ShowWaitIndicator.Show();
			}

			CloseInteractionRequest = closeInteractionRequest;

			if (newInvoice != null)
			{
				isNew = true;
			}
			IsNew = isNew;
			RowId = rowId;
			ReadOnly = readOnly;
			FlagAlwaysIsNewHasChanges = flagAlwaysIsNewHasChanges;

			Invoice entity;
			if (!IsNew)
			{
				entity = await businessService.GetInvoice(RowId);
			}
			else
			{
				entity = newInvoice;
			}
			Entity = entity;

			InvoiceItemEntities = new ObservableCollection<InvoiceItem>(Entity.InvoiceItems.OrderBy(q => q.ItemDate));
			BuildLastInvoiceItemEntities();
			InvoiceItemEntities.ForEach(q => SubscribeInvoiceItemRow(q));

			InvoicePaymentEntities = new ObservableCollection<InvoicePayment>(Entity.InvoicePayments);
			InvoicePaymentEntities.ForEach(q => SubscribeInvoicePaymentRow(q));

			InvoiceRefundEntities = new ObservableCollection<InvoiceRefund>(Entity.InvoiceRefunds);
			InvoiceRefundEntities.ForEach(q => SubscribeInvoiceRefundRow(q));

			InvoiceClaimEntities = new ObservableCollection<InvoiceClaim>(Entity.InvoiceClaims);
			InvoiceClaimEntities.ForEach(q => SubscribeInvoiceClaimRow(q));

			CalcInvoiceFields();
			SetupReadOnly();
			IsCollapsedLayoutPayments = !InvoicePaymentEntities.Any();
			IsCollapsedLayoutRefunds = !InvoiceRefundEntities.Any();

			var prow = InvoicePaymentEntities.FirstOrDefault(q => q.PaymentRowId == selectPaymentRowId);
			if (prow != null)
			{
				InvoicePaymentSelectedEntity = prow;
				BehaviorGridConrolInvoicePayment.Focus();
			}

			ResetHasChange();
			ShowWaitIndicator.Hide();
			IsVisibilityUserControl = true;
		}


		void SetupReadOnly()
		{
			//if (InvoicePaymentEntities.Count > 0)
			//{
			//	ReadOnly = true;
			//}
		}


		string GetDescription(InvoiceItem item)
		{
			//if (InvoiceType == Invoice.InvoiceTypeEnum.Supply)
			{
				var supply = LookupDataProvider.FindMedicalService(item.ServcieOrSupplyRowId);
				var category = LookupDataProvider.FindCategory(supply.CategoryRowId)?.Name;
				//var lines = new string[]
				//{
				//	"Item: " + supply.Name,
				//	(string.IsNullOrEmpty(supply.Model) ? "" : "Model: " + supply.Model),
				//	(string.IsNullOrEmpty(supply.Supplier) ? "" : "Supplier: " + supply.Supplier),
				//	(string.IsNullOrEmpty(supply.Size) ? "" : "Size: " + supply.Size),
				//	(string.IsNullOrEmpty(category) ? "" : "Category: " + category),
				//};
				var lines = new string[]
				{
					supply.UsePrintLabel ? supply.PrintLabel : supply.Name,
					(string.IsNullOrEmpty(supply.Notes) ? "" : "" + supply.Notes),
					(string.IsNullOrEmpty(supply.Model) ? "" : "" + supply.Model),
					(string.IsNullOrEmpty(supply.Supplier) ? "" : "" + supply.Supplier),
				};


				var description = string.Join("\n", lines.Where(q => !string.IsNullOrEmpty(q)).ToArray());
				return description;
			}
			//else return "";
		}

		#region Region InvoiceItem_operations

		public void InvoiceItemNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (InvoiceType == TypeHelper.InvoiceType.Appointment)
				{
					await InvoiceItemNewAppointment();
				}
				else if (InvoiceType == TypeHelper.InvoiceType.Supply)
				{
					await InvoiceItemNewSupply();
				}
				else if (InvoiceType == TypeHelper.InvoiceType.ThirdParty)
				{
					InvoiceItemNewThirdParty();
				}
				else throw new LogicalException("InvoiceType error");
			});
		}

		async Task InvoiceItemNewAppointment()
		{
			var excludeAppointments = InvoiceItemEntities.Where(q => q.AppointmentRowId != null).Select(q => (Guid)q.AppointmentRowId);
			var ret2 = await PickAppointmentViewModel.PickRow(
									ShowDXWindowsInteractionRequest,
									PickAppointmentViewModel.PickModeEnum.Main,
									patientRowId: Entity.PatientRowId,
									completed: true,
									inInvoice: false,
									excludeAppointments: excludeAppointments,
									multipleSelectionMode: true);
			if (!ret2.IsSuccess) return;

			var appointments = ret2.PickRows;
			foreach (var appointment in appointments)
			{
				var invoiceItem = InvoiceItem.CreateFromAppointment(appointment);
				invoiceItem.InvoiceRowId = Entity.RowId;
				InvoiceItemEntities.Add(invoiceItem);
				SubscribeInvoiceItemRow(invoiceItem);
			}
			CalcInvoiceFields();


		}


		async Task InvoiceItemNewSupply()
		{
			var ret2 = await PickMedicalServicesOrSuppliesViewModel.PickRow(
									ShowDXWindowsInteractionRequest,
									PickMedicalServicesOrSuppliesViewModel.PickModeEnum.PickSupply);
			if (!ret2.IsSuccess) return;

			var mrow = ret2.PickRow;
			var row = new InvoiceItem
			{
				RowId = Guid.NewGuid(),
				InvoiceRowId = Entity.RowId,
				AppointmentRowId = null,
				ServcieOrSupplyRowId = mrow.RowId,
				Price = mrow.UnitPrice,
				Tax = mrow.TaxRate,
				Units = null,
				IsChanged = true,
				IsNew = true,
			};
			row.Description = GetDescription(row);
			InvoiceItemEntities.Add(row);
			InvoiceItemSelectedEntity = row;
			SubscribeInvoiceItemRow(row);
			CalcInvoiceFields();

			DispatcherUIHelper.Run(() =>
			{
				BehaviorGridConrolInvoiceItem.SetCurrentColumn("Units");
				BehaviorGridConrolInvoiceItem.ShowEditor(true);
			});
		}

		void InvoiceItemNewThirdParty()
		{
			var row = new InvoiceItem
			{
				RowId = Guid.NewGuid(),
				InvoiceRowId = Entity.RowId,
				AppointmentRowId = null,
				ServcieOrSupplyRowId = null,
				Price = null,
				Tax = null,
				Units = null,
				IsChanged = true,
				IsNew = true,
			};
			row.Description = "";
			InvoiceItemEntities.Add(row);
			InvoiceItemSelectedEntity = row;
			SubscribeInvoiceItemRow(row);
			CalcInvoiceFields();

			DispatcherUIHelper.Run(() =>
			{
				BehaviorGridConrolInvoiceItem.SetCurrentColumn("Units");
				BehaviorGridConrolInvoiceItem.ShowEditor(true);
			});
		}


		public void InvoiceItemDelete()
		{
			var row = InvoiceItemSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				InvoiceItemEntities.Remove(row);
				Entity.IsChanged = true;
				CalcInvoiceFields();
			}
		}
		public bool CanInvoiceItemDelete() => (InvoiceItemSelectedEntity != null);

		#endregion



		#region Region InvoiceClaim_operations

		public void InvoiceClaimNew()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickInsuranceCoverageViewModel.PickRow(new PickInsuranceCoverageViewModel.OpenParams
				{
					PickMode = PickInsuranceCoverageViewModel.PickModeEnum.ForPatient,
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					PatientRowId = Entity.PatientRowId,
					CoverageDates = GetCoverageDates(),
				});
				if (!ret2.IsSuccess) return;

				var insuranceCoverage = ret2.PickRow;
				var sentAmont = (Entity.Total ?? 0) - (Entity.ApproveAmont ?? 0);
				var row = new InvoiceClaim
				{
					RowId = Guid.NewGuid(),
					InvoiceRowId = Entity.RowId,
					InsuranceCoverageRowId = insuranceCoverage.RowId,
					InsuranceCoverage = insuranceCoverage,
					SentDate = DateTime.Today,
					SentAmont = sentAmont,
					IsChanged = true,
					IsNew = true,
				};
				InvoiceClaimEntities.Add(row);
				InvoiceClaimSelectedEntity = row;
				SubscribeInvoiceClaimRow(row);
				CalcInvoiceFields();

				DispatcherUIHelper.Run(() =>
				{
					BehaviorGridConrolInvoiceClaim.SetCurrentColumn("SentAmont");
					BehaviorGridConrolInvoiceClaim.ShowEditor(true);
				});
			});
		}

		DateTime[] GetCoverageDates()
		{
			IEnumerable<DateTime?> ret;
			if (Entity.InvoiceType == TypeHelper.InvoiceType.Supply)
			{
				ret = new[] { Entity.InvoiceDate };
			}
			else
			{
				ret = InvoiceItemEntities.Select(q => q.ItemDate);
			}

			return ret.OfType<DateTime>().ToArray();
		}

		public void InvoiceClaimDelete()
		{
			var row = InvoiceClaimSelectedEntity;
			var messageBoxService = this.GetRequiredService<IMessageBoxService>();
			var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret == MessageResult.Yes)
			{
				InvoiceClaimEntities.Remove(row);
				Entity.IsChanged = true;
				CalcInvoiceFields();
			}
		}
		public bool CanInvoiceClaimDelete() => (InvoiceClaimSelectedEntity != null);

		public void InvoiceClaimChangeInsuranceCoverage()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret2 = await PickInsuranceCoverageViewModel.PickRow(new PickInsuranceCoverageViewModel.OpenParams
				{
					PickMode = PickInsuranceCoverageViewModel.PickModeEnum.ForPatient,
					ShowDXWindowsInteractionRequest = ShowDXWindowsInteractionRequest,
					PatientRowId = Entity.PatientRowId,
					CoverageDates = GetCoverageDates(),
				});
				if (!ret2.IsSuccess) return;

				var insuranceCoverage = ret2.PickRow;
				var row = InvoiceClaimSelectedEntity;
				row.InsuranceCoverageRowId = insuranceCoverage.RowId;
				row.InsuranceCoverage = insuranceCoverage;
				CalcInvoiceFields();
			});
		}
		public bool CanInvoiceClaimChangeInsuranceCoverage() => (InvoiceClaimSelectedEntity != null);

		#endregion




		#region Region InvoicePayment_operations

		public void InvoicePaymentNew()
        {
            //if (InvoiceType == Invoice.InvoiceTypeEnum.Supply)
            {
                DispatcherUIHelper.Run(async () =>
                {
                    var ret2 = await PickPaymentViewModel.PickRow(
                        ShowDXWindowsInteractionRequest,
                        PickPaymentViewModel.PickModeEnum.Main,
                        patientRowId: Entity.PatientRowId,
						excludePayments: InvoicePaymentEntities.Select(q => q.PaymentRowId).ToArray(),
						hasNoDistributedAmount: true);
                    if (!ret2.IsSuccess) return;

					var payment = ret2.PickRow;//.ToPayment();
                    var row = new InvoicePayment
                    {
                        RowId = Guid.NewGuid(),
                        InvoiceRowId = Entity.RowId,
						PaymentRowId = payment.RowId,
                        Amount = payment.Amount,
                        Payment = payment,
                        IsChanged = true,
                        IsNew = true,
                    };
                    //row.Description = GetDescription(row);
                    InvoicePaymentEntities.Add(row);
                    InvoicePaymentSelectedEntity = row;
                    SubscribeInvoicePaymentRow(row);
                    CalcInvoiceFields();

                    DispatcherUIHelper.Run(() =>
                    {
						BehaviorGridConrolInvoicePayment.SetCurrentColumn("Amount");
						BehaviorGridConrolInvoicePayment.ShowEditor(true);
                    });
                });
            }
        }
        public void InvoicePaymentDelete()
        {
            var row = InvoicePaymentSelectedEntity;
            var messageBoxService = this.GetRequiredService<IMessageBoxService>();
            var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
            if (ret == MessageResult.Yes)
            {
                InvoicePaymentEntities.Remove(row);
                Entity.IsChanged = true;
                CalcInvoiceFields();
            }
        }
        public bool CanInvoicePaymentDelete() => (InvoicePaymentSelectedEntity != null);

		#endregion





		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.Empty(Entity.InvoiceDate, "Invoice Date", errors);
			//ValidateHelper.Empty(Entity.PrintTemplate, "Template", errors);

			if (InvoiceItemEntities.Count == 0)
			{
				errors.Add("Invoice Items list cannot be empty");
			}

			if (ShowThirdPartyServiceProvider)
			{
				ValidateHelper.Empty(Entity.ServiceProviderRowId, "Provider", errors);
			}

			ValidateHelper.PositiveEnumerable(this, InvoiceItemEntities, (q) => q.Units, "Units", () => InvoiceItemSelectedEntity, errors);
			ValidateHelper.PositiveEnumerable(this, InvoiceItemEntities, (q) => q.Price, "Unit Price", () => InvoiceItemSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, InvoiceItemEntities, (q) => q.Tax, "Unit Tax", () => InvoiceItemSelectedEntity, errors);

			ValidateHelper.ValidateEnumerable(this, InvoiceClaimEntities, 
				(q) => ((q.ApproveDate == null && q.ApproveAmont == null) || (q.ApproveDate != null && q.ApproveAmont != null)),
				"Approve Amount and Approve Date must be both empty or both not empty", () => InvoiceClaimSelectedEntity, errors);



			ValidateHelper.EmptyEnumerable(this, InvoicePaymentEntities, (q) => q.Amount, "Amount", () => InvoicePaymentSelectedEntity, errors);

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

			//patient
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var patient = await businessService.GetPatient(Entity.PatientRowId, isAddressOnly:true);

			//calculate fields
			Entity.BillTo = patient.FullName;
			if (patient.AddressToUse == TypeHelper.AddressToUse.SecondAddress)
			{
				Entity.BillToAddress1 = patient.Address2;
				Entity.BillToCity = patient.City2;
				Entity.BillToProvince = patient.Province2;
				Entity.BillToPostCode = patient.Postcode2;
			}
			else
			{
				Entity.BillToAddress1 = patient.Address1;
				Entity.BillToCity = patient.City1;
				Entity.BillToProvince = patient.Province1;
				Entity.BillToPostCode = patient.Postcode1;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();
			updateEntity.InvoiceItems = InvoiceItemEntities.Select(q => q.GetPocoClone()).ToList();
			updateEntity.InvoicePayments = InvoicePaymentEntities.Select(q => q.GetPocoClone()).ToList();
			updateEntity.InvoiceClaims = InvoiceClaimEntities.Select(q =>
			{
				var ret = q.GetPocoClone();
				ret.InvoiceClaimDetails = q.InvoiceClaimDetails.GetPocoCloneList();
				return ret;
			}).ToList();

			//save
			var uret = IsNew ?
				await businessService.PostInvoice(updateEntity) :
				await businessService.PutInvoice(updateEntity);
			//await Task.Delay(3000);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			if (IsNew)
			{
				var serverReturn = JsonConvert.DeserializeObject<ServerReturnUpdateInvoice>(uret.ResponseJson);
				var invoiceNumber = serverReturn.InvoiceNumber;
				updateEntity.InvoiceNumber = invoiceNumber;
				Entity.InvoiceNumber = invoiceNumber;
			}

			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			ShowWaitIndicator.Hide();
			IsNew = false;
			ResetHasChange();

			//close
			if (andClose)
			{
				CloseCore(force:true);
			}

			return true;
		}


		






		
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest?.Raise(null);
		}

		bool HasChange()
		{
			if (Entity == null || InvoiceItemEntities == null || InvoiceClaimEntities == null)
			{
				return false;
			}

			if (Entity.IsChanged || InvoiceItemEntities.Any(q => q.IsChanged) || InvoiceClaimEntities.Any(q => q.IsChanged))
			{
				System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + true);
				return true;
			}
			if (IsNew && FlagAlwaysIsNewHasChanges)
			{
				System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + true);
				return true;
			}
			System.Diagnostics.Debug.WriteLine("OneModel.HasChange()=" + false);
			return false;
		}

		void ResetHasChange()
		{
			Entity.IsChanged = false;
			InvoiceItemEntities.ForEach(q => q.IsChanged = false);
			InvoiceClaimEntities.ForEach(q => q.IsChanged = false);
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

		public void Close() => CloseCore();


		public async Task ClosingEvent(CancelEventArgs arg)
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

		Guid lastNewActionBookmark;
		public void NewOpenPatient(bool isnew)
		{
			lastNewActionBookmark = (isnew ? Guid.NewGuid() : default(Guid));
			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OnePatientView,
				Param = new OnePatientViewModel.OpenParams
				{
					IsNew = isnew,
					RowId = (isnew ? default(Guid) : Entity.PatientRowId),
					NewActionBookmark = lastNewActionBookmark,
				},
			});
		}
		public void OpenPatient() => NewOpenPatient(false);
		public bool CanOpenPatient() => (!GuidHelper.IsNullOrEmpty(Entity?.PatientRowId));

		void OnMsgRowChange(MsgRowChange<Patient> msg)
		{
			if (msg.RowAction == RowAction.Update && Entity.PatientRowId == msg.Row.RowId)
			{
				Entity.Patient = msg.Row;
			}
			else if (msg.RowAction == RowAction.Insert && msg.Options == "NewActionBookmark=" + lastNewActionBookmark)
			{
				Entity.PatientRowId = msg.Row.RowId;
				Entity.Patient = msg.Row;
			}
		}





		public void SubscribeInvoiceItemRow(InvoiceItem row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcInvoiceFields();
			};

			row.OnAddRowFromPopup = (column) =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneMedicalServiceView,
					Param = "IsNew;ItemType=" + TypeHelper.MedicalItemType.ThirdPartyService,
				});
			};

			row.OnOpenDetail = () =>
			{
				var param = new OneAppointmentViewModel.OpenParams
				{
					IsNew = false,
					RowId = (Guid)row.AppointmentRowId,
					IsReadOnly = true,
				};
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneAppointmentView,
					Param = param,
				});
			};
		}

		public void SubscribeInvoiceClaimRow(InvoiceClaim row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcInvoiceFields();

				if (e.PropertyName == nameof(row.ApproveAmont))
				{
					if (row.ApproveAmont != null && row.ApproveDate == null)
					{
						row.ApproveDate = DateTime.Today;
					}
					else if (row.ApproveAmont == null)
					{
						row.ApproveDate = null;
					}
				}

				if (e.PropertyName == nameof(row.ApproveDate))
				{
					if (row.ApproveDate == null)
					{
						row.ApproveAmont = null;
						row.InvoiceClaimDetails.Clear();
					}
				}
			};

			row.OnOpenDetail = () =>
			{
				DispatcherUIHelper.Run(() =>
				{
					var param = new InvoiceClaimDetailsViewModel.OpenParams
					{
						Invoice = Entity,
						InvoiceClaim = row,
						ReadOnly = ReadOnly,
					};
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InvoiceClaimDetailsView,
						Param = param,
					});
				});
			};
		}
		

		void SubscribeInvoicePaymentRow(InvoicePayment row)
        {
            (row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
            {
                CalcInvoiceFields();
            };
            row.OnOpenDetail = () =>
            {
                ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
                {
                    ViewCode = ViewCodes.PaymentWindowView,
                    Param = new PaymentWindowViewModel.OpenParams { IsNew = false, RowId = row.PaymentRowId, SelectInvoiceRowId = row.InvoiceRowId },
                });
            };
        }

		void SubscribeInvoiceRefundRow(InvoiceRefund row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				CalcInvoiceFields();
			};
			row.OnOpenDetail = () =>
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.RefundWindowView,
					Param = new RefundWindowViewModel.OpenParams { IsNew = false, RowId = row.RefundRowId, SelectInvoiceRowId = row.InvoiceRowId },
				});
			};
		}


		List<InvoiceItem> lastInvoiceItemEntities;
		void BuildLastInvoiceItemEntities()
		{
			lastInvoiceItemEntities = InvoiceItemEntities.Select(q => q.Clone()).ToList();
		}

		public void CalcInvoiceFields()
		{
			CalcInvoiceClaimsForHasNoCoverage();

			Invoice.CalcTotalFields(Entity, InvoiceItemEntities, InvoiceClaimEntities);
			CalcInvoiceDate();

			Entity.PaymentTotal = InvoicePaymentEntities.Sum(q => q.Amount);
            Entity.PaymentRequest = Entity.Total - Entity.PaymentTotal;

			BehaviorGridConrolInvoiceItem.UpdateTotalSummary();
			BehaviorGridConrolInvoiceClaim.UpdateTotalSummary();
			BehaviorGridConrolInvoicePayment.UpdateTotalSummary();
			

			BuildLastInvoiceItemEntities();
		}

		void CalcInvoiceClaimsForHasNoCoverage()
		{
			if (Entity.HasNoCoverage)
			{
				if (InvoiceClaimEntities.Count > 1) throw new LogicalException();
				if (InvoiceClaimEntities.Count == 0)
				{
					InvoiceClaimEntities.Add(new InvoiceClaim
					{
						RowId = Guid.NewGuid(),
						InvoiceRowId = Entity.RowId,
						SentDate = DateTime.Today,
						ApproveDate = DateTime.Today,
					});
				}
				var irow = InvoiceClaimEntities.First();
				var total = Invoice.GetInvoiceItemsTotal(InvoiceItemEntities);
				irow.SentAmont = total;
				irow.ApproveAmont = total;
			}
		}


		void CalcInvoiceDate()
		{
			if (Entity.InvoiceType != TypeHelper.InvoiceType.Appointment)
			{
				return;
			}

			var zzz = (from a in InvoiceItemEntities join b in lastInvoiceItemEntities on a.RowId equals b.RowId select new { a, b }).ToArray();

			var count = 
				(from a in InvoiceItemEntities join b in lastInvoiceItemEntities on a.RowId equals b.RowId select new { a, b })
				.Count(q => q.a.ItemDate == q.b.ItemDate);
			if (InvoiceItemEntities.Count == count && lastInvoiceItemEntities.Count == count)
			{
				return; //даты не менялись
			}

			Entity.InvoiceDate = InvoiceItemEntities.Where(q => q.ItemDate != null).Max(q => q.ItemDate);
		}




		public void AddRowFromPopup(string column)
		{
			if (column == nameof(Entity.Status1RowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneInvoiceStatusView,
					Param = "IsNew",
				});
			}

			else if (column == nameof(Entity.ThirdPartyServiceProviderRowId))
			{
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneThirdPartyServiceProviderView,
					Param = "IsNew",
				});
			}
		}

		public void PreviewKeyDown(KeyEventArgs e)
		{
			if (e.Key == Key.R && KeyboardHelper.IsShiftPressed && KeyboardHelper.IsControlPressed && KeyboardHelper.IsAltPressed)
			{
				IsVisibilityRate = !IsVisibilityRate;
			}
		}





		public void PrintInvoice()
		{
			DispatcherUIHelper.Run(async () =>
			{
				NLog.vv();
				if (string.IsNullOrEmpty(Entity.PrintTemplate))
				{
					MessageBoxService.ShowError("Select Print Template before print invoice");
					return;
				}

				NLog.vv(() => Entity.IsNew + " --- " + Entity.RowId);
				var ret = await OnClose(true);
				NLog.vv(() => ret);
				if (ret == OnCloseReturn.Yes)
				{
					NLog.vv(() => Entity.RowId);
					ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
					{
						ViewCode = ViewCodes.InvoicePrintView,
						Param = new InvoicePrintViewModel.OpenParams { RowId = Entity.RowId },
					});
				}
			});
		}






	}

}
