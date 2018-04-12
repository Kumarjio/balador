USE [runnerdevice]
GO

/****** Object:  Table [dbo].[Balador_Resource]    Script Date: 4/12/2018 9:38:08 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Balador_Resource](
	[ResourceId] [uniqueidentifier] NOT NULL,
	[Content_type] [nvarchar](50) NOT NULL,
	[FileName] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Balador_Resource] PRIMARY KEY CLUSTERED 
(
	[ResourceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


