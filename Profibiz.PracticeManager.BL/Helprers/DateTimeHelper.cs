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
	public static class DateTimeHelper
	{
		public static DateTime Now => DateTime.Now;
	}
}