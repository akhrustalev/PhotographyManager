CREATE TABLE [dbo].[UserRoles] (
    [ID]       INT            IDENTITY (1, 1) NOT NULL,
    [RoleName] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.UserRoles] PRIMARY KEY CLUSTERED ([ID] ASC)
);

