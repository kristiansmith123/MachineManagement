CREATE TABLE [dbo].[VirtualMachines]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] NVARCHAR(MAX) NOT NULL, 
    [IP] NVARCHAR(50) NOT NULL, 
    [Notes] NVARCHAR(MAX) NULL, 
    [Username] NVARCHAR(MAX) NULL, 
    [Password] NVARCHAR(MAX) NULL, 
    [ProxyIP] NVARCHAR(50) NULL, 
    [ProxyUsername] NVARCHAR(MAX) NULL, 
    [ProxyPassword] NVARCHAR(MAX) NULL, 
    [BotUsername] NVARCHAR(MAX) NULL, 
    [BotPassword] NVARCHAR(MAX) NULL, 
    [ProxyName] NVARCHAR(MAX) NULL, 
    [ProxyResourceGroup] NVARCHAR(MAX) NULL,
)
