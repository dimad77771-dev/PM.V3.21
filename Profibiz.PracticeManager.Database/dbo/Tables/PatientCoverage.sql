CREATE TABLE [dbo].[PatientCoverage] (
    [RowId]                       UNIQUEIDENTIFIER CONSTRAINT [DF_PatientCoverage_RowId] DEFAULT (newid()) NOT NULL,
    [PatientRowId]                UNIQUEIDENTIFIER NOT NULL,
    [MedicalServiceOrSupplyRowId] UNIQUEIDENTIFIER NOT NULL,
    [InsuranceProviderRowId]      UNIQUEIDENTIFIER NOT NULL,
    [CoverageStartDate]           DATETIME         NULL,
    [CoverageValidUntil]          DATETIME         NULL,
    [AnnualAmountCovered]         MONEY            NULL,
    [PercentageCovered]           MONEY            NULL,
    [HourlyRateCap]               MONEY            NULL,
    [CreatedBy]                   NVARCHAR (255)   NULL,
    [CreatedDateTime]             DATETIME         NULL,
    [UpdatedBy]                   NVARCHAR (255)   NULL,
    [UpdatedDateTime]             DATETIME         NULL,
    CONSTRAINT [PK_PatientCoverage] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_PatientCoverage_InsuranceProviders] FOREIGN KEY ([InsuranceProviderRowId]) REFERENCES [dbo].[InsuranceProviders] ([RowId]),
    CONSTRAINT [FK_PatientCoverage_MedicalServicesAndSupplies] FOREIGN KEY ([MedicalServiceOrSupplyRowId]) REFERENCES [dbo].[MedicalServicesOrSupplies] ([RowId]),
    CONSTRAINT [FK_PatientCoverage_Patients] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId])
);

