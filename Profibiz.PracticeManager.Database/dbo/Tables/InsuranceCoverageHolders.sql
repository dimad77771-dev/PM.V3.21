CREATE TABLE [dbo].[InsuranceCoverageHolders] (
    [RowId]                  UNIQUEIDENTIFIER NOT NULL,
    [InsuranceCoverageRowId] UNIQUEIDENTIFIER NOT NULL,
    [PolicyHolderRowId]      UNIQUEIDENTIFIER NOT NULL,
    [PolicyHolderType]       NVARCHAR (50)    NOT NULL,
    CONSTRAINT [PK_PatientCoverages] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InsuranceCoverageHolders_InsuranceCoverages] FOREIGN KEY ([InsuranceCoverageRowId]) REFERENCES [dbo].[InsuranceCoverages] ([RowId]),
    CONSTRAINT [FK_InsuranceCoverageHolders_Patients] FOREIGN KEY ([PolicyHolderRowId]) REFERENCES [dbo].[Patients] ([RowId])
);

