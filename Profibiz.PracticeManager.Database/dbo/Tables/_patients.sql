CREATE TABLE [dbo].[_patients] (
    [RowId]                      NVARCHAR (255) NULL,
    [Title]                      NVARCHAR (255) NULL,
    [FirstName]                  NVARCHAR (255) NULL,
    [MiddleName]                 NVARCHAR (255) NULL,
    [LastName]                   NVARCHAR (255) NULL,
    [PreferredName]              NVARCHAR (255) NULL,
    [FamilyHeadRowId]            NVARCHAR (255) NULL,
    [FamilyMemberType]           NVARCHAR (255) NULL,
    [RelationToFamilyHead]       NVARCHAR (255) NULL,
    [BirthDate]                  DATETIME       NULL,
    [Sex]                        NVARCHAR (255) NULL,
    [CardNo]                     NVARCHAR (255) NULL,
    [FirstSeen]                  NVARCHAR (255) NULL,
    [SendInvoicesToFamilyMember] NVARCHAR (255) NULL,
    [Address1]                   NVARCHAR (255) NULL,
    [Province1]                  NVARCHAR (255) NULL,
    [City1]                      NVARCHAR (255) NULL,
    [Postcode1]                  NVARCHAR (255) NULL,
    [Address2]                   NVARCHAR (255) NULL,
    [Province2]                  NVARCHAR (255) NULL,
    [City2]                      NVARCHAR (255) NULL,
    [Postcode2]                  NVARCHAR (255) NULL,
    [AddressToUse]               NVARCHAR (255) NULL,
    [HomePhoneNumber]            NVARCHAR (255) NULL,
    [MobileNumber]               NVARCHAR (255) NULL,
    [Occupation]                 NVARCHAR (255) NULL,
    [EmployerName]               NVARCHAR (255) NULL,
    [WorkPhone]                  NVARCHAR (255) NULL,
    [Fax]                        NVARCHAR (255) NULL,
    [EmailAddress]               NVARCHAR (255) NULL,
    [FamilyDoctor]               NVARCHAR (255) NULL,
    [FamilyDoctorAddress]        NVARCHAR (255) NULL,
    [FamilyDoctorPhoneNumber]    NVARCHAR (255) NULL,
    [IsDeleted]                  FLOAT (53)     NULL,
    [CreatedBy]                  NVARCHAR (255) NULL,
    [CreatedDateTime]            DATETIME       NULL,
    [UpdatedBy]                  NVARCHAR (255) NULL,
    [UpdatedDateTime]            DATETIME       NULL,
    [HasHighBloodPressure]       FLOAT (53)     NULL,
    [HasPacemaker]               FLOAT (53)     NULL,
    [HasDiabetes]                FLOAT (53)     NULL,
    [HasHepatitis]               FLOAT (53)     NULL,
    [HasHeadaches]               FLOAT (53)     NULL,
    [HasSurgeries]               FLOAT (53)     NULL,
    [HasMetalImplants]           FLOAT (53)     NULL,
    [HasFractures]               FLOAT (53)     NULL,
    [HasNeckPain]                FLOAT (53)     NULL,
    [HasBackPain]                FLOAT (53)     NULL,
    [HasShoulderElbowHandPain]   FLOAT (53)     NULL,
    [HasHipKneeFootPain]         FLOAT (53)     NULL,
    [OtherMedicalConditions]     NVARCHAR (255) NULL,
    [HealthHistoryNotes]         NVARCHAR (255) NULL
);

