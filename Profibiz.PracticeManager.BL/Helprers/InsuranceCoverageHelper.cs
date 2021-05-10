using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using DTO = Profibiz.PracticeManager.DTO;
using EF = Profibiz.PracticeManager.EF;
using System.Linq.Expressions;
using Profibiz.PracticeManager.Model;

namespace Profibiz.PracticeManager.BL
{
	public static class InsuranceCoverageHelper
	{
		public static EF.Patient GetPolicyOwner(this EF.InsuranceCoverage insuranceCoverage)
		{
			var ret = insuranceCoverage.InsuranceCoverageHolders.FirstOrDefault(z => z.PolicyHolderType == TypeHelper.PolicyHolderType.Owner)?.Patient;
			return ret;
		}


		//public static Guid[] PatientInsuranceCoveragesRowIds(this EF.InsuranceCoverage insuranceCoverage)
		//{
		//	var ret = insuranceCoverage.InsuranceCoverageHolders.FirstOrDefault(z => z.PolicyHolderType == TypeHelper.PolicyHolderType.Owner)?.Patient;
		//	return ret;
		//}

	}
}