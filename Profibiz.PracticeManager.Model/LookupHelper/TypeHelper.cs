using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Model
{
    public static class TypeHelper
    {
        public static class Sex
        {
            public const string Male = "Male";
            public const string Female = "Female";
            public const string Other = "Other";
        }

        public static class AddressToUse 
        {
            public const string FirstAddres = "FirstAddres";
            public const string SecondAddress = "SecondAddress";
            public const string Email = "Email";
        }

        public static class FamilyMemberType
        {
            public const string Head = "Head";
            public const string Member = "Member";
        }

        public static class SendAccountTo
        {
            public const string ToFamily = "ToFamily";
            public const string To3RdParty = "To3RdParty";
        }

		public static class ChargeModel
		{
			public const string PerVisit = "Per Visit";
			public const string PerHour = "Per Hour";
		}

		public static class MedicalItemType
        {
            public const string Service = "Service";
            public const string Supply = "Supply";
			public const string ThirdPartyService = "Third Party Service";
		}

		public static class CategoryType
		{
			public const string Service = "Service";
			public const string Supply = "Supply";
		}

		public static class TemplateType
		{
			public const string Template = "Template";
			public const string Form = "Form";
		}



		public static class PolicyHolderType
		{
			public const string Owner = "Policy Owner";
			public const string Beneficiary = "Policy Beneficiary";
		}

		public static class InsuranceCoverageYearType
		{
			public const string CalendarYear = "Calendar Year";
			public const string BeneficialYear = "Beneficial Year";
			public const string AcademicYear = "Academic Year";
		}	

		public static class InvoiceType
		{
			public const string Appointment = "Appointment";
			public const string Supply = "Supply";
			public const string ThirdParty = "Third Party";
			public const string Chargeout = "Chargeout";
		}

		public static class FormType
		{
			public const string Appointment = "Appointment";
			public const string Patient = "Patient";
		}


		public static class ChargeoutType
		{
			public const string Appointment = "Appointment";
			public const string Supply = "Supply";
			public const string ThirdParty = "Third Party";
		}

		public static class FormsStatus
        {
            public const string Sent = "Sent";
            public const string Filed = "Filed";
            public const string OK = "OK";
        }
        public static string InvoiceType_Appointment => InvoiceType.Appointment;
		public static string InvoiceType_Supply => InvoiceType.Supply;
		public static string InvoiceType_ThirdParty => InvoiceType.ThirdParty;

		public static string ChargeoutType_Appointment => ChargeoutType.Appointment;
		public static string ChargeoutType_Supply => ChargeoutType.Supply;
		public static string ChargeoutType_ThirdParty => ChargeoutType.ThirdParty;


		public static class RefundItemsType
		{
			public const string Invoice = "Invoice";
			public const string Payment = "Payment";
		}
		public static string RefundItemsType_Invoice => RefundItemsType.Invoice;
		public static string RefundItemsType_Payment => RefundItemsType.Payment;

		public static class RefchargeItemsType
		{
			public const string Chargeout = "Chargeout";
			public const string Paycharge = "Paycharge";
		}
		public static string RefchargeItemsType_Chargeout => RefchargeItemsType.Chargeout;
		public static string RefchargeItemsType_Paycharge => RefchargeItemsType.Paycharge;


		public static class ServiceProviderServiceType
		{
			public const string InHouse = "In House";
			public const string ThirdParty = "Third Party";
		}

		public static class ServiceProviderEmploymentType
		{
			public const string Service = "SERVICE";
			public const string Payg = "PAYG";
		}


		public static class SupplierRefundItemsType
		{
			public const string Order = "Order";
			public const string SupplierPayment = "SupplierPayment";
		}
		public static string SupplierRefundItemsType_Order => SupplierRefundItemsType.Order;
		public static string SupplierRefundItemsType_SupplierPayment => SupplierRefundItemsType.SupplierPayment;



	}
}
