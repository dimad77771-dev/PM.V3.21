using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;
using System.Linq.Expressions;
using Profibiz.PracticeManager.Model;
using System.Reflection;
using System.Net.Http;
using System.Web.Http;
using System.Net;
using Profibiz.PracticeManager.SharedCode;
using Newtonsoft.Json;
using System.Text;

namespace Profibiz.PracticeManager.BL
{
	public static class ExceptionHelper
	{
		public static void UserUpdateError(UserErrorCodes code, String userErrorText = null, Object infoObject = null)
		{
			if (string.IsNullOrEmpty(userErrorText))
			{
				userErrorText = code.ToString();
			}

			var userInfo = new UserErrorInformation
			{
				Code = code,
				Message = userErrorText,
			};
			if (infoObject != null)
			{
				userInfo.InfoObjectJson = JsonConvert.SerializeObject(infoObject);
			}
			var json = JsonConvert.SerializeObject(userInfo);

			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var message = new HttpResponseMessage((HttpStatusCode)422)
			{
				Content = content
			};
			throw new HttpResponseException(message);
		}

		public static bool IsDeleteReferenceConstraintException(Exception ex)
		{
			var message = (ex.InnerException?.InnerException?.Message ?? "");
			return 
				(
					message.Contains("The DELETE statement conflicted with the REFERENCE constraint") || 
					message.Contains("The DELETE statement conflicted with the SAME TABLE REFERENCE constraint")
				);
		}

		public static bool IsInvoiceNumberDuplicateConstraintException(Exception ex)
		{
			var message = (ex.InnerException?.InnerException?.Message ?? "");
			return message.Contains("Violation of UNIQUE KEY constraint 'UK_Invoices_InvoiceNumber'");
		}

		public static bool IsChargeoutNumberDuplicateConstraintException(Exception ex)
		{
			var message = (ex.InnerException?.InnerException?.Message ?? "");
			return message.Contains("Violation of UNIQUE KEY constraint 'UK_Chargeouts_ChargeoutNumber'");
		}


		public static bool IsCHECKVIEW_InsuranceCoveragesDuplicateConstraintException(Exception ex)
		{
			var message = (ex.InnerException?.InnerException?.Message ?? "");
			return message.Contains("Cannot insert duplicate key row in object 'dbo.CHECKVIEW_InsuranceCoverages' with unique index 'IX_CHECKVIEW_InsuranceCoverages'");
			//Cannot insert duplicate key row in object 'dbo.CHECKVIEW_InsuranceCoverages' with unique index 'IX_CHECKVIEW_InsuranceCoverages'. The duplicate key value is (541200, 4e248013-7e6b-437a-87f8-cdeb1a9dd4cb, Jan  1 2018 12:00AM).
		}


		public static void InternalError(string err = "")
		{
			err = "Server return update error" + (string.IsNullOrEmpty(err) ? "" : ". " + err); 
			UserUpdateError(UserErrorCodes.InternalError, err);
		}
	}


	
}