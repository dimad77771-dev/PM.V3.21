CREATE TABLE [dbo].[PatientMedicalConditions] (
    [RowId]                 UNIQUEIDENTIFIER NOT NULL,
    [MedicalConditionRowId] UNIQUEIDENTIFIER NOT NULL,
    [PatientRowId]          UNIQUEIDENTIFIER NOT NULL,
    [Value]                 BIT              NULL,
    [Note]                  NVARCHAR (255)   NULL,
    CONSTRAINT [PK_PatientMedicalConditions] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_PatientMedicalConditions_MedicalConditions] FOREIGN KEY ([MedicalConditionRowId]) REFERENCES [dbo].[MedicalConditions] ([RowId]),
    CONSTRAINT [FK_PatientMedicalConditions_Patients] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId])
);

