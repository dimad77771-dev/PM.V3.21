using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
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
using System.Diagnostics;
using XtraAppointment = DevExpress.XtraScheduler.Appointment;
using TimeOfDayInterval = DevExpress.XtraScheduler.TimeOfDayInterval;
using Xtra = DevExpress.XtraScheduler;
using Profibiz.PracticeManager.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Windows.Media;
using Profibiz.PracticeManager.Patients.BusinessService;
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class AppointmentsSchedulerViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService = ServiceHelper.GetInstance<IPatientsBusinessService>();
		ILookupsBusinessService lookupsService = ServiceHelper.GetInstance<ILookupsBusinessService>();
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		#region Constaints
		public static readonly Guid ALL_APPOINTMENTBOOKS = default(Guid);
		public static readonly String ALL_APPOINTMENTBOOKS_NAME = "ALL APPOINTMENT BOOKS";
		#endregion

		public OpenParams OpenParam { get; set; }
		public virtual ObservableCollection<HospitalAppointment> Appointments { get; set; }
		public virtual ObservableCollection<XtraAppointment> SelectedAppointments { get; set; } = new ObservableCollection<XtraAppointment>();
		XtraAppointment SelectedAppointment => (SelectedAppointments != null && SelectedAppointments.Count == 1) ? SelectedAppointments[0] : null;

		public virtual double DayViewVerticalCellMinHeight { get; set; } = 20;
		public virtual TimeSpan TimeScale { get; set; } = new TimeSpan(0, 30, 0);
		public virtual TimeOfDayInterval VisibleTimeInterval { get; set; } = TimeOfDayInterval.Day;

		public virtual Xtra.WeekDays WeekWorkDays { get; set; } = Xtra.WeekDays.EveryDay;
		public virtual SchedulerControlManager SchedulerControlManager { get; set; } = new SchedulerControlManager();
		public virtual DevExpress.Xpf.Scheduler.SchedulerControl SchedulerControl999 { get; set; } = new DevExpress.Xpf.Scheduler.SchedulerControl();

		public virtual ObservableCollection<AppointmentBook> AllAppointmentBooks { get; set; }
		public virtual AppointmentBook SelectedAppointmentBook { get; set; }

		public virtual ObservableCollection<Doctor> Doctors { get; set; }
		public virtual ObservableCollection<Doctor> AllDoctors { get; set; }
		public virtual ObservableCollection<ServiceProvider> AllServiceProviders { get; set; }
		public virtual ObservableCollection<InsuranceProvider> AllInsuranceProviders { get; set; }

		public virtual ObservableCollection<Doctor> RibbonSpecialistListItems { get; set; }
		public virtual Int32 RibbonSpecialistListColumnCount { get; set; }

		public virtual ObservableCollection<InsuranceProvider> RibbonInsuranceFilterListItems { get; set; }
		public virtual Int32 RibbonInsuranceFilterListColumnCount { get; set; }


		public virtual ObservableCollection<InsuranceProvidersViewGroup> AllInsuranceProvidersViewGroups { get; set; }
		public virtual InsuranceProvidersViewGroup SelectedInsuranceProvidersViewGroup { get; set; }
		


		public enum ViewModeEnum { AppointmentBooks, InsuranceGroups, OnePatient }
		public virtual ViewModeEnum ViewMode { get; set; }
		public virtual Patient OnePatient { get; set; }
		public bool IsOnePatientAllAppointment => 
			(ViewMode == ViewModeEnum.OnePatient && SelectedAppointmentBook != null && SelectedAppointmentBook?.RowId == ALL_APPOINTMENTBOOKS);
		public bool IsOnePatientOneAppointment => 
			(ViewMode == ViewModeEnum.OnePatient && SelectedAppointmentBook != null && SelectedAppointmentBook?.RowId != ALL_APPOINTMENTBOOKS);
		public List<Appointment> AllAppointmentInAppointmentBook;

		public Boolean IsEnableControl { get; set; }
		public Boolean IsMainRibbonShow { get; set; }
		public Boolean IsSmallRibbonHide { get; set; }
		public Boolean AllAppointmentBooksComboBoxShow => (ViewMode == ViewModeEnum.OnePatient);
		public String RibbonStyle => (ViewMode == ViewModeEnum.OnePatient ? "TabletOffice" : "Office2010");
		public Boolean RibbonHideHeaderAndTabsGrid => (ViewMode == ViewModeEnum.OnePatient);
		public Boolean IsVisibleRibbonEditAppointments => (ViewMode != ViewModeEnum.OnePatient);
		public Boolean IsVisibleRibbonActiveView => true;
		public Boolean IsVisibleRibbonGroupBy => (ViewMode == ViewModeEnum.AppointmentBooks);
		public Boolean IsVisibleRibbonAppointmentBook => (ViewMode == ViewModeEnum.AppointmentBooks);
		public Boolean IsVisibleRibbonSpecialists => (ViewMode == ViewModeEnum.AppointmentBooks);
		public Boolean IsVisibleRibbonInsuranceFilters => (ViewMode == ViewModeEnum.AppointmentBooks);
		public Boolean IsVisibleRibbonInsuranceProvidersViewGroup => (ViewMode == ViewModeEnum.InsuranceGroups);

		public virtual DateTime FilterFrom { get; set; }
		public virtual DateTime FilterTo { get; set; }




		public int Test123 = 0;
		public AppointmentsSchedulerViewModel() : base()
		{
			NLog.vv();

			var ret = GlobalSettings.Instance.Appointment.GetAppointmentsViewDateFilter();
			FilterFrom = ret.FilterFrom;
			FilterTo = ret.FilterTo;

			this.PropertyChanged += AppointmentsSchedulerViewModel_PropertyChanged;
			CellBackgroundBrushSelector = new CellBackgroundBrushSelectorClass(this);
		}

		private void AppointmentsSchedulerViewModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			var b = e.PropertyName;
		}

		public async Task OnOpen(object param)
		{
			NLog.vv(() => param);
			if (QueryHelper.IsStringParam(param))
			{
				if ((string)param == "ViewMode=OnePatient")
				{
					ViewMode = ViewModeEnum.OnePatient;
				}
				else if ((string)param == "ViewMode=InsuranceGroups")
				{
					ViewMode = ViewModeEnum.InsuranceGroups;
				}
				else if ((string)param == "ViewMode=AppointmentBooks")
				{
					ViewMode = ViewModeEnum.AppointmentBooks;
				}
				else throw new ArgumentException();
			}
			else
			{
				OpenParam = (param as OpenParams) ?? new OpenParams();
				ViewMode = ViewModeEnum.InsuranceGroups;

				//if (OpenParam.OnePatient != null)
				//{
				//	ViewMode = ViewModeEnum.OnePatient;
				//	OnePatient = OpenParam.OnePatient;
				//}
			}

			MessengerHelper.Register<MsgRowChange<Appointment>>(this, OnMsgRowChangeAppointment);
			MessengerHelper.Register<MsgRowChange<Patient>>(this, OnMsgRowChangePatient);

			GroupType = Xtra.SchedulerGroupType.None;
			IsSmallRibbonHide = (ViewMode != ViewModeEnum.OnePatient);
			await LoadData();
		}



		async Task LoadData()
		{
			NLog.vv();

			if (ViewMode == ViewModeEnum.OnePatient && OnePatient == null)
			{
				IsEnableControl = false;
				return;
			}

			IsEnableControl = true;
			ShowWaitIndicator.Show();

			await lookupsService.UpdateAllLookups();
			AllAppointmentBooks = LookupDataProvider.Instance.AppointmentBooks.OrderBy(q => q.DisplayOrder).ToObservableCollection();
			AllServiceProviders = LookupDataProvider.Instance.ServiceProvidersEx.ToObservableCollection();
			AllInsuranceProvidersViewGroups = LookupDataProvider.Instance.InsuranceProvidersViewGroups.ToObservableCollection();
			if (ViewMode != ViewModeEnum.AppointmentBooks)
			{
				AllAppointmentBooks.Insert(0, new AppointmentBook
				{
					RowId = ALL_APPOINTMENTBOOKS,
					Name = ALL_APPOINTMENTBOOKS_NAME,
					DisplayOrder = -100,
					Interval = 15,
					StartAt = new DateTime(2000, 1, 1, 0, 0, 0),
					FinishAt = new DateTime(2000, 1, 1, 0, 0, 0),
				});
			}

			SelectedAppointmentBook = AllAppointmentBooks.FirstOrDefault();
			SelectedInsuranceProvidersViewGroup = AllInsuranceProvidersViewGroups.FirstOrDefault();


			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();
		}

		async Task LoadAppointmentData()
		{
			NLog.vv();

			ShowWaitIndicator.Show();

			var appointmentBookRowId = (SelectedAppointmentBook?.RowId == ALL_APPOINTMENTBOOKS ? (Guid?)null : SelectedAppointmentBook?.RowId);
			var patientRowId = OnePatient?.RowId;
			var insuranceProvidersViewGroupRowId = SelectedInsuranceProvidersViewGroup?.RowId;
			var task2 = ViewMode == ViewModeEnum.InsuranceGroups ?
				businessService.GetAppointmentList(insuranceProvidersViewGroupRowId: insuranceProvidersViewGroupRowId, startFrom: FilterFrom, startTo: FilterTo ) :
				businessService.GetAppointmentList(appointmentBookRowId: appointmentBookRowId, patientRowId: patientRowId, startFrom: FilterFrom, startTo: FilterTo );
			var task3 = TaskHelper.IfTrue(() => IsOnePatientOneAppointment,
				businessService.GetAppointmentList(appointmentBookRowId: appointmentBookRowId, patientRowId: null));
			//var task444 = Task.Delay(10000);
			await TaskHelper.WhenAll(task2, task3);//, task444);

			var allAppointments = task2.Result;
			if (task3 != null)
			{
				AllAppointmentInAppointmentBook = task3.Result;
			}

			AllDoctors = AllServiceProviders.Select((q, n) => new Doctor
			{
				Entity = q,
				Id = q.RowId,
				Name = q.FullName,
				BackgroundColor = q.AppointmentBackgroundColor,
				ForegroundColor = q.AppointmentForegroundColor,
			}).ToObservableCollection();

			Doctors = new ObservableCollection<Doctor>(
				AllDoctors.Where(q => 
					ViewMode == ViewModeEnum.AppointmentBooks ? 
					SelectedAppointmentBook.ServiceProviders.Select(z => z.RowId).Contains(q.Id) || q.Entity.RowId == ServiceProvider.NO_DOCTOR : 
					true));

			AllInsuranceProviders = LookupDataProvider.Instance.InsuranceProviders.ToObservableCollection();


			

			RibbonSpecialistListItems = Doctors.Where(q => q.Id != ServiceProvider.NO_DOCTOR).Select(q => q).ToObservableCollection();
			RibbonSpecialistListColumnCount = Math.Max((RibbonSpecialistListItems.Count + 1) / 2, 1);
			//var zz11 = await businessService.GetPrintDocuments(""); RibbonSpecialistListItems = zz11.Select(q => new Doctor { Name = q.Name }).ToObservableCollection();
			//RibbonSpecialistListColumnCount = 5;
			RibbonInsuranceFilterListItems = AllInsuranceProviders.Select(q => q).ToObservableCollection();
			RibbonInsuranceFilterListColumnCount = Math.Min(4, Math.Max((RibbonInsuranceFilterListItems.Count + 1) / 2, 1));

			Appointments = allAppointments.Select(q => Appointment2HospitalAppointment(q)).ToObservableCollection();

			CustomizeSchedulerView();

			await DaysInfoBuild(forInit:true);
			//LookupDataProvider.IsPublicHoliday

			ShowWaitIndicator.Hide();
			DXSplashScreenHelper.Hide();

			//await Task.Delay(5000);
			//throw new Exception("333333333333333333333333333");
		}

		public void Filter()
		{
			DispatcherUIHelper.Run(async () =>
			{
				GlobalSettings.Instance.Appointment.SetAppointmentsViewDateFilter(FilterFrom, FilterTo);
				await LoadData();
				await LoadAppointmentData();
			});
		}

		public async Task SetOnePatient(Patient newOnePatient)
		{
			OnePatient = newOnePatient;
			await LoadData();
		}

		protected void OnSelectedAppointmentBookChanged(AppointmentBook oldSelectedAppointmentBook)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (oldSelectedAppointmentBook == null || SelectedAppointmentBook.RowId != oldSelectedAppointmentBook.RowId)
				{
					await LoadAppointmentData();
				}
			});
		}

		protected void OnSelectedInsuranceProvidersViewGroupChanged(InsuranceProvidersViewGroup oldSelectedInsuranceProvidersViewGroup)
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (oldSelectedInsuranceProvidersViewGroup == null || SelectedInsuranceProvidersViewGroup.RowId != oldSelectedInsuranceProvidersViewGroup.RowId)
				{
					await LoadAppointmentData();
				}
			});
		}

		public HospitalAppointment Appointment2HospitalAppointment(Appointment row)
		{
			var ret = new HospitalAppointment
			{
				Entity = row,
				RowId = row.RowId,
				StartDate = row.Start,
				EndDate = row.Finish,
				DoctorId = row.ServiceProviderRowId ?? ServiceProvider.NO_DOCTOR,
				MedicalServicesOrSupplyRowId = row.MedicalServicesOrSupplyRowId,
				InsuranceProviderRowId = row.InsuranceProviderRowId,
				Patient = row.Patient,
				PatientName = row.Patient.FullName,
				Notes = row.Notes,
				Location = row.Description,
				InsuranceNumber = row.RefNumber,
				ParentViewModel = this,
			};
			ret.Doctor = AllDoctors.Single(z => z.Id == ret.DoctorId);
			ret.MedicalServicesOrSupply = LookupDataProvider.FindMedicalService(ret.MedicalServicesOrSupplyRowId);
			ret.InsuranceProvider = AllInsuranceProviders.SingleOrDefault(z => z.RowId == ret.InsuranceProviderRowId);
			//if (ret.InsuranceProvider == null) ret.InsuranceProvider = AllInsuranceProviders[0];
			ret.AppointmentBook = AllAppointmentBooks.Single(z => z.RowId == row.AppointmentBookRowId);
			return ret;
		}

		void OnMsgRowChangeAppointment(MsgRowChange<Appointment> msg)
		{
			MessengerHelper.RunAction(this, async () =>
			{
				if (AllDoctors == null)
				{
					//дикий глюк!
					NLog.vv(() => "AppointmentsSchedulerViewModel.OnMsgRowChangeAppointment=PROBLEM");
					return;
				}

				var row = msg.Row;
				var action = msg.RowAction;
				if (action == RowAction.Update)
				{
					var index = Appointments.FindIndex(q => q.RowId == row.RowId);
					if (index >= 0)
					{
						var nrow = Appointment2HospitalAppointment(row);
						Appointments[index] = nrow;
						SchedulerControlManager.SetSelection(nrow.StartDate, nrow.EndDate, nrow.DoctorId);
					}

					if (AllAppointmentInAppointmentBook != null)
					{
						var index2 = AllAppointmentInAppointmentBook.FindIndex(q => q.RowId == row.RowId);
						if (index2 >= 0)
						{
							AllAppointmentInAppointmentBook[index2] = row;
							UpdateCellBackgroundBrush();
						}
					}
				}
				else if (action == RowAction.Insert)
				{
					//AllDoctors = null;
					//AllDoctors = (new Doctor[] { null, null, null }).ToObservableCollection();
					var nrow = Appointment2HospitalAppointment(row);
					Appointments.Add(nrow);
					SchedulerControlManager.SetSelection(nrow.StartDate, nrow.EndDate, nrow.DoctorId);

					if (AllAppointmentInAppointmentBook != null)
					{
						AllAppointmentInAppointmentBook.Add(row);
						UpdateCellBackgroundBrush();
					}
				}

				await DaysInfoBuild();
			});
		}

		void OnMsgRowChangePatient(MsgRowChange<Patient> msg)
		{
			MessengerHelper.RunAction(this, () =>
			{
				if (msg.RowAction == RowAction.Update)
				{
					if (Appointments == null) return;

					var appointments = Appointments.Where(q => q.Patient?.RowId == msg.Row.RowId).ToArray();
					foreach (var appointment in appointments)
					{
						var index = Appointments.IndexOf(appointment);
						appointment.Entity.Patient = msg.Row;
						var nrow = Appointment2HospitalAppointment(appointment.Entity);
						Appointments[index] = nrow;
					}
				}
			});
		}




		public void NewEntity(bool isNotRegisteredMode = false) => AddEditEntity(null, isNotRegisteredMode);
		public void EditEntity()
		{
			if (SelectedAppointment != null)
			{
				AddEditEntity(XtraAppointment2Appointment(SelectedAppointment));
			}
		}
		public void MouseDoubleClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			e.Handled = true;
			EditEntity();
		}
		void AddEditEntity(Appointment row, bool isNotRegisteredMode = false)
		{
			var param = new OneAppointmentViewModel.OpenParams
			{
				IsNew = (row == null),
				RowId = (row == null ? default(Guid) : row.RowId),
				AppointmentBookRowId = SelectedAppointmentBook.RowId,
				DaysInfo = DaysInfo,
				InsuranceProvidersViewGroupRowId = (ViewMode == ViewModeEnum.InsuranceGroups ? SelectedInsuranceProvidersViewGroup?.RowId : null),
				IsNotRegisteredMode = isNotRegisteredMode,
			};
			if (row == null)
			{
				param.NewStart = SchedulerControlManager.SelectedInterval?.Start;
				param.NewFinish = SchedulerControlManager.SelectedInterval?.End;
				if (ViewMode == ViewModeEnum.AppointmentBooks)
				{
					if (SchedulerControlManager.SelectedResourceId is Guid)
					{
						param.NewServiceProviderRowId = (Guid)SchedulerControlManager.SelectedResourceId;
					}
				}
				if (ViewMode == ViewModeEnum.OnePatient)
				{
					param.NewPatient = OnePatient;
				}
			}
			if (ViewMode == ViewModeEnum.OnePatient)
			{
				param.IsLockPatient = true;
			}

			ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
			{
				ViewCode = ViewCodes.OneAppointmentView,
				Param = param,
			});
		}
		public void DeleteEntity()
		{
			DispatcherUIHelper.Run(async () =>
			{
				if (SelectedAppointment != null)
				{
					var row = XtraAppointment2Appointment(SelectedAppointment);
					var ret = MessageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Appointment"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
					if (ret == MessageResult.Yes)
					{
						ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
						var uret = await businessService.DeleteAppointment(row.RowId);
						ShowWaitIndicator.Hide();
						if (!uret.Validate(MessageBoxService)) return;
						var arow = Appointments.Single(q => q.RowId == row.RowId);
						Appointments.Remove(arow);
						if (AllAppointmentInAppointmentBook != null)
						{
							AllAppointmentInAppointmentBook.RemoveAll(q => q.RowId == row.RowId);
							UpdateCellBackgroundBrush();
						}
						SchedulerControlManager.SetSelection(row.Start, row.Start, row.ServiceProviderRowId);
						await DaysInfoBuild();
					}

				}
			});
		}

		public void AppointmentViewInfoCustomize(DevExpress.Xpf.Scheduler.AppointmentViewInfoCustomizingEventArgs e)
		{
			var rowId = (Guid)e.ViewInfo.Appointment.CustomFields["RowId"];
			var entity = Appointments.Single(q => q.RowId == rowId);
			e.ViewInfo.CustomViewInfo = entity;
		}

		public void CustomizeVisualViewInfo(DevExpress.Xpf.Scheduler.CustomizeVisualViewInfoEventArgs e)
		{
			//var rowId = (Guid)e.ViewInfo.Appointment.CustomFields["RowId"];
			//var entity = Appointments.Single(q => q.RowId == rowId);
			//e.ViewInfo.CustomViewInfo = entity;
			var a = e;
		}

		


		Appointment XtraAppointment2Appointment(XtraAppointment xtraAppointment)
		{
			return (xtraAppointment?.CustomFields["Entity"] as Appointment);
		}

		

		public void AppointmentResizing(Xtra.AppointmentResizeEventArgs e)
		{
			//var appointment = (Appointment)e.EditedAppointment.CustomFields["Entity"];
			//if (appointment.InInvoice)
			//{
			//	e.Allow = false;
			//	e.Handled = true;
			//}
			//var a = e;
			Debug.WriteLine(e.EditedAppointment.End);
		}

		async public void AppointmentResized(Xtra.AppointmentResizeEventArgs e)
		{
			e.Handled = true;
			if (!await SaveAppointment(e.EditedAppointment))
			{
				e.Allow = false;
			}
		}

		async public void AppointmentDrop(DevExpress.XtraScheduler.AppointmentDragEventArgs e)
		{
			if (!await SaveAppointment(e.EditedAppointment))
			{
				e.Allow = false;
			}
		}

		async Task<bool> SaveAppointment(XtraAppointment item)
		{
			DateTime finishTime, startTime;
			if (item.Start < item.End)
			{
				startTime = item.Start;
				finishTime = item.End;
			}
			else if (item.End < item.Start)
			{
				startTime = item.End;
				finishTime = item.Start;
			}
			else
			{
				return false;
			}
			var serviceProviderRowId = (Guid?)item.ResourceId;
			if (serviceProviderRowId == ServiceProvider.NO_DOCTOR)
			{
				serviceProviderRowId = null;
			}

			//appointment
			var appointment = (Appointment)item.CustomFields["Entity"];
			if (appointment.InInvoice)
			{
				var err = "You cannot change appointment included in invoice";
				MessageBoxService.ShowError(err);
				return false;
			}

			//quest
			var text = "Change appointment time to " + startTime.FormatHHMM() + " - " + finishTime.FormatHHMM();
			var ret = MessageBoxService.ShowMessage(text, CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
			if (ret != MessageResult.Yes)
			{
				return false;
			}

			//updateEntity
			appointment.Start = startTime;
			appointment.Finish = finishTime;
			appointment.ServiceProviderRowId = serviceProviderRowId;
			var updateEntity = appointment.GetPocoClone();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await businessService.PutAppointment(new List<Appointment>() { updateEntity });
			ShowWaitIndicator.Hide();
			if (!OneAppointmentViewModel.ValidateAppointmentUpdateErrror(uret, false, MessageBoxService))
			{
				return false;
			}

			var index = Appointments.FindIndex(q => q.RowId == appointment.RowId);
			var nrow = Appointment2HospitalAppointment(appointment);
			Appointments[index] = nrow;
			if (AllAppointmentInAppointmentBook != null)
			{
				var index2 = AllAppointmentInAppointmentBook.FindIndex(q => q.RowId == appointment.RowId);
				if (index2 >= 0)
				{
					AllAppointmentInAppointmentBook[index2] = appointment;
					UpdateCellBackgroundBrush();
				}
			}
				


			//MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			//IsNew = false;
			//ResetHasChange();

			return true;
		}

		public void PopupMenuShowing(DevExpress.Xpf.Scheduler.SchedulerMenuEventArgs e)
		{
			//delete
			var menuNames = e.Menu.Items.OfType<System.Windows.FrameworkContentElement>().Select(q => q.Name).Where(q => !string.IsNullOrEmpty(q)).ToArray();
			menuNames.ForEach(q =>
			{
				e.Customizations.Add(new DevExpress.Xpf.Bars.RemoveBarItemAndLinkAction()
				{
					ItemName = q
				});
			});

			if (!IsEnableControl || IsOnePatientAllAppointment)
			{
				return;
			}

			//add
			if (e.Menu.Name == "DefaultMenu")
			{
				var myMenuItem1 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserNewAppointment",
					Content = "New Appointment",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-new-employee-16.png")),
				};
				myMenuItem1.ItemClick += (a,b) => NewEntity();
				e.Customizations.Add(myMenuItem1);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());

				var myMenuItem2 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserNewAppointmentForNotRegistered",
					Content = "Send Appointment Link",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-mail-merge-16.png")),
				};
				myMenuItem2.ItemClick += (a, b) => NewEntity(isNotRegisteredMode: true);
				e.Customizations.Add(myMenuItem2);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());
			}
			else if (e.Menu.Name == "AppointmentMenu")
			{
				var myMenuItem1 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserEditAppointment",
					Content = "Edit Appointment",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-edit-16.png")),
				};
				myMenuItem1.ItemClick += (a, b) => EditEntity();
				e.Customizations.Add(myMenuItem1);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());

				var myMenuItem2 = new DevExpress.Xpf.Bars.BarButtonItem()
				{
					Name = "UserDeleteAppointment",
					Content = "Delete Appointment",
					Glyph = new BitmapImage(new Uri("pack://application:,,,/Profibiz.PracticeManager.InfrastructureExt;component/Resources/icon-delete-16.png")),
				};
				myMenuItem2.ItemClick += (a, b) => DeleteEntity();
				e.Customizations.Add(myMenuItem2);
				e.Customizations.Add(new DevExpress.Xpf.Bars.BarItemLinkSeparator());
			}
		}


		public void ActiveViewChanged(EventArgs e)
		{
			CustomizeSchedulerView();
		}


		public void ViewZoomIn()
		{
			SchedulerControlManager.ZoomIn();
		}

		public void ViewZoomOut()
		{
			SchedulerControlManager.ZoomOut();
		}


		[ImplementPropertyChanged]
		public class Doctor
		{
			public Guid Id { get; set; }
			public String Name { get; set; }
			public String BackgroundColor { get; set; }
			public String ForegroundColor { get; set; }
			public Boolean IsSelected { get; set; }
			public ServiceProvider Entity { get; set; }

			public DelegateCommand DoctorSelectUnselectCommand => new DelegateCommand(() =>
			{
				IsSelected = !IsSelected;
			});
		}

		public DaysInfoClass DaysInfo { get; set; }
		public class DaysInfoClass
		{
			public Dictionary<Guid, DaysInfoOne> DoctorInfo { get; set; } = new Dictionary<Guid, DaysInfoOne>(); //данные по докторам

			public int GetAppointmentCount(Guid serviceProviderRowId, DateTime date)
			{
				var doctorInfo = DoctorInfo[serviceProviderRowId];
				var appointmentCount = 0;
				if (doctorInfo.AppointmentCount.ContainsKey(date))
				{
					appointmentCount = doctorInfo.AppointmentCount[date];
				}
				return appointmentCount;
			}

			public string GetAppointmentCountText(Guid serviceProviderRowId, DateTime date)
			{
				var doctorInfo = DoctorInfo[serviceProviderRowId];
				var appointmentCount = GetAppointmentCount(serviceProviderRowId, date);
				return appointmentCount + "/" + doctorInfo.MaxAppointmentCount;
			}

			public bool GetAppointmentCountIsMaximum(Guid serviceProviderRowId, DateTime date)
			{
				var doctorInfo = DoctorInfo[serviceProviderRowId];
				var appointmentCount = GetAppointmentCount(serviceProviderRowId, date);
				return appointmentCount >= doctorInfo.MaxAppointmentCount;
			}

			public DayStatus GetDayStatus(Guid serviceProviderRowId, DateTime date)
			{
				var vacationDates = DoctorInfo[serviceProviderRowId].VacationDates;
				var schedulerRecords = DoctorInfo[serviceProviderRowId].SchedulerRecords;

				if (vacationDates.Contains(date) || LookupDataProvider.IsPublicHoliday(date))
				{
					return DayStatus.Holiday;
				}
				else if (SchedulerRecordFunc.IsWorkDate(schedulerRecords, date))
				{
					return DayStatus.Work;
				}
				else if (HolidaysFunc.IsHoliday(date))
				{
					return DayStatus.Holiday;
				}
				else
				{
					return DayStatus.Nowork;
				}
			}
		}

		public class DaysInfoOne
		{
			public DaysInfoOne() { }
			public DateTime[] VacationDates; //выходные дни
			public Int32 MaxAppointmentCount; //сколько максимум Appointment
			public Dictionary<DateTime, Int32> AppointmentCount = new Dictionary<DateTime, Int32>(); //сколько Appointment по дням
			public List<SchedulerRecord> SchedulerRecords; //информация о расписании клиента
		}
		public enum DayStatus { Work, Holiday, Nowork }

		async Task DaysInfoBuild(bool forInit = false)
		{
			var daysInfo = new DaysInfoClass();
			foreach (var serviceProviderRowId in Doctors.Select(q => q.Id).Where(q => q != ALL_APPOINTMENTBOOKS))
			{
				DateTime[] vacationDates;
				List<SchedulerRecord> schedulerRecords;
				if (forInit)
				{
					vacationDates = (await businessService.GetCalendarEventList(serviceProviderRowId: serviceProviderRowId, isVacation: true))
						.Select(q => q.Start).Distinct().ToArray();
					schedulerRecords = await businessService.GetSchedulerRecords(serviceProviderRowId: serviceProviderRowId);
				}
				else
				{
					vacationDates = DaysInfo.DoctorInfo[serviceProviderRowId].VacationDates;
					schedulerRecords = DaysInfo.DoctorInfo[serviceProviderRowId].SchedulerRecords;
				}

				var serviceProvider = LookupDataProvider.FindServiceProvider(serviceProviderRowId);
				var daysInfoOne = new DaysInfoOne
				{
					VacationDates = vacationDates,
					MaxAppointmentCount = serviceProvider.MaximumDayAppointments,
					SchedulerRecords = schedulerRecords,
				};
				daysInfoOne.AppointmentCount =
					Appointments
					.Select(q => q.Entity)
					.Where(q => q.ServiceProviderRowId == serviceProviderRowId)
					.GroupBy(q => q.Start.Date)
					.ToDictionary(q => q.Key, q => q.Count());
				daysInfo.DoctorInfo.Add(serviceProviderRowId, daysInfoOne);
			}
			DaysInfo = daysInfo;
		}


		public class HospitalAppointment
		{
			public Guid RowId { get; set; }
			public DateTime StartDate { get; set; }
			public DateTime EndDate { get; set; }
			public string PatientName { get; set; }
			public Patient Patient { get; set; }
			public string Notes { get; set; }
			public string Location { get; set; }
			public Guid DoctorId { get; set; }
			public Doctor Doctor { get; set; }
			public Guid? InsuranceProviderRowId { get; set; }
			public InsuranceProvider InsuranceProvider { get; set; }
			public Guid? MedicalServicesOrSupplyRowId { get; set; }
			public MedicalServicesOrSupply MedicalServicesOrSupply { get; set; }
			public string InsuranceNumber { get; set; }
			public bool FirstVisit { get; set; }
			public string Recurrence { get; set; }
			public int Type { get; set; }
			public Appointment Entity { get; set; }
			public AppointmentBook AppointmentBook { get; set; }
			public AppointmentsSchedulerViewModel ParentViewModel { get; set; }

			public String StartEndDateString
			{
				get
				{
					return StartDate.FormatHHMM() + " - " + EndDate.FormatHHMM();
				}
			}
		}


		public void ChangeActiveView(Xtra.SchedulerViewType value)
		{
			ActiveViewType = value;
			if (ActiveViewType == Xtra.SchedulerViewType.Week)
			{
				//GroupType = Xtra.SchedulerGroupType.None;
			}
			CustomizeSchedulerView();
		}
		public Xtra.SchedulerViewType ActiveViewType { get; set; } = Xtra.SchedulerViewType.WorkWeek;

		public void ChangeGroupType(Xtra.SchedulerGroupType value)
		{
			GroupType = value;
		}
		public Xtra.SchedulerGroupType GroupType { get; set; } = Xtra.SchedulerGroupType.Resource;

		public enum ShowHoursEnum { Working, All }
		public ShowHoursEnum ShowHours { get; set; } = ShowHoursEnum.All;
		public Boolean ShowHoursIsEnabled { get; set; }
		public Boolean ShowHoursIsVisible { get; set; }
		public void OnShowHoursChanged()
		{
			CustomizeSchedulerView();
		}

		public String ShowDays { get; set; } = "All";
		public void OnShowDaysChanged()
		{
			CustomizeSchedulerView();
		}
		public Boolean ShowDaysIsEnabled { get; set; }
		public Boolean ShowDaysIsVisible { get; set; }

		public Int32 ShowDays2 { get; set; } = 1;
		public void OnShowDays2Changed()
		{
			CustomizeSchedulerView();
		}
		public Boolean ShowDays2IsEnabled { get; set; }
		public Boolean ShowDays2IsVisible { get; set; }

		public String StartEndTimeVisibility { get; set; } = "Auto";
		public Boolean StartEndTimeVisibilityIsEnabled { get; set; }
		public Boolean StartEndTimeVisibilityIsVisible { get; set; }

		public Boolean CompressWeekend { get; set; } = true;
		public Boolean CompressWeekendIsEnabled { get; set; } = true;
		public Boolean CompressWeekendIsVisible { get; set; } = true;
		

		public void CustomizeSchedulerView()
		{
			if (SelectedAppointmentBook == null) return;

			TimeScale = new TimeSpan(0, SelectedAppointmentBook.Interval, 0);
			VisibleTimeInterval = 
				(ShowHours == ShowHoursEnum.Working && SelectedAppointmentBook.RowId != ALL_APPOINTMENTBOOKS) ?
				new TimeOfDayInterval(SelectedAppointmentBook.StartAt.TimeOfDay, SelectedAppointmentBook.FinishAt.TimeOfDay) :
				new TimeOfDayInterval(new TimeSpan(0, 0, 0), new TimeSpan(24, 0, 0));

			var mon_fri = Xtra.WeekDays.Monday | Xtra.WeekDays.Tuesday | Xtra.WeekDays.Wednesday | Xtra.WeekDays.Thursday | Xtra.WeekDays.Friday;
			var mon_sat = mon_fri | Xtra.WeekDays.Saturday;
			WeekWorkDays =
					ShowDays == "Mon-Fri" ? mon_fri :
					ShowDays == "Mon-Sat" ? mon_sat : 
					Xtra.WeekDays.EveryDay;


			ShowHoursIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day || ActiveViewType == Xtra.SchedulerViewType.WorkWeek);
			ShowDaysIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.WorkWeek);
			ShowDays2IsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day);
			ShowDays2IsVisible = (ActiveViewType == Xtra.SchedulerViewType.Day);
			ShowDaysIsVisible = !ShowDays2IsVisible;
			ShowHoursIsVisible = true;
			StartEndTimeVisibilityIsVisible = true;

			StartEndTimeVisibilityIsEnabled = (ActiveViewType == Xtra.SchedulerViewType.Day || ActiveViewType == Xtra.SchedulerViewType.WorkWeek);

			var isMonthView = (ActiveViewType == Xtra.SchedulerViewType.Month);
			CompressWeekendIsVisible = isMonthView;
			StartEndTimeVisibilityIsVisible = StartEndTimeVisibilityIsVisible && !isMonthView;
			ShowDays2IsVisible = ShowDays2IsVisible && !isMonthView;
			ShowDaysIsVisible = ShowDaysIsVisible && !isMonthView;
			ShowHoursIsVisible = ShowHoursIsVisible && !isMonthView;
		}


		public IEnumerable<Appointment> FindInIntervalAllAppointmentInAppointmentBook(DateTime iStart, DateTime iFinish)
		{
			return AllAppointmentInAppointmentBook.Where(q => DateTimeHelper.IntervalsIntersection(iStart, iFinish, q.Start, q.Finish));
		}

		public CellBackgroundBrushSelectorClass CellBackgroundBrushSelector { get; set; }
		public class CellBackgroundBrushSelectorClass : ICellBrushSelector, DevExpress.Xpf.Scheduler.Internal.ISchedulerDefaultCellBrushSelector
		{
			private AppointmentsSchedulerViewModel ParentViewModel { get; set; }
			public DayViewCellBackgroundBrushSelector DefaultSelector = new DayViewCellBackgroundBrushSelector();

			public CellBackgroundBrushSelectorClass(AppointmentsSchedulerViewModel parent)
			{
				ParentViewModel = parent;
			}

			public System.Windows.Media.Brush SelectBrush(VisualResourceCellBaseContent content)
			{
				if (ParentViewModel.IsOnePatientOneAppointment)
				{
					var workTimeCell = content as VisualWorkTimeCellBaseContent;
					if (workTimeCell == null) return null;

					var iStart = workTimeCell.IntervalStart;
					var iFinish = workTimeCell.IntervalEnd;
					var ex = ParentViewModel.FindInIntervalAllAppointmentInAppointmentBook(iStart, iFinish).Any();

					var color = ex ? System.Windows.Media.Color.FromRgb(255, 186, 186) : System.Windows.Media.Color.FromRgb(191, 255, 191);
					return new SolidColorBrush { Color = color };
				}
				else
				{
					return DefaultSelector.SelectBrush(content);
				}



				
				//	return null;
				//if (workTimeCell.Brushes == null)
				//	return null;
				//if (workTimeCell.IsWorkTime)
				//	return workTimeCell.Brushes.CellLight;
				//else
				//	return workTimeCell.Brushes.Cell;
			}
		}

		void UpdateCellBackgroundBrush()
		{
			CellBackgroundBrushSelector = new CellBackgroundBrushSelectorClass(this);
		}





		public class OpenParams
		{
			//public Patient OnePatient { get; set; }

			//public Guid AppointmentBookRowId { get; set; }
			//public bool IsNew { get; set; }
			//public Guid RowId { get; set; }
		}
	}






}
