
CREATE view [dbo].[PatientFamilyMemberView] as 
select 
A.RowId PatientRowId,
B.RowId FamilyMemberPatientRowId,
A.FamilyHeadRowId
from Patients A
join Patients B on B.FamilyHeadRowId = A.FamilyHeadRowId and B.RowId <> A.RowId
