using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EF = Profibiz.PracticeManager.EF;
using DTO = Profibiz.PracticeManager.DTO;
using System.Transactions;
using EntityFramework.Extensions;
using System.Data.SqlClient;
using Profibiz.PracticeManager.SharedCode;
using MimeKit;
using System.Configuration;
using MailKit.Net.Smtp;
using System.Security.Cryptography;
using System.IO;
using Newtonsoft.Json;

namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		public static string ShellBaseCatalog { get; set; } = ConfigurationManager.AppSettings["shell.base.catalog"];

		public ShellUpgradeUtils.ShellFileInfo[] GetShellFiles(int all)
		{
			return ShellUpgradeUtils.GetShellFiles(ShellBaseCatalog, all);
		}

		public byte[] GetShellFile(string file)
		{
			return File.ReadAllBytes(Path.Combine(ShellBaseCatalog, file));
		}

	}
}
