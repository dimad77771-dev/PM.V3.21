CREATE TABLE [dbo].[AppointmentBooks] (
    [RowId]                      UNIQUEIDENTIFIER NOT NULL,
    [Name]                       NVARCHAR (255)   NOT NULL,
    [Description]                NVARCHAR (255)   NULL,
    [DisplayOrder]               INT              NULL,
    [StartAt]                    DATETIME         NOT NULL,
    [FinishAt]                   DATETIME         NOT NULL,
    [Interval]                   INT              NOT NULL,
    [AppointmentBackgroundColor] VARCHAR (50)     NOT NULL,
    [AppointmentForegroundColor] VARCHAR (50)     NOT NULL,
    CONSTRAINT [PK_AppointmentBooks] PRIMARY KEY CLUSTERED ([RowId] ASC)
);

