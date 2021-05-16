using DevExpress.Mvvm;
using Profibiz.PracticeManager.Infrastructure;
using Profibiz.PracticeManager.InfrastructureExt.Common;
using Profibiz.PracticeManager.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
	public class MyHttpClient : HttpClient
	{
		public MyHttpClient()
		{
			if (UserManager.UserRowId != null)
			{
				this.DefaultRequestHeaders.Add("CurrentUserRowId", UserManager.UserRowId.ToString());
			}
		}
	}
}
