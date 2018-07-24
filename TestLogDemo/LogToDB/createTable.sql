CREATE TABLE [dbo].[Log](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NOT NULL,
	[Thread] [nvarchar](255) NOT NULL,
	[Level] [nvarchar](50) NOT NULL,
	[Logger] [nvarchar](255) NOT NULL,
	[MethodName] [nvarchar](1000) NOT NULL,
	[FolderName] [nvarchar](1000) NOT NULL,
	[PathName] [nvarchar](1000) NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Exception] [nvarchar](2000) NULL
)