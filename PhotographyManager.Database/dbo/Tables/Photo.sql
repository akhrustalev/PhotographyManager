CREATE TABLE [dbo].[Photo] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)   NULL,
    [ShootingTime]  DATETIME       NULL,
    [ShootingPlace] VARCHAR (100)  NULL,
    [ShutterSpeed]  FLOAT (53)     NULL,
    [ISO]           VARCHAR (100)  NULL,
    [CameraModel]   NVARCHAR (100) NULL,
    [Diaphragm]     VARCHAR (100)  NULL,
    [FocalDistance] FLOAT (53)     NULL,
    [Flash]         BIT            NULL,
    [UserID]        INT            NOT NULL,
    CONSTRAINT [PK_dbo.Photo] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Photo_dbo.User_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_UserID]
    ON [dbo].[Photo]([UserID] ASC);

