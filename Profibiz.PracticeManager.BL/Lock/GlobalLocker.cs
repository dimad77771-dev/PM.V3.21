using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;


namespace Profibiz.PracticeManager.BL
{
	public static class GlobalLocker
	{
		public static Object InvoiceUpdate { get; set; } = new object();
		public static Object ChargeoutUpdate { get; set; } = new object();
	}
}