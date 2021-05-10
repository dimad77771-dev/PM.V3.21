using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class UnhandledExceptionProccesing
	{
		private static string _baseUrl = ConfigurationManager.AppSettings["service.url"];

		async static public Task<bool> SendErrorToServer(string err)
		{
			var errorInfo = new ClientError
			{
				RowId = Guid.NewGuid(),
				ErrorDateTime = DateTimeHelper.Now,
				ErrorText = err,
				MachineName = Environment.MachineName,
				OSVersion = Environment.OSVersion.VersionString,
				UserName = Environment.UserName,
				UserDomainName = Environment.UserDomainName
			};

			var _client = new HttpClient();
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			var json = JsonConvert.SerializeObject(errorInfo);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var response = await _client.PostAsync("api/lookups/PostErrorToServer", content);
			if (response.IsSuccessStatusCode)
			{
				return true;
			}
			else
			{
				var errtext = await response.Content.ReadAsStringAsync();
				return false;
			}
		}

		public class ClientError
		{
			public Guid RowId { get; set; }
			public DateTime ErrorDateTime { get; set; }
			public String ErrorText { get; set; }
			public String MachineName { get; set; }
			public String OSVersion { get; set; }
			public String UserName { get; set; }
			public String UserDomainName { get; set; }
		}

		//async public static Task<bool> SendErrorServer()
		//{
		//	var dir = GetErrorDir();
		//	if (Directory.Exists(dir))
		//	{
		//		var files = Directory.EnumerateFiles(dir).Where(q => !q.Contains(GetLastErrorFile())).OrderBy(q => q).ToArray();
		//		if (files.Length > 0)
		//		{
		//			var arg = new PushErrorLogArgument
		//			{
		//				BusinessRowId = UserOptions.GetBusinessRowId(),
		//				UserName = UserOptions.GetUser(),
		//				LogFiles = new List<PushErrorLogArgumentFile>(),
		//			};

		//			foreach (var file in files)
		//			{
		//				var errtext = File.ReadAllText(file);
		//				arg.LogFiles.Add(new PushErrorLogArgumentFile { ErrorFile = file, ErrorText = errtext });
		//			}

		//			await UIFunc.ShowLoading("Send error log...");
		//			var ret = await WebServiceFunc.PushErrorLog(arg);
		//			if (!string.IsNullOrEmpty(ret))
		//			{
		//				await UIFunc.HideLoading();
		//				return false;
		//			}

		//			foreach (var file in files)
		//			{
		//				File.Delete(file);
		//			}
		//		}
		//	}

		//	await UIFunc.HideLoading();
		//	return true;
		//}
	}
}
