

CREATE view [dbo].[PaymentsView] as
select
*,
isnull((select sum(Q.Amount) from InvoicePayments Q where Q.PaymentRowId = A.RowId),0) AmountInInvoices,
(select Q.FullName from PatientsView Q where Q.RowId = A.PatientRowId) as PatientFullName
from Payments A

