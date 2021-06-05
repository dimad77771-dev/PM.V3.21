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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using DevExpress.Xpf.Core;
using Profibiz.PracticeManager.Patients.Views;
using System.Windows.Controls;
using System.Windows;
using DevExpress.Xpf.Editors;
using System.Windows.Data;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	[POCOViewModel]
	public class FormDocmodelViewModel : ViewModelBase
	{
		#region Service
		IPatientsBusinessService businessService = BusinessServiceHelper.GetPatientsBusinessService();
		ILookupsBusinessService lookupsBusinessService = BusinessServiceHelper.GetLookupsBusinessService();
		public ShowWaitIndicator ShowWaitIndicator { get; set; } = new ShowWaitIndicator();
		public IMessageBoxService MessageBoxService => this.GetRequiredService<IMessageBoxService>();
		public InteractionRequest<ShowDXWindowsActionParam> ShowDXWindowsInteractionRequest { get; set; } = new InteractionRequest<ShowDXWindowsActionParam>();
		public virtual GridControlBehaviorManager BehaviorGridConrol { get; set; } = new GridControlBehaviorManager();
		#endregion
		public Guid? AppointmentRowId { get; set; }
		public Guid? PatientRowId { get; set; }
		public MoldelType RowType { get; set; } = MoldelType.Patient;
		public bool IsReadOnly { get; set; } = false;
		//public OpenParams OpenParam { get; set; }
		public virtual ObservableCollection<FormDocmodel> Entities { get; set; }
		public virtual FormDocmodel Entity { get; set; }
		public virtual FormDocmodel[] FormDictionary { get; set; }
		public virtual FormDocmodel CurrentForm { get; set; }
		public virtual String CurrentFormCode { get; set; }
		public virtual FormItem[] FormItems { get; set; }
		public FormDocmodelView MainView { get; set; }
		public Grid MainGrid { get; set; }


		public FormDocmodelViewModel() : base()
		{
		}


		public async Task LoadData()
		{
			FormDictionary = (await GetFormDictionary());
			CurrentForm = FormDictionary.FirstOrDefault(q => q.Form.Code == CurrentFormCode);
			if (CurrentForm == null) return;

			Entities = (await GetList()).ToObservableCollection();
			ResetHasChange();
			if (Entities.Any())
			{
				SetSelectedEntity(Entities[0]);
			}
			else
			{
				if (!IsReadOnly)
				{
					NewCore();
					ResetHasChange();
				}
			}
			ResetHasChange();
		}

		async Task<FormDocmodel[]> GetFormDictionary()
		{
			var qry = "formDictionary=1";
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetFormDocmodelList(qry));
			return rows.ToArray();
		}


		async Task<List<FormDocmodel>> GetList()
		{
			var qry = "patientRowId=" + PatientRowId.ToWebQuery() + "&formRowId=" + CurrentForm.Form.RowId.ToWebQuery();
			var rows = await lookupsBusinessService.RunTaskAndUpdateAllLookups(businessService.GetFormDocmodelList(qry));
			rows = rows.OrderByDescending(q => q.PatientForm.Date).ToList();
			rows.ForEach(q => q.SetupFormDictionary(FormDictionary));
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
			if (Entity.FormItems?.Any(q => q.IsChanged) == true)
			{
				return true;
			}
			if (Entity.AppointmentForm?.IsChanged == true)
			{
				return true;
			}
			if (Entity.PatientForm?.IsChanged == true)
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
				if (q.FormItems != null)
				{
					q.FormItems.ForEach(z => z.IsChanged = false);
				}
				if (q.AppointmentForm != null)
				{
					q.AppointmentForm.IsChanged = false;
				}
				if (q.PatientForm != null)
				{
					q.PatientForm.IsChanged = false;
				}
			});
		}

		bool Validate()
		{
			if (Entity == null) return true;

			List<string> errors = new List<string>();
			ValidateHelper.Empty(Entity.PatientForm.Date, "Date", errors);

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
			var updateEntity = GetSavedItems();

			//save
			ShowWaitIndicator.Show(ShowWaitIndicator.Mode.Save);
			var uret = Entity.IsNew ?
				await businessService.PostFormDocmodel(updateEntity) :
				await businessService.PutFormDocmodel(updateEntity);
			ShowWaitIndicator.Hide();
			if (!uret.Validate(MessageBoxService))
			{
				return false;
			}

			MessengerHelper.SendMsgRowChange(updateEntity, Entity.IsNew);
			Entity.IsNew = false;
			ResetHasChange();

			Entities.ForEach(q =>
			{
				BaseModelHelper.RaisePropertyChanged(q, nameof(FormDocmodel.Date));
			});


			return true;
		}

		FormDocmodel GetSavedItems()
		{
			var model = new FormDocmodel();
			var procdate = DateTime.Today;

			if (RowType == MoldelType.Patient)
			{
				var patientForm = Entity.PatientForm.GetPocoClone();
				model.PatientForm = patientForm;

				var patientFormItems = new List<PatientFormItem>();
				foreach (var formItem in FormItems)
				{
					var patientFormItem = new PatientFormItem
					{
						RowId = Guid.NewGuid(),
						PatientFormRowId = Entity.PatientForm.RowId,
						FormItemRowId = formItem.RowId,
						Date = procdate,
						ValueBoolean = formItem.ValueBoolean,
						ValueDateTime = formItem.ValueDateTime,
						ValueNumeric = formItem.ValueNumeric,
						ValueText = formItem.ValueText,
					};
					patientFormItems.Add(patientFormItem);
				}
				model.PatientFormItems = patientFormItems.ToArray();
			}

			return model;
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
					var rowId = row.PatientForm.RowId;
					var uret = await businessService.DeleteFormDocmodel(rowId);
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
		public bool CanDelete() => (Entity != null && !IsReadOnly);


		void ClearNewRows()
		{
			var delrows = Entities.Where(q => q.IsNew).ToArray();
			Entities.RemoveRange(delrows);
		}

		void NewCore()
		{
			var nrow = new FormDocmodel
			{
				RowId = Guid.NewGuid(),
				IsNew = true,
				PatientForm = new PatientForm
				{
					RowId = Guid.NewGuid(),
					PatientRowId = PatientRowId,
					FormRowId = CurrentForm.Form.RowId,
					Date = DateTime.Today,
				},
			};
			nrow.SetupFormDictionary(FormDictionary);
			Entities.Insert(0, nrow);
			SetSelectedEntity(nrow);
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
		public bool CanNew() => (!IsReadOnly);

		public void BeforeLayoutRefresh(CancelRoutedEventArgs e)
		{
			e.Cancel = cancelBeforeLayoutRefresh;
		}
		bool cancelBeforeLayoutRefresh = true;
		void SetSelectedEntity(FormDocmodel row)
		{
			cancelBeforeLayoutRefresh = false;
			Entity = row;
			SetupFormItems();
			ResetHasChange();
			cancelBeforeLayoutRefresh = true;
		}

		void SetupFormItems()
		{
			var mode3 = new[] { "CONS1","CONS2","TERMS" }.Contains(CurrentFormCode);

			FormItems = Entity.FormItems.OrderBy(q => q.SectionOrder).ThenBy(q => q.OrderInSection).ToArray();

			MainGrid.ColumnDefinitions.Clear();
			MainGrid.RowDefinitions.Clear();
			MainGrid.Children.Clear();

			var columnCount = 2;
			if (mode3)
			{
				columnCount = 1;
			}
			for (int k = 0; k < columnCount; k++)
			{
				if (mode3)
				{
					MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
					MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) });
				}
				else
				{
					MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
					MainGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
				}
			}

			var width2 = 180;
			bool isFirstRow = true;
			bool isFirstSection = true;
			FormItem prevFormItem = null;
			foreach (var formItem in FormItems)
			{
				bool isStartSection = false;
				int row, col;
				if (prevFormItem == null)
				{
					row = 0;
					col = 0;
					isStartSection = true;
				}
				else
				{
					col = prevFormItem.ColNum;
					row = prevFormItem.RowNum;
					if (formItem.SectionOrder == prevFormItem.SectionOrder)
					{
						col++;
						if (col >= columnCount)
						{
							col = 0;
							row++;
						}
					}
					else
					{
						isStartSection = true;
						col = 0;
						row++;
					}
				}

				if (isFirstRow)
				{
					var labelDate = new Label
					{
						Content = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = "Date" },
						Margin = new Thickness(5),
						HorizontalAlignment = HorizontalAlignment.Right,
						HorizontalContentAlignment = HorizontalAlignment.Right,
						VerticalAlignment = VerticalAlignment.Center,
						VerticalContentAlignment = VerticalAlignment.Center,
					};
					MainGrid.Children.Add(labelDate);
					Grid.SetColumn(labelDate, 0);
					Grid.SetRow(labelDate, 0);

					var dateEdit = new DateEdit
					{
						Style = MainView.TryFindResource("baseEditStyle1") as Style,
						Width = width2,
						HorizontalAlignment = HorizontalAlignment.Left,
						IsReadOnly = IsReadOnly,
					};
					var valueBinding = new Binding("PatientForm.Date");
					valueBinding.Source = Entity;
					valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					valueBinding.ValidatesOnDataErrors = true;
					valueBinding.NotifyOnSourceUpdated = true;
					dateEdit.SetBinding(TextEdit.EditValueProperty, valueBinding);

					MainGrid.Children.Add(dateEdit);
					Grid.SetColumn(dateEdit, 1);
					Grid.SetRow(dateEdit, 0);

					row++;
					isFirstRow = false;
				}

				if (isStartSection)
				{
					var labelSection = new Label
					{
						Content = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = formItem.SectionName },  //, TextDecorations = TextDecorations.Underline
						Margin = (!isFirstSection ? new Thickness(5, 20, 5, 5) : new Thickness(5)),
						HorizontalAlignment = HorizontalAlignment.Left,
						HorizontalContentAlignment = HorizontalAlignment.Left,
						VerticalAlignment = VerticalAlignment.Center,
						VerticalContentAlignment = VerticalAlignment.Center,
						Style = MainView.TryFindResource("SectionLabelStyle") as Style,
					};
					labelSection.FontSize = 1.2 * labelSection.FontSize;

					MainGrid.Children.Add(labelSection);
					Grid.SetColumn(labelSection, 0);
					Grid.SetColumnSpan(labelSection, 2);
					Grid.SetRow(labelSection, row);

					row++;
					isFirstSection = false;
				}

				Control label;
				if (mode3)
				{
					label = new TheArtOfDev.HtmlRenderer.WPF.HtmlPanel
					{
						Text = formItem.Name,
						Margin = new Thickness(5),
						HorizontalAlignment = HorizontalAlignment.Right,
						HorizontalContentAlignment = HorizontalAlignment.Right,
						VerticalAlignment = VerticalAlignment.Center,
						VerticalContentAlignment = VerticalAlignment.Center,
						Background = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Transparent),

					};
				}
				else
				{
					var labelName = formItem.Name;
					labelName = (labelName ?? "").Replace("<br/>", "\n");
					label = new Label
					{
						Content = new TextBlock { TextWrapping = TextWrapping.Wrap, Text = labelName },
						Margin = new Thickness(5),
						HorizontalAlignment = HorizontalAlignment.Right,
						HorizontalContentAlignment = HorizontalAlignment.Right,
						VerticalAlignment = VerticalAlignment.Center,
						VerticalContentAlignment = VerticalAlignment.Center,
					};
				}

				MainGrid.Children.Add(label);
				Grid.SetColumn(label, col * 2);
				Grid.SetRow(label, row);

				BaseEdit textEdit = null;
				string valueColumn = null;
				if (formItem.IsText == true)
				{
					textEdit = new TextEdit
					{
						Style = MainView.TryFindResource("baseEditStyle1") as Style,
						IsReadOnly = IsReadOnly,
					};
					valueColumn = nameof(formItem.ValueText);
				}
				else if (formItem.IsBoolean == true)
				{
					textEdit = new CheckEdit
					{
						Style = MainView.TryFindResource("baseEditStyle1") as Style,
						IsReadOnly = IsReadOnly,
					};
					valueColumn = nameof(formItem.ValueBoolean);
					if (formItem.ValueBoolean == null)
					{
						formItem.ValueBoolean = false;
					}
				}
				else if (formItem.IsDateTime == true)
				{
					textEdit = new DateEdit
					{
						Style = MainView.TryFindResource("baseEditStyle1") as Style,
						Width = width2,
						HorizontalAlignment = HorizontalAlignment.Left,
						IsReadOnly = IsReadOnly,
					};
					valueColumn = nameof(formItem.ValueDateTime);
				}
				else if (formItem.IsNumeric == true)
				{
					textEdit = new TextEdit
					{
						Style = MainView.TryFindResource("baseEditStyle1") as Style,
						MaskType = MaskType.Numeric,
						Width = width2,
						HorizontalAlignment = HorizontalAlignment.Left,
						IsReadOnly = IsReadOnly,
					};
					valueColumn = nameof(formItem.ValueNumeric);
				}

				if (textEdit != null)
				{
					var valueBinding = new Binding(valueColumn);
					valueBinding.Source = formItem;
					valueBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
					valueBinding.ValidatesOnDataErrors = true;
					valueBinding.NotifyOnSourceUpdated = true;
					textEdit.SetBinding(TextEdit.EditValueProperty, valueBinding);

					MainGrid.Children.Add(textEdit);
					Grid.SetColumn(textEdit, col * 2 + 1);
					Grid.SetRow(textEdit, row);
				}


				formItem.RowNum = row;
				formItem.ColNum = col;
				prevFormItem = formItem;
			}

			var totalRows = FormItems.Max(q => q.RowNum) + 1;
			for (int i = 0; i < totalRows; i++)
			{
				MainGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
			}
		}



		public void MouseClick(System.Windows.Input.MouseButtonEventArgs e)
		{
			DispatcherUIHelper.Run(async () =>
			{
				var hitInfo = BehaviorGridConrol.GetCalcHitInfo(e);
				if (hitInfo == null) return;

				var row = BehaviorGridConrol.GetRow<FormDocmodel>(hitInfo.RowHandle);
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


		//public class OpenParams
		//{
		//	public Guid? AppointmentRowId { get; set; }
		//	public Guid? PatientRowId { get; set; }
		//	public MoldelType Type { get; set; } = MoldelType.Patient;
		//}
		public enum MoldelType { Patient, Appointment }


	}


}
