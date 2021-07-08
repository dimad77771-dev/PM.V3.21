using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevExpress.Mvvm;
using DevExpress.Mvvm.DataAnnotations;
using Profibiz.PracticeManager.Infrastructure;
using System.Collections.ObjectModel;


namespace Profibiz.PracticeManager.Model
{
	//public class InvoiceTemplateModel
	//{
	//	public string Name { get; set; }
	//	public string Code { get; set; }
	//	public string InvoiceType { get; set; }
	//	public bool IsDefault { get; set; }
	//}

	public static class InvoiceTemplateInfo
	{
		public static Template[] All => LookupDataProvider.Instance.Templates.ToArray();

		public static ObservableCollection<Template> GetForInvoiceType(string invoiceType)
		{
			return All.Where(q => q.InvoiceType == invoiceType && q.IsEnabled && !q.IsHidden).ToObservableCollection();
		}

		public static string GetDefaultForInvoiceType(string invoiceType)
		{
			return All.Where(q => q.InvoiceType == invoiceType && q.IsDefault).FirstOrDefault()?.Code;
		}
	}
}




