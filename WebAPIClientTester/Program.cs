using Nito.AsyncEx;
using Profibiz.PracticeManager.Model;
using Profibiz.PracticeManager.Patients.BusinessService;
using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIClientTester
{
    class Program
    {
		static async Task RunTest()
		{
			var bs = new PatientsBusinessService();
			//var ret = await bs.GetPatientList("");
			//var p = bs.GetpatientCoverage(new Guid("11857B00-04A8-4AB0-A15F-48135C5E4258"));

			//var patient = new Patient();
			//await bs.PutPatient(patient);

			//var task = bs.GetPatientPhoto(new Guid("83841FB2-0D30-4482-B2C5-5A91FDB278DB"));
			//var bb = await task;

			var tasks = new List<Task>();
			for (int i = 0; i < 100; i++)
			{
				tasks.Add(Task.Run(async () =>
				{
					DateTime dt1 = DateTime.Now;
					var bb = await bs.GetPatientPhoto(new Guid("83841FB2-0D30-4482-B2C5-5A91FDB278DB"));
					DateTime dt2 = DateTime.Now;
					log("GetPatientPhoto=" + (dt2 - dt1).TotalMilliseconds + ";\t" + dt1.ToString("hh:mm:ss.ffff") + ";" + dt2.ToString("hh:mm:ss.ffff"));
				}));
			}

			var atask = Task.WhenAll(tasks);
			await atask;



			//var response = await _client.GetResponse(_baseUrl, "api/patients/GetPatientPhoto/" + rowId.ToString());
			//var photo = await response.Content.ReadAsByteArrayAsync();
			//return photo;

		}

		static void log(object arg)
		{
			Console.WriteLine(arg);
			Debug.WriteLine(arg);
		}

		static void Main(string[] args)
        {
			AsyncContext.Run(RunTest); 
        }
	}
}
