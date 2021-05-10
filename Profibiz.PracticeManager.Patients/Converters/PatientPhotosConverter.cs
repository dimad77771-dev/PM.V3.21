using System;
using System.Globalization;
using System.Windows.Data;
using DevExpress.DevAV;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Profibiz.PracticeManager.Patients.Converters
{
    public class PatientPhotosConverter : IValueConverter
	{
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			var businessService = ServiceLocator.Current.GetInstance<IPatientsBusinessService>();
			var rowId = value as Guid?;
			if (rowId == null)
			{
				return null;
			}
			//var task = businessService.GetPatientPhoto((Guid)rowId);
			//return task;
			//task.Wait(2000);
			//return task.Result;

			var task = System.Threading.Tasks.Task.Run(async () =>
			{
				//Debug.WriteLine("ret=" + rowId);
				//var ret = await businessService.GetPatientPhoto((Guid)rowId);
				//Debug.WriteLine("ret=" + ret?.Length);
				//return ret;
				var bytes = File.ReadAllBytes(@"E:\PROJECTS\Profibiz.PracticeManager\___SQL\_Photos\095cbda2-951b-40ae-a6cf-fa4d6410987b.png");
				return bytes;
			});
			return Nito.AsyncEx.NotifyTaskCompletion.Create(task);
			//return Nito.AsyncEx.NotifyTaskCompletion(task);
			//return new TaskCompletionNotifier<byte[]>(task);


			//var bytes = File.ReadAllBytes(@"E:\PROJECTS\Profibiz.PracticeManager\___SQL\_Photos\095cbda2-951b-40ae-a6cf-fa4d6410987b.png");
			//return bytes;

			//var photo = businessService.GetPatientPhoto((Guid)rowId).Result;
			//return photo;

		}

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
    }
}
