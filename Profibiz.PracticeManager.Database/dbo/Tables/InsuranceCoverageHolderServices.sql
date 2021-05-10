CREATE TABLE [dbo].[InsuranceCoverageHolderServices] (
    [RowId]                         UNIQUEIDENTIFIER NOT NULL,
    [InsuranceCoverageHolderRowId]  UNIQUEIDENTIFIER NOT NULL,
    [InsuranceCoverageServiceRowId] UNIQUEIDENTIFIER NOT NULL,
    [StartDate]                     DATETIME         NULL,
    [EndDate]                       DATETIME         NULL,
    [AnnualAmountCovered]           MONEY            NULL,
    [PercentageCovered]             DECIMAL (18, 5)  NULL,
    [HourlyRateCap]                 MONEY            NULL,
    [CreatedBy]                     NVARCHAR (50)    NULL,
    [CreatedDateTime]               DATETIME         NULL,
    [UpdatedBy]                     NVARCHAR (50)    NULL,
    [UpdatedDateTime]               DATETIME         NULL,
    [IsPrescriptionRequired]        BIT              DEFAULT ((0)) NOT NULL,
    CONSTRAINT [PK_InsuranceCoverageHolderServices] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InsuranceCoverageHolderServices_InsuranceCoverageHolders] FOREIGN KEY ([InsuranceCoverageHolderRowId]) REFERENCES [dbo].[InsuranceCoverageHolders] ([RowId]),
    CONSTRAINT [FK_InsuranceCoverageHolderServices_InsuranceCoverageServices1] FOREIGN KEY ([InsuranceCoverageServiceRowId]) REFERENCES [dbo].[InsuranceCoverageServices] ([RowId])
);

