CREATE TABLE [dbo].[Categories] (
    [Id]   INT           IDENTITY NOT NULL,
    [Name] NVARCHAR (50) NOT NULL,
    CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED (Id)
);
GO
CREATE UNIQUE INDEX [UIX_Categories_Name] ON dbo.[Categories] ([Name]);
GO