﻿CREATE TABLE [dbo].[Appointments] (
    [RowId]                        UNIQUEIDENTIFIER NOT NULL,
    [Start]                        DATETIME         NOT NULL,
    [Finish]                       DATETIME         NOT NULL,
    [Notes]                        NVARCHAR (MAX)   NULL,
    [PatientRowId]                 UNIQUEIDENTIFIER NOT NULL,
    [Description]                  NVARCHAR (255)   NULL,
    [Type]                         NVARCHAR (50)    NULL,
    [RefNumber]                    NVARCHAR (50)    NULL,
    [RefStatus]                    NVARCHAR (50)    NULL,
    [AppointmentBookRowId]         UNIQUEIDENTIFIER NOT NULL,
    [CreatedBy]                    NVARCHAR (255)   NULL,
    [CreateDateTime]               DATETIME         NULL,
    [UpdatedBy]                    NVARCHAR (255)   NULL,
    [UpdatedDateTime]              DATETIME         NULL,
    [ServiceProviderRowId]         UNIQUEIDENTIFIER NULL,
    [Completed]                    BIT              CONSTRAINT [DF_Appointments_Completed] DEFAULT ((0)) NOT NULL,
    [MedicalServicesOrSupplyRowId] UNIQUEIDENTIFIER NULL,
    [InsuranceProviderRowId]       UNIQUEIDENTIFIER NULL,
    [Status1RowId]                 UNIQUEIDENTIFIER NULL,
    [Status2RowId]                 UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK__Appointme__Medic__0D0FEE32] FOREIGN KEY ([MedicalServicesOrSupplyRowId]) REFERENCES [dbo].[MedicalServicesOrSupplies] ([RowId]),
    CONSTRAINT [FK_Appointments_AppointmentBooks] FOREIGN KEY ([AppointmentBookRowId]) REFERENCES [dbo].[AppointmentBooks] ([RowId]),
    CONSTRAINT [FK_Appointments_AppointmentStatuses_1] FOREIGN KEY ([Status1RowId]) REFERENCES [dbo].[AppointmentStatuses] ([RowId]),
    CONSTRAINT [FK_Appointments_AppointmentStatuses_2] FOREIGN KEY ([Status2RowId]) REFERENCES [dbo].[AppointmentStatuses] ([RowId]),
    CONSTRAINT [FK_Appointments_InsuranceProviders] FOREIGN KEY ([InsuranceProviderRowId]) REFERENCES [dbo].[InsuranceProviders] ([RowId]),
    CONSTRAINT [FK_Appointments_Patients] FOREIGN KEY ([PatientRowId]) REFERENCES [dbo].[Patients] ([RowId]),
    CONSTRAINT [FK_Appointments_ServiceProviders] FOREIGN KEY ([ServiceProviderRowId]) REFERENCES [dbo].[ServiceProviders] ([RowId])
);

