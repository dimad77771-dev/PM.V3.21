using System;
using System.Configuration;
using System.IO;

namespace Profibiz.PracticeManager.EF
{
	public interface IEntityCreatedUpdated
	{
		Guid? CreatedByUserRowId { get; set; }
		Guid? UpdatedByUserRowId { get; set; }
		DateTime? CreatedByDateTime { get; set; }
		DateTime? UpdatedByDateTime { get; set; }
	}

	public partial class AppointmentBook : IEntityCreatedUpdated { }
	public partial class AppointmentClinicalNote : IEntityCreatedUpdated { }
	public partial class AppointmentInsuranceProvider : IEntityCreatedUpdated { }
	public partial class AppointmentRemainder : IEntityCreatedUpdated { }
	public partial class AppointmentStatus : IEntityCreatedUpdated { }
	public partial class AppointmentT : IEntityCreatedUpdated { }
	public partial class AppointmentTreatmentNote : IEntityCreatedUpdated { }
	public partial class CalendarEventStatus : IEntityCreatedUpdated { }
	public partial class CalendarEventT : IEntityCreatedUpdated { }
	public partial class Category : IEntityCreatedUpdated { }
	public partial class ChargeoutItem : IEntityCreatedUpdated { }
	public partial class ChargeoutPaycharge : IEntityCreatedUpdated { }
	public partial class ChargeoutRecipient : IEntityCreatedUpdated { }
	public partial class ChargeoutRefchargeT : IEntityCreatedUpdated { }
	public partial class ChargeoutStatus : IEntityCreatedUpdated { }
	public partial class ChargeoutT : IEntityCreatedUpdated { }
	public partial class City : IEntityCreatedUpdated { }
	public partial class ClientError : IEntityCreatedUpdated { }
	public partial class EmailChargeAttachment : IEntityCreatedUpdated { }
	public partial class EmailChargeRecipient : IEntityCreatedUpdated { }
	public partial class EmailChargeT : IEntityCreatedUpdated { }
	public partial class EmailSendAttachment : IEntityCreatedUpdated { }
	public partial class EmailSendRecipient : IEntityCreatedUpdated { }
	public partial class EmailSendT : IEntityCreatedUpdated { }
	public partial class FormDocument : IEntityCreatedUpdated { }
	public partial class InsuranceCoverage : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageHolder : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageHolderService : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageItem : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageItemCategory : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageItemHolder : IEntityCreatedUpdated { }
	public partial class InsuranceCoverageService : IEntityCreatedUpdated { }
	public partial class InsuranceProvider : IEntityCreatedUpdated { }
	public partial class InsuranceProvidersViewGroup : IEntityCreatedUpdated { }
	public partial class InsuranceProvidersViewGroupMapping : IEntityCreatedUpdated { }
	public partial class InventoryT : IEntityCreatedUpdated { }
	public partial class InvoiceClaimDetail : IEntityCreatedUpdated { }
	public partial class InvoiceClaimStatus : IEntityCreatedUpdated { }
	public partial class InvoiceClaimT : IEntityCreatedUpdated { }
	public partial class InvoiceItem : IEntityCreatedUpdated { }
	public partial class InvoicePayment : IEntityCreatedUpdated { }
	public partial class InvoiceRefundT : IEntityCreatedUpdated { }
	public partial class InvoiceStatus : IEntityCreatedUpdated { }
	public partial class InvoiceT : IEntityCreatedUpdated { }
	public partial class MedicalCondition : IEntityCreatedUpdated { }
	public partial class MedicalHistoryRecordT : IEntityCreatedUpdated { }
	public partial class MedicalServicesOrSupply : IEntityCreatedUpdated { }
	public partial class OrderItemT : IEntityCreatedUpdated { }
	public partial class OrderPayment : IEntityCreatedUpdated { }
	public partial class OrderT : IEntityCreatedUpdated { }
	public partial class Patient : IEntityCreatedUpdated { }
	public partial class PatientDocument : IEntityCreatedUpdated { }
	public partial class PatientMedicalCondition : IEntityCreatedUpdated { }
	public partial class PatientNote : IEntityCreatedUpdated { }
	public partial class PatientNoteStatus : IEntityCreatedUpdated { }
	public partial class PatientPhoto : IEntityCreatedUpdated { }
	public partial class PaychargeRefchargeT : IEntityCreatedUpdated { }
	public partial class PaychargeT : IEntityCreatedUpdated { }
	public partial class PaymentRefundT : IEntityCreatedUpdated { }
	public partial class PaymentT : IEntityCreatedUpdated { }
	public partial class PayrollPaymentAllocationT : IEntityCreatedUpdated { }
	public partial class PayrollPaymentT : IEntityCreatedUpdated { }
	public partial class PrintDocument : IEntityCreatedUpdated { }
	public partial class ProfessionalAssociation : IEntityCreatedUpdated { }
	public partial class PublicHoliday : IEntityCreatedUpdated { }
	public partial class RefchargeT : IEntityCreatedUpdated { }
	public partial class Referrer : IEntityCreatedUpdated { }
	public partial class RefundT : IEntityCreatedUpdated { }
	public partial class SchedulerRecord : IEntityCreatedUpdated { }
	public partial class SchedulerRecordItem : IEntityCreatedUpdated { }
	public partial class ServiceProvider : IEntityCreatedUpdated { }
	public partial class ServiceProviderAssociation : IEntityCreatedUpdated { }
	public partial class ServiceProviderService : IEntityCreatedUpdated { }
	public partial class Supplier : IEntityCreatedUpdated { }
	public partial class SupplierPaymentRefundT : IEntityCreatedUpdated { }
	public partial class SupplierPaymentT : IEntityCreatedUpdated { }
	public partial class SupplierRefundT : IEntityCreatedUpdated { }
	public partial class Template : IEntityCreatedUpdated { }
	public partial class ThirdPartyServiceProvider : IEntityCreatedUpdated { }
	public partial class TreatmentPlanRecordT : IEntityCreatedUpdated { }
	public partial class User : IEntityCreatedUpdated { }
	public partial class UserSetting : IEntityCreatedUpdated { }
	public partial class WorkInout : IEntityCreatedUpdated { }
	public partial class AppointmentForm : IEntityCreatedUpdated { }
	public partial class AppointmentFormItem : IEntityCreatedUpdated { }
	public partial class PatientForm : IEntityCreatedUpdated { }
	public partial class PatientFormItem : IEntityCreatedUpdated { }
	public partial class Form : IEntityCreatedUpdated { }
	public partial class FormItem : IEntityCreatedUpdated { }


}