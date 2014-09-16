CREATE TABLE [dbo].[UsersInRoles] (
    [RoleId] INT NOT NULL,
    [UserId] INT NOT NULL,
    CONSTRAINT [PK_dbo.UsersInRoles] PRIMARY KEY CLUSTERED ([RoleId] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.UsersInRoles_dbo.User_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[User] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.UsersInRoles_dbo.UserRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[UserRoles] ([ID]) ON DELETE CASCADE
);


GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[UsersInRoles]([RoleId] ASC);


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UsersInRoles]([UserId] ASC);

