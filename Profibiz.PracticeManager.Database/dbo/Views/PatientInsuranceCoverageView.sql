
CREATE view [dbo].[PatientInsuranceCoverageView] as
select
	A.RowId as PatientRowId,
	B.InsuranceCoverageRowId,
	C.InsuranceProviderRowId,
	C.PolicyNumber,
	C.CoverageStartDate,
	C.CoverageValidUntil
from Patients A
join InsuranceCoverageHolders B on B.PolicyHolderRowId = A.RowId
join InsuranceCoverages C on C.RowId = B.InsuranceCoverageRowId

--PatientInsuranceCoverageView

--select * from InsuranceCoverageHolders B
