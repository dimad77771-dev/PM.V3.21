
CREATE view [dbo].[PatientInsuranceProvidersViewGroupView] as
select
A.*,
B.InsuranceProvidersViewGroupRowId 
from PatientInsuranceCoverageView A
join InsuranceProvidersViewGroupMappings B on B.InsuranceProviderRowId = A.InsuranceProviderRowId
