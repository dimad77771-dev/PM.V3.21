using DevExpress.Xpf.Grid;
using DevExpress.XtraPrinting;
using DevExpress.XtraRichEdit;
using Microsoft.Practices.ServiceLocation;
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
using System.Windows.Media.Imaging;

namespace Profibiz.PracticeManager.Infrastructure
{
	public static class RichTextFunc
	{
		public static byte[] ConvertDocxToPdf(byte[] docx)
		{
			var rtfServer = new RichEditDocumentServer();
			var docxStream = new MemoryStream(docx);
			var pdfStream = new MemoryStream();

			rtfServer.LoadDocument(docxStream, DocumentFormat.OpenXml);
			rtfServer.ExportToPdf(pdfStream);
			var pdfBytes = pdfStream.ToArray();

			docxStream.Dispose();
			pdfStream.Dispose();
			rtfServer.Dispose();

			var g = new DevExpress.Xpf.RichEdit.RichEditControl();

			return pdfBytes;
		}
	}
}

