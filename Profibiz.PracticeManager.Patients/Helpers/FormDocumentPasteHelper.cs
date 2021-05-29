using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Core;
using DevExpress.Xpf.Core.Native;
using Profibiz.PracticeManager.Infrastructure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Profibiz.PracticeManager.Patients.ViewModels
{
	public static class FormDocumentPasteHelper
	{
		public static List<FormDocumentPasteClass> AllFormDocumentPasteItems { get; set; } = BuildAllFormDocumentPasteItems();

		public static List<FormDocumentPasteClass> BuildAllFormDocumentPasteItems()
		{
			return EnumFunc.GetValues<FormDocumentPasteEnum>().Select(q => new FormDocumentPasteClass(q)).ToList();
		}


	}

	public class FormDocumentPasteClass
	{
		public FormDocumentPasteEnum Type { get; set; }
		public String Name { get; set; }

		public FormDocumentPasteClass(FormDocumentPasteEnum type)
		{
			Type = type;
			Name = Enum.GetName(typeof(FormDocumentPasteEnum), type).Replace("_", " ").PadRight(50 - Enum.GetName(typeof(FormDocumentPasteEnum), type).Length, ' ');
		}
	}


	public enum FormDocumentPasteEnum
	{
		Patient_Name,
		Appoinment_Date,
	}

	public class FormDocumentPasteFieldsBarSubItem : BarSubItem
	{
		public FormDocumentPasteFieldsBarSubItem()
		{
			this.Content = "Paste Filed";
			var a32 = (DXImageInfo)new DXImageConverter().ConvertFromString("BONote_32x32.png");
			var a16 = (DXImageInfo)new DXImageConverter().ConvertFromString("BONote_16x16.png");
			this.LargeGlyph = new BitmapImage(a32.MakeUri());
			this.Glyph = new BitmapImage(a16.MakeUri());
			this.GetItemData += OnGetItemData;
		}

		void OnGetItemData(object sender, EventArgs e)
		{
			UpdateItems();
		}

		void UpdateItems()
		{
			foreach (var item in FormDocumentPasteHelper.AllFormDocumentPasteItems)
			{
				AppendItem(item);
			}
		}

		void AppendItem(FormDocumentPasteClass arg)
		{
			BarButtonItem item = new BarButtonItem();
			item.Content = arg.Name;
			item.CommandParameter = arg;
			var binding = new Binding();
			binding.Path = new PropertyPath("FormDocumentPasteApplyCommand");
			BindingOperations.SetBinding(item, BarButtonItem.CommandProperty, binding);
			ItemLinks.Add(item);
		}

	}
}
