CREATE TABLE [dbo].[Album] (
    [ID]          INT            IDENTITY (1, 1) NOT NULL,
    [Name]        VARCHAR (50)   NULL,
    [Discription] NVARCHAR (MAX) NULL,
    [UserID]      INT            NOT NULL,
    CONSTRAINT [PK_dbo.Album] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Album_dbo.User_UserID] FOREIGN KEY ([UserID]) REFERENCES [dbo].[User] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_UserID]
    ON [dbo].[Album]([UserID] ASC);

