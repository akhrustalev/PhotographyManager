CREATE TABLE [dbo].[Membership] (
    [ID]           INT            NOT NULL,
    [Password]     NVARCHAR (128) NOT NULL,
    [PasswordSalt] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.Membership] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_dbo.Membership_dbo.User_ID] FOREIGN KEY ([ID]) REFERENCES [dbo].[User] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [IX_ID]
    ON [dbo].[Membership]([ID] ASC);

