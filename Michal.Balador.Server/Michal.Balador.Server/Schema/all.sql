USE [master]
GO
/****** Object:  Database [runnerde_runner]    Script Date: 1/2/2018 3:57:16 PM ******/
CREATE DATABASE [runnerde_runner]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'runnerde_runner', FILENAME = N'G:\runnerde_runner.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'runnerde_runner_log', FILENAME = N'G:\runnerde_runner_log.ldf' , SIZE = 18560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [runnerde_runner] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [runnerde_runner].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [runnerde_runner] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [runnerde_runner] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [runnerde_runner] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [runnerde_runner] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [runnerde_runner] SET ARITHABORT OFF 
GO
ALTER DATABASE [runnerde_runner] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [runnerde_runner] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [runnerde_runner] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [runnerde_runner] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [runnerde_runner] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [runnerde_runner] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [runnerde_runner] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [runnerde_runner] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [runnerde_runner] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [runnerde_runner] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [runnerde_runner] SET  ENABLE_BROKER 
GO
ALTER DATABASE [runnerde_runner] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [runnerde_runner] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [runnerde_runner] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [runnerde_runner] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [runnerde_runner] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [runnerde_runner] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [runnerde_runner] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [runnerde_runner] SET RECOVERY FULL 
GO
ALTER DATABASE [runnerde_runner] SET  MULTI_USER 
GO
ALTER DATABASE [runnerde_runner] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [runnerde_runner] SET DB_CHAINING OFF 
GO
ALTER DATABASE [runnerde_runner] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [runnerde_runner] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [runnerde_runner]
GO
/****** Object:  User [runne_michal]    Script Date: 1/2/2018 3:57:16 PM ******/
CREATE USER [runne_michal] FOR LOGIN [runne_michal] WITH DEFAULT_SCHEMA=[runne_michal]
GO
/****** Object:  User [runne_king]    Script Date: 1/2/2018 3:57:16 PM ******/
CREATE USER [runne_king] FOR LOGIN [runne_king] WITH DEFAULT_SCHEMA=[runne_king]
GO
ALTER ROLE [db_owner] ADD MEMBER [runne_michal]
GO
ALTER ROLE [db_owner] ADD MEMBER [runne_king]
GO
/****** Object:  Schema [runne_king]    Script Date: 1/2/2018 3:57:16 PM ******/
CREATE SCHEMA [runne_king]
GO
/****** Object:  Schema [runne_michal]    Script Date: 1/2/2018 3:57:16 PM ******/
CREATE SCHEMA [runne_michal]
GO
/****** Object:  StoredProcedure [dbo].[deleteallshipping]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
--exec [dbo].[deleteallshipping]
 CREATE proc [dbo].[deleteallshipping]
  as 
  begin
  delete  FROM AttachmentShippings
 delete  FROM Comments
   delete  FROM TimeLines
   delete from dbo.RequestItemShips
   delete from dbo.RequestShippings
   delete  FROM  ShippingItems

  delete  FROM [dbo].[Shippings]
  
  delete   FROM [kipodealdb_test].[dbo].[NotifyMessages]

  UPDATE [XbzCounters] SET [LastNumber] =0
     



  end


GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ApplicationUserShippings]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ApplicationUserShippings](
	[ApplicationUser_Id] [nvarchar](128) NOT NULL,
	[Shipping_ShippingId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.ApplicationUserShippings] PRIMARY KEY CLUSTERED 
(
	[ApplicationUser_Id] ASC,
	[Shipping_ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[User_Id] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[UserId] [nvarchar](128) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[UserName] [nvarchar](max) NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[AddressUser_CityCode] [nvarchar](max) NULL,
	[AddressUser_CityName] [nvarchar](max) NULL,
	[AddressUser_StreetCode] [nvarchar](max) NULL,
	[AddressUser_StreetName] [nvarchar](max) NULL,
	[AddressUser_ExtraDetail] [nvarchar](max) NULL,
	[AddressUser_StreetNum] [nvarchar](max) NULL,
	[AddressUser_IsSensor] [bit] NULL,
	[AddressUser_UID] [int] NULL,
	[AddressUser_Lat] [float] NULL,
	[AddressUser_Lng] [float] NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[FirstName] [nvarchar](max) NULL,
	[LastName] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Tel] [nvarchar](max) NULL,
	[Organization_OrgId] [uniqueidentifier] NULL,
	[IsActive] [bit] NULL,
	[Department] [nvarchar](max) NULL,
	[Subdivision] [nvarchar](max) NULL,
	[EmpId] [nvarchar](max) NULL,
	[DefaultView] [int] NULL,
	[ViewAll] [bit] NULL,
	[Discriminator] [nvarchar](128) NOT NULL,
	[GrantUserManager] [uniqueidentifier] NULL,
	[IsClientUser] [bit] NULL,
	[UserTypeSender] [int] NULL,
	[ProfilePicPath] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[EmailConfirmed] [bit] NULL,
	[PhoneNumberConfirmed] [bit] NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NULL,
	[AccessFailedCount] [int] NULL,
	[TwoFactorEnabled] [bit] NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[AttachmentShippings]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AttachmentShippings](
	[CommentId] [uniqueidentifier] NOT NULL,
	[Shipping_ShippingId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Path] [nvarchar](max) NULL,
	[IsSign] [bit] NOT NULL,
	[TypeMime] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.AttachmentShippings] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BussinessClosures]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BussinessClosures](
	[BussinessClosureId] [uniqueidentifier] NOT NULL,
	[DayOfWeek] [int] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[IsDayOff] [bit] NOT NULL,
	[SpecialDate] [datetime] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Year] [int] NOT NULL,
	[ShippingCompany] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.BussinessClosures] PRIMARY KEY CLUSTERED 
(
	[BussinessClosureId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Cities]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cities](
	[CityCode] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Cities] PRIMARY KEY CLUSTERED 
(
	[CityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Clients]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clients](
	[Id] [nvarchar](128) NOT NULL,
	[Secret] [nvarchar](max) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[ApplicationType] [int] NOT NULL,
	[Active] [bit] NOT NULL,
	[RefreshTokenLifeTime] [int] NOT NULL,
	[AllowedOrigin] [nvarchar](100) NULL,
 CONSTRAINT [PK_dbo.Clients] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Comments]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comments](
	[CommentId] [uniqueidentifier] NOT NULL,
	[Shipping_ShippingId] [uniqueidentifier] NULL,
	[JobType] [nvarchar](max) NULL,
	[JobTitle] [nvarchar](max) NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[DiscountId] [uniqueidentifier] NOT NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[Organizations_OrgId] [uniqueidentifier] NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[IsSweeping] [bit] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Discounts] PRIMARY KEY CLUSTERED 
(
	[DiscountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DistanceCities]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DistanceCities](
	[CityCode1] [nvarchar](128) NOT NULL,
	[CityCode2] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[DestinationAddress] [nvarchar](max) NULL,
	[OriginAddress] [nvarchar](max) NULL,
	[DestinationLat] [float] NOT NULL,
	[DestinationLng] [float] NOT NULL,
	[OriginLat] [float] NOT NULL,
	[OriginLng] [float] NOT NULL,
	[DistanceValue] [real] NOT NULL,
	[DistanceText] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[FixedDistanceValue] [float] NOT NULL,
	[CityName1] [nvarchar](max) NULL,
	[CityName2] [nvarchar](max) NULL,
	[Url] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.DistanceCities] PRIMARY KEY CLUSTERED 
(
	[CityCode1] ASC,
	[CityCode2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Distances]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Distances](
	[DistanceId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Desc] [nvarchar](max) NULL,
	[FromDistance] [real] NULL,
	[ToDistance] [real] NULL,
 CONSTRAINT [PK_dbo.Distances] PRIMARY KEY CLUSTERED 
(
	[DistanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Friend]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Friend](
	[UserId1] [uniqueidentifier] NOT NULL,
	[UserId2] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Friend] PRIMARY KEY CLUSTERED 
(
	[UserId1] ASC,
	[UserId2] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Leads]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Leads](
	[LeadId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Email] [nvarchar](max) NULL,
	[Tel] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Leads] PRIMARY KEY CLUSTERED 
(
	[LeadId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Member]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Member](
	[MemberId] [uniqueidentifier] NOT NULL,
	[UserId1] [uniqueidentifier] NOT NULL,
	[UserId2] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Member] PRIMARY KEY CLUSTERED 
(
	[MemberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[MoreAddress]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoreAddress](
	[MoreAddressId] [uniqueidentifier] NOT NULL,
	[ObjectId] [uniqueidentifier] NOT NULL,
	[ObjectTableCode] [int] NOT NULL,
	[Address_CityCode] [nvarchar](max) NULL,
	[Address_CityName] [nvarchar](max) NULL,
	[Address_StreetCode] [nvarchar](max) NULL,
	[Address_StreetName] [nvarchar](max) NULL,
	[Address_ExtraDetail] [nvarchar](max) NULL,
	[Address_StreetNum] [nvarchar](max) NULL,
	[Address_IsSensor] [bit] NOT NULL,
	[Address_UID] [int] NOT NULL,
	[Address_Lat] [float] NOT NULL,
	[Address_Lng] [float] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Tel1] [nvarchar](max) NULL,
	[Name1] [nvarchar](max) NULL,
	[Name2] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.MoreAddress] PRIMARY KEY CLUSTERED 
(
	[MoreAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NaiveEFAuthSessionEntry]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NaiveEFAuthSessionEntry](
	[Key] [nvarchar](max) NULL,
	[TicketString] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NotifyMessages]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NotifyMessages](
	[NotifyMessageId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](max) NOT NULL,
	[Body] [nvarchar](max) NOT NULL,
	[ToUrl] [nvarchar](max) NULL,
	[userId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[IsRead] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.NotifyMessages] PRIMARY KEY CLUSTERED 
(
	[NotifyMessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OrganizationDistances]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrganizationDistances](
	[Organization_OrgId] [uniqueidentifier] NOT NULL,
	[Distance_DistanceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.OrganizationDistances] PRIMARY KEY CLUSTERED 
(
	[Organization_OrgId] ASC,
	[Distance_DistanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Organizations]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Organizations](
	[OrgId] [uniqueidentifier] NOT NULL,
	[AddressOrg_CityCode] [nvarchar](max) NULL,
	[AddressOrg_CityName] [nvarchar](max) NULL,
	[AddressOrg_StreetCode] [nvarchar](max) NULL,
	[AddressOrg_StreetName] [nvarchar](max) NULL,
	[AddressOrg_ExtraDetail] [nvarchar](max) NULL,
	[AddressOrg_StreetNum] [nvarchar](max) NULL,
	[AddressOrg_IsSensor] [bit] NOT NULL,
	[AddressOrg_UID] [int] NOT NULL,
	[AddressOrg_Lat] [float] NOT NULL,
	[AddressOrg_Lng] [float] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Domain] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[PriceValueException] [decimal](18, 2) NULL,
	[Perfix] [nvarchar](max) NULL,
	[OrganizationCode] [int] NOT NULL,
	[ShippingCompanyIdDefault] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Organizations] PRIMARY KEY CLUSTERED 
(
	[OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[PriceLists]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PriceLists](
	[PriceListId] [uniqueidentifier] NOT NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[Organizations_OrgId] [uniqueidentifier] NULL,
	[ObjectId] [uniqueidentifier] NOT NULL,
	[ObjectTypeCode] [int] NOT NULL,
	[PriceValue] [decimal](18, 2) NULL,
	[PriceValueType] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[BeginDate] [datetime] NOT NULL,
	[EndDate] [datetime] NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[QuntityType] [int] NOT NULL,
	[IsPublish] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.PriceLists] PRIMARY KEY CLUSTERED 
(
	[PriceListId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductOrganizations]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductOrganizations](
	[Product_ProductId] [uniqueidentifier] NOT NULL,
	[Organization_OrgId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.ProductOrganizations] PRIMARY KEY CLUSTERED 
(
	[Product_ProductId] ASC,
	[Organization_OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ProductNumber] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[IsCalculatingShippingInclusive] [bit] NOT NULL,
	[Desc] [nvarchar](max) NULL,
	[ParentProductId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductShippingCompanies]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductShippingCompanies](
	[Product_ProductId] [uniqueidentifier] NOT NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.ProductShippingCompanies] PRIMARY KEY CLUSTERED 
(
	[Product_ProductId] ASC,
	[ShippingCompany_ShippingCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ProductSystems]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductSystems](
	[ProductSystemId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[ProductKey] [int] NOT NULL,
	[ProductTypeKey] [int] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[SetDefaultValue] [bit] NULL,
 CONSTRAINT [PK_dbo.ProductSystems] PRIMARY KEY CLUSTERED 
(
	[ProductSystemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RefreshTokens]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RefreshTokens](
	[Id] [nvarchar](128) NOT NULL,
	[Subject] [nvarchar](50) NOT NULL,
	[ClientId] [nvarchar](50) NOT NULL,
	[IssuedUtc] [datetime] NOT NULL,
	[ExpiresUtc] [datetime] NOT NULL,
	[ProtectedTicket] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.RefreshTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestItemShips]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestItemShips](
	[RequestItemShipId] [uniqueidentifier] NOT NULL,
	[RequestShipping_RequestShippingId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[PriceValueType] [int] NOT NULL,
	[PriceValue] [decimal](18, 2) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[ObjectTypeId] [uniqueidentifier] NULL,
	[ObjectTypeIdCode] [int] NULL,
	[Amount] [int] NOT NULL,
	[ProductValue] [decimal](18, 2) NULL,
	[IsDiscount] [bit] NOT NULL,
	[QuntityType] [int] NOT NULL,
	[StatusRecord] [int] NOT NULL,
 CONSTRAINT [PK_dbo.RequestItemShips] PRIMARY KEY CLUSTERED 
(
	[RequestItemShipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[RequestShippings]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RequestShippings](
	[RequestShippingId] [uniqueidentifier] NOT NULL,
	[Shipping_ShippingId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[Organizations_OrgId] [uniqueidentifier] NULL,
	[StatusCode] [int] NOT NULL,
	[StatusReasonCode] [int] NOT NULL,
	[Price] [money] NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[OwnerId] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Total] [money] NOT NULL,
 CONSTRAINT [PK_dbo.RequestShippings] PRIMARY KEY CLUSTERED 
(
	[RequestShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_dbo.Role] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingCompanies]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingCompanies](
	[ShippingCompanyId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[ManagerId] [uniqueidentifier] NULL,
	[AddressCompany_CityCode] [nvarchar](max) NULL,
	[AddressCompany_CityName] [nvarchar](max) NULL,
	[AddressCompany_StreetCode] [nvarchar](max) NULL,
	[AddressCompany_StreetName] [nvarchar](max) NULL,
	[AddressCompany_ExtraDetail] [nvarchar](max) NULL,
	[AddressCompany_StreetNum] [nvarchar](max) NULL,
	[AddressCompany_IsSensor] [bit] NOT NULL,
	[AddressCompany_UID] [int] NOT NULL,
	[AddressCompany_Lat] [float] NOT NULL,
	[AddressCompany_Lng] [float] NOT NULL,
	[Tel] [nvarchar](max) NULL,
	[ContactFullName] [nvarchar](max) NULL,
	[ContactTel] [nvarchar](max) NULL,
	[Perfix] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ShippingCompanies] PRIMARY KEY CLUSTERED 
(
	[ShippingCompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingCompanyDistances]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingCompanyDistances](
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NOT NULL,
	[Distance_DistanceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.ShippingCompanyDistances] PRIMARY KEY CLUSTERED 
(
	[ShippingCompany_ShippingCompanyId] ASC,
	[Distance_DistanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingCompanyOrganizations]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingCompanyOrganizations](
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NOT NULL,
	[Organization_OrgId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_dbo.ShippingCompanyOrganizations] PRIMARY KEY CLUSTERED 
(
	[ShippingCompany_ShippingCompanyId] ASC,
	[Organization_OrgId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShippingItems]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippingItems](
	[ShippingItemId] [uniqueidentifier] NOT NULL,
	[Product_ProductId] [uniqueidentifier] NULL,
	[Shipping_ShippingId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Quantity] [float] NOT NULL,
 CONSTRAINT [PK_dbo.ShippingItems] PRIMARY KEY CLUSTERED 
(
	[ShippingItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Shippings]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shippings](
	[ShippingId] [uniqueidentifier] NOT NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[Target_CityCode] [nvarchar](max) NULL,
	[Target_CityName] [nvarchar](max) NULL,
	[Target_StreetCode] [nvarchar](max) NULL,
	[Target_StreetName] [nvarchar](max) NULL,
	[Target_ExtraDetail] [nvarchar](max) NULL,
	[Target_StreetNum] [nvarchar](max) NULL,
	[Target_IsSensor] [bit] NOT NULL,
	[Target_UID] [int] NOT NULL,
	[Target_Lat] [float] NOT NULL,
	[Target_Lng] [float] NOT NULL,
	[Source_CityCode] [nvarchar](max) NULL,
	[Source_CityName] [nvarchar](max) NULL,
	[Source_StreetCode] [nvarchar](max) NULL,
	[Source_StreetName] [nvarchar](max) NULL,
	[Source_ExtraDetail] [nvarchar](max) NULL,
	[Source_StreetNum] [nvarchar](max) NULL,
	[Source_IsSensor] [bit] NOT NULL,
	[Source_UID] [int] NOT NULL,
	[Source_Lat] [float] NOT NULL,
	[Source_Lng] [float] NOT NULL,
	[ShipType_ShipTypeId] [uniqueidentifier] NULL,
	[Distance_DistanceId] [uniqueidentifier] NULL,
	[StatusShipping_StatusShippingId] [uniqueidentifier] NULL,
	[Organization_OrgId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[OwnerId] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Price] [money] NOT NULL,
	[DiscountPrice] [money] NOT NULL,
	[ActualPrice] [money] NOT NULL,
	[FastSearchNumber] [bigint] NOT NULL,
	[ApprovalRequest] [uniqueidentifier] NULL,
	[ApprovalShip] [uniqueidentifier] NULL,
	[GrantRunner] [uniqueidentifier] NULL,
	[BroughtShippingSender] [uniqueidentifier] NULL,
	[BroughtShipmentCustomer] [uniqueidentifier] NULL,
	[NoBroughtShipmentCustomer] [uniqueidentifier] NULL,
	[CancelByUser] [uniqueidentifier] NULL,
	[CancelByAdmin] [uniqueidentifier] NULL,
	[ArrivedShippingSender] [uniqueidentifier] NULL,
	[ClosedShippment] [uniqueidentifier] NULL,
	[NotifyType] [int] NOT NULL,
	[NotifyText] [nvarchar](max) NULL,
	[Recipient] [nvarchar](max) NULL,
	[ActualRecipient] [nvarchar](max) NULL,
	[TelSource] [nvarchar](max) NULL,
	[TelTarget] [nvarchar](max) NULL,
	[NameSource] [nvarchar](max) NULL,
	[NameTarget] [nvarchar](max) NULL,
	[SlaTime] [datetime] NULL,
	[ActualTelTarget] [nvarchar](max) NULL,
	[ActualNameTarget] [nvarchar](max) NULL,
	[EndDesc] [nvarchar](max) NULL,
	[SigBackType] [int] NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[Direction] [int] NOT NULL,
	[TimeWaitStartSend] [datetime] NULL,
	[TimeWaitEndSend] [datetime] NULL,
	[TimeWaitStartSGet] [datetime] NULL,
	[TimeWaitEndGet] [datetime] NULL,
	[OfferId] [uniqueidentifier] NULL,
	[ApprovalPriceException] [uniqueidentifier] NULL,
	[IsInProccess] [bit] NOT NULL,
	[WalkOrder] [int] NOT NULL,
	[DistanceValue] [real] NOT NULL,
	[DistanceText] [nvarchar](max) NULL,
	[FixedDistanceValue] [float] NOT NULL,
	[ArrivedShippingGet] [uniqueidentifier] NULL,
	[ParentId] [uniqueidentifier] NULL,
	[GroupName] [nvarchar](50) NULL,
	[OrganizationTarget_OrgId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.Shippings] PRIMARY KEY CLUSTERED 
(
	[ShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ShipTypes]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipTypes](
	[ShipTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Desc] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.ShipTypes] PRIMARY KEY CLUSTERED 
(
	[ShipTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Slas]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Slas](
	[SlaId] [uniqueidentifier] NOT NULL,
	[Priority] [int] NOT NULL,
	[Mins] [float] NOT NULL,
	[ShipType_ShipTypeId] [uniqueidentifier] NULL,
	[ShippingCompany_ShippingCompanyId] [uniqueidentifier] NULL,
	[Distance_DistanceId] [uniqueidentifier] NULL,
	[Organizations_OrgId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Slas] PRIMARY KEY CLUSTERED 
(
	[SlaId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StatusShippings]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StatusShippings](
	[StatusShippingId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[OrderDirection] [int] NOT NULL,
	[IsOnActive] [bit] NULL,
 CONSTRAINT [PK_dbo.StatusShippings] PRIMARY KEY CLUSTERED 
(
	[StatusShippingId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SyncTable]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyncTable](
	[SyncTableId] [uniqueidentifier] NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[DeviceId] [nvarchar](max) NULL,
	[ClientId] [nvarchar](max) NULL,
	[LastUpdateRecord] [datetime] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[SyncStatus] [int] NOT NULL,
	[ObjectTableCode] [int] NOT NULL,
	[ObjectId] [uniqueidentifier] NOT NULL,
	[SyncStateRecord] [int] NOT NULL,
 CONSTRAINT [PK_dbo.SyncTable] PRIMARY KEY CLUSTERED 
(
	[SyncTableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TableTests]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TableTests](
	[TableTestId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Code] [int] NOT NULL,
	[Name] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.TableTests] PRIMARY KEY CLUSTERED 
(
	[TableTestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TimeLines]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeLines](
	[TimeLineId] [uniqueidentifier] NOT NULL,
	[Shipping_ShippingId] [uniqueidentifier] NULL,
	[Status] [int] NOT NULL,
	[StatusShipping_StatusShippingId] [uniqueidentifier] NULL,
	[Name] [nvarchar](max) NULL,
	[Desc] [nvarchar](max) NULL,
	[DescHtml] [nvarchar](max) NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[ApprovalRequest] [uniqueidentifier] NULL,
	[ApprovalShip] [uniqueidentifier] NULL,
	[OwnerShip] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.TimeLines] PRIMARY KEY CLUSTERED 
(
	[TimeLineId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 1/2/2018 3:57:16 PM ******/
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
/****** Object:  Table [dbo].[UserClaim]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserClaim](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserClaim] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserNotifies]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserNotifies](
	[UserNotifyId] [uniqueidentifier] NOT NULL,
	[DeviceId] [nvarchar](max) NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.UserNotifies] PRIMARY KEY CLUSTERED 
(
	[UserNotifyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[UserRole]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserRole](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.UserRole] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[WebHooks]    Script Date: 1/2/2018 3:57:16 PM ******/
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
/****** Object:  Table [dbo].[XbzCounters]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[XbzCounters](
	[XbzCounterId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](max) NULL,
	[LastNumber] [bigint] NOT NULL,
	[CreatedOn] [datetime] NULL,
	[ModifiedOn] [datetime] NULL,
	[CreatedBy] [uniqueidentifier] NULL,
	[ModifiedBy] [uniqueidentifier] NULL,
	[IsActive] [bit] NOT NULL,
	[Organizations_OrgId] [uniqueidentifier] NULL,
 CONSTRAINT [PK_dbo.XbzCounters] PRIMARY KEY CLUSTERED 
(
	[XbzCounterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  View [dbo].[Dashboard]    Script Date: 1/2/2018 3:57:16 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Dashboard]
AS
SELECT    stp.StatusShippingId, st.TargetId, st.TargetType, stp.[Desc], ISNULL(st.c, 0) AS TragetCount
FROM            dbo.StatusShippings AS stp inner JOIN
                             (SELECT        sp.StatusShippingId AS id, s.ShippingCompany_ShippingCompanyId AS TargetId, 
							 1 as TargetType, 
                                                         COUNT(s.ShippingId) AS c
                               FROM            dbo.StatusShippings AS sp INNER JOIN
                                                         dbo.Shippings AS s ON s.StatusShipping_StatusShippingId = sp.StatusShippingId
                               	where s.StatusShipping_StatusShippingId is not null and sp.IsOnActive is not null and sp.IsOnActive =1
							   GROUP BY sp.StatusShippingId, s.ShippingCompany_ShippingCompanyId
                               UNION ALL
                               SELECT        sp.StatusShippingId AS id, s.Organization_OrgId AS TargetId, 2 as TargetType, COUNT(s.ShippingId) AS c
                               FROM            dbo.StatusShippings AS sp inner JOIN
                                                        dbo.Shippings AS s ON s.StatusShipping_StatusShippingId = sp.StatusShippingId
														where s.Organization_OrgId is not null and sp.IsOnActive is not null and sp.IsOnActive =1
                               GROUP BY sp.StatusShippingId, s.Organization_OrgId) AS st ON stp.StatusShippingId = st.id



GO
ALTER TABLE [dbo].[BussinessClosures] ADD  DEFAULT ((0)) FOR [Year]
GO
ALTER TABLE [dbo].[BussinessClosures] ADD  DEFAULT ('00000000-0000-0000-0000-000000000000') FOR [ShippingCompany]
GO
ALTER TABLE [dbo].[DistanceCities] ADD  DEFAULT ((0)) FOR [FixedDistanceValue]
GO
ALTER TABLE [dbo].[NotifyMessages] ADD  DEFAULT ((0)) FOR [IsRead]
GO
ALTER TABLE [dbo].[PriceLists] ADD  DEFAULT ((0)) FOR [QuntityType]
GO
ALTER TABLE [dbo].[PriceLists] ADD  DEFAULT ((0)) FOR [IsPublish]
GO
ALTER TABLE [dbo].[RequestItemShips] ADD  DEFAULT ((0)) FOR [Amount]
GO
ALTER TABLE [dbo].[RequestItemShips] ADD  DEFAULT ((0)) FOR [IsDiscount]
GO
ALTER TABLE [dbo].[RequestItemShips] ADD  DEFAULT ((0)) FOR [QuntityType]
GO
ALTER TABLE [dbo].[RequestItemShips] ADD  DEFAULT ((0)) FOR [StatusRecord]
GO
ALTER TABLE [dbo].[RequestShippings] ADD  DEFAULT ((0)) FOR [Total]
GO
ALTER TABLE [dbo].[ShippingCompanies] ADD  DEFAULT ((0)) FOR [AddressCompany_IsSensor]
GO
ALTER TABLE [dbo].[ShippingCompanies] ADD  DEFAULT ((0)) FOR [AddressCompany_UID]
GO
ALTER TABLE [dbo].[ShippingCompanies] ADD  DEFAULT ((0)) FOR [AddressCompany_Lat]
GO
ALTER TABLE [dbo].[ShippingCompanies] ADD  DEFAULT ((0)) FOR [AddressCompany_Lng]
GO
ALTER TABLE [dbo].[Shippings] ADD  DEFAULT ((0)) FOR [IsInProccess]
GO
ALTER TABLE [dbo].[Shippings] ADD  DEFAULT ((0)) FOR [WalkOrder]
GO
ALTER TABLE [dbo].[Shippings] ADD  DEFAULT ((0)) FOR [DistanceValue]
GO
ALTER TABLE [dbo].[Shippings] ADD  DEFAULT ((0)) FOR [FixedDistanceValue]
GO
ALTER TABLE [dbo].[TableTests] ADD  DEFAULT ((0)) FOR [Code]
GO
ALTER TABLE [dbo].[ApplicationUserShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ApplicationUserShippings_dbo.AspNetUsers_ApplicationUser_Id] FOREIGN KEY([ApplicationUser_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserShippings] CHECK CONSTRAINT [FK_dbo.ApplicationUserShippings_dbo.AspNetUsers_ApplicationUser_Id]
GO
ALTER TABLE [dbo].[ApplicationUserShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ApplicationUserShippings_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ApplicationUserShippings] CHECK CONSTRAINT [FK_dbo.ApplicationUserShippings_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id] FOREIGN KEY([User_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_User_Id]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.Organizations_Organization_OrgId] FOREIGN KEY([Organization_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_dbo.AspNetUsers_dbo.Organizations_Organization_OrgId]
GO
ALTER TABLE [dbo].[AspNetUsers]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUsers_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[AspNetUsers] CHECK CONSTRAINT [FK_dbo.AspNetUsers_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[AttachmentShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AttachmentShippings_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[AttachmentShippings] CHECK CONSTRAINT [FK_dbo.AttachmentShippings_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[Comments]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Comments_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[Comments] CHECK CONSTRAINT [FK_dbo.Comments_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Discounts_dbo.Organizations_Organizations_OrgId] FOREIGN KEY([Organizations_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_dbo.Discounts_dbo.Organizations_Organizations_OrgId]
GO
ALTER TABLE [dbo].[Discounts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Discounts_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[Discounts] CHECK CONSTRAINT [FK_dbo.Discounts_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[OrganizationDistances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrganizationDistances_dbo.Distances_Distance_DistanceId] FOREIGN KEY([Distance_DistanceId])
REFERENCES [dbo].[Distances] ([DistanceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrganizationDistances] CHECK CONSTRAINT [FK_dbo.OrganizationDistances_dbo.Distances_Distance_DistanceId]
GO
ALTER TABLE [dbo].[OrganizationDistances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.OrganizationDistances_dbo.Organizations_Organization_OrgId] FOREIGN KEY([Organization_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[OrganizationDistances] CHECK CONSTRAINT [FK_dbo.OrganizationDistances_dbo.Organizations_Organization_OrgId]
GO
ALTER TABLE [dbo].[PriceLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PriceLists_dbo.Organizations_Organizations_OrgId] FOREIGN KEY([Organizations_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[PriceLists] CHECK CONSTRAINT [FK_dbo.PriceLists_dbo.Organizations_Organizations_OrgId]
GO
ALTER TABLE [dbo].[PriceLists]  WITH CHECK ADD  CONSTRAINT [FK_dbo.PriceLists_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[PriceLists] CHECK CONSTRAINT [FK_dbo.PriceLists_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[ProductOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductOrganizations_dbo.Organizations_Organization_OrgId] FOREIGN KEY([Organization_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductOrganizations] CHECK CONSTRAINT [FK_dbo.ProductOrganizations_dbo.Organizations_Organization_OrgId]
GO
ALTER TABLE [dbo].[ProductOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductOrganizations_dbo.Products_Product_ProductId] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductOrganizations] CHECK CONSTRAINT [FK_dbo.ProductOrganizations_dbo.Products_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductShippingCompanies]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductShippingCompanies_dbo.Products_Product_ProductId] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductShippingCompanies] CHECK CONSTRAINT [FK_dbo.ProductShippingCompanies_dbo.Products_Product_ProductId]
GO
ALTER TABLE [dbo].[ProductShippingCompanies]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ProductShippingCompanies_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ProductShippingCompanies] CHECK CONSTRAINT [FK_dbo.ProductShippingCompanies_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[RequestItemShips]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestItemShips_dbo.RequestShippings_RequestShipping_RequestShippingId] FOREIGN KEY([RequestShipping_RequestShippingId])
REFERENCES [dbo].[RequestShippings] ([RequestShippingId])
GO
ALTER TABLE [dbo].[RequestItemShips] CHECK CONSTRAINT [FK_dbo.RequestItemShips_dbo.RequestShippings_RequestShipping_RequestShippingId]
GO
ALTER TABLE [dbo].[RequestShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestShippings_dbo.Organizations_Organizations_OrgId] FOREIGN KEY([Organizations_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[RequestShippings] CHECK CONSTRAINT [FK_dbo.RequestShippings_dbo.Organizations_Organizations_OrgId]
GO
ALTER TABLE [dbo].[RequestShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestShippings_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[RequestShippings] CHECK CONSTRAINT [FK_dbo.RequestShippings_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[RequestShippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.RequestShippings_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[RequestShippings] CHECK CONSTRAINT [FK_dbo.RequestShippings_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[ShippingCompanyDistances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingCompanyDistances_dbo.Distances_Distance_DistanceId] FOREIGN KEY([Distance_DistanceId])
REFERENCES [dbo].[Distances] ([DistanceId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShippingCompanyDistances] CHECK CONSTRAINT [FK_dbo.ShippingCompanyDistances_dbo.Distances_Distance_DistanceId]
GO
ALTER TABLE [dbo].[ShippingCompanyDistances]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingCompanyDistances_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShippingCompanyDistances] CHECK CONSTRAINT [FK_dbo.ShippingCompanyDistances_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[ShippingCompanyOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingCompanyOrganizations_dbo.Organizations_Organization_OrgId] FOREIGN KEY([Organization_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShippingCompanyOrganizations] CHECK CONSTRAINT [FK_dbo.ShippingCompanyOrganizations_dbo.Organizations_Organization_OrgId]
GO
ALTER TABLE [dbo].[ShippingCompanyOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingCompanyOrganizations_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ShippingCompanyOrganizations] CHECK CONSTRAINT [FK_dbo.ShippingCompanyOrganizations_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[ShippingItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingItems_dbo.Products_Product_ProductId] FOREIGN KEY([Product_ProductId])
REFERENCES [dbo].[Products] ([ProductId])
GO
ALTER TABLE [dbo].[ShippingItems] CHECK CONSTRAINT [FK_dbo.ShippingItems_dbo.Products_Product_ProductId]
GO
ALTER TABLE [dbo].[ShippingItems]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ShippingItems_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[ShippingItems] CHECK CONSTRAINT [FK_dbo.ShippingItems_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Shippings_dbo.Distances_Distance_DistanceId] FOREIGN KEY([Distance_DistanceId])
REFERENCES [dbo].[Distances] ([DistanceId])
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_dbo.Shippings_dbo.Distances_Distance_DistanceId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Shippings_dbo.Organizations_Organization_OrgId] FOREIGN KEY([Organization_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_dbo.Shippings_dbo.Organizations_Organization_OrgId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Shippings_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_dbo.Shippings_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Shippings_dbo.ShipTypes_ShipType_ShipTypeId] FOREIGN KEY([ShipType_ShipTypeId])
REFERENCES [dbo].[ShipTypes] ([ShipTypeId])
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_dbo.Shippings_dbo.ShipTypes_ShipType_ShipTypeId]
GO
ALTER TABLE [dbo].[Shippings]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Shippings_dbo.StatusShippings_StatusShipping_StatusShippingId] FOREIGN KEY([StatusShipping_StatusShippingId])
REFERENCES [dbo].[StatusShippings] ([StatusShippingId])
GO
ALTER TABLE [dbo].[Shippings] CHECK CONSTRAINT [FK_dbo.Shippings_dbo.StatusShippings_StatusShipping_StatusShippingId]
GO
ALTER TABLE [dbo].[Slas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Slas_dbo.Distances_Distance_DistanceId] FOREIGN KEY([Distance_DistanceId])
REFERENCES [dbo].[Distances] ([DistanceId])
GO
ALTER TABLE [dbo].[Slas] CHECK CONSTRAINT [FK_dbo.Slas_dbo.Distances_Distance_DistanceId]
GO
ALTER TABLE [dbo].[Slas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Slas_dbo.Organizations_Organizations_OrgId] FOREIGN KEY([Organizations_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[Slas] CHECK CONSTRAINT [FK_dbo.Slas_dbo.Organizations_Organizations_OrgId]
GO
ALTER TABLE [dbo].[Slas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Slas_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId] FOREIGN KEY([ShippingCompany_ShippingCompanyId])
REFERENCES [dbo].[ShippingCompanies] ([ShippingCompanyId])
GO
ALTER TABLE [dbo].[Slas] CHECK CONSTRAINT [FK_dbo.Slas_dbo.ShippingCompanies_ShippingCompany_ShippingCompanyId]
GO
ALTER TABLE [dbo].[Slas]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Slas_dbo.ShipTypes_ShipType_ShipTypeId] FOREIGN KEY([ShipType_ShipTypeId])
REFERENCES [dbo].[ShipTypes] ([ShipTypeId])
GO
ALTER TABLE [dbo].[Slas] CHECK CONSTRAINT [FK_dbo.Slas_dbo.ShipTypes_ShipType_ShipTypeId]
GO
ALTER TABLE [dbo].[TimeLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TimeLines_dbo.Shippings_Shipping_ShippingId] FOREIGN KEY([Shipping_ShippingId])
REFERENCES [dbo].[Shippings] ([ShippingId])
GO
ALTER TABLE [dbo].[TimeLines] CHECK CONSTRAINT [FK_dbo.TimeLines_dbo.Shippings_Shipping_ShippingId]
GO
ALTER TABLE [dbo].[TimeLines]  WITH CHECK ADD  CONSTRAINT [FK_dbo.TimeLines_dbo.StatusShippings_StatusShipping_StatusShippingId] FOREIGN KEY([StatusShipping_StatusShippingId])
REFERENCES [dbo].[StatusShippings] ([StatusShippingId])
GO
ALTER TABLE [dbo].[TimeLines] CHECK CONSTRAINT [FK_dbo.TimeLines_dbo.StatusShippings_StatusShipping_StatusShippingId]
GO
ALTER TABLE [dbo].[UserClaim]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserClaim_dbo.User_User_Id] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserClaim] CHECK CONSTRAINT [FK_dbo.UserClaim_dbo.User_User_Id]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.Role_RoleId]
GO
ALTER TABLE [dbo].[UserRole]  WITH CHECK ADD  CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[UserRole] CHECK CONSTRAINT [FK_dbo.UserRole_dbo.User_UserId]
GO
ALTER TABLE [dbo].[XbzCounters]  WITH CHECK ADD  CONSTRAINT [FK_dbo.XbzCounters_dbo.Organizations_Organizations_OrgId] FOREIGN KEY([Organizations_OrgId])
REFERENCES [dbo].[Organizations] ([OrgId])
GO
ALTER TABLE [dbo].[XbzCounters] CHECK CONSTRAINT [FK_dbo.XbzCounters_dbo.Organizations_Organizations_OrgId]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "stp"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 216
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "st"
            Begin Extent = 
               Top = 6
               Left = 254
               Bottom = 136
               Right = 424
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 9
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Dashboard'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Dashboard'
GO
USE [master]
GO
ALTER DATABASE [runnerde_runner] SET  READ_WRITE 
GO
