USE [FurrLife]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CartProducts]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CartProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
	[DateCreated] [datetime] NULL,
	[SessionId] [nvarchar](450) NULL,
 CONSTRAINT [PK_CartProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Discounts]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Discounts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Percentage] [int] NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Discounts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Menu]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Menu](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](300) NULL,
	[Description] [varchar](500) NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Menu] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderProducts]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderProducts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NULL,
	[OrderId] [int] NULL,
	[ProductId] [int] NULL,
	[Price] [decimal](11, 2) NULL,
	[Discounts] [int] NULL,
	[UserId] [nvarchar](450) NULL,
 CONSTRAINT [PK_OrderProducts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Orders]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Orders](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ReferenceNo] [varchar](max) NULL,
	[Payment] [varchar](50) NULL,
	[CustomerRequest] [varchar](max) NULL,
	[TotalAmount] [decimal](11, 2) NULL,
	[IsPaid] [bit] NULL,
	[Discounts] [int] NULL,
	[SeniorCitizenID] [varchar](50) NULL,
	[PWDID] [varchar](50) NULL,
	[UserId] [nvarchar](450) NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Persons]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Persons](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LastName] [varchar](300) NULL,
	[FirstName] [varchar](300) NULL,
	[MiddleName] [varchar](300) NULL,
	[Email] [varchar](300) NULL,
	[Birthday] [datetime] NULL,
	[CompleteAddress] [varchar](500) NULL,
	[ContactNumber] [varchar](30) NULL,
	[Gender] [varchar](10) NULL,
	[IsAdmin] [bit] NULL,
	[IsStaff] [bit] NULL,
	[IsMember] [bit] NULL,
	[Wallet] [decimal](11, 2) NULL,
	[CardNumber] [varchar](1000) NULL,
	[IsSeniorCitizen] [bit] NULL,
	[SeniorCitizenID] [varchar](200) NULL,
	[IsPWD] [bit] NULL,
	[PWDID] [varchar](200) NULL,
	[AccumulatedRewards] [decimal](11, 2) NULL,
	[Salary] [decimal](11, 2) NULL,
	[DateCreated] [datetime] NULL,
	[UserId] [nvarchar](300) NULL,
 CONSTRAINT [PK_Persons] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NULL,
	[Description] [varchar](1000) NULL,
	[Price] [decimal](11, 2) NULL,
	[ImageURL] [varchar](max) NULL,
	[ProductRating] [int] NULL,
	[Discounts] [int] NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
	[UnitId] [int] NULL,
	[MenuId] [int] NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Units]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Units](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](200) NULL,
	[Code] [varchar](200) NULL,
	[IsActive] [bit] NULL,
	[DateCreated] [datetime] NULL,
 CONSTRAINT [PK_Units] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'6.0.21')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'7161b8ef-2428-4a01-ad13-d2bdba7fc091', N'markperez06.ph@gmail.com', N'MARKPEREZ06.PH@GMAIL.COM', N'markperez06.ph@gmail.com', N'MARKPEREZ06.PH@GMAIL.COM', 1, N'AQAAAAEAACcQAAAAEDF7Z0t1yBIMhTmUBlPVksmTJZsqI9ajRGkJlDawrcoqb1QvbOP+At0I85xUavA4bQ==', N'3VNKAAEEK475AAYEKSX7KWDJWXXXY4JF', N'8f88edfb-3a05-4620-b37c-31daf9a92276', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[CartProducts] ON 
GO
INSERT [dbo].[CartProducts] ([Id], [ProductId], [Quantity], [DateCreated], [SessionId]) VALUES (77, 11, 1, CAST(N'2023-10-17T15:40:34.757' AS DateTime), N'71bb9017-cce0-40eb-90b3-532333574762')
GO
INSERT [dbo].[CartProducts] ([Id], [ProductId], [Quantity], [DateCreated], [SessionId]) VALUES (80, 11, 1, CAST(N'2023-10-18T12:48:58.990' AS DateTime), N'6d2e4a81-47da-4d94-80d5-f3bf9bfa67fa')
GO
INSERT [dbo].[CartProducts] ([Id], [ProductId], [Quantity], [DateCreated], [SessionId]) VALUES (81, 12, 1, CAST(N'2023-10-18T12:48:59.450' AS DateTime), N'6d2e4a81-47da-4d94-80d5-f3bf9bfa67fa')
GO
SET IDENTITY_INSERT [dbo].[CartProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Discounts] ON 
GO
INSERT [dbo].[Discounts] ([Id], [Name], [Percentage], [IsActive], [DateCreated]) VALUES (1, N'Less 20%', 20, 1, NULL)
GO
INSERT [dbo].[Discounts] ([Id], [Name], [Percentage], [IsActive], [DateCreated]) VALUES (2, N'Less 10%', 10, 1, NULL)
GO
INSERT [dbo].[Discounts] ([Id], [Name], [Percentage], [IsActive], [DateCreated]) VALUES (3, N'0%', 0, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[Discounts] OFF
GO
SET IDENTITY_INSERT [dbo].[Menu] ON 
GO
INSERT [dbo].[Menu] ([Id], [Name], [Description], [IsActive], [DateCreated]) VALUES (1, N'Breakfast Meal', N'Pagkaing pang umaga, barya lang po sa umaga mga ma`am and sir.', 1, NULL)
GO
INSERT [dbo].[Menu] ([Id], [Name], [Description], [IsActive], [DateCreated]) VALUES (2, N'Lunch Meal', N'Pagkaing pang tanghali, may barya na po kami mga ma`am and sir.', 1, NULL)
GO
INSERT [dbo].[Menu] ([Id], [Name], [Description], [IsActive], [DateCreated]) VALUES (3, N'Drinks', N'Inumin lang to mga ma`m and sir.', 1, NULL)
GO
INSERT [dbo].[Menu] ([Id], [Name], [Description], [IsActive], [DateCreated]) VALUES (6, N'Merienda Meal', N'Pagkaing pang merienda, may merienda na po kami mga ma`am and sir.', 1, CAST(N'2023-10-13T23:18:58.820' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Menu] OFF
GO
SET IDENTITY_INSERT [dbo].[OrderProducts] ON 
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (27, 2, 7, 12, CAST(90.00 AS Decimal(11, 2)), 10, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (28, 1, 7, 13, CAST(150.00 AS Decimal(11, 2)), 20, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (29, 2, 8, 11, CAST(150.00 AS Decimal(11, 2)), 10, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (30, 2, 8, 12, CAST(90.00 AS Decimal(11, 2)), 10, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (31, 2, 8, 13, CAST(150.00 AS Decimal(11, 2)), 20, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (32, 2, 8, 16, CAST(280.00 AS Decimal(11, 2)), 0, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (33, 2, 8, 15, CAST(250.00 AS Decimal(11, 2)), 20, NULL)
GO
INSERT [dbo].[OrderProducts] ([Id], [Quantity], [OrderId], [ProductId], [Price], [Discounts], [UserId]) VALUES (34, 1, 8, 14, CAST(150.00 AS Decimal(11, 2)), 10, NULL)
GO
SET IDENTITY_INSERT [dbo].[OrderProducts] OFF
GO
SET IDENTITY_INSERT [dbo].[Orders] ON 
GO
INSERT [dbo].[Orders] ([Id], [ReferenceNo], [Payment], [CustomerRequest], [TotalAmount], [IsPaid], [Discounts], [SeniorCitizenID], [PWDID], [UserId], [DateCreated]) VALUES (7, N'#SKS7', N'Cash', N'asdas', CAST(282.00 AS Decimal(11, 2)), 1, 10, N'0123456789', N'0123456789', NULL, CAST(N'2023-10-18T13:36:25.543' AS DateTime))
GO
INSERT [dbo].[Orders] ([Id], [ReferenceNo], [Payment], [CustomerRequest], [TotalAmount], [IsPaid], [Discounts], [SeniorCitizenID], [PWDID], [UserId], [DateCreated]) VALUES (8, N'#SKS8', N'Membership Card', N'This is a sample customer request', CAST(1767.00 AS Decimal(11, 2)), 0, 10, N'0123456789', NULL, N'7161b8ef-2428-4a01-ad13-d2bdba7fc091', CAST(N'2023-10-18T13:37:27.793' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Orders] OFF
GO
SET IDENTITY_INSERT [dbo].[Persons] ON 
GO
INSERT [dbo].[Persons] ([Id], [LastName], [FirstName], [MiddleName], [Email], [Birthday], [CompleteAddress], [ContactNumber], [Gender], [IsAdmin], [IsStaff], [IsMember], [Wallet], [CardNumber], [IsSeniorCitizen], [SeniorCitizenID], [IsPWD], [PWDID], [AccumulatedRewards], [Salary], [DateCreated], [UserId]) VALUES (1, N'Perez', N'Mark Anthony', N'Laceda', N'markperez06.ph@gmail.com', CAST(N'2023-11-10T00:00:00.000' AS DateTime), N'Sitio Pag-Asa Tibag Tarlac City', N'091234567890', N'Male', 0, 0, 1, CAST(10000.00 AS Decimal(11, 2)), N'1234567890', 0, N'', 0, N'', CAST(12.24 AS Decimal(11, 2)), CAST(18000.00 AS Decimal(11, 2)), CAST(N'2023-11-10T00:00:00.000' AS DateTime), N'7161b8ef-2428-4a01-ad13-d2bdba7fc091')
GO
SET IDENTITY_INSERT [dbo].[Persons] OFF
GO
SET IDENTITY_INSERT [dbo].[Products] ON 
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (11, N'Breakfast Meal', N'Breakfast Meal', CAST(150.00 AS Decimal(11, 2)), N'/Products/01c7326b-b390-4fdf-8c43-07fca7f15d7d_prod-5.png', 5, 10, 1, CAST(N'2023-10-14T00:31:35.580' AS DateTime), 1, 1)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (12, N'Drinks', N'Drinks', CAST(90.00 AS Decimal(11, 2)), N'/Products/31248576-cb97-4f4b-80c1-2f0d63bdd3f5_prod.png', 3, 10, 1, CAST(N'2023-10-14T00:25:18.120' AS DateTime), 1, 3)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (13, N'Lunch Meal', N'Lunch Meal', CAST(150.00 AS Decimal(11, 2)), N'/Products/1cbc5b7e-21cb-4ae5-b8a7-3458a143c034_prod-1.png', 1, 20, 1, CAST(N'2023-10-14T00:25:29.147' AS DateTime), 1, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (14, N'Lunch Meal', N'Lunch Meal', CAST(150.00 AS Decimal(11, 2)), N'/Products/98229999-78ad-48a3-9e9b-2dc524eaff40_prod-2.png', 0, 10, 1, NULL, 1, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (15, N'Lunch Meal', N'Lunch Meal', CAST(250.00 AS Decimal(11, 2)), N'/Products/b9bf2fce-f993-490c-ba96-e8087853fa03_prod-3.png', 0, 20, 1, NULL, 1, 2)
GO
INSERT [dbo].[Products] ([Id], [Name], [Description], [Price], [ImageURL], [ProductRating], [Discounts], [IsActive], [DateCreated], [UnitId], [MenuId]) VALUES (16, N'Lunch Meal', N'Lunch Meal', CAST(280.00 AS Decimal(11, 2)), N'/Products/dc5c3765-59b0-40e6-94d4-feab761b22ed_prod-4.png', 4, 0, 1, CAST(N'2023-10-14T00:32:24.653' AS DateTime), 1, 6)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Units] ON 
GO
INSERT [dbo].[Units] ([Id], [Name], [Code], [IsActive], [DateCreated]) VALUES (1, N'Unit', N'Code', 1, CAST(N'2023-10-11T21:42:17.420' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[Units] OFF
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
/****** Object:  StoredProcedure [dbo].[Create_Model]    Script Date: 18/10/2023 3:19:28 pm ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE proc [dbo].[Create_Model] (@table as VARCHAR(100))
as
declare @TableName as sysname
set @TableName = @table
declare @Result as varchar(max)
set @Result = 'public class ' + @TableName + '{'
select @Result = @Result + '
    public ' + ColumnType + NullableSign + ' ' + ColumnName + ' { get; set; }'
from
(
    select 
        replace(col.name, ' ', '_') ColumnName,
        column_id ColumnId,
        case typ.name 
            when 'bigint' then 'long'
            when 'binary' then 'byte[]'
            when 'bit' then 'bool'
            when 'char' then 'string'
            when 'date' then 'DateTime'
            when 'datetime' then 'DateTime'
            when 'datetime2' then 'DateTime'
            when 'datetimeoffset' then 'DateTimeOffset'
            when 'decimal' then 'decimal'
            when 'float' then 'float'
            when 'image' then 'byte[]'
            when 'int' then 'int'
            when 'money' then 'decimal'
            when 'nchar' then 'string'
            when 'ntext' then 'string'
            when 'numeric' then 'decimal'
            when 'nvarchar' then 'string'
            when 'real' then 'double'
            when 'smalldatetime' then 'DateTime'
            when 'smallint' then 'short'
            when 'smallmoney' then 'decimal'
            when 'text' then 'string'
            when 'time' then 'TimeSpan'
            when 'timestamp' then 'DateTime'
            when 'tinyint' then 'byte'
            when 'uniqueidentifier' then 'Guid'
            when 'varbinary' then 'byte[]'
            when 'varchar' then 'string'
            else 'UNKNOWN_' + typ.name
        end ColumnType,
        case 
            when col.is_nullable = 1 and typ.name in ('bigint', 'bit', 'date', 'datetime', 'datetime2', 'datetimeoffset', 'decimal', 'float', 'int', 'money', 'numeric', 'real', 'smalldatetime', 'smallint', 'smallmoney', 'time', 'tinyint', 'uniqueidentifier') 
            then '?' 
            else '' 
        end NullableSign
    from sys.columns col
        join sys.types typ on
            col.system_type_id = typ.system_type_id AND col.user_type_id = typ.user_type_id
    where object_id = object_id(@TableName)
) t
order by ColumnId

set @Result = @Result  + '
}'

print @Result



GO
