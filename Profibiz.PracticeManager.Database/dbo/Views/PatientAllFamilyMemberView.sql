CREATE view [dbo].[PatientAllFamilyMemberView] as 
select 
A.RowId PatientRowId,
B.RowId FamilyMemberPatientRowId,
A.FamilyHeadRowId
from Patients A
join Patients B on B.FamilyHeadRowId = A.FamilyHeadRowId 

