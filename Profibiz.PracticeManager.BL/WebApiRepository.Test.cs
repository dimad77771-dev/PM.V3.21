using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Profibiz.PracticeManager.EF;
using System.Transactions;
using EntityFramework.Extensions;
using System.Linq.Expressions;
using LinqKit;
using System.Data.Entity;
using System.Diagnostics;
using Newtonsoft.Json;


namespace Profibiz.PracticeManager.BL
{
    public partial class WebApiRepository
    {
		Random rand = new Random();

		public void TestBuildBigDb001()
		{
			var db = EF.PracticeManagerEntities.Connection;

			
			var allPatients = db.Patients.ToArray();
			var serviceProviders = db.ServiceProviders.ToArray();
			var appointmentBooks = db.AppointmentBooks.ToArray();

			var serviceProvider = GetRandom(serviceProviders);
			var appointment = new AppointmentT
			{
				RowId = Guid.NewGuid(),
				PatientRowId = GetRandom(allPatients).RowId,
				AppointmentBookRowId = (Guid)serviceProvider.AppointmentBookRowId,
				ServiceProviderRowId = serviceProvider.RowId,
				Completed = true,
				Start = new DateTime(2017,01,01, 16,00,00),
				Finish = new DateTime(2017, 01, 01, 17,00,00),
			};

			//var wh = ExpressionFunc.True<EF.PaymentV>();
			//if (rowId != null)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.RowId == rowId);
			//}
			//         if (patientRowId != null)
			//         {
			//             wh = PredicateBuilder.And(wh, q => q.PatientRowId == patientRowId);
			//         }
			//if (hasNoDistributedAmount == 1)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.Amount > q.AmountInInvoices);
			//}
			//if (paymentDateFrom != null)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.PaymentDate >= paymentDateFrom);
			//}
			//if (paymentDateTo != null)
			//{
			//	wh = PredicateBuilder.And(wh, q => q.PaymentDate <= paymentDateTo);
			//}


			//var list = db.PaymentsV.Where(wh.Expand());

			//var mapper = AutoMapperHelper.GetPocoMapper(typeof(EF.PaymentV));
			//return mapper.Map<List<DTO.Payment>>(list);
		}

		T GetRandom<T>(T[] arr)
		{
			return arr[rand.Next(0, arr.Length - 1)];
		}
	}
}

