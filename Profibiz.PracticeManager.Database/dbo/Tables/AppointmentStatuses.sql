CREATE TABLE [dbo].[AppointmentStatuses] (
    [RowId]           UNIQUEIDENTIFIER NOT NULL,
    [Name]            NVARCHAR (255)   NOT NULL,
    [ShortName]       NVARCHAR (255)   NOT NULL,
    [BackgroundColor] VARCHAR (50)     NOT NULL,
    [ForegroundColor] VARCHAR (50)     NOT NULL,
    [DisplayOrder]    INT              NOT NULL,
    CONSTRAINT [PK_AppointmentStatuses] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

