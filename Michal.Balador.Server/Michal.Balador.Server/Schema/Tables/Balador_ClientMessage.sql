USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_ClientMessage]    Script Date: 4/11/2018 11:10:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_ClientMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[ContactId] [nvarchar](128) NULL,
	[ClientId] [nvarchar](128) NOT NULL,
	[Messsage] [nvarchar](max) NOT NULL,
	[AccountId] [nvarchar](128) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
	[MesssageType] [nvarchar](50) NOT NULL,
	[ConversationId] [uniqueidentifier] NOT NULL,
	[JobId] [uniqueidentifier] NULL,
	[Replay] [bit] NOT NULL,
	[Archive] [bit] NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
	[Retry] [int] NOT NULL,
	[Direction] [bit] NOT NULL,
	[LeadId] [uniqueidentifier] NULL,
	[ResourceId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_Balador_ClientMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


