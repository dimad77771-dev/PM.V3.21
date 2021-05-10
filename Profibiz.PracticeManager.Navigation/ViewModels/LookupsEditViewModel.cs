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
using PropertyChanged;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Patients.BusinessService;

namespace Profibiz.PracticeManager.Navigation.ViewModels
{
	[POCOViewModel]
	public class LookupsEditViewModel : ViewModelBase 
	{
		#region Services
		ILookupsBusinessService lookupsBusinessService;
		IPatientsBusinessService businessService;
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual Int32 TabControlSelectedIndex { get; set; }
		public virtual ObservableCollection<MedicalServicesOrSupply> MedicalServicesEntities { get; set; }
		public virtual MedicalServicesOrSupply MedicalServicesSelectedEntity { get; set; }
		public virtual ObservableCollection<InsuranceProvider> InsuranceProvidersEntities { get; set; }
		public virtual InsuranceProvider InsuranceProvidersSelectedEntity { get; set; }
		public virtual ObservableCollection<ProfessionalAssociation> ProfessionalAssociationsEntities { get; set; }
		public virtual ProfessionalAssociation ProfessionalAssociationsSelectedEntity { get; set; }


		public LookupsEditViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			lookupsBusinessService = _lookupsBusinessService;
			businessService = _businessService;
		}

		public void OnOpen(OpenParams param)
		{
			OpenParam = param;
			LoadData();
		}

		async void LoadData()
		{
			ShowWaitIndicator.Show();
			await lookupsBusinessService.UpdateAllLookups();
			MedicalServicesEntities = new ObservableCollection<MedicalServicesOrSupply>(LookupDataProvider.Instance.MedicalServices.OrderBy(q => q.ItemType).OrderBy(q => q.Name));
			InsuranceProvidersEntities = new ObservableCollection<InsuranceProvider>(LookupDataProvider.Instance.InsuranceProviders.OrderBy(q => q.CompanyName));
			ProfessionalAssociationsEntities = new ObservableCollection<ProfessionalAssociation>(LookupDataProvider.Instance.ProfessionalAssociations.OrderBy(q => q.Name));
			ResetHasChange();
			//await Task.Delay(5000);
			ShowWaitIndicator.Hide();
		}

		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));
		public void Close() => CloseCore();



		bool Validate()
		{
			List<string> errors = new List<string>();

			ValidateHelper.EmptyEnumerable(this, MedicalServicesEntities, (q) => q.Code, "\"CODE\" on table \"Medical Services/Supplies\"", () => MedicalServicesSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, MedicalServicesEntities, (q) => q.Name, "\"NAME\" on table \"Medical Services/Supplies\"", () => MedicalServicesSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, MedicalServicesEntities, (q) => q.ItemType, "\"ITEM TYPE\" on table \"Medical Services/Supplies\"", () => MedicalServicesSelectedEntity, errors);

			ValidateHelper.EmptyEnumerable(this, InsuranceProvidersEntities, (q) => q.Code, "\"CODE\" on table \"Insurance Providers\"", () => InsuranceProvidersSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, InsuranceProvidersEntities, (q) => q.CompanyName, "\"COMPANY NAME\" on table \"Insurance Providers\"", () => InsuranceProvidersSelectedEntity, errors);

			ValidateHelper.EmptyEnumerable(this, ProfessionalAssociationsEntities, (q) => q.Code, "\"CODE\" on table \"Professional Associations\"", () => ProfessionalAssociationsSelectedEntity, errors);
			ValidateHelper.EmptyEnumerable(this, ProfessionalAssociationsEntities, (q) => q.Name, "\"NAME\" on table \"Professional Associations\"", () => ProfessionalAssociationsSelectedEntity, errors);


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
			//validate
			if (!Validate())
			{
				return false;
			}

			//update.updateMedicalServicesEntities
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var updateMedicalServicesEntities = MedicalServicesEntities.Select(q => q.GetPocoClone()).ToArray();
			var uret1 = await lookupsBusinessService.PutMedicalServicesOrSupplies(updateMedicalServicesEntities);
			ShowWaitIndicator.Hide();
			if (!uret1.Validate(MessageBoxService)) return false;
			SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.MedicalConditions);


			//update.updateMedicalServicesEntities
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var updateInsuranceProvidersEntities = InsuranceProvidersEntities.Select(q => q.GetPocoClone()).ToArray();
			var uret2 = await lookupsBusinessService.PutInsuranceProviders(updateInsuranceProvidersEntities);
			ShowWaitIndicator.Hide();
			if (!uret2.Validate(MessageBoxService)) return false;
			SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.InsuranceProviders);

			//update.updateMedicalServicesEntities
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var updateProfessionalAssociationsEntities = ProfessionalAssociationsEntities.Select(q => q.GetPocoClone()).ToArray();
			var uret3 = await lookupsBusinessService.PutProfessionalAssociations(updateProfessionalAssociationsEntities);
			ShowWaitIndicator.Hide();
			if (!uret3.Validate(MessageBoxService)) return false;
			SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.ProfessionalAssociations);

			////save
			//MessengerHelper.SendMsgRowChange(Entity, IsNew);
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
			CloseInteractionRequest.Raise(null);
		}

		bool HasChange()
		{
			return (
						(MedicalServicesEntities.Any(q => q.IsChanged)) ||
						(InsuranceProvidersEntities.Any(q => q.IsChanged)) ||
						(ProfessionalAssociationsEntities.Any(q => q.IsChanged)) ||
						false
					);
		}

		void ResetHasChange()
		{
			MedicalServicesEntities.ForEach(q => q.IsChanged = false);
			InsuranceProvidersEntities.ForEach(q => q.IsChanged = false);
			ProfessionalAssociationsEntities.ForEach(q => q.IsChanged = false);

			MedicalServicesEntities.ForEach(q => q.IsNew = false);
			InsuranceProvidersEntities.ForEach(q => q.IsNew = false);
			ProfessionalAssociationsEntities.ForEach(q => q.IsNew = false);
		}

		void SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum table)
		{
			var msg = new MsgRowLookupUpdate(table);
			MessengerHelper.Send(msg);
		}


		async Task<bool> OnClose(bool showOKCancel = false)
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


		object GetCurrentTable()
		{
			if (TabControlSelectedIndex == 0)
			{
				return MedicalServicesEntities;
			}
			else if (TabControlSelectedIndex == 1)
			{
				return InsuranceProvidersEntities;
			}
			else if (TabControlSelectedIndex == 2)
			{
				return ProfessionalAssociationsEntities;
			}
			return null;
		}


		public void NewRow()
		{
			if (GetCurrentTable() == MedicalServicesEntities)
			{
				var row = new MedicalServicesOrSupply { RowId = Guid.NewGuid(), IsNew = true };
				MedicalServicesEntities.Add(row);
				MedicalServicesSelectedEntity = row;
			}
			else if (GetCurrentTable() == InsuranceProvidersEntities)
			{
				var row = new InsuranceProvider { RowId = Guid.NewGuid(), IsNew = true };
				InsuranceProvidersEntities.Add(row);
				InsuranceProvidersSelectedEntity = row;
			}
			else if (GetCurrentTable() == ProfessionalAssociationsEntities)
			{
				var row = new ProfessionalAssociation { RowId = Guid.NewGuid(), IsNew = true };
				ProfessionalAssociationsEntities.Add(row);
				ProfessionalAssociationsSelectedEntity = row;
			}
		}
		async public void DeleteRow()
		{
			if (GetCurrentTable() == MedicalServicesEntities)
			{
				var row = MedicalServicesSelectedEntity;
				var ret = MessageBoxService.Confirmation("Do you want to delete row \"" + row.Code + "\"?");
				if (ret == MessageResult.Yes)
				{
					if (row.IsNew)
					{
						MedicalServicesEntities.Remove(row);
					}
					else
					{
						if (!await OnClose(showOKCancel: true)) return;

						ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
						var uret = await lookupsBusinessService.DeleteMedicalServicesOrSupply(row);
						ShowWaitIndicator.Hide();
						if (!uret.Validate(MessageBoxService)) return;
						MedicalServicesEntities.Remove(row);
						SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.MedicalConditions);
					}
				}
			}
			else if (GetCurrentTable() == InsuranceProvidersEntities)
			{
				var row = InsuranceProvidersSelectedEntity;
				var ret = MessageBoxService.Confirmation("Do you want to delete row \"" + row.Code + "\"?");
				if (ret == MessageResult.Yes)
				{
					if (row.IsNew)
					{
						InsuranceProvidersEntities.Remove(row);
					}
					else
					{
						if (!await OnClose(showOKCancel: true)) return;

						ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
						var uret = await lookupsBusinessService.DeleteInsuranceProvider(row);
						ShowWaitIndicator.Hide();
						if (!uret.Validate(MessageBoxService)) return;
						InsuranceProvidersEntities.Remove(row);
						SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.InsuranceProviders);
					}
				}
			}
			else if (GetCurrentTable() == ProfessionalAssociationsEntities)
			{
				var row = ProfessionalAssociationsSelectedEntity;
				var ret = MessageBoxService.Confirmation("Do you want to delete row \"" + row.Code + "\"?");
				if (ret == MessageResult.Yes)
				{
					if (row.IsNew)
					{
						ProfessionalAssociationsEntities.Remove(row);
					}
					else
					{
						if (!await OnClose(showOKCancel: true)) return;

						ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
						var uret = await lookupsBusinessService.DeleteProfessionalAssociation(row);
						ShowWaitIndicator.Hide();
						if (!uret.Validate(MessageBoxService)) return;
						ProfessionalAssociationsEntities.Remove(row);
						SendMsgRowLookupUpdate(MsgRowLookupUpdate.TableEnum.ProfessionalAssociations);
					}
				}
			}
		}
		public bool CanDeleteRow()
		{
			if (
					(GetCurrentTable() == MedicalServicesEntities && MedicalServicesEntities != null) ||
					(GetCurrentTable() == InsuranceProvidersEntities && InsuranceProvidersSelectedEntity != null) ||
					(GetCurrentTable() == ProfessionalAssociationsEntities && ProfessionalAssociationsSelectedEntity != null) ||
					false
				)
			{
				return true;
			}
			return false;
		}











		public class OpenParams
		{
		}

	}

}
