

CREATE VIEW [dbo].[InvoicesView]
AS

select
T.*,
T.Amount + T.Tax as Total,
T.PaymentAmount + T.PaymentTax as PaymentTotal,
(T.Amount + T.Tax) - (T.PaymentAmount + T.PaymentTax) as PaymentRequest,
(select Q.FullName from PatientsView Q where Q.RowId = T.PatientRowId) as PatientFullName
from
(
	SELECT 
	*,
	(select isnull(sum(Price * Units),0) from InvoiceItems Q where Q.InvoiceRowId = A.RowId) as Amount,
	(select isnull(sum(Tax * Units),0) from InvoiceItems Q where Q.InvoiceRowId = A.RowId) as Tax,
	(select isnull(sum(Amount),0) from InvoicePayments Q where Q.InvoiceRowId = A.RowId) as PaymentAmount,
	(select isnull(sum(Tax),0) from InvoicePayments Q where Q.InvoiceRowId = A.RowId) as PaymentTax
	From Invoices A
) as T


