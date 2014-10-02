USE [dbName]
GO

EXEC sp_changedbowner 'sa'
GO

ALTER DATABASE [dbName] SET TRUSTWORTHY ON;
GO