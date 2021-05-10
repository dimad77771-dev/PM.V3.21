using DevExpress.XtraRichEdit;
using DevExpress.XtraRichEdit.Printing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public class CustomXpfRichEditPrinter : XpfRichEditPrinter
	{
		public CustomXpfRichEditPrinter(IRichEditControl control) : base(control) { }

		public void PrintToMyPrinter(PrintDialog printDialog, String jobName)
		{
			var document = this.CreateFixedDocument();
			printDialog.PrintDocument(document.DocumentPaginator, jobName);
		}
	}
}

