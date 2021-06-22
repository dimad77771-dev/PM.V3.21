using System;
using System.Linq;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;
using Newtonsoft.Json;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Patients.BusinessService;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class GlobalSettings
	{
		public static GlobalSettings Instance { get; set; }

		[JsonIgnore]
		public UserSetting UserSettings { get; set; }

		public static void Update(Action<GlobalSettings> action)
		{
			action(Instance);
			SendSettingsToServer();
		}

		public static void SendSettingsToServer()
		{
			var lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
			var json = JsonConvert.SerializeObject(Instance);
			var userSettings = new UserSetting
			{
				UserCode = RuntimeHelper.GetUserCode(),
				Json = json,
			};
			lookupsBusinessService.PostUserSettings(userSettings).ContinueWith((q) =>
			{
				NLog.vv(() => q);
			});
		}

		public static void ReadSettingsFromServer2()
		{
			var sync = new ManualResetEvent(false);
			var lookupsBusinessService = new LookupsBusinessService();
			var userCode = RuntimeHelper.GetUserCode();
			var json = "";
			var userSetting = default(UserSetting);
			var task = Task.Run(async () =>
			{
				var task2 = lookupsBusinessService.GetUserSettings(userCode);
				var row = await task2;
				if (row.IsRequestError)
				{
					System.Windows.MessageBox.Show("Connection to server failed. Please make sure your VPN connection is ON", "Error", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Error);
					System.Windows.Application.Current.Shutdown();
				}

				var task1 = lookupsBusinessService.UpdateAllLookups();
				await task1;
				//await Task.WhenAll(task1, task2);
				
				userSetting = row;
				json = row.Json;
				sync.Set();
			});
			sync.WaitOne();

			Instance = string.IsNullOrEmpty(json) ?
				new GlobalSettings() :
				JsonConvert.DeserializeObject<GlobalSettings>(json);
			Instance.UserSettings = userSetting;
		}



		public FinancesSettings Finances { get; } = new FinancesSettings();
		public class FinancesSettings
		{
			public DateTime? FinancesViewDateFilterFrom { get; set; }
			public DateTime? FinancesViewDateFilterTo { get; set; }
			public Int32 FinancesViewInvoicesFilterDateMode { get; set; }

			public class GetFinancesViewDateFilterReturn
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
				public Int32 InvoicesFilterDateMode;
			}
			public GetFinancesViewDateFilterReturn GetFinancesViewDateFilter()
			{
				if (FinancesViewDateFilterFrom == null)
				{
					FinancesViewDateFilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					FinancesViewDateFilterTo = DateTimeHelper.LastDayCurrentMonth();
					FinancesViewInvoicesFilterDateMode = 0;
				}

				return new GetFinancesViewDateFilterReturn
				{
					FilterFrom = FinancesViewDateFilterFrom.Value,
					FilterTo = FinancesViewDateFilterTo.Value,
					InvoicesFilterDateMode = FinancesViewInvoicesFilterDateMode,
				};
			}

			public void SetFinancesViewDateFilter(DateTime FilterFrom, DateTime FilterTo, Int32? InvoicesFilterDateMode = null)
			{
				FinancesViewDateFilterFrom = FilterFrom;
				FinancesViewDateFilterTo = FilterTo;
				if (InvoicesFilterDateMode != null)
				{
					FinancesViewInvoicesFilterDateMode = InvoicesFilterDateMode.Value;
				}
				SendSettingsToServer();
			}
		}

		public ChargeoutsSettings Chargeouts { get; } = new ChargeoutsSettings();
		public class ChargeoutsSettings
		{
			public DateTime? ChargeoutsViewDateFilterFrom { get; set; }
			public DateTime? ChargeoutsViewDateFilterTo { get; set; }
			public Int32 ChargeoutsViewInvoicesFilterDateMode { get; set; }

			public class GetChargeoutsViewDateFilterReturn
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
				public Int32 ChargeoutsFilterDateMode;
			}
			public GetChargeoutsViewDateFilterReturn GetChargeoutsViewDateFilter()
			{
				if (ChargeoutsViewDateFilterFrom == null)
				{
					ChargeoutsViewDateFilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					ChargeoutsViewDateFilterTo = DateTimeHelper.LastDayCurrentMonth();
					ChargeoutsViewInvoicesFilterDateMode = 0;
				}

				return new GetChargeoutsViewDateFilterReturn
				{
					FilterFrom = ChargeoutsViewDateFilterFrom.Value,
					FilterTo = ChargeoutsViewDateFilterTo.Value,
					ChargeoutsFilterDateMode = ChargeoutsViewInvoicesFilterDateMode,
				};
			}

			public void SetChargeoutsViewDateFilter(DateTime FilterFrom, DateTime FilterTo, Int32? InvoicesFilterDateMode = null)
			{
				ChargeoutsViewDateFilterFrom = FilterFrom;
				ChargeoutsViewDateFilterTo = FilterTo;
				if (InvoicesFilterDateMode != null)
				{
					ChargeoutsViewInvoicesFilterDateMode = InvoicesFilterDateMode.Value;
				}
				SendSettingsToServer();
			}
		}

		public PayrollAllDoctorsSettings PayrollAllDoctors { get; } = new PayrollAllDoctorsSettings();
		public class PayrollAllDoctorsSettings
		{
			public DateTime? FilterFrom { get; set; }
			public DateTime? FilterTo { get; set; }

			public class Return
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public Return Get()
			{
				if (FilterFrom == null)
				{
					FilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					FilterTo = DateTimeHelper.LastDayCurrentMonth();
				}

				return new Return
				{
					FilterFrom = FilterFrom.Value,
					FilterTo = FilterTo.Value,
				};
			}

			public void Set(DateTime filterFrom, DateTime filterTo)
			{
				FilterFrom = filterFrom;
				FilterTo = filterTo;
				SendSettingsToServer();
			}
		}

		public InventoryListSettings InventoryList { get; } = new InventoryListSettings();
		public class InventoryListSettings
		{
			public DateTime? FilterFrom { get; set; }
			public DateTime? FilterTo { get; set; }

			public class Return
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public Return Get()
			{
				if (FilterFrom == null)
				{
					FilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					FilterTo = DateTimeHelper.LastDayCurrentMonth();
				}

				return new Return
				{
					FilterFrom = FilterFrom.Value,
					FilterTo = FilterTo.Value,
				};
			}

			public void Set(DateTime filterFrom, DateTime filterTo)
			{
				FilterFrom = filterFrom;
				FilterTo = filterTo;
				SendSettingsToServer();
			}
		}



		public InvoicesBuilderSettings InvoicesBuilder { get; } = new InvoicesBuilderSettings();
		public class InvoicesBuilderSettings
		{
			public Boolean IsShowGenerated { get; set; } = true;
		}


		public AppointmentSettings Appointment { get; } = new AppointmentSettings();
		public class AppointmentSettings
		{
			public DateTime? AppointmentViewDateFilterFrom { get; set; }
			public DateTime? AppointmentViewDateFilterTo { get; set; }

			public class GetAppointmentsViewDateFilterReturn
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public GetAppointmentsViewDateFilterReturn GetAppointmentsViewDateFilter()
			{
				if (AppointmentViewDateFilterFrom == null)
				{
					AppointmentViewDateFilterFrom = DateTimeHelper.FirstDayCurrentMonth().AddMonths(-3);
					AppointmentViewDateFilterTo = DateTimeHelper.MAX_DATE;
				}

				return new GetAppointmentsViewDateFilterReturn
				{
					FilterFrom = AppointmentViewDateFilterFrom.Value,
					FilterTo = AppointmentViewDateFilterTo.Value,
				};
			}

			public void SetAppointmentsViewDateFilter(DateTime FilterFrom, DateTime FilterTo)
			{
				AppointmentViewDateFilterFrom = FilterFrom;
				AppointmentViewDateFilterTo = FilterTo;
				SendSettingsToServer();
			}

			public String DefaultActiveViewType { get; set; }

			public void SetDefaultActiveViewType(string defaultActiveViewType)
			{
				DefaultActiveViewType = defaultActiveViewType;
				SendSettingsToServer();
			}
		}

		public CalendarEventSettings CalendarEvent { get; } = new CalendarEventSettings();
		public class CalendarEventSettings
		{
			public DateTime? CalendarEventViewDateFilterFrom { get; set; }
			public DateTime? CalendarEventViewDateFilterTo { get; set; }

			public class GetCalendarEventsViewDateFilterReturn
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public GetCalendarEventsViewDateFilterReturn GetCalendarEventsViewDateFilter()
			{
				if (CalendarEventViewDateFilterFrom == null)
				{
					CalendarEventViewDateFilterFrom = DateTimeHelper.FirstDayCurrentMonth().AddMonths(-3);
					CalendarEventViewDateFilterTo = DateTimeHelper.MAX_DATE;
				}

				return new GetCalendarEventsViewDateFilterReturn
				{
					FilterFrom = CalendarEventViewDateFilterFrom.Value,
					FilterTo = CalendarEventViewDateFilterTo.Value,
				};
			}

			public void SetCalendarEventsViewDateFilter(DateTime FilterFrom, DateTime FilterTo)
			{
				CalendarEventViewDateFilterFrom = FilterFrom;
				CalendarEventViewDateFilterTo = FilterTo;
				SendSettingsToServer();
			}
		}


		public PatientsListSettings PatientsList { get; } = new PatientsListSettings();
		public class PatientsListSettings
		{
			public Boolean IsShowAppointments { get; set; } = true;

		}

		public LeftNavigationPanelSettings LeftNavigationPanel { get; } = new LeftNavigationPanelSettings();
		public class LeftNavigationPanelSettings
		{
			public Boolean NavBarIsExpanded { get; set; } = true;
		}


		public EmailSendListSettings EmailSendList { get; } = new EmailSendListSettings();
		public class EmailSendListSettings
		{
			public DateTime? FilterFrom { get; set; }
			public DateTime? FilterTo { get; set; }

			public class Return
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public Return Get()
			{
				if (FilterFrom == null)
				{
					FilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					FilterTo = DateTimeHelper.LastDayCurrentMonth();
				}

				return new Return
				{
					FilterFrom = FilterFrom.Value,
					FilterTo = FilterTo.Value,
				};
			}

			public void Set(DateTime filterFrom, DateTime filterTo)
			{
				FilterFrom = filterFrom;
				FilterTo = filterTo;
				SendSettingsToServer();
			}
		}


		public EmailChargeListSettings EmailChargeList { get; } = new EmailChargeListSettings();
		public class EmailChargeListSettings
		{
			public DateTime? FilterFrom { get; set; }
			public DateTime? FilterTo { get; set; }

			public class Return
			{
				public DateTime FilterFrom;
				public DateTime FilterTo;
			}
			public Return Get()
			{
				if (FilterFrom == null)
				{
					FilterFrom = DateTimeHelper.FirstDayCurrentMonth();
					FilterTo = DateTimeHelper.LastDayCurrentMonth();
				}

				return new Return
				{
					FilterFrom = FilterFrom.Value,
					FilterTo = FilterTo.Value,
				};
			}

			public void Set(DateTime filterFrom, DateTime filterTo)
			{
				FilterFrom = filterFrom;
				FilterTo = filterTo;
				SendSettingsToServer();
			}
		}



	}




}
