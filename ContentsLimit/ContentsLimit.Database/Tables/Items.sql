CREATE TABLE [dbo].[Items] (
    [Id]                    INT           IDENTITY NOT NULL,
    [CategoryId]            INT           NOT NULL,
    [Name]                  NVARCHAR (50) NOT NULL,
    [Cost]                  DECIMAL       NOT NULL,
    CONSTRAINT [PK_Items] PRIMARY KEY CLUSTERED (Id),
    CONSTRAINT [FK_Items_Categories] FOREIGN KEY (CategoryId) REFERENCES [dbo].[Categories] (Id) ON DELETE CASCADE,
);
GO
CREATE INDEX [UIX_Items_CategoryId] ON dbo.[Items] ([CategoryId]);
GO
CREATE INDEX [UIX_Items_Name] ON dbo.[Items] ([Name]);
GO