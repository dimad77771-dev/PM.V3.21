CREATE TABLE [dbo].[ClientErrors] (
    [RowId]          UNIQUEIDENTIFIER NOT NULL,
    [ErrorDateTime]  DATETIME         NOT NULL,
    [ErrorText]      NVARCHAR (MAX)   NULL,
    [MachineName]    NVARCHAR (255)   NULL,
    [OSVersion]      NVARCHAR (255)   NULL,
    [UserName]       NVARCHAR (255)   NULL,
    [UserDomainName] NVARCHAR (255)   NULL,
    [ServerDateTime] DATETIME         NOT NULL,
    CONSTRAINT [PK_ClientErrors] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

