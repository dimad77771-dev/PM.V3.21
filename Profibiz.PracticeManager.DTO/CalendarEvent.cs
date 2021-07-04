using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class CalendarEvent
	{
		public CalendarEvent()
		{
		}

		public Guid RowId { get; set; }
		public DateTime Start { get; set; }
		public DateTime Finish { get; set; }
		public string Notes { get; set; }
		public Guid? PatientRowId { get; set; }
		public string Description { get; set; }
		public string Type { get; set; }
		public bool AllDay { get; set; }
		public int RemainderInMinutes { get; set; }
		public bool IsDisabled { get; set; }
		public DateTime? SnoozedTo { get; set; }
		public string RefNumber { get; set; }
		public string RefStatus { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? CreateDateTime { get; set; }
		public string UpdatedBy { get; set; }
		public DateTime? UpdatedDateTime { get; set; }
		public bool Completed { get; set; }
		public Guid? Status1RowId { get; set; }
		public Guid? Status2RowId { get; set; }
		public bool IsVacation { get; set; }
		public bool IsBusyEvent { get; set; }
		public Guid? ServiceProviderRowId { get; set; }
		public string PatientFullName { get; set; }
		public string ServiceProviderFullName { get; set; }

		public virtual Patient Patient { get; set; }


		public bool UpdateFlagRemainderFieldsOnly { get; set; }
	}
}
