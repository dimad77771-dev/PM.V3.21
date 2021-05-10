CREATE TABLE [dbo].[InvoicePayments] (
    [RowId]          UNIQUEIDENTIFIER NOT NULL,
    [InvoiceRowId]   UNIQUEIDENTIFIER NOT NULL,
    [PaymentRowId]   UNIQUEIDENTIFIER NOT NULL,
    [Amount]         DECIMAL (18, 4)  NULL,
    [Tax]            DECIMAL (18, 4)  NULL,
    [AllocationDate] DATETIME         NOT NULL,
    [Created]        DATETIME         NULL,
    [CreatedBy]      NVARCHAR (50)    NULL,
    [Updated]        DATETIME         NULL,
    [UpdatedBy]      NVARCHAR (50)    NULL,
    CONSTRAINT [PK_InvoicePayments] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InvoicePayments_Invoices] FOREIGN KEY ([InvoiceRowId]) REFERENCES [dbo].[Invoices] ([RowId]),
    CONSTRAINT [FK_InvoicePayments_Payments] FOREIGN KEY ([PaymentRowId]) REFERENCES [dbo].[Payments] ([RowId])
);

