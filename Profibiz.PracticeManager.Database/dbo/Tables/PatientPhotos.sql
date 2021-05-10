CREATE TABLE [dbo].[PatientPhotos] (
    [RowId]        UNIQUEIDENTIFIER CONSTRAINT [DF_PatientPhotos_RowId] DEFAULT (newid()) NOT NULL,
    [PatientRowId] UNIQUEIDENTIFIER NOT NULL,
    [Photo]        VARBINARY (MAX)  NULL,
    CONSTRAINT [PK_PatientPhotos_RowId] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK__PatientPh__Patie__0A688BB1] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId]),
    UNIQUE NONCLUSTERED ([PatientRowId] ASC)
);

