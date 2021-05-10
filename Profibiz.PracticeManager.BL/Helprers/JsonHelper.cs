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
using Newtonsoft.Json;

namespace Profibiz.PracticeManager.BL
{
	public static class JsonHelper
	{
		public static T DeserializeObject<T>(string json) where T : new()
		{
			if (string.IsNullOrEmpty(json))
			{
				return new T();
			}
			else
			{
				return JsonConvert.DeserializeObject<T>(json);
			}
		}



	}
}