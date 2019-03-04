USE [ECommerce]
GO

--DROP table [dbo].[Currency]

SELECT TOP 1000 [CurrencyKey]
      ,[CurrencyAlternateKey]
      ,[CurrencyName]
  FROM [AdventureWorksDW].[dbo].[DimCurrency]

/****** Object:  Table [dbo].[DimCurrency]    Script Date: 3/4/2019 5:22:46 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Currency](
	[CurrencyKey] [int] NOT NULL,
	[CurrencyAlternateKey] [nchar](3) NOT NULL,
	[CurrencyName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_DimCurrency_CurrencyKey] PRIMARY KEY CLUSTERED 
(
	[CurrencyKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


