



CREATE  VIEW [dbo].[PatientsListView]
AS

SELECT 
Patients.RowId
,FirstName
,LastName
,BirthDate
,Sex
,FamilyMemberType
,FamilyHeadRowId
,(
SELECT DISTINCT STUFF(REPLACE((SELECT '#!' + LTRIM(RTRIM(ips.Code)) AS 'data()' 
From (
Select Distinct ip.Code
 from InsuranceCoverageHolders ich 
      inner join InsuranceCoverages ic on (ich.InsuranceCoverageRowId = ic.RowId) 
	  inner join InsuranceProviders ip on (ip.RowId = ic.InsuranceProviderRowId)
Where ich.PolicyHolderRowId = Patients.RowId
and ich.PolicyHolderType = 'Policy Owner'
) ips
FOR XML PATH('')),' #!','/'), 1, 2, '') 
) as PrimaryPolicies

,(
SELECT DISTINCT STUFF(REPLACE((SELECT '#!' + LTRIM(RTRIM(ips.Code)) AS 'data()' 
From (
Select Distinct ip.Code
 from InsuranceCoverageHolders ich 
      inner join InsuranceCoverages ic on (ich.InsuranceCoverageRowId = ic.RowId) 
	  inner join InsuranceProviders ip on (ip.RowId = ic.InsuranceProviderRowId)
Where ich.PolicyHolderRowId = Patients.RowId
and ich.PolicyHolderType = 'Policy Beneficiary'
) ips
FOR XML PATH('')),' #!','/'), 1, 2, '') 
) as SecondaryPolicies
,EmailAddress
,HomePhoneNumber
,MobileNumber
,Referrers.Name as ReferrerName
,Photo
From Patients left outer join Referrers on (Patients.ReferrerRowId = Referrers.RowId)
Where IsDeleted = 0



