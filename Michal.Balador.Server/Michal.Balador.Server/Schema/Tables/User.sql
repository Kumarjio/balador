USE [runnerdevice]
GO

/****** Object:  Table [dbo].[User]    Script Date: 3/13/2018 4:02:28 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[User](
	[Id] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Tel] [nvarchar](max) NULL,
	[IsActive] [bit] NULL,
	[ProfilePicPath] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[TwoFactorEnabled] [bit] NULL,
	[Discriminator] [nvarchar](50) NULL,
 CONSTRAINT [PK_dbo.User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

