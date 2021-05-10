CREATE TABLE [dbo].[InsuranceCoverages] (
    [RowId]                     UNIQUEIDENTIFIER NOT NULL,
    [InsuranceProviderRowId]    UNIQUEIDENTIFIER NOT NULL,
    [PolicyNumber]              NVARCHAR (255)   NOT NULL,
    [CoverageStartDate]         DATETIME         NULL,
    [CoverageValidUntil]        DATETIME         NULL,
    [IsForAllListed]            BIT              CONSTRAINT [DF_InsuranceCoverages_IsForAllListed] DEFAULT ((1)) NULL,
    [CreatedBy]                 NVARCHAR (255)   NULL,
    [CreateDateTime]            DATETIME         NULL,
    [UpdatedBy]                 NVARCHAR (255)   NULL,
    [UpdatedDateTime]           DATETIME         NULL,
    [PlanNumber]                NVARCHAR (255)   NULL,
    [DivisionNumber]            NVARCHAR (255)   NULL,
    [ID]                        NVARCHAR (255)   NULL,
    [InsuranceCoverageYearType] NVARCHAR (50)    NULL,
    CONSTRAINT [PK_InsuranceCoverages] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InsuranceCoverages_InsuranceProviders] FOREIGN KEY ([InsuranceProviderRowId]) REFERENCES [dbo].[InsuranceProviders] ([RowId])
);

