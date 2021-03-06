USE [master]
GO
/****** Object:  Database [RedditScan]    Script Date: 2017/12/26 下午 02:25:24 ******/
CREATE DATABASE [RedditScan]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RedditScan', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RedditScan.mdf' , SIZE = 1373184KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RedditScan_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\RedditScan_log.ldf' , SIZE = 625792KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RedditScan] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RedditScan].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RedditScan] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RedditScan] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RedditScan] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RedditScan] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RedditScan] SET ARITHABORT OFF 
GO
ALTER DATABASE [RedditScan] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RedditScan] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RedditScan] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RedditScan] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RedditScan] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RedditScan] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RedditScan] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RedditScan] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RedditScan] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RedditScan] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RedditScan] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RedditScan] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RedditScan] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RedditScan] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RedditScan] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RedditScan] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RedditScan] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RedditScan] SET RECOVERY FULL 
GO
ALTER DATABASE [RedditScan] SET  MULTI_USER 
GO
ALTER DATABASE [RedditScan] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RedditScan] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RedditScan] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RedditScan] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [RedditScan] SET DELAYED_DURABILITY = DISABLED 
GO
USE [RedditScan]
GO
/****** Object:  Table [dbo].[chenDetail]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[chenDetail](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_chenDetail_UID]  DEFAULT (lower(newid())),
	[ListUID] [char](36) NOT NULL,
	[Author] [nvarchar](50) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_chenDetail] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChenList]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ChenList](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_ChenList_UID]  DEFAULT (lower(newid())),
	[Type] [varchar](50) NULL,
	[Title] [nvarchar](400) NOT NULL,
	[AuthorUrl] [nvarchar](100) NULL,
	[Author] [nvarchar](50) NULL,
	[Url] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PostDate] [datetime] NULL,
	[Likes] [int] NOT NULL,
	[RepliesCount] [int] NOT NULL,
	[Views] [int] NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_ChenList_CreateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_ChenList] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ChenProfile]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChenProfile](
	[ID] [nvarchar](200) NOT NULL,
	[Name] [nvarchar](200) NULL,
	[Sex] [nvarchar](4) NULL,
	[Follows] [int] NULL,
	[Address] [nvarchar](20) NULL,
	[Answers] [nvarchar](20) NULL,
	[Posts] [int] NULL,
	[ReplyCounts] [int] NULL,
 CONSTRAINT [PK_ChenProfile] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[FormosaList]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FormosaList](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_FormosaList_ID]  DEFAULT (lower(newid())),
	[Type] [varchar](50) NOT NULL,
	[Title] [nvarchar](400) NOT NULL,
	[Author] [varchar](50) NULL,
	[Url] [nvarchar](200) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PostDate] [datetime] NULL,
	[RepliesCount] [int] NOT NULL,
	[Views] [int] NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_FormosaList_CreateTime]  DEFAULT (getdate()),
 CONSTRAINT [PK_FormosaList] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FormosaReply]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FormosaReply](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_FormosaReply_UID]  DEFAULT (lower(newid())),
	[UID_List] [char](36) NOT NULL,
	[ID] [varchar](50) NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FormosaReply] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FormosaReply2]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[FormosaReply2](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_FormosaReply2_UID]  DEFAULT (lower(newid())),
	[UID_List] [char](36) NOT NULL,
	[ID] [int] NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[PostDate] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_FormosaReply2] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FormosaUser]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FormosaUser](
	[Name] [nvarchar](50) NOT NULL,
	[Location] [nvarchar](50) NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[List]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[List](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_List_UID]  DEFAULT (lower(newid())),
	[ID] [varchar](50) NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[Title] [nvarchar](400) NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[MessageCount] [varchar](50) NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NOT NULL CONSTRAINT [DF_List_CreateTime]  DEFAULT (getdate()),
	[Description] [nvarchar](max) NULL,
	[PostDate] [datetime] NULL,
 CONSTRAINT [PK_List] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RedditAll]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RedditAll](
	[UID] [char](36) NULL,
	[ID] [varchar](50) NOT NULL,
	[Type] [varchar](10) NOT NULL,
	[Title] [nvarchar](400) NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[MessageCount] [varchar](50) NOT NULL,
	[Url] [nvarchar](200) NOT NULL,
	[CreateTime] [datetime] NOT NULL,
	[Description] [nvarchar](max) NULL,
	[PostDate] [datetime] NULL,
	[ReplyAuthor] [varchar](50) NULL,
	[ReplyPostDate] [datetime] NULL,
	[ReplyMessage] [nvarchar](max) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Reply]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Reply](
	[UID] [char](36) NOT NULL CONSTRAINT [DF_Reply_UID]  DEFAULT (lower(newid())),
	[ID] [varchar](50) NOT NULL,
	[Author] [varchar](50) NOT NULL,
	[City] [varchar](50) NULL,
	[PostDate] [datetime] NOT NULL,
	[Message] [nvarchar](max) NOT NULL,
	[Parent] [char](36) NULL,
 CONSTRAINT [PK_Reply] PRIMARY KEY CLUSTERED 
(
	[UID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  View [dbo].[RedditView1]    Script Date: 2017/12/26 下午 02:25:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** SSMS 中 SelectTopNRows 命令的指令碼  ******/
CREATE VIEW [dbo].[RedditView1]
AS
SELECT          TOP (100) PERCENT l.UID, l.ID, l.Type, l.Title, l.Author, l.MessageCount, l.Url, l.Description, l.PostDate, 
                            r.Author AS Reply_Author, r.Message AS Reply_Message, r.PostDate AS Reply_PostDate, l.CreateTime
FROM              dbo.List AS l LEFT OUTER JOIN
                            dbo.Reply AS r ON r.ID = l.ID
ORDER BY   l.CreateTime, Reply_PostDate

GO
ALTER TABLE [dbo].[Reply]  WITH CHECK ADD  CONSTRAINT [FK_Reply_Reply] FOREIGN KEY([Parent])
REFERENCES [dbo].[Reply] ([UID])
GO
ALTER TABLE [dbo].[Reply] CHECK CONSTRAINT [FK_Reply_Reply]
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
         Begin Table = "l"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 212
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 250
               Bottom = 136
               Right = 415
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
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RedditView1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'RedditView1'
GO
USE [master]
GO
ALTER DATABASE [RedditScan] SET  READ_WRITE 
GO
