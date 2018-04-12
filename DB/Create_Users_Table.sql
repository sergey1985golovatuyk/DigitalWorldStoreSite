CREATE TABLE [dbo].[User]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [Name] NVARCHAR(50) NOT NULL, 
    [LastName] NVARCHAR(50) NOT NULL, 
    [MiddleName] NVARCHAR(50) NULL, 
    [Email] NVARCHAR(150) NOT NULL, 
    [Password] NVARCHAR(50) NOT NULL, 
    [AddedDate] DATETIME NOT NULL, 
    [ActivatedDate] DATETIME NULL , 
    [ActivatedLink ] NVARCHAR(50) NOT NULL, 
    [LastVisitDate] DATETIME NULL
)
