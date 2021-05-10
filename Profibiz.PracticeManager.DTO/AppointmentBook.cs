using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.DTO
{
	public partial class AppointmentBook
	{
		public AppointmentBook()
		{
			this.ServiceProviders = new HashSet<ServiceProvider>();
			this.Appointments = new HashSet<Appointment>();
		}

		public System.Guid RowId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public Nullable<int> DisplayOrder { get; set; }
		public System.DateTime StartAt { get; set; }
		public System.DateTime FinishAt { get; set; }
		public int Interval { get; set; }
		public string AppointmentBackgroundColor { get; set; }
		public string AppointmentForegroundColor { get; set; }


		public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
		public virtual ICollection<Appointment> Appointments { get; set; }
	}
}
