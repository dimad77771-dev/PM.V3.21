using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Configuration;
using System.Threading.Tasks;
using System.Net;
using RestSharp;
using Vonage;
using Vonage.Request;

namespace Profibiz.PracticeManager.Service
{
	public static class SmsFunc
	{
		public static string NexmoApiKey { get; set; } = ConfigurationManager.AppSettings["nexmo.api.key"];
		public static string NexmoApiSecret { get; set; } = ConfigurationManager.AppSettings["nexmo.api.secret"];
		public static string NexmoFrom { get; set; } = ConfigurationManager.AppSettings["nexmo.from"];
		public static string SendRealSMS { get; set; } = ConfigurationManager.AppSettings["nexmo.sendRealSMS"];

		public static string SendSms(string phone, string message)
		{
			if (SendRealSMS != "1")
			{
				return "";
			}

			if (phone == @"(111) 111-1111" || phone == @"(222) 222-2222")
			{
				phone = "+380675099163";
			}
			else
			{
				phone = string.Join("", (phone ?? "").Where(q => char.IsDigit(q)));
				phone = phone.Length == 10 ? "+1" + phone : "+" + phone;
			}

			var credentials = Credentials.FromApiKeyAndSecret(
			NexmoApiKey,
			NexmoApiSecret
			);

			var vonageClient = new VonageClient(credentials);

			try
			{
				var response = vonageClient.SmsClient.SendAnSms(new Vonage.Messaging.SendSmsRequest()
				{
					To = phone,
					From = NexmoFrom,
					Text = message,
				});
			}
			catch (Exception ex)
			{
				return ex.ToString();
			}

			return "";
		}
   //     public static bool SendSms__old(string phone, string message)
   //     {
   //         System.Diagnostics.Debug.WriteLine("SMS=" + message);

   //         if (SendRealSMS != "1")
   //         {
   //             return true;
   //         }

   //         if (phone == @"(111) 111-1111" || phone == @"(222) 222-2222")
   //         {
   //             phone = "+380675099163";
   //         }
   //         else
   //         {
   //             phone = string.Join("", (phone ?? "").Where(q => char.IsDigit(q)));
   //             phone = phone.Length == 10 ? "+1" + phone : "+" + phone;
   //         }

   //         var savedSecurityProtocol = ServicePointManager.SecurityProtocol;
   //         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
   //         var plivo = new RestAPI(auth_id, auth_token);
   //         var resp = plivo.send_message(new Dictionary<string, string>()
   //                  {
   //                      { "src", phoneSrc },	// Sender's phone number with country code
			//	{ "dst", phone },		// Receiver's phone number wiht country code
			//	{ "text", message }		// Your SMS text message
			//});
   //         if (resp.StatusCode == HttpStatusCode.Accepted)
   //         {
   //             return true;
   //         }
   //         else
   //         {
   //             //LogFunc.WriteElmahException(new Exception($"SMS not sent. Phone: {phone}. Message: {message}", resp.ErrorException));
   //             return false;
   //         }


   //         var savedSecurityProtocol = ServicePointManager.SecurityProtocol;
   //         ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
   //         var client = new RestClient("https://rest.nexmo.com");
   //         var request = new RestRequest("sms/json", Method.POST);

   //         request.AddParameter("api_key", NexmoApiKey);
   //         request.AddParameter("api_secret", NexmoApiSecret);
   //         request.AddParameter("from", NexmoFrom);
   //         phone = "+380675099163";
   //         phone = "+14168758075";
   //         phone = "4168758075";
   //         phone = "4168759717";
   //         request.AddParameter("to", phone);
   //         request.AddParameter("text", message);

   //         var response = client.Execute(request);
   //         if (response.ErrorException != null)
   //         {
   //             NLog.Error(new Exception($"SMS not sent. Phone: {phone}. Message: {message}", response.ErrorException));
   //             return false;
   //         }
   //         var content = response.Content ?? "";
   //         if (!content.Contains("status\": \"0"))
   //         {
   //             NLog.Error(new Exception($"SMS not sent. Phone: {phone}. Message: {message}. Response: {response} ({content})"));
   //             return false;
   //         }

   //         return true;
   //     }
    }
}
