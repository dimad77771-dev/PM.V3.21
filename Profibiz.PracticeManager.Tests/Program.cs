using Profibiz.PracticeManager.BL;
using Profibiz.PracticeManager.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profibiz.PracticeManager.Tests
{
	class Program
	{
		static void Main(string[] args)
		{
			//var repository = new WebApiRepository();
			////var arr = repository.GetAppointmentBooks();
			////var arr = repository.GetAppointmentList(null, null, null, null, null, null, null, null, true, null);
			////var arr = repository.GetPaycharge(new Guid("EEB6BA09-FD5C-4F57-8C08-D462B87F356B"));
			//var arr = repository.GetPaycharge(new Guid("EEB6BA09-FD5C-4F57-8C08-D462B87F356B"));

			//var e = 1000;

			//var patientRowIds = new[] { "0D924587-7FB1-4B9E-9566-018DB0C70FA3", "F391F4C4-1C27-417A-A236-05DA97334F18", "E58437AE-4F41-4785-A13B-0D65B27008CA", "3463FEB4-E275-4E45-850A-212DE253C85A", "0B707BC2-A941-4E89-B477-22658BE293FC", "CC5A4889-7D11-431F-AD03-22C69C1B5093", "310D1BE7-41BC-4A77-AA8F-2C5831C59637", "A6260864-D670-4702-B83F-302C29394164", "B464207C-0F19-461D-A63E-3455BC70D6BC", "D30F417C-2929-4828-997B-433869226C07", "D133C107-C2F5-41CE-9F93-514204B0054A", "DC8EE246-7EF3-45FB-A05B-51A676D7A216", "5BFD9580-C653-4C99-9BF4-56D6BC438867", "3FB72DDB-F1C1-4A48-92E9-5EF9E9BBBB66", "3BE3CAD5-5C87-4FAA-BAEC-61D239BB17DB", "47434583-857E-4EE5-A69D-65E6643D8CB3", "9ED7F340-B8DA-4C47-B127-662B6D2B521B", "309F912B-ABC2-4C79-84E9-67A092C3CA5B", "0F8BB390-8E39-4483-8226-6B737D66D2EB", "9B3A9373-4D2D-4D1D-BDC2-7348A38E1364", "F975E53E-E337-4FDD-B5B4-76BB071B46F4", "FEC0C553-91E6-454E-BF1D-78A85B8B1FED", "6E6EEB81-4234-43DD-BC50-82CAFE90B575", "5F5A08F0-FF38-4BEB-93B1-85A98FAAD3D4", "C3568D73-610F-4689-964A-86B4B2531C3F", "956A5C49-A621-4420-BB36-88178A97DA6A", "D2BCCEF6-5666-4E15-9403-8AC261D0F2E0", "0059E3DE-5353-4697-8DFC-9673F6781723", "617FA198-F268-4432-8F1E-989B8052DFFD", "3B6D0C07-95FB-449E-AEC3-9CF5E6BB5671", "3489D880-AF15-45AE-9477-9E310D78EBCD", "35B7CF82-2041-4537-938C-A141BA057ECF", "D98391E4-A802-44A4-8A42-A3EA027C3C2A", "4127636E-5F75-43FA-9EBF-A71B886CEA50", "1EFA7DFB-BAEE-4285-85BA-AEBD32EF6A1B", "46529B70-E70A-4C03-AC80-B3728F038457", "F754028E-F38A-46DB-BFEF-BFE6CE2742EA", "E7175057-5563-40FE-B5B4-D2D03EB76326", "125E62DA-884F-45AB-B3EA-DD5E2E7E510B", "5FB5DCF4-515D-465D-BF20-E39C5B5ADA67", "6202A8C7-F98C-4F0D-81C2-E63C88B28A50", "216A11E9-4822-4652-8443-E680E46DEA92", "C84B8600-A7EC-476A-BC4C-EB6663B15156", "510A510D-C0A0-4B84-8AC9-F0CF7D135128", "BB8B88CC-F8A8-4A3E-887E-F527527F4B53", "D26F6F8D-3415-45B0-829C-F5A6DDE5DED3", "B4837B64-7F30-454F-8511-FFFCC81429BB" }
			//	.Select(q => new Guid(q))
			//	.ToArray();
			var patientRowIds = new Guid[0];	//ничего не оставляем

			var db = new PracticeManagerEntities();

			var transaction = db.Database.BeginTransaction();
			var patients = db.Patients.Where(q => !patientRowIds.Contains(q.RowId));
			var invoices = patients.SelectMany(q => q.Invoices);

			var invoiceClaims = db.InvoiceClaimsT.Where(q => invoices.Select(z => z.RowId).Contains(q.InvoiceRowId));
			var invoiceClaimDetails = invoiceClaims.SelectMany(q => q.InvoiceClaimDetails);
			var invoiceItems = invoices.SelectMany(q => q.InvoiceItems);
			var inventories = invoiceItems.SelectMany(q => q.Inventories);
			var invoicePayments = db.InvoicePayments.Where(q => invoices.Any(z => z.RowId == q.InvoiceRowId));
			var emailSends = db.EmailSendsT.Where(q => invoices.Any(z => z.RowId == q.InvoiceRowId));
			var emailSendAttachments = emailSends.SelectMany(q => q.EmailSendAttachments);
			var emailSendRecipients = emailSends.SelectMany(q => q.EmailSendRecipients);

			var invoiceRefunds = db.InvoiceRefundsT.Where(q => invoices.Any(z => z.RowId == q.InvoiceRowId));

			var appointments = patients.SelectMany(q => q.Appointments);


			var invoiceItems2 = appointments.SelectMany(q => q.InvoiceItems);
			var inventories2 = invoiceItems2.SelectMany(q => q.Inventories);

			var insuranceCoverageHolders = db.InsuranceCoverageHolders.Where(q => patients.Any(z => z.RowId == q.PolicyHolderRowId));
			var insuranceCoverageItemHolders = insuranceCoverageHolders.SelectMany(q => q.InsuranceCoverageItemHolders);
			var insuranceCoverageHolderServices = insuranceCoverageHolders.SelectMany(q => q.InsuranceCoverageHolderServices);

			var payments = db.PaymentsT.Where(q => patients.Any(z => z.RowId == q.PatientRowId));
			var invoicePayments2 = db.InvoicePayments.Where(q => payments.Any(z => z.RowId == q.PaymentRowId));
			var paymentRefunds = db.PaymentRefundsT.Where(q => payments.Any(z => z.RowId == q.PaymentRowId));

			var calendarEvents = patients.SelectMany(q => q.CalendarEvents);

			var refunds = db.RefundsT.Where(q => patients.Any(z => z.RowId == q.PatientRowId));
			var paymentRefunds2 = refunds.SelectMany(q => q.PaymentRefunds);
			var invoiceRefunds2 = refunds.SelectMany(q => q.InvoiceRefunds);

			var patientNotes = patients.SelectMany(q => q.PatientNotes);

			var medicalHistoryRecords = patients.SelectMany(q => q.MedicalHistoryRecords);
			var treatmentPlanRecords = patients.SelectMany(q => q.TreatmentPlanRecords);
			var patientDocuments = patients.SelectMany(q => q.PatientDocuments);

			//db.send
			//invoices.SelectMany(q => q.maa)
			//invoices.SelectMany(q => q.Inv)


			//db.AppointmentClinicalNotes.Where(q => q.)
			//invoices.Select(q => q.

			patientDocuments.DeleteFromQuery();
			treatmentPlanRecords.DeleteFromQuery();
			medicalHistoryRecords.DeleteFromQuery();
			patientNotes.DeleteFromQuery();

			invoiceRefunds2.DeleteFromQuery();
			paymentRefunds2.DeleteFromQuery();
			refunds.DeleteFromQuery();

			calendarEvents.DeleteFromQuery();

			paymentRefunds.DeleteFromQuery();
			invoicePayments2.DeleteFromQuery();
			payments.DeleteFromQuery();

			insuranceCoverageHolderServices.DeleteFromQuery();
			insuranceCoverageItemHolders.DeleteFromQuery();
			insuranceCoverageHolders.DeleteFromQuery();

			inventories2.DeleteFromQuery();
			invoiceItems2.DeleteFromQuery();
			appointments.DeleteFromQuery();

			invoiceRefunds.DeleteFromQuery();

			emailSendAttachments.DeleteFromQuery();
			emailSendRecipients.DeleteFromQuery();
			emailSends.DeleteFromQuery();
			invoicePayments.DeleteFromQuery();

			inventories.DeleteFromQuery();
			invoiceItems.DeleteFromQuery();

			invoiceClaimDetails.DeleteFromQuery();
			invoiceClaims.DeleteFromQuery();
			invoices.DeleteFromQuery();

			patients.DeleteFromQuery();

			//transaction.Rollback();
			transaction.Commit();


			//var gg = patients.ToArray();
		}
	}
}

