CREATE TABLE [dbo].[Invoices] (
    [RowId]                          UNIQUEIDENTIFIER NOT NULL,
    [PatientRowId]                   UNIQUEIDENTIFIER NOT NULL,
    [InvoiceType]                    NVARCHAR (255)   NULL,
    [InvoiceNumber]                  NVARCHAR (50)    NULL,
    [InvoiceDate]                    DATETIME         NULL,
    [BillTo]                         NVARCHAR (255)   NULL,
    [BillToAddress1]                 NVARCHAR (255)   NULL,
    [BillToAddress2]                 NVARCHAR (255)   NULL,
    [BillToCity]                     NVARCHAR (255)   NULL,
    [BillToProvince]                 NVARCHAR (255)   NULL,
    [BillToPostCode]                 NVARCHAR (255)   NULL,
    [Created]                        DATETIME         NULL,
    [CreatedBy]                      NVARCHAR (50)    NULL,
    [Updated]                        DATETIME         NULL,
    [UpdateBy]                       NVARCHAR (50)    NULL,
    [PrintTemplate]                  VARCHAR (255)    NULL,
    [ThirdPartyServiceProviderRowId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_Invoices_Patients] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId]),
    CONSTRAINT [FK_Invoices_Tpsp] FOREIGN KEY ([ThirdPartyServiceProviderRowId]) REFERENCES [dbo].[ThirdPartyServiceProviders] ([RowId])
);

