CREATE TABLE [dbo].[ServiceProviderServices] (
    [RowId]                       UNIQUEIDENTIFIER NOT NULL,
    [ServiceProviderRowId]        UNIQUEIDENTIFIER NOT NULL,
    [MedicalServiceOrSupplyRowId] UNIQUEIDENTIFIER NOT NULL,
    [BasePrice]                   DECIMAL (18, 4)  NOT NULL,
    [BasePriceTaxRate]            DECIMAL (18, 4)  NOT NULL,
    [HourlyRate]                  DECIMAL (18, 4)  NOT NULL,
    [HourlyRateTaxRate]           DECIMAL (18, 4)  NOT NULL,
    [ChargeModel]                 NVARCHAR (50)    NULL,
    [Created]                     DATETIME         NULL,
    [CreatedBy]                   NVARCHAR (50)    NULL,
    [Updated]                     DATETIME         NULL,
    [UpdatedBy]                   NVARCHAR (50)    NULL,
    CONSTRAINT [PK_ServiceProviderServices] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_ServiceProviderServices_MedicalServicesOrSupplies] FOREIGN KEY ([MedicalServiceOrSupplyRowId]) REFERENCES [dbo].[MedicalServicesOrSupplies] ([RowId]),
    CONSTRAINT [FK_ServiceProviderServices_ServiceProviders] FOREIGN KEY ([ServiceProviderRowId]) REFERENCES [dbo].[ServiceProviders] ([RowId]),
    CONSTRAINT [UK_ServiceProviderServices] UNIQUE NONCLUSTERED ([ServiceProviderRowId] ASC, [MedicalServiceOrSupplyRowId] ASC)
);

