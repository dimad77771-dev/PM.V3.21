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
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class CalendarEventsRemindersViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		public virtual InteractionRequest<CloseDXWindowsActionParam> CloseInteractionRequest { get; set; } = new InteractionRequest<CloseDXWindowsActionParam>();
		#endregion
		public virtual ObservableCollection<CalendarEvent> RemainderEntities { get; set; } = new ObservableCollection<CalendarEvent>();
		//public virtual CalendarEvent RemainderSelectedEntity { get; set; }
		public virtual ObservableCollection<CalendarEvent> RemainderSelectedEntities { get; set; } = new ObservableCollection<CalendarEvent>();
		public virtual EventCalendarSnoozedModel SnoozedTo { get; set; } = EventCalendarSnoozedInfo.Default;
		public OpenParms OpenParm { get; set; }

		public CalendarEventsRemindersViewModel() : base()
		{
		}

		public void OnOpen(OpenParms parm)
		{
			OpenParm = parm;
			DispatcherUIHelper.Run(async () =>
			{
				await LoadData();
			});
		}


		async Task LoadData()
		{
			var rows = OpenParm.RemainderEntities;
			RemainderEntities = rows.OrderByDescending(q => q.StartDate).ToObservableCollection();
			RemainderEntities.ForEach(q => SubscribeCalendarEventRow(q));
		}



		//public async void Delete()
		//{
		//	var messageBoxService = this.GetRequiredService<IMessageBoxService>();
		//	var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "Refund"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
		//	if (ret == MessageResult.Yes)
		//	{
		//		var row = SelectedEntity;
		//		ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
		//		var uret = await businessService.DeleteRefund(row.RowId);
		//		ShowWaitIndicator.Hide();
		//		if (!uret.Validate(messageBoxService)) return;
		//		Entities.Remove(row);
		//	}
		//}
		//public bool CanDelete() => (SelectedEntity != null);

		void SubscribeCalendarEventRow(CalendarEvent row)
		{
			row.OnOpenDetail = () =>
			{
				var param = new OneCalendarEventViewModel.OpenParams
				{
					IsNew = false,
					RowId = row.RowId,
					//IsReadOnly = true,
				};
				ShowDXWindowsInteractionRequest.Raise(new ShowDXWindowsActionParam
				{
					ViewCode = ViewCodes.OneCalendarEventView,
					Param = param,
				});
			};
		}

		public void ClosingEvent(CancelEventArgs arg)
		{
			OpenParm.OnCloseAction?.Invoke();
		}



		public void Dismiss()
		{
			UpdateCore(RemainderSelectedEntities.ToList(), isDismiss: true);
		}
		public bool CanDismiss() => RemainderSelectedEntities.Count > 0;


		public void DismissAll()
		{
			UpdateCore(RemainderEntities.ToList(), isDismiss: true);
		}
		public bool CanDismissAll() => RemainderEntities.Count > 0;


		public void Snooze()
		{
			UpdateCore(RemainderSelectedEntities.ToList(), snoozedTo: SnoozedTo);
		}
		public bool CanSnooze() => RemainderSelectedEntities.Count > 0;


		void UpdateCore(List<CalendarEvent> rows, bool isDismiss = false, EventCalendarSnoozedModel snoozedTo = null)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var isSnoozed = (snoozedTo != null);

				if (isSnoozed)
				{
					rows.ForEach(q =>
					{
						if (snoozedTo.BeforeStartMode)
						{
							q.SnoozedTo = q.Finish.AddMinutes(-snoozedTo.Minutes);
						}
						else
						{
							q.SnoozedTo = DateTimeHelper.Now.AddMinutes(snoozedTo.Minutes);
						}
					});
				}
				else if (isDismiss)
				{
					rows.ForEach(q =>
					{
						q.SnoozedTo = null;
						q.RemainderInMinutes = CalendarEvent.REMAINDER_NONE;
					});
				}
				else throw new ArgumentException();

				//updateEntity
				var uRows = rows.Select(q => q.GetPocoClone()).ToList();
				uRows.ForEach(q => q.UpdateFlagRemainderFieldsOnly = true);

				//save
				ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
				var uret = await businessService.PutCalendarEvent(uRows);
				ShowWaitIndicator.Hide();
				if (!uret.Validate(MessageBoxService))
				{
					return;
				}

				RemoveRowsAndCloseWindow(rows);
				return;
			});
		}


		void RemoveRowsAndCloseWindow(List<CalendarEvent> rows)
		{
			RemainderEntities.RemoveRange(rows);
			if (RemainderEntities.Count == 0)
			{
				CloseInteractionRequest?.Raise(null);
			}
		}

		public class OpenParms
		{
			public List<CalendarEvent> RemainderEntities { get; set; }
			public Action OnCloseAction { get; set; }
		}
	}



	public class CalendarEventsRemindersService : IGlobalService
	{
		public void Start()
		{
			//return; //пока не надо ничего делать

			Task.Run(async () =>
			{
				while (true)
				{
					var businessService = BusinessServiceHelper.GetPatientsBusinessService();
					var rows = await businessService.GetCalendarEventList(forRemainder: true);
					Debug.WriteLine("row.count=" + rows.Count);
					if (rows.Count > 0)
					{
						var sync = new ManualResetEvent(false);
						Application.Current.Dispatcher.Invoke(() =>
						{
							var ShowDXWindowsInteractionRequest = new ShowDXWindowsAction();
							ShowDXWindowsInteractionRequest.OpenWindow(new ShowDXWindowsActionParam
							{
								ViewCode = ViewCodes.CalendarEventsRemindersView,
								Param = new CalendarEventsRemindersViewModel.OpenParms
								{
									RemainderEntities = rows,
									OnCloseAction = () =>
									{
										sync.Set();
									},
								},
							});
						});
						sync.WaitOne();
					}

					await Task.Delay(10000);
				}
			})
			.ContinueWith(t =>
			{
				if (t.Exception != null)
				{
					Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action(() =>
					{
						throw new AggregateException(t.Exception);
					}));
				}
			});
		}
	}
}	
