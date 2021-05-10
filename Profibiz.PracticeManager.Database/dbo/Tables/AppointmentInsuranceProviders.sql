CREATE TABLE [dbo].[AppointmentInsuranceProviders] (
    [RowId]                  UNIQUEIDENTIFIER NOT NULL,
    [AppointmentRowId]       UNIQUEIDENTIFIER NOT NULL,
    [InsuranceProviderRowId] UNIQUEIDENTIFIER NOT NULL,
    [Percentage]             DECIMAL (18, 2)  NOT NULL,
    CONSTRAINT [PK_AppointmentInsuranceProviders] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_AppointmentInsuranceProviders_Appointments] FOREIGN KEY ([AppointmentRowId]) REFERENCES [dbo].[Appointments] ([RowId]),
    CONSTRAINT [FK_AppointmentInsuranceProviders_InsuranceProviders] FOREIGN KEY ([InsuranceProviderRowId]) REFERENCES [dbo].[InsuranceProviders] ([RowId])
);

