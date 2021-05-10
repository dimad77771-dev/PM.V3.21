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
using Xtra = DevExpress.XtraScheduler;
using Profibiz.PracticeManager.Model;
using System.Windows.Data;
using System.Globalization;
using System.Windows.Markup;
using System.Drawing;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Scheduler.Drawing;
using System.Windows.Media;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public class MyMonthViewDayHeaderControl : MonthViewDayHeaderControl
	{
		protected override string[] CalculateCaptions()
		{
			//return base.CalculateCaptions();
			//return new string[] { Date.Day.ToString() };
			//return new string[] { "B" };
			//return new string[] { Date.ToString("MMM dd, yyyy") };
			return new string[] { Date.ToString("MMM dd") };
		}
	}

}
