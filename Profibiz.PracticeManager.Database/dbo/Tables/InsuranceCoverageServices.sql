CREATE TABLE [dbo].[InsuranceCoverageServices] (
    [RowId]                  UNIQUEIDENTIFIER NOT NULL,
    [InsuranceCoverageRowId] UNIQUEIDENTIFIER NOT NULL,
    [CoversAllHolders]       BIT              NOT NULL,
    [StartDate]              DATETIME         NULL,
    [EndDate]                DATETIME         NULL,
    [AnnualAmountCovered]    MONEY            NULL,
    [PercentageCovered]      DECIMAL (18, 5)  NULL,
    [HourlyRateCap]          MONEY            NULL,
    [CreatedBy]              NVARCHAR (50)    NULL,
    [CreatedDateTime]        DATETIME         NULL,
    [UpdatedBy]              NVARCHAR (50)    NULL,
    [UpdatedDateTime]        DATETIME         NULL,
    [IsPrescriptionRequired] BIT              DEFAULT ((0)) NOT NULL,
    [CategoryRowId]          UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_PatientCoverageDetails] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InsuranceCoverageServices_Categories] FOREIGN KEY ([CategoryRowId]) REFERENCES [dbo].[Categories] ([RowId]),
    CONSTRAINT [FK_InsuranceCoverageServices_InsuranceCoverages] FOREIGN KEY ([InsuranceCoverageRowId]) REFERENCES [dbo].[InsuranceCoverages] ([RowId])
);

