CREATE VIEW [dbo].[ServiceProvidersListView]
AS
SELECT
A.RowId,
A.Title,
A.FirstName,
A.LastName,
A.BirthDate,
A.Rate,
A.TaxRate,
A.AddressLine,
A.Province,
A.City,
A.Postcode,
A.PhoneNumber,
A.MobilePhoneNumber,
A.Qualifications,
A.WorkWeekDays,
A.EmailAddress,
A.AppointmentBookRowId,
A.AppointmentBackgroundColor,
A.AppointmentForegroundColor,
A.Position,

B.AssociationRowId,
B.RegistrationNumber,
B.RegistrationDate,
B.RegistrationExpiryDate,

C.Code as AssociationCode,
C.Name as AssociationName,
Stuff(
  (
     SELECT N', ' + IsNUll(pa.Code,'') + ' ' + IsNull(sa.RegistrationNumber, '') 
	 FROM ServiceProviderAssociations sa inner join  ProfessionalAssociations pa on (sa.AssociationRowId = pa.RowId) 
	 WHERE sa.ServiceProviderRowId = A.ROwID
	 FOR XML PATH(''),TYPE)
    .value('text()[1]','nvarchar(max)'),1,2,N''
  ) as AssosiationsList

From ServiceProviders A
outer apply (select top 1 * from ServiceProviderAssociations Q where Q.ServiceProviderRowId = A.RowId and Q.IsPrimary = 1) B
left join ProfessionalAssociations C on C.RowId = B.AssociationRowId

