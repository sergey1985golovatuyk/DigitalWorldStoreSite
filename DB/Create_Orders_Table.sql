CREATE TABLE [dbo].[Table]
(
	[OrderId] INT NOT NULL PRIMARY KEY,
	[CustomerFirstName] NVARCHAR(500) NOT NULL,
	[CustomerLastName] NVARCHAR(500) NOT NULL,
	[CustomerMiddleName] NVARCHAR(500) NOT NULL,
	[DeliveryAddress]  NVARCHAR(500) NOT NULL,
	[CustomerEmail] NVARCHAR(500) NOT NULL,
	[CustomerPhone] NVARCHAR(500) NOT NULL,
	[TotalPrice] DECIMAL NOT NULL
)
