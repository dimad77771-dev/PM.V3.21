using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profibiz.PracticeManager.Model;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Specialized;

namespace Profibiz.PracticeManager.Patients.BusinessServiceInterface
{
    public static class BusinessServiceHelprer
	{
		static IPatientsBusinessService businessService => ServiceLocator.Current.GetInstance<IPatientsBusinessService>();

		public async static Task<List<Payment>> GetPaymentList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("patientRowId", patientRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paymentDateFrom", paymentDateFrom);
			values.Add("paymentDateTo", paymentDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetPaymentList(query);
		}

		public async static Task<List<Paycharge>> GetPaychargeList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paychargeDateFrom = null, DateTime? paychargeDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("patientRowId", patientRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paychargeDateFrom", paychargeDateFrom);
			values.Add("paychargeDateTo", paychargeDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetPaychargeList(query);
		}

		public async static Task<List<SupplierPayment>> GetSupplierPaymentList(Guid? rowId = null, Guid? supplierRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("supplierRowId", supplierRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paymentDateFrom", paymentDateFrom);
			values.Add("paymentDateTo", paymentDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetSupplierPaymentList(query);
		}

		public async static Task<List<Refund>> GetRefundList(Guid? rowId = null, Guid? patientRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("patientRowId", patientRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paymentDateFrom", paymentDateFrom);
			values.Add("paymentDateTo", paymentDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetRefundList(query);
		}

		public async static Task<List<Refcharge>> GetRefchargeList(Guid? rowId = null, Guid? chargeoutRecipientRowId = null, int? hasNoDistributedAmount = null, DateTime? paychargeDateFrom = null, DateTime? paychargeDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("chargeoutRecipientRowId", chargeoutRecipientRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paychargeDateFrom", paychargeDateFrom);
			values.Add("paychargeDateTo", paychargeDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetRefchargeList(query);
		}

		public async static Task<List<SupplierRefund>> GetSupplierRefundList(Guid? rowId = null, Guid? supplierRowId = null, int? hasNoDistributedAmount = null, DateTime? paymentDateFrom = null, DateTime? paymentDateTo = null)
		{
			var values = new Dictionary<string, object>();
			values.Add("rowId", rowId);
			values.Add("supplierRowId", supplierRowId);
			values.Add("hasNoDistributedAmount", hasNoDistributedAmount);
			values.Add("paymentDateFrom", paymentDateFrom);
			values.Add("paymentDateTo", paymentDateTo);

			var query = Dictionary2Query(values);
			return await businessService.GetSupplierRefundList(query);
		}


		static string Dictionary2Query(Dictionary<string, object> vals)
		{
			var query = "";
			foreach (var val in vals)
			{
				var vv = val.Value;
				if (vv != null)
				{
					var str = vv.ToString();
					if (vv is DateTime || vv is DateTime?)
					{
						str = ((DateTime)vv).ToString("yyyy-MM-dd");
					}
					query += (query == "" ? "" : "&") + val.Key + "=" + str;
				}
			}
			return query;
		}

	}
}
