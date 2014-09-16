CREATE TABLE [dbo].[User] (
    [ID]            INT            IDENTITY (1, 1) NOT NULL,
    [Name]          VARCHAR (50)   NULL,
    [Discriminator] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED ([ID] ASC)
);

