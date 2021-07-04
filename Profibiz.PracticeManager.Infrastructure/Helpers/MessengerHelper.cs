using DevExpress.Mvvm;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace Profibiz.PracticeManager.Infrastructure
{
	public static class MessengerHelper
	{
		static MessengerHelper()
		{
			//Messenger.Default = new Messenger(true, ActionReferenceType.StrongReference);
		}

		public static void Send<T>(T msg)
		{
			Messenger.Default.Send(msg);
		}
		public static void SendMsgRowChange<T>(T row, RowAction rowAction)
		{
			Messenger.Default.Send<MsgRowChange<T>>(new MsgRowChange<T> { Row = row, RowAction = rowAction });
		}
		public static void SendMsgRowChange<T>(T row, bool isNew, string options = "")
		{
			Messenger.Default.Send<MsgRowChange<T>>(new MsgRowChange<T>
			{
				Row = row,
				RowAction = isNew ? RowAction.Insert : RowAction.Update,
				Options = options,
			});
		}

		public static void Register<T>(object recipient, Action<T> action)
		{
			Messenger.Default.Register(recipient, action);

			//MessengerExtensions.Register(Messenger.Default, recipient, (T arg) => 
			//{
			//	try
			//	{
			//		action(arg);
			//	}
			//	catch(Exception ex)
			//	{
			//		NLog.Error(ex.ToString());
			//		if (RuntimeHelper.Debuger)
			//		{
			//			throw new AggregateException(ex);
			//		}
			//	}
			//});
		}

		public static void RunAction(Object viewmodel, Action action)
		{
			DispatcherUIHelper.Run(() =>
			{
				if (!ViewModelExtensions.IsViewModelActive(viewmodel)) return;

				try
				{
					action.Invoke();
				}
				catch (Exception ex)
				{
					NLog.Error(ex.ToString());
					if (RuntimeHelper.Debuger)
					{
						throw new AggregateException(ex);
					}
				}
			});
		}

		//public static void RunAction(Action action)
		//{
		//	try
		//	{
		//		action.Invoke();
		//	}
		//	catch (Exception ex)
		//	{
		//		NLog.Error(ex.ToString());
		//		if (RuntimeHelper.Debuger)
		//		{
		//			throw new AggregateException(ex);
		//		}
		//	}
		//}

		public static void UpdateEntities<M,T>(M model, ObservableCollection<T> Entities, T arow, RowAction rowAction, Func<T,T,bool> keyFunc, Expression<Func<T>> selectedEntityExpression)
		{
			if (Entities == null || arow == null) return;

			

			if (rowAction == RowAction.Delete)
			{
				Entities.RemoveRange(q => keyFunc(q, arow));
			}
			else if (rowAction == RowAction.Update)
			{
				var index = Entities.FindIndex(q => keyFunc(q, arow));
				if (index >= 0)
				{
					arow = CloneHelper.Copy(arow);
					Entities[index] = arow;
					var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
					var prop = typeof(M).GetProperty(propName);
					prop.SetValue(model, arow);
				}
			}
			else if (rowAction == RowAction.Insert)
			{
				arow = CloneHelper.Copy(arow);
				Entities.Insert(0, arow);
				var propName = PropertyHelper.GetPropertyName<T>(selectedEntityExpression);
				var prop = typeof(M).GetProperty(propName);
				prop.SetValue(model, arow);
			}
		}
	}

	public class MsgRowChange<T>
	{
		public T Row { get; set; }
		public RowAction RowAction { get; set; }
		public String Options { get; set; }
	}
	public enum RowAction { Insert, Update, Delete }


	public class MsgRowLookupUpdate
	{
		public MsgRowLookupUpdate(TableEnum table)
		{
			Table = table;
		}

		public TableEnum Table { get; set; }
		public enum TableEnum
		{
			MedicalConditions, InsuranceProviders, ProfessionalAssociations, ThirdPartyServiceProviders, AppointmentBooks,
			AppointmentStatuses, CalendarEventStatuses, Referrers, Suppliers, Categories,
			InvoiceStatuses, ChargeoutStatuses, ChargeoutRecipientes,
			PublicHolidays, PatientNoteStatuses, Settings, Users, Templates,
		}
	}


	public class MsgLeftNavigationPanelNeedClosePopUp
	{
	}


}
