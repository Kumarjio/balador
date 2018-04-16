USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_AccountContact]    Script Date: 4/16/2018 2:54:53 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_AccountContact](
	[Id] [uniqueidentifier] NOT NULL,
	[AccountId] [nvarchar](180) NOT NULL,
	[ContactId] [nvarchar](180) NULL,
	[LeadId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
	[Status] [int] NOT NULL,
 CONSTRAINT [PK_Balador_AccountContact_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

