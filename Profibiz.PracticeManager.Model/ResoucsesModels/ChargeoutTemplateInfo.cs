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
	public static class ChargeoutTemplateInfo
	{
		public static Template[] All => LookupDataProvider.Instance.Templates.ToArray();

		public static ObservableCollection<Template> GetForChargeoutType(string chargeoutType)
		{
			return All.Where(q => q.InvoiceType == chargeoutType).ToObservableCollection();
		}

		public static string GetDefaultForChargeoutType(string chargeoutType)
		{
			return All.Where(q => q.InvoiceType == chargeoutType && q.IsDefault).FirstOrDefault()?.Code;
		}
	}
}




