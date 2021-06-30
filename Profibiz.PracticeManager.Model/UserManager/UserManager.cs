using DevExpress.Mvvm.UI;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;
using System.Windows;

namespace Profibiz.PracticeManager.Model
{
    public static class UserManager
	{ 
		public static Guid? UserRowId { get; set; }
		public static String UserName { get; set; }
		public static User Role { get; set; }

		//public static Window ShellWindow { get; set; }
	}
}
