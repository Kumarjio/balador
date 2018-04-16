USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_AccountContact]    Script Date: 4/16/2018 1:15:34 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_AccountContact](
	[AccountId] [nvarchar](180) NOT NULL,
	[ContactId] [nvarchar](180) NOT NULL,
	[LeadId] [uniqueidentifier] NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_Balador_AccountContact] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC,
	[ContactId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


