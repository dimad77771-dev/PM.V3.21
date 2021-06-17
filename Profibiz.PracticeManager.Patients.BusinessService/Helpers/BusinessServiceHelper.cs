using Profibiz.PracticeManager.Patients.BusinessServiceInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.ServiceLocation;
using Profibiz.PracticeManager.Model;
using Newtonsoft.Json;
using System.Net;
using Profibiz.PracticeManager.SharedCode;

namespace Profibiz.PracticeManager.Patients.BusinessService
{
    public static class BusinessServiceHelper
    {
        public static IPatientsBusinessService GetPatientsBusinessService()
        {
            return ServiceLocator.Current.GetInstance<IPatientsBusinessService>();
        }

        public static ILookupsBusinessService GetLookupsBusinessService()
        {
            return ServiceLocator.Current.GetInstance<ILookupsBusinessService>();
        }

        async static public Task<UpdateReturn> ValidateResponse(this HttpResponseMessage response, bool throwException = false)
		{
			if (response.IsSuccessStatusCode)
			{
				var json = await response.Content.ReadAsStringAsync();
				return new UpdateReturn
				{
					IsSuccess = true,
					ResponseJson = json,
				};
			}

			var errtext = await response.Content.ReadAsStringAsync();
			var ret = new UpdateReturn
			{
				IsSuccess = false,
				AllErrorText = errtext,
			};

			if (response.StatusCode == (HttpStatusCode)422)
			{
				ret.IsUserError = true;
				//ret.UserErrorAllJson = errtext;
				var errobj = JsonConvert.DeserializeObject<UserErrorInformation>(errtext);
				ret.UserErrorText = errobj.Message;
				ret.UserErrorCode = errobj.Code;
				ret.UserErrorInfoObjectJson = errobj.InfoObjectJson;
			}
			else
			{
				BusinessServiceExceptionInfo exceptionInfo;
				try
				{
					exceptionInfo = JsonConvert.DeserializeObject<BusinessServiceExceptionInfo>(errtext);
					if (exceptionInfo != null)
					{
						ret.Message = exceptionInfo.Message;
						ret.ExceptionMessage = exceptionInfo.ExceptionMessage;
						ret.ExceptionType = exceptionInfo.ExceptionType;
						ret.StackTrace = exceptionInfo.StackTrace;
					}
				}
				catch (Exception) { }
			}

			if (throwException)
			{
				throw new AggregateException(new BusinessServiceException(ret));
				//throw new Exception(BusinessServiceException.UpdateReturnToString(ret));
			}
			return ret;
		}

		async static public Task<HttpResponseMessage> GetResponse(this HttpClient _client, string _baseUrl, string requestUri)
		{
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

			HttpResponseMessage response = await _client.GetAsync(requestUri);
			var rrr = await ValidateResponse(response, true);
			var gg = rrr;

			return response;
		}

		async static public Task<HttpResponseMessage> GetResponseWithException(this HttpClient _client, string _baseUrl, string requestUri, int timeoutSeconds = 0)
		{
			_client.BaseAddress = new Uri(_baseUrl);
			_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
			if (timeoutSeconds > 0)
			{
				_client.Timeout = TimeSpan.FromSeconds(timeoutSeconds);
			}

			HttpResponseMessage response = null;
			try
			{
				response = await _client.GetAsync(requestUri);
			}
			catch (Exception ex)
			{
				return null;
			}
			var rrr = await ValidateResponse(response, true);

			return response;
		}


		async static public Task UpdateAllLookups()
		{
			var lookupsBusinessService = GetLookupsBusinessService();
			await lookupsBusinessService.UpdateAllLookups();
		}



		public class BusinessServiceExceptionInfo
		{
			public string Message { get; set; }
			public string ExceptionMessage { get; set; }
			public string ExceptionType { get; set; }
			public string StackTrace { get; set; }
			public BusinessServiceExceptionInfo InnerException { get; set; }
		}
	}









}
