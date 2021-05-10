CREATE TABLE [dbo].[ServiceProviderAssociations] (
    [RowId]                  UNIQUEIDENTIFIER NOT NULL,
    [AssociationRowId]       UNIQUEIDENTIFIER NOT NULL,
    [ServiceProviderRowId]   UNIQUEIDENTIFIER NOT NULL,
    [RegistrationNumber]     NVARCHAR (255)   NULL,
    [RegistrationDate]       DATETIME         NULL,
    [RegistrationExpiryDate] DATETIME         NULL,
    [IsPrimary]              BIT              NOT NULL,
    CONSTRAINT [PK_ServiceProviderAssociations] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_ServiceProviderAssociations_ProfessionalAssociations] FOREIGN KEY ([AssociationRowId]) REFERENCES [dbo].[ProfessionalAssociations] ([RowId]),
    CONSTRAINT [FK_ServiceProviderAssociations_ServiceProviders] FOREIGN KEY ([ServiceProviderRowId]) REFERENCES [dbo].[ServiceProviders] ([RowId])
);

