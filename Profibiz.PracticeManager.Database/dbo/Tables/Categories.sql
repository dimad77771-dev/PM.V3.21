CREATE TABLE [dbo].[Categories] (
    [RowId]        UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (255)   NOT NULL,
    [CategoryType] NVARCHAR (255)   NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

