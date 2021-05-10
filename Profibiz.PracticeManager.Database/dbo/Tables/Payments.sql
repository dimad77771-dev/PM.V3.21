CREATE TABLE [dbo].[Payments] (
    [RowId]           UNIQUEIDENTIFIER NOT NULL,
    [PatientRowId]    UNIQUEIDENTIFIER NOT NULL,
    [Amount]          DECIMAL (18, 4)  NULL,
    [PaymentDate]     DATETIME         NULL,
    [Notes]           NVARCHAR (MAX)   NULL,
    [Created]         DATETIME         NULL,
    [CreatedBy]       NVARCHAR (50)    NULL,
    [Updated]         DATETIME         NULL,
    [UpdatedBy]       NVARCHAR (50)    NULL,
    [PaymentType]     NVARCHAR (50)    NULL,
    [BankName]        NVARCHAR (255)   NULL,
    [ChequeNumber]    NVARCHAR (255)   NULL,
    [BrunchNumber]    NVARCHAR (255)   NULL,
    [TransitNumber]   NVARCHAR (255)   NULL,
    [AccountNumber]   NVARCHAR (255)   NULL,
    [Image]           VARBINARY (MAX)  NULL,
    [TransactionId]   NVARCHAR (255)   NULL,
    [FullDescription] AS               ([dbo].[f_Payment_FullDescription]([Amount],[PaymentType],[BankName],[ChequeNumber],[BrunchNumber],[TransitNumber],[AccountNumber],[TransactionId],[Notes])),
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_Payments_Patients] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId])
);

