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
	public class SchedulerRecordViewModel : ViewModelBase
	{
		#region Services
		IPatientsBusinessService businessService;
		ILookupsBusinessService lookupsBusinessService;
		IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public GridControlBehaviorManager BehaviorGridConrolEntities0 { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager BehaviorGridConrolEntities1 { get; set; } = new GridControlBehaviorManager();
		public GridControlBehaviorManager[] BehaviorGridConrolEntities { get; set; }
		public virtual ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public virtual InteractionRequest<ShowDXWindowsActionParam> OpenWindowInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		#endregion
		public OpenParams OpenParam { get; set; }
		public virtual Order Entity { get; set; }
		public virtual ObservableCollection<RowModel>[] AllEntities { get; set; } = new ObservableCollection<RowModel>[2];
		public virtual RowModel[] AllSelectedEntity { get; set; } = new RowModel[2];

		const int LEVEL_BASE = 0;
		const int LEVEL_EXCEPTION = 1;
		int[] AllLevels = new[] { LEVEL_BASE, LEVEL_EXCEPTION };


		public SchedulerRecordViewModel(IPatientsBusinessService _businessService, ILookupsBusinessService _lookupsBusinessService) : base()
		{
			businessService = _businessService;
			lookupsBusinessService = _lookupsBusinessService;
			BehaviorGridConrolEntities = new[] { BehaviorGridConrolEntities0, BehaviorGridConrolEntities1 };
		}

		public void OnOpen(object arg)
		{
			if (arg is OpenParams)
			{
				OpenParam = (OpenParams)arg;
			}
			DispatcherUIHelper.Run2(LoadData());
		}

		async Task LoadData()
		{
			ShowWaitIndicator.Show();

			var records = await businessService.GetSchedulerRecords(OpenParam.ServiceProviderRowId);
			foreach (var level in AllLevels)
			{
				var entities = 
					records
					.Where(q => q.IsBaseRecord == Level2IsBase(level))
					.Select(q => new RowModel(q))
					.OrderByDescending(q => q.SchedulerRecord.StartPeriod)
					.ToObservableCollection();
				AllEntities[level] = entities;
				this.RaisePropertyChanged(nameof(AllEntities));
			}

			ResetHasChange();
			ShowWaitIndicator.Hide();
		}

		bool Level2IsBase(int level) => (level == LEVEL_BASE ? true : level == LEVEL_EXCEPTION ? false : LogicalException.Throw<bool>());
		int Object2Level(object arg) => Int32.Parse(arg.ToString());


		public void Close() => CloseCore();
		public void Save() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: false));
		public void SaveAndClose() => DispatcherUIHelper.Run(async () => await SaveCore(andClose: true));




		bool Validate()
		{
			List<string> errors = new List<string>();

			foreach (var level in AllLevels)
			{
				var entities = AllEntities[level];

				foreach (var model in entities)
				{
					if (model.SchedulerRecord.StartPeriod == null)
					{
						errors.Add("Start Period is required");
						AllSelectedEntity[level] = model;
					}
					else if (model.SchedulerRecord.FinishPeriod == null)
					{
						errors.Add("Finish Period is required");
						AllSelectedEntity[level] = model;
					}
					else
					{
						var startPeriod = (DateTime)model.SchedulerRecord.StartPeriod;
						var finishPeriod = (DateTime)model.SchedulerRecord.FinishPeriod;
						if (finishPeriod.Date < startPeriod.Date)
						{
							errors.Add("Start Period cannot be more than Finish Period");
							AllSelectedEntity[level] = model;
						}
					}

					for (var dayWeek = 0; dayWeek < DAY_IN_WEEK; dayWeek++)
					{
						var startTime = model.GetStartTime(dayWeek);
						var finishTime = model.GetFinishTime(dayWeek);
						if (startTime != null && finishTime == null)
						{
							errors.Add("To(" + WeekDayNames[dayWeek] + ") is required");
							AllSelectedEntity[level] = model;
						}
						else if (startTime == null && finishTime != null)
						{
							errors.Add("From(" + WeekDayNames[dayWeek] + ") is required");
							AllSelectedEntity[level] = model;
						}
						else if (startTime != null && finishTime != null)
						{
							var stime = ((DateTime)startTime).TimeOfDay;
							var ftime = ((DateTime)finishTime).TimeOfDay;
							if (ftime < stime)
							{
								errors.Add("From(" + WeekDayNames[dayWeek] + ") cannot be more than To(" + WeekDayNames[dayWeek] + ")");
								AllSelectedEntity[level] = model;
							}
						}
					}
				}
			}

			if (errors.Count == 0)
			{
				ValidateIntersection(errors);
			}

			if (errors.Count > 0)
			{
				var err = string.Join("\n", errors.ToArray());
				MessageBoxService.ShowMessage(err, CommonResources.Validation_Error, MessageButton.OK, MessageIcon.Error);
				return false;
			}


			return true;
		}

		void ValidateIntersection(List<string> errors)
		{
			foreach (var level in AllLevels)
			{
				var entities = AllEntities[level];

				var rows = entities
					.Select(q => new { StartPeriod = ((DateTime)q.SchedulerRecord.StartPeriod).Date, FinishPeriod = ((DateTime)q.SchedulerRecord.FinishPeriod).Date })
					.OrderByDescending(q => q.StartPeriod)
					.ToArray();
				for(int i = 0; i < rows.Length - 1; i++)
				{
					if (rows[i + 1].FinishPeriod >= rows[i].StartPeriod)
					{
						var error = "Intersection of intervals:\n" +
							"   " + rows[i + 1].StartPeriod.ToShortDateString() + "-" + rows[i + 1].FinishPeriod.ToShortDateString() + "\n" +
							"   " + "  and" + "\n" +
							"   " + rows[i + 0].StartPeriod.ToShortDateString() + "-" + rows[i + 0].FinishPeriod.ToShortDateString();
						errors.Add(error);
					}
				}
			}
		}

		async Task<bool> SaveCore(bool andClose)
		{
			//validate
			if (!Validate())
			{
				return false;
			}


			var records = new List<SchedulerRecord>();
			foreach (var level in AllLevels)
			{
				foreach (var model in AllEntities[level])
				{
					var record = model.SchedulerRecord;
					record.SchedulerRecordItems.Clear();
					records.Add(record);

					for (var dayWeek = 0; dayWeek < DAY_IN_WEEK; dayWeek++)
					{
						var startTime = model.GetStartTime(dayWeek);
						var finishTime = model.GetFinishTime(dayWeek);
						if (startTime != null && finishTime != null)
						{
							var item = new SchedulerRecordItem
							{
								RowId = Guid.NewGuid(),
								SchedulerRecordRowId = record.RowId,
								DayWeek = dayWeek,
								StartTime = (DateTime)startTime,
								FinishTime = (DateTime)finishTime,
							};
							record.SchedulerRecordItems.Add(item);
						}
					}
				}
			}
			if (records.Count == 0)
			{
				records.Add(new SchedulerRecord
				{
					RowId = default(Guid),
					ServiceProviderRowId = OpenParam.ServiceProviderRowId
				});
			}

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = await businessService.PutSchedulerRecords(records);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			//ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			//MessengerHelper.SendMsgRowChange(updateEntity, IsNew);
			//ShowWaitIndicator.Hide();
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

		bool HasChangeInternal = false;
		bool HasChange()
		{
			if (HasChangeInternal) return true;

			foreach (var level in AllLevels)
			{
				if (AllEntities[level].Any(q => q.HasChanges()))
				{
					return true;
				}
			}
			return false;
		}

		void ResetHasChange()
		{
			foreach (var level in AllLevels)
			{
				AllEntities[level].ForEach(q =>
				{
					q.IsChanged = false;
					q.SchedulerRecord.IsChanged = false;
				});
			}
			HasChangeInternal = false;
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


		public void SchedulerRecordNew(object arg)
		{
			DispatcherUIHelper.Run(() =>
			{
				var level = Object2Level(arg);
				var entity = AllEntities[level];

				var row = new SchedulerRecord
				{
					RowId = Guid.NewGuid(),
					ServiceProviderRowId = OpenParam.ServiceProviderRowId,
					IsBaseRecord = Level2IsBase(level),
					IsChanged = true,
				};
				var model = new RowModel(row);
				entity.Insert(0, model);
				AllSelectedEntity[level] = model;

				DispatcherUIHelper.Run(() =>
				{
					BehaviorGridConrolEntities[level].SetCurrentColumn("SchedulerRecord.StartPeriod");
					BehaviorGridConrolEntities[level].ShowEditor(true);
				});
			});
		}

		public void SchedulerRecordDelete(object arg)
		{
			DispatcherUIHelper.Run(() =>
			{
				var level = Object2Level(arg);
				var row = AllSelectedEntity[level];
				var messageBoxService = this.GetRequiredService<IMessageBoxService>();
				var ret = messageBoxService.ShowMessage(string.Format(CommonResources.Confirmation_Delete, "row"), CommonResources.Confirmation_Caption, MessageButton.YesNo, MessageIcon.Question);
				if (ret == MessageResult.Yes)
				{
					AllEntities[level].Remove(row);
					HasChangeInternal = true;
				}
			});
		}
		public bool CanSchedulerRecordDelete(object arg) => (AllSelectedEntity[Object2Level(arg)] != null);


		public class OpenParams
		{
			public Guid ServiceProviderRowId { get; set; }
		}

		public const int DAY_IN_WEEK = 7;
		public string[] WeekDayNames { get; set; } = new[] { "SUNDAY", "MONDAY", "TUESDAY", "WEDNESDAY", "THURSDAY", "FRIDAY", "SATURDAY" };

		[ImplementPropertyChanged]
		public class RowModel
		{
			public SchedulerRecord SchedulerRecord { get; set; }
			public DateTime? StartTime0 { get; set; }
			public DateTime? FinishTime0 { get; set; }
			public DateTime? StartTime1 { get; set; }
			public DateTime? FinishTime1 { get; set; }
			public DateTime? StartTime2 { get; set; }
			public DateTime? FinishTime2 { get; set; }
			public DateTime? StartTime3 { get; set; }
			public DateTime? FinishTime3 { get; set; }
			public DateTime? StartTime4 { get; set; }
			public DateTime? FinishTime4 { get; set; }
			public DateTime? StartTime5 { get; set; }
			public DateTime? FinishTime5 { get; set; }
			public DateTime? StartTime6 { get; set; }
			public DateTime? FinishTime6 { get; set; }

			public RowModel(SchedulerRecord _schedulerRecord = null)
			{
				SchedulerRecord = _schedulerRecord;
				if (SchedulerRecord == null)
				{
					SchedulerRecord = new SchedulerRecord();
				}
				else
				{
					foreach(var item in SchedulerRecord.SchedulerRecordItems)
					{
						SetStartTime(item.DayWeek, item.StartTime);
						SetFinishTime(item.DayWeek, item.FinishTime);
					}
				}
			}

			public void SetStartTime(int dayWeek, DateTime? arg)
			{
				var prop = typeof(RowModel).GetProperty("StartTime" + dayWeek);
				prop.SetValue(this, arg);
			}
			public void SetFinishTime(int dayWeek, DateTime? arg)
			{
				var prop = typeof(RowModel).GetProperty("FinishTime" + dayWeek);
				prop.SetValue(this, arg);
			}
			public DateTime? GetStartTime(int dayWeek)
			{
				var prop = typeof(RowModel).GetProperty("StartTime" + dayWeek);
				return (DateTime?)prop.GetValue(this);
			}
			public DateTime? GetFinishTime(int dayWeek)
			{
				var prop = typeof(RowModel).GetProperty("FinishTime" + dayWeek);
				return (DateTime?)prop.GetValue(this);
			}

			public bool IsChanged { get; set; }
			public bool HasChanges() => IsChanged || SchedulerRecord.IsChanged;
		}
	}
}
