CREATE TABLE [dbo].[Referrers] (
    [RowId]        UNIQUEIDENTIFIER NOT NULL,
    [Name]         NVARCHAR (255)   NOT NULL,
    [AdrdessLine1] NVARCHAR (255)   NULL,
    [Province]     NVARCHAR (50)    NULL,
    [City]         NVARCHAR (50)    NULL,
    [Postcode]     NVARCHAR (50)    NULL,
    [Phone]        NVARCHAR (255)   NULL,
    [Email]        NVARCHAR (255)   NULL,
    [Created]      DATETIME         NULL,
    [CreatedBy]    NVARCHAR (50)    NULL,
    [Updated]      DATETIME         NULL,
    [UpdatedBy]    NVARCHAR (50)    NULL,
    CONSTRAINT [PK_Referrer] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

