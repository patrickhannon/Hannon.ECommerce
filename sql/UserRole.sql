drop database AdvertureWorks
GO

USE [ECommerce]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 2/28/2019 11:40:23 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

IF OBJECT_ID('dbo.UserRole', 'U') IS NOT NULL 
	DROP TABLE [dbo].[UserRole]

CREATE TABLE [dbo].[UserRole](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[UserRoleId] INT NOT NULL
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] 

GO
SET ANSI_PADDING OFF
GO

declare @userId UniqueIdentifier, @role varchar(30)

SELECT @userId = '' 
		@role = 'Customer'

--insert into UserRole(UserId, UserRoleId) 
select @userId, r.RoleId 
FROM UserRole ur INNER JOIN [Role] r ON r.RoleId 
WHERE r.Name = @role;
