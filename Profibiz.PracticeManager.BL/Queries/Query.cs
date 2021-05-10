using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using LinqKit;
using System.Data.Entity;
using Profibiz.PracticeManager.EF;


namespace Profibiz.PracticeManager.BL
{
	public static class Query
	{
		//имеет ли <Patient> страховку <InsuranceProvider> ?
		public static IQueryable<Guid> GetPatientRowIdByInsuranceProvidersViewGroupRowId(this PracticeManagerEntities db, Guid? insuranceProvidersViewGroupRowId)
		{
			var qryPatient = db.PatientInsuranceProvidersViewGroupViews
				.Where(q => q.InsuranceProvidersViewGroupRowId == insuranceProvidersViewGroupRowId)
				.Select(q => q.PatientRowId);
			return qryPatient;
		}
	}
}

