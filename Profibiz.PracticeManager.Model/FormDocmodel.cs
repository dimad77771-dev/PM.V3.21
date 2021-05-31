using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Infrastructure;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public class FormDocmodel
	{
		public FormDocmodel()
		{
		}

		public Guid RowId { get; set; }

		public Form Form { get; set; }
		public FormItem[] FormItems { get; set; }
		public AppointmentForm AppointmentForm { get; set; }
		public AppointmentFormItem[] AppointmentFormItems { get; set; }
		public PatientForm PatientForm { get; set; }
		public PatientFormItem[] PatientFormItems { get; set; }

		public DateTime? Date => PatientForm?.Date ?? AppointmentForm?.Date;

		public bool IsNew { get; set; }

		public bool IsChanged { get; set; }
		public string Rowtype9 => "-";

		public void SetupFormDictionary(FormDocmodel[]  FormDictionary)
		{
			var formRowId = PatientForm?.FormRowId;
			if (formRowId != null)
			{
				var oneFormDictionary = FormDictionary.Single(q => q.Form.RowId == formRowId);
				Form = oneFormDictionary.Form.GetPocoClone();
				FormItems = oneFormDictionary.FormItems.Select(q => q.GetPocoClone()).ToArray();
			}

			foreach (var formItem in FormItems)
			{
				if (formItem.RowId == new Guid("0DC3E2E4-674A-4E52-9BBA-A64BF5E1C9BE"))
				{
					var g = 100;
				}

				{
					var irow = PatientFormItems?.FirstOrDefault(q => q.FormItemRowId == formItem.RowId);
					if (irow != null)
					{
						formItem.ValueText = irow.ValueText;
						formItem.ValueDateTime = irow.ValueDateTime;
						formItem.ValueBoolean = irow.ValueBoolean;
						formItem.ValueNumeric = irow.ValueNumeric;
					}
				}
				{
					var irow = AppointmentFormItems?.FirstOrDefault(q => q.FormItemRowId == formItem.RowId);
					if (irow != null)
					{
						formItem.ValueText = irow.ValueText;
						formItem.ValueDateTime = irow.ValueDateTime;
						formItem.ValueBoolean = irow.ValueBoolean;
						formItem.ValueNumeric = irow.ValueNumeric;
					}
				}
			}
		}
	}
}
