CREATE TABLE [dbo].[NLogItems] (
    [RowId]       UNIQUEIDENTIFIER NOT NULL,
    [ActivityId]  UNIQUEIDENTIFIER NOT NULL,
    [MachineName] NVARCHAR (255)   NOT NULL,
    [Date]        DATETIME         NULL,
    [Level]       NVARCHAR (50)    NULL,
    [Logger]      NVARCHAR (MAX)   NULL,
    [Message]     NVARCHAR (MAX)   NULL,
    CONSTRAINT [PK_NLogItems] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

