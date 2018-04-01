USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_Leads]    Script Date: 4/1/2018 9:30:19 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_Leads](
	[LeadId] [uniqueidentifier] NOT NULL,
	[NickName] [nvarchar](50) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[Owner] [nchar](128) NULL,
	[MessageType] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Leads] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

