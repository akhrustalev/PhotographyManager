CREATE TABLE [dbo].[Album2Photo] (
    [AlbumID] INT NOT NULL,
    [PhotoID] INT NOT NULL,
    CONSTRAINT [PK_dbo.Album2Photo] PRIMARY KEY CLUSTERED ([AlbumID] ASC, [PhotoID] ASC),
    CONSTRAINT [FK_dbo.Album2Photo_dbo.Album_AlbumID] FOREIGN KEY ([AlbumID]) REFERENCES [dbo].[Album] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.Album2Photo_dbo.Photo_PhotoID] FOREIGN KEY ([PhotoID]) REFERENCES [dbo].[Photo] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_AlbumID]
    ON [dbo].[Album2Photo]([AlbumID] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_PhotoID]
    ON [dbo].[Album2Photo]([PhotoID] ASC);

