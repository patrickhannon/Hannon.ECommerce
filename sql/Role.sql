USE [ECommerce]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2/28/2019 11:30:21 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF OBJECT_ID('dbo.Role', 'U') IS NOT NULL 
	DROP TABLE [dbo].[Role]

CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](30) NOT NULL
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO

INSERT INTO [dbo].[Role] ([Name])VALUES ('Customer')

SELECT [RoleId]
      ,[Name]
FROM [ECommerce].[dbo].[Role]

SET ANSI_PADDING OFF
GO


