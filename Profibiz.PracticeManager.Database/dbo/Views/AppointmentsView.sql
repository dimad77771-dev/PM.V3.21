
CREATE VIEW [dbo].[AppointmentsView]
AS
select
T.*,
(select Q.FullName from PatientsView Q where Q.RowId = T.PatientRowId) as PatientFullName,
B.InvoiceRowId as InvoiceRowId,
C.InvoiceNumber as InvoiceNumber
--case when exists (select 1 from InvoiceItems Q where Q.AppointmentRowId = T.RowId) then cast(1 as bit) else cast(0 as bit) end as InInvoice
From Appointments T
left join InvoiceItems B on B.AppointmentRowId = T.RowId
left join Invoices C on C.RowId = B.InvoiceRowId

