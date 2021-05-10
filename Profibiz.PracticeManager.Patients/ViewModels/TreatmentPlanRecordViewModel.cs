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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Xpf.Core;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class TreatmentPlanRecordViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public virtual Guid PatientRowId { get; set; }
		public virtual ObservableCollection<TreatmentPlanRecord> Entities { get; set; }
		public virtual TreatmentPlanRecord Entity { get; set; }



		public TreatmentPlanRecordViewModel() : base()
		{
		}


		public async Task LoadData()
		{
			Entities = (await GetList()).ToObservableCollection();
			ResetHasChange();
			if (Entities.Any())
			{
				SetSelectedEntity(Entities[0]);
			}
			else
			{
				NewCore();
				ResetHasChange();
			}

			Entities.ForEach(q => TreatmentPlanRecordSubscribeRow(q));
		}

		bool inTreatmentPlanRecordSubscribeRowProc = false;
		void TreatmentPlanRecordSubscribeRow(TreatmentPlanRecord row)
		{
			(row as INotifyPropertyChanged).PropertyChanged += (s, e) =>
			{
				if (!inTreatmentPlanRecordSubscribeRowProc)
				{
					var propname = e.PropertyName;
					if (propname.ToLower().EndsWith("_m") || propname.ToLower().EndsWith("_p"))
					{
						inTreatmentPlanRecordSubscribeRowProc = true;

						var allprops = typeof(TreatmentPlanRecord).GetProperties();
						var prop = allprops.Single(q => q.Name == propname);
						var propname2 = propname.Substring(0, propname.Length - 2) + (propname.ToLower().EndsWith("_m") ? "_p" : "_m");
						var prop2 = allprops.Single(q => q.Name == propname2);

						var val = (bool)prop.GetValue(s);
						var val2 = (bool)prop2.GetValue(s);

						if (val == true && val2 == true)
						{
							prop2.SetValue(s, false);
						}

						inTreatmentPlanRecordSubscribeRowProc = false;
					}
				}
			};
		}



		async Task<List<TreatmentPlanRecord>> GetList()
		{
			var qry = "patientRowId=" + PatientRowId.ToWebQuery();
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetTreatmentPlanRecordList(qry));
			rows = rows.OrderByDescending(q => q.RecordDate).ToList();
			return rows;
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
					return await Save() ? OnCloseReturn.Yes : OnCloseReturn.Cancel;
				}
				else throw new ArgumentException();
			}
			else
			{
				return OnCloseReturn.Yes;
			}
		}


		public bool HasChange()
		{
			if (Entity == null)
			{
				return false;
			}
			if (Entity.IsChanged)
			{
				return true;
			}
			return false;
		}
		void ResetHasChange()
		{
			Entities.ForEach(q =>
			{
				q.IsChanged = false;
				//q.IsNew = false;
			});
		}

		bool Validate()
		{
			if (Entity == null) return true;

			List<string> errors = new List<string>();
			ValidateHelper.Empty(Entity.RecordDate, "Date", errors);

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}
			return true;
		}


		public async Task<bool> Save()
		{
			if (Entity == null) return true;

			//validate
			if (!Validate())
			{
				return false;
			}

			//updateEntity
			var updateEntity = Entity.GetPocoClone();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = Entity.IsNew ?
				await businessService.PostTreatmentPlanRecord(updateEntity) :
				await businessService.PutTreatmentPlanRecord(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, Entity.IsNew);
			Entity.IsNew = false;
			ResetHasChange();


			return true;
		}

		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		bool forceClose;
		void CloseCore(bool force = false)
		{
			this.forceClose = force;
			CloseInteractionRequest.Raise(null);
		}



		public void Delete()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var row = Entity;
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
					var inventoryRowId = row.RowId;
					var uret = await businessService.DeleteTreatmentPlanRecord(row.RowId);
					ShowWaitIndicator.Hide();
					if (!uret.Validate(messageBoxService)) return;

					Entities.Remove(row);
					if (Entities.Any())
					{
						Entity = Entities[0];
					}
				}
			});
		}
		public bool CanDelete() => (Entity != null);


		void ClearNewRows()
		{
			var delrows = Entities.Where(q => q.IsNew).ToArray();
			Entities.RemoveRange(delrows);
		}

		void NewCore()
		{
			var nrow = new TreatmentPlanRecord
			{
				RowId = Guid.NewGuid(),
				PatientRowId = PatientRowId,
				RecordDate = DateTime.Today,
				IsNew = true,
			};
			Entities.Insert(0, nrow);
			Entity = nrow;
			TreatmentPlanRecordSubscribeRow(nrow);
		}


		public void New()
		{
			DispatcherUIHelper.Run(async () =>
			{
				var ret = await OnClose(showOKCancel: true);
				if (ret != OnCloseReturn.Cancel)
				{
					ClearNewRows();
					NewCore();
				}
			});
		}

		public void BeforeLayoutRefresh(CancelRoutedEventArgs e)
		{
			e.Cancel = cancelBeforeLayoutRefresh;
		}
		bool cancelBeforeLayoutRefresh = true;
		void SetSelectedEntity(TreatmentPlanRecord row)
		{
			cancelBeforeLayoutRefresh = false;
			Entity = row;
			cancelBeforeLayoutRefresh = true;
		}


		public void MouseClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var hitInfo = BehaviorGridConrol.GetCalcHitInfo(e);
				if (hitInfo == null) return;

				var row = BehaviorGridConrol.GetRow<TreatmentPlanRecord>(hitInfo.RowHandle);
				if (row != null && row != Entity)
				{
					//SetSelectedEntity(row);
					var ret = await OnClose(showOKCancel: true);
					if (ret != OnCloseReturn.Cancel)
					{
						SetSelectedEntity(row);
					}
				}
			});
		}
	}


}
