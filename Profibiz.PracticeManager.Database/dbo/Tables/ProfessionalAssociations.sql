CREATE TABLE [dbo].[ProfessionalAssociations] (
    [RowId]        UNIQUEIDENTIFIER NOT NULL,
    [Code]         NVARCHAR (255)   NULL,
    [Name]         NVARCHAR (255)   NULL,
    [AddressLine1] NVARCHAR (255)   NULL,
    [AddressLine2] NVARCHAR (255)   NULL,
    [Province]     NVARCHAR (255)   NULL,
    [City]         NVARCHAR (255)   NULL,
    [Postcode]     NVARCHAR (255)   NULL,
    [PhoneNumber]  NVARCHAR (255)   NULL,
    [EmailAddress] NVARCHAR (255)   NULL,
    [Fax]          NVARCHAR (255)   NULL,
    [WebSite]      NVARCHAR (255)   NULL,
    [Notes]        NVARCHAR (4000)  NULL,
    CONSTRAINT [PK_ProfessionalAssociations] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

