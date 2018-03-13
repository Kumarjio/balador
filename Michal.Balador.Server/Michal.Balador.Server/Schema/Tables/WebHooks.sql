USE [runnerdevice]
GO

/****** Object:  Table [dbo].[WebHooks]    Script Date: 3/13/2018 4:05:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[WebHooks](
	[User] [nvarchar](256) NOT NULL,
	[Id] [nvarchar](64) NOT NULL,
	[ProtectedData] [nvarchar](max) NOT NULL,
	[RowVer] [timestamp] NOT NULL,
 CONSTRAINT [PK_WebHooks.WebHooks] PRIMARY KEY CLUSTERED 
(
	[User] ASC,
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

