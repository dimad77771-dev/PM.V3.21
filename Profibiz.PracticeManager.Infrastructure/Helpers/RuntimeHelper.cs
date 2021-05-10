using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class RuntimeHelper
	{
		public static bool Debuger => System.Diagnostics.Debugger.IsAttached;
		public static bool Release => !Debuger;
		public static bool IsMachineD => (GetMachineUniq() == "beb37e45-3470-4d05-8e19-382a94c41ab3");

		public static string GetUserCode()
		{
			var usercode = Environment.UserName;
			if (string.IsNullOrEmpty(usercode))
			{
				usercode = "<default user>";
			}
			return usercode;
		}

		public static string machineUniq = "";
		public static string GetMachineUniq()
		{
			if (string.IsNullOrEmpty(machineUniq))
			{
				machineUniq = ReadCryptographyCryptographyMachineGuid();
				if (string.IsNullOrEmpty(machineUniq)) throw new LogicalException("MachineGuid Key is not defined");
			}
			return machineUniq.ToLower();
		}


		static string ReadCryptographyCryptographyMachineGuid()
		{
			// start out trying to read machine guid on 32 bit machine
			object value = Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Cryptography", @"MachineGuid", (object)"defaultValue");

			if (value != null && value.ToString() != "defaultValue")
			{
				return value.ToString();
			}

			// read machine guid on 64 bit machine
			RegistryKey regKeyBase = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
			RegistryKey regKey = regKeyBase.OpenSubKey(@"SOFTWARE\Microsoft\Cryptography", RegistryKeyPermissionCheck.ReadSubTree);
			value = regKey.GetValue("MachineGuid", (object)"defaultValue");

			regKeyBase.Close();
			regKey.Close();

			if (value != null && value.ToString() != "defaultValue")
			{
				return value.ToString();
			}

			return string.Empty;
		}
	}
}
