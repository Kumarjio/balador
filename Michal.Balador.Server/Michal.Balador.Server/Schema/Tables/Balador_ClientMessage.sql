USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_ClientMessage]    Script Date: 3/13/2018 4:01:27 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_ClientMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[Messsage] [nvarchar](max) NOT NULL,
   [MesssageType] [nvarchar](50) NOT NULL,
	[AccountId] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Balador_ClientMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

