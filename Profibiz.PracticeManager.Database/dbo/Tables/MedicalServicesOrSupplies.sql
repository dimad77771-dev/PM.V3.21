CREATE TABLE [dbo].[MedicalServicesOrSupplies] (
    [RowId]         UNIQUEIDENTIFIER NOT NULL,
    [Code]          NVARCHAR (255)   NOT NULL,
    [Name]          NVARCHAR (255)   NOT NULL,
    [ItemType]      NVARCHAR (255)   NOT NULL,
    [Notes]         NVARCHAR (4000)  NULL,
    [UnitPrice]     DECIMAL (18, 4)  NULL,
    [TaxRate]       DECIMAL (18, 4)  NULL,
    [Model]         NVARCHAR (255)   NULL,
    [Supplier]      NVARCHAR (255)   NULL,
    [Size]          NVARCHAR (255)   NULL,
    [CategoryRowId] UNIQUEIDENTIFIER NULL,
    CONSTRAINT [PK_MedicalServicesAndSupplies] PRIMARY KEY CLUSTERED ([RowId] ASC),
    CONSTRAINT [FK_MedicalServicesOrSupplies_Categories] FOREIGN KEY ([CategoryRowId]) REFERENCES [dbo].[Categories] ([RowId])
);

