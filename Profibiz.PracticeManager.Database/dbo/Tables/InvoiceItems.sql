CREATE TABLE [dbo].[InvoiceItems] (
    [RowId]                UNIQUEIDENTIFIER NOT NULL,
    [InvoiceRowId]         UNIQUEIDENTIFIER NOT NULL,
    [AppointmentRowId]     UNIQUEIDENTIFIER NULL,
    [ServcieOrSupplyRowId] UNIQUEIDENTIFIER NULL,
    [Units]                DECIMAL (18, 4)  NULL,
    [Price]                DECIMAL (18, 4)  NULL,
    [Tax]                  DECIMAL (18, 4)  NULL,
    [Description]          NVARCHAR (MAX)   NULL,
    [Created]              DATETIME         NULL,
    [CreatedBy]            NVARCHAR (50)    NULL,
    [Updated]              DATETIME         NULL,
    [UpdatedBy]            NVARCHAR (50)    NULL,
    [ItemDate]             DATETIME         NULL,
    CONSTRAINT [PK_InvoiceItems] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_InvoiceItems_Appointments] FOREIGN KEY ([AppointmentRowId]) REFERENCES [dbo].[Appointments] ([RowId]),
    CONSTRAINT [FK_InvoiceItems_Invoices] FOREIGN KEY ([InvoiceRowId]) REFERENCES [dbo].[Invoices] ([RowId]),
    CONSTRAINT [FK_InvoiceItems_MedicalServicesOrSupplies] FOREIGN KEY ([ServcieOrSupplyRowId]) REFERENCES [dbo].[MedicalServicesOrSupplies] ([RowId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [indx_InvoiceItems_AppointmentRowId]
    ON [dbo].[InvoiceItems]([AppointmentRowId] ASC) WHERE ([AppointmentRowId] IS NOT NULL);

