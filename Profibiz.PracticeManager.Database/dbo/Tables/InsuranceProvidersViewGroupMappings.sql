CREATE TABLE [dbo].[InsuranceProvidersViewGroupMappings] (
    [RowId]                            UNIQUEIDENTIFIER NOT NULL,
    [InsuranceProvidersViewGroupRowId] UNIQUEIDENTIFIER NOT NULL,
    [InsuranceProviderRowId]           UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([RowId] ASC),
    FOREIGN KEY ([InsuranceProviderRowId]) REFERENCES [dbo].[InsuranceProviders] ([RowId]),
    FOREIGN KEY ([InsuranceProvidersViewGroupRowId]) REFERENCES [dbo].[InsuranceProvidersViewGroups] ([RowId]),
    UNIQUE NONCLUSTERED ([InsuranceProvidersViewGroupRowId] ASC, [InsuranceProviderRowId] ASC)
);

