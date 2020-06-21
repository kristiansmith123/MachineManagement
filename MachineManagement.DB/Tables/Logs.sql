CREATE TABLE [dbo].[Logs]
(
	[LogId] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Level] NVARCHAR(MAX) NULL,
    [CallSite] NVARCHAR(MAX) NULL,
    [Type] NVARCHAR(MAX) NULL,
    [Message] NVARCHAR(MAX) NULL,
    [StackTrace] NVARCHAR(MAX) NULL,
    [InnerException] NVARCHAR(MAX) NULL,
    [AdditionalInfo] NVARCHAR(MAX) NULL, 
    [LoggedOnDate] NVARCHAR(MAX) NULL,
)
