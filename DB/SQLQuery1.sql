CREATE TABLE [dbo].[CartLists] (
    [RecordId]  INT            IDENTITY (1, 1) NOT NULL,
    [CartId]    NVARCHAR (MAX) NOT NULL,
    [ProductId] INT            NOT NULL,
    [Quantity]  INT            NOT NULL,
    [Created]   DATETIME       NOT NULL)