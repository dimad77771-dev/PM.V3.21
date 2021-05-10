using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
	[ImplementPropertyChanged]
	public partial class AppointmentBook
	{
		public AppointmentBook()
		{
		}

		public Guid RowId { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
		public int? DisplayOrder { get; set; }
		public DateTime StartAt { get; set; }
		public DateTime FinishAt { get; set; }
		public int Interval { get; set; }
		public string AppointmentBackgroundColor { get; set; }
		public string AppointmentForegroundColor { get; set; }


		public virtual ObservableCollection<ServiceProvider> ServiceProviders { get; set; } = new ObservableCollection<ServiceProvider>();
		public virtual ObservableCollection<Appointment> Appointments { get; set; } = new ObservableCollection<Appointment>();

		public bool IsChanged { get; set; }
		public bool IsNew { get; set; }

		public string Rowtype9 => "-";

	}
}
