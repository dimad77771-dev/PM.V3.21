CREATE  VIEW [dbo].[PatientsView] AS
SELECT 
*,
LastName + (CASE WHEN FirstName is null or FirstName = '' THEN '' ELSE ', ' END) + FirstName as FullName
From Patients
