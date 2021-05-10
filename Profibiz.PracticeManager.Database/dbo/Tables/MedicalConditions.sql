CREATE TABLE [dbo].[MedicalConditions] (
    [RowId] UNIQUEIDENTIFIER NOT NULL,
    [Code]  NVARCHAR (255)   NOT NULL,
    [Name]  NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_MedicalConditions] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

