CREATE TABLE [dbo].[PhotoImage] (
    [ID]          INT   NOT NULL,
    [BigImage]    IMAGE NULL,
    [MiddleImage] IMAGE NULL,
    [MiniImage]   IMAGE NULL,
    CONSTRAINT [PK_dbo.PhotoImage] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.PhotoImage_dbo.Photo_ID] FOREIGN KEY ([ID]) REFERENCES [dbo].[Photo] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ID]
    ON [dbo].[PhotoImage]([ID] ASC);

